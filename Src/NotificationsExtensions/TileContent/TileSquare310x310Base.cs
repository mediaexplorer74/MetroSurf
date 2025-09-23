// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare310x310Base
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Text;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare310x310Base : TileNotificationBase
  {
    private IWide310x150TileNotificationContent m_Wide310x150Content;
    private bool m_RequireWide310x150Content = true;

    public IWide310x150TileNotificationContent Wide310x150Content
    {
      get => this.m_Wide310x150Content;
      set => this.m_Wide310x150Content = value;
    }

    public bool RequireWide310x150Content
    {
      get => this.m_RequireWide310x150Content;
      set => this.m_RequireWide310x150Content = value;
    }

    public TileSquare310x310Base(string templateName, string fallbackName, int imageCount, int textCount)
      : base(templateName, fallbackName, imageCount, textCount)
    {
    }

    public override string GetContent()
    {
      if (this.RequireWide310x150Content && this.Wide310x150Content == null)
        throw new NotificationContentValidationException("Wide310x150 tile content should be included with each large tile. If this behavior is undesired, use the RequireWide310x150Content property.");
      if (this.Wide310x150Content != null && this.Wide310x150Content.RequireSquare150x150Content && this.Wide310x150Content.Square150x150Content == null)
        throw new NotificationContentValidationException("This tile's wide content requires square content. If this behavior is undesired, use the Wide310x150Content.RequireSquare150x150Content property.");

      StringBuilder visualBuilder = new StringBuilder(string.Empty);
      visualBuilder.AppendFormat("<visual version='{0}'", 3);
      if (!string.IsNullOrWhiteSpace(this.Lang))
        visualBuilder.AppendFormat(" lang='{0}'", (object) Util.HttpEncode(this.Lang));
      if (this.Branding != TileBranding.Logo)
        visualBuilder.AppendFormat(" branding='{0}'", (object) this.Branding.ToString().ToLowerInvariant());
      if (!string.IsNullOrWhiteSpace(this.BaseUri))
        visualBuilder.AppendFormat(" baseUri='{0}'", (object) Util.HttpEncode(this.BaseUri));
      if (this.AddImageQuery)
        visualBuilder.AppendFormat(" addImageQuery='true'");
      visualBuilder.Append(">");

      StringBuilder sb = new StringBuilder(string.Empty);
      sb.AppendFormat("<tile>{0}", (object) visualBuilder);
      if (this.Wide310x150Content != null)
      {
        if (!(this.Wide310x150Content is IWide310x150TileInternal wide310x150Content))
          throw new NotificationContentValidationException("The provided wide tile content class is unsupported.");
        sb.Append(wide310x150Content.SerializeBindings(this.Lang, this.BaseUri, this.Branding, this.AddImageQuery));
      }

      sb.AppendFormat("<binding template='{0}'", (object) this.TemplateName);
      if (!string.IsNullOrWhiteSpace(this.FallbackName))
        sb.AppendFormat(" fallback='{0}'", (object) this.FallbackName);
      if (!string.IsNullOrWhiteSpace(this.ContentId))
        sb.AppendFormat(" contentId='{0}'", (object) this.ContentId.ToLowerInvariant());
      sb.AppendFormat(">{0}</binding></visual></tile>", (object) this.SerializeProperties(this.Lang, this.BaseUri, this.AddImageQuery));
      return sb.ToString();
    }
  }
}
