using System;
using Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementGO : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private PlacementSO placementData;
    [SerializeField] private ChipHandler chipHandler;

    public static event Action<PlacementGO> OnClick;
    public static event Action<PlacementGO> OnEnter;
    public static event Action<PlacementGO> OnExit;

    public PlacementSO PlacementData => placementData;

    public void Add(ChipSO chip, int amount)
    {
        chipHandler.Insert(chip, amount);
    }

    public void Remove(int amount)
    {
        chipHandler.Remove(amount);
    }

    public void Clear()
    {
        chipHandler.Clear();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExit?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
    }

    private void OnValidate()
    {
        if (placementData == null || gameObject.name == $"PlacementGO_{placementData.name}") return;

        name = $"PlacementGO_{placementData.name}";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = placementData == null ? Color.darkRed : Color.deepSkyBlue;

        Gizmos.DrawSphere(transform.position, 0.025f);
    }
}