﻿using UnityEngine;
using System;
using System.Linq;

namespace Scripting
{
    [Serializable]
    public class TextQuery
    {
        [SerializeField] private string contentId;
        [SerializeField] private string query;

        public string Resolve()
        {
            ScriptContentConfig associatedConfig =
                ScriptContentConfig.ContentConfigs.First((config) => config.Id == contentId);

            return associatedConfig.GetDisplayFromQuery(query);
        }
    }
}
