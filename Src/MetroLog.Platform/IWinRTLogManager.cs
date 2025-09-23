// Decompiled with JetBrains decompiler
// Type: MetroLog.IWinRTLogManager
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using System.Threading.Tasks;
using Windows.Storage;

#nullable disable
namespace MetroLog
{
  public interface IWinRTLogManager : ILogManager
  {
    Task<IStorageFile> GetCompressedLogFile();

    Task ShareLogFile(string title, string description);
  }
}
