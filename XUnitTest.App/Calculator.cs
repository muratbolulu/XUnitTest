using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTest.App
{
    public class Calculator
    {
        private ICalculatorService _calculatorService { get; set; }

        public Calculator(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public int Add(int a, int b)
        {
            return _calculatorService.Add(a, b);
        }

        public int AddTwo(int a, int b)
        {
            return _calculatorService.AddTwo(a, b);
        }


        public int Multip(int a, int b)
        {
            if (a == 0)
            {
                throw new Exception("a==0 olamaz.");
            }

            return a * b;
        }
    }
}
