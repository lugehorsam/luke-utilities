using System;
using UnityEngine;
using Scripting;

namespace Utilities.Scripting
{
    [Serializable]
    public class PromptDatum : TextDatum
    {
        public override string ResourcesPath
        {
            get;
        }
    }    

}
