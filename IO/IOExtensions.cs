using UnityEngine;
using System.IO;
using System.Collections.Generic;

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
                Diagnostics.Report(excpt.Message);
            }
        }

        public static string GetFullPathToUnityFile(string fileName)
        {
            string [] paths = GetAllFilesRecursive(Application.dataPath);

            foreach (var path in paths)
            {
                if (Path.GetFileName(path) == fileName)
                {
                    return path;
                }
            }

            throw new System.Exception("Couldn't find Unity file: " + fileName);
        }
    }
}
