module internal Firely.FhirPath.TypeExtensions

open System
open System.Collections.Generic

type Type with
  member t.GetContainerInterface(containerType: Type): Type option =
    seq { yield! t.GetInterfaces(); yield containerType }
    |> Seq.tryFind (fun x -> x.IsGenericType && x.GetGenericTypeDefinition() = containerType)
    
  member t.IsContainerOf(containerType: Type) = t.GetContainerInterface(containerType).IsSome
  
  member t.IsCollection = t.IsContainerOf(typedefof<ICollection<_>>)

  member t.GetContainerParamOf(containerType: Type): Type option = 
    let getExactlyOneArgument (t: Type) = t.GetGenericArguments() |> Array.tryExactlyOne
    t.GetContainerInterface(containerType) |> Option.bind getExactlyOneArgument

  member t.GetCollectionElement(): Type option = t.GetContainerInterface(typedefof<ICollection<_>>)

  member t.GetDelegateParameters(): Type[] = 
    if t.IsGenericType && t.IsAssignableTo(typeof<Delegate>) then 
      t.GetGenericArguments()
    else 
      Array.empty