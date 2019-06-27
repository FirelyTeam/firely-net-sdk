using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

/*
  Copyright (c) 2011+, HL7, Inc.
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

#pragma warning disable 1591 // suppress XML summary warnings 

//
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A set of rules about how FHIR is used
    /// </summary>
    [FhirType("ImplementationGuide", IsResource=true)]
    [DataContract]
    public partial class ImplementationGuide : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ImplementationGuide; } }
        [NotMapped]
        public override string TypeName { get { return "ImplementationGuide"; } }
        
        /// <summary>
        /// The license that applies to an Implementation Guide (using an SPDX license Identifiers, or 'not-open-source'). The binding is required but new SPDX license Identifiers are allowed to be used (https://spdx.org/licenses/).
        /// (url: http://hl7.org/fhir/ValueSet/spdx-license)
        /// </summary>
        [FhirEnumeration("SPDXLicense")]
        public enum SPDXLicense
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("not-open-source", "http://hl7.org/fhir/spdx-license"), Description("Not open source")]
            NotOpenSource,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("0BSD", "http://hl7.org/fhir/spdx-license"), Description("BSD Zero Clause License")]
            N0BSD,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AAL", "http://hl7.org/fhir/spdx-license"), Description("Attribution Assurance License")]
            AAL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Abstyles", "http://hl7.org/fhir/spdx-license"), Description("Abstyles License")]
            Abstyles,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Adobe-2006", "http://hl7.org/fhir/spdx-license"), Description("Adobe Systems Incorporated Source Code License Agreement")]
            Adobe2006,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Adobe-Glyph", "http://hl7.org/fhir/spdx-license"), Description("Adobe Glyph List License")]
            AdobeGlyph,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ADSL", "http://hl7.org/fhir/spdx-license"), Description("Amazon Digital Services License")]
            ADSL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AFL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v1.1")]
            AFL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AFL-1.2", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v1.2")]
            AFL1_2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AFL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v2.0")]
            AFL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AFL-2.1", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v2.1")]
            AFL2_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AFL-3.0", "http://hl7.org/fhir/spdx-license"), Description("Academic Free License v3.0")]
            AFL3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Afmparse", "http://hl7.org/fhir/spdx-license"), Description("Afmparse License")]
            Afmparse,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AGPL-1.0-only", "http://hl7.org/fhir/spdx-license"), Description("Affero General Public License v1.0 only")]
            AGPL1_0Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AGPL-1.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("Affero General Public License v1.0 or later")]
            AGPL1_0OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AGPL-3.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Affero General Public License v3.0 only")]
            AGPL3_0Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AGPL-3.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Affero General Public License v3.0 or later")]
            AGPL3_0OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Aladdin", "http://hl7.org/fhir/spdx-license"), Description("Aladdin Free Public License")]
            Aladdin,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AMDPLPA", "http://hl7.org/fhir/spdx-license"), Description("AMD's plpa_map.c License")]
            AMDPLPA,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AML", "http://hl7.org/fhir/spdx-license"), Description("Apple MIT License")]
            AML,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("AMPAS", "http://hl7.org/fhir/spdx-license"), Description("Academy of Motion Picture Arts and Sciences BSD")]
            AMPAS,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ANTLR-PD", "http://hl7.org/fhir/spdx-license"), Description("ANTLR Software Rights Notice")]
            ANTLRPD,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Apache-1.0", "http://hl7.org/fhir/spdx-license"), Description("Apache License 1.0")]
            Apache1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Apache-1.1", "http://hl7.org/fhir/spdx-license"), Description("Apache License 1.1")]
            Apache1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Apache-2.0", "http://hl7.org/fhir/spdx-license"), Description("Apache License 2.0")]
            Apache2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("APAFML", "http://hl7.org/fhir/spdx-license"), Description("Adobe Postscript AFM License")]
            APAFML,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("APL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Adaptive Public License 1.0")]
            APL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("APSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Apple Public Source License 1.0")]
            APSL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("APSL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Apple Public Source License 1.1")]
            APSL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("APSL-1.2", "http://hl7.org/fhir/spdx-license"), Description("Apple Public Source License 1.2")]
            APSL1_2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("APSL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Apple Public Source License 2.0")]
            APSL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Artistic-1.0-cl8", "http://hl7.org/fhir/spdx-license"), Description("Artistic License 1.0 w/clause 8")]
            Artistic1_0Cl8,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Artistic-1.0-Perl", "http://hl7.org/fhir/spdx-license"), Description("Artistic License 1.0 (Perl)")]
            Artistic1_0Perl,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Artistic-1.0", "http://hl7.org/fhir/spdx-license"), Description("Artistic License 1.0")]
            Artistic1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Artistic-2.0", "http://hl7.org/fhir/spdx-license"), Description("Artistic License 2.0")]
            Artistic2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Bahyph", "http://hl7.org/fhir/spdx-license"), Description("Bahyph License")]
            Bahyph,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Barr", "http://hl7.org/fhir/spdx-license"), Description("Barr License")]
            Barr,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Beerware", "http://hl7.org/fhir/spdx-license"), Description("Beerware License")]
            Beerware,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BitTorrent-1.0", "http://hl7.org/fhir/spdx-license"), Description("BitTorrent Open Source License v1.0")]
            BitTorrent1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BitTorrent-1.1", "http://hl7.org/fhir/spdx-license"), Description("BitTorrent Open Source License v1.1")]
            BitTorrent1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Borceux", "http://hl7.org/fhir/spdx-license"), Description("Borceux license")]
            Borceux,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-1-Clause", "http://hl7.org/fhir/spdx-license"), Description("BSD 1-Clause License")]
            BSD1Clause,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-2-Clause-FreeBSD", "http://hl7.org/fhir/spdx-license"), Description("BSD 2-Clause FreeBSD License")]
            BSD2ClauseFreeBSD,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-2-Clause-NetBSD", "http://hl7.org/fhir/spdx-license"), Description("BSD 2-Clause NetBSD License")]
            BSD2ClauseNetBSD,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-2-Clause-Patent", "http://hl7.org/fhir/spdx-license"), Description("BSD-2-Clause Plus Patent License")]
            BSD2ClausePatent,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-2-Clause", "http://hl7.org/fhir/spdx-license"), Description("BSD 2-Clause 'Simplified' License")]
            BSD2Clause,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-3-Clause-Attribution", "http://hl7.org/fhir/spdx-license"), Description("BSD with attribution")]
            BSD3ClauseAttribution,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-3-Clause-Clear", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause Clear License")]
            BSD3ClauseClear,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-3-Clause-LBNL", "http://hl7.org/fhir/spdx-license"), Description("Lawrence Berkeley National Labs BSD variant license")]
            BSD3ClauseLBNL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-3-Clause-No-Nuclear-License-2014", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause No Nuclear License 2014")]
            BSD3ClauseNoNuclearLicense2014,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-3-Clause-No-Nuclear-License", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause No Nuclear License")]
            BSD3ClauseNoNuclearLicense,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-3-Clause-No-Nuclear-Warranty", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause No Nuclear Warranty")]
            BSD3ClauseNoNuclearWarranty,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-3-Clause", "http://hl7.org/fhir/spdx-license"), Description("BSD 3-Clause 'New' or 'Revised' License")]
            BSD3Clause,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-4-Clause-UC", "http://hl7.org/fhir/spdx-license"), Description("BSD-4-Clause (University of California-Specific)")]
            BSD4ClauseUC,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-4-Clause", "http://hl7.org/fhir/spdx-license"), Description("BSD 4-Clause 'Original' or 'Old' License")]
            BSD4Clause,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-Protection", "http://hl7.org/fhir/spdx-license"), Description("BSD Protection License")]
            BSDProtection,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSD-Source-Code", "http://hl7.org/fhir/spdx-license"), Description("BSD Source Code Attribution")]
            BSDSourceCode,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("BSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Boost Software License 1.0")]
            BSL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("bzip2-1.0.5", "http://hl7.org/fhir/spdx-license"), Description("bzip2 and libbzip2 License v1.0.5")]
            Bzip21_0_5,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("bzip2-1.0.6", "http://hl7.org/fhir/spdx-license"), Description("bzip2 and libbzip2 License v1.0.6")]
            Bzip21_0_6,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Caldera", "http://hl7.org/fhir/spdx-license"), Description("Caldera License")]
            Caldera,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CATOSL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Computer Associates Trusted Open Source License 1.1")]
            CATOSL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 1.0 Generic")]
            CCBY1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 2.0 Generic")]
            CCBY2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 2.5 Generic")]
            CCBY2_5,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 3.0 Unported")]
            CCBY3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution 4.0 International")]
            CCBY4_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 1.0 Generic")]
            CCBYNC1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 2.0 Generic")]
            CCBYNC2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 2.5 Generic")]
            CCBYNC2_5,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 3.0 Unported")]
            CCBYNC3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial 4.0 International")]
            CCBYNC4_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-ND-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 1.0 Generic")]
            CCBYNCND1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-ND-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 2.0 Generic")]
            CCBYNCND2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-ND-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 2.5 Generic")]
            CCBYNCND2_5,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-ND-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 3.0 Unported")]
            CCBYNCND3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-ND-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial No Derivatives 4.0 International")]
            CCBYNCND4_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-SA-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 1.0 Generic")]
            CCBYNCSA1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-SA-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 2.0 Generic")]
            CCBYNCSA2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-SA-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 2.5 Generic")]
            CCBYNCSA2_5,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-SA-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 3.0 Unported")]
            CCBYNCSA3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-NC-SA-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Non Commercial Share Alike 4.0 International")]
            CCBYNCSA4_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-ND-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 1.0 Generic")]
            CCBYND1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-ND-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 2.0 Generic")]
            CCBYND2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-ND-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 2.5 Generic")]
            CCBYND2_5,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-ND-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 3.0 Unported")]
            CCBYND3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-ND-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution No Derivatives 4.0 International")]
            CCBYND4_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-SA-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 1.0 Generic")]
            CCBYSA1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-SA-2.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 2.0 Generic")]
            CCBYSA2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-SA-2.5", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 2.5 Generic")]
            CCBYSA2_5,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-SA-3.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 3.0 Unported")]
            CCBYSA3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC-BY-SA-4.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Attribution Share Alike 4.0 International")]
            CCBYSA4_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CC0-1.0", "http://hl7.org/fhir/spdx-license"), Description("Creative Commons Zero v1.0 Universal")]
            CC01_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CDDL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Common Development and Distribution License 1.0")]
            CDDL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CDDL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Common Development and Distribution License 1.1")]
            CDDL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CDLA-Permissive-1.0", "http://hl7.org/fhir/spdx-license"), Description("Community Data License Agreement Permissive 1.0")]
            CDLAPermissive1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CDLA-Sharing-1.0", "http://hl7.org/fhir/spdx-license"), Description("Community Data License Agreement Sharing 1.0")]
            CDLASharing1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CECILL-1.0", "http://hl7.org/fhir/spdx-license"), Description("CeCILL Free Software License Agreement v1.0")]
            CECILL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CECILL-1.1", "http://hl7.org/fhir/spdx-license"), Description("CeCILL Free Software License Agreement v1.1")]
            CECILL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CECILL-2.0", "http://hl7.org/fhir/spdx-license"), Description("CeCILL Free Software License Agreement v2.0")]
            CECILL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CECILL-2.1", "http://hl7.org/fhir/spdx-license"), Description("CeCILL Free Software License Agreement v2.1")]
            CECILL2_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CECILL-B", "http://hl7.org/fhir/spdx-license"), Description("CeCILL-B Free Software License Agreement")]
            CECILLB,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CECILL-C", "http://hl7.org/fhir/spdx-license"), Description("CeCILL-C Free Software License Agreement")]
            CECILLC,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ClArtistic", "http://hl7.org/fhir/spdx-license"), Description("Clarified Artistic License")]
            ClArtistic,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CNRI-Jython", "http://hl7.org/fhir/spdx-license"), Description("CNRI Jython License")]
            CNRIJython,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CNRI-Python-GPL-Compatible", "http://hl7.org/fhir/spdx-license"), Description("CNRI Python Open Source GPL Compatible License Agreement")]
            CNRIPythonGPLCompatible,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CNRI-Python", "http://hl7.org/fhir/spdx-license"), Description("CNRI Python License")]
            CNRIPython,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Condor-1.1", "http://hl7.org/fhir/spdx-license"), Description("Condor Public License v1.1")]
            Condor1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CPAL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Common Public Attribution License 1.0")]
            CPAL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Common Public License 1.0")]
            CPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CPOL-1.02", "http://hl7.org/fhir/spdx-license"), Description("Code Project Open License 1.02")]
            CPOL1_02,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Crossword", "http://hl7.org/fhir/spdx-license"), Description("Crossword License")]
            Crossword,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CrystalStacker", "http://hl7.org/fhir/spdx-license"), Description("CrystalStacker License")]
            CrystalStacker,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("CUA-OPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("CUA Office Public License v1.0")]
            CUAOPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Cube", "http://hl7.org/fhir/spdx-license"), Description("Cube License")]
            Cube,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("curl", "http://hl7.org/fhir/spdx-license"), Description("curl License")]
            Curl,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("D-FSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Deutsche Freie Software Lizenz")]
            DFSL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("diffmark", "http://hl7.org/fhir/spdx-license"), Description("diffmark license")]
            Diffmark,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("DOC", "http://hl7.org/fhir/spdx-license"), Description("DOC License")]
            DOC,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Dotseqn", "http://hl7.org/fhir/spdx-license"), Description("Dotseqn License")]
            Dotseqn,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("DSDP", "http://hl7.org/fhir/spdx-license"), Description("DSDP License")]
            DSDP,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("dvipdfm", "http://hl7.org/fhir/spdx-license"), Description("dvipdfm License")]
            Dvipdfm,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ECL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Educational Community License v1.0")]
            ECL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ECL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Educational Community License v2.0")]
            ECL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("EFL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Eiffel Forum License v1.0")]
            EFL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("EFL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Eiffel Forum License v2.0")]
            EFL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("eGenix", "http://hl7.org/fhir/spdx-license"), Description("eGenix.com Public License 1.1.0")]
            EGenix,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Entessa", "http://hl7.org/fhir/spdx-license"), Description("Entessa Public License v1.0")]
            Entessa,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("EPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Eclipse Public License 1.0")]
            EPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("EPL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Eclipse Public License 2.0")]
            EPL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ErlPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Erlang Public License v1.1")]
            ErlPL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("EUDatagrid", "http://hl7.org/fhir/spdx-license"), Description("EU DataGrid Software License")]
            EUDatagrid,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("EUPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("European Union Public License 1.0")]
            EUPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("EUPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("European Union Public License 1.1")]
            EUPL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("EUPL-1.2", "http://hl7.org/fhir/spdx-license"), Description("European Union Public License 1.2")]
            EUPL1_2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Eurosym", "http://hl7.org/fhir/spdx-license"), Description("Eurosym License")]
            Eurosym,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Fair", "http://hl7.org/fhir/spdx-license"), Description("Fair License")]
            Fair,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Frameworx-1.0", "http://hl7.org/fhir/spdx-license"), Description("Frameworx Open License 1.0")]
            Frameworx1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("FreeImage", "http://hl7.org/fhir/spdx-license"), Description("FreeImage Public License v1.0")]
            FreeImage,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("FSFAP", "http://hl7.org/fhir/spdx-license"), Description("FSF All Permissive License")]
            FSFAP,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("FSFUL", "http://hl7.org/fhir/spdx-license"), Description("FSF Unlimited License")]
            FSFUL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("FSFULLR", "http://hl7.org/fhir/spdx-license"), Description("FSF Unlimited License (with License Retention)")]
            FSFULLR,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("FTL", "http://hl7.org/fhir/spdx-license"), Description("Freetype Project License")]
            FTL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GFDL-1.1-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.1 only")]
            GFDL1_1Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GFDL-1.1-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.1 or later")]
            GFDL1_1OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GFDL-1.2-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.2 only")]
            GFDL1_2Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GFDL-1.2-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.2 or later")]
            GFDL1_2OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GFDL-1.3-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.3 only")]
            GFDL1_3Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GFDL-1.3-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Free Documentation License v1.3 or later")]
            GFDL1_3OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Giftware", "http://hl7.org/fhir/spdx-license"), Description("Giftware License")]
            Giftware,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GL2PS", "http://hl7.org/fhir/spdx-license"), Description("GL2PS License")]
            GL2PS,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Glide", "http://hl7.org/fhir/spdx-license"), Description("3dfx Glide License")]
            Glide,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Glulxe", "http://hl7.org/fhir/spdx-license"), Description("Glulxe License")]
            Glulxe,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("gnuplot", "http://hl7.org/fhir/spdx-license"), Description("gnuplot License")]
            Gnuplot,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GPL-1.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v1.0 only")]
            GPL1_0Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GPL-1.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v1.0 or later")]
            GPL1_0OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GPL-2.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v2.0 only")]
            GPL2_0Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GPL-2.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v2.0 or later")]
            GPL2_0OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GPL-3.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v3.0 only")]
            GPL3_0Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("GPL-3.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU General Public License v3.0 or later")]
            GPL3_0OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("gSOAP-1.3b", "http://hl7.org/fhir/spdx-license"), Description("gSOAP Public License v1.3b")]
            GSOAP1_3b,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("HaskellReport", "http://hl7.org/fhir/spdx-license"), Description("Haskell Language Report License")]
            HaskellReport,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("HPND", "http://hl7.org/fhir/spdx-license"), Description("Historical Permission Notice and Disclaimer")]
            HPND,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("IBM-pibs", "http://hl7.org/fhir/spdx-license"), Description("IBM PowerPC Initialization and Boot Software")]
            IBMPibs,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ICU", "http://hl7.org/fhir/spdx-license"), Description("ICU License")]
            ICU,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("IJG", "http://hl7.org/fhir/spdx-license"), Description("Independent JPEG Group License")]
            IJG,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ImageMagick", "http://hl7.org/fhir/spdx-license"), Description("ImageMagick License")]
            ImageMagick,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("iMatix", "http://hl7.org/fhir/spdx-license"), Description("iMatix Standard Function Library Agreement")]
            IMatix,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Imlib2", "http://hl7.org/fhir/spdx-license"), Description("Imlib2 License")]
            Imlib2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Info-ZIP", "http://hl7.org/fhir/spdx-license"), Description("Info-ZIP License")]
            InfoZIP,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Intel-ACPI", "http://hl7.org/fhir/spdx-license"), Description("Intel ACPI Software License Agreement")]
            IntelACPI,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Intel", "http://hl7.org/fhir/spdx-license"), Description("Intel Open Source License")]
            Intel,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Interbase-1.0", "http://hl7.org/fhir/spdx-license"), Description("Interbase Public License v1.0")]
            Interbase1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("IPA", "http://hl7.org/fhir/spdx-license"), Description("IPA Font License")]
            IPA,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("IPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("IBM Public License v1.0")]
            IPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ISC", "http://hl7.org/fhir/spdx-license"), Description("ISC License")]
            ISC,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("JasPer-2.0", "http://hl7.org/fhir/spdx-license"), Description("JasPer License")]
            JasPer2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("JSON", "http://hl7.org/fhir/spdx-license"), Description("JSON License")]
            JSON,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LAL-1.2", "http://hl7.org/fhir/spdx-license"), Description("Licence Art Libre 1.2")]
            LAL1_2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LAL-1.3", "http://hl7.org/fhir/spdx-license"), Description("Licence Art Libre 1.3")]
            LAL1_3,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Latex2e", "http://hl7.org/fhir/spdx-license"), Description("Latex2e License")]
            Latex2e,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Leptonica", "http://hl7.org/fhir/spdx-license"), Description("Leptonica License")]
            Leptonica,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LGPL-2.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Library General Public License v2 only")]
            LGPL2_0Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LGPL-2.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Library General Public License v2 or later")]
            LGPL2_0OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LGPL-2.1-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Lesser General Public License v2.1 only")]
            LGPL2_1Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LGPL-2.1-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Lesser General Public License v2.1 or later")]
            LGPL2_1OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LGPL-3.0-only", "http://hl7.org/fhir/spdx-license"), Description("GNU Lesser General Public License v3.0 only")]
            LGPL3_0Only,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LGPL-3.0-or-later", "http://hl7.org/fhir/spdx-license"), Description("GNU Lesser General Public License v3.0 or later")]
            LGPL3_0OrLater,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LGPLLR", "http://hl7.org/fhir/spdx-license"), Description("Lesser General Public License For Linguistic Resources")]
            LGPLLR,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Libpng", "http://hl7.org/fhir/spdx-license"), Description("libpng License")]
            Libpng,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("libtiff", "http://hl7.org/fhir/spdx-license"), Description("libtiff License")]
            Libtiff,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LiLiQ-P-1.1", "http://hl7.org/fhir/spdx-license"), Description("Licence Libre du Québec – Permissive version 1.1")]
            LiLiQP1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LiLiQ-R-1.1", "http://hl7.org/fhir/spdx-license"), Description("Licence Libre du Québec – Réciprocité version 1.1")]
            LiLiQR1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LiLiQ-Rplus-1.1", "http://hl7.org/fhir/spdx-license"), Description("Licence Libre du Québec – Réciprocité forte version 1.1")]
            LiLiQRplus1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Linux-OpenIB", "http://hl7.org/fhir/spdx-license"), Description("Linux Kernel Variant of OpenIB.org license")]
            LinuxOpenIB,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Lucent Public License Version 1.0")]
            LPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LPL-1.02", "http://hl7.org/fhir/spdx-license"), Description("Lucent Public License v1.02")]
            LPL1_02,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LPPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.0")]
            LPPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LPPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.1")]
            LPPL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LPPL-1.2", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.2")]
            LPPL1_2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LPPL-1.3a", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.3a")]
            LPPL1_3a,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("LPPL-1.3c", "http://hl7.org/fhir/spdx-license"), Description("LaTeX Project Public License v1.3c")]
            LPPL1_3c,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MakeIndex", "http://hl7.org/fhir/spdx-license"), Description("MakeIndex License")]
            MakeIndex,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MirOS", "http://hl7.org/fhir/spdx-license"), Description("MirOS License")]
            MirOS,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MIT-0", "http://hl7.org/fhir/spdx-license"), Description("MIT No Attribution")]
            MIT0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MIT-advertising", "http://hl7.org/fhir/spdx-license"), Description("Enlightenment License (e16)")]
            MITAdvertising,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MIT-CMU", "http://hl7.org/fhir/spdx-license"), Description("CMU License")]
            MITCMU,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MIT-enna", "http://hl7.org/fhir/spdx-license"), Description("enna License")]
            MITEnna,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MIT-feh", "http://hl7.org/fhir/spdx-license"), Description("feh License")]
            MITFeh,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MIT", "http://hl7.org/fhir/spdx-license"), Description("MIT License")]
            MIT,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MITNFA", "http://hl7.org/fhir/spdx-license"), Description("MIT +no-false-attribs license")]
            MITNFA,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Motosoto", "http://hl7.org/fhir/spdx-license"), Description("Motosoto License")]
            Motosoto,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("mpich2", "http://hl7.org/fhir/spdx-license"), Description("mpich2 License")]
            Mpich2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Mozilla Public License 1.0")]
            MPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Mozilla Public License 1.1")]
            MPL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MPL-2.0-no-copyleft-exception", "http://hl7.org/fhir/spdx-license"), Description("Mozilla Public License 2.0 (no copyleft exception)")]
            MPL2_0NoCopyleftException,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MPL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Mozilla Public License 2.0")]
            MPL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MS-PL", "http://hl7.org/fhir/spdx-license"), Description("Microsoft Public License")]
            MSPL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MS-RL", "http://hl7.org/fhir/spdx-license"), Description("Microsoft Reciprocal License")]
            MSRL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("MTLL", "http://hl7.org/fhir/spdx-license"), Description("Matrix Template Library License")]
            MTLL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Multics", "http://hl7.org/fhir/spdx-license"), Description("Multics License")]
            Multics,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Mup", "http://hl7.org/fhir/spdx-license"), Description("Mup License")]
            Mup,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NASA-1.3", "http://hl7.org/fhir/spdx-license"), Description("NASA Open Source Agreement 1.3")]
            NASA1_3,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Naumen", "http://hl7.org/fhir/spdx-license"), Description("Naumen Public License")]
            Naumen,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NBPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Net Boolean Public License v1")]
            NBPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NCSA", "http://hl7.org/fhir/spdx-license"), Description("University of Illinois/NCSA Open Source License")]
            NCSA,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Net-SNMP", "http://hl7.org/fhir/spdx-license"), Description("Net-SNMP License")]
            NetSNMP,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NetCDF", "http://hl7.org/fhir/spdx-license"), Description("NetCDF license")]
            NetCDF,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Newsletr", "http://hl7.org/fhir/spdx-license"), Description("Newsletr License")]
            Newsletr,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NGPL", "http://hl7.org/fhir/spdx-license"), Description("Nethack General Public License")]
            NGPL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NLOD-1.0", "http://hl7.org/fhir/spdx-license"), Description("Norwegian Licence for Open Government Data")]
            NLOD1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NLPL", "http://hl7.org/fhir/spdx-license"), Description("No Limit Public License")]
            NLPL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Nokia", "http://hl7.org/fhir/spdx-license"), Description("Nokia Open Source License")]
            Nokia,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NOSL", "http://hl7.org/fhir/spdx-license"), Description("Netizen Open Source License")]
            NOSL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Noweb", "http://hl7.org/fhir/spdx-license"), Description("Noweb License")]
            Noweb,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Netscape Public License v1.0")]
            NPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Netscape Public License v1.1")]
            NPL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NPOSL-3.0", "http://hl7.org/fhir/spdx-license"), Description("Non-Profit Open Software License 3.0")]
            NPOSL3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NRL", "http://hl7.org/fhir/spdx-license"), Description("NRL License")]
            NRL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("NTP", "http://hl7.org/fhir/spdx-license"), Description("NTP License")]
            NTP,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OCCT-PL", "http://hl7.org/fhir/spdx-license"), Description("Open CASCADE Technology Public License")]
            OCCTPL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OCLC-2.0", "http://hl7.org/fhir/spdx-license"), Description("OCLC Research Public License 2.0")]
            OCLC2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ODbL-1.0", "http://hl7.org/fhir/spdx-license"), Description("ODC Open Database License v1.0")]
            ODbL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OFL-1.0", "http://hl7.org/fhir/spdx-license"), Description("SIL Open Font License 1.0")]
            OFL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OFL-1.1", "http://hl7.org/fhir/spdx-license"), Description("SIL Open Font License 1.1")]
            OFL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OGTSL", "http://hl7.org/fhir/spdx-license"), Description("Open Group Test Suite License")]
            OGTSL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-1.1", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v1.1")]
            OLDAP1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-1.2", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v1.2")]
            OLDAP1_2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-1.3", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v1.3")]
            OLDAP1_3,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-1.4", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v1.4")]
            OLDAP1_4,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.0.1", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.0.1")]
            OLDAP2_0_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.0", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.0 (or possibly 2.0A and 2.0B)")]
            OLDAP2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.1", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.1")]
            OLDAP2_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.2.1", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.2.1")]
            OLDAP2_2_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.2.2", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License 2.2.2")]
            OLDAP2_2_2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.2", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.2")]
            OLDAP2_2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.3", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.3")]
            OLDAP2_3,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.4", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.4")]
            OLDAP2_4,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.5", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.5")]
            OLDAP2_5,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.6", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.6")]
            OLDAP2_6,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.7", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.7")]
            OLDAP2_7,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OLDAP-2.8", "http://hl7.org/fhir/spdx-license"), Description("Open LDAP Public License v2.8")]
            OLDAP2_8,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OML", "http://hl7.org/fhir/spdx-license"), Description("Open Market License")]
            OML,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OpenSSL", "http://hl7.org/fhir/spdx-license"), Description("OpenSSL License")]
            OpenSSL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Open Public License v1.0")]
            OPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OSET-PL-2.1", "http://hl7.org/fhir/spdx-license"), Description("OSET Public License version 2.1")]
            OSETPL2_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 1.0")]
            OSL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OSL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 1.1")]
            OSL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OSL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 2.0")]
            OSL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OSL-2.1", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 2.1")]
            OSL2_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("OSL-3.0", "http://hl7.org/fhir/spdx-license"), Description("Open Software License 3.0")]
            OSL3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("PDDL-1.0", "http://hl7.org/fhir/spdx-license"), Description("ODC Public Domain Dedication & License 1.0")]
            PDDL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("PHP-3.0", "http://hl7.org/fhir/spdx-license"), Description("PHP License v3.0")]
            PHP3_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("PHP-3.01", "http://hl7.org/fhir/spdx-license"), Description("PHP License v3.01")]
            PHP3_01,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Plexus", "http://hl7.org/fhir/spdx-license"), Description("Plexus Classworlds License")]
            Plexus,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("PostgreSQL", "http://hl7.org/fhir/spdx-license"), Description("PostgreSQL License")]
            PostgreSQL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("psfrag", "http://hl7.org/fhir/spdx-license"), Description("psfrag License")]
            Psfrag,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("psutils", "http://hl7.org/fhir/spdx-license"), Description("psutils License")]
            Psutils,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Python-2.0", "http://hl7.org/fhir/spdx-license"), Description("Python License 2.0")]
            Python2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Qhull", "http://hl7.org/fhir/spdx-license"), Description("Qhull License")]
            Qhull,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("QPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Q Public License 1.0")]
            QPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Rdisc", "http://hl7.org/fhir/spdx-license"), Description("Rdisc License")]
            Rdisc,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("RHeCos-1.1", "http://hl7.org/fhir/spdx-license"), Description("Red Hat eCos Public License v1.1")]
            RHeCos1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("RPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Reciprocal Public License 1.1")]
            RPL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("RPL-1.5", "http://hl7.org/fhir/spdx-license"), Description("Reciprocal Public License 1.5")]
            RPL1_5,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("RPSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("RealNetworks Public Source License v1.0")]
            RPSL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("RSA-MD", "http://hl7.org/fhir/spdx-license"), Description("RSA Message-Digest License")]
            RSAMD,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("RSCPL", "http://hl7.org/fhir/spdx-license"), Description("Ricoh Source Code Public License")]
            RSCPL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Ruby", "http://hl7.org/fhir/spdx-license"), Description("Ruby License")]
            Ruby,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SAX-PD", "http://hl7.org/fhir/spdx-license"), Description("Sax Public Domain Notice")]
            SAXPD,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Saxpath", "http://hl7.org/fhir/spdx-license"), Description("Saxpath License")]
            Saxpath,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SCEA", "http://hl7.org/fhir/spdx-license"), Description("SCEA Shared Source License")]
            SCEA,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Sendmail", "http://hl7.org/fhir/spdx-license"), Description("Sendmail License")]
            Sendmail,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SGI-B-1.0", "http://hl7.org/fhir/spdx-license"), Description("SGI Free Software License B v1.0")]
            SGIB1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SGI-B-1.1", "http://hl7.org/fhir/spdx-license"), Description("SGI Free Software License B v1.1")]
            SGIB1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SGI-B-2.0", "http://hl7.org/fhir/spdx-license"), Description("SGI Free Software License B v2.0")]
            SGIB2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SimPL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Simple Public License 2.0")]
            SimPL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SISSL-1.2", "http://hl7.org/fhir/spdx-license"), Description("Sun Industry Standards Source License v1.2")]
            SISSL1_2,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SISSL", "http://hl7.org/fhir/spdx-license"), Description("Sun Industry Standards Source License v1.1")]
            SISSL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Sleepycat", "http://hl7.org/fhir/spdx-license"), Description("Sleepycat License")]
            Sleepycat,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SMLNJ", "http://hl7.org/fhir/spdx-license"), Description("Standard ML of New Jersey License")]
            SMLNJ,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SMPPL", "http://hl7.org/fhir/spdx-license"), Description("Secure Messaging Protocol Public License")]
            SMPPL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SNIA", "http://hl7.org/fhir/spdx-license"), Description("SNIA Public License 1.1")]
            SNIA,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Spencer-86", "http://hl7.org/fhir/spdx-license"), Description("Spencer License 86")]
            Spencer86,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Spencer-94", "http://hl7.org/fhir/spdx-license"), Description("Spencer License 94")]
            Spencer94,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Spencer-99", "http://hl7.org/fhir/spdx-license"), Description("Spencer License 99")]
            Spencer99,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Sun Public License v1.0")]
            SPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SugarCRM-1.1.3", "http://hl7.org/fhir/spdx-license"), Description("SugarCRM Public License v1.1.3")]
            SugarCRM1_1_3,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("SWL", "http://hl7.org/fhir/spdx-license"), Description("Scheme Widget Library (SWL) Software License Agreement")]
            SWL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("TCL", "http://hl7.org/fhir/spdx-license"), Description("TCL/TK License")]
            TCL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("TCP-wrappers", "http://hl7.org/fhir/spdx-license"), Description("TCP Wrappers License")]
            TCPWrappers,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("TMate", "http://hl7.org/fhir/spdx-license"), Description("TMate Open Source License")]
            TMate,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("TORQUE-1.1", "http://hl7.org/fhir/spdx-license"), Description("TORQUE v2.5+ Software License v1.1")]
            TORQUE1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("TOSL", "http://hl7.org/fhir/spdx-license"), Description("Trusster Open Source License")]
            TOSL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Unicode-DFS-2015", "http://hl7.org/fhir/spdx-license"), Description("Unicode License Agreement - Data Files and Software (2015)")]
            UnicodeDFS2015,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Unicode-DFS-2016", "http://hl7.org/fhir/spdx-license"), Description("Unicode License Agreement - Data Files and Software (2016)")]
            UnicodeDFS2016,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Unicode-TOU", "http://hl7.org/fhir/spdx-license"), Description("Unicode Terms of Use")]
            UnicodeTOU,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Unlicense", "http://hl7.org/fhir/spdx-license"), Description("The Unlicense")]
            Unlicense,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("UPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Universal Permissive License v1.0")]
            UPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Vim", "http://hl7.org/fhir/spdx-license"), Description("Vim License")]
            Vim,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("VOSTROM", "http://hl7.org/fhir/spdx-license"), Description("VOSTROM Public License for Open Source")]
            VOSTROM,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("VSL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Vovida Software License v1.0")]
            VSL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("W3C-19980720", "http://hl7.org/fhir/spdx-license"), Description("W3C Software Notice and License (1998-07-20)")]
            W3C19980720,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("W3C-20150513", "http://hl7.org/fhir/spdx-license"), Description("W3C Software Notice and Document License (2015-05-13)")]
            W3C20150513,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("W3C", "http://hl7.org/fhir/spdx-license"), Description("W3C Software Notice and License (2002-12-31)")]
            W3C,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Watcom-1.0", "http://hl7.org/fhir/spdx-license"), Description("Sybase Open Watcom Public License 1.0")]
            Watcom1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Wsuipa", "http://hl7.org/fhir/spdx-license"), Description("Wsuipa License")]
            Wsuipa,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("WTFPL", "http://hl7.org/fhir/spdx-license"), Description("Do What The F*ck You Want To Public License")]
            WTFPL,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("X11", "http://hl7.org/fhir/spdx-license"), Description("X11 License")]
            X11,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Xerox", "http://hl7.org/fhir/spdx-license"), Description("Xerox License")]
            Xerox,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("XFree86-1.1", "http://hl7.org/fhir/spdx-license"), Description("XFree86 License 1.1")]
            XFree861_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("xinetd", "http://hl7.org/fhir/spdx-license"), Description("xinetd License")]
            Xinetd,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Xnet", "http://hl7.org/fhir/spdx-license"), Description("X.Net License")]
            Xnet,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("xpp", "http://hl7.org/fhir/spdx-license"), Description("XPP License")]
            Xpp,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("XSkat", "http://hl7.org/fhir/spdx-license"), Description("XSkat License")]
            XSkat,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("YPL-1.0", "http://hl7.org/fhir/spdx-license"), Description("Yahoo! Public License v1.0")]
            YPL1_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("YPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Yahoo! Public License v1.1")]
            YPL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Zed", "http://hl7.org/fhir/spdx-license"), Description("Zed License")]
            Zed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Zend-2.0", "http://hl7.org/fhir/spdx-license"), Description("Zend License v2.0")]
            Zend2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Zimbra-1.3", "http://hl7.org/fhir/spdx-license"), Description("Zimbra Public License v1.3")]
            Zimbra1_3,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Zimbra-1.4", "http://hl7.org/fhir/spdx-license"), Description("Zimbra Public License v1.4")]
            Zimbra1_4,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("zlib-acknowledgement", "http://hl7.org/fhir/spdx-license"), Description("zlib/libpng License with Acknowledgement")]
            ZlibAcknowledgement,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("Zlib", "http://hl7.org/fhir/spdx-license"), Description("zlib License")]
            Zlib,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ZPL-1.1", "http://hl7.org/fhir/spdx-license"), Description("Zope Public License 1.1")]
            ZPL1_1,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ZPL-2.0", "http://hl7.org/fhir/spdx-license"), Description("Zope Public License 2.0")]
            ZPL2_0,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/spdx-license)
            /// </summary>
            [EnumLiteral("ZPL-2.1", "http://hl7.org/fhir/spdx-license"), Description("Zope Public License 2.1")]
            ZPL2_1,
        }

        /// <summary>
        /// A code that indicates how the page is generated.
        /// (url: http://hl7.org/fhir/ValueSet/guide-page-generation)
        /// </summary>
        [FhirEnumeration("GuidePageGeneration")]
        public enum GuidePageGeneration
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-page-generation)
            /// </summary>
            [EnumLiteral("html", "http://hl7.org/fhir/guide-page-generation"), Description("HTML")]
            Html,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-page-generation)
            /// </summary>
            [EnumLiteral("markdown", "http://hl7.org/fhir/guide-page-generation"), Description("Markdown")]
            Markdown,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-page-generation)
            /// </summary>
            [EnumLiteral("xml", "http://hl7.org/fhir/guide-page-generation"), Description("XML")]
            Xml,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-page-generation)
            /// </summary>
            [EnumLiteral("generated", "http://hl7.org/fhir/guide-page-generation"), Description("Generated")]
            Generated,
        }

        /// <summary>
        /// Code of parameter that is input to the guide.
        /// (url: http://hl7.org/fhir/ValueSet/guide-parameter-code)
        /// </summary>
        [FhirEnumeration("GuideParameterCode")]
        public enum GuideParameterCode
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("apply", "http://hl7.org/fhir/guide-parameter-code"), Description("Apply Metadata Value")]
            Apply,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("path-resource", "http://hl7.org/fhir/guide-parameter-code"), Description("Resource Path")]
            PathResource,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("path-pages", "http://hl7.org/fhir/guide-parameter-code"), Description("Pages Path")]
            PathPages,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("path-tx-cache", "http://hl7.org/fhir/guide-parameter-code"), Description("Terminology Cache Path")]
            PathTxCache,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("expansion-parameter", "http://hl7.org/fhir/guide-parameter-code"), Description("Expansion Profile")]
            ExpansionParameter,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("rule-broken-links", "http://hl7.org/fhir/guide-parameter-code"), Description("Broken Links Rule")]
            RuleBrokenLinks,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("generate-xml", "http://hl7.org/fhir/guide-parameter-code"), Description("Generate XML")]
            GenerateXml,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("generate-json", "http://hl7.org/fhir/guide-parameter-code"), Description("Generate JSON")]
            GenerateJson,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("generate-turtle", "http://hl7.org/fhir/guide-parameter-code"), Description("Generate Turtle")]
            GenerateTurtle,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guide-parameter-code)
            /// </summary>
            [EnumLiteral("html-template", "http://hl7.org/fhir/guide-parameter-code"), Description("HTML Template")]
            HtmlTemplate,
        }

        [FhirType("DependsOnComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class DependsOnComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DependsOnComponent"; } }
            
            /// <summary>
            /// Identity of the IG that this depends on
            /// </summary>
            [FhirElement("uri", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _UriElement;
            
            /// <summary>
            /// Identity of the IG that this depends on
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                    if (value == null)
                        UriElement = null; 
                    else
                        UriElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// NPM Package name for IG this depends on
            /// </summary>
            [FhirElement("packageId", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Id PackageIdElement
            {
                get { return _PackageIdElement; }
                set { _PackageIdElement = value; OnPropertyChanged("PackageIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _PackageIdElement;
            
            /// <summary>
            /// NPM Package name for IG this depends on
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PackageId
            {
                get { return PackageIdElement != null ? PackageIdElement.Value : null; }
                set
                {
                    if (value == null)
                        PackageIdElement = null; 
                    else
                        PackageIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("PackageId");
                }
            }
            
            /// <summary>
            /// Version of the IG
            /// </summary>
            [FhirElement("version", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Version of the IG
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Version
            {
                get { return VersionElement != null ? VersionElement.Value : null; }
                set
                {
                    if (value == null)
                        VersionElement = null; 
                    else
                        VersionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Version");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DependsOnComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.Canonical)UriElement.DeepCopy();
                    if(PackageIdElement != null) dest.PackageIdElement = (Hl7.Fhir.Model.Id)PackageIdElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DependsOnComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DependsOnComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(PackageIdElement, otherT.PackageIdElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DependsOnComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(PackageIdElement, otherT.PackageIdElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UriElement != null) yield return UriElement;
                    if (PackageIdElement != null) yield return PackageIdElement;
                    if (VersionElement != null) yield return VersionElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UriElement != null) yield return new ElementValue("uri", UriElement);
                    if (PackageIdElement != null) yield return new ElementValue("packageId", PackageIdElement);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                }
            }

            
        }
        
        
        [FhirType("GlobalComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class GlobalComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "GlobalComponent"; } }
            
            /// <summary>
            /// Type this profile applies to
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ResourceType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ResourceType> _TypeElement;
            
            /// <summary>
            /// Type this profile applies to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ResourceType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.ResourceType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Profile that all resources must conform to
            /// </summary>
            [FhirElement("profile", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical ProfileElement
            {
                get { return _ProfileElement; }
                set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _ProfileElement;
            
            /// <summary>
            /// Profile that all resources must conform to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Profile
            {
                get { return ProfileElement != null ? ProfileElement.Value : null; }
                set
                {
                    if (value == null)
                        ProfileElement = null; 
                    else
                        ProfileElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Profile");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GlobalComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ResourceType>)TypeElement.DeepCopy();
                    if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.Canonical)ProfileElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new GlobalComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GlobalComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GlobalComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (ProfileElement != null) yield return ProfileElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (ProfileElement != null) yield return new ElementValue("profile", ProfileElement);
                }
            }

            
        }
        
        
        [FhirType("DefinitionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class DefinitionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DefinitionComponent"; } }
            
            /// <summary>
            /// Grouping used to present related resources in the IG
            /// </summary>
            [FhirElement("grouping", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImplementationGuide.GroupingComponent> Grouping
            {
                get { if(_Grouping==null) _Grouping = new List<Hl7.Fhir.Model.ImplementationGuide.GroupingComponent>(); return _Grouping; }
                set { _Grouping = value; OnPropertyChanged("Grouping"); }
            }
            
            private List<Hl7.Fhir.Model.ImplementationGuide.GroupingComponent> _Grouping;
            
            /// <summary>
            /// Resource in the implementation guide
            /// </summary>
            [FhirElement("resource", Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImplementationGuide.ResourceComponent> Resource
            {
                get { if(_Resource==null) _Resource = new List<Hl7.Fhir.Model.ImplementationGuide.ResourceComponent>(); return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private List<Hl7.Fhir.Model.ImplementationGuide.ResourceComponent> _Resource;
            
            /// <summary>
            /// Page/Section in the Guide
            /// </summary>
            [FhirElement("page", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.ImplementationGuide.PageComponent Page
            {
                get { return _Page; }
                set { _Page = value; OnPropertyChanged("Page"); }
            }
            
            private Hl7.Fhir.Model.ImplementationGuide.PageComponent _Page;
            
            /// <summary>
            /// Defines how IG is built by tools
            /// </summary>
            [FhirElement("parameter", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImplementationGuide.ParameterComponent> Parameter
            {
                get { if(_Parameter==null) _Parameter = new List<Hl7.Fhir.Model.ImplementationGuide.ParameterComponent>(); return _Parameter; }
                set { _Parameter = value; OnPropertyChanged("Parameter"); }
            }
            
            private List<Hl7.Fhir.Model.ImplementationGuide.ParameterComponent> _Parameter;
            
            /// <summary>
            /// A template for building resources
            /// </summary>
            [FhirElement("template", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImplementationGuide.TemplateComponent> Template
            {
                get { if(_Template==null) _Template = new List<Hl7.Fhir.Model.ImplementationGuide.TemplateComponent>(); return _Template; }
                set { _Template = value; OnPropertyChanged("Template"); }
            }
            
            private List<Hl7.Fhir.Model.ImplementationGuide.TemplateComponent> _Template;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DefinitionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Grouping != null) dest.Grouping = new List<Hl7.Fhir.Model.ImplementationGuide.GroupingComponent>(Grouping.DeepCopy());
                    if(Resource != null) dest.Resource = new List<Hl7.Fhir.Model.ImplementationGuide.ResourceComponent>(Resource.DeepCopy());
                    if(Page != null) dest.Page = (Hl7.Fhir.Model.ImplementationGuide.PageComponent)Page.DeepCopy();
                    if(Parameter != null) dest.Parameter = new List<Hl7.Fhir.Model.ImplementationGuide.ParameterComponent>(Parameter.DeepCopy());
                    if(Template != null) dest.Template = new List<Hl7.Fhir.Model.ImplementationGuide.TemplateComponent>(Template.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DefinitionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DefinitionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Grouping, otherT.Grouping)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Page, otherT.Page)) return false;
                if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.Matches(Template, otherT.Template)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DefinitionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Grouping, otherT.Grouping)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Page, otherT.Page)) return false;
                if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.IsExactly(Template, otherT.Template)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Grouping) { if (elem != null) yield return elem; }
                    foreach (var elem in Resource) { if (elem != null) yield return elem; }
                    if (Page != null) yield return Page;
                    foreach (var elem in Parameter) { if (elem != null) yield return elem; }
                    foreach (var elem in Template) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Grouping) { if (elem != null) yield return new ElementValue("grouping", elem); }
                    foreach (var elem in Resource) { if (elem != null) yield return new ElementValue("resource", elem); }
                    if (Page != null) yield return new ElementValue("page", Page);
                    foreach (var elem in Parameter) { if (elem != null) yield return new ElementValue("parameter", elem); }
                    foreach (var elem in Template) { if (elem != null) yield return new ElementValue("template", elem); }
                }
            }

            
        }
        
        
        [FhirType("GroupingComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class GroupingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "GroupingComponent"; } }
            
            /// <summary>
            /// Descriptive name for the package
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Descriptive name for the package
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Human readable text describing the package
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Human readable text describing the package
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new GroupingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GroupingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                }
            }

            
        }
        
        
        [FhirType("ResourceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ResourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ResourceComponent"; } }
            
            /// <summary>
            /// Location of the resource
            /// </summary>
            [FhirElement("reference", Order=40)]
            [CLSCompliant(false)]
			[References()]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// Versions this applies to (if different to IG)
            /// </summary>
            [FhirElement("fhirVersion", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.FHIRVersion>> FhirVersionElement
            {
                get { if(_FhirVersionElement==null) _FhirVersionElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FHIRVersion>>(); return _FhirVersionElement; }
                set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.FHIRVersion>> _FhirVersionElement;
            
            /// <summary>
            /// Versions this applies to (if different to IG)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.FHIRVersion?> FhirVersion
            {
                get { return FhirVersionElement != null ? FhirVersionElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        FhirVersionElement = null; 
                    else
                        FhirVersionElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FHIRVersion>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FHIRVersion>(elem)));
                    OnPropertyChanged("FhirVersion");
                }
            }
            
            /// <summary>
            /// Human Name for the resource
            /// </summary>
            [FhirElement("name", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Human Name for the resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Reason why included in guide
            /// </summary>
            [FhirElement("description", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Reason why included in guide
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Is an example/What is this an example of?
            /// </summary>
            [FhirElement("example", Order=80, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical))]
            [DataMember]
            public Hl7.Fhir.Model.Element Example
            {
                get { return _Example; }
                set { _Example = value; OnPropertyChanged("Example"); }
            }
            
            private Hl7.Fhir.Model.Element _Example;
            
            /// <summary>
            /// Grouping this is part of
            /// </summary>
            [FhirElement("groupingId", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Id GroupingIdElement
            {
                get { return _GroupingIdElement; }
                set { _GroupingIdElement = value; OnPropertyChanged("GroupingIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _GroupingIdElement;
            
            /// <summary>
            /// Grouping this is part of
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string GroupingId
            {
                get { return GroupingIdElement != null ? GroupingIdElement.Value : null; }
                set
                {
                    if (value == null)
                        GroupingIdElement = null; 
                    else
                        GroupingIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("GroupingId");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ResourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(FhirVersionElement != null) dest.FhirVersionElement = new List<Code<Hl7.Fhir.Model.FHIRVersion>>(FhirVersionElement.DeepCopy());
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Example != null) dest.Example = (Hl7.Fhir.Model.Element)Example.DeepCopy();
                    if(GroupingIdElement != null) dest.GroupingIdElement = (Hl7.Fhir.Model.Id)GroupingIdElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ResourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ResourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Example, otherT.Example)) return false;
                if( !DeepComparable.Matches(GroupingIdElement, otherT.GroupingIdElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Example, otherT.Example)) return false;
                if( !DeepComparable.IsExactly(GroupingIdElement, otherT.GroupingIdElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Reference != null) yield return Reference;
                    foreach (var elem in FhirVersionElement) { if (elem != null) yield return elem; }
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Example != null) yield return Example;
                    if (GroupingIdElement != null) yield return GroupingIdElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                    foreach (var elem in FhirVersionElement) { if (elem != null) yield return new ElementValue("fhirVersion", elem); }
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Example != null) yield return new ElementValue("example", Example);
                    if (GroupingIdElement != null) yield return new ElementValue("groupingId", GroupingIdElement);
                }
            }

            
        }
        
        
        [FhirType("PageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PageComponent"; } }
            
            /// <summary>
            /// Where to find that page
            /// </summary>
            [FhirElement("name", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirUrl),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Name
            {
                get { return _Name; }
                set { _Name = value; OnPropertyChanged("Name"); }
            }
            
            private Hl7.Fhir.Model.Element _Name;
            
            /// <summary>
            /// Short title shown for navigational assistance
            /// </summary>
            [FhirElement("title", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Short title shown for navigational assistance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Title
            {
                get { return TitleElement != null ? TitleElement.Value : null; }
                set
                {
                    if (value == null)
                        TitleElement = null; 
                    else
                        TitleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Title");
                }
            }
            
            /// <summary>
            /// html | markdown | xml | generated
            /// </summary>
            [FhirElement("generation", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ImplementationGuide.GuidePageGeneration> GenerationElement
            {
                get { return _GenerationElement; }
                set { _GenerationElement = value; OnPropertyChanged("GenerationElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ImplementationGuide.GuidePageGeneration> _GenerationElement;
            
            /// <summary>
            /// html | markdown | xml | generated
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ImplementationGuide.GuidePageGeneration? Generation
            {
                get { return GenerationElement != null ? GenerationElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        GenerationElement = null; 
                    else
                        GenerationElement = new Code<Hl7.Fhir.Model.ImplementationGuide.GuidePageGeneration>(value);
                    OnPropertyChanged("Generation");
                }
            }
            
            /// <summary>
            /// Nested Pages / Sections
            /// </summary>
            [FhirElement("page", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImplementationGuide.PageComponent> Page
            {
                get { if(_Page==null) _Page = new List<Hl7.Fhir.Model.ImplementationGuide.PageComponent>(); return _Page; }
                set { _Page = value; OnPropertyChanged("Page"); }
            }
            
            private List<Hl7.Fhir.Model.ImplementationGuide.PageComponent> _Page;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Name != null) dest.Name = (Hl7.Fhir.Model.Element)Name.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(GenerationElement != null) dest.GenerationElement = (Code<Hl7.Fhir.Model.ImplementationGuide.GuidePageGeneration>)GenerationElement.DeepCopy();
                    if(Page != null) dest.Page = new List<Hl7.Fhir.Model.ImplementationGuide.PageComponent>(Page.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Name, otherT.Name)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(GenerationElement, otherT.GenerationElement)) return false;
                if( !DeepComparable.Matches(Page, otherT.Page)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(GenerationElement, otherT.GenerationElement)) return false;
                if( !DeepComparable.IsExactly(Page, otherT.Page)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Name != null) yield return Name;
                    if (TitleElement != null) yield return TitleElement;
                    if (GenerationElement != null) yield return GenerationElement;
                    foreach (var elem in Page) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Name != null) yield return new ElementValue("name", Name);
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                    if (GenerationElement != null) yield return new ElementValue("generation", GenerationElement);
                    foreach (var elem in Page) { if (elem != null) yield return new ElementValue("page", elem); }
                }
            }

            
        }
        
        
        [FhirType("ParameterComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ParameterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ParameterComponent"; } }
            
            /// <summary>
            /// apply | path-resource | path-pages | path-tx-cache | expansion-parameter | rule-broken-links | generate-xml | generate-json | generate-turtle | html-template
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ImplementationGuide.GuideParameterCode> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ImplementationGuide.GuideParameterCode> _CodeElement;
            
            /// <summary>
            /// apply | path-resource | path-pages | path-tx-cache | expansion-parameter | rule-broken-links | generate-xml | generate-json | generate-turtle | html-template
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ImplementationGuide.GuideParameterCode? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.ImplementationGuide.GuideParameterCode>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Value for named type
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Value for named type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.ImplementationGuide.GuideParameterCode>)CodeElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ParameterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            
        }
        
        
        [FhirType("TemplateComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class TemplateComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TemplateComponent"; } }
            
            /// <summary>
            /// Type of template specified
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Type of template specified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null; 
                    else
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// The source location for the template
            /// </summary>
            [FhirElement("source", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SourceElement
            {
                get { return _SourceElement; }
                set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SourceElement;
            
            /// <summary>
            /// The source location for the template
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Source
            {
                get { return SourceElement != null ? SourceElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceElement = null; 
                    else
                        SourceElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Source");
                }
            }
            
            /// <summary>
            /// The scope in which the template applies
            /// </summary>
            [FhirElement("scope", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ScopeElement
            {
                get { return _ScopeElement; }
                set { _ScopeElement = value; OnPropertyChanged("ScopeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ScopeElement;
            
            /// <summary>
            /// The scope in which the template applies
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Scope
            {
                get { return ScopeElement != null ? ScopeElement.Value : null; }
                set
                {
                    if (value == null)
                        ScopeElement = null; 
                    else
                        ScopeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Scope");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TemplateComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.FhirString)SourceElement.DeepCopy();
                    if(ScopeElement != null) dest.ScopeElement = (Hl7.Fhir.Model.FhirString)ScopeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TemplateComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TemplateComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.Matches(ScopeElement, otherT.ScopeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TemplateComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.IsExactly(ScopeElement, otherT.ScopeElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (SourceElement != null) yield return SourceElement;
                    if (ScopeElement != null) yield return ScopeElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (SourceElement != null) yield return new ElementValue("source", SourceElement);
                    if (ScopeElement != null) yield return new ElementValue("scope", ScopeElement);
                }
            }

            
        }
        
        
        [FhirType("ManifestComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ManifestComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ManifestComponent"; } }
            
            /// <summary>
            /// Location of rendered implementation guide
            /// </summary>
            [FhirElement("rendering", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUrl RenderingElement
            {
                get { return _RenderingElement; }
                set { _RenderingElement = value; OnPropertyChanged("RenderingElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUrl _RenderingElement;
            
            /// <summary>
            /// Location of rendered implementation guide
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Rendering
            {
                get { return RenderingElement != null ? RenderingElement.Value : null; }
                set
                {
                    if (value == null)
                        RenderingElement = null; 
                    else
                        RenderingElement = new Hl7.Fhir.Model.FhirUrl(value);
                    OnPropertyChanged("Rendering");
                }
            }
            
            /// <summary>
            /// Resource in the implementation guide
            /// </summary>
            [FhirElement("resource", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImplementationGuide.ManifestResourceComponent> Resource
            {
                get { if(_Resource==null) _Resource = new List<Hl7.Fhir.Model.ImplementationGuide.ManifestResourceComponent>(); return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private List<Hl7.Fhir.Model.ImplementationGuide.ManifestResourceComponent> _Resource;
            
            /// <summary>
            /// HTML page within the parent IG
            /// </summary>
            [FhirElement("page", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImplementationGuide.ManifestPageComponent> Page
            {
                get { if(_Page==null) _Page = new List<Hl7.Fhir.Model.ImplementationGuide.ManifestPageComponent>(); return _Page; }
                set { _Page = value; OnPropertyChanged("Page"); }
            }
            
            private List<Hl7.Fhir.Model.ImplementationGuide.ManifestPageComponent> _Page;
            
            /// <summary>
            /// Image within the IG
            /// </summary>
            [FhirElement("image", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ImageElement
            {
                get { if(_ImageElement==null) _ImageElement = new List<Hl7.Fhir.Model.FhirString>(); return _ImageElement; }
                set { _ImageElement = value; OnPropertyChanged("ImageElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ImageElement;
            
            /// <summary>
            /// Image within the IG
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Image
            {
                get { return ImageElement != null ? ImageElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ImageElement = null; 
                    else
                        ImageElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Image");
                }
            }
            
            /// <summary>
            /// Additional linkable file in IG
            /// </summary>
            [FhirElement("other", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> OtherElement
            {
                get { if(_OtherElement==null) _OtherElement = new List<Hl7.Fhir.Model.FhirString>(); return _OtherElement; }
                set { _OtherElement = value; OnPropertyChanged("OtherElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _OtherElement;
            
            /// <summary>
            /// Additional linkable file in IG
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Other
            {
                get { return OtherElement != null ? OtherElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        OtherElement = null; 
                    else
                        OtherElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Other");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ManifestComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RenderingElement != null) dest.RenderingElement = (Hl7.Fhir.Model.FhirUrl)RenderingElement.DeepCopy();
                    if(Resource != null) dest.Resource = new List<Hl7.Fhir.Model.ImplementationGuide.ManifestResourceComponent>(Resource.DeepCopy());
                    if(Page != null) dest.Page = new List<Hl7.Fhir.Model.ImplementationGuide.ManifestPageComponent>(Page.DeepCopy());
                    if(ImageElement != null) dest.ImageElement = new List<Hl7.Fhir.Model.FhirString>(ImageElement.DeepCopy());
                    if(OtherElement != null) dest.OtherElement = new List<Hl7.Fhir.Model.FhirString>(OtherElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ManifestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ManifestComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RenderingElement, otherT.RenderingElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Page, otherT.Page)) return false;
                if( !DeepComparable.Matches(ImageElement, otherT.ImageElement)) return false;
                if( !DeepComparable.Matches(OtherElement, otherT.OtherElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ManifestComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RenderingElement, otherT.RenderingElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Page, otherT.Page)) return false;
                if( !DeepComparable.IsExactly(ImageElement, otherT.ImageElement)) return false;
                if( !DeepComparable.IsExactly(OtherElement, otherT.OtherElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RenderingElement != null) yield return RenderingElement;
                    foreach (var elem in Resource) { if (elem != null) yield return elem; }
                    foreach (var elem in Page) { if (elem != null) yield return elem; }
                    foreach (var elem in ImageElement) { if (elem != null) yield return elem; }
                    foreach (var elem in OtherElement) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RenderingElement != null) yield return new ElementValue("rendering", RenderingElement);
                    foreach (var elem in Resource) { if (elem != null) yield return new ElementValue("resource", elem); }
                    foreach (var elem in Page) { if (elem != null) yield return new ElementValue("page", elem); }
                    foreach (var elem in ImageElement) { if (elem != null) yield return new ElementValue("image", elem); }
                    foreach (var elem in OtherElement) { if (elem != null) yield return new ElementValue("other", elem); }
                }
            }

            
        }
        
        
        [FhirType("ManifestResourceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ManifestResourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ManifestResourceComponent"; } }
            
            /// <summary>
            /// Location of the resource
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=40)]
            [CLSCompliant(false)]
			[References()]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// Is an example/What is this an example of?
            /// </summary>
            [FhirElement("example", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical))]
            [DataMember]
            public Hl7.Fhir.Model.Element Example
            {
                get { return _Example; }
                set { _Example = value; OnPropertyChanged("Example"); }
            }
            
            private Hl7.Fhir.Model.Element _Example;
            
            /// <summary>
            /// Relative path for page in IG
            /// </summary>
            [FhirElement("relativePath", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUrl RelativePathElement
            {
                get { return _RelativePathElement; }
                set { _RelativePathElement = value; OnPropertyChanged("RelativePathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUrl _RelativePathElement;
            
            /// <summary>
            /// Relative path for page in IG
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RelativePath
            {
                get { return RelativePathElement != null ? RelativePathElement.Value : null; }
                set
                {
                    if (value == null)
                        RelativePathElement = null; 
                    else
                        RelativePathElement = new Hl7.Fhir.Model.FhirUrl(value);
                    OnPropertyChanged("RelativePath");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ManifestResourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(Example != null) dest.Example = (Hl7.Fhir.Model.Element)Example.DeepCopy();
                    if(RelativePathElement != null) dest.RelativePathElement = (Hl7.Fhir.Model.FhirUrl)RelativePathElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ManifestResourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ManifestResourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(Example, otherT.Example)) return false;
                if( !DeepComparable.Matches(RelativePathElement, otherT.RelativePathElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ManifestResourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(Example, otherT.Example)) return false;
                if( !DeepComparable.IsExactly(RelativePathElement, otherT.RelativePathElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Reference != null) yield return Reference;
                    if (Example != null) yield return Example;
                    if (RelativePathElement != null) yield return RelativePathElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                    if (Example != null) yield return new ElementValue("example", Example);
                    if (RelativePathElement != null) yield return new ElementValue("relativePath", RelativePathElement);
                }
            }

            
        }
        
        
        [FhirType("ManifestPageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ManifestPageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ManifestPageComponent"; } }
            
            /// <summary>
            /// HTML page name
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// HTML page name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Title of the page, for references
            /// </summary>
            [FhirElement("title", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Title of the page, for references
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Title
            {
                get { return TitleElement != null ? TitleElement.Value : null; }
                set
                {
                    if (value == null)
                        TitleElement = null; 
                    else
                        TitleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Title");
                }
            }
            
            /// <summary>
            /// Anchor available on the page
            /// </summary>
            [FhirElement("anchor", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> AnchorElement
            {
                get { if(_AnchorElement==null) _AnchorElement = new List<Hl7.Fhir.Model.FhirString>(); return _AnchorElement; }
                set { _AnchorElement = value; OnPropertyChanged("AnchorElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _AnchorElement;
            
            /// <summary>
            /// Anchor available on the page
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Anchor
            {
                get { return AnchorElement != null ? AnchorElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        AnchorElement = null; 
                    else
                        AnchorElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Anchor");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ManifestPageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(AnchorElement != null) dest.AnchorElement = new List<Hl7.Fhir.Model.FhirString>(AnchorElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ManifestPageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ManifestPageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(AnchorElement, otherT.AnchorElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ManifestPageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(AnchorElement, otherT.AnchorElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (TitleElement != null) yield return TitleElement;
                    foreach (var elem in AnchorElement) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                    foreach (var elem in AnchorElement) { if (elem != null) yield return new ElementValue("anchor", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// Canonical identifier for this implementation guide, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Canonical identifier for this implementation guide, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Business version of the implementation guide
        /// </summary>
        [FhirElement("version", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the implementation guide
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Name for this implementation guide (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this implementation guide (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Name for this implementation guide (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this implementation guide (human friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if (value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Date last changed
        /// </summary>
        [FhirElement("date", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date last changed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if (value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if (value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the implementation guide
        /// </summary>
        [FhirElement("description", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Description;
        
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for implementation guide (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Copyright;
        
        /// <summary>
        /// NPM Package name for IG
        /// </summary>
        [FhirElement("packageId", InSummary=true, Order=220)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Id PackageIdElement
        {
            get { return _PackageIdElement; }
            set { _PackageIdElement = value; OnPropertyChanged("PackageIdElement"); }
        }
        
        private Hl7.Fhir.Model.Id _PackageIdElement;
        
        /// <summary>
        /// NPM Package name for IG
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PackageId
        {
            get { return PackageIdElement != null ? PackageIdElement.Value : null; }
            set
            {
                if (value == null)
                  PackageIdElement = null; 
                else
                  PackageIdElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("PackageId");
            }
        }
        
        /// <summary>
        /// SPDX license code for this IG (or not-open-source)
        /// </summary>
        [FhirElement("license", InSummary=true, Order=230)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ImplementationGuide.SPDXLicense> LicenseElement
        {
            get { return _LicenseElement; }
            set { _LicenseElement = value; OnPropertyChanged("LicenseElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ImplementationGuide.SPDXLicense> _LicenseElement;
        
        /// <summary>
        /// SPDX license code for this IG (or not-open-source)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ImplementationGuide.SPDXLicense? License
        {
            get { return LicenseElement != null ? LicenseElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  LicenseElement = null; 
                else
                  LicenseElement = new Code<Hl7.Fhir.Model.ImplementationGuide.SPDXLicense>(value);
                OnPropertyChanged("License");
            }
        }
        
        /// <summary>
        /// FHIR Version(s) this Implementation Guide targets
        /// </summary>
        [FhirElement("fhirVersion", InSummary=true, Order=240)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.FHIRVersion>> FhirVersionElement
        {
            get { if(_FhirVersionElement==null) _FhirVersionElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FHIRVersion>>(); return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.FHIRVersion>> _FhirVersionElement;
        
        /// <summary>
        /// FHIR Version(s) this Implementation Guide targets
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.FHIRVersion?> FhirVersion
        {
            get { return FhirVersionElement != null ? FhirVersionElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  FhirVersionElement = null; 
                else
                  FhirVersionElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FHIRVersion>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FHIRVersion>(elem)));
                OnPropertyChanged("FhirVersion");
            }
        }
        
        /// <summary>
        /// Another Implementation guide this depends on
        /// </summary>
        [FhirElement("dependsOn", InSummary=true, Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ImplementationGuide.DependsOnComponent> DependsOn
        {
            get { if(_DependsOn==null) _DependsOn = new List<Hl7.Fhir.Model.ImplementationGuide.DependsOnComponent>(); return _DependsOn; }
            set { _DependsOn = value; OnPropertyChanged("DependsOn"); }
        }
        
        private List<Hl7.Fhir.Model.ImplementationGuide.DependsOnComponent> _DependsOn;
        
        /// <summary>
        /// Profiles that apply globally
        /// </summary>
        [FhirElement("global", InSummary=true, Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ImplementationGuide.GlobalComponent> Global
        {
            get { if(_Global==null) _Global = new List<Hl7.Fhir.Model.ImplementationGuide.GlobalComponent>(); return _Global; }
            set { _Global = value; OnPropertyChanged("Global"); }
        }
        
        private List<Hl7.Fhir.Model.ImplementationGuide.GlobalComponent> _Global;
        
        /// <summary>
        /// Information needed to build the IG
        /// </summary>
        [FhirElement("definition", Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.ImplementationGuide.DefinitionComponent Definition
        {
            get { return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private Hl7.Fhir.Model.ImplementationGuide.DefinitionComponent _Definition;
        
        /// <summary>
        /// Information about an assembled IG
        /// </summary>
        [FhirElement("manifest", Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.ImplementationGuide.ManifestComponent Manifest
        {
            get { return _Manifest; }
            set { _Manifest = value; OnPropertyChanged("Manifest"); }
        }
        
        private Hl7.Fhir.Model.ImplementationGuide.ManifestComponent _Manifest;
        

        public static ElementDefinition.ConstraintComponent ImplementationGuide_IG_0 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
            Key = "ig-0",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Name should be usable as an identifier for the module by machine processing applications such as code generation",
            Xpath = "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
        };

        public static ElementDefinition.ConstraintComponent ImplementationGuide_IG_2 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "definition.resource.fhirVersion.all(%context.fhirVersion contains $this)",
            Key = "ig-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If a resource has a fhirVersion, it must be oe of the versions defined for the Implementation Guide",
            Xpath = "count(for $id in (f:resource/f:fhirVersion) return $id[not(ancestor::f:fhirVersion/@value=$id/@value)])=0"
        };

        public static ElementDefinition.ConstraintComponent ImplementationGuide_IG_1 = new ElementDefinition.ConstraintComponent()
        { 
            Expression = "definition.all(resource.groupingId.all(%context.grouping.id contains $this))",
            Key = "ig-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If a resource has a groupingId, it must refer to a grouping defined in the Implementation Guide",
            Xpath = "count(for $id in (f:resource/f:groupingId) return $id[not(ancestor::f:grouping/@id=$id/@value)])=0"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(ImplementationGuide_IG_0);
            InvariantConstraints.Add(ImplementationGuide_IG_2);
            InvariantConstraints.Add(ImplementationGuide_IG_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ImplementationGuide;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactDetail>(Contact.DeepCopy());
                if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
                if(PackageIdElement != null) dest.PackageIdElement = (Hl7.Fhir.Model.Id)PackageIdElement.DeepCopy();
                if(LicenseElement != null) dest.LicenseElement = (Code<Hl7.Fhir.Model.ImplementationGuide.SPDXLicense>)LicenseElement.DeepCopy();
                if(FhirVersionElement != null) dest.FhirVersionElement = new List<Code<Hl7.Fhir.Model.FHIRVersion>>(FhirVersionElement.DeepCopy());
                if(DependsOn != null) dest.DependsOn = new List<Hl7.Fhir.Model.ImplementationGuide.DependsOnComponent>(DependsOn.DeepCopy());
                if(Global != null) dest.Global = new List<Hl7.Fhir.Model.ImplementationGuide.GlobalComponent>(Global.DeepCopy());
                if(Definition != null) dest.Definition = (Hl7.Fhir.Model.ImplementationGuide.DefinitionComponent)Definition.DeepCopy();
                if(Manifest != null) dest.Manifest = (Hl7.Fhir.Model.ImplementationGuide.ManifestComponent)Manifest.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ImplementationGuide());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ImplementationGuide;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.Matches(PackageIdElement, otherT.PackageIdElement)) return false;
            if( !DeepComparable.Matches(LicenseElement, otherT.LicenseElement)) return false;
            if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.Matches(DependsOn, otherT.DependsOn)) return false;
            if( !DeepComparable.Matches(Global, otherT.Global)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(Manifest, otherT.Manifest)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ImplementationGuide;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.IsExactly(PackageIdElement, otherT.PackageIdElement)) return false;
            if( !DeepComparable.IsExactly(LicenseElement, otherT.LicenseElement)) return false;
            if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.IsExactly(DependsOn, otherT.DependsOn)) return false;
            if( !DeepComparable.IsExactly(Global, otherT.Global)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(Manifest, otherT.Manifest)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (UrlElement != null) yield return UrlElement;
				if (VersionElement != null) yield return VersionElement;
				if (NameElement != null) yield return NameElement;
				if (TitleElement != null) yield return TitleElement;
				if (StatusElement != null) yield return StatusElement;
				if (ExperimentalElement != null) yield return ExperimentalElement;
				if (DateElement != null) yield return DateElement;
				if (PublisherElement != null) yield return PublisherElement;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				if (Description != null) yield return Description;
				foreach (var elem in UseContext) { if (elem != null) yield return elem; }
				foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
				if (Copyright != null) yield return Copyright;
				if (PackageIdElement != null) yield return PackageIdElement;
				if (LicenseElement != null) yield return LicenseElement;
				foreach (var elem in FhirVersionElement) { if (elem != null) yield return elem; }
				foreach (var elem in DependsOn) { if (elem != null) yield return elem; }
				foreach (var elem in Global) { if (elem != null) yield return elem; }
				if (Definition != null) yield return Definition;
				if (Manifest != null) yield return Manifest;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Description != null) yield return new ElementValue("description", Description);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                if (PackageIdElement != null) yield return new ElementValue("packageId", PackageIdElement);
                if (LicenseElement != null) yield return new ElementValue("license", LicenseElement);
                foreach (var elem in FhirVersionElement) { if (elem != null) yield return new ElementValue("fhirVersion", elem); }
                foreach (var elem in DependsOn) { if (elem != null) yield return new ElementValue("dependsOn", elem); }
                foreach (var elem in Global) { if (elem != null) yield return new ElementValue("global", elem); }
                if (Definition != null) yield return new ElementValue("definition", Definition);
                if (Manifest != null) yield return new ElementValue("manifest", Manifest);
            }
        }

    }
    
}
