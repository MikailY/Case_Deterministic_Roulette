using System;

namespace Events
{
    public class Event_OnGetChipText
    {
        public readonly Action<ChipText> GetChipTextAction;

        public Event_OnGetChipText(Action<ChipText> getChipTextAction)
        {
            GetChipTextAction = getChipTextAction;
        }
    }
}