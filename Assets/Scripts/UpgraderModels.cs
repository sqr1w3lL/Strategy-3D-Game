using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class UpgradeMeta
{
    [MenuItem("Tools/Upgrade All FBX Metadata")]
    private static void Upgrade()
    {
        string[] guids = AssetDatabase.FindAssets("t:Model", new[] { "Assets" });

        List<string> fbxPaths = new List<string>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            if (Path.GetExtension(path).ToLower() == ".fbx")
            {
                fbxPaths.Add(path);
            }
        }

        if (fbxPaths.Count == 0)
        {
            Debug.LogWarning("No FBX models found!");
            return;
        }

        AssetDatabase.ForceReserializeAssets(
            fbxPaths,
            ForceReserializeAssetsOptions.ReserializeMetadata
        );

        Debug.Log($"Metadata upgrade completed! Processed {fbxPaths.Count} FBX files.");
    }
}
