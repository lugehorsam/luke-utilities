using System;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Scripting
{
    public abstract class ScriptContentConfig<TScriptObject> : ScriptContentConfig
        where TScriptObject : ScriptObject {

        private List<TScriptObject> content = new List<TScriptObject>();


        public override string GetDisplayFromQuery(string queryJson)
        {
            return GetObjectFromQuery(queryJson).Display;
        }

        TScriptObject GetObjectFromQuery(string queryJson)
        {
            TScriptObject[] queryCandidates = GetQueryCandidates(queryJson);
            if (queryCandidates.Length == 0)
            {
                throw new Exception(string.Format("No valid candidates found for query json {0}", queryJson));
            }
            return queryCandidates.First();
        }

        TScriptObject[] GetQueryCandidates(string queryJson)
        {
            var foundObjects = new List<TScriptObject>();

            var contentObj = JsonUtility.FromJson<TScriptObject>(queryJson);
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
        public static ReadOnlyCollection<ScriptContentConfig> ContentConfigs
        {
            get { return new ReadOnlyCollection<ScriptContentConfig>(contentConfigs); }
        }

        static readonly List<ScriptContentConfig> contentConfigs = new List<ScriptContentConfig>();

        public abstract string Id { get; }
        public abstract string ResourcesPath { get; }
        public abstract string GetDisplayFromQuery(string queryJson);

        public ScriptContentConfig()
        {
            Debug.Log("adding content config");
            contentConfigs.Add(this);
        }
    }
}

