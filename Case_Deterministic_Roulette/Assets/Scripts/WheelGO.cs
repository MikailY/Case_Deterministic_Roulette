using UnityEngine;

public class WheelGO : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PocketGO[] pocketObjects;

    private void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }

    private void OnValidate()
    {
        pocketObjects = GetComponentsInChildren<PocketGO>();
    }
}