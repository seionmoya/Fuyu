#if NET5_0_OR_GREATER
using System.Text;
using Fuyu.Common.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fuyu.Tests.Common.Units;

[TestClass]
public class ZlibTest
{
    [TestMethod]
    public void TestSmallZip()
    {
        // Initializing
        var test_string = "Testing Zlib with some string _ AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA SSSSSSSSSSSSSSSSSS FFFFFFFFFFFF VVVVVVVVVVVVVVVVVVVVVVVV 234534545";
        byte[] bytes = Encoding.UTF8.GetBytes(test_string);
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), test_string);

        // compressinh
        var compressed_bytes = MemoryZlib.Compress(bytes, System.IO.Compression.CompressionLevel.SmallestSize);
        Assert.IsNotNull(compressed_bytes);
        Assert.AreNotEqual(0, compressed_bytes.Length);

        var decomp_bytes = MemoryZlib.Decompress(compressed_bytes);
        Assert.IsNotNull(decomp_bytes);
        Assert.AreNotEqual(0, decomp_bytes.Length);

        Assert.AreEqual(Encoding.UTF8.GetString(decomp_bytes), test_string);
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), Encoding.UTF8.GetString(decomp_bytes));

    }


}
#endif