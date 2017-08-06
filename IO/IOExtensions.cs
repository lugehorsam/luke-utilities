using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class IOExtensions 
    {
        public static string[] GetAllFilesRecursive(string path)
        {
            List<string> paths = new List<string>();
            DirSearch(paths, path);
            return paths.ToArray();
        }
    
        static void DirSearch(List<string> listFilesFound, string sDir) 
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir)) 
                {
                    foreach (string f in Directory.GetFiles(d)) 
                    {
                        listFilesFound.Add(f);
                    }
                    DirSearch(listFilesFound, d);
                }
            }
            catch (System.Exception excpt) 
            {
                Diag.Report(excpt.Message);
            }
        }

        public static string GetPathToFileFromAssets(string fileName)
        {
            string [] paths = GetAllFilesRecursive(Application.dataPath);

            foreach (var path in paths)
            {
                string file = Path.GetFileName(path);
                if (file == fileName)
                {
                    return path;
                }
            }

            throw new Exception("Couldn't find filename in Unity: " + fileName);
        }

        public static string GetPathToDirectoryFromAssets(string directoryName)
        {
            return Directory.GetDirectories(Application.dataPath, directoryName, SearchOption.AllDirectories)
                .First();                  
        }
    }
}
