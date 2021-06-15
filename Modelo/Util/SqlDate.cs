using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    [Serializable]
    public class SqlDate
    {
        public static DateTime MinValue = new DateTime(1753, 1, 1, 12, 0, 0);
        public static DateTime MaxValue = new DateTime(2753, 1, 1, 12, 0, 0);
    }
}
