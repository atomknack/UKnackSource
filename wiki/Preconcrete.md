
# Preconcrete namespace:

Preconcrete namespace is one of special namespaces in UKnack library.
Types in this namespace mostly intended to be used in [Concrete namespace](Concrete) or in developing other MonoBehaviours or ScriptableObjects.

All classes located in it:
- Should NOT be directly instantiable and therefore only abstract, generic or types that not inherit UnityEngine.Object allowed.
- No sealed types that inherit UnityEngine.Object allowed.
- Expected be changed in compatible way with child classes.