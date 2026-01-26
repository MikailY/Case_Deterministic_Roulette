using UnityEngine;

public class WheelGO : MonoBehaviour
{
    [SerializeField] private PocketGO[] pocketObjects;

    private void OnValidate()
    {
        pocketObjects = GetComponentsInChildren<PocketGO>();
    }
}