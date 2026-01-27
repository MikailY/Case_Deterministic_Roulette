using System;
using System.Collections.Generic;
using System.IO;
using Data;
using UnityEditor;
using UnityEngine;

namespace Helpers
{
    public static class NumberHelper
    {
        public static bool TryLoadNumberAssets(out List<NumberSO> assets)
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(NumberSO)}");

            assets = new List<NumberSO>();

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<NumberSO>(path);
                if (asset == null) continue;
                assets.Add(asset);
            }

            return assets.Count > 0;
        }

        [MenuItem("Tools/NumberHelper.Generate()")]
        public static void Generate()
        {
            const int amount = 37;
            const string folderPath = "Assets/Data/Numbers";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            for (var i = 0; i < amount; i++)
            {
                var instance = ScriptableObject.CreateInstance<NumberSO>();

                instance.value = i;
                instance.type = GetTypeByIndex(i);
                instance.color = GetColorByType(instance.type);

                var assetPath = $"{folderPath}/Number {i}.asset";
                var uniquePath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

                AssetDatabase.CreateAsset(instance, uniquePath);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();
        }

        //red:0,black:1,green:2
        private static int GetTypeByIndex(int index)
        {
            return index switch
            {
                0 => 2,
                >= 1 and <= 10 or >= 19 and <= 28 => index % 2 == 0 ? 1 : 0,
                _ => index % 2 == 0 ? 0 : 1
            };
        }

        private static Color GetColorByType(int type)
        {
            return type switch
            {
                0 => Color.red,
                1 => Color.black,
                2 => Color.green,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}