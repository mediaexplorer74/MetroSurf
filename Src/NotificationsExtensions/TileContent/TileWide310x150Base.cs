// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileWide310x150Base
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Text;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileWide310x150Base : TileNotificationBase, IWide310x150TileInternal
  {
    private ISquare150x150TileNotificationContent m_Square150x150Content;
    private bool m_RequireSquare150x150Content = true;

    public TileWide310x150Base(string templateName, string fallbackName, int imageCount, int textCount)
      : base(templateName, fallbackName, imageCount, textCount)
    {
    }

    public ISquare150x150TileNotificationContent Square150x150Content
    {
      get => this.m_Square150x150Content;
      set => this.m_Square150x150Content = value;
    }

    public bool RequireSquare150x150Content
    {
      get => this.m_RequireSquare150x150Content;
      set => this.m_RequireSquare150x150Content = value;
    }

    public override string GetContent()
    {
      if (this.RequireSquare150x150Content && this.Square150x150Content == null)
        throw new NotificationContentValidationException("Square150x150 tile content should be included with each wide tile. If this behavior is undesired, use the RequireSquare150x150Content property.");
      StringBuilder stringBuilder1 = new StringBuilder(string.Empty);
      stringBuilder1.AppendFormat("<visual version='{0}'", 3);
      if (!string.IsNullOrWhiteSpace(this.Lang))
        stringBuilder1.AppendFormat(" lang='{0}'", Util.HttpEncode(this.Lang));
      if (this.Branding != TileBranding.Logo)
        stringBuilder1.AppendFormat(" branding='{0}'", this.Branding.ToString().ToLowerInvariant());
      if (!string.IsNullOrWhiteSpace(this.BaseUri))
        stringBuilder1.AppendFormat(" baseUri='{0}'", Util.HttpEncode(this.BaseUri));
      if (this.AddImageQuery)
        stringBuilder1.AppendFormat(" addImageQuery='true'");
      stringBuilder1.Append(">");
      StringBuilder stringBuilder2 = new StringBuilder(string.Empty);
      stringBuilder2.AppendFormat("<tile>{0}", stringBuilder1);
      if (this.Square150x150Content != null)
      {
        if (!(this.Square150x150Content is ISquare150x150TileInternal square150x150Content))
          throw new NotificationContentValidationException("The provided square tile content class is unsupported.");
        stringBuilder2.Append(square150x150Content.SerializeBinding(this.Lang, this.BaseUri, this.Branding, this.AddImageQuery));
      }
      stringBuilder2.AppendFormat("<binding template='{0}'", this.TemplateName);
      if (!string.IsNullOrWhiteSpace(this.FallbackName))
        stringBuilder2.AppendFormat(" fallback='{0}'", this.FallbackName);
      stringBuilder2.AppendFormat(">{0}</binding></visual></tile>", this.SerializeProperties(this.Lang, this.BaseUri, this.AddImageQuery));
      return stringBuilder2.ToString();
    }

    public string SerializeBindings(
      string globalLang,
      string globalBaseUri,
      TileBranding globalBranding,
      bool globalAddImageQuery)
    {
      StringBuilder stringBuilder = new StringBuilder(string.Empty);
      if (this.Square150x150Content != null)
      {
        if (!(this.Square150x150Content is ISquare150x150TileInternal square150x150Content))
          throw new NotificationContentValidationException("The provided square tile content class is unsupported.");
        stringBuilder.Append(square150x150Content.SerializeBinding(this.Lang, this.BaseUri, this.Branding, this.AddImageQuery));
      }
      stringBuilder.AppendFormat("<binding template='{0}'", this.TemplateName);
      if (!string.IsNullOrWhiteSpace(this.FallbackName))
        stringBuilder.AppendFormat(" fallback='{0}'", this.FallbackName);
      if (!string.IsNullOrWhiteSpace(this.Lang) && !this.Lang.Equals(globalLang))
      {
        stringBuilder.AppendFormat(" lang='{0}'", Util.HttpEncode(this.Lang));
        globalLang = this.Lang;
      }
      if (this.Branding != TileBranding.Logo && this.Branding != globalBranding)
        stringBuilder.AppendFormat(" branding='{0}'", this.Branding.ToString().ToLowerInvariant());
      if (!string.IsNullOrWhiteSpace(this.BaseUri) && !this.BaseUri.Equals(globalBaseUri))
      {
        stringBuilder.AppendFormat(" baseUri='{0}'", Util.HttpEncode(this.BaseUri));
        globalBaseUri = this.BaseUri;
      }
      if (this.AddImageQueryNullable.HasValue)
      {
        bool? imageQueryNullable = this.AddImageQueryNullable;
        bool flag = globalAddImageQuery;
        if ((imageQueryNullable.GetValueOrDefault() == flag ? (!imageQueryNullable.HasValue ? 1 : 0) : 1) != 0)
        {
          stringBuilder.AppendFormat(" addImageQuery='{0}'", this.AddImageQuery.ToString().ToLowerInvariant());
          globalAddImageQuery = this.AddImageQuery;
        }
      }
      if (!string.IsNullOrWhiteSpace(this.ContentId))
        stringBuilder.AppendFormat(" contentId='{0}'", this.ContentId.ToLowerInvariant());
      stringBuilder.AppendFormat("> {0}</binding>", this.SerializeProperties(globalLang, globalBaseUri, globalAddImageQuery));
      return stringBuilder.ToString();
    }
  }
}
