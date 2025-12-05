using NUnit.Framework;
using Test_UNIT; // namespace của project chính

namespace TestNUNIT
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_ShouldReturnCorrectSum()
        {
            var result = _calculator.Add(2, 3);
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Subtract_ShouldReturnCorrectDifference()
        {
            var result = _calculator.Subtract(5, 3);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Divide_ByZero_ShouldThrowException()
        {
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
        }

        [TestCase(10, 2, 5)]
        [TestCase(9, 3, 3)]
        [TestCase(8, 4, 2)]
        public void Divide_ShouldReturnExpectedResult(int a, int b, int expected)
        {
            var result = _calculator.Divide(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}