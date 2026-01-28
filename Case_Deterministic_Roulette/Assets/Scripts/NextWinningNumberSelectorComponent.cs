using Data;
using Events;
using Helpers;
using UnityEngine;

public class NextWinningNumberSelectorComponent : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private NextWinningNumberSelectorWidgetComponent prefab;
    [SerializeField] private Transform container;
    [SerializeField] private NumberSO[] numbers;

    private NextWinningNumberSelectorWidgetComponent _activeWidget;

    private void Start()
    {
        foreach (var number in numbers)
        {
            var widget = Instantiate(prefab, container);
            widget.Set(number);
        }
    }

    private void OnWidgetClicked(NextWinningNumberSelectorWidgetComponent obj)
    {
        if (_activeWidget != null)
            _activeWidget.DeSelect();

        obj.Select();

        EventBus<Event_OnNextWinningSelected>.Publish(new Event_OnNextWinningSelected(obj.Number));

        _activeWidget = obj;
    }

    private void OnReset(Event_OnReset obj)
    {
        canvasGroup.Show();

        if (_activeWidget == null) return;

        _activeWidget.DeSelect();
    }

    private void OnSpinStarted(Event_OnSpinStarted obj)
    {
        canvasGroup.Hide();
    }

    private void OnEnable()
    {
        NextWinningNumberSelectorWidgetComponent.OnClicked += OnWidgetClicked;
        EventBus<Event_OnReset>.Subscribe(OnReset);
        EventBus<Event_OnSpinStarted>.Subscribe(OnSpinStarted);
    }

    private void OnDisable()
    {
        NextWinningNumberSelectorWidgetComponent.OnClicked -= OnWidgetClicked;
        EventBus<Event_OnReset>.Unsubscribe(OnReset);
        EventBus<Event_OnSpinStarted>.Unsubscribe(OnSpinStarted);
    }

    private void OnValidate()
    {
        NumberHelper.TryLoadNumberAssets(out var numberAssets);
        numbers = numberAssets.ToArray();
        canvasGroup = GetComponent<CanvasGroup>();
    }
}