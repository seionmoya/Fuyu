using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Fuyu.Backend.Core.Services;

// Comment: Should we move this to elsewhere?
public static class CryptographyService
{
    public static byte[] Key => Encoding.UTF8.GetBytes("Qo*np7*yPHqWX8ZB3ZO@m1k4");

    public static byte[] EncryptAes(byte[] data)
    {
        Aes aes = Aes.Create();
        aes.GenerateIV();
        aes.Padding = PaddingMode.Zeros;
        aes.Key = Key;
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        // TODO: Make this better. Maybe just aes.EncryptEcb ?
        var memory = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memory, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(data, 0, data.Length);
        cryptoStream.Close();
        // EFT concat the bytes
        return aes.IV.Concat(memory.ToArray()).ToArray();
    }

    public static byte[] DecryptAes(byte[] data)
    {
        Aes aes = Aes.Create();
        aes.GenerateIV();
        aes.Padding = PaddingMode.Zeros;
        aes.IV = data[..16];
        aes.Key = Key;
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        // TODO: Make this better. Maybe just aes.DecryptEcb ?
        var memory = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memory, decryptor, CryptoStreamMode.Write);
        cryptoStream.Write(data[..16], 0, data.Length - 16);
        cryptoStream.Close();
        return [];
    }
}
