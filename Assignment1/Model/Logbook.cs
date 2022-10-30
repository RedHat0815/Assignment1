namespace Assignment1.Model
{
    public class Logbook
    {

        public List<Journey> journeys { get; set; }

        public long distanceTotal
        {
            get
            {
                long sum = 0;
                foreach (var journey in journeys)
                {
                    sum += journey.Distance;
                }
                return sum;
            }
        }



    }
}
