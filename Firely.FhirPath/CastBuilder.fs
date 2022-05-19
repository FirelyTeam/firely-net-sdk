namespace Firely.FhirPath

open System
open System.Collections.Generic
open System.Linq.Expressions

type GenericParameterAssignments() =
    inherit Dictionary<Type,Type>()

type GeneratedExpression = 
    {
        Generated: Expression;
        Complexity: int;
        Success: bool;
        AssignmentHypothesis: option<Type*Type>;
    }

type ICastBuilder = 
    abstract member CanApproach: source:Type -> target:Type -> bool
    abstract member Convert: source:Expression -> target:Type -> gp:GenericParameterAssignments -> disp:ConverterCollection -> GeneratedExpression

and ConverterCollection([<ParamArray>] converters: ICastBuilder[]) = 
    member val Converters = converters with get, set

    member cc.Convert(source: Expression, target: Type, gp: GenericParameterAssignments) : GeneratedExpression = 
        let converter = cc.Converters |> Array.tryFind (fun c -> c.CanApproach source.Type target)

        match converter with
        | Some c -> c.Convert source target gp cc
        | None -> { Generated = source; Complexity = 0; Success = false; AssignmentHypothesis = None }
