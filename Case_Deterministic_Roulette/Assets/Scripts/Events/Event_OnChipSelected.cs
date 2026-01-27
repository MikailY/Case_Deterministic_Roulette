using Data;

namespace Events
{
    public class Event_OnChipSelected
    {
        public readonly PlacedChip PlacedChip;

        public Event_OnChipSelected(PlacedChip placedChip)
        {
            PlacedChip = placedChip;
        }
    }
}