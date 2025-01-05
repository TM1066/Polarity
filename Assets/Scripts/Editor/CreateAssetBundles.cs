using System;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Assets/CreateAllAssetBundles")]
    private static void BuildAllAssetBundles()
    {
        string assetBundleDataPath = Application.dataPath + "/AssetBundles";

        if (!File.Exists(assetBundleDataPath))
        {
            File.Create(assetBundleDataPath);
        }

        try 
        {
            BuildPipeline.BuildAssetBundles (assetBundleDataPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error: " + e.Message);
        }
    }
}
