namespace Events
{
    public class Event_OnPlacedBet
    {
        public readonly PlacedBet PlacedBet;
        public readonly int TotalAmountOnPlacement;

        public Event_OnPlacedBet(PlacedBet placedBet, int totalAmountOnPlacement)
        {
            PlacedBet = placedBet;
            TotalAmountOnPlacement = totalAmountOnPlacement;
        }
    }
}