// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.JsonPostTarget
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using MetroLog.Internal;
using MetroLog.Layouts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

#nullable disable
namespace MetroLog.Targets
{
  public class JsonPostTarget : BufferedTarget
  {
    public Uri Url { get; private set; }

    public event EventHandler<HttpClientEventArgs> BeforePost;

    public JsonPostTarget(int threshold, Uri uri)
      : this((Layout) new NullLayout(), threshold, uri)
    {
    }

    public JsonPostTarget(Layout layout, int threshold, Uri url)
      : base(layout, threshold)
    {
      this.Url = url;
    }

    protected override async Task DoFlushAsync(
      LogWriteContext context,
      IEnumerable<LogEventInfo> toFlush)
    {
      string json = new JsonPostWrapper(PlatformAdapter.Resolve<ILoggingEnvironment>(), toFlush).ToJson();
      HttpClient client = new HttpClient();
      StringContent content = new StringContent(json);
      content.Headers.ContentType.MediaType = "text/json";
      this.OnBeforePost(new HttpClientEventArgs(client));
      HttpResponseMessage httpResponseMessage = await client.PostAsync(this.Url, (HttpContent) content);
    }

    protected virtual void OnBeforePost(HttpClientEventArgs args)
    {
      if (this.BeforePost == null)
        return;
      this.BeforePost((object) this, args);
    }
  }
}
