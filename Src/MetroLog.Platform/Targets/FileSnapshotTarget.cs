// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.FileSnapshotTarget
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using MetroLog.Layouts;
using System;
using System.Threading.Tasks;
using Windows.Storage;

#nullable disable
namespace MetroLog.Targets
{
  public class FileSnapshotTarget : WinRTFileTarget
  {
    public FileSnapshotTarget()
      : this((Layout) new FileSnapshotLayout())
    {
    }

    public FileSnapshotTarget(Layout layout)
      : base(layout)
    {
      this.FileNamingParameters.IncludeLevel = true;
      this.FileNamingParameters.IncludeLogger = true;
      this.FileNamingParameters.IncludeSession = false;
      this.FileNamingParameters.IncludeSequence = true;
      this.FileNamingParameters.IncludeTimestamp = FileTimestampMode.DateTime;
      this.FileNamingParameters.CreationMode = FileCreationMode.ReplaceIfExisting;
    }

    protected override Task WriteTextToFileCore(IStorageFile file, string contents)
    {
      return FileIO.WriteTextAsync(file, contents).AsTask();
    }
  }
}
