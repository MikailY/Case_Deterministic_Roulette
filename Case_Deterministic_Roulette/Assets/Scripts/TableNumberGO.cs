using UnityEngine;

public class TableNumberGO : MonoBehaviour
{
    [SerializeField] private NumberSO numberData;
    [SerializeField] private GameObject highlightObject;

    public NumberSO NumberData => numberData;

    public void Highlight()
    {
        highlightObject.SetActive(true);
    }

    public void Unhighlight()
    {
        highlightObject.SetActive(false);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellowNice;
    //
    //     Gizmos.DrawSphere(transform.position, 0.025f);
    // }
}