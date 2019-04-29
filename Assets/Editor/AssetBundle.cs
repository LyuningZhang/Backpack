using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class AssetBundle : MonoBehaviour
{
    //生成到的目录  
    public static string BuildPath = Application.dataPath + "/Ab";

    [MenuItem("Build/BuildAb")]
    public static void BuildWindowsResource()
    {
        if (Directory.Exists(BuildPath))
        {
            DeleteFolder(BuildPath);
        }
        else
        {
            Directory.CreateDirectory(BuildPath);
        }
        BuildPipeline.BuildAssetBundles(BuildPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();
    }

    public static void DeleteFolder(string dir)
    {
        foreach (string d in Directory.GetFileSystemEntries(dir))
        {
            if (File.Exists(d))
            {
                FileInfo fi = new FileInfo(d);
                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;
                File.Delete(d);
            }
            else
            {
                DirectoryInfo d1 = new DirectoryInfo(d);
                if (d1.GetFiles().Length != 0)
                {
                    DeleteFolder(d1.FullName);
                }
                Directory.Delete(d);
            }
        }
    }
}
