using System;

namespace Hl7.Fhir.Model.CdsHooks;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class CdsHookElementAttribute() : Attribute;