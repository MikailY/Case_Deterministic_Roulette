namespace Events
{
    public class Event_OnReturnChipText
    {
        public readonly ChipText ObjectToReturn;

        public Event_OnReturnChipText(ChipText objectToReturn)
        {
            ObjectToReturn = objectToReturn;
        }
    }
}