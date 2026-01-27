namespace Events
{
    public class Event_OnRepeatedBet
    {
        public readonly PlacedBet[]  PlacedBets;

        public Event_OnRepeatedBet(PlacedBet[] placedBets)
        {
            PlacedBets = placedBets;
        }
    }
}