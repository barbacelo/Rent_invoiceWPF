using System.Windows;
using log4net;
using System;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Input;

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

            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.KeyDownEvent, new KeyEventHandler(TextBox_KeyDown));

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

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter & (sender as TextBox).AcceptsReturn == false) MoveToNextUIElement(e);
        }

        void MoveToNextUIElement(KeyEventArgs e)
        {
            // Creating a FocusNavigationDirection object and setting it to a
            // local field that contains the direction selected.
            FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;

            // MoveFocus takes a TraveralReqest as its argument.
            TraversalRequest request = new TraversalRequest(focusDirection);

            // Gets the element with keyboard focus.
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {
                if (elementWithFocus.MoveFocus(request)) e.Handled = true;
            }
        }

    }
}
