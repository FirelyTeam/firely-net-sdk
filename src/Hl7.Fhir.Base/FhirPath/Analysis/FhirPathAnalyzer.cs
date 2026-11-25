/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Analysis;

/// <summary>
/// Statically checks a FHIRPath expression against the FHIR model, and reports the semantic errors defined by the
/// FHIRPath specification: paths to elements that do not exist, type-suffixed names of choice elements, type names
/// the focus can never be, functions used on (or with arguments of) types they never accept, and (optionally)
/// order-dependent functions used on unordered collections.
/// </summary>
/// <remarks>
/// <para>The analyzer computes, for each part of the expression, the set of types its items can have. An error is
/// reported only when that set is known and not empty, and none of its types allow the operation. Anything the
/// analyzer cannot determine (an unknown variable, an abstract type such as <c>Resource</c>, an unknown function) is
/// treated as "could be anything", which never leads to an error. An empty set means the code can never be reached
/// (e.g. after <c>ofType()</c> with a type the focus cannot have), and does not lead to errors either.</para>
/// <para>Type tests ('is' operator or <c>is()</c> function) on <c>$this</c> narrow the possible types of
/// <c>$this</c> in the operand that the logical operator guards: <c>A implies B</c>, <c>A and B</c> and
/// <c>iif(A, B, ...)</c> check B assuming A is true, <c>A or B</c> and <c>iif(A, ..., B)</c> assuming A is false.</para>
/// <para>Analysis is separate from compilation: <see cref="FhirPathCompiler.Compile(string)"/> does not run it.</para>
/// </remarks>
public sealed class FhirPathAnalyzer
{
    private readonly ModelInspector _inspector;

    /// <summary>
    /// Creates an analyzer that checks expressions against the FHIR model described by the given inspector
    /// (e.g. <c>ModelInfo.ModelInspector</c> for a FHIR release).
    /// </summary>
    public FhirPathAnalyzer(ModelInspector inspector)
    {
        _inspector = inspector ?? throw new ArgumentNullException(nameof(inspector));
    }

    /// <summary>
    /// Parses and analyzes an expression.
    /// </summary>
    /// <exception cref="FormatException">The expression is not syntactically valid.</exception>
    /// <exception cref="ArgumentException">The context contains a type name that is not a known FHIR or System type.</exception>
    public FhirPathAnalysisResult Analyze(string expression, FhirPathAnalysisContext? context = null) =>
        Analyze(new FhirPathCompiler().Parse(expression), context);

    /// <summary>
    /// Analyzes a parsed expression.
    /// </summary>
    /// <exception cref="ArgumentException">The context contains a type name that is not a known FHIR or System type.</exception>
    public FhirPathAnalysisResult Analyze(Expression expression, FhirPathAnalysisContext? context = null)
    {
        if (expression is null) throw new ArgumentNullException(nameof(expression));
        context ??= new FhirPathAnalysisContext();

        var walker = new Walker(this, context);
        var result = walker.Run(expression);
        return new FhirPathAnalysisResult(walker.Diagnostics, result.Types?.Select(t => t.ToString()).ToList());
    }

    #region Types

    /// <summary>
    /// A type an item can have: a FHIR type (with its class mapping) or a FHIRPath System type.
    /// </summary>
    internal sealed record AType(string Name, ClassMapping? Mapping)
    {
        public bool IsSystem => Mapping is null;
        public override string ToString() => IsSystem ? "System." + Name : Name;
    }

    /// <summary>
    /// The set of types the items of a collection can have (<c>null</c> if unknown), and whether the collection
    /// has a defined order.
    /// </summary>
    internal sealed class TypeSet
    {
        public static readonly TypeSet Unknown = new(null);
        public static readonly TypeSet Empty = new([]);

        public TypeSet(IEnumerable<AType>? types, bool unordered = false)
        {
            Types = types is null ? null : new HashSet<AType>(types);
            Unordered = unordered;
        }

        public HashSet<AType>? Types { get; }
        public bool Unordered { get; }
        public bool IsKnown => Types is not null;

