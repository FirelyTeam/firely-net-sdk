namespace Firely.FhirPath

open System.Linq.Expressions
open System

type CallWrapperExpression(source: LambdaExpression, parameters: Expression[], targetType: Type) = 
    inherit Expression()

    override e.CanReduce = false

    override e.NodeType = ExpressionType.Extension

    override e.Type = targetType

    member val Wrapped = source with get

    member val Parameters = parameters with get
 
