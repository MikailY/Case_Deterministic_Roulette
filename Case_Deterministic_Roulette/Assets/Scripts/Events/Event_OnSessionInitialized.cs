namespace Events
{
    public class Event_OnSessionInitialized
    {
        public readonly BoardSession Session;

        public Event_OnSessionInitialized(BoardSession session)
        {
            Session = session;
        }
    }
}