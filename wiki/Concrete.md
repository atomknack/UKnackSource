
# Concrete namespace:

Concrete namespace is one of special namespaces in UKnack library.

All classes located in it:
- Should be instantiated and therefore not abstract and not generic.
- Each located in separate file that name is identical to class name. 
- Expected be changed almost never
- Any changes to the existing changes expected to be Unity serialized friendly in a way that no values of existing serialized scripts should be lost.
- Any script from this namespace before removal should be marked as obsolete for at least 2 consecutive major versions (This can be changed if there will be breaking changes from Unity).