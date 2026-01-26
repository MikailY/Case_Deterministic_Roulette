using UnityEngine;

[CreateAssetMenu(fileName = "ChipSO", menuName = "Scriptable Objects/ChipSO")]
public class ChipSO : ScriptableObject
{
    public int value;
    public Color color;
    public Sprite sprite;
}