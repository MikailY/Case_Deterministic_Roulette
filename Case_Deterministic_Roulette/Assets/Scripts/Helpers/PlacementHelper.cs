using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Helpers
{
    public static class PlacementHelper
    {
        public static bool TryLoadPlacementAssets(out List<PlacementSO> assets)
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(PlacementSO)}");

            assets = new List<PlacementSO>();

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<PlacementSO>(path);
                if (asset == null) continue;
                assets.Add(asset);
            }

            return assets.Count > 0;
        }

        [MenuItem("Tools/PlacementHelper.GenerateStraightPlacements()")]
        public static void GenerateStraightPlacements()
        {
            const int amount = 37;
            const string folderPath = "Assets/Data/Placements";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (!NumberHelper.TryLoadNumberAssets(out var numbers))
            {
                Debug.LogError("There 's no rewards data loaded");
                return;
            }

            if (numbers.Count != amount)
            {
                Debug.LogError($"Numbers  count({numbers.Count}) does not match {amount}");
                return;
            }

            foreach (var numberSo in numbers)
            {
                var instance = ScriptableObject.CreateInstance<PlacementSO>();

                instance.bindings = new[] { numberSo };
                instance.payout = 35;

                var assetPath = $"{folderPath}/straight {numberSo.value}.asset";
                var uniquePath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

                AssetDatabase.CreateAsset(instance, uniquePath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();
        }
    }
}