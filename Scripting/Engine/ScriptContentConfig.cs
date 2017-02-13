using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Scripting
{
    public abstract class ScriptContentConfig<TScriptObject> : ScriptContentConfig
        where TScriptObject : ScriptObject {

        private List<TScriptObject> content = new List<TScriptObject>();

        public TScriptObject HandleQuery(TextQuery query)
        {
            TScriptObject[] queryCandidates = GetQueryCandidates(query);
            return queryCandidates.First();
        }

        TScriptObject[] GetQueryCandidates(TextQuery query)
        {
            var foundObjects = new List<TScriptObject>();

            var contentObj = JsonUtility.FromJson<TScriptObject>(query.QueryJSON);
            FieldInfo[] relevantFields = ReflectionUtils.GetNonDefaultFields(contentObj);

            foreach (TScriptObject obj in content)
            {
                foreach (var field in relevantFields)
                {
                    if (field.GetValue(obj) == field.GetValue(contentObj))
                    {
                        foundObjects.Add(contentObj);
                    }
                }
            }
            return foundObjects.ToArray();
        }
    }

    public abstract class ScriptContentConfig
    {
        public static List<ScriptContentConfig> ContentConfigs
        {
            get { return ContentConfigs; }
        }

        private readonly List<ScriptContentConfig> contentConfigs = new List<ScriptContentConfig>();

        public abstract string Id { get; }
        public abstract string ResourcesPath { get; }
    }
}

