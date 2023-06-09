
# SOEventToUnityEventAdapter:

This class intended to transform call from global space SOEvent to UnityEvent in concrete Scene MonoBehaviour.

inheritance MonoBehaviour -> preconcrete SOEventToUnityEventAdapter -> concrete SOEventToUnityEventAdapter_Concrete_%TYPE%

## Concrete:

MonoBehaviour menu names are  UKnack/SOEventToUnityEventAdapters/SOEvent_%TYPE%_toUnityEvent

- SOEventToUnityEventAdapter_Concrete_nongeneric
- SOEventToUnityEventAdapter_Concrete_bool
- SOEventToUnityEventAdapter_Concrete_int
- SOEventToUnityEventAdapter_Concrete_string
- SOEventToUnityEventAdapter_Concrete_string_string
- ...
- SOEventToUnityEventAdapter_Concrete_Vector2
- SOEventToUnityEventAdapter_Concrete_Vector3
- SOEventToUnityEventAdapter_Concrete_object

All above scrips based

## Preconcrete:

namespace UKnack.Preconcrete.Events:

- SOEventToUnityEventAdapter (non generic)
- SOEventToUnityEventAdapter\<T\>
- SOEventToUnityEventAdapter\<T1, T2\>
- SOEventToUnityEventAdapter\<T1, T2, T3\>
