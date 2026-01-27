namespace Events
{
    public class Event_OnUndoBet
    {
        public readonly PlacedBet PlacedBet;
        public readonly int TotalAmountOnPlacement;

        public Event_OnUndoBet(PlacedBet placedBet, int totalAmountOnPlacement)
        {
            PlacedBet = placedBet;
            TotalAmountOnPlacement = totalAmountOnPlacement;
        }
    }
}