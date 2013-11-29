/*
  Copyright (c) 2011-2013, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using Hl7.Fhir.Model;
using System.Reflection;
using Hl7.Fhir.Client;


namespace Hl7.Fhir.Support
{
    public class ResourceLocation
    {
        public const string RESTOPER_METADATA = "metadata";
        public const string RESTOPER_HISTORY = "history";
        public const string RESTOPER_SEARCH = "search";
        public const string RESTOPER_BINARY = "binary";
        public const string RESTOPER_VALIDATE = "validate";
        public const string RESTOPER_TAGS = "tags";
       
        public static string GetCollectionNameForResource(Resource r)
        {
            if (r == null) return null;

            return GetCollectionNameForResource(r.GetType());
        }

        public static string GetCollectionNameForResource(Type t)
        {
            if (typeof(Resource).IsAssignableFrom(t))
                return ModelInfo.GetResourceNameForType(t).ToLower();
            else
                throw new ArgumentException(String.Format("Cannot determine collection name, type {0} is " +
                        "not a resource type", t.Name));
        }

        public static string GetCollectionName(ResourceType t)
        {
            return t.ToString().ToLower();
        }


         //No, this is not Path.Combine. It's for Uri's
        public static string Combine(string path1, string path2)
        {
            if (String.IsNullOrEmpty(path1)) return path2;
            if (String.IsNullOrEmpty(path2)) return path1;

            return path1.TrimEnd('/') + "/" + path2.TrimStart('/');
        }

        /// <summary>
        /// Determine whether the url in location is absolute (specifies protocol, hostname and path)
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static bool IsAbsolute(string location)
        {
            return new Uri(location, UriKind.RelativeOrAbsolute).IsAbsoluteUri;
        }

        /// <summary>
        /// Determine whether location is absolute (specifies protocol, hostname and path)
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static bool IsAbsolute(Uri location)
        {
            return location.IsAbsoluteUri;
        }

        private UriBuilder _location;


        /// <summary>
        /// Construct a ResourceLocation with all of its parts empty
        /// </summary>
        public ResourceLocation()
        {
            _location = new UriBuilder();
        }


        /// <summary>
        /// Construct a new ResourceLocation with parts filled according to the specified location
        /// </summary>
        /// <param name="location">A string containing an absolute or relative url</param>
        /// <remarks>
        /// * This constructor will parse the location to not only find it usual Uri parts 
        /// (host, path, query, etc), but also its Fhir-specific parts, like collection, service path, 
        /// identifier, version and REST operation.
        /// * If the location is relative, http://localhost is assumed to be the protocol and host.
        /// </remarks>
        public ResourceLocation(string location) : this(new Uri(location, UriKind.RelativeOrAbsolute))
        {
        }


        /// <summary>
        /// Construct a new ResourceLocation with parts filled according to the specified location
        /// </summary>
        /// <param name="location"></param>
        /// <seealso cref="ResourceLocation.#ctor(System.String)"/>
        public ResourceLocation(Uri location)
        {
           if (!location.IsAbsoluteUri)
                construct(new Uri(LOCALHOST, location));
            else
                construct(location);
        }

        /// <summary>
        /// Construct a new ResourceLocation based on an absolute base path and a relative location.
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="location"></param>
        public ResourceLocation(string basePath, string location)
            : this(new Uri(basePath, UriKind.RelativeOrAbsolute), location)
        {
        }

        public ResourceLocation(Uri baseUri, string location)
            : this(baseUri, new Uri(location, UriKind.RelativeOrAbsolute))
        {
        }

        /// <summary>
        /// Construct a ResourceLocation based on a baseUri and a relative path
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="location"></param>
        /// <remarks>
        /// The relative path is appended after the baseUri, resolving any backtracks and path separators,
        /// so if the relative path starts with a '/', it will be appended right after the host part of
        /// the baseUri, even if the baseUri would have contained paths parts itself.
        /// 
        /// If the relative path contains backtracks (..), these will all be resolved, by following the
        /// backtrack. The resulting ResourceLocation will not contain backtracks.
        /// 
        /// Note that the final '/' for the baseUri is relevant: depending on its presence the last part
        /// of the baseUri is either a resource of a path part. To avoid confusing, this function
        /// assumes the baseUri is always a path, not a resource location
        /// </remarks>
        public ResourceLocation(Uri baseUri, Uri location)
        {
            if (!baseUri.IsAbsoluteUri)
                throw new ArgumentException("basePath must be an absolute Uri", "baseUri");

            if (location.IsAbsoluteUri)
                throw new ArgumentException("location must be a relative Uri", "location");

            if (!baseUri.ToString().EndsWith("/"))
                baseUri = new Uri(baseUri.ToString() + "/");

            construct(new Uri(baseUri, location));
        }

        private static readonly Uri LOCALHOST = new Uri("http://localhost");


        /// <summary>
        /// Build a ResourceLocation based on the name of a collection.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        /// <remarks>ResourceLocations are always absolute, its host will be http://localhost</remarks>
        public static ResourceLocation Build(string collectionName)
        {
            return Build(LOCALHOST, collectionName);
        }


        /// <summary>
        /// Build a ResourceLocation based on an absolute endpoint and the name of a collection.
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static ResourceLocation Build(Uri baseUri, string collectionName)
        {
            if (!baseUri.IsAbsoluteUri)
                throw new ArgumentException("baseUri must be absolute", "baseUri");

            if (String.IsNullOrEmpty(collectionName))
                throw new ArgumentException("collection must not be empty", "collectionName");

            return new ResourceLocation(baseUri, collectionName + "/");
        }


        /// <summary>
        /// Build a ResourceLocation based on the name of a collection and a resource id
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="collectionName"></param>
        /// <param name="id">The id, without '@'</param>
        /// <returns></returns>
        public static ResourceLocation Build(string collectionName, string id)
        {
            return Build(LOCALHOST, collectionName, id);
        }


        /// <summary>
        /// Build a ResourceLocation based on the endpoint, the name of a collection and a resource id
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="collectionName"></param>
        /// <param name="id">The id, without '@'</param>
        /// <returns></returns>
        public static ResourceLocation Build(Uri baseUri, string collectionName, string id)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentException("id must not be empty", "id");

            var result = ResourceLocation.Build(baseUri, collectionName);
            result.Id = id;

            return result;
        }


        /// <summary>
        /// Build a new ResourceLocation based on the name of the collection, the id and version.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        /// <param name="versionId"></param>
        /// <returns></returns>       
        public static ResourceLocation Build(string collectionName, string id, string versionId)
        {
            return Build(LOCALHOST, collectionName, id, versionId);
        }


        /// <summary>
        /// Build a new ResourceLocation based on the endpoint, name of the collection, the id and version.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="id"></param>
        /// <param name="versionId"></param>
        /// <returns></returns>
        public static ResourceLocation Build(Uri baseUri, string collectionName, string id, string versionId)
        {
            if (String.IsNullOrEmpty(versionId))
                throw new ArgumentException("versionId must not be empty", "versionId");

            var result = ResourceLocation.Build(baseUri, collectionName, id);
            result.VersionId = versionId;
            var x = result.ToString();
            return result;
        }


        private void construct(Uri absolutePath)
        {
            _location = new UriBuilder(absolutePath);
            parseLocationParts();
        }

        /// <summary>
        /// The schema used in the ResourceLocation (http, mail, etc)
        /// </summary>
        public string Scheme
        {
            get { return _location.Scheme; }
            set { _location.Scheme = value; }
        }

        /// <summary>
        /// The hostname used in the ResourceLocation (e.g. www.hl7.org)
        /// </summary>
        public string Host 
        {
            get { return _location.Host; }
            set { _location.Host = value; }
        }


        public int Port
        {
            get { return _location.Port; }
            set { _location.Port = value; }
        }

        /// <summary>
        /// The path of the ResourceLocation (e.g. /svc/patient/@1)
        /// </summary>
        public string Path
        {
            get { return _location.Path; }

            set 
            { 
                _location.Path = value; 
                parseLocationParts(); 
            }
        }

        //public string Fragment
        //{
        //    get { return _location.Fragment; }
        //    set { _location.Fragment = value;  }
        //}

        /// <summary>
        /// The query part of the ResourceLocation, including the '?' (e.g. ?name=Kramer&gender=M)
        /// </summary>
        public string Query
        {
            get { return _location.Query; }
            set { _location.Query = value.TrimStart('?'); }
        }


        /// <summary>
        /// The service path of a Fhir REST uri (e.g. /svc/fhir)
        /// </summary>
        private string _service;
        public string Service
        {
            get { return _service; }

            set
            {
                _service = value;
                _location.Path = buildPath();
            }
        }
        
        
        /// <summary>
        /// The specified Fhir REST operation (e.g. history, validate)
        /// </summary>
        private string _operation;
        public string Operation 
        { 
            get { return _operation; }

            set
            {
                _operation = value;
                _location.Path = buildPath();
            }
        }

        /// <summary>
        /// The collection part of a Fhir REST Url, corresponding to a Resource (patient, observation)
        /// </summary>
        private string _collection;
        public string Collection 
        {
            get { return _collection; }

            set
            {
                _collection = value;
                _location.Path = buildPath();
            }
        }
        
        /// <summary>
        /// The id part of a Fhir REST url, without the '@'
        /// </summary>
        private string _id;
        public string Id 
        {
            get { return _id; }

            set
            {
                _id = value;
                _location.Path = buildPath();
            }
        }

        private string buildPath()
        {
            var path = new StringBuilder("/");

            if (!String.IsNullOrEmpty(Service)) 
                path.Append(Service + "/");

            path.Append(buildOperationPath());

            return path.ToString();
        }

        private string buildOperationPath()
        {
            string path = String.Empty;

            if (!String.IsNullOrEmpty(Collection))
            {
                path = Collection;

                if (!String.IsNullOrEmpty(Id))
                {
                    // Exception: the VALIDATE operation sits between the collection and the id
                    if(Operation == RESTOPER_VALIDATE)
                        path += "/" + Operation;

                    path += "/@" + Id;

                    // If there's a version id, append the version, separated by the path part /history/
                    if (!String.IsNullOrEmpty(VersionId))
                        path += "/history/@" + VersionId;
                }
            }

            // All operations, except Validate, are appended at the end of the operation path
            if (!String.IsNullOrEmpty(Operation) && Operation != RESTOPER_VALIDATE )
            {
                if (path != String.Empty && !path.EndsWith("/")) path += "/";
                path += Operation;
            }

            return path;
        }

        
        /// <summary>
        /// The version part of a Fhir REST url, without the '@'
        /// </summary>
        private string _versionId;
        public string VersionId 
        {
            get { return _versionId; }

            set
            {
                _versionId = value;
                _location.Path = buildPath();
            }
        }

        private static readonly string[] resourceCollections = ModelInfo.SupportedResources.Select(res => res.ToLower()).ToArray();

        private bool isResourceCollection(string part)
        {
            return resourceCollections.Contains(part);
        }

        private void parseLocationParts()
        {
            _service = null;
            _operation = null;
            _collection = null;
            _id = null;
            _versionId = null;

            if (!String.IsNullOrEmpty(_location.Path))
            {
                var path = _location.Path.Trim('/');

                // The empty path, /, used for batch and conformance
                if (path == String.Empty)
                    return;

                // Parse <service>/<resourcetype>/@<id>(/operation(/@<version>(/operation)?)?)?
                var instancePattern = @"^(?:(.*)/)?         # 1: Capture the service path
                                        (\w+)               # 2: Capture the resourcetype (mandatory)....
                                        /@([^/]+)           # 3:...always followed by /@id
                                        (?:                 # optionally, this is followed by
                                          /(\w+)            # 4: /operation
                                          (?:               # optionally, followed by
                                            /@([^/]+)       # 5: /@version
                                            (?:             # optionally, followed by
                                              /(\w+)        # 6: /operation
                                            )?              # (optional operation)
                                          )?                # (optional @version)
                                        )?                  # (optional after id)
                                        $";              

                var match = Regex.Match(path,instancePattern, RegexOptions.RightToLeft | RegexOptions.IgnorePatternWhitespace);
              
                // unfortunately /patient/validate/@id matches this too, check for that
                if (match.Success && match.Groups[2].Value != RESTOPER_VALIDATE )
                {
                    // Match groups from back to front: versionId, history?, id, collection, service path
                    if (match.Groups[6].Success)
                        _operation = match.Groups[6].Value;
                    if (match.Groups[5].Success)
                        _versionId = match.Groups[5].Value;
                    if (match.Groups[4].Success)
                    {
                        if (match.Groups[4].Value == RESTOPER_HISTORY && _versionId != null )
                        { 
                            //Ignore history as a keyword between id and version: not a true operation, just a separator. between id and version
                            ;
                        }
                        else if (_operation != null)
                            // Two operations, both behind the id and behind the full path...unsupported
                            throw new ArgumentException("Path " + path + " contains two possible operations");
                        else
                            _operation = match.Groups[4].Value;
                    }
                    _id = match.Groups[3].Value;
                    _collection = match.Groups[2].Value;
                    if (match.Groups[1].Success)
                        _service = match.Groups[1].Value;

                    return;
                }
                                
                // Not a resource id or version-specific location, try other options...
                var parts = path.Split('/');
                var lastPart = parts.Length - 1;
                var serviceParts = parts.Length;

                // Check for <service>/<resourcetype>/validate/@id
                if (parts.Length >= 3 && parts[lastPart-1] == RESTOPER_VALIDATE)
                {
                    _operation = RESTOPER_VALIDATE;
                    _id = parts[lastPart].TrimStart('@');
                    _collection = parts[lastPart-2];
                    serviceParts = parts.Length - 3;
                }

                // Check for <service>/<resourcetype>/<operation>
                else if( parts.Length >= 2 && isResourceCollection(parts[lastPart - 1]) )
                {
                    _operation = parts[parts.Length - 1];
                    _collection = parts[parts.Length - 2];
                    serviceParts = parts.Length - 2;
                }

                // Check for <service>/<history|metadata|tags>
                else if (parts[lastPart] == ResourceLocation.RESTOPER_METADATA 
                    || parts[lastPart] == ResourceLocation.RESTOPER_HISTORY 
                    || parts[lastPart] == ResourceLocation.RESTOPER_TAGS) 
                {
                    _operation = parts[lastPart];
                    serviceParts = parts.Length - 1;                    
                }

                // Check for <service>/<resourcetype>
                else if (isResourceCollection(parts[lastPart]))
                {
                    _collection = parts[lastPart];
                    serviceParts = parts.Length - 1;
                }

                // Assume any remaining parts are part of the Service path
                _service = serviceParts > 0 ? String.Join("/", parts, 0, serviceParts) : null;
            }
        }


        /// <summary>
        /// Make a new ResourceLocation that represents a location after navigating to the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <example>If the current path is "http://hl7.org/svc/patient", NavigatingTo("../observation") will 
        /// result in a ResourceLocation of "http://hl7.org/svc/observation"</example>
        public ResourceLocation NavigateTo(string path)
        {
            return NavigateTo(new Uri(path, UriKind.RelativeOrAbsolute));
        }

        public ResourceLocation NavigateTo(Uri path)
        {
            if (path.IsAbsoluteUri)
                throw new ArgumentException("Can only navigate to relative paths", "path");

            return new ResourceLocation(new Uri(_location.Uri, path));
        }

        /// <summary>
        /// Return the absolute Uri that represents this full ResourceLocation
        /// </summary>
        /// <returns></returns>
        public Uri ToUri()
        {
            return _location.Uri;
        }

        /// <summary>
        /// Return the absolute Uri that represents this full ResourceLocation as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToUri().ToString();
        }

        /// <summary>
        /// Return the absolute Uri representing the ResourceLocation's base endpoint (e.g. http://hl7.org/fhir/svc)
        /// </summary>
        /// <returns>An absolute uri to reach the Fhir service endpoint</returns>
        public Uri ServiceUri
        {
            get
            {
                var path = Combine(_location.Uri.GetComponents(UriComponents.SchemeAndServer,UriFormat.Unescaped), Service);
                if (!path.EndsWith("/")) path = path + "/";

                return new Uri(path, UriKind.Absolute);
            }
        }

        /// <summary>
        /// Return the path of the ResourceLocation relative to the ServicePath  (e.g. patient/@1/history)
        /// </summary>
        /// <returns>A relative uri</returns>
        public Uri OperationPath
        {
            get
            {
                var path = new StringBuilder(buildOperationPath());
//                if (!String.IsNullOrEmpty(Fragment))
//                    path.Append("#" + Fragment);
                if (!String.IsNullOrEmpty(Query))
                    path.Append(Query);

                return new Uri(path.ToString(), UriKind.Relative);
            }
        }


        public static Uri BuildResourceIdPath(string collection, string id)
        {
            return ResourceLocation.Build(LOCALHOST, collection, id).OperationPath;
        }

        public static Uri BuildResourceIdPath(string collection, string id, string version)
        {
            return ResourceLocation.Build(LOCALHOST, collection, id, version).OperationPath;
        }

        public static string GetCollectionFromResourceId(Uri versionedUrl)
        {
            if (versionedUrl.IsAbsoluteUri)
                return new ResourceLocation(versionedUrl).Collection;
            else
                return new ResourceLocation(LOCALHOST, versionedUrl).Collection;
        }

        public static string GetIdFromResourceId(Uri versionedUrl)
        {
            if (versionedUrl.IsAbsoluteUri)
                return new ResourceLocation(versionedUrl).Id;
            else
                return new ResourceLocation(LOCALHOST, versionedUrl).Id;
        }


        public static string GetVersionFromResourceId(Uri versionedUrl)
        {
            if (versionedUrl.IsAbsoluteUri)
                return new ResourceLocation(versionedUrl).VersionId;
            else
                return new ResourceLocation(LOCALHOST, versionedUrl).VersionId;
        }


        public void SetParam(string key, string value)
        {
            var pars = splitParams(Query);
            pars[key] = value;
            Query = joinParams(pars);
        }

        public void ClearParam(string key)
        {
            var pars = splitParams(Query);
            pars.Remove(key);
            Query = joinParams(pars);
        }


        public void AddParam(string key, string value)
        {
            var pars = splitParams(Query);

            if(value==null) value = String.Empty;

            if (pars.ContainsKey(key))
                pars[key] = pars[key] + "," + value;
            else
                pars[key] = value;

            Query = joinParams(pars);
        }

        private static Dictionary<string,string> splitParams(string query)
        {
            var result = new Dictionary<string,string>();

            var pars = QueryParam.Split(query);

            foreach(var kv in pars)
            {
               var key = kv.Item1;
               var value = kv.Item2;
 
               if (result.ContainsKey(key))
                        result[key] = result[key] + "," + value;
                    else
                        result.Add(key, value);
            }

            return result;
        }


        private static string joinParams(Dictionary<string,string> pars)
        {
            var result = new List<Tuple<string, string>>();

            foreach (var paramName in pars.Keys)
            {
                var paramContent = pars[paramName];
                var values = paramContent.Split(',');

                foreach (var value in values)
                    result.Add(new Tuple<string, string>(paramName, value));
            }

            return QueryParam.Join(result);
        }


        public void AddParams(IEnumerable<Tuple<string,string>> parameters)
        {
            foreach (var kv in parameters)
                AddParam(kv.Item1, kv.Item2);
        }

        public void SetParams(IEnumerable<Tuple<string,string>> parameters)
        {
            ClearParams();

            AddParams(parameters);
        }

        public void ClearParams()
        {
            Query = String.Empty;
        }
    }
}
