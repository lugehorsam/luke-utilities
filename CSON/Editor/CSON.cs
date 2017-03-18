﻿using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class CSON
{
    private const string BASH_SCRIPT_PATH = "/Editor/CSON.sh";
    private const string CSON_PATH = "/Content/";
    private const string JSON_PATH = "/Resources/JSON/";
    private static bool TRANSPILE_ON_PLAY = false;

    static CSON()
    {
        EditorApplication.playmodeStateChanged += HandlePlaymodeStateChanged;
    }

    static void HandlePlaymodeStateChanged()
    {
        if (TRANSPILE_ON_PLAY)
            CSONToJSON();
    }

    [MenuItem("Assets/CSON To JSON")]
    public static void CSONToJSON()
    {
        BashScript script = new BashScript
        (
            Application.dataPath + BASH_SCRIPT_PATH,
            new [] {
                Application.dataPath + CSON_PATH,
                Application.dataPath + JSON_PATH
            },
            Application.dataPath
        );

        try
        {
            script.Run();
        }
        catch (BashScriptException e)
        {
            Debug.LogError(e.Message);
        }
        Debug.Log(script.StdOut);
    }
}
