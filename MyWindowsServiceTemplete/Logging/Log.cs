using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsServiceTemplete.Logging
{
    public class Log
    {
        private static Log instance;
        private static ServiceBase service;

        private Log() { }

        public static Log Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Log();
                }
                return instance;
            }
        }

        public void Init(ServiceBase theService)
        {
            service = theService;

            // Register custom EventLog
            //service.ServiceName = "PoiCameraServiceToTfs";
            ////service.EventLog = new System.Diagnostics.EventLog();

            //((ISupportInitialize)(service.EventLog)).BeginInit();
            //if (!EventLog.SourceExists(service.EventLog.Source))
            //{
            //    EventLog.CreateEventSource(service.EventLog.Source, service.EventLog.Log);
            //}
            //((ISupportInitialize)(service.EventLog)).EndInit();

            //service.EventLog.Source = service.ServiceName;
            //service.EventLog.Log = "Application";
        }

        public void Write(string message)
        {
            service.EventLog.WriteEntry(message);
        }

        public void Write(string message, Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            sb.AppendLine("Exception: " + ex.GetType().Name);
            sb.AppendLine("Message: " + ex.Message);
            sb.AppendLine("Stacktrace:" + ex.StackTrace);
            if (ex.InnerException == null)
            {
                sb.AppendLine("InnerExeption: null");
            }
            else
            {
                sb.AppendLine("InnerExeption: " + ex.InnerException.ToString());
            }


            service.EventLog.WriteEntry(sb.ToString(), EventLogEntryType.Error);
        }

        public void Write(string message, EventLogEntryType type)
        {
            service.EventLog.WriteEntry(message, type);
        }
    }
