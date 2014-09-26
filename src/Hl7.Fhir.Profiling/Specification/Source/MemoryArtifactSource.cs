using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Specification.Source
{
    public class MemoryArtifactSource : IArtifactSource
    {

        List<ResourceEntry> entries;

        public MemoryArtifactSource(IEnumerable<ResourceEntry> entries)
        {
            this.entries = entries.ToList();
        }

        private void load()
        {

        }

        bool _isPrepared = false;
        public void Prepare()
        {
            if (!_isPrepared)
            {
                load();
                _isPrepared = true;
            }
        }

        public Stream ReadContentArtifact(string name)
        {
            Prepare();
            throw new NotImplementedException();
        }

        public Resource ReadResourceArtifact(Uri artifactId)
        {

            Prepare();
            foreach (ResourceEntry entry in entries)
            {
                if (entry.Id.ToString().ToLower() == artifactId.ToString().ToLower())
                    return entry.Resource;
            }
            return null;
        }
    }
}
