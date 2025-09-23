// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.NotificationBase
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System;
using System.Text;
using Windows.Data.Xml.Dom;

#nullable disable
namespace NotificationsExtensions
{
  internal abstract class NotificationBase
  {
    private bool m_StrictValidation = true;
    private NotificationContentImage[] m_Images;
    private INotificationContentText[] m_TextFields;
    private string m_Lang;
    private string m_BaseUri;
    private string m_TemplateName;
    private string m_FallbackName;
    private bool? m_AddImageQueryNullable;

    protected NotificationBase(
      string templateName,
      string fallbackName,
      int imageCount,
      int textCount)
    {
      this.m_TemplateName = templateName;
      this.m_FallbackName = fallbackName;
      this.m_Images = new NotificationContentImage[imageCount];
      for (int index = 0; index < this.m_Images.Length; ++index)
        this.m_Images[index] = new NotificationContentImage();
      this.m_TextFields = new INotificationContentText[textCount];
      for (int index = 0; index < this.m_TextFields.Length; ++index)
        this.m_TextFields[index] = (INotificationContentText) new NotificationContentText();
    }

    public bool StrictValidation
    {
      get => this.m_StrictValidation;
      set => this.m_StrictValidation = value;
    }

    public abstract string GetContent();

    public override string ToString() => this.GetContent();

    public XmlDocument GetXml()
    {
      XmlDocument xml = new XmlDocument();
      xml.LoadXml(this.GetContent());
      return xml;
    }

    public INotificationContentImage[] Images => (INotificationContentImage[]) this.m_Images;

    public INotificationContentText[] TextFields => this.m_TextFields;

    public string BaseUri
    {
      get => this.m_BaseUri;
      set
      {
        if (this.StrictValidation)
        {
          if (!string.IsNullOrEmpty(value))
          {
            Uri uri;
            try
            {
              uri = new Uri(value);
            }
            catch (Exception ex)
            {
              throw new ArgumentException("Invalid URI. Use a valid URI or turn off StrictValidation", ex);
            }
            if (!uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) && !uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase) && !uri.Scheme.Equals("ms-appx", StringComparison.OrdinalIgnoreCase) && (!uri.Scheme.Equals("ms-appdata", StringComparison.OrdinalIgnoreCase) || !string.IsNullOrEmpty(uri.Authority) || !uri.AbsolutePath.StartsWith("/local/") && !uri.AbsolutePath.StartsWith("local/")))
              throw new ArgumentException("The BaseUri must begin with http://, https://, ms-appx:///, or ms-appdata:///local/.", nameof (value));
          }
        }
        this.m_BaseUri = value;
      }
    }

    public string Lang
    {
      get => this.m_Lang;
      set => this.m_Lang = value;
    }

    public bool AddImageQuery
    {
      get => this.m_AddImageQueryNullable.HasValue && this.m_AddImageQueryNullable.Value;
      set => this.m_AddImageQueryNullable = new bool?(value);
    }

    public bool? AddImageQueryNullable
    {
      get => this.m_AddImageQueryNullable;
      set => this.m_AddImageQueryNullable = value;
    }

    protected string SerializeProperties(
      string globalLang,
      string globalBaseUri,
      bool globalAddImageQuery)
    {
      globalLang = globalLang != null ? globalLang : string.Empty;
      globalBaseUri = string.IsNullOrWhiteSpace(globalBaseUri) ? (string) null : globalBaseUri;
      StringBuilder stringBuilder1 = new StringBuilder(string.Empty);
      for (int index = 0; index < this.m_Images.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.m_Images[index].Src))
        {
          string str1 = Util.HttpEncode(this.m_Images[index].Src);
          bool? imageQueryNullable;
          bool flag;
          if (!string.IsNullOrWhiteSpace(this.m_Images[index].Alt))
          {
            string str2 = Util.HttpEncode(this.m_Images[index].Alt);
            imageQueryNullable = this.m_Images[index].AddImageQueryNullable;
            if (imageQueryNullable.HasValue)
            {
              imageQueryNullable = this.m_Images[index].AddImageQueryNullable;
              flag = globalAddImageQuery;
              if ((imageQueryNullable.GetValueOrDefault() == flag ? (imageQueryNullable.HasValue ? 1 : 0) : 0) == 0)
              {
                StringBuilder stringBuilder2 = stringBuilder1;
                object[] objArray = new object[4];
                flag = this.m_Images[index].AddImageQuery;
                objArray[0] = (object) flag.ToString().ToLowerInvariant();
                objArray[1] = (object) (index + 1);
                objArray[2] = (object) str1;
                objArray[3] = (object) str2;
                stringBuilder2.AppendFormat("<image addImageQuery='{0}' id='{1}' src='{2}' alt='{3}'/>", objArray);
                continue;
              }
            }
            stringBuilder1.AppendFormat("<image id='{0}' src='{1}' alt='{2}'/>", (object) (index + 1), (object) str1, (object) str2);
          }
          else
          {
            imageQueryNullable = this.m_Images[index].AddImageQueryNullable;
            if (imageQueryNullable.HasValue)
            {
              imageQueryNullable = this.m_Images[index].AddImageQueryNullable;
              flag = globalAddImageQuery;
              if ((imageQueryNullable.GetValueOrDefault() == flag ? (imageQueryNullable.HasValue ? 1 : 0) : 0) == 0)
              {
                StringBuilder stringBuilder3 = stringBuilder1;
                object[] objArray = new object[3];
                flag = this.m_Images[index].AddImageQuery;
                objArray[0] = (object) flag.ToString().ToLowerInvariant();
                objArray[1] = (object) (index + 1);
                objArray[2] = (object) str1;
                stringBuilder3.AppendFormat("<image addImageQuery='{0}' id='{1}' src='{2}'/>", objArray);
                continue;
              }
            }
            stringBuilder1.AppendFormat("<image id='{0}' src='{1}'/>", (object) (index + 1), (object) str1);
          }
        }
      }
      for (int index = 0; index < this.m_TextFields.Length; ++index)
      {
        if (!string.IsNullOrWhiteSpace(this.m_TextFields[index].Text))
        {
          string str3 = Util.HttpEncode(this.m_TextFields[index].Text);
          if (!string.IsNullOrWhiteSpace(this.m_TextFields[index].Lang) && !this.m_TextFields[index].Lang.Equals(globalLang))
          {
            string str4 = Util.HttpEncode(this.m_TextFields[index].Lang);
            stringBuilder1.AppendFormat("<text id='{0}' lang='{1}'>{2}</text>", (object) (index + 1), (object) str4, (object) str3);
          }
          else
            stringBuilder1.AppendFormat("<text id='{0}'>{1}</text>", (object) (index + 1), (object) str3);
        }
      }
      return stringBuilder1.ToString();
    }

    public string TemplateName => this.m_TemplateName;

    public string FallbackName => this.m_FallbackName;
  }
}
