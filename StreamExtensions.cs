using System.IO;

namespace WebManipulation
{
    public static class StreamExtensions
    {
        public static byte[] ReadFully(this Stream stream)
        {
            if (stream == null)
                return new byte[0];

            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}