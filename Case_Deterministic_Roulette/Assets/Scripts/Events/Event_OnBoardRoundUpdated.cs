namespace Events
{
    public class Event_OnBoardRoundUpdated
    {
        public readonly BoardRound Data;

        public Event_OnBoardRoundUpdated(BoardRound data)
        {
            Data = data;
        }
    }
}