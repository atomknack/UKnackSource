using UKnack;
using UKnack.Commands;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UKnack.Concrete.Commands.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LoadSceneSingleSyncCommand", menuName = "UKnack/Commands/LoadSceneSingleSync")]
    public class LoadSceneSingleSyncCommand : CommandScriptableObject, ICommand<string>
    {
        [SerializeField]
        private string _sceneName;

        public override void Execute()
        {
            Execute(_sceneName);
        }

        public void Execute(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

    }
}

