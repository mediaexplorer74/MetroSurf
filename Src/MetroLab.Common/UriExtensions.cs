// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.UriExtensions
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MetroLab.Common
{
  public static class UriExtensions
  {
    public static Uri Append(this Uri uri, params string[] paths)
    {
      return new Uri(((IEnumerable<string>) paths).Aggregate<string, string>(uri.AbsoluteUri, (Func<string, string, string>) ((current, path) => string.Format("{0}/{1}", (object) current.TrimEnd('/'), (object) path.TrimStart('/')))));
    }
  }
}
