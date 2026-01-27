using System.Linq;
using Events;
using UnityEngine;


public class ChipsSelectorComponent : MonoBehaviour
{
    [SerializeField] private ChipSelectorComponentWidget[] widgets;

    private ChipSelectorComponentWidget _selectedWidget;

    private void OnChipWidgetClicked(ChipSelectorComponentWidget widget)
    {
        SelectWidget(widget);
    }

    private void OnSessionInitialized(Event_OnSessionInitialized obj)
    {
        for (var i = 0; i < widgets.Length; i++)
        {
            widgets[i].Set(obj.Session.ChipValues[i]);
        }

        SelectWidget(widgets.First());
    }

    private void SelectWidget(ChipSelectorComponentWidget widget)
    {
        if (_selectedWidget != null)
            _selectedWidget.DeSelect();

        widget.Select();

        EventBus<Event_OnChipSelected>.Publish(new Event_OnChipSelected(new PlacedChip(widget.Data, widget.Value)));

        _selectedWidget = widget;
    }

    private void OnEnable()
    {
        ChipSelectorComponentWidget.OnClicked += OnChipWidgetClicked;
        EventBus<Event_OnSessionInitialized>.Subscribe(OnSessionInitialized);
    }

    private void OnDisable()
    {
        ChipSelectorComponentWidget.OnClicked -= OnChipWidgetClicked;
        EventBus<Event_OnSessionInitialized>.Unsubscribe(OnSessionInitialized);
    }

    private void OnValidate()
    {
        widgets = GetComponentsInChildren<ChipSelectorComponentWidget>();
    }
}