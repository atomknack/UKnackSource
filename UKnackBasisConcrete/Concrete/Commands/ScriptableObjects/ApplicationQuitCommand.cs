using UnityEngine;
using UKnack.Commands;

namespace UKnack.Concrete.Commands.ScriptableObjects
{

    [CreateAssetMenu(fileName = "ApplicationQuitCommand", menuName = "UKnack/Commands/ApplicationQuitCommand")]
    public class ApplicationQuitCommand : CommandScriptableObject
    {
        public override void Execute()
        {
            Application.Quit();
        }
    }
}