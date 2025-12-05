using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuanLyQuanNet;

namespace QuanLyQuanNet.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator calc = new Calculator();

        // Phương thức hỗ trợ kiểm tra kết quả
        private void CheckResult(object expected, object actual)
        {
            if (Equals(expected, actual))
                Console.WriteLine("✅ PASS");
            else
                Console.WriteLine($"❌ FAILED | Expected: {expected}, Actual: {actual}");
        }

        [TestMethod]
        public void Test_Add()
        {
            int result = calc.Add(5, 3);
            CheckResult(8, result);
        }

        [TestMethod]
        public void Test_IsEven()
        {
            bool result = calc.IsEven(10);
            CheckResult(true, result);
        }

        [TestMethod]
        public void Test_Add_FailCase()
        {
            int result = calc.Add(2, 2);
            CheckResult(5, result);
        }
    }
}
