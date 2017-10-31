namespace Command
{
    using System;
    using UnityEngine;
    using Utilities;
    
    [Serializable] 
    public class CommandData
    {
        [SerializeField] private CommandMode _commandMode;
        [SerializeField] private CommandObject _commandObject;

        public CommandObject CommandObject
        {
            get { return _commandObject; }
        }
            
        public CommandMode CommandMode
        {
            get { return _commandMode; }
        }
    }
}