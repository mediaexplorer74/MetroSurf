// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Utils
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System.IO;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;

#nullable disable
namespace MetroLab.Common
{
  public static class Utils
  {
    public static string CalculateMd5(string str)
    {
      return CryptographicBuffer.EncodeToHexString(HashAlgorithmProvider.OpenAlgorithm("MD5").HashData(CryptographicBuffer.ConvertStringToBinary(str, (BinaryStringEncoding) 0)));
    }

    public static async Task<string> CalculateMd5Async(StorageFile file)
    {
      string md5Async;
      using (Stream fileStream = await ((IStorageFile) file).OpenStreamForReadAsync())
      {
        using (IInputStream inputStream = fileStream.AsInputStream())
          md5Async = await Utils.CalculateMd5Async(inputStream);
      }
      return md5Async;
    }

    public static async Task<string> CalculateMd5Async(IInputStream inputStream)
    {
      HashAlgorithmProvider algorithmProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
      Buffer buffer = new Buffer(65536U);
      CryptographicHash hash = algorithmProvider.CreateHash();
      while (true)
      {
        IBuffer ibuffer = await inputStream.ReadAsync((IBuffer) buffer, 65536U, (InputStreamOptions) 0);
        if (buffer.Length > 0U)
          hash.Append((IBuffer) buffer);
        else
          break;
      }
      return CryptographicBuffer.EncodeToHexString(hash.GetValueAndReset());
    }
  }
}
