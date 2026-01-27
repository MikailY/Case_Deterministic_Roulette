namespace Events
{
    public class Event_OnSessionUpdated
    {
        public readonly BoardSession Session;

        public Event_OnSessionUpdated(BoardSession session)
        {
            Session = session;
        }
    }
}