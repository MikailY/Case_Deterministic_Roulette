using Events;
using Helpers;
using TMPro;
using UnityEngine;

public class TopBarInfoComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text totalBetText;
    [SerializeField] private TMP_Text minMaxBetText;

    private int _balance;
    private int _totalBetAmount;

    private void OnSessionInitialized(Event_OnSessionInitialized obj)
    {
        StartCoroutine(balanceText.TextAnimation(0, obj.Session.ChipAmount, 0.25f, "BALANCE: {0}"));
        totalBetText.text = $"TOTAL BET: {TextHelper.FormatChip(0)}";
        minMaxBetText.text =
            $"LIMIT MIN {TextHelper.FormatChip(obj.Session.MinAllowedBetAmount)} / MAX {TextHelper.FormatChip(obj.Session.MaxAllowedBetAmount)}";
        _balance = obj.Session.ChipAmount;
    }

    private void OnBoardRoundUpdated(Event_OnBoardRoundUpdated obj)
    {
        StartCoroutine(balanceText.TextAnimation(_balance - _totalBetAmount, _balance - obj.Data.TotalBetAmount, 0.25f,
            "BALANCE: {0}"));
        StartCoroutine(totalBetText.TextAnimation(_totalBetAmount, obj.Data.TotalBetAmount, 0.25f, "TOTAL BET: {0}"));
        _totalBetAmount = obj.Data.TotalBetAmount;
    }

    private void OnSessionUpdated(Event_OnSessionUpdated obj)
    {
        _balance = obj.Session.ChipAmount;
    }

    private void OnEnable()
    {
        EventBus<Event_OnSessionInitialized>.Subscribe(OnSessionInitialized);
        EventBus<Event_OnBoardRoundUpdated>.Subscribe(OnBoardRoundUpdated);
        EventBus<Event_OnSessionUpdated>.Subscribe(OnSessionUpdated);
    }

    private void OnDisable()
    {
        EventBus<Event_OnSessionInitialized>.Unsubscribe(OnSessionInitialized);
        EventBus<Event_OnBoardRoundUpdated>.Unsubscribe(OnBoardRoundUpdated);
        EventBus<Event_OnSessionUpdated>.Unsubscribe(OnSessionUpdated);
    }
}