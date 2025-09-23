// Decompiled with JetBrains decompiler
// Type: MetroLog.ZipFile
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

#nullable disable
namespace MetroLog
{
  internal static class ZipFile
  {
    public static Task CreateFromDirectory(IStorageFolder source, Stream destinationArchive)
    {
      return ZipFile.DoCreateFromDirectory(source, destinationArchive, new CompressionLevel?(), (Encoding) null);
    }

    private static async Task DoCreateFromDirectory(
      IStorageFolder source,
      Stream destinationArchive,
      CompressionLevel? compressionLevel,
      Encoding entryNameEncoding)
    {
      string fullName = ((IStorageItem) source).Path;
      using (ZipArchive destination = ZipFile.Open(destinationArchive, ZipArchiveMode.Create, entryNameEncoding))
      {
        foreach (IStorageItem sourceFile in await source.GetStorageItemsRecursive())
        {
          int length = sourceFile.Path.Length - fullName.Length;
          string entryName = sourceFile.Path.Substring(fullName.Length, length).TrimStart('\\', '/');
          if (sourceFile is IStorageFile)
          {
            ZipArchiveEntry entryFromFile = await ZipFile.DoCreateEntryFromFile(destination, (IStorageFile) sourceFile, entryName, compressionLevel);
          }
          else
            destination.CreateEntry(entryName + "\\");
          entryName = (string) null;
        }
      }
    }

    public static ZipArchive OpenRead(Stream archive) => ZipFile.Open(archive, ZipArchiveMode.Read);

    public static ZipArchive Open(Stream archive, ZipArchiveMode mode, Encoding entryNameEncoding = null)
    {
      if (archive == null)
        throw new ArgumentNullException(nameof (archive));
      return new ZipArchive(archive, mode, true, entryNameEncoding);
    }

    private static async Task<bool> IsDirEmpty(IStorageFolder possiblyEmptyDir)
    {
      return (await possiblyEmptyDir.GetFilesAsync()).Count == 0;
    }

    private static async Task<IEnumerable<IStorageItem>> GetStorageItemsRecursive(
      this IStorageFolder parent)
    {
      List<IStorageItem> list = new List<IStorageItem>();
      Stack<IStorageFolder> stack = new Stack<IStorageFolder>();
      stack.Push(parent);
      while (stack.Count > 0)
      {
        IStorageFolder current = stack.Pop();
        IReadOnlyList<StorageFile> filesAsync = await current.GetFilesAsync();
        if (filesAsync.Count > 0)
          list.AddRange((IEnumerable<IStorageItem>) filesAsync);
        else
          list.Add((IStorageItem) current);
        foreach (IStorageFolder istorageFolder in (IEnumerable<StorageFolder>) await current.GetFoldersAsync())
          stack.Push(istorageFolder);
        current = (IStorageFolder) null;
      }
      return (IEnumerable<IStorageItem>) list;
    }

    private static async Task<ZipArchiveEntry> DoCreateEntryFromFile(
      ZipArchive destination,
      IStorageFile sourceFile,
      string entryName,
      CompressionLevel? compressionLevel)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (sourceFile == null)
        throw new ArgumentNullException(nameof (sourceFile));
      if (entryName == null)
        throw new ArgumentNullException(nameof (entryName));
      ZipArchiveEntry entryFromFile;
      using (Stream stream = ((IRandomAccessStream) await ((IRandomAccessStreamReference) sourceFile).OpenReadAsync()).AsStream())
      {
        ZipArchiveEntry zipArchiveEntry = compressionLevel.HasValue ? destination.CreateEntry(entryName, compressionLevel.Value) : destination.CreateEntry(entryName);
        DateTime dateTime = (await ((IStorageItem) sourceFile).GetBasicPropertiesAsync()).DateModified.UtcDateTime;
        if (dateTime.Year < 1980 || dateTime.Year > 2107)
          dateTime = new DateTime(1980, 1, 1, 0, 0, 0);
        zipArchiveEntry.LastWriteTime = (DateTimeOffset) dateTime;
        using (Stream destination1 = zipArchiveEntry.Open())
          await stream.CopyToAsync(destination1);
        entryFromFile = zipArchiveEntry;
      }
      return entryFromFile;
    }
  }
}