        /// <summary>The set is known and not empty: only then can an operation on it be an error.</summary>
        public bool IsCheckable => Types is { Count: > 0 };

        public TypeSet AsUnordered(bool unordered) => unordered == Unordered ? this : new(Types, unordered);

        public static TypeSet Union(TypeSet a, TypeSet b) =>
            a.Types is null || b.Types is null
                ? Unknown.AsUnordered(a.Unordered || b.Unordered)
                : new(a.Types.Concat(b.Types), a.Unordered || b.Unordered);

        public static TypeSet Intersect(TypeSet a, TypeSet b) =>
            a.Types is null ? b : b.Types is null ? a : new(a.Types.Where(b.Types.Contains));

        public override string ToString() =>
            Types is null ? "(unknown)" : Types.Count == 0 ? "(none)" : string.Join(" | ", Types.Select(t => t.ToString()).OrderBy(n => n));
    }

    private static readonly string[] SYSTEM_TYPES = ["Any", "Boolean", "String", "Integer", "Long", "Decimal", "Date", "DateTime", "Time", "Quantity"];

    private static AType systemType(string name) => new(name, null);

    /// <summary>
    /// The FHIR type of a class mapping, by its FHIR type name: a code with a required binding (<c>Code&lt;T&gt;</c>)
    /// has a mapping named after its enum, but is of FHIR type <c>code</c>.
    /// </summary>
    private static AType fromMapping(ClassMapping mapping) => new(mapping.IsCodeOfT ? "code" : mapping.Name, mapping);

    internal static readonly AType SYSTEM_BOOLEAN = systemType("Boolean");
    internal static readonly AType SYSTEM_STRING = systemType("String");
    internal static readonly AType SYSTEM_INTEGER = systemType("Integer");

    /// <summary>
    /// The FHIR primitive types that are specializations of other FHIR primitive types, which the .NET
    /// classes do not reflect in their inheritance.
    /// </summary>
    private static readonly Dictionary<string, string> PRIMITIVE_BASE_TYPES = new()
    {
        ["code"] = "string",
        ["id"] = "string",
        ["markdown"] = "string",
        ["url"] = "uri",
        ["canonical"] = "uri",
        ["uuid"] = "uri",
        ["oid"] = "uri",
        ["unsignedInt"] = "integer",
        ["positiveInt"] = "integer",
    };

    /// <summary>
    /// Resolves a (possibly qualified) type name: unqualified names are resolved as FHIR types first, then
    /// as System types. Returns <c>null</c> if the name is not a known type.
    /// </summary>
    internal AType? ResolveType(string? name)
    {
        if (string.IsNullOrEmpty(name)) return null;

        if (name.StartsWith("System.", StringComparison.Ordinal))
            return resolveSystem(name.Substring("System.".Length));
        if (name.StartsWith("FHIR.", StringComparison.Ordinal))
            return resolveFhir(name.Substring("FHIR.".Length));

        return resolveFhir(name) ?? resolveSystem(name);

        AType? resolveSystem(string n) => SYSTEM_TYPES.Contains(n) ? systemType(n) : null;
        // Type names are case-sensitive (the inspector's lookup is not): 'String' is not the FHIR type 'string'.
        AType? resolveFhir(string n) => _inspector.FindClassMapping(n) is { } cm && cm.Name == n ? fromMapping(cm) : null;
    }

    internal TypeSet ResolveTypes(IReadOnlyCollection<string>? names, string argumentName)
    {
        if (names is null) return TypeSet.Unknown;

        var types = new List<AType>();
        foreach (var name in names)
            types.Add(ResolveType(name) ?? throw new ArgumentException($"'{name}' is not a known FHIR or System type", argumentName));

        return new TypeSet(types);
    }

    /// <summary>
    /// Whether a FHIR type has a statically known set of elements. Abstract types (Resource, Element, DataType, ...)
    /// stand for any of their subtypes, and dynamic (custom) types are not known statically.
    /// </summary>
    private static bool isConcrete(ClassMapping cm) => !cm.NativeType.IsAbstract && !cm.IsCustomMapping;

    /// <summary>
    /// Whether an item of type <paramref name="type"/> is (or may be) of type <paramref name="target"/>. Returns
    /// <c>null</c> when this cannot be determined.
    /// </summary>
    private static bool? isOfType(AType type, AType target)
    {
        if (type.IsSystem || target.IsSystem)
            return type.IsSystem && target.IsSystem ? type.Name == target.Name || target.Name == "Any" : null;

        var source = type.Mapping!;
        var targetMapping = target.Mapping!;

        if (type.Name == target.Name || targetMapping.NativeType.IsAssignableFrom(source.NativeType))
            return true;

        // An item of an abstract type can be any of its subtypes, and a concrete type can have subtypes as well
        // (e.g. a Quantity can be an Age).
        if (!isConcrete(source) || source.NativeType.IsAssignableFrom(targetMapping.NativeType)) return null;

        for (var name = type.Name; PRIMITIVE_BASE_TYPES.TryGetValue(name, out var baseName); name = baseName)
            if (baseName == target.Name) return true;

        return false;
    }

    /// <summary>
    /// Splits the types of a collection into those that are of the given type (<c>is</c> is true) and those that
    /// are not (<c>is</c> is false). Types for which this cannot be determined are in both.
    /// </summary>
    private static (TypeSet whenTrue, TypeSet whenFalse) splitOnType(TypeSet types, AType? target)
    {
        if (types.Types is null || target is null) return (types, types);

        var whenTrue = new List<AType>();
        var whenFalse = new List<AType>();
        foreach (var type in types.Types)
        {
            var isIt = isOfType(type, target);
            if (isIt == true) whenTrue.Add(type);
            if (isIt is null) whenTrue.Add(isSubtype(target, type) ? target : type);   // e.g. a Resource that is a Patient
            if (isIt != true) whenFalse.Add(type);
        }

        return (new TypeSet(whenTrue, types.Unordered), new TypeSet(whenFalse, types.Unordered));
    }

    private static bool isSubtype(AType sub, AType super) =>
        sub.Mapping is { } s && super.Mapping is { } t && t.NativeType.IsAssignableFrom(s.NativeType);

    /// <summary>Whether items of this type can be converted to a string (and so support e.g. <c>toString()</c>).</summary>
    private static bool isStringConvertible(AType type) =>
        type.IsSystem || typeof(P.IToSystemPrimitive).IsAssignableFrom(type.Mapping!.NativeType) || !isConcrete(type.Mapping);

    private static bool isBoolean(AType type) =>
        type.IsSystem ? type.Name is "Boolean" or "Any" : type.Mapping!.Name == "boolean" || !isConcrete(type.Mapping);

    #endregion

    /// <summary>
    /// Walks an expression, computing the possible types of each node and collecting diagnostics.
    /// </summary>
    private sealed class Walker(FhirPathAnalyzer analyzer, FhirPathAnalysisContext context) : ExpressionVisitor<TypeSet>
    {
        public List<FhirPathDiagnostic> Diagnostics { get; } = [];

        /// <summary>
        /// The possible types of <c>$this</c>, innermost scope on top. A new scope starts for the arguments of a
        /// function that iterates over its focus (e.g. <c>where()</c>), and when a type guard narrows <c>$this</c>.
        /// </summary>
        private readonly Stack<TypeSet> _this = new();

        public TypeSet Run(Expression expression)
        {
            _this.Push(analyzer.ResolveTypes(context.InputTypes, nameof(context.InputTypes)));
            // Resolve these up front, so an unknown type name is reported even if the expression does not use them.
            _resource = analyzer.ResolveTypes(context.ResourceTypes, nameof(context.ResourceTypes));
            _rootResource = analyzer.ResolveTypes(context.RootResourceTypes, nameof(context.RootResourceTypes));
            foreach (var variable in context.Variables)
                _variables[variable.Key] = analyzer.ResolveTypes(variable.Value, nameof(context.Variables));

            return expression.Accept(this);
        }

