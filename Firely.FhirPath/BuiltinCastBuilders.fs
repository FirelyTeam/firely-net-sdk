namespace Firely.FhirPath

open System
open System.Linq.Expressions
open TypeExtensions

type FunctionConverter() = 
    interface ICastBuilder with
        member fc.CanApproach source target = 
            let sl = source.GetDelegateParameters().Length
            let tl = target.GetDelegateParameters().Length
            sl = tl && sl <> 0
             
        member fc.Convert source target gp disp = 
            // Need to handle this more elegantly, by returning a failed cast
            let sourceFunc = source :?> LambdaExpression
            let sourceElementTypes = Seq.toArray sourceFunc.Parameters 
            let targetElementTypes = target.GetDelegateParameters();

            let convertedParams = 
                let converter = Array.map2 (fun f t -> disp.Convert(f,t,gp))
                converter sourceElementTypes targetElementTypes              

            let resultFuncType = 
                let funcParameters = Array.map (fun cp -> cp.Generated.Type) convertedParams
                Expression.GetFuncType(funcParameters)
            
            let totalComplexity = 1 + Array.sumBy (fun cp -> cp.Complexity) convertedParams
            let totalSuccess = Array.forall (fun cp -> cp.Success) convertedParams
            let firstNewHypothesis = 
                convertedParams |> Array.choose (fun cp -> cp.AssignmentHypothesis) |> Array.tryHead
        
            let callWrapper = CallWrapperExpression(sourceFunc, Array.map (fun cp -> cp.Generated) convertedParams, resultFuncType)

            { 
                Generated = callWrapper
                Complexity = totalComplexity;
                Success = totalSuccess;
                AssignmentHypothesis = firstNewHypothesis
            }

type ValueConverter() = 
    interface ICastBuilder with
        member vc.CanApproach source target =
            try 
                Expression.Convert(Expression.Parameter(source), target) |> ignore
                true
            with | :? InvalidOperationException -> false

        member vc.Convert source target gp cc =           
            try
                let conversion = Expression.Convert(source,target)
                { Generated = conversion; Complexity = 1; Success = true; AssignmentHypothesis = None }
            with 
                | :? InvalidOperationException -> { Generated = source; Complexity = 1; Success = false; AssignmentHypothesis = None}

            
        
