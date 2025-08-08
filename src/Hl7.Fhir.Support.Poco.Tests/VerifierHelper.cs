#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using VerifyMSTest;
using VerifyTests;

namespace Hl7.Fhir.Support.Poco.Tests;

public class VerifierHelper
{
    private readonly VerifySettings _settings;

    public VerifierHelper(VerifySettings? settings = null)
    {
        _settings = settings ?? new VerifySettings();
        _settings.UseDirectory("snapshots");
        _settings.DisableDiff();
    }

    private static string buildVerifierPath(string sourceFile = "")
    {
#if CI_BUILD
        return Path.Combine(Directory.GetCurrentDirectory(), "Serialization", Path.GetFileName(sourceFile));
#else
        return sourceFile;
#endif
    }
    

    public async Task Check()
    {
        await VerifyChecks.Run();
    }

    public async Task Verify(object target, [CallerFilePath] string sourceFile = "")
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        await Verifier.Verify(target, _settings, buildVerifierPath(sourceFile));
    }
}