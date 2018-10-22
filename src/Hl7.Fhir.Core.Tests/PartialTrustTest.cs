#if NET_FW

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

// https://blogs.msdn.microsoft.com/shawnfa/2004/10/22/getting-the-current-permissions-in-a-named-permission-set/
// https://blogs.msdn.microsoft.com/shawnfa/2004/10/25/creating-an-appdomain-with-limited-permissions/

namespace Hl7.Fhir.Core.Tests
{
    [TestClass]
    public class PartialTrustTest
    {
        [TestMethod]
        public void TestPartialTrust()
        {
            // https://docs.microsoft.com/en-us/dotnet/framework/misc/how-to-run-partially-trusted-code-in-a-sandbox

            Evidence ev = new Evidence();
            ev.AddHostEvidence(new Zone(SecurityZone.MyComputer));
            PermissionSet internetPS = SecurityManager.GetStandardSandbox(ev);

            var Hl7CoreAssembly = typeof(Patient).Assembly;
            var Hl7CoreTestAssembly = Assembly.GetExecutingAssembly();

            var callingDomain = Thread.GetDomain();
            AppDomainSetup adSetup = new AppDomainSetup()
            {
                ApplicationBase = callingDomain.SetupInformation.ApplicationBase
                // ApplicationBase = Path.GetFullPath(Hl7CoreAssembly.Location)
            };

            StrongName[] strongNames = new[]
            {
                StrongNameFromAssembly(Hl7CoreTestAssembly),
                StrongNameFromAssembly(Hl7CoreAssembly)
            };

            AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, internetPS, strongNames);

            // Try to execute some code in the new domain
            newDomain.DoCallBack(Test);

            // Try to create a FHIR object in the new domain
            var objType = typeof(Code);
            ObjectHandle handle = Activator.CreateInstanceFrom(
                newDomain,
                objType.Assembly.ManifestModule.FullyQualifiedName,
                objType.FullName
            );
            Code instance = (Code)handle.Unwrap();

            Assert.IsNotNull(instance);
        }

        StrongName StrongNameFromAssembly(Assembly assembly)
        {
            var name = assembly.GetName();
            var blob = new StrongNamePublicKeyBlob(name.GetPublicKey());
            return new StrongName(blob, name.Name, name.Version);
        }

        void ExecuteUntrustedCode(string assemblyName, string typeName, string entryPoint, Object[] parameters)
        {
            //Load the MethodInfo for a method in the new assembly. This might be a method you know, or   
            //you can use Assembly.EntryPoint to get to the entry point in an executable.  
            MethodInfo target = Assembly.Load(assemblyName).GetType(typeName).GetMethod(entryPoint);
            try
            {
                // Invoke the method.  
                target.Invoke(null, parameters);
            }
            catch (Exception ex)
            {
                //When information is obtained from a SecurityException extra information is provided if it is   
                //accessed in full-trust.  
                (new PermissionSet(PermissionState.Unrestricted)).Assert();
                Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
                CodeAccessPermission.RevertAssert();
                Console.ReadLine();
            }
        }

        static void Test()
        {
            Debug.WriteLine("Hello world!");
        }

    }
}

#endif
