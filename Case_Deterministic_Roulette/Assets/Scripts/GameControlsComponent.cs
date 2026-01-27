using Events;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class GameControlsComponent : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button undoBetButton;
    [SerializeField] private Button repeatBetButton;
    [SerializeField] private Button clearBetButton;
    [SerializeField] private Button spinButton;

    private void OnSessionInitialized(Event_OnSessionInitialized obj)
    {
        canvasGroup.Show();

        EventBus<Event_OnGetBoardRound>.Publish(new Event_OnGetBoardRound((round =>
        {
            repeatBetButton.interactable = round.HasPreviousBet;

            undoBetButton.interactable = false;
        })));
    }

    private void OnSpinStarted(Event_OnSpinStarted obj)
    {
        canvasGroup.Hide();
    }

    private void OnSpinEnded(Event_OnSpinEnded obj)
    {
    }

    private void OnReset(Event_OnReset obj)
    {
        canvasGroup.Show();
    }

    private void OnBoardRoundUpdated(Event_OnBoardRoundUpdated obj)
    {
        var hasPlacedBet = obj.Data.PlacedBets.Count > 0;
        repeatBetButton.interactable = obj.Data.HasPreviousBet;
        undoBetButton.interactable = hasPlacedBet;
        clearBetButton.interactable = hasPlacedBet;
        spinButton.interactable = hasPlacedBet;
    }

    private void UndoBetButtonClicked()
    {
        EventBus<Event_OnUndoBetButtonClicked>.Publish(new Event_OnUndoBetButtonClicked());
    }

    private void RepeatBetButtonClicked()
    {
        EventBus<Event_OnRepeatBetButtonClicked>.Publish(new Event_OnRepeatBetButtonClicked());
    }

    private void ClearBetButtonClicked()
    {
        EventBus<Event_OnClearBetButtonClicked>.Publish(new Event_OnClearBetButtonClicked());
    }

    private void SpinButtonClicked()
    {
        EventBus<Event_OnSpinButtonClicked>.Publish(new Event_OnSpinButtonClicked());
    }

    private void OnEnable()
    {
        repeatBetButton.onClick.AddListener(RepeatBetButtonClicked);
        undoBetButton.onClick.AddListener(UndoBetButtonClicked);
        clearBetButton.onClick.AddListener(ClearBetButtonClicked);
        spinButton.onClick.AddListener(SpinButtonClicked);

        EventBus<Event_OnSessionInitialized>.Subscribe(OnSessionInitialized);
        EventBus<Event_OnSpinStarted>.Subscribe(OnSpinStarted);
        EventBus<Event_OnSpinEnded>.Subscribe(OnSpinEnded);
        EventBus<Event_OnReset>.Subscribe(OnReset);
        EventBus<Event_OnBoardRoundUpdated>.Subscribe(OnBoardRoundUpdated);
    }

    private void OnDisable()
    {
        repeatBetButton.onClick.RemoveListener(RepeatBetButtonClicked);
        undoBetButton.onClick.RemoveListener(UndoBetButtonClicked);
        clearBetButton.onClick.RemoveListener(ClearBetButtonClicked);
        spinButton.onClick.RemoveListener(SpinButtonClicked);

        EventBus<Event_OnSessionInitialized>.Unsubscribe(OnSessionInitialized);
        EventBus<Event_OnSpinStarted>.Unsubscribe(OnSpinStarted);
        EventBus<Event_OnSpinEnded>.Unsubscribe(OnSpinEnded);
        EventBus<Event_OnReset>.Unsubscribe(OnReset);
        EventBus<Event_OnBoardRoundUpdated>.Unsubscribe(OnBoardRoundUpdated);
    }

    private void OnValidate()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
}