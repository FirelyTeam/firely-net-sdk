using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace PracticeAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class PracticeAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "FhirAnalyzer";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        private static Dictionary<string, int> _assemblies = new Dictionary<string, int> { { "Hl7.Fhir.DSTU2.Core", 2 }, { "Hl7.Fhir.STU3.Core", 3 }, { "Hl7.Fhir.R4.Core", 4 } };
        private static List<string> _loadedAssemblies = new List<string>();
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {

            context.RegisterSyntaxNodeAction(
                AnalyzeNode, SyntaxKind.SimpleMemberAccessExpression);

            context.RegisterCompilationStartAction(compilationStartContext =>
            {
                var compilation = compilationStartContext.Compilation;
                _loadedAssemblies.Clear();
                foreach (var assembly in _assemblies)
                {
                    if (compilation.References.Any(r => r.Display.Contains($"{assembly.Key}.dll")))
                    {
                        _loadedAssemblies.Add(assembly.Key);
                    }
                }
            });
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var memberAccess = (MemberAccessExpressionSyntax)context.Node;
            var variableSymbol = context.SemanticModel.GetTypeInfo(memberAccess.Expression);

            var member = variableSymbol.Type.GetMembers().SingleOrDefault(m => m.Name == memberAccess.Name.ToString());
            if (member != null)
            {
                var x = member.ContainingAssembly;

                var attributes = member.GetAttributes();
                var versionAttribute = attributes.SingleOrDefault(a => a.AttributeClass.Name == "FhirVersionAttribute");
                if (versionAttribute != null)
                {
                    var startingVersionInfo = versionAttribute.NamedArguments.SingleOrDefault(a => a.Key == "StartingVersion");
                    var startingVersionValue = startingVersionInfo.Value;

                    var endingVersionInfo = versionAttribute.NamedArguments.SingleOrDefault(a => a.Key == "EndingVersion");
                    var endingVersionValue = startingVersionInfo.Value;

                    var excludedVersionsInfo = versionAttribute.NamedArguments.SingleOrDefault(a => a.Key == "ExcludedVersions");
                    var excludedVersionsValue = excludedVersionsInfo.Value;

                    if(!IsValidVersion(startingVersionValue, endingVersionValue, excludedVersionsValue))
                        context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
                    return;
                }
            }
        }

        private bool IsValidVersion(TypedConstant startingVersion, TypedConstant endingVersion, TypedConstant excludedVersions)
        {
            var copyLoadedAssemblies = _loadedAssemblies.ToList();
            if (!excludedVersions.IsNull)
                copyLoadedAssemblies.RemoveAll(a => excludedVersions.Value.ToString().Contains(a));
            if (!startingVersion.IsNull)
            {
                var index = _assemblies[startingVersion.Value.ToString()];
                var highestVersion = _assemblies.Where(a => copyLoadedAssemblies.Contains(a.Key)).Max(a => a.Value);
                if (index > highestVersion)
                    return false;
            }
            if (!endingVersion.IsNull)
            {
                var index = _assemblies[endingVersion.Value.ToString()];
                var lowestVersion = _assemblies.Where(a => copyLoadedAssemblies.Contains(a.Key)).Max(a => a.Value);
                if (index < lowestVersion)
                    return false;
            }

            return true;
        }


        private void AnalyzeNode1(SyntaxNodeAnalysisContext context)
        {
            var localDeclaration = (LocalDeclarationStatementSyntax)context.Node;
            // make sure the declaration isn't already const:
            if (localDeclaration.Modifiers.Any(SyntaxKind.ConstKeyword))
            {
                return;
            }

            // Perform data flow analysis on the local declaration.
            var dataFlowAnalysis = context.SemanticModel.AnalyzeDataFlow(localDeclaration);
            var dataFlowAnalysis1 = context.SemanticModel.GetDeclaredSymbol(localDeclaration);
            //var x = dataFlowAnalysis.ReadOutside;
            //var l = x.First()?.Locations.First();
            //if(l!=null)
            //{
            //    context.ReportDiagnostic(Diagnostic.Create(Rule1, l));
            //}
            // Retrieve the local symbol for each variable in the local declaration
            // and ensure that it is not written outside of the data flow analysis region.
            var variable = localDeclaration.Declaration.Variables.Single();
            var variableSymbol = context.SemanticModel.GetDeclaredSymbol(variable);
            if (dataFlowAnalysis.WrittenOutside.Contains(variableSymbol))
            {
                return;
            }

            context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
        }

        private void AnalyzeNodeOriginal(SyntaxNodeAnalysisContext context)
        {
            var localDeclaration = (LocalDeclarationStatementSyntax)context.Node;
            // make sure the declaration isn't already const:
            if (localDeclaration.Modifiers.Any(SyntaxKind.ConstKeyword))
            {
                return;
            }

            // Perform data flow analysis on the local declaration.
            var dataFlowAnalysis = context.SemanticModel.AnalyzeDataFlow(localDeclaration);

            // Retrieve the local symbol for each variable in the local declaration
            // and ensure that it is not written outside of the data flow analysis region.
            var variable = localDeclaration.Declaration.Variables.Single();
            var variableSymbol = context.SemanticModel.GetDeclaredSymbol(variable);
            if (dataFlowAnalysis.WrittenOutside.Contains(variableSymbol))
            {
                return;
            }

            context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
        }
    }
}
