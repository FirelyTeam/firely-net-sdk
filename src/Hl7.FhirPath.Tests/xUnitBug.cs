using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hl7.Fhir.Core.Tests
{
    public class xUnitBug
    {
        [Fact, Trait("TestCategory", "DoesNotMatter")]
        public void WillPreventBugInxUnitTestRunner()
        {
        }

    }
}
