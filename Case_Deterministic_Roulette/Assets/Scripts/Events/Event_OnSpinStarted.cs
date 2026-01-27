using Data;

namespace Events
{
    public class Event_OnSpinStarted
    {
        public readonly NumberSO Result;

        public Event_OnSpinStarted(NumberSO result)
        {
            Result = result;
        }
    }
}