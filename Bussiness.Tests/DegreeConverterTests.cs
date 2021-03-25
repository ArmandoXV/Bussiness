using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Tests
{
    [TestFixture]
    class DegreeConverterTests
    {
        [Test]
        public void ToFahrenheit_zero_Return32()
        {
            //Arrange
            //Act
            double result =
           DegreeConverter.ToFahrenheit(0);
            //Assert
            Assert.That(result, Is.EqualTo(32));
        }

        [Test]
        [TestCase(3, "Fizz")]
        public void FizzBuzz_ReturnCorrectValue(int n, string s)
        {
            //Arrange

            //Act
            string result = DegreeConverter.FizzBuzz(n);
            //Assert
            Assert.That(result, Is.EqualTo(s));

        }


        [Test]
        [TestCase(5)]
        public void Push_CheckTopOfTheStack(int n)
        {
            //Arrange
            MyStack<int> stack = new MyStack<int>();


            //Act
            stack.Push(n);

            //Assert
            Assert.That(stack.Peek, Is.EqualTo(n));
        }

        [Test]
        [TestCase(5)]
        public void Pop_CheckTheReturnValue(int count)
        {
            //Arrange
            MyStack<int> stack = new MyStack<int>();
            for (int i = 0; i < count; i++)
            {
                stack.Push(i);
            }

            //Act            
            stack.Pop();

            //Assert
            if (count == 0)
                Assert.Throws<InvalidOperationException>(() => stack.Pop());
            Assert.That(stack.Count, Is.EqualTo(count - 1));
        }

        [Test]
        [TestCase(6)]
        public void Peek_Chucker(int count)
        {
            //Arrange
            MyStack<int> stack = new MyStack<int>();

            for (int i = 0; i < count; i++)
            {
                stack.Push(i);
            }

            //Assert
            Assert.That(stack.Peek, Is.EqualTo(count - 1));
        }
    }    
}
