using UKnack.Attributes;
using UnityEngine;

namespace UKnack.Concrete.Commmon
{
    [AddComponentMenu("UKnack/Common/DontDestroyOnLoad")]
    public sealed class DontDestroyOnLoad_InvokedOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}

