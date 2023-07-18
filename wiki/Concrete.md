
# Concrete namespace:

Concrete namespace is one of special namespaces in UKnack library.

All classes located in it:
- Should be instantiated and therefore not abstract and not generic.
- Each located in separate file that name is identical to class name. 
- Expected be changed almost never
- Any changes to the existing changes expected to be Unity serialized friendly in a way that no values of existing serialized scripts should be lost.
- Any script from this namespace before removal should be marked as obsolete for at least ~~2~~ 1 consecutive major versions (This can be changed if there will be breaking changes from Unity).

## usual expected behavior

Usually concrete scripts expected to be created from UnityEditor, and their fields should be filled from UnityEditor.
If concrete class allow some kind of runtime changes, such changes should be open by <b>public</b> methods, properties or interfaces (less preferred, but still acceptable is <b>internal</b> modifier).

## Dependency-Injection

In my opinion UnityEditor is dependency injection tool by itself. Just default dependency injection practices using UnityEditor is ... not very developed or user-friendly. So, one of this library objectives is to make UnityEditor as tool for dependency injection more easy to use. But if you really want to, you can implement some external support for outside dependency injection for UKnackBasis.