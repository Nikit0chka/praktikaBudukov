using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Praktik1;

public partial class EmployeeRegistrationWindow : Window
{
    string _directoryPath = @"C:\Users\voron\OneDrive\Рабочий стол\BudukovPraktika\FinalDirectory";
    string _filePath = @"C:\Users\voron\OneDrive\Рабочий стол\BudukovPraktika\FinalDirectory\employee.txt";

    public EmployeeRegistrationWindow()
    {
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        InitializeComponent();
    }

    private void registerUser_Click(object sender, RoutedEventArgs e)
    {
        if (CheckAllINput())
            FillFile();
    }

    private void backButton_Click(object sender, RoutedEventArgs e) => OpenAuthorizationWindow();

    private void OpenAuthorizationWindow()
    {
        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
        authorizationWindow.Show();
        Close();
    }

    private bool CheckAllINput()
    {
        bool isNoException = true;
        string exceptionsContent = "";

        if (!CheckIDInput())
        {
            idTextBox.BorderBrush = Brushes.Red;
            isNoException = false;
            exceptionsContent += "Check ID input!\n";
        }
        else
            idTextBox.BorderBrush = Brushes.Green;
        if (!CheckEmailInput())
        {
            emailTextBox.BorderBrush = Brushes.Red;
            isNoException = false;
            exceptionsContent += "Check email input!\n";
        }
        else
            emailTextBox.BorderBrush = Brushes.Green;
        if (!CheckPhoneNumberInput())
        {
            phoneNumberTextBox.BorderBrush = Brushes.Red;
            isNoException = false;
            exceptionsContent += "Check phone number input!\n";
        }
        else
            phoneNumberTextBox.BorderBrush = Brushes.Green;
        if (!CheckNameSurnamePatronymicInput(nameTextBox))
        {
            nameTextBox.BorderBrush = Brushes.Red;
            isNoException = false;
            exceptionsContent += "Check name input!\n";
        }
        else
            nameTextBox.BorderBrush = Brushes.Green;
        if (!CheckNameSurnamePatronymicInput(surnameTextBox))
        {
            surnameTextBox.BorderBrush = Brushes.Red;
            isNoException = false;
            exceptionsContent += "Check surname input!\n";
        }
        else
            surnameTextBox.BorderBrush = Brushes.Green;
        if (!CheckNameSurnamePatronymicInput(patronymicTextBox))
        {
            patronymicTextBox.BorderBrush = Brushes.Red;
            isNoException = false;
            exceptionsContent += "Check patronymic input!\n";
        }
        else
            patronymicTextBox.BorderBrush = Brushes.Green;
        if (!CheckPassportInput())
        {
            passportTextBox.BorderBrush = Brushes.Red;
            isNoException = false;
            exceptionsContent += "Check passport input!\n";
        }
        else
            passportTextBox.BorderBrush = Brushes.Green;

        MessageBox.Show(exceptionsContent == "" ? "Registred sucsessfull!" : exceptionsContent);

        return isNoException;
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

    private bool CheckNameSurnamePatronymicInput(TextBox input)
    {
        if (input.Text.Length == 0 && input.Name == "patronymicTextBox")
            return true;
        if (input.Text.Length == 0)
            return false;

        return (input.Text.Any(Char.IsLetter) &&
            input.Text.Skip(1).Any(Char.IsLower) &&
            !Char.IsLower(input.Text[0])) ? true : false;
    }

    private bool CheckIDInput()
    {
        return (idTextBox.Text.Length != 0 &&
            idTextBox.Text.Any(Char.IsNumber)) ? true : false;
    }

    private bool CheckPassportInput()
    {
        return (passportTextBox.Text.Length == 10 &&
            passportTextBox.Text.Any(Char.IsNumber)) ? true : false;
    }

    private bool CheckPhoneNumberInput()
    {
        if (phoneNumberTextBox.Text.Length == 0)
            return true;
        return ((phoneNumberTextBox.Text[0] == '+' && phoneNumberTextBox.Text.Length == 12) ||
            (phoneNumberTextBox.Text[0] == '8' && phoneNumberTextBox.Text.Length == 11) &&
            phoneNumberTextBox.Text.Skip(1).Any(Char.IsNumber)) ? true : false;
    }

    private bool CheckEmailInput()
    {
        return (emailTextBox.Text.Length != 0 &&
            emailTextBox.Text.Contains('@') &&
            emailTextBox.Text.Substring
            (emailTextBox.Text.IndexOf('@')).
            Contains('.')) ? true : false;
    }
}