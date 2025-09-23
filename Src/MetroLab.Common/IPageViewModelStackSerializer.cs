// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.IPageViewModelStackSerializer
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System.IO;
using System.Threading.Tasks;

#nullable disable
namespace MetroLab.Common
{
  public interface IPageViewModelStackSerializer
  {
    Task SerializeAsync(IPageViewModel[] pageViewModels, Stream streamForWrite);

    Task<IPageViewModel[]> DeserializeAsync(Stream streamForRead);
  }
}
