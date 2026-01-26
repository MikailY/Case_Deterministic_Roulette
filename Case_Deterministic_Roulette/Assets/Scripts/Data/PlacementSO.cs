using System.Linq;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PlacementSO))]
[CanEditMultipleObjects]
public class PlacementSOEditor : Editor
{
    private string _pattern;

    public override void OnInspectorGUI()
    {
        var items = targets.OfType<PlacementSO>().ToArray();

        if (items.Length <= 0) return;

        base.OnInspectorGUI();

        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("Naming Pattern:", GUILayout.Width(150));
            _pattern = EditorGUILayout.TextField(_pattern, GUILayout.ExpandWidth(true));
        }

        GUI.enabled = !string.IsNullOrEmpty(_pattern);

        if (GUILayout.Button(
                $"Rename Targets By Pattern (ex. {(string.IsNullOrEmpty(_pattern) ? "[pattern]" : _pattern)} 0_1_2...)"))
        {
            RenameItems(items, _pattern);
        }

        GUI.enabled = true;
    }

    private static void RenameItems(PlacementSO[] items, string pattern)
    {
        foreach (var item in items)
        {
            Rename(item, pattern);
        }
    }

    private static void Rename(PlacementSO data, string pattern)
    {
        if (string.IsNullOrEmpty(pattern)) return;

        var newName = pattern + " " + data.bindings.Select(x => x.value.ToString()).Aggregate((a, b) => $"{a}_{b}");

        var assetPath = AssetDatabase.GetAssetPath(data);
        var result = AssetDatabase.RenameAsset(assetPath, newName);

        if (string.IsNullOrEmpty(result))
        {
            Debug.Log($"Successfully renamed to: {newName}");
            AssetDatabase.SaveAssets();
        }
        else
        {
            Debug.LogError($"Error renaming asset: {result}");
        }
    }
}

[CreateAssetMenu(fileName = "PlacementSO", menuName = "Scriptable Objects/PlacementSO")]
public class PlacementSO : ScriptableObject
{
    public int payout;
    public NumberSO[] bindings;
}