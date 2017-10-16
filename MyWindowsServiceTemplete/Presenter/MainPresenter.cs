using MyWindowsServiceTemplete.Configuration;
using MyWindowsServiceTemplete.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsServiceTemplete.Presenter
{
    public class MainPresenter
    {
        private ServiceBase service = null;

        public MainPresenter(ServiceBase service)
        {
            this.service = service;
        }

        public bool Init()
        {
            bool success = true;

            // Log will be initialize in Service class.
            //InitEventLog();

            // Test read app.config will be call in Service class.
            //success &= TestReadConfig();

            // Test other things you want to test before DoWork.
            

            return success;
        }

        public void InitEventLog()
        {
            Log.Instance.Init(service);
        }

        public bool TestReadConfig()
        {
            Type type = typeof(AppConfig); // MyClass is static class with static properties
            foreach (var p in type.GetProperties())
            {
                try
                {
                    var v = p.GetValue(null, null); // static classes cannot be instanced, so use null...
                }
                catch (Exception ex)
                {
                    Log.Instance.Write("Error reading config name = " + p.Name, ex);
                    return false;
                }
            }

            return true;
        }

        public void RunMainWork()
        {
            try
            {
                // Code your main work here.
            }
            catch (Exception ex)
            {
                Log.Instance.Write("MyWork error", ex);
                Log.Instance.Write("MyWork done with error. See previous error log for more detail.");
                return;
            }

            Log.Instance.Write("MyWork done successfully.");
        }
    }
}
