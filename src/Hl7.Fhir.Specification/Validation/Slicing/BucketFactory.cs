/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Validation
{
    internal static class BucketFactory
    {
        public static IBucket CreateRoot(ElementDefinitionNavigator root, IResourceResolver resolver, Validator validator)
        {
            // Create a single bucket
            var entryBucket = new ElementBucket(root, validator);

            if (root.Current.Slicing == null)
                return entryBucket;
            else
                return CreateGroup(root, resolver, validator, entryBucket, atRoot: true);
        }

        public static IBucket CreateGroup(ElementDefinitionNavigator root, IResourceResolver resolver, Validator validator, IBucket entryBucket, bool atRoot)
        {
            var discriminatorSpecs = root.Current.Slicing.Discriminator.ToArray();  // copy, since root will move after this
            var location = root.Current.Path;
            var slices = root.FindMemberSlices(atRoot);
            var bm = root.Bookmark();
            var subs = new List<IBucket>();

            foreach (var slice in slices)
            {
                root.ReturnToBookmark(slice);

                IBucket subBucket;

                if (discriminatorSpecs.Any())
                {
                    var discriminators = discriminatorSpecs.Select(ds => DiscriminatorFactory.Build(ds, resolver, location, root, validator));
                    subBucket = new DiscriminatorBucket(root, validator, discriminators.ToArray());
                }
                else
                    // Discriminator-less matching
                    subBucket = new ConstraintsBucket(root, validator);

                if (root.Current.Slicing == null)
                    subs.Add(subBucket);
                else
                    subs.Add(CreateGroup(root, resolver, validator, subBucket, atRoot: false));
            }

            root.ReturnToBookmark(bm);

            return new SliceGroupBucket(root.Current.Slicing, entryBucket, subs);
        }

    }

}