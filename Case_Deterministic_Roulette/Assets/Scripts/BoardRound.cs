using System.Collections.Generic;
using System.Linq;
using Data;

public class BoardRound
{
    public List<PlacedBet> PlacedBets { get; set; } = new();
    public List<PlacedBet> PreviousPlacedBets { get; set; }
    public PlacedChip CurrentChipToPlace { get; set; }
    public int TotalBetAmount { get; set; }
    public NumberSO WinningNumber { get; set; }
    public bool HasPreviousBet => PreviousPlacedBets != null && PreviousPlacedBets.Any();
}