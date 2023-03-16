using System;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;

namespace Praktik1;

public partial class AuthorizationWindow : Window
{
    private const int MAX_AUTHORIZATION_ATTEMPTS = 3;

    private CancellationTokenSource _isAuthorizationWindowOpened = new CancellationTokenSource();
    private Stopwatch _timeFromLastInput = Stopwatch.StartNew();
    private TimeSpan _maxNonUseTime = new TimeSpan(0, 3, 0);
    
    private int _authorizationAttempts = 0;

    public AuthorizationWindow()
    {
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        InitializeComponent();
        NotifyAppNotUsingAsync(_isAuthorizationWindowOpened.Token);
    }

    private void Button_Click(object sender, RoutedEventArgs e) => CheckAllInput();

    private void OpenEmployeeRegistrationWindow()
    {
        _isAuthorizationWindowOpened.Cancel();
        EmployeeRegistrationWindow mainWindow = new EmployeeRegistrationWindow();
        mainWindow.Show();
        Close();
    }

    private void RestartStopWatchOnAnyInput(object sender, KeyEventArgs e) => _timeFromLastInput.Restart();

    private void CheckAllInput()
    {
        _authorizationAttempts++;

        if (passwordPasBox.Password == "employee" && loginTextBox.Text == "employee")
            OpenEmployeeRegistrationWindow();

        if (_authorizationAttempts == MAX_AUTHORIZATION_ATTEMPTS)
        {
            MessageBox.Show("Autorization is not enable for 1 minute!");
            DisableInputForOneMinute();
            _authorizationAttempts = 0;
        }
        else
        {
            passwordPasBox.BorderBrush = Brushes.Red;
            loginTextBox.BorderBrush = Brushes.Red;
        }
    }
    
    private async void DisableInputForOneMinute()
    {
        authorizationButton.IsEnabled = false;
        loginTextBox.BorderBrush = Brushes.Gray;
        passwordPasBox.BorderBrush = Brushes.Gray;

        await Task.Delay(60 * 1000);

        authorizationButton.IsEnabled = true;
    }

    private async void NotifyAppNotUsingAsync(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
                if (_timeFromLastInput.Elapsed > _maxNonUseTime)
                {
                    if (MessageBox.Show("Close?", "App is not using!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        Close();
                    _timeFromLastInput.Restart();
                }
        });
    }
}