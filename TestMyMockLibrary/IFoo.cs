using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMyMockLibrary
{
    public interface IFoo
    {
        string Name { get; set; }
        int Value { get; set; }

        void GetValueByRef(ref int value);
        void GetValueByOut(out int value);
    }
}
