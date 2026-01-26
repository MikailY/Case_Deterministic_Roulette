using UnityEditor;
using UnityEngine;

namespace Helpers
{
    public static class PocketHelper
    {
        public static void GeneratePockets(PocketGO prefab, Transform parent)
        {
            const float angle = 360f / 37;

            for (var i = 0; i < 37; i++)
            {
                var go = (PocketGO)PrefabUtility.InstantiatePrefab(prefab, parent);
                var rotValue = angle * i;
                go.transform.localEulerAngles = new Vector3(0, rotValue, 0);
                go.name = $"Pocket-{i}";
            }
        }
    }
}