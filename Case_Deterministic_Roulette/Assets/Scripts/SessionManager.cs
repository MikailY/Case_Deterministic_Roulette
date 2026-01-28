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
            Statistics = new SessionStatistics(),
            ChipValues = new[]
            {
                100,
                200,
                400,
                800,
                2000
            }
        };

        for (var i = 0; i < Random.Range(5, 10); i++)
        {
            _session.PreviousResults.Add(numbers[Random.Range(0, numbers.Length)]);
        }

        EventBus<Event_OnSessionInitialized>.Publish(new Event_OnSessionInitialized(_session));
    }

    private void OnGetBoardSession(Event_OnGetBoardSession obj)
    {
        obj.OnGetAction?.Invoke(_session);
    }

    private void OnSpinEnded(Event_OnSpinEnded obj)
    {
        EventBus<Event_OnGetBoardRound>.Publish(new Event_OnGetBoardRound(round =>
        {
            var winningPlacements = round.PlacedBets.GroupBy(x => x.Placement)
                .Where(x => x.Key.PlacementData.bindings.Any(y => y == round.WinningNumber));

            var winningsAmount = winningPlacements.Sum(winningPlacement =>
                (winningPlacement.Key.PlacementData.payout + 1) * winningPlacement.Sum(x => x.PlacedChip.Amount));

            _session.ChipAmount += winningsAmount - round.TotalBetAmount;

            _session.PreviousResults.Insert(0, round.WinningNumber);
            _session.PreviousResults = _session.PreviousResults.Take(10).ToList();

            _session.Statistics.TotalBetAmount += round.TotalBetAmount;
            _session.Statistics.TotalWin += winningsAmount;
            _session.Statistics.TotalSpins += 1;
            if (winningsAmount > _session.Statistics.HighestWin)
                _session.Statistics.HighestWin = winningsAmount;

            Debug.Log($"STATS;\n" +
                      $"TotalSpins:{_session.Statistics.TotalSpins}\n" +
                      $"TotalWin:{_session.Statistics.TotalWin}\n" +
                      $"TotalBetAmount:{_session.Statistics.TotalBetAmount}\n" +
                      $"OverallResult:{_session.Statistics.OverallResult}\n" +
                      $"HighestWin:{_session.Statistics.HighestWin}\n" +
                      $"");

            EventBus<Event_OnSessionUpdated>.Publish(new Event_OnSessionUpdated(_session));
        }));
    }

    private void OnEnable()
    {
        EventBus<Event_OnGetBoardSession>.Subscribe(OnGetBoardSession);
        EventBus<Event_OnSpinEnded>.Subscribe(OnSpinEnded);
    }

    private void OnDisable()
    {
        EventBus<Event_OnGetBoardSession>.Unsubscribe(OnGetBoardSession);
        EventBus<Event_OnSpinEnded>.Unsubscribe(OnSpinEnded);
    }

    private void OnValidate()
    {
        NumberHelper.TryLoadNumberAssets(out var numberAssets);
        numbers = numberAssets.ToArray();
    }
}