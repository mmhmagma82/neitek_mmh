using Common.Model;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Application.Service.Tools
{
    public static class ReadData
    {
        public static string RdString(object _data)
        {
            if (_data == null || string.IsNullOrEmpty(_data.ToString()) || string.IsNullOrWhiteSpace(_data.ToString())) return "";
            return _data.ToString().Trim();
        }
        public static string RdEncryptedString(object _data)
        {
            if (_data == null || string.IsNullOrEmpty(_data.ToString()) || string.IsNullOrWhiteSpace(_data.ToString())) return "";
            return Crypto.Decrypt(_data.ToString().Trim());
        }
        public static int RdInt(object _data)
        {
            if (_data == null || string.IsNullOrEmpty(_data.ToString()) || string.IsNullOrWhiteSpace(_data.ToString())) return 0;
            return int.Parse(_data.ToString());
        }
        public static double RdDouble(object _data)
        {
            if (_data == null || string.IsNullOrEmpty(_data.ToString()) || string.IsNullOrWhiteSpace(_data.ToString())) return 0.0;
            return double.Parse(_data.ToString());
        }
        public static float RdFloat(object _data)
        {
            if (_data == null || string.IsNullOrEmpty(_data.ToString()) || string.IsNullOrWhiteSpace(_data.ToString())) return 0;
            return float.Parse(_data.ToString());
        }
        public static DateTime RdDate(object _data)
        {
            if (_data == null || string.IsNullOrEmpty(_data.ToString()) || string.IsNullOrWhiteSpace(_data.ToString())) return DateTime.Now;
            return DateTime.Parse(_data.ToString());
        }
        public static bool RdBool(object _data)
        {
            if (_data == null || string.IsNullOrEmpty(_data.ToString()) || string.IsNullOrWhiteSpace(_data.ToString())) return false;
            return bool.Parse(_data.ToString().Replace("0", "false").Replace("1", "true"));
        }
        public static double RdEncryptedNumber(object _data)
        {
            if (_data == null || string.IsNullOrEmpty(_data.ToString()) || string.IsNullOrWhiteSpace(_data.ToString())) return 0.0;
            return double.Parse(Crypto.Decrypt(_data.ToString().Trim()));
        }
        public static string RdCompressString(object _data)
        {
            if (_data != null || !string.IsNullOrEmpty(_data.ToString()) || !string.IsNullOrWhiteSpace(_data.ToString()))
            {
                _data = _data.ToString().Replace("\n", "|-!|");
                byte[] compressedBytes;
                using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(_data.ToString())))
                {
                    var compressedStream = new MemoryStream();
                    var compressorStream = new DeflateStream(compressedStream, CompressionLevel.Fastest, true);
                    uncompressedStream.CopyTo(compressorStream);
                    compressedBytes = compressedStream.ToArray();
                }
                return Convert.ToBase64String(compressedBytes);
            }
            else
                return "";
        }
        public static string RdDecompressString(object _data)
        {
            if (_data != null || !string.IsNullOrEmpty(_data.ToString()) || !string.IsNullOrWhiteSpace(_data.ToString()))
            {
                byte[] decompressedBytes;
                var compressedStream = new MemoryStream(Convert.FromBase64String(_data.ToString()));
                using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
                {
                    var decompressedStream = new MemoryStream();
                    decompressorStream.CopyTo(decompressedStream);
                    decompressedBytes = decompressedStream.ToArray();
                }
                return Encoding.UTF8.GetString(decompressedBytes).Replace("|-!|", "<br />");
            }
            else
                return "";
        }
    }
}