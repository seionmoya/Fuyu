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
        var testString = "Testing Zlib with some string _ AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA SSSSSSSSSSSSSSSSSS FFFFFFFFFFFF VVVVVVVVVVVVVVVVVVVVVVVV 234534545";
        var bytes = Encoding.UTF8.GetBytes(testString);
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), testString);

        // compressing
        var compressed_bytes = MemoryZlib.Compress(bytes, System.IO.Compression.CompressionLevel.SmallestSize);
        Assert.IsNotNull(compressed_bytes);
        Assert.AreNotEqual(0, compressed_bytes.Length);

        // decompressing
        var decomp_bytes = MemoryZlib.Decompress(compressed_bytes);
        Assert.IsNotNull(decomp_bytes);
        Assert.AreNotEqual(0, decomp_bytes.Length);

        // check if decompressed and original is same
        Assert.AreEqual(Encoding.UTF8.GetString(decomp_bytes), testString);
        Assert.AreEqual(Encoding.UTF8.GetString(bytes), Encoding.UTF8.GetString(decomp_bytes));
    }
}
#endif