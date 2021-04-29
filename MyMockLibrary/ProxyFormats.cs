using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMockLibrary
{
    public static class ProxyFormats
    {
        public static string ProxyClassFormat
        {
            get
            {
                return AssemblyResources.GetResource("MyMockLibrary.ProxyClass.fmt");
            }
        }
        public static string ProxyMethodFormat
        {
            get
            {
                return AssemblyResources.GetResource("MyMockLibrary.ProxyMethod.fmt");
            }
        }
        public static string ProxyPropertyFormat
        {
            get
            {
                return AssemblyResources.GetResource("MyMockLibrary.ProxyProperty.fmt");
            }
        }
    }
}
