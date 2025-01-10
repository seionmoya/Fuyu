using System.Text.RegularExpressions;
using Fuyu.Backend.Core.Models.Accounts;

namespace Fuyu.Backend.Core.Services;

// TODO:
// * store max username length, min/max password length, security requirements in config
// -- seionmoya, 2024/09/08
public static partial class AccountValidationService
{
    private const int _minUsernameLength = 3;
    private const int _maxUsernameLength = 15;
    private const int _minPasswordLength = 8;
    private const int _maxPasswordLength = 32;

    // compile-time generated regex
    // `A-Z`:          may contain uppercase alpha
    // `a-z`:          may contain lowercase alpha
    // `0-9`:          may contain digits
    // NOTE: regex has no length constraint as the validation method
    //       contains it instead.
    // -- seionmoya, 2024/09/08
    [GeneratedRegex("^[a-zA-Z0-9_-]{0,}$")]
    private static partial Regex UsernameRegex();

    // compile-time generated regex
    // `(?=.*?[A-Z])`:          is at least one uppercase alpha present
    // NOTE: regex has no length constraint as the validation method
    //       contains it instead.
    // -- seionmoya, 2024/09/08
    [GeneratedRegex("^(?=.*?[A-Z]).{0,}$")]
    private static partial Regex LowerCaseRegex();

    // compile-time generated regex
    // NOTE: regex has no length constraint as the validation method
    //       contains it instead.
    // -- seionmoya, 2024/09/08
    [GeneratedRegex("^(?=.*?[a-z]).{0,}$")]
    private static partial Regex UpperCaseRegex();

    // compile-time generated regex
    // `(?=.*?[0-9])`:          is at least one digit present
    // NOTE: regex has no length constraint as the validation method
    //       contains it instead.
    // -- seionmoya, 2024/09/08
    [GeneratedRegex("^(?=.*?[0-9]).{0,}$")]
    private static partial Regex DigitRegex();

    // compile-time generated regex
    // `(?=.*?[#?!@$%^&*-])`:   is at least one special character present
    // NOTE: regex has no length constraint as the validation method
    //       contains it instead.
    // -- seionmoya, 2024/09/08
    [GeneratedRegex("^(?=.*?[#?!@$%^&*-]).{0,}$")]
    private static partial Regex SpecialRegex();

    // compile-time generated regex
    // `(?=.*?[A-Z])`:          is at least one uppercase alpha present
    // `(?=.*?[a-z])`:          is at least one lowercase alpha present
    // `(?=.*?[0-9])`:          is at least one digit present
    // `(?=.*?[#?!@$%^&*-])`:   is at least one special character present
    // NOTE: regex has no length constraint as the validation method
    //       contains it instead.
    // -- seionmoya, 2024/09/08
    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{0,}$")]
    private static partial Regex PasswordRegex();

    public static ERegisterStatus ValidateUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return ERegisterStatus.UsernameEmpty;
        }

        if (username.Length < _minUsernameLength)
        {
            return ERegisterStatus.UsernameTooLong;
        }

        if (username.Length > _maxUsernameLength)
        {
            return ERegisterStatus.UsernameTooLong;
        }

        if (!UsernameRegex().IsMatch(username))
        {
            return ERegisterStatus.UsernameInvalidCharacter;
        }

        return ERegisterStatus.Success;
    }

    public static ERegisterStatus ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return ERegisterStatus.PasswordEmpty;
        }

        if (password.Length < _minPasswordLength)
        {
            return ERegisterStatus.PasswordTooShort;
        }

        if (password.Length > _maxPasswordLength)
        {
            return ERegisterStatus.PasswordTooLong;
        }

        if (!LowerCaseRegex().IsMatch(password))
        {
            return ERegisterStatus.PasswordMissingLowerCase;
        }

        if (!UpperCaseRegex().IsMatch(password))
        {
            return ERegisterStatus.PasswordMissingUpperCase;
        }

        if (!DigitRegex().IsMatch(password))
        {
            return ERegisterStatus.PasswordMissingDigit;
        }

        if (!SpecialRegex().IsMatch(password))
        {
            return ERegisterStatus.PasswordMissingSpecial;
        }

        if (!PasswordRegex().IsMatch(password))
        {
            return ERegisterStatus.PasswordInvalidCharacter;
        }

        return ERegisterStatus.Success;
    }
}