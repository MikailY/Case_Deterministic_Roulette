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

    private void OnSessionInitialized(Event_OnSessionInitialized obj)
    {
        _balance = obj.Session.ChipAmount;
        balanceText.text = $"BALANCE: {StringHelper.FormatChip(_balance)}";
        totalBetText.text = $"TOTAL BET: {StringHelper.FormatChip(0)}";
        minMaxBetText.text =
            $"LIMIT MIN {StringHelper.FormatChip(obj.Session.MinAllowedBetAmount)} / MAX {StringHelper.FormatChip(obj.Session.MaxAllowedBetAmount)}";
    }

    private void OnBoardRoundUpdated(Event_OnBoardRoundUpdated obj)
    {
        totalBetText.text = $"TOTAL BET: {StringHelper.FormatChip(obj.Data.TotalBetAmount)}";
        balanceText.text = $"BALANCE: {StringHelper.FormatChip(_balance - obj.Data.TotalBetAmount)}";
    }

    private void OnSessionUpdated(Event_OnSessionUpdated obj)
    {
        _balance = obj.Session.ChipAmount;
        balanceText.text = $"BALANCE: {StringHelper.FormatChip(_balance)}";
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