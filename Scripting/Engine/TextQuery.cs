using UnityEngine;
using System;
using System.Linq;

namespace Scripting
{
    [Serializable]
    public class TextQuery<TScriptObject> where TScriptObject : ScriptObject
    {
        [SerializeField] private TScriptObject query;

        public string Resolve()
        {
            Debug.Log("Content configs length is " + ScriptContentConfig.ContentConfigs.Count);
            ScriptContentConfig associatedConfig =
                ScriptContentConfig.ContentConfigs.FirstOrDefault(
                    (config) =>
                    {
                        return config.Id == contentId;
                    }
                );

            if (associatedConfig == null)
            {
                throw new Exception(string.Format("Content with id {0} and query {1} is missing config", contentId, query));
            }
            return associatedConfig.GetDisplayFromQuery(query);
        }
    }

    [Serializable]
    public class TextQuery
    {
        [SerializeField] private string contentId;


    }
}
