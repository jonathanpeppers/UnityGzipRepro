using System.Collections;
using NUnit.Framework;
using System.Text;
using System.IO;
using System.IO.Compression;

[TestFixture]
public class GzipTests
{
    [Test]
    public void GzipShouldWork()
    {
        string text = "Hello World";
        string actual;

        using (var memoryStream = new MemoryStream())
        {
            //Compress
            using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
            using (var writer = new StreamWriter(gzip))
            {
                writer.Write(text);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);

            //Decompress
            using (var gzip = new GZipStream(memoryStream, CompressionMode.Decompress, true))
            using (var reader = new StreamReader(gzip))
            {
                actual = reader.ReadToEnd();
            }
        }

        Assert.AreEqual(text, actual);
    }

    [Test]
    public void DeflateShouldWork()
    {
        string text = "Hello World";
        string actual;

        using (var memoryStream = new MemoryStream())
        {
            //Compress
            using (var gzip = new DeflateStream(memoryStream, CompressionMode.Compress, true))
            using (var writer = new StreamWriter(gzip))
            {
                writer.Write(text);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            
            //Decompress
            using (var gzip = new DeflateStream(memoryStream, CompressionMode.Decompress, true))
            using (var reader = new StreamReader(gzip))
            {
                actual = reader.ReadToEnd();
            }
        }

        Assert.AreEqual(text, actual);
    }
}
