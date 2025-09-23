// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.CacheAgent
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;

#nullable disable
namespace VPN.ViewModel
{
  public static class CacheAgent
  {
    public const string GoogleCredentialsLocalSettingsName = "google";
    public const string FacebookCredentialsLocalSettingsName = "facebook";
    public const string KeepSolidCredentialsLocalSettingsName = "keepsolid";
    private const string IsNeedToShowApplicationTourLocalSettingsName = "applicationTour";
    private const string IsUserKnowHowToConnectSettingsName = "isUserKnowHowToConnect";

    public static void SaveToLocalSettings<T>(string name, T objectToSave)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(typeof (T)).WriteObject((Stream) memoryStream, (object) objectToSave);
        byte[] array = memoryStream.ToArray();
        ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)[name] = (object) Encoding.UTF8.GetString(array, 0, ((IEnumerable<byte>) array).Count<byte>());
      }
    }

    public static T LoadFromLocalSettings<T>(string name) where T : class
    {
      try
      {
        string s = (string) ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)[name];
        if (s == null)
          return default (T);
        using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(s)))
          return (T) new DataContractJsonSerializer(typeof (T)).ReadObject((Stream) memoryStream);
      }
      catch
      {
        return default (T);
      }
    }

    public static bool IsNeedToShowApplicationTour
    {
      get
      {
        return ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["applicationTour"] == null;
      }
      set
      {
        ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["applicationTour"] = (object) "no";
      }
    }

    public static bool IsUserKnowHowToConnect
    {
      get
      {
        return ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["isUserKnowHowToConnect"] != null;
      }
      set
      {
        ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["isUserKnowHowToConnect"] = (object) true;
      }
    }

    public static void DeleteLoginCache()
    {
      ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["google"] = (object) null;
      ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["facebook"] = (object) null;
      ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["keepsolid"] = (object) null;
    }

    public static string Encrypt(string dataToEncrypt, string openPartOfPassword, out string key)
    {
      key = CacheAgent.GenerateSalt();
      return CacheAgent.Encrypt(dataToEncrypt, Constants.GetDeviceID() + Constants.GetDeviceModel() + openPartOfPassword + "L,2W~fKo|]*,VFiZ8&n", key);
    }

    private static string Encrypt(string dataToEncrypt, string password, string salt)
    {
      uint iterationCount = 10000;
      IBuffer keyMaterial;
      IBuffer iv;
      CacheAgent.GenerateKeyMaterial(password, salt, iterationCount, out keyMaterial, out iv);
      IBuffer binary = CryptographicBuffer.ConvertStringToBinary(dataToEncrypt, (BinaryStringEncoding) 0);
      return CryptographicBuffer.EncodeToBase64String(CryptographicEngine.Encrypt(SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7).CreateSymmetricKey(keyMaterial), binary, iv));
    }

    public static string Decrypt(string dataToDecrypt, string openPartOfPassword, string salt)
    {
      string password = Constants.GetDeviceID() + Constants.GetDeviceModel() + openPartOfPassword + "L,2W~fKo|]*,VFiZ8&n";
      uint num = 10000;
      string salt1 = salt;
      int iterationCount = (int) num;
      IBuffer ibuffer1;
      ref IBuffer local1 = ref ibuffer1;
      IBuffer ibuffer2;
      ref IBuffer local2 = ref ibuffer2;
      CacheAgent.GenerateKeyMaterial(password, salt1, (uint) iterationCount, out local1, out local2);
      byte[] array = CryptographicEngine.Decrypt(SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7).CreateSymmetricKey(ibuffer1), CryptographicBuffer.DecodeFromBase64String(dataToDecrypt), ibuffer2).ToArray();
      return Encoding.UTF8.GetString(array, 0, array.Length);
    }

    private static void GenerateKeyMaterial(
      string password,
      string salt,
      uint iterationCount,
      out IBuffer keyMaterial,
      out IBuffer iv)
    {
      KeyDerivationParameters derivationParameters1 = KeyDerivationParameters.BuildForPbkdf2(CryptographicBuffer.ConvertStringToBinary(salt, (BinaryStringEncoding) 0), iterationCount);
      CryptographicKey key = KeyDerivationAlgorithmProvider.OpenAlgorithm(KeyDerivationAlgorithmNames.Pbkdf2Sha256).CreateKey(CryptographicBuffer.ConvertStringToBinary(password, (BinaryStringEncoding) 0));
      int num1 = 32;
      int num2 = 16;
      uint num3 = (uint) (num1 + num2);
      KeyDerivationParameters derivationParameters2 = derivationParameters1;
      int num4 = (int) num3;
      byte[] array = CryptographicEngine.DeriveKeyMaterial(key, derivationParameters2, (uint) num4).ToArray();
      keyMaterial = WindowsRuntimeBuffer.Create(array, 0, num1, num1);
      iv = WindowsRuntimeBuffer.Create(array, num1, num2, num2);
    }

    public static string GenerateSalt()
    {
      IBuffer random = CryptographicBuffer.GenerateRandom(1024U);
      return CryptographicBuffer.EncodeToBase64String(HashAlgorithmProvider.OpenAlgorithm("SHA256").HashData(random));
    }
  }
}
