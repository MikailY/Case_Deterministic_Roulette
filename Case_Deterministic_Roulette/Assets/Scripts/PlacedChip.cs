using Data;

public class PlacedChip
{
    public readonly ChipSO Chip;
    public readonly int Amount;

    public PlacedChip(ChipSO chip, int amount)
    {
        Chip = chip;
        Amount = amount;
    }
}