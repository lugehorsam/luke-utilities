namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using UnityEngine;

    public static class IOExt
    {
        public static string[] GetAllFilesRecursive(string path)
        {
            var paths = new List<string>();
            DirSearch(paths, path);
            return paths.ToArray();
        }

        private static void DirSearch(List<string> listFilesFound, string sDir)
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
            catch (Exception excpt)
            {
                Diag.Report(excpt.Message);
            }
        }

        public static string GetPathToFileFromAssets(string fileName)
        {
            string[] paths = GetAllFilesRecursive(Application.dataPath);

            foreach (string path in paths)
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
            return Directory.GetDirectories(Application.dataPath, directoryName, SearchOption.AllDirectories).First();
        }
    }
}
