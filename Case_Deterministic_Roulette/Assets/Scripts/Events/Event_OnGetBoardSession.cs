using System;

namespace Events
{
    public class Event_OnGetBoardSession
    {
        public readonly Action<BoardSession> OnGetAction;

        public Event_OnGetBoardSession(Action<BoardSession> onGetAction)
        {
            OnGetAction = onGetAction;
        }
    }
}