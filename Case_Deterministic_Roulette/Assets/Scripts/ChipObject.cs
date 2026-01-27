using Data;
using UnityEngine;

public class ChipObject : MonoBehaviour
{
    [SerializeField] private Transform textTransform;
    public ChipSO Chip { get; set; }
    public Transform TextTransform => textTransform;
}