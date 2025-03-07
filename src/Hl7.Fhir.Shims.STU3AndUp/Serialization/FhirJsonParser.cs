/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public class FhirJsonParser(ParserSettings? settings = null)
    : BaseFhirJsonParser(ModelInfo.ModelInspector, settings);