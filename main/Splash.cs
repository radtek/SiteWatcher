using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SiteWatcher
{
    public class Splash
    {
        static frmSplash MySplashForm = null;
        static Thread MySplashThread = null;

        static void ShowThread()
        {
            MySplashForm = new frmSplash();
            Application.Run(MySplashForm);
        }

        static public void Show()
        {
            if (MySplashThread != null)
                return;

            MySplashThread = new Thread(new ThreadStart(Splash.ShowThread));
            MySplashThread.IsBackground = true;
            MySplashThread.ApartmentState = ApartmentState.STA;
            MySplashThread.Start();
        }

        static public void Close()
        {
            if (MySplashThread == null) return;
            if (MySplashForm == null) return;

            try
            {
                MySplashForm.Invoke(new MethodInvoker(MySplashForm.Close));
            }
            catch (Exception)
            {
            }
            MySplashThread = null;
            MySplashForm = null;
        }

        static public string Status
        {
            set
            {
                if (MySplashForm == null)
                {
                    return;
                }

                MySplashForm.StatusInfo = value;
            }
            get
            {
                if (MySplashForm == null)
                {
                    throw new InvalidOperationException("Splash Form not on screen");
                }
                return MySplashForm.StatusInfo;
            }
        }
    }
}
