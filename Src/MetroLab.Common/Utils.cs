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
using System;

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
        md5Async = await CalculateMd5FromStreamAsync(fileStream);
      }
      return md5Async;
    }

    private static async Task<string> CalculateMd5FromStreamAsync(Stream stream)
    {
      var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
      using (var ms = new MemoryStream())
      {
        await stream.CopyToAsync(ms);
        var buffer = ms.ToArray();
        var cbuf = CryptographicBuffer.CreateFromByteArray(buffer);
        var hash = alg.HashData(cbuf);
        return CryptographicBuffer.EncodeToHexString(hash);
      }
    }

    public static async Task<string> CalculateMd5Async(IInputStream inputStream)
    {
      using (var stream = inputStream.AsStreamForRead())
      {
        return await CalculateMd5FromStreamAsync(stream);
      }
    }
  }
}
