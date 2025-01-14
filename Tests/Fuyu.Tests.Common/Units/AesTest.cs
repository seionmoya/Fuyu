using System.Text;
using Fuyu.Common.Compression;
using Fuyu.Common.Hashing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fuyu.Tests.Common.Units;

[TestClass]
public class AesTest
{
    [TestMethod]
    public void TestAesFull()
    {
        // Initializing
        byte[] key = Encoding.UTF8.GetBytes("Qo*np7*yPHqWX8ZB3ZO@m1k4");
        var testString = "testAESFunctionWith Some string0000000000000000000sdfsdffsfsfds";
        byte[] bytes = Encoding.UTF8.GetBytes(testString);
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), testString);

        // testing enc
        var dataEncrypted = AesHelper.EncryptAes(bytes, key);
        Assert.IsNotNull(dataEncrypted);
        Assert.AreNotEqual(0, dataEncrypted.Length);

        // testing dec
        var dataDecypted = AesHelper.DecryptAes(dataEncrypted, key);
        Assert.IsNotNull(dataDecypted);
        Assert.AreNotEqual(0, dataDecypted.Length);

        // test if the 2 bytes are the same
        Assert.AreEqual(testString, Encoding.UTF8.GetString(dataDecypted));
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), Encoding.UTF8.GetString(dataDecypted));
    }

    [TestMethod]
    public void TestAesZip()
    {
        // Initializing
        byte[] key = Encoding.UTF8.GetBytes("Qo*np7*yPHqWX8ZB3ZO@m1k4");
        var testString = "testAESFunctionWith Some string";
        byte[] bytes = Encoding.UTF8.GetBytes(testString);
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), testString);
        var compressedBytes = MemoryZlib.Compress(bytes, System.IO.Compression.CompressionLevel.SmallestSize);

        // testing enc
        var dataEncrypted = AesHelper.EncryptAes(compressedBytes, key);
        Assert.IsNotNull(dataEncrypted);
        Assert.AreNotEqual(0, dataEncrypted.Length);

        // testing dec
        var dataDecypted = AesHelper.DecryptAes(dataEncrypted, key);
        Assert.IsNotNull(dataDecypted);
        Assert.AreNotEqual(0, dataDecypted.Length);

        // testing decompress with zlib
        var decompressedBytes = MemoryZlib.Decompress(compressedBytes);
        Assert.IsNotNull(decompressedBytes);
        Assert.AreNotEqual(0, decompressedBytes.Length);

        // test if the 2 bytes are the same
        Assert.AreEqual(testString, Encoding.UTF8.GetString(decompressedBytes));
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), Encoding.UTF8.GetString(decompressedBytes));
    }
}