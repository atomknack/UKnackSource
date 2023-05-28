using System;
using System.Collections.Generic;
using UKnack.Attributes;
using UKnack.Preconcrete.NamedSingletons;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Concrete.NamedSingletons
{

    [AddComponentMenu("UKnack/NamedSingletons/NamedSingletonRegisterOrSelfDestuct")]
    [DisallowMultipleComponent]
    internal class RegisterNamedSingletonOrSelfDestruct : MonoBehaviour
    {
        [SerializeField]
        [ValidReference]
        private NamedSingletonIdentity _namedSingletonId;

        [SerializeField]
        private UnityEvent _onRegisterSuccess;
        [SerializeField]
        private UnityEvent _onRegisterFail;

        private bool _isRegistered;
        private NamedSingletonIdentity.SuccessfullyRegisteredTag _registered;

        private void Awake()
        {
            if (_namedSingletonId == null)
                throw new ArgumentNullException(nameof(_namedSingletonId));

            if (_namedSingletonId.TryRegister(out _registered))
            {
                _isRegistered = true;
                _onRegisterSuccess.Invoke();
                return;
            }

            _isRegistered = false;
            Destroy(gameObject);
            _onRegisterFail.Invoke();
        }


        private void OnDestroy()
        {
            if (_isRegistered)
            {
                _namedSingletonId.Unregister(_registered);
                _isRegistered = false;
            }
        }
    }
}

