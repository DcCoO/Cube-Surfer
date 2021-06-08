using UnityEngine;
using UnityEditor;

public class CreateLevels
{
    [MenuItem("Assets/Create/Procedural Level")]
    public static void CreateLevel()
    {
        Level asset = ScriptableObject.CreateInstance<Level>();

        int numPaths = 5;

        int numParts = Random.Range(6, 13);
        asset.parts = new int[numParts];
        asset.parts[numParts - 1] = 4;
        for (int i = 0; i < numParts - 1; ++i) asset.parts[i] = Random.Range(0, numPaths);

        AssetDatabase.CreateAsset(asset, $"Assets/Prefabs/Levels/Level {Random.Range(0, 10000)}.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}