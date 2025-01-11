using System.Text;
using Fuyu.Common.Hashing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fuyu.Common.Compression;

namespace Fuyu.Tests.Common.Units;

[TestClass]
public class AesTest
{

    [TestMethod]
    public void TestAesFull()
    {
        // Initializing
        byte[] Key = Encoding.UTF8.GetBytes("Qo*np7*yPHqWX8ZB3ZO@m1k4");
        var test_string = "testAESFunctionWith Some string0000000000000000000sdfsdffsfsfds";
        byte[] bytes = Encoding.UTF8.GetBytes(test_string);
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), test_string);

        // testing enc
        var data_crypted = AesHelper.EncryptAes(bytes, Key);
        Assert.IsNotNull(data_crypted);
        Assert.AreNotEqual(0, data_crypted.Length);

        // testing dec
        var data_decypted = AesHelper.DecryptAes(data_crypted, Key);
        Assert.IsNotNull(data_decypted);
        Assert.AreNotEqual(0, data_decypted.Length);

        Assert.AreEqual(test_string, Encoding.UTF8.GetString(data_decypted));
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), Encoding.UTF8.GetString(data_decypted));

    }

    [TestMethod]
    public void TestAesZip()
    {
        // Initializing
        byte[] Key = Encoding.UTF8.GetBytes("Qo*np7*yPHqWX8ZB3ZO@m1k4");
        var test_string = "testAESFunctionWith Some string";
        byte[] bytes = Encoding.UTF8.GetBytes(test_string);
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), test_string);
        var compressed_bytes = MemoryZlib.Compress(bytes, System.IO.Compression.CompressionLevel.SmallestSize);


        // testing enc
        var data_crypted = AesHelper.EncryptAes(compressed_bytes, Key);
        Assert.IsNotNull(data_crypted);
        Assert.AreNotEqual(0, data_crypted.Length);

        // testing dec
        var data_decypted = AesHelper.DecryptAes(data_crypted, Key);
        Assert.IsNotNull(data_decypted);
        Assert.AreNotEqual(0, data_decypted.Length);

        var decomp_bytes = MemoryZlib.Decompress(compressed_bytes);
        Assert.IsNotNull(decomp_bytes);
        Assert.AreNotEqual(0, decomp_bytes.Length);

        Assert.AreEqual(Encoding.UTF8.GetString(decomp_bytes), test_string);
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), Encoding.UTF8.GetString(decomp_bytes));

    }
}