        private TypeSet This => _this.Peek();
        private TypeSet _resource = TypeSet.Unknown;
        private TypeSet _rootResource = TypeSet.Unknown;
        private readonly Dictionary<string, TypeSet> _variables = [];

        private TypeSet inScope(TypeSet @this, Func<TypeSet> action)
        {
            _this.Push(@this);
            try { return action(); }
            finally { _this.Pop(); }
        }

        private TypeSet visitInScope(Expression expression, TypeSet @this) => inScope(@this, () => expression.Accept(this));

        private void report(FhirPathDiagnosticCode code, string message, Expression expression) =>
            Diagnostics.Add(new FhirPathDiagnostic(code, message, expression));

        #region Visitor

        public override TypeSet VisitConstant(ConstantExpression expression)
        {
            if (expression is IdentifierExpression) return TypeSet.Unknown;

            return expression.ExpressionType is { Namespace: "System" } ts && ts.Name != "Any" && analyzer.ResolveType("System." + ts.Name) is { } type
                ? new TypeSet([type])
                : TypeSet.Unknown;
        }

        public override TypeSet VisitNewNodeListInit(NewNodeListInitExpression expression)
        {
            var result = TypeSet.Empty;
            foreach (var item in expression.Contents)
                result = TypeSet.Union(result, item.Accept(this));
            return result;
        }

        public override TypeSet VisitVariableRef(VariableRefExpression expression)
        {
            if (expression is AxisExpression axis)
            {
                return axis.AxisName switch
                {
                    "this" or "that" => This,
                    "index" => integer,
                    _ => TypeSet.Unknown
                };
            }

            var name = expression.Name.TrimStart('%');
            return name switch
            {
                "resource" => _resource,
                "rootResource" => _rootResource,
                "context" => _this.Last(),
                "ucum" or "sct" or "loinc" => @string,
                _ => _variables.TryGetValue(name, out var types) ? types : TypeSet.Unknown
            };
        }

        public override TypeSet VisitCustomExpression(CustomExpression expression) =>
            expression is BracketExpression be ? be.Operand.Accept(this) : base.VisitCustomExpression(expression);

        public override TypeSet VisitFunctionCall(FunctionCallExpression expression) => expression switch
        {
            ChildExpression ce => navigate(ce.Focus.Accept(this), ce.ChildName, ce),
            IndexerExpression ie => indexer(ie),
            BinaryExpression be => binary(be),
            UnaryExpression ue => unaryOperand(ue.Operand),
            SortDirectionExpression se => unaryOperand(se.Operand),
            _ => function(expression)
        };

        #endregion

        #region Paths

        private TypeSet navigate(TypeSet focus, string name, Expression expression)
        {
            if (string.IsNullOrEmpty(name)) return TypeSet.Unknown;

            // A path step can be a type name, which selects the focus if it is of that type (e.g. "Patient.name").
            // FHIR element names never start with an uppercase letter, so this is the only thing it can select.
            if (char.IsUpper(name[0]) && analyzer.ResolveType(name) is { IsSystem: false } typeName)
            {
                if (!focus.IsKnown) return new TypeSet([typeName], focus.Unordered);

                var (matching, _) = splitOnType(focus, typeName);
                if (focus.IsCheckable && matching.Types!.Count == 0)
                    report(FhirPathDiagnosticCode.WrongTypeName, $"The focus ({focus}) can never be of type '{name}'", expression);
                return matching;
            }

            if (!focus.IsKnown) return TypeSet.Unknown.AsUnordered(focus.Unordered);

            var result = new List<AType>();
            bool unknown = false, found = false;
            bool choiceName = false;

            foreach (var type in focus.Types!)
            {
                if (type.IsSystem)
                {
                    // System types have no elements in FHIRPath, but the engine does give e.g. a Quantity's value.
                    unknown = true;
                    continue;
                }

                var mapping = type.Mapping!;
                if (!isConcrete(mapping))
                {
                    unknown = true;
                    continue;
                }

                if (name == "value" && mapping.IsFhirPrimitive)
                {
                    // The value of a FHIR primitive is a System type.
                    found = unknown = true;
                    continue;
                }

                // The inspector's lookup is case-insensitive, but FHIRPath names are not.
                if (mapping.FindMappedElementByName(name) is { } element && element.Name == name)
                {
                    found = true;
                    try
                    {
                        result.AddRange(element.FhirTypeMappings.Select(fromMapping));
                    }
                    catch (Exception)
                    {
                        unknown = true;
                    }
                }
                else if (mapping.FindMappedElementByChoiceName(name) is { } choice && name.StartsWith(choice.Name, StringComparison.Ordinal) && name != choice.Name)
                {
                    choiceName = true;
                }
            }

            if (!found && !unknown && focus.IsCheckable)
            {
                if (choiceName)
                    report(FhirPathDiagnosticCode.ChoiceElementName, $"'{name}' is not a valid path: refer to a choice element by its name without a type suffix, and use ofType() to select a type", expression);
                else
                    report(FhirPathDiagnosticCode.UnknownElement, $"The element '{name}' does not exist on {focus}", expression);
            }

            return (unknown ? TypeSet.Unknown : new TypeSet(result)).AsUnordered(focus.Unordered);
        }

