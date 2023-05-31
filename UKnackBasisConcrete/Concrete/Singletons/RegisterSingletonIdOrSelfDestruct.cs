using System;
using UKnack.Attributes;
using UKnack.Singletons;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Concrete.NamedSingletons
{

    [AddComponentMenu("UKnack/Singletons/RegisterSingletonIdOrSelfDestruct")]
    [DisallowMultipleComponent]
    internal class RegisterSingletonIdOrSelfDestruct : MonoBehaviour
    {
        [SerializeField]
        [ValidReference]
        private ScriptableSingletonIdentity _scriptableSingletonId;

        [SerializeField]
        private UnityEvent _onRegisterSuccess;
        [SerializeField]
        private UnityEvent _onRegisterFail;

        private bool _isRegistered;
        private ScriptableSingletonIdentity.SuccessfullyRegisteredTag _registered;

        private void Awake()
        {
            if (_scriptableSingletonId == null)
                throw new ArgumentNullException(nameof(_scriptableSingletonId));

            if (_scriptableSingletonId.TryRegister(out _registered))
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
                _scriptableSingletonId.Unregister(_registered);
                _isRegistered = false;
            }
        }
    }
}

