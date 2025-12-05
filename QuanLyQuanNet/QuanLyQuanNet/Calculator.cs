using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanNet
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b; // trả về integer
        }

        public bool IsEven(int number)
        {
            return number % 2 == 0; // trả về boolean
        }
    }
}
