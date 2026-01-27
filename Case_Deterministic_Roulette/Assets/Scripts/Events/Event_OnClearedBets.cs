namespace Events
{
    public class Event_OnClearedBets
    {
        public readonly PlacementGO[]  Placements;

        public Event_OnClearedBets(PlacementGO[] placements)
        {
            Placements = placements;
        }
    }
}