using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Tests
{
    [TestFixture]
    class PeriodTests
    {
        [Test]
        [TestCase(-4,6)]
        [TestCase(4, 6)]
        [TestCase(13, 6)]
        public void GetToTest(int from,int to)
        {
            //Arrange
            Period period = new Period(from, to);
            int getFrom;
            int getTo;

            //Act
            getFrom = period.From;
            getTo = period.To;

            //Assert
            Assert.That(getTo, Is.EqualTo(to));
        }

        [Test]
        [TestCase(4, 6)]
        [TestCase(56, 7)]
        public void GetFromTest(int from, int to)
        {
            //Arrange
            Period period = new Period(from, to);
            int getFrom;
            int getTo;

            //Act
            getFrom = period.From;
            getTo = period.To;

            //Assert
            Assert.That(getFrom ,Is.EqualTo(from));
        }

        [Test]
        public void MakeEmptyPeriodTest()
        {
            //Arrange
            Period period = Period.MakeEmptyPeriod();
            bool res = false;

            //Act
            res = period.Empty;

            //Assert
            Assert.That(res, Is.EqualTo(true));
        }

        [Test]
        [TestCase(4, 6,3)]
        [TestCase(0, 9, 10)]
        [TestCase(0, 0, 0)]
        [TestCase(-2, 9, 12)]
        [TestCase(2, -9, 12)]
        public void LengthTest(int from, int to,int length)
        {
            //Arrange
            Period period = new Period(from, to);
            if (from == to)
                period = Period.MakeEmptyPeriod();
                     
            int res;
            //Act
            res = period.Length;

            //Assert
            Assert.That(res, Is.EqualTo(length));
        }

        [Test]
        [TestCase(4, 6, 1,5)]
        [TestCase(0, 9, 3,3)]
        [TestCase(-5,10,2,-3)]
        [TestCase(-5, -20, 2, -7)]
        [TestCase(5, -20, 2, 3)]
        public void BracketTest(int from, int to, int i,int n)
        {
            //Arrange
            Period period = new Period(from, to);
            if (from == to)
                period = Period.MakeEmptyPeriod();
            int res;

            //Act
            res = period[i];

            //Assert
            Assert.That(res, Is.EqualTo(n));
        }
        
        [Test]
        [TestCase(0,5,6,10,0,0)]
        [TestCase(0, 5, 3, 10, 3, 5)]
        [TestCase(0, 5, -5, 1, 0, 1)]
        [TestCase(0, 20, 3, 10, 3, 10)]
        [TestCase(0, 5, -2, 10, 0, 5)]
        [TestCase(0, 5, -10, 10, 0, 5)]
        public void OverlapwithTest(int from,int to, int otherFrom, int otherTo,int resFrom,int resTo)
        {
            //Arrange
            Period firstPeriod = new Period(from, to);
            Period secondPeriod = new Period(otherFrom, otherTo);
            Period resPeriod = new Period(resFrom, resTo);
            if (resFrom == resTo)
                resPeriod = Period.MakeEmptyPeriod();
            Period res;

            //Act
            res = firstPeriod.OverlapWith(secondPeriod);

            //Assert
            Assert.That(res, Is.EqualTo(resPeriod));
        }

    }
}
