// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.FileTargetBase
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Internal;
using MetroLog.Layouts;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#nullable disable
namespace MetroLog.Targets
{
  public abstract class FileTargetBase : AsyncTarget
  {
    protected const string LogFolderName = "MetroLogs";
    private readonly AsyncLock _lock = new AsyncLock();

    public FileNamingParameters FileNamingParameters { get; private set; }

    public int RetainDays { get; set; }

    protected DateTime NextCleanupUtc { get; set; }

    protected FileTargetBase(Layout layout)
      : base(layout)
    {
      this.FileNamingParameters = new FileNamingParameters();
      this.RetainDays = 30;
    }

    protected abstract Task EnsureInitialized();

    protected abstract Task DoCleanup(Regex pattern, DateTime threshold);

    protected abstract Task<Stream> GetCompressedLogsInternal();

    internal Task<Stream> GetCompressedLogs() => this.GetCompressedLogsInternal();

    internal async Task ForceCleanupAsync()
    {
      DateTime threshold = DateTime.UtcNow.AddDays((double) -this.RetainDays);
      await this.DoCleanup(this.FileNamingParameters.GetRegex(), threshold);
    }

    private async Task CheckCleanupAsync()
    {
      DateTime utcNow = DateTime.UtcNow;
      if (utcNow < this.NextCleanupUtc)
        return;
      if (this.RetainDays < 1)
        return;
      try
      {
        DateTime threshold = utcNow.AddDays((double) -this.RetainDays);
        await this.DoCleanup(this.FileNamingParameters.GetRegex(), threshold);
      }
      finally
      {
        this.NextCleanupUtc = DateTime.UtcNow.AddHours(1.0);
      }
    }

    protected override sealed async Task<LogWriteOperation> WriteAsyncCore(
      LogWriteContext context,
      LogEventInfo entry)
    {
      LogWriteOperation logWriteOperation;
      using (await this._lock.LockAsync())
      {
        await this.EnsureInitialized();
        await this.CheckCleanupAsync();
        logWriteOperation = await this.DoWriteAsync(this.FileNamingParameters.GetFilename(context, entry), this.Layout.GetFormattedString(context, entry), entry);
      }
      return logWriteOperation;
    }

    protected abstract Task<LogWriteOperation> DoWriteAsync(
      string fileName,
      string contents,
      LogEventInfo entry);
  }
}
