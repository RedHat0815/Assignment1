namespace Assignment1.Model
{
    public class Journey
    {
        public Journey()
        {

        }

        public Journey(DateTime start, DateTime end, string driver, long distance, string description)
        {
            Start = start;
            End = end;
            Driver = driver;
            Distance = distance;
            Description = description;
        }

        public long Id { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Driver { get; set; }
        public long Distance { get; set; }
        public string Description { get; set; }

        //public long SumLogbook { get; set; }


    }
}
