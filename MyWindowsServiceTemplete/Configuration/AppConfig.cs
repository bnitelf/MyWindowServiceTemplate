using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsServiceTemplete.Configuration
{
    public class AppConfig
    {
        public static bool UseRunEveryMinute
        {
            get
            {
                return ReadBoolValue("UseRunEveryMinute");
            }
        }

        public static float RunEveryMinute
        {
            get
            {
                return ReadFloatValue("RunEveryMinute");
            }
        }

        public static bool UseRunEverydayAtTime
        {
            get
            {
                return ReadBoolValue("UseRunEverydayAtTime");
            }
        }

        public static DateTime RunEverydayAtTime
        {
            get
            {
                return ReadTimeValue("RunEverydayAtTime");
            }
        }
        private static void CheckKeyExist(string key)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                throw new Exception(string.Format("No key \"{0}\" found in app.config at <appSettings> section."));
            }
        }

        private static DateTime ReadTimeValue(string key)
        {
            CheckKeyExist(key);

            string msg = string.Format("Error reading app.config \"{0}\". Expect format = hh:mm:ss", key);
            string valueStr = ConfigurationManager.AppSettings[key];
            DateTime dtNow = DateTime.Now;
            DateTime datetime;
            string[] splits = valueStr.Split(':');
            if (splits.Length != 3)
            {
                throw new Exception(msg);
            }
            else
            {
                int hr, min, sec;
                if (int.TryParse(splits[0], out hr) && int.TryParse(splits[1], out min) && int.TryParse(splits[2], out sec))
                {
                    datetime = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, hr, min, sec);
                    return datetime;
                }
                else
                {
                    throw new Exception(msg);
                }
            }
        }

        private static int ReadIntValue(string key)
        {
            CheckKeyExist(key);

            string valueStr = ConfigurationManager.AppSettings[key];
            int value;
            if (int.TryParse(valueStr, out value))
            {
                return value;
            }

            throw new Exception(string.Format("Error reading app.config \"{0}\". Expect int value.", key));
        }

        private static float ReadFloatValue(string key)
        {
            CheckKeyExist(key);

            string valueStr = ConfigurationManager.AppSettings[key];
            float value;
            if (float.TryParse(valueStr, out value))
            {
                return value;
            }

            throw new Exception(string.Format("Error reading app.config \"{0}\". Expect float value.", key));
        }

        private static double ReadDoubleValue(string key)
        {
            CheckKeyExist(key);

            string valueStr = ConfigurationManager.AppSettings[key];
            double value;
            if (double.TryParse(valueStr, out value))
            {
                return value;
            }

            throw new Exception(string.Format("Error reading app.config \"{0}\". Expect double value.", key));
        }

        private static bool ReadBoolValue(string key)
        {
            CheckKeyExist(key);

            string valueStr = ConfigurationManager.AppSettings[key];
            bool value;
            if (bool.TryParse(valueStr, out value))
            {
                return value;
            }

            throw new Exception(string.Format("Error reading app.config \"{0}\". Expect bool value. Example true, false", key));
        }

        private static string ReadStringValue(string key)
        {
            CheckKeyExist(key);

            string valueStr = ConfigurationManager.AppSettings[key];
            return valueStr;
        }

        
    }
}
