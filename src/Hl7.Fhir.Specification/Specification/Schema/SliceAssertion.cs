using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class SliceAssertion : Assertion, IGroupAssertion
    {
        public class Slice : Assertion
        {
            public readonly string Name;
            public readonly Assertion Condition;
            public readonly Assertion Assertion;

            public Slice(string name, Assertion condition, Assertion assertion)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Condition = condition ?? throw new ArgumentNullException(nameof(condition));
                Assertion = assertion ?? throw new ArgumentNullException(nameof(assertion));
            }

            public override IEnumerable<Assertions> Collect() => Condition.Collect().Product(Assertion.Collect());

            public override JToken ToJson() =>
                new JObject(
                    new JProperty("name", Name),
                    new JProperty("condition", Condition.ToJson().MakeNestedProp()),
                    new JProperty("assertion", Assertion.ToJson().MakeNestedProp())
                    );
        }


        public readonly bool Ordered;
        public readonly Assertion Default;
        public readonly Slice[] Slices;

        public SliceAssertion(bool ordered, Assertion @default, params Slice[] slices) : this(ordered, @default, slices.AsEnumerable())
        {
        }

        public SliceAssertion(bool ordered, params Slice[] slices) : this(ordered, slices.AsEnumerable())
        {
        }

        public SliceAssertion(bool ordered, IEnumerable<Slice> slices)
            : this(ordered, null, slices)
        {
        }

        public SliceAssertion(bool ordered, Assertion @default, IEnumerable<Slice> slices)
        {
            Ordered = ordered;
            Default = @default ?? new ElementSchema(ResultAssertion.Failure,
                            new Trace("Element does not match any slice and the group is closed."));
            Slices = slices.ToArray() ?? throw new ArgumentNullException(nameof(slices));
        }

        public override IEnumerable<Assertions> Collect()
            => Slices.Aggregate(Default.Collect(), (sum, slice) => sum.Product(slice.Collect()));

        public List<(Assertions, IElementNavigator)> Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
        {
            var lastMatchingSlice = -1;
            var defaultInUse = false;
            var results = new List<(Assertions, IElementNavigator)>();

            // Go over the elements in the instance, in order
            foreach (var candidate in input)
            {
                Assertions result = Assertions.Empty;

                bool hasSucceeded = false;

                // Try to find the child slice that this element matches
                for (var sliceNumber = 0; sliceNumber < Slices.Length; sliceNumber++)
                {
                    var sliceName = Slices[sliceNumber].Name;
                    var sliceResult = Slices[sliceNumber].Condition.Validate(candidate, vc);

                    if (sliceResult.Result.IsSuccessful)
                    {
                        // The instance matched a slice that we have already passed, if order matters, 
                        // this is not allowed
                        if (sliceNumber < lastMatchingSlice && Ordered)
                            result += new Trace($"Element matches slice '{sliceName}', but this " +
                                $"is out of order for this group, since a previous element already matched slice '{Slices[lastMatchingSlice].Name}'");
                        //validator.Trace(outcome, $"Element matches slice '{sliceName}', but this " +
                        //        $"is out of order for group '{Name}', since a previous element already matched slice '{ChildSlices[lastMatchingSlice].Name}'",
                        //    Issue.CONTENT_ELEMENT_SLICING_OUT_OF_ORDER, candidate);
                        else
                            lastMatchingSlice = sliceNumber;

                        if (defaultInUse && Ordered)
                        {
                            result += new Trace($"Element matched slice '{sliceName}', but it appears after a non-match, which is not allowed for an open-at-end group");
                            // We found a match while we already added a non-match to a "open at end" slicegroup, that's not allowed
                            //validator.Trace(outcome, $"Element matched slice '{sliceName}', but it appears after a non-match, which is not allowed for an open-at-end group",
                            //            Issue.CONTENT_ELEMENT_FAILS_SLICING_RULE, candidate);
                        }

                        hasSucceeded = true;
                        result += sliceResult;
                        break;      // TODO: should add this break to original code too (in SliceGroupBucket.cs)
                    }
                }

                // So we found no slice that can take this candidate, let's pass it to the default slice
                if (!hasSucceeded)
                {
                    defaultInUse = true;

                    var defaultResult = Default.Validate(candidate, vc).Result;

                    if (defaultResult.IsSuccessful)
                        result += new Trace("Element was determined to be in the open slice for group");
                    else
                    {
                        // Sorry, won't fly
                        result += new Trace("Element does not match any slice, but the group is closed.");
                        result += defaultResult;
                    }
                }

                results.Add((result, candidate));
            }

            return results;
        }

        public override JToken ToJson()
        {
            var def = Default.ToJson();
            if (def is JProperty) def = new JObject(def);

            return new JProperty("slice", new JObject(
                new JProperty("ordered", Ordered.ToString()),
                new JProperty("slice", new JArray() { Slices.Select(s => s.ToJson()) }),
                new JProperty("default", def)));
        }
    }
}

