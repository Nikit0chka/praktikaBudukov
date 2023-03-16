using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Praktik1;

public partial class EmployeeRegistrationWindow : Window
{
    private string _directoryPath = @"C:\Users\voron\OneDrive\Рабочий стол\BudukovPraktika\FinalDirectory";
    private string _filePath = @"C:\Users\voron\OneDrive\Рабочий стол\BudukovPraktika\FinalDirectory\employee.txt";
    private bool _isAllInputCorrect = false;
    private ValidateEmployeeRegistrationTextBoxes validateTextBoxes;

    public EmployeeRegistrationWindow()
    {
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        InitializeComponent();

        validateTextBoxes = new ValidateEmployeeRegistrationTextBoxes();
        DataContext = validateTextBoxes;
    }

    private void registerUser_Click(object sender, RoutedEventArgs e)
    {
        if (_isAllInputCorrect)
            FillFile();

    }

    private void backButton_Click(object sender, RoutedEventArgs e) => OpenAuthorizationWindow();

    private void OpenAuthorizationWindow()
    {
        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
        authorizationWindow.Show();
        Close();
    }

    private void FillFile()
    {
        try
        {
            Directory.CreateDirectory(_directoryPath);

            if (IsIndexAlreadyRegistred(idTextBox.Text))
            {
                File.AppendAllText(_filePath, "ID: " + idTextBox.Text + " ");
                File.AppendAllText(_filePath, "Surname: " + surnameTextBox.Text + " ");
                File.AppendAllText(_filePath, "Name: " + nameTextBox.Text + " ");
                File.AppendAllText(_filePath, "Patronymic: " + patronymicTextBox.Text + " ");
                File.AppendAllText(_filePath, "Passport: " + passportTextBox.Text + " ");
                File.AppendAllText(_filePath, "Email: " + emailTextBox.Text + " \n");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private bool IsIndexAlreadyRegistred(string index)
    {
        string[] employeFileContent = new string[] { };

        try
        {
            if (!File.Exists(_filePath))
                return true;

            employeFileContent = File.ReadAllLines(_filePath);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        foreach (string line in employeFileContent)
            if (line.Split()[1] == index)
            {
                MessageBox.Show("Employee with this ID is already registred!");
                idTextBox.BorderBrush = Brushes.Purple;
                return false;
            }
        idTextBox.BorderBrush = Brushes.Green;
        return true;
    }

    private void InputError(object sender, ValidationErrorEventArgs e)
    {
        _isAllInputCorrect = false;
    }

    private void AnyTextChange(object sender, TextChangedEventArgs e)
    {
        _isAllInputCorrect = true;
    }
}