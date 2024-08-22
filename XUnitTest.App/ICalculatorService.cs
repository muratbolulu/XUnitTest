using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTest.App
{
    public interface ICalculatorService
    {
        int Add(int a, int b);

        int AddTwo(int a, int b);
    }
}
