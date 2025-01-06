#if NET6_0_OR_GREATER
using System.IO;
using System.IO.Compression;

namespace Fuyu.Common.Compression
{
    public static class MemoryZlib
    {
        public static bool IsCompressed(byte[] data)
        {
            if (data == null || data.Length < 3)
            {
                return false;
            }

            // data[0]: Info (CM/CINFO) Header; must be 0x78
            if (data[0] != 0x78)
            {
                return false;
            }

            // data[1]: Flags (FLG) Header; compression level.
            switch (data[1])
            {
                case 0x01:  // lowest   (0-2)
                case 0x5E:  // low      (3-4)
                case 0x9C:  // normal   (5-6)
                case 0xDA:  // high     (7-9)
                    return true;

                default:    // no match
                    return false;
            }
        }

        // NOTE: assumes this is running inside the backend or launcher
        // -- seionmoya, 2024-10-07
        public static byte[] Compress(byte[] data, CompressionLevel level)
        {
            using (var msin = new MemoryStream(data))
            {
                using (var msout = new MemoryStream())
                {
                    using (var zs = new ZLibStream(msout, level))
                    {
                        msin.CopyTo(zs);
                        zs.Flush();
                        return msout.ToArray();
                    }
                }
            }
        }

        // NOTE: assumes this is running inside the backend or launcher
        // -- seionmoya, 2024-10-07
        public static byte[] Decompress(byte[] data)
        {

            using (var msin = new MemoryStream(data))
            {
                using (var msout = new MemoryStream())
                {
                    using (var zs = new ZLibStream(msin, CompressionMode.Decompress))
                    {
                        zs.CopyTo(msout);
                        return msout.ToArray();
                    }
                }
            }
        }
    }
}
#endif