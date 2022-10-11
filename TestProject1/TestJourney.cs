using Assignment1.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestProject1
{
    public class TestClassJourney 
    {
        [Fact]
        public void TestJourney()
        {
            var testJourney1 = new Journey(new DateTime(2010, 3, 11), new DateTime(2010, 3, 11), "test1", 5, "test1");
            var testJourney2 = new Journey(new DateTime(2010, 3, 11), new DateTime(2010, 3, 11), "test2", 5, "test2");
            var testJourney3 = new Journey(new DateTime(2010, 3, 11), new DateTime(2010, 3, 11), "test3", 5, "test3");

            List<Journey> listJourney = new List<Journey>();
            listJourney.Add(testJourney1);
            listJourney.Add(testJourney2);
            listJourney.Add(testJourney3);

            var logbook = new Logbook();
            logbook.journeys = listJourney;

            Assert.Equal(15, logbook.distanceTotal);
            //Assert.NotEqual(20, logbook.distanceTotal);

        }
    }
}