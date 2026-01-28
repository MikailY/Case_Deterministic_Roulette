using Data;
using UnityEngine;

public class PocketGO : MonoBehaviour
{
    [SerializeField] private NumberSO numberData;
    [SerializeField] private Transform target;
    [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform curvePoint;

    public NumberSO NumberData => numberData;
    public Transform Target => target;
    public Transform CurvePoint => curvePoint;
    public Transform EntryPoint => entryPoint;

    // private void OnDrawGizmos()
    // {
    //     if (numberData == null) return;
    //
    //     Gizmos.color = numberData.color;
    //
    //     Gizmos.DrawSphere(target.position, 0.025f);
    //
    //     // for (var i = 0; i < transform.childCount; i++)
    //     // {
    //     //     Gizmos.DrawSphere(transform.GetChild(i).position, 0.025f);
    //     // }
    // }

    private void OnValidate()
    {
        target = transform.GetChild(1);
    }
}