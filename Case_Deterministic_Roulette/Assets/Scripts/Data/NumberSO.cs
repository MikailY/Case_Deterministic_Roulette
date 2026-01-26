using UnityEngine;

[CreateAssetMenu(fileName = "NumberSO", menuName = "Scriptable Objects/NumberSO")]
public class NumberSO : ScriptableObject
{
    public int value;
    public int type;
    public Color color;
}