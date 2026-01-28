using System.Collections.Generic;
using Data;

public class BoardSession
{
    public int ChipAmount { get; set; }
    public int MinAllowedBetAmount { get; set; }
    public int MaxAllowedBetAmount { get; set; }
    public int MaxAllowedBetAmountTotal { get; set; }
    public List<NumberSO> PreviousResults { get; set; } = new();
    public int[] ChipValues { get; set; }
    public SessionStatistics Statistics { get; set; } = new();
}

public class SessionStatistics
{
    public int TotalSpins { get; set; }
    public int TotalBetAmount { get; set; }
    public int TotalWin { get; set; }
    public int HighestWin { get; set; }
    public int OverallResult => TotalWin - TotalBetAmount;
}