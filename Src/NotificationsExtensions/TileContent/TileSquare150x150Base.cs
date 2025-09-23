// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare150x150Base
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Text;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare150x150Base : TileNotificationBase, ISquare150x150TileInternal
  {
    private ISquare71x71TileNotificationContent m_Square71x71Content;
    private bool m_RequireSquare71x71Content;

    public ISquare71x71TileNotificationContent Square71x71Content
    {
      get => this.m_Square71x71Content;
      set => this.m_Square71x71Content = value;
    }

    public bool RequireSquare71x71Content
    {
      get => this.m_RequireSquare71x71Content;
      set => this.m_RequireSquare71x71Content = value;
    }

    public TileSquare150x150Base(string templateName, string fallbackName, int imageCount, int textCount)
      : base(templateName, fallbackName, imageCount, textCount)
    {
    }

    public override string GetContent()
    {
      if (this.RequireSquare71x71Content && this.Square71x71Content == null)
        throw new NotificationContentValidationException("Square71x71 tile content should be included with each medium tile. If this behavior is undesired, use the RequireSquare71x71Content property.");
      StringBuilder stringBuilder = new StringBuilder(string.Empty);
      stringBuilder.AppendFormat("<tile><visual version='{0}'", 3);
      if (!string.IsNullOrWhiteSpace(this.Lang))
        stringBuilder.AppendFormat(" lang='{0}'", Util.HttpEncode(this.Lang));
      if (this.Branding != TileBranding.Logo)
        stringBuilder.AppendFormat(" branding='{0}'", this.Branding.ToString().ToLowerInvariant());
      if (!string.IsNullOrWhiteSpace(this.BaseUri))
        stringBuilder.AppendFormat(" baseUri='{0}'", Util.HttpEncode(this.BaseUri));
      if (this.AddImageQuery)
        stringBuilder.AppendFormat(" addImageQuery='true'");
      stringBuilder.Append(">");
      if (this.Square71x71Content != null)
      {
        if (!(this.Square71x71Content is ISquare71x71TileInternal square71x71Content))
          throw new NotificationContentValidationException("The provided small tile content class is unsupported.");
        stringBuilder.Append(square71x71Content.SerializeBinding(this.Lang, this.BaseUri, this.Branding, this.AddImageQuery));
      }
      stringBuilder.Append(this.SerializeBinding(this.Lang, this.BaseUri, this.Branding, this.AddImageQuery));
      stringBuilder.Append("</visual></tile>");
      return stringBuilder.ToString();
    }

    public string SerializeBinding(
      string globalLang,
      string globalBaseUri,
      TileBranding globalBranding,
      bool globalAddImageQuery)
    {
      StringBuilder stringBuilder = new StringBuilder(string.Empty);
      if (this.Square71x71Content != null)
      {
        if (!(this.Square71x71Content is ISquare71x71TileInternal square71x71Content))
          throw new NotificationContentValidationException("The provided small tile content class is unsupported.");
        stringBuilder.Append(square71x71Content.SerializeBinding(this.Lang, this.BaseUri, this.Branding, this.AddImageQuery));
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
      stringBuilder.AppendFormat(">{0}</binding>", this.SerializeProperties(globalLang, globalBaseUri, globalAddImageQuery));
      return stringBuilder.ToString();
    }
  }
}
