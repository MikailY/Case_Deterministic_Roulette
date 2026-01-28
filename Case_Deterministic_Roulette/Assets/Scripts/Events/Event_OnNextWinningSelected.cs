using Data;

namespace Events
{
    public class Event_OnNextWinningSelected
    {
        public readonly NumberSO NextWinningNumber;

        public Event_OnNextWinningSelected(NumberSO nextWinningNumber)
        {
            NextWinningNumber = nextWinningNumber;
        }
    }
}