namespace Command
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    using Utilities;

    public class CommandBehaviour : MonoBehaviour
    {
        [SerializeField] private List<CommandData> _commands;

        private void Start()
        {
            var cmdQueue = new CommandQueue();
            
            foreach (var command in _commands)
            {
                var runCmd = command.CommandObject.Run(gameObject);

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
