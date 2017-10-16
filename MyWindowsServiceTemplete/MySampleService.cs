using MyWindowsServiceTemplete.Configuration;
using MyWindowsServiceTemplete.Logging;
using MyWindowsServiceTemplete.Presenter;
using MyWindowsServiceTemplete.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyWindowsServiceTemplete
{
    public partial class MySampleService : ServiceBase
    {
        private MainPresenter presenter = null;
        private static Timer aTimer;
        private static bool isRunning = false;

        public MySampleService()
        {
            InitializeComponent();

            // Bind presenter, it'll be used as main control flow.
            presenter = new MainPresenter(this);
            presenter.InitEventLog();
        }

        protected override void OnStart(string[] args)
        {
            bool success = presenter.TestReadConfig();
            if (!success)
            {
                Log.Instance.Write("Error reading app.config.", EventLogEntryType.Error);
                return;
            }

            // Read App.Config and set Timer properly.
            float interval = 60;
            if (AppConfig.UseRunEveryMinute)
            {
                interval = AppConfig.RunEveryMinute * 60;
            }
            Log.Instance.Write("Set timer interval to " + interval);

            // Create a timer with the specified second interval.
            aTimer = new System.Timers.Timer(1000 * interval);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            DateTime dtNow = DateTime.Now;

            if (AppConfig.UseRunEveryMinute)
            {
                DoWork();
            }
            else if (AppConfig.UseRunEverydayAtTime)
            {
                DateTime scheduledTime = AppConfig.RunEverydayAtTime;
                if (DateTimeUtil.GetHourIn24(dtNow) == DateTimeUtil.GetHourIn24(scheduledTime)
                    && dtNow.Minute == scheduledTime.Minute)
                {
                    DoWork();
                }
            }

        }

        private void DoWork()
        {
            Log.Instance.Write("DoWork started.");

            bool success = presenter.Init();

            if (!success)
            {
                Log.Instance.Write("Initialize fail. No task can continue.");
            }
            else
            {
                Log.Instance.Write("Initialize success.");
                if (!isRunning)
                {
                    isRunning = true;
                    presenter.RunMainWork();
                    isRunning = false;
                }
                else
                {
                    Log.Instance.Write("Old work still running. So skip work this time.");
                }
            }
        }

        protected override void OnPause()
        {
        }

        protected override void OnContinue()
        {
        }

        protected override void OnStop()
        {
        }
    }
}
