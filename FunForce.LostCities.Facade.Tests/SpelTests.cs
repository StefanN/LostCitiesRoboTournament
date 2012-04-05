using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;


namespace FunForce.LostCities.Facade.Tests
{
    [TestFixture]
    public class SpelTests
    {
        SpelStatus spelStatus ;

        [SetUp]
        public void SetUp()
        {
            spelStatus = new SpelStatus();
        }

        [Test]
        public void CreateFacade()
        {
            Assert.IsNotNull(spelStatus);
        }

        [Test]
        public void DoActie()
        {
            SpelBord tmp = spelStatus.DoActie();
            Assert.IsNotNull(tmp);
        }

        [Test]
        public void GetStatus()
        {
            SpelBord tmp = spelStatus.GetStatus();
            Assert.IsNotNull(tmp);
        }
    }
}
