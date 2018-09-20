using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chappyware.Data.Factories;

namespace Chappyware.Data.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void GetGoals()
        {

            PlayerFactory factory = PlayerFactory.Instance;

            Player p = factory.GetPlayer("Sidney Crosby", "PIT");

            int goals = p.GetGoals(DateTime.Today.AddDays(-30), DateTime.Today);

        }
    }
}
