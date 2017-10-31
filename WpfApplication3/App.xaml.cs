using System.Windows;
using log4net;
using System;
using System.Windows.Threading;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GlobalContext.Properties["username"] = Environment.UserName;

            var log = LogManager.GetLogger(GetType());

            Current.DispatcherUnhandledException +=
                (s, ex) => log.Fatal("Dispatcher Unhandled Exception: {0}", ex.Exception);
            AppDomain.CurrentDomain.UnhandledException +=
                (s, ex) => log.Fatal($"AppDomain.CurrentDomain Exception: {ex.ExceptionObject}");

            void DomainUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
            {
                var exception = unhandledExceptionEventArgs.ExceptionObject as Exception;
                MessageBox.Show(exception.ToString());
            }
            void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs ex)
            {
                MessageBox.Show(ex.Exception.ToString());
                ex.Handled = true;
            }
        }
    }
}
