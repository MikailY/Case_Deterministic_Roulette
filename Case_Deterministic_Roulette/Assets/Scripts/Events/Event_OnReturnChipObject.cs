namespace Events
{
    public class Event_OnReturnChipObject
    {
        public readonly ChipObject ReturnedObject;

        public Event_OnReturnChipObject(ChipObject returnedObject)
        {
            ReturnedObject = returnedObject;
        }
    }
}