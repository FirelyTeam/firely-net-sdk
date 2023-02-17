/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System.Collections.Generic;
using Model = Hl7.Fhir.Model;

namespace Hl7.FhirPath.R4.Tests
{
    static class ConverterExtensions
    {
        public static void setValue(this Model.Quantity me, double? value)
        {
            if (value.HasValue)
                me.Value = (decimal)value.Value;
            else
                me.Value = null;
        }
        public static void setUnit(this Model.Quantity me, string value)
        {
            me.Unit = value;
        }
        public static void setCode(this Model.Quantity me, string value)
        {
            me.Code = value;
        }
        public static void setSystem(this Model.Quantity me, string value)
        {
            me.System = value;
        }
        public static void setValueSet(this Model.ElementDefinition.ElementDefinitionBindingComponent me, string value)
        {
            me.ValueSet = value;
        }
        public static Model.Canonical getValueSet(this Model.ElementDefinition.ElementDefinitionBindingComponent me)
        {
            return me.ValueSetElement;
        }

        public static Model.Range setLow(this Model.Range me, Model.Quantity value)
        {
            me.Low = value;
            return me;
        }
        public static Model.Range setHigh(this Model.Range me, Model.Quantity value)
        {
            me.High = value;
            return me;
        }

        public static Model.RiskAssessment.PredictionComponent addPrediction(this Model.RiskAssessment me)
        {
            var item = new Model.RiskAssessment.PredictionComponent();
            me.Prediction.Add(item);
            return item;
        }
        public static List<Model.RiskAssessment.PredictionComponent> getPrediction(this Model.RiskAssessment me)
        {
            return me.Prediction;
        }

        public static Model.Element getProbability(this Model.RiskAssessment.PredictionComponent me)
        {
            return me.Probability;
        }
        public static Model.RiskAssessment.PredictionComponent setProbability(this Model.RiskAssessment.PredictionComponent me, 
            Model.DataType value)
        {
            me.Probability = value;
            return me;
        }
    }
}