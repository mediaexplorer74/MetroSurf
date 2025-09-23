// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.FileStreamingTarget
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
  public class FileStreamingTarget : WinRTFileTarget
  {
    public FileStreamingTarget()
      : this((Layout) new SingleLineLayout())
    {
    }

    public FileStreamingTarget(Layout layout)
      : base(layout)
    {
      this.FileNamingParameters.IncludeLevel = false;
      this.FileNamingParameters.IncludeLogger = false;
      this.FileNamingParameters.IncludeSequence = false;
      this.FileNamingParameters.IncludeSession = false;
      this.FileNamingParameters.IncludeTimestamp = FileTimestampMode.Date;
      this.FileNamingParameters.CreationMode = FileCreationMode.AppendIfExisting;
    }

    protected override Task WriteTextToFileCore(IStorageFile file, string contents)
    {
      return FileIO.AppendTextAsync(file, contents + Environment.NewLine).AsTask();
    }
  }
}