        private TypeSet indexer(IndexerExpression expression)
        {
            var focus = expression.Focus.Accept(this);
            checkOrdered(focus, "an indexer", expression);
            expression.Index.Accept(this);
            return focus.AsUnordered(false);
        }

        private void checkOrdered(TypeSet focus, string what, Expression expression)
        {
            if (context.CheckOrderedFunctions && focus.Unordered)
                report(FhirPathDiagnosticCode.UnorderedCollection, $"{what} is used on a collection that has no defined order", expression);
        }

        #endregion

        #region Operators

        private TypeSet unaryOperand(Expression operand)
        {
            operand.Accept(this);
            return TypeSet.Unknown;
        }

        private TypeSet binary(BinaryExpression expression)
        {
            var (left, right) = operands(expression);

            switch (expression.Op)
            {
                case "is":
                    left.Accept(this);
                    return boolean;
                case "as":
                    return splitOnType(left.Accept(this), typeArgument(right)).whenTrue;
                case "and" or "implies":
                    left.Accept(this);
                    visitInScope(right, guard(left).whenTrue);
                    return boolean;
                case "or":
                    left.Accept(this);
                    visitInScope(right, guard(left).whenFalse);
                    return boolean;
                case "|":
                    return TypeSet.Union(left.Accept(this), right.Accept(this));
            }

            left.Accept(this);
            right.Accept(this);

            return expression.Op switch
            {
                "xor" or "=" or "!=" or "~" or "!~" or "<" or ">" or "<=" or ">=" or "in" or "contains" => boolean,
                "&" => @string,
                _ => TypeSet.Unknown
            };
        }

        /// <summary>
        /// The operands of a binary operator. The parser stores them as the two arguments (the focus is <c>$that</c>),
        /// so <see cref="BinaryExpression.Left"/> and <see cref="BinaryExpression.Right"/> cannot be used.
        /// </summary>
        private static (Expression left, Expression right) operands(BinaryExpression expression)
        {
            var arguments = expression.Arguments.ToList();
            return arguments.Count == 2 ? (arguments[0], arguments[1]) : (expression.Focus, arguments[0]);
        }

        private static readonly TypeSet boolean = new([SYSTEM_BOOLEAN]);
        private static readonly TypeSet @string = new([SYSTEM_STRING]);
        private static readonly TypeSet integer = new([SYSTEM_INTEGER]);

        private AType? typeArgument(Expression expression) =>
            expression is IdentifierExpression { Value: { } name } ? analyzer.ResolveType(name) : null;

        #endregion

        #region Type guards

