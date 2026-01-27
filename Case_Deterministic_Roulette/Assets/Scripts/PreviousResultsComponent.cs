using System.Linq;
using Events;
using Helpers;
using UnityEngine;

public class PreviousResultsComponent : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private PreviousResultsComponentWidget activeWidget;
    [SerializeField] private PreviousResultsComponentWidget[] widgets;

    private void OnSessionInitialized(Event_OnSessionInitialized obj)
    {
        activeWidget.Set();

        for (var index = 0; index < widgets.Length; index++)
        {
            var widget = widgets[index];
            var previousResult = obj.Session.PreviousResults.ElementAtOrDefault(index);
            if (previousResult == null)
                widget.Set();
            else
                widget.Set(previousResult);
        }
    }

    private void OnSpinStarted(Event_OnSpinStarted obj)
    {
        canvasGroup.Hide();
    }

    private void OnReset(Event_OnReset obj)
    {
        EventBus<Event_OnGetBoardRound>.Publish(new Event_OnGetBoardRound((round =>
        {
            activeWidget.Set(round.WinningNumber);

            canvasGroup.Show();
        })));
    }

    private void OnEnable()
    {
        EventBus<Event_OnSessionInitialized>.Subscribe(OnSessionInitialized);
        EventBus<Event_OnSpinStarted>.Subscribe(OnSpinStarted);
        EventBus<Event_OnReset>.Subscribe(OnReset);
    }

    private void OnDisable()
    {
        EventBus<Event_OnSessionInitialized>.Unsubscribe(OnSessionInitialized);
        EventBus<Event_OnSpinStarted>.Unsubscribe(OnSpinStarted);
        EventBus<Event_OnReset>.Unsubscribe(OnReset);
    }

    private void OnValidate()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
}