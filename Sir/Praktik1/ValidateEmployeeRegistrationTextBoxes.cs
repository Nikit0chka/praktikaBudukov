using System;
using System.Linq;
using System.ComponentModel;

namespace Praktik1;

internal class ValidateEmployeeRegistrationTextBoxes : IDataErrorInfo
{
    public string? ID { get; set; }
    public string? Surname { get; set; }
    public string? Name { get; set; }
    public string? Patronymic { get; set; }
    public string? Passport { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    private bool IsNoErrors;

    public bool IsAllInputCorrect()
    {
        return IsNoErrors;
    }

    public string this[string columnName]
    {
        get
        {
            string error = string.Empty;
            switch (columnName)
            {
                case "ID":
                    if (ID == null)
                    {
                        error = "ID must be not empty!";
                        break;
                    }
                    if (!ID.Any(Char.IsNumber))
                        error = "ID must contain only numbers!";
                    break;

                case "Surname":
                    if (Surname == null)
                    {
                        error = "Surname must be not empty!";
                        break;
                    }
                    if (!Surname.Any(Char.IsLetter))
                        error = "Surname can contain only letters!";
                    if (!Surname.Skip(1).Any(Char.IsLower))
                        error = "All surname letters must be lower except first!";
                    if (Char.IsLower(Surname[0]))
                        error = "First surname letter must be higher!";
                    break;

                case "Name":
                    if (Name == null)
                    {
                        error = "Name must be not empty!";
                        break;
                    }
                    if (!Name.Any(Char.IsLetter))
                        error = "Name can contain only letters!";
                    if (!Name.Skip(1).Any(Char.IsLower))
                        error = "All name letters must be lower except first!";
                    if (Char.IsLower(Name[0]))
                        error = "First name letter must be higher!";
                    break;

                case "Patronymic":
                    if (Patronymic == null)
                        break;
                    if (!Patronymic.Any(Char.IsLetter))
                        error = "Patronymic can contain only letters!";
                    if (!Patronymic.Skip(1).Any(Char.IsLower))
                        error = "All patronymic letters must be lower except first!";
                    if (Char.IsLower(Patronymic[0]))
                        error = "First patronymic letter must be higher!";
                    break;

                case "Passport":
                    if (Passport == null)
                    {
                        error = "Passport must be not empty!";
                        break;
                    }
                    if (Passport.Length != 10)
                        error = "Passport must include ten numbers!";
                    if (!Passport.Any(Char.IsNumber))
                        error = "Passport must include only numbers!";
                    break;

                case "PhoneNumber":
                    if (PhoneNumber == null)
                        break;
                    if ((PhoneNumber[0] != '+' || PhoneNumber.Length != 12) && (PhoneNumber[0] != '8' || PhoneNumber.Length != 11))
                        error = "Phone number must look like +79873691806 or 89873691806!";
                    if (!PhoneNumber.Skip(1).Any(Char.IsNumber))
                        error = "Phone number must contain only numbers except '+'!";
                    break;

                case "Email":
                    if (Email == null)
                    {
                        error = "Email must be not empty!";
                        break;
                    }
                    if (!Email.Contains('@') || !Email.Substring(Email.IndexOf('@')).Contains('.'))
                        error = "Email must contain '@' and '.' in domen!";
                    break;
            }
            IsNoErrors = (error == string.Empty) ? true : false;

            return error;
        }
    }
    public string Error
    {
        get => throw new NotImplementedException();
    }
}
