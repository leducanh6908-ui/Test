using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_UNIT
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;

        public int Subtract(int a, int b) => a - b;

        public int Divide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException("Không thể chia cho 0");
            return a / b;
        }
    }
}