        /// <summary>
        /// The possible types of <c>$this</c> when the given condition is true, and when it is false.
        /// </summary>
        private (TypeSet whenTrue, TypeSet whenFalse) guard(Expression condition)
        {
            condition = unwrap(condition);

            switch (condition)
            {
                case BinaryExpression { Op: "is" } be when isThis(operands(be).left):
                    return splitOnType(This, typeArgument(operands(be).right));
                case BinaryExpression { Op: "and" } be:
                    {
                        var (lt, lf) = guard(operands(be).left);
                        var (rt, rf) = guard(operands(be).right);
                        return (TypeSet.Intersect(lt, rt), TypeSet.Union(lf, rf));
                    }
                case BinaryExpression { Op: "or" } be:
                    {
                        var (lt, lf) = guard(operands(be).left);
                        var (rt, rf) = guard(operands(be).right);
                        return (TypeSet.Union(lt, rt), TypeSet.Intersect(lf, rf));
                    }
                case BinaryExpression:
                    break;
                case FunctionCallExpression { FunctionName: "is" } fe when isThis(fe.Focus) && fe.Arguments.Count() == 1:
                    return splitOnType(This, typeArgument(fe.Arguments.Single()));
                case FunctionCallExpression { FunctionName: "not" } fe when !fe.Arguments.Any():
                    {
                        var (t, f) = guard(fe.Focus);
                        return (f, t);
                    }
            }

            return (This, This);
        }

        private static Expression unwrap(Expression expression)
        {
            while (expression is BracketExpression be) expression = be.Operand;
            return expression;
        }

        private static bool isThis(Expression expression) =>
            unwrap(expression) is AxisExpression { AxisName: "this" or "that" };

        #endregion

        #region Functions

        /// <summary>Functions whose arguments are evaluated for each item of the focus, with <c>$this</c> set to that item.</summary>
        private static readonly HashSet<string> ITERATING_FUNCTIONS = ["where", "select", "all", "exists", "repeat", "repeatAll", "sort", "iif"];

        /// <summary>Functions whose result depends on the order of their focus.</summary>
        private static readonly HashSet<string> ORDERED_FUNCTIONS = ["first", "last", "tail", "skip", "take"];

        private static readonly HashSet<string> BOOLEAN_FUNCTIONS =
        [
            "empty", "exists", "all", "allTrue", "anyTrue", "allFalse", "anyFalse", "subsetOf", "supersetOf", "isDistinct",
            "not", "is", "hasValue", "contains", "startsWith", "endsWith", "matches", "matchesFull", "memberOf", "conformsTo",
            "comparable", "convertsToBoolean", "convertsToInteger", "convertsToLong", "convertsToDecimal", "convertsToString",
            "convertsToDate", "convertsToDateTime", "convertsToTime", "convertsToQuantity", "htmlChecks",
        ];

        private static readonly HashSet<string> STRING_FUNCTIONS =
        [
            "toString", "substring", "upper", "lower", "replace", "replaceMatches", "trim", "join", "encode", "decode",
            "escape", "unescape", "toChars", "split",
        ];

        private static readonly HashSet<string> INTEGER_FUNCTIONS = ["count", "length", "indexOf", "lastIndexOf", "toInteger"];

        /// <summary>Functions whose result holds (some of) the items of their focus.</summary>
        private static readonly HashSet<string> FILTERING_FUNCTIONS = ["where", "first", "last", "tail", "skip", "take", "single", "distinct", "trace", "intersect", "exclude", "sort"];

