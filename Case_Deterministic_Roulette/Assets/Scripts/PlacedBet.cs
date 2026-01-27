public class PlacedBet
{
    public readonly PlacementGO Placement;
    public readonly PlacedChip PlacedChip;

    public PlacedBet(PlacementGO placement, PlacedChip placedChip)
    {
        Placement = placement;
        PlacedChip = placedChip;
    }
}