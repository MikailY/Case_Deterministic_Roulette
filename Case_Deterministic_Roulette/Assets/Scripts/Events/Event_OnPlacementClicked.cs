namespace Events
{
    public class Event_OnPlacementClicked
    {
        public readonly PlacementGO Placement;

        public Event_OnPlacementClicked(PlacementGO placement)
        {
            Placement = placement;
        }
    }
}