using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UKnack.Attributes;
using UKnack.Commands;

namespace UKnack.Concrete.Commands
{
    [AddComponentMenu("UKnack/Commands/DestroyGameObject")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class DestroyGameObjectCommand : CommandMonoBehaviour
    {
        [SerializeField]
        [MarkNullAsColor(0.1f, 0.3f, 0.1f, "This gameobject will self-destruct on Command if value is null")]
        private GameObject _toBeDestroyed;

        public override void Execute()
        {
            if (_toBeDestroyed == null)
                _toBeDestroyed = gameObject;

            Destroy(_toBeDestroyed);
        }
    }
}