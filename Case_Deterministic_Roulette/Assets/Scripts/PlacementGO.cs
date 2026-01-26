using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementGO : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private PlacementSO placementData;
    [SerializeField] private Transform chipTarget;

    public static event Action<PlacementSO> OnClick;
    public static event Action<PlacementSO> OnEnter;
    public static event Action<PlacementSO> OnExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke(placementData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExit?.Invoke(placementData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(placementData);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = placementData == null ? Color.darkRed : Color.deepSkyBlue;

        Gizmos.DrawSphere(transform.position, 0.025f);
    }

    private void OnValidate()
    {
        if (placementData == null || gameObject.name == $"PlacementGO_{placementData.name}") return;
    
        name = $"PlacementGO_{placementData.name}";
    }
}