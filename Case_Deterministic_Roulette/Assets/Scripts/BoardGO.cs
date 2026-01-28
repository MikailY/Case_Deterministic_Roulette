using System.Linq;
using Events;
using UnityEngine;

public class BoardGO : MonoBehaviour
{
    [SerializeField] private WheelGO wheelObject;
    [SerializeField] private PlacementGO[] placementObjects;
    [SerializeField] private TableNumberGO[] tableNumberObjects;

    private bool _isInteractable;

    private void PlacementGOOnOnClick(PlacementGO obj)
    {
        if (_isInteractable) return;

        EventBus<Event_OnPlacementClicked>.Publish(new Event_OnPlacementClicked(obj));
    }

    private void PlacementGOOnOnEnter(PlacementGO obj)
    {
        if (_isInteractable) return;

        foreach (var tableNumberGo in tableNumberObjects.Where(x => obj.PlacementData.bindings.Contains(x.NumberData)))
        {
            tableNumberGo.Highlight();
        }
    }

    private void PlacementGOOnOnExit(PlacementGO obj)
    {
        if (_isInteractable) return;

        foreach (var tableNumberGo in tableNumberObjects.Where(x => obj.PlacementData.bindings.Contains(x.NumberData)))
        {
            tableNumberGo.Unhighlight();
        }
    }

    private void OnSpinStarted(Event_OnSpinStarted obj)
    {
        foreach (var tableNumberGo in tableNumberObjects)
        {
            tableNumberGo.Unhighlight();
        }

        _isInteractable = true;

        wheelObject.StartBall(obj.Result,
            () =>
            {
                EventBus<Event_OnSpinEnded>.Publish(new Event_OnSpinEnded(obj.Result));
            });
    }

    private void OnReset(Event_OnReset obj)
    {
        _isInteractable = false;
    }

    private void OnPlacedBet(Event_OnPlacedBet obj)
    {
        obj.PlacedBet.Placement.Add(obj.PlacedBet.PlacedChip.Chip, obj.TotalAmountOnPlacement);
    }

    private void OnUndoBet(Event_OnUndoBet obj)
    {
        obj.PlacedBet.Placement.Remove(obj.TotalAmountOnPlacement);
    }

    private void OnClearedBets(Event_OnClearedBets obj)
    {
        foreach (var placement in obj.Placements)
            placement.Clear();
    }

    private void OnRepeatBet(Event_OnRepeatedBet obj)
    {
        foreach (var placedBet in obj.PlacedBets)
            placedBet.Placement.Add(placedBet.PlacedChip.Chip, placedBet.PlacedChip.Amount);
    }

    private void OnEnable()
    {
        PlacementGO.OnClick += PlacementGOOnOnClick;
        PlacementGO.OnEnter += PlacementGOOnOnEnter;
        PlacementGO.OnExit += PlacementGOOnOnExit;

        EventBus<Event_OnSpinStarted>.Subscribe(OnSpinStarted);
        EventBus<Event_OnReset>.Subscribe(OnReset);
        EventBus<Event_OnPlacedBet>.Subscribe(OnPlacedBet);
        EventBus<Event_OnUndoBet>.Subscribe(OnUndoBet);
        EventBus<Event_OnClearedBets>.Subscribe(OnClearedBets);
        EventBus<Event_OnRepeatedBet>.Subscribe(OnRepeatBet);
    }

    private void OnDisable()
    {
        PlacementGO.OnClick -= PlacementGOOnOnClick;
        PlacementGO.OnEnter -= PlacementGOOnOnEnter;
        PlacementGO.OnExit -= PlacementGOOnOnExit;

        EventBus<Event_OnSpinStarted>.Unsubscribe(OnSpinStarted);
        EventBus<Event_OnReset>.Unsubscribe(OnReset);
        EventBus<Event_OnPlacedBet>.Unsubscribe(OnPlacedBet);
        EventBus<Event_OnUndoBet>.Unsubscribe(OnUndoBet);
        EventBus<Event_OnClearedBets>.Unsubscribe(OnClearedBets);
        EventBus<Event_OnRepeatedBet>.Unsubscribe(OnRepeatBet);
    }

    private void OnValidate()
    {
        wheelObject = GetComponentInChildren<WheelGO>();
        placementObjects = GetComponentsInChildren<PlacementGO>();
        tableNumberObjects = GetComponentsInChildren<TableNumberGO>();
    }
}