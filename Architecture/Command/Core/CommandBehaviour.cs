namespace Enact
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Utilities;

    public class CommandBehaviour : MonoBehaviour
    {
        [SerializeField] private List<CommandData> _commands;

        private void Start()
        {
            Diag.Log(EnactFeatures.Effector, $"{this} running with {_commands.Count} commands.");
            var cmdQueue = new CommandQueue();
            
            foreach (var command in _commands)
            {
                IEnumerator runCmd = command.CommandObject.Run(gameObject);                 
                
                if (command.CommandMode == CommandMode.Parallel)
                {
                    cmdQueue.AddParallel(runCmd);
                }
                else
                {
                    cmdQueue.AddSerial(runCmd);
                }
            }

            StartCoroutine(cmdQueue);
        }
    }
}
