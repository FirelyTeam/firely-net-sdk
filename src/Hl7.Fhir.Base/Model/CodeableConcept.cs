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

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;

public partial class CodeableConcept : ICoded, P.IToSystemPrimitive
{
    public CodeableConcept()
    {
    }

    public CodeableConcept(string? system, string? code, string? text = null)
    {
        if (!string.IsNullOrEmpty(system) || !string.IsNullOrEmpty(code))
        {
            this.Coding = [new Coding(system, code)];
        }
        this.Text = text;
    }

    public CodeableConcept(string? system, string? code, string? display, string? text)
    {
        if (!string.IsNullOrEmpty(system) || !string.IsNullOrEmpty(code) || !string.IsNullOrEmpty(display))
        {
            this.Coding = [new Coding(system, code, display)];
        }
        this.Text = text;
    }

    public CodeableConcept(IEnumerable<Coding> codes)
    {
        this.Coding = codes.ToList();
    }

    public CodeableConcept Add(string system, string code, string? display = null)
    {
        Coding.Add(new Coding(system, code, display));

        return this;
    }

    /// <summary>
    /// Converts this CodeableConcept to a <see cref="P.Concept"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">One or more of the codes are
    /// not convertable to a System Code. See <see cref="Model.Coding.ToSystemCode()"/>.</exception>
    public P.Concept ToSystemConcept() =>
        ((P.IToSystemPrimitive)this).TryConvertToSystemType(out var result)
            ? (P.Concept)result
            : throw new InvalidOperationException("Not all Codings are convertible.");

    bool P.IToSystemPrimitive.TryConvertToSystemType([NotNullWhen(true)] out P.Any? result)
    {
        var codes = Coding.Select(c => c.TryConvertToSystemTypeInternal()).ToArray();
        if (codes.Any(c => c is null))
        {
            result = null;
            return false;
        }

        result = new P.Concept(codes.Cast<P.Code>(), this.Text);
        return true;
    }

    /// <inheritdoc cref="ICoded.ToCodings"/>
    public IReadOnlyCollection<Coding> ToCodings() => Coding;
}