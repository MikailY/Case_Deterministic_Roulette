using System.Collections;
using System.Linq;
using Data;
using Events;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private NumberSO[] numbers;

    private readonly BoardRound _boardRound = new();

    private void OnGetBoardRound(Event_OnGetBoardRound obj)
    {
        obj.OnGetAction?.Invoke(_boardRound);
    }

    private void OnPlacementClicked(Event_OnPlacementClicked obj)
    {
        EventBus<Event_OnGetBoardSession>.Publish(new Event_OnGetBoardSession(OnGetSession));

        return;

        void OnGetSession(BoardSession session)
        {
            var totalBetAmount = _boardRound.TotalBetAmount + _boardRound.CurrentChipToPlace.Amount;

            if (session.ChipAmount < totalBetAmount)
            {
                EventBus<Event_ShowInfoMessage>.Publish(
                    new Event_ShowInfoMessage("Not enough balance to bet!"));
                return;
            }

            if (session.MaxAllowedBetAmountTotal < totalBetAmount)
            {
                EventBus<Event_ShowInfoMessage>.Publish(
                    new Event_ShowInfoMessage(
                        $"Max allowed total bet amount is {StringHelper.FormatChip(session.MaxAllowedBetAmountTotal)}!"));
                return;
            }

            var maxAllowedBetOnPlacement = session.MaxAllowedBetAmount * obj.Placement.PlacementData.bindings.Length;
            var placedBetsOnPlacement = _boardRound.PlacedBets
                .Where(x => x.Placement == obj.Placement)
                .Sum(y => y.PlacedChip.Amount);
            
            if (maxAllowedBetOnPlacement < placedBetsOnPlacement + _boardRound.CurrentChipToPlace.Amount)
            {
                EventBus<Event_ShowInfoMessage>.Publish(
                    new Event_ShowInfoMessage(
                        $"Max allowed bet amount is {StringHelper.FormatChip(maxAllowedBetOnPlacement)}!"));
                return;
            }

            var newBet = new PlacedBet(obj.Placement, _boardRound.CurrentChipToPlace);

            _boardRound.PlacedBets.Add(newBet);

            _boardRound.TotalBetAmount = totalBetAmount;

            EventBus<Event_OnPlacedBet>.Publish(new Event_OnPlacedBet(newBet,
                placedBetsOnPlacement + _boardRound.CurrentChipToPlace.Amount));

            EventBus<Event_OnBoardRoundUpdated>.Publish(new Event_OnBoardRoundUpdated(_boardRound));
        }
    }

    private void OnChipSelected(Event_OnChipSelected obj)
    {
        _boardRound.CurrentChipToPlace = obj.PlacedChip;

        EventBus<Event_OnBoardRoundUpdated>.Publish(new Event_OnBoardRoundUpdated(_boardRound));
    }

    private void OnSpinButtonClicked(Event_OnSpinButtonClicked obj)
    {
        if (_boardRound.PlacedBets.Count <= 0) return;

        var numberSo = _boardRound.NextWinningNumber != null
            ? _boardRound.NextWinningNumber
            : numbers.ElementAt(Random.Range(0, 37));

        _boardRound.WinningNumber = numberSo;

        EventBus<Event_OnSpinStarted>.Publish(new Event_OnSpinStarted(numberSo));
    }

    private void OnUndoBetButtonClicked(Event_OnUndoBetButtonClicked obj)
    {
        if (_boardRound.PlacedBets.Count <= 0) return;

        var lastBet = _boardRound.PlacedBets.Last();

        _boardRound.TotalBetAmount -= lastBet.PlacedChip.Amount;

        _boardRound.PlacedBets.Remove(lastBet);

        var placedBetsOnPlacement = _boardRound.PlacedBets
            .Where(x => x.Placement == lastBet.Placement)
            .Sum(y => y.PlacedChip.Amount);

        EventBus<Event_OnUndoBet>.Publish(new Event_OnUndoBet(lastBet, placedBetsOnPlacement));
        EventBus<Event_OnBoardRoundUpdated>.Publish(new Event_OnBoardRoundUpdated(_boardRound));
    }

    private void OnClearBetButtonClicked(Event_OnClearBetButtonClicked obj)
    {
        var placementsToClear = _boardRound.PlacedBets.GroupBy(x => x.Placement).Select(x => x.Key).ToArray();

        _boardRound.TotalBetAmount = 0;
        _boardRound.PlacedBets.Clear();

        EventBus<Event_OnClearedBets>.Publish(new Event_OnClearedBets(placementsToClear));
        EventBus<Event_OnBoardRoundUpdated>.Publish(new Event_OnBoardRoundUpdated(_boardRound));
    }

    private void OnRepeatBetButtonClicked(Event_OnRepeatBetButtonClicked obj)
    {
        EventBus<Event_OnGetBoardSession>.Publish(new Event_OnGetBoardSession(OnGetSession));

        return;

        void OnGetSession(BoardSession session)
        {
            if (!_boardRound.HasPreviousBet) return;

            var betsToPlace = _boardRound.PreviousPlacedBets.GroupBy(x => x.Placement)
                .Select(y =>
                    new PlacedBet(y.Key, new PlacedChip(y.Last().PlacedChip.Chip, y.Sum(z => z.PlacedChip.Amount))))
                .ToList();

            var betsToPlaceAmount = betsToPlace.Sum(x => x.PlacedChip.Amount);

            if (betsToPlaceAmount > session.ChipAmount)
            {
                EventBus<Event_ShowInfoMessage>.Publish(
                    new Event_ShowInfoMessage("Not enough balance to repeat previous bet!"));
                return;
            }

            if (_boardRound.PlacedBets.Count > 0)
            {
                var placementsToClear = _boardRound.PlacedBets.GroupBy(x => x.Placement).Select(x => x.Key).ToArray();

                _boardRound.TotalBetAmount = 0;
                _boardRound.PlacedBets.Clear();

                EventBus<Event_OnClearedBets>.Publish(new Event_OnClearedBets(placementsToClear));
            }

            _boardRound.PlacedBets = betsToPlace;
            _boardRound.TotalBetAmount = betsToPlaceAmount;

            EventBus<Event_OnRepeatedBet>.Publish(new Event_OnRepeatedBet(_boardRound.PlacedBets.ToArray()));
            EventBus<Event_OnBoardRoundUpdated>.Publish(new Event_OnBoardRoundUpdated(_boardRound));
        }
    }

    private void OnSpinEnded(Event_OnSpinEnded obj)
    {
        StartCoroutine(DelayResetForSecond(2));

        return;

        IEnumerator DelayResetForSecond(int delay)
        {
            yield return new WaitForSeconds(delay);

            EventBus<Event_OnReset>.Publish(new Event_OnReset());

            if (_boardRound.PlacedBets.Count <= 0) yield return null;

            _boardRound.PreviousPlacedBets = _boardRound.PlacedBets.ToList();
            var placementsToClear = _boardRound.PlacedBets.GroupBy(x => x.Placement).Select(x => x.Key).ToArray();

            _boardRound.TotalBetAmount = 0;
            _boardRound.PlacedBets.Clear();
            _boardRound.NextWinningNumber = null;

            EventBus<Event_OnClearedBets>.Publish(new Event_OnClearedBets(placementsToClear));
            EventBus<Event_OnBoardRoundUpdated>.Publish(new Event_OnBoardRoundUpdated(_boardRound));
        }
    }

    private void OnNextWinningSelected(Event_OnNextWinningSelected obj)
    {
        _boardRound.NextWinningNumber = obj.NextWinningNumber;
    }

    private void OnEnable()
    {
        EventBus<Event_OnGetBoardRound>.Subscribe(OnGetBoardRound);
        EventBus<Event_OnPlacementClicked>.Subscribe(OnPlacementClicked);
        EventBus<Event_OnChipSelected>.Subscribe(OnChipSelected);
        EventBus<Event_OnSpinButtonClicked>.Subscribe(OnSpinButtonClicked);
        EventBus<Event_OnUndoBetButtonClicked>.Subscribe(OnUndoBetButtonClicked);
        EventBus<Event_OnClearBetButtonClicked>.Subscribe(OnClearBetButtonClicked);
        EventBus<Event_OnRepeatBetButtonClicked>.Subscribe(OnRepeatBetButtonClicked);
        EventBus<Event_OnSpinEnded>.Subscribe(OnSpinEnded);
        EventBus<Event_OnNextWinningSelected>.Subscribe(OnNextWinningSelected);
    }

    private void OnDisable()
    {
        EventBus<Event_OnGetBoardRound>.Unsubscribe(OnGetBoardRound);
        EventBus<Event_OnPlacementClicked>.Unsubscribe(OnPlacementClicked);
        EventBus<Event_OnChipSelected>.Unsubscribe(OnChipSelected);
        EventBus<Event_OnSpinButtonClicked>.Unsubscribe(OnSpinButtonClicked);
        EventBus<Event_OnUndoBetButtonClicked>.Unsubscribe(OnUndoBetButtonClicked);
        EventBus<Event_OnClearBetButtonClicked>.Unsubscribe(OnClearBetButtonClicked);
        EventBus<Event_OnRepeatBetButtonClicked>.Unsubscribe(OnRepeatBetButtonClicked);
        EventBus<Event_OnSpinEnded>.Unsubscribe(OnSpinEnded);
        EventBus<Event_OnNextWinningSelected>.Unsubscribe(OnNextWinningSelected);
    }

    private void OnValidate()
    {
        NumberHelper.TryLoadNumberAssets(out var assets);

        numbers = assets.ToArray();
    }
}