/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization;

internal class PocoDeserializerState
{
    public readonly ExceptionAggregator Errors = new();
    public readonly PathStack Path = new();

    private readonly Dictionary<string, Action> _validations = new();

    /// <summary>
    /// Add a validation for a given key (mostly, property name), overwriting
    /// an already scheduled action for the same key, if any.
    /// </summary>
    public void ScheduleDelayedValidation(string key, Action validation)
    {
        _validations[key] = validation;
    }

    /// <summary>
    /// Run all delayed validations, clear the list.
    /// </summary>
    public void RunDelayedValidations()
    {
        foreach (var validation in _validations.Values) validation();
        _validations.Clear();
    }
}