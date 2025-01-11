using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Fuyu.Common.Hashing;

public static class AesHelper
{
    public static byte[] EncryptAes(byte[] data, byte[] Key)
    {
        using Aes aes = Aes.Create();
        aes.Padding = PaddingMode.Zeros;
        aes.Key = Key;
        aes.GenerateIV();
        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        cs.Write(data, 0, data.Length);
        cs.Close();
        // Concat faster than making new, using that.
        return aes.IV.Concat(ms.ToArray()).ToArray();
    }

#if NET5_0_OR_GREATER
    public static byte[] DecryptAes(byte[] data, byte[] key, bool removeLastByte = true)
    {
        using Aes aes = Aes.Create();
        aes.Padding = PaddingMode.Zeros;
        aes.Key = key;
        // This is faster than Linq.
        aes.IV = data[..16];
        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var memory = new MemoryStream();
        using var cryptoStream = new CryptoStream(memory, decryptor, CryptoStreamMode.Write);
        byte[] newData = data[16..];
        cryptoStream.Write(newData, 0, newData.Length);
        cryptoStream.Close();
        byte[] result = memory.ToArray();
        if (removeLastByte)
        {
            result = result[..^1];
        }
        return result;
    }
#else
    public static byte[] DecryptAes(byte[] data, byte[] Key, bool removeLastByte = true)
    {
        using Aes aes = Aes.Create();
        aes.Padding = PaddingMode.Zeros;
        aes.Key = Key;
        aes.IV = data.Take(16).ToArray();
        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var memory = new MemoryStream();
        using var cryptoStream = new CryptoStream(memory, decryptor, CryptoStreamMode.Write);
        byte[] newData = data.Skip(16).ToArray();
        cryptoStream.Write(newData, 0, newData.Length);
        cryptoStream.Close();
        byte[] result = memory.ToArray();
        if (removeLastByte)
        {
            result = result.Take(result.Length - 1).ToArray();
        }
        return result;
    }
#endif

}