using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsTeamManager.Models;
using SportsTeamManager.Controllers;

namespace UnitTestSportsManager
{
    [TestClass]
    public class UnitTestModels
    {
        [TestMethod]
        public void TestUpdateTime()
        {
            Match m = new Match { Date = "02 Jan 2015", Time = "14:00" };
            m.UpdateTime();
            DateTime expected = new DateTime(2015, 1, 2, 14, 0, 0);

            Assert.AreEqual(expected, m.TimeAndDate);
        }


    }
}
