using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsServiceTemplete.Utility
{
    public class DateTimeUtil
    {
        public static int GetHourIn24(DateTime dt)
        {
            string hrStr = dt.ToString("HH");
            int hr = 0;
            if (int.TryParse(hrStr, out hr))
            {
                return hr;
            }
            else
            {
                return hr;
            }
        }
    }
}
