using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Events;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private NumberSO[] numbers;

    private BoardSession _session;

    private void Start()
    {
        //TODO LOAD FROM SAVED DATA
        _session = new BoardSession
        {
            ChipAmount = 99999,
            MinAllowedBetAmount = 100,
            MaxAllowedBetAmount = 400,
            MaxAllowedBetAmountTotal = 10000,
            PreviousResults = new List<NumberSO> { numbers[Random.Range(0, numbers.Length)] },
            ChipValues = new[]
            {
                100,
                200,
                400,
                800,
                2000
            }
        };

        EventBus<Event_OnSessionInitialized>.Publish(new Event_OnSessionInitialized(_session));
    }

    private void OnEnable()
    {
        EventBus<Event_OnSpinEnded>.Subscribe(OnSpinEnded);
    }

    private void OnDisable()
    {
        EventBus<Event_OnSpinEnded>.Unsubscribe(OnSpinEnded);
    }

    private void OnSpinEnded(Event_OnSpinEnded obj)
    {
        EventBus<Event_OnGetBoardRound>.Publish(new Event_OnGetBoardRound(round =>
        {
            var winningPlacements = round.PlacedBets.GroupBy(x => x.Placement)
                .Where(x => x.Key.PlacementData.bindings.Any(y => y == round.WinningNumber));

            var winningsAmount = winningPlacements.Sum(winningPlacement =>
                winningPlacement.Key.PlacementData.payout * winningPlacement.Sum(x => x.PlacedChip.Amount));

            Debug.LogWarning($"{_session.ChipAmount} += {winningsAmount} - {round.TotalBetAmount}");

            _session.ChipAmount += winningsAmount - round.TotalBetAmount;

            EventBus<Event_OnSessionUpdated>.Publish(new Event_OnSessionUpdated(_session));
        }));
    }

    private void OnValidate()
    {
        NumberHelper.TryLoadNumberAssets(out var numberAssets);
        numbers = numberAssets.ToArray();
    }
}