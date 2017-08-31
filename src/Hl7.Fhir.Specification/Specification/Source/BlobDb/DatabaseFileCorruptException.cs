using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class DatabaseFileCorruptException : Exception
    {
        public long Position { get; private set; }
        public DatabaseFileCorruptException(string message, long position) : base(message)
        {
            Position = position;
        }

        public DatabaseFileCorruptException(string message, long position, Exception inner) : base(message, inner)
        {
            Position = position;
        }

    }
}
