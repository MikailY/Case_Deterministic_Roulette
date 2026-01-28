namespace Events
{
    public struct Event_ShowInfoMessage
    {
        public readonly string Message;

        public Event_ShowInfoMessage(string message)
        {
            Message = message;
        }
    }
}