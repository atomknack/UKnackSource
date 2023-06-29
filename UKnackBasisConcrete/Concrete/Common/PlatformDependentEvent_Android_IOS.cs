using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Concrete.Commmon
{
    [AddComponentMenu("UKnack/Common/PlatformDependentEvent/Android_IOS")]
    public class PlatformDependentEvent_Android_IOS : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _androidOnEnable;

        [SerializeField]
        private UnityEvent _iosOnEnable;

        private void OnEnable()
        {

#if UNITY_ANDROID
            _androidOnEnable.Invoke();
            return;
#endif

#if UNITY_IOS
            _ios.Invoke();
            return;
#endif

        }

    }
}

