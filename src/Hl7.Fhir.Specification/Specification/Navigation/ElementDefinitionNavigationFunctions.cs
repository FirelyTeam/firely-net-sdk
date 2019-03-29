/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation
{
    // Static path navigation utility functions
    public partial class ElementDefinitionNavigator
    {
        private static bool isDeeperPath(string me, string that) => NumberOfParts(that) > NumberOfParts(me);

        /// <summary>Determines if the specified element paths represent sibling elements.</summary>
        public static bool IsSibling(string me, string him) => GetParentPath(me) == GetParentPath(him);

        /// <summary>Determines if the specified child path represent a direct child of the specified parent path.</summary>
        public static bool IsDirectChildPath(string parent, string child) => IsChildPath(parent, child) && child.IndexOf('.', parent.Length + 1) == -1;
        // => return child.StartsWith(parent + ".") && child.IndexOf('.', parent.Length + 1) == -1;
        // A child with a single path segment, is "root" and child of "no" parent
        //if (parent == String.Empty && child.IndexOf('.') == -1) return true;

        /// <summary>Determines if the specified child path represent a (direct/grand) child of the specified parent path.</summary>
        public static bool IsChildPath(string parent, string child) => child.StartsWith(parent + ".");

        /// <summary>Returns the parent path of the specified element path, or an empty string.</summary>
        public static string GetParentPath(string child)
        {
            var dot = child.LastIndexOf(".");
            return dot != -1 ? child.Substring(0, dot) : String.Empty;
        }

        /// <summary>Determines if the specified element path represents a root element.</summary>
        public static bool IsRootPath(string path) => !string.IsNullOrEmpty(path) && !path.Contains(".");

        /// <summary>Determines if the specified element path represents a (modifier) extension element.</summary>
        /// <returns><c>true</c> if <paramref name="path"/> ends with <c>.extension</c> or <c>.modifierExtension</c>, or <c>false</c> otherwise.</returns>
        public static bool IsExtensionPath(string path) => !string.IsNullOrEmpty(path) && (path.EndsWith(".extension") || path.EndsWith(".modifierExtension"));

        /// <summary>Returns the root component of the specified element path.</summary>
        /// <param name="path">An element path.</param>
        /// <returns>A root path.</returns>
        public static string GetPathRoot(string path)
        {
            var dot = path.IndexOf('.');
            return dot > 0 ? path.Substring(0, dot) : path;
        }

        /// <summary>Replace the root component of the specified element path.</summary>
        /// <param name="path">An element path.</param>
        /// <param name="newRoot">The new path root.</param>
        /// <returns>An element path.</returns>
        public static string ReplacePathRoot(string path, string newRoot)
        {
            var dot = path.IndexOf('.');
            return dot > 0 ? newRoot + path.Substring(dot) : newRoot;
        }

        /// <summary>Returns the hierarchical depth of the specified element path, i.e. the number of path segments.</summary>
        internal static int NumberOfParts(string path)
        {
            var count = 1;
            for (var i = 0; i < path.Length; i++)
                if (path[i] == '.') count++;

            return count;
        }

        /// <summary>Returns the last path component of the specified element path, or the empty string.</summary>
        public static string GetLastPathComponent(string path)
        {
            if (string.IsNullOrEmpty(path)) { return string.Empty; }
            var pos = path.LastIndexOf(".");
            return pos > -1 ? path.Substring(pos + 1) : path;
        }

        /// <summary>Determines if the specified element path represents a choice type element.</summary>
        /// <returns><c>true</c> if <paramref name="path"/> ends with <c>[x]</c>, or <c>false</c> otherwise.</returns>
        public static bool IsChoiceTypeElement(string path)
        {
            return path != null && path.EndsWith("[x]");
        }

        /// <summary>Determines if an element name matches a choice element name in the base profile.</summary>
        /// <param name="choiceName">A choice type element name that ends with <c>[x]</c>.</param>
        /// <param name="otherName">An element name.</param>
        /// <example><code>IsRenamedChoiceTypeElement("value[x]", "valueCodeableConcept")</code></example>
        public static bool IsRenamedChoiceTypeElement(string choiceName, string otherName)
        {
            return otherName != null
                && IsChoiceTypeElement(choiceName)
                && otherName.Length > (choiceName.Length - 3)
                && String.Compare(choiceName, 0, otherName, 0, choiceName.Length - 3) == 0;
        }

        /// <summary>Determines if the specified element path matches a base element path.</summary>
        /// <param name="basePath">A base element path.</param>
        /// <param name="path">An derived element path.</param>
        /// <example><code>
        /// IsCandidateBasePath("DomainResource.meta", "Patient.meta")
        /// IsCandidateBasePath("Extension.value[x]", "Extension.valueBoolean")
        /// IsCandidateBasePath("Element.id", "Extension.url.id")
        /// </code></example>
        internal static bool IsCandidateBasePath(string basePath, string path)
        {
            var dot1 = basePath != null ? basePath.LastIndexOf('.') : -1;
            var dot2 = path != null ? path.LastIndexOf('.') : -1;
            if (dot1 > 0 && dot2 > 0)
            {
                var basePathPart = basePath.Substring(dot1 + 1);
                var pathPart = path.Substring(dot2 + 1);
                return basePathPart == pathPart || IsRenamedChoiceTypeElement(basePathPart, pathPart);
            }
            return !string.IsNullOrEmpty(basePath)
                && !string.IsNullOrEmpty(path)
                && dot1 == -1 && dot2 == -1;
                // && !ModelInfo.IsCoreModelType(baseElementPath);
        }


        // Helper functions for (re-)slicing


        private const char RESLICE_NAME_SEPARATOR_CHAR = '/';
        private const string RESLICE_NAME_SEPARATOR = "/";

        //static bool IsInvalidSliceName(string sliceName) => sliceName != null && sliceName.StartsWith("/") || sliceName.Contains("//") || sliceName.EndsWith("/");

        /// <summary>Determines if the specified element name represents a reslice: "slice/reslice[/reslice2...]".</summary>
        public static bool IsResliceName(string sliceName) => sliceName != null && sliceName.Contains(RESLICE_NAME_SEPARATOR);

        /// <summary>Extracts the name of the base slice from a reslicing constraint name.</summary>
        /// <returns>The name of the base slice, or <c>null</c>.</returns>
        /// <example><code>
        /// GetBaseSliceName("A/B") == "A"
        /// GetBaseSliceName("A/B/C") == "A/B"
        /// GetBaseSliceName("A") == null
        /// </code></example>
        public static string GetBaseSliceName(string resliceName)
        {
            if (resliceName != null)
            {
                var pos = resliceName.LastIndexOf(RESLICE_NAME_SEPARATOR);
                if (pos >= 0)
                {
                    return resliceName.Substring(0, pos);
                }
            }
            return null;
        }

        /// <summary>Determines if the specified slice name represents a reslice of an existing slice.</summary>
        /// <param name="sliceName">The name of the candidate slice.</param>
        /// <param name="baseSliceName">The name of an existing slice.</param>
        /// <returns><c>true</c> if <paramref name="sliceName"/> is a reslice of <paramref name="baseSliceName"/>, or <c>false</c> otherwise.</returns>
        /// <example><code>
        /// IsDirectResliceOf("A/B", "A") == true
        /// 
        /// IsDirectResliceOf("A", "A") == false
        /// IsDirectResliceOf("B/A", "A") == false
        /// IsDirectResliceOf("A/B/C", "A") == false
        /// </code></example>
        public static bool IsDirectResliceOf(string sliceName, string baseSliceName)
        {
            return sliceName != null
                && baseSliceName != null
                && sliceName.Length > baseSliceName.Length + 1
                && string.CompareOrdinal(sliceName, 0, baseSliceName, 0, baseSliceName.Length) == 0
                && sliceName[baseSliceName.Length] == RESLICE_NAME_SEPARATOR_CHAR
                && sliceName.IndexOf(RESLICE_NAME_SEPARATOR, baseSliceName.Length + 1, StringComparison.Ordinal) == -1;
        }

        /// <summary>Determines if the specified slice name represents a (nested) reslice of an existing slice.</summary>
        /// <param name="sliceName">The name of the candidate slice.</param>
        /// <param name="baseSliceName">The name of an existing slice.</param>
        /// <returns><c>true</c> if <paramref name="sliceName"/> is a (nested) reslice of <paramref name="baseSliceName"/>, or <c>false</c> otherwise.</returns>
        /// <example><code>
        /// IsResliceOf("A/B", "A") == true
        /// IsResliceOf("A/B/C", "A") == true
        /// 
        /// IsResliceOf("A", "A") == false
        /// IsResliceOf("B/A", "A") == false
        /// </code></example>
        public static bool IsResliceOf(string sliceName, string baseSliceName)
        {
            return sliceName != null
                && baseSliceName != null
                && sliceName.Length > baseSliceName.Length + 1
                && string.CompareOrdinal(sliceName, 0, baseSliceName, 0, baseSliceName.Length) == 0
                && sliceName[baseSliceName.Length] == RESLICE_NAME_SEPARATOR_CHAR;
        }

        /// <summary>Determines if the specified slice names represent sibling slices.</summary>
        /// <returns><c>true</c> if <paramref name="sliceName"/> represents a sibling slice of <paramref name="siblingSliceName"/>, or <c>false</c> otherwise.</returns>
        /// <example><code>
        /// IsSiblingSliceOf("A", "B") == true
        /// IsSiblingSliceOf("A", null) == true
        /// IsSiblingSliceOf("A/1", "A/2") == true
        /// 
        /// IsSiblingSliceOf("A", "A") == false
        /// IsSiblingSliceOf("A/1", "A") == false
        /// </code></example>
        public static bool IsSiblingSliceOf(string sliceName, string siblingSliceName)
        {
            if (string.IsNullOrEmpty(sliceName))
            {
                return !string.IsNullOrEmpty(siblingSliceName) && !IsResliceName(siblingSliceName);
            }
            if (string.IsNullOrEmpty(siblingSliceName))
            {
                return !string.IsNullOrEmpty(sliceName) && !IsResliceName(sliceName);
            }

            var baseName = GetBaseSliceName(sliceName);
            var siblingBaseName = GetBaseSliceName(siblingSliceName);
            if (baseName == null && siblingBaseName == null)
            {
                return !StringComparer.Ordinal.Equals(sliceName, siblingSliceName);
            }

            if (StringComparer.Ordinal.Equals(baseName, siblingBaseName))
            {
                var resliceName = sliceName.Substring(baseName.Length);
                var siblingResliceName = siblingSliceName.Substring(siblingBaseName.Length);
                return !StringComparer.Ordinal.Equals(resliceName, siblingResliceName);
            }

            return false;
        }
    }
}