        private TypeSet function(FunctionCallExpression expression)
        {
            var name = expression.FunctionName;
            var arguments = expression.Arguments.ToList();
            var focus = expression.Focus.Accept(this);

            if (name == "iif") return iif(focus, arguments, expression);

            if (name is "repeat" or "repeatAll" && arguments.Count == 1) return repeat(focus, arguments[0]);

            TypeSet lastArgument = TypeSet.Unknown;
            if (ITERATING_FUNCTIONS.Contains(name))
            {
                // $this is each item of the focus.
                foreach (var argument in arguments)
                    lastArgument = visitInScope(argument, focus.AsUnordered(false));
            }
            else
            {
                // What $this is in other arguments is not defined consistently, so it is left unknown.
                foreach (var argument in arguments.Where(a => a is not IdentifierExpression))
                    lastArgument = visitInScope(argument, TypeSet.Unknown);
            }

            if (ORDERED_FUNCTIONS.Contains(name))
                checkOrdered(focus, $"The function {name}()", expression);

            switch (name)
            {
                case "toString" when arguments.Count == 0:
                    if (focus.IsCheckable && !focus.Types!.Any(isStringConvertible))
                        report(FhirPathDiagnosticCode.InvalidFocusType, $"toString() can only be used on primitive values, not on {focus}", expression);
                    return @string;
                case "as" or "ofType" when arguments.Count == 1:
                    return splitOnType(focus, typeArgument(arguments[0])).whenTrue;
                case "select" when arguments.Count == 1:
                    return lastArgument.AsUnordered(focus.Unordered);
                case "children" or "descendants":
                    return TypeSet.Unknown.AsUnordered(true);
                case "union" or "combine" when arguments.Count == 1:
                    return TypeSet.Union(focus, lastArgument);
            }

            if (BOOLEAN_FUNCTIONS.Contains(name)) return boolean;
            if (STRING_FUNCTIONS.Contains(name)) return @string;
            if (INTEGER_FUNCTIONS.Contains(name)) return integer;
            if (FILTERING_FUNCTIONS.Contains(name)) return ORDERED_FUNCTIONS.Contains(name) || name == "sort" ? focus.AsUnordered(false) : focus;

            return TypeSet.Unknown;
        }

        /// <summary>
        /// <c>repeat()</c> runs its projection on the focus, and then again on every item it returns, so <c>$this</c>
        /// in the projection can be of the focus' types and of the projection's own result types. The projection is
        /// analyzed with <c>$this</c> growing until it stops changing, reporting diagnostics only on the final pass.
        /// </summary>
        private TypeSet repeat(TypeSet focus, Expression projection)
        {
            const int MAX_ROUNDS = 20;
            var @this = focus.AsUnordered(false);

            var settled = false;

            // One more pass than MAX_ROUNDS, to see whether the types of the last round have settled.
            for (var round = 0; round <= MAX_ROUNDS && @this.IsKnown && !settled; round++)
            {
                var grown = TypeSet.Union(@this, inScopeQuietly(projection, @this)).AsUnordered(false);
                settled = grown.IsKnown && grown.Types!.SetEquals(@this.Types!);
                @this = grown;
            }

            // If the types did not settle, $this is left unknown, which never leads to errors.
            if (!settled) @this = TypeSet.Unknown;

            var projected = visitInScope(projection, @this);
            return projected.AsUnordered(focus.Unordered || projected.Unordered);
        }

        private TypeSet inScopeQuietly(Expression expression, TypeSet @this)
        {
            var diagnosticCount = Diagnostics.Count;
            var result = visitInScope(expression, @this);
            Diagnostics.RemoveRange(diagnosticCount, Diagnostics.Count - diagnosticCount);
            return result;
        }

        private TypeSet iif(TypeSet focus, List<Expression> arguments, FunctionCallExpression expression)
        {
            if (arguments.Count is < 2 or > 3)
            {
                foreach (var argument in arguments) visitInScope(argument, focus);
                return TypeSet.Unknown;
            }

            return inScope(focus.AsUnordered(false), () =>
            {
                var criterion = arguments[0].Accept(this);
                // The engine evaluates a FHIR element (e.g. a code) used as a criterion by the singleton evaluation of
                // collections, so only criteria that are System values (literals, function results) are checked.
                if (criterion.IsCheckable && criterion.Types!.All(t => t.IsSystem) && !criterion.Types!.Any(isBoolean))
                    report(FhirPathDiagnosticCode.InvalidArgumentType, $"The criterion of iif() must be a boolean, but is {criterion}", arguments[0]);

                var (whenTrue, whenFalse) = guard(arguments[0]);
                var result = visitInScope(arguments[1], whenTrue);
                return arguments.Count == 3 ? TypeSet.Union(result, visitInScope(arguments[2], whenFalse)) : TypeSet.Unknown;
            });
        }

        #endregion
    }
}
