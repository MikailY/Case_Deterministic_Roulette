using System;
using System.Linq;
using UnityEngine;

public class BoardGO : MonoBehaviour
{
    [SerializeField] private WheelGO wheelObject;
    [SerializeField] private PlacementGO[] placementObjects;
    [SerializeField] private TableNumberGO[] tableNumberObjects;

    private void OnValidate()
    {
        wheelObject = GetComponentInChildren<WheelGO>();
        placementObjects = GetComponentsInChildren<PlacementGO>();
        tableNumberObjects = GetComponentsInChildren<TableNumberGO>();
    }

    private void PlacementGOOnOnClick(PlacementSO data)
    {
    }

    private void PlacementGOOnOnEnter(PlacementSO data)
    {
        foreach (var tableNumberGo in tableNumberObjects.Where(x => data.bindings.Contains(x.NumberData)))
        {
            tableNumberGo.Highlight();
        }
    }

    private void PlacementGOOnOnExit(PlacementSO data)
    {
        foreach (var tableNumberGo in tableNumberObjects.Where(x => data.bindings.Contains(x.NumberData)))
        {
            tableNumberGo.Unhighlight();
        }
    }

    private void OnEnable()
    {
        PlacementGO.OnClick += PlacementGOOnOnClick;
        PlacementGO.OnEnter += PlacementGOOnOnEnter;
        PlacementGO.OnExit += PlacementGOOnOnExit;
    }

    private void OnDisable()
    {
        PlacementGO.OnClick -= PlacementGOOnOnClick;
        PlacementGO.OnEnter -= PlacementGOOnOnEnter;
        PlacementGO.OnExit -= PlacementGOOnOnExit;
    }
}