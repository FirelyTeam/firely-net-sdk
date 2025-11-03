/*
  Copyright (c) 2011-2012, HL7, Inc
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

#nullable enable

using System;
using System.Diagnostics;
using System.Security.AccessControl;

namespace Hl7.Fhir.Model;


[DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
public class SearchParamDefinition
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
    {
        get => $"{Resource} {Name} {Type} ({Expression})";
    }

    public string? Resource { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Url { get; set; }
    public Markdown? Description { get; set; }
    public SearchParamType Type { get; set; }

    /// <summary>
    /// One or more paths into the Resource instance that the search parameter
    /// uses
    /// </summary>
    public string[]? Path { get; set; }

    /// <summary>
    /// The XPath expression for evaluating this search parameter
    /// </summary>
    public string? XPath { get; set; }

    /// <summary>
    /// The FHIR Path expresssion that can be used to extract the data
    /// for this search parameter
    /// </summary>
    public string? Expression { get; set; }

    /// <summary>
    /// If this is a reference, the possible types of resources that the
    /// parameters references to
    /// </summary>
    public VersionIndependentResourceTypesAll[]? Target { get; set; }

    /// <summary>
    /// Used to define the parts of a composite search parameter.
    /// </summary>
    public SearchParamComponent[]? Component { get; set; }
}

public readonly record struct SearchParamComponent(string Definition, string Expression);