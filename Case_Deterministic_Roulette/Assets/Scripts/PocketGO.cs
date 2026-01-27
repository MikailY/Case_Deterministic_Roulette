using Data;
using UnityEngine;

public class PocketGO : MonoBehaviour
{
    [SerializeField] private NumberSO numberData;

    private void OnDrawGizmos()
    {
        if (numberData == null) return;

        Gizmos.color = numberData.color;

        for (var i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.025f);
        }
    }
}