using UnityEngine;
using UKnack.Commands;

namespace UKnack.Concrete.Commands.ScriptableObjects
{

    [AddComponentMenu("UKnack/Commands/ApplicationQuitCommand")]
    public class ApplicationQuitCommand : CommandScriptableObject
    {
        public override void Execute()
        {
            Application.Quit();
        }
    }
}