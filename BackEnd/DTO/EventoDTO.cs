namespace BackEnd.DTO
{
    public class EventoDTO
    {
        public int IdEvent { get; set; }
        public string EventName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Location { get; set; }
        public int IdClient { get; set; }
        public string ClientName { get; set; }
    }
}
