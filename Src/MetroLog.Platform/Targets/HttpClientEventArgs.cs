// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.HttpClientEventArgs
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using System;
using System.Net.Http;

#nullable disable
namespace MetroLog.Targets
{
  public class HttpClientEventArgs : EventArgs
  {
    public HttpClient Client { get; private set; }

    public HttpClientEventArgs(HttpClient client)
    {
    }
  }
}
