namespace Fuyu.Backend.Core.Models.Accounts
{
    public enum ERegisterStatus
    {
        UsernameEmpty,
        UsernameTooShort,
        UsernameTooLong,
        UsernameInvalid,
        PasswordEmpty,
        PasswordTooShort,
        PasswordTooLong,
        PasswordInvalid,
        AlreadyExists,
        Success
    }
}