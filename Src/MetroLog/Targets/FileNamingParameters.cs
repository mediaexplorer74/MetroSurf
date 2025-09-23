// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.FileNamingParameters
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace MetroLog.Targets
{
  public class FileNamingParameters
  {
    public bool IncludeLevel { get; set; }

    public FileTimestampMode IncludeTimestamp { get; set; }

    public bool IncludeLogger { get; set; }

    public bool IncludeSession { get; set; }

    public bool IncludeSequence { get; set; }

    public FileCreationMode CreationMode { get; set; }

    public FileNamingParameters()
    {
      this.IncludeLevel = false;
      this.IncludeTimestamp = FileTimestampMode.Date;
      this.IncludeLogger = false;
      this.IncludeSession = true;
      this.IncludeSequence = false;
      this.CreationMode = FileCreationMode.AppendIfExisting;
    }

    public string GetFilename(LogWriteContext context, LogEventInfo entry)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("Log");
      if (this.IncludeLevel)
      {
        stringBuilder1.Append(" - ");
        stringBuilder1.Append(entry.Level.ToString().ToUpper());
      }
      if (this.IncludeLogger)
      {
        stringBuilder1.Append(" - ");
        stringBuilder1.Append(entry.Logger);
      }
      if (this.IncludeTimestamp != FileTimestampMode.None)
      {
        bool flag = (this.IncludeTimestamp & FileTimestampMode.Date) != 0;
        DateTimeOffset timeStamp;
        if (flag)
        {
          stringBuilder1.Append(" - ");
          StringBuilder stringBuilder2 = stringBuilder1;
          timeStamp = entry.TimeStamp;
          string str = timeStamp.ToString("yyyyMMdd");
          stringBuilder2.Append(str);
        }
        if ((this.IncludeTimestamp & FileTimestampMode.Time) != 0)
        {
          if (flag)
            stringBuilder1.Append(" ");
          else
            stringBuilder1.Append(" - ");
          StringBuilder stringBuilder3 = stringBuilder1;
          timeStamp = entry.TimeStamp;
          string str = timeStamp.ToString("HHmmss");
          stringBuilder3.Append(str);
        }
      }
      if (this.IncludeSession)
      {
        stringBuilder1.Append(" - ");
        stringBuilder1.Append((object) context.Environment.SessionId);
      }
      if (this.IncludeSequence)
      {
        stringBuilder1.Append(" - ");
        stringBuilder1.Append(entry.SequenceID);
      }
      stringBuilder1.Append(".log");
      return stringBuilder1.ToString();
    }

    public Regex GetRegex()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("^Log");
      if (this.IncludeLevel)
      {
        stringBuilder.Append("\\s*-\\s*");
        stringBuilder.Append("\\w+");
      }
      if (this.IncludeLogger)
      {
        stringBuilder.Append("\\s*-\\s*");
        stringBuilder.Append("[\\w\\s]+");
      }
      if (this.IncludeTimestamp != FileTimestampMode.None)
      {
        bool flag = (this.IncludeTimestamp & FileTimestampMode.Date) != 0;
        if (flag)
        {
          stringBuilder.Append("\\s*-\\s*");
          stringBuilder.Append("[0-9]{8}");
        }
        if ((this.IncludeTimestamp & FileTimestampMode.Time) != 0)
        {
          if (flag)
            stringBuilder.Append("\\s+");
          else
            stringBuilder.Append("\\s*-\\s*");
          stringBuilder.Append("[0-9]{6}");
        }
      }
      if (this.IncludeSession)
      {
        stringBuilder.Append("\\s*-\\s*");
        stringBuilder.Append("[a-fA-F0-9\\-]+");
      }
      if (this.IncludeSequence)
      {
        stringBuilder.Append("\\s*-\\s*");
        stringBuilder.Append("[0-9]+");
      }
      stringBuilder.Append(".log$");
      return new Regex(stringBuilder.ToString(), RegexOptions.IgnoreCase | RegexOptions.Singleline);
    }
  }
}
