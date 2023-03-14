using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Praktik1;

public partial class AuthorizationWindow : Window
{
    private const int MAX_AUTHORIZATION_ATTEMPTS = 3;

    private CancellationTokenSource _isAuthorizationWindowOpened = new CancellationTokenSource();
    private Stopwatch timeFromLastInput = Stopwatch.StartNew();
    private TimeSpan _maxNonUseTime = new TimeSpan(0, 3, 0);
    private int _authorizationAttempts = 0;

    public AuthorizationWindow()
    {
        InitializeComponent();
        IsAppUsing(_isAuthorizationWindowOpened.Token);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        CheckAllInput();
    }

    private void OpenEmployeeRegistrationWindow()
    {
        _isAuthorizationWindowOpened.Cancel();
        EmployeeRegistrationWindow mainWindow = new EmployeeRegistrationWindow();
        mainWindow.Show();
        Close();
    }

    private async void IsAppUsing(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
                if (timeFromLastInput.Elapsed > _maxNonUseTime)
                    if (MessageBox.Show("Close?", "App is not using!", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        Close();
                    else
                        timeFromLastInput.Restart();
        });
    }

    private void RestartTimerOnAnyInput(object sender, KeyEventArgs e)
    {
        timeFromLastInput.Restart();
    }

    private async void DisableInputForOneMinute()
    {
        authorizationButton.IsEnabled = false;

        await Task.Delay(60 * 1000);

        passwordPasBox.BorderBrush = Brushes.Gray;
        loginTextBox.BorderBrush = Brushes.Gray;
        authorizationButton.IsEnabled = true;
    }

    private void CheckAllInput()
    {
        _authorizationAttempts++;

        if (passwordPasBox.Password == "employee" && loginTextBox.Text == "employee")
            OpenEmployeeRegistrationWindow();

        if (_authorizationAttempts == MAX_AUTHORIZATION_ATTEMPTS)
        {
            MessageBox.Show("Autorization is not enable for 3 minute!");
            DisableInputForOneMinute();
            _authorizationAttempts = 0;
        }
        passwordPasBox.BorderBrush = Brushes.Red;
        loginTextBox.BorderBrush = Brushes.Red;
    }
}