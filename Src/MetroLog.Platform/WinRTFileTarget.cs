// Decompiled with JetBrains decompiler
// Type: MetroLog.WinRTFileTarget
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using MetroLog.Layouts;
using MetroLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;

#nullable disable
namespace MetroLog
{
  public abstract class WinRTFileTarget(Layout layout) : FileTargetBase(layout)
  {
    private static StorageFolder _logFolder;

    public static async Task<StorageFolder> EnsureInitializedAsync()
    {
      if (WinRTFileTarget._logFolder == null)
        WinRTFileTarget._logFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("MetroLogs", (CreationCollisionOption) 3);
      return WinRTFileTarget._logFolder;
    }

    protected override async Task<Stream> GetCompressedLogsInternal()
    {
      MemoryStream ms = new MemoryStream();
      await ZipFile.CreateFromDirectory((IStorageFolder) WinRTFileTarget._logFolder, (Stream) ms);
      ms.Position = 0L;
      return (Stream) ms;
    }

    protected override Task EnsureInitialized() => (Task) WinRTFileTarget.EnsureInitializedAsync();

    protected override sealed async Task DoCleanup(Regex pattern, DateTime threshold)
    {
      List<StorageFile> toDelete = new List<StorageFile>();
      foreach (StorageFile storageFile in (IEnumerable<StorageFile>) await WinRTFileTarget._logFolder.GetFilesAsync())
      {
        if (pattern.Match(storageFile.Name).Success && storageFile.DateCreated <= (DateTimeOffset) threshold)
          toDelete.Add(storageFile);
      }
      Regex zipPattern = new Regex("^Log(.*).zip$");
      foreach (StorageFile storageFile in (IEnumerable<StorageFile>) await ApplicationData.Current.TemporaryFolder.GetFilesAsync())
      {
        if (zipPattern.Match(storageFile.Name).Success)
          toDelete.Add(storageFile);
      }
      foreach (StorageFile file in toDelete)
      {
        try
        {
          await file.DeleteAsync();
        }
        catch (Exception ex)
        {
          InternalLogger.Current.Warn(string.Format("Failed to delete '{0}'.", (object) file.Path), ex);
        }
      }
    }

    protected override sealed async Task<LogWriteOperation> DoWriteAsync(
      string fileName,
      string contents,
      LogEventInfo entry)
    {
      await this.WriteTextToFileCore((IStorageFile) await WinRTFileTarget._logFolder.CreateFileAsync(fileName, this.FileNamingParameters.CreationMode == FileCreationMode.AppendIfExisting ? (CreationCollisionOption) 3 : (CreationCollisionOption) 1), contents);
      return new LogWriteOperation((Target) this, entry, true);
    }

    protected abstract Task WriteTextToFileCore(IStorageFile file, string contents);
  }
}
