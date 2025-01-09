using System.Text;
using Fuyu.Common.Hashing;

namespace Fuyu.Backend.Core.Services;

public static class CryptographyService
{
    public static byte[] Key => Encoding.UTF8.GetBytes("Qo*np7*yPHqWX8ZB3ZO@m1k4");

    public static byte[] EncryptAes(byte[] data)
    {
        return AesHelper.EncryptAes(data, Key);
    }

    public static byte[] DecryptAes(byte[] data)
    {
        return AesHelper.DecryptAes(data, Key);
    }
}
