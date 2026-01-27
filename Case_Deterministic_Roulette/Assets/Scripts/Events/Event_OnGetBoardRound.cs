using System;

namespace Events
{
    public class Event_OnGetBoardRound
    {
        public readonly Action<BoardRound> OnGetAction;

        public Event_OnGetBoardRound(Action<BoardRound> onGetAction)
        {
            OnGetAction = onGetAction;
        }
    }
}