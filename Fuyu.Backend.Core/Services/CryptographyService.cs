using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Fuyu.Backend.Core.Services;

public class CryptographyService
{
    public static CryptographyService Instance => instance.Value;
    private static readonly Lazy<CryptographyService> instance = new(() => new CryptographyService());

    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private CryptographyService()
    {


    }
    public byte[] Key => Encoding.UTF8.GetBytes("Qo*np7*yPHqWX8ZB3ZO@m1k4");

    public byte[] EncryptAes(byte[] data)
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

    public byte[] DecryptAes(byte[] data)
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
