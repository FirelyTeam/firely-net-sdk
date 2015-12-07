/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Navigation
{

    // A circular generic constraint of the form IFoo<T> where T : IFoo<T>
    // expresses that type T represents the implementing class.
    // It allows derived interfaces and subclasses to operate on T = own type
    // Generic derived interfaces and implementing classes should follow the same pattern,
    // further limiting the constraint to their own type.
    // Concrete, non-generic derived interfaces and implementing classes should specify their own type for T
    //
    // Example:
    //   
    //     IFoo<T> where T : IFoo<T> { T Foo { get; } }
    //     IBar<T> : IFoo<T> where T : IBar<T> { }
    //     Bar<T> : IBar<T> where T : Bar<T> { }
    //     Baz<T> : Bar<T> where T : Baz<T> { }
    //     Baz : Baz<Baz> { } // Foo returns a Baz
    //
    // Here, we have IDoublyLinkedTree<T> : ILinkedList<T>
    //
    //     ILinkedList<T>.FirstChild         returns T = ILinkedList<T>
    //     IDoublyLinkedTree<T>.FirstChild   returns T = IDoublyLinkedTree<T>
    //     NavigationTree<T>.FirstChild      returns T = NavigationTree<T>
    //     ValueNavigationTree<T>.FirstChild returns T = ValueNavigationTree<T>
    //     FhirNavigationTree<V>.FirstChild  returns T = FhirNavigationTree<V>
    //     FhirInstanceTree.FirstChild       returns T = FhirInstanceTree

    /// <summary>Common generic interface for a linked tree.</summary>
    /// <typeparam name="T">The tree type.</typeparam>
    /// <example><code>MyTree : ILinkedTree&lt;MyTree&gt; { }</code></example>
    public interface ILinkedTree<out T> : ITree<T> where T : ILinkedTree<T>
    {
        /// <summary>Returns a reference to the next sibling node.</summary>
        T NextSibling { get; }

        /// <summary>Returns a reference to the first child node.</summary>
        T FirstChild { get; }
    }

    /// <summary>Common generic interface for a doubly linked tree.</summary>
    /// <typeparam name="T">The tree type.</typeparam>
    /// <example><code>MyTree : IDoublyLinkedTree&lt;MyTree&gt; { }</code></example>
    public interface IDoublyLinkedTree<out T> : ILinkedTree<T> where T : IDoublyLinkedTree<T>
    {
        /// <summary>Returns a reference to the parent tree node.</summary>
        T Parent { get; }

        /// <summary>Returns a reference to the previous sibling tree node.</summary>
        T PreviousSibling { get; }
    }

}
