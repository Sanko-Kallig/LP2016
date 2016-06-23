using Microsoft.VisualStudio.TestTools.UnitTesting;
using Administration_Sloepke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration_Sloepke.Tests
{
    [TestClass()]
    public class HiringContractTests
    {
        [TestMethod()]
        public void CalculateBudgetTest()
        {
            HiringContract  HC = new HiringContract();
            HC.Boats = new List<IBoat>();
            HC.Boats.Add(new MotorBoat("MotorBoat", 15, "TestMotor", 15));
            HC.StartDate = new DateTime(2016, 06, 20);
            HC.EndDate = DateTime.Now;
            Assert.IsTrue(HC.CalculateBudget(80) == 76.5);
        }
    }
}