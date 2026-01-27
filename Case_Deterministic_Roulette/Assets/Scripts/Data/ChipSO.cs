using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ChipSO", menuName = "Scriptable Objects/ChipSO")]
    public class ChipSO : ScriptableObject
    {
        public Color color;
        public ChipObject prefab;
    }
}