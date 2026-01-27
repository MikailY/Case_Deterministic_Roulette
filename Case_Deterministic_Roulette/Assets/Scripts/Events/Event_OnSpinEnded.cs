using Data;

namespace Events
{
    public class Event_OnSpinEnded
    {
        public readonly NumberSO Result;

        public Event_OnSpinEnded(NumberSO result)
        {
            Result = result;
        }
    }
}