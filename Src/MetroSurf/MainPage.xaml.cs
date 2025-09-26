// Decompiled with JetBrains decompiler
// Type: MetroSurf_8._1.MainPage
// Assembly: MetroSurf 8.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2E5FE69-3186-4CAE-91FC-629F72538042
// Assembly location: C:\Users\Admin\Desktop\RE\MetroSurf\MetroSurf 8.1.dll


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Popups;
using Windows.System;
//using System.Net.Http;
using System.Threading.Tasks;
using Windows.Web.Http;


namespace MetroSurf_8._1
{
    public sealed partial class MainPage : Page
    {
        private string pendingUrl;
        private string htmlToInject;
        private bool isLoadingCustomContent;
        private string currentLoadedUrl = "";
      
       
       
        public MainPage()
        {
            this.InitializeComponent();
            this.LumiaBrowserX.NavigateToString("\r\n<html>\r\n<head>\r\n    <meta name='viewport' content='width=device-width, initial-scale=1.0'>\r\n    <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>\r\n    <style>\r\n        body {\r\n            padding: 10px;\r\n            font-family: 'Segoe UI', Tahoma, sans-serif;\r\n            font-size: 20px;\r\n            line-height: 1.5;\r\n            background-color: #ffffff;\r\n            color: #000000;\r\n        }\r\n        h2 {\r\n            font-size: 26px;\r\n            margin-bottom: 12px;\r\n        }\r\n        p {\r\n            margin-bottom: 16px;\r\n        }\r\n        strong {\r\n            font-weight: bold;\r\n            color: red;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <h2>Welcome to MetroSurf</h2>\r\n    <p>This is an early alpha release of MetroSurf. Please note that bugs and unexpected behavior may occur.</p>\r\n<p>To get started, simply enter a URL into the address bar.</p>\r\n</body>\r\n</html>");
        }


        private async void AddressBar_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter)
                return;
            string url = this.AddressBar.Text.Trim();
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;
            e.Handled = true;
            await this.LoadCustomContent(url);
        }

        private async Task LoadCustomContent(string url)
        {
            this.pendingUrl = url;
            this.isLoadingCustomContent = true;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Linux; Android 8.1.0; Pixel 2 Build/OPM1.171019.011) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.3325.181 Mobile Safari/537.36");
                    httpClient.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                    httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
                    var response = await httpClient.GetAsync(new Uri(this.pendingUrl));
                    response.EnsureSuccessStatusCode();
                    this.htmlToInject = await response.Content.ReadAsStringAsync();
                }
                this.LumiaBrowserX.Navigate(new Uri(this.pendingUrl));
            }
            catch (Exception ex)
            {
                await new MessageDialog("Download failed: " + ex.Message).ShowAsync();
                this.isLoadingCustomContent = false;
            }
        }

        private async void LumiaBrowserX_NavigationStarting(object sender, WebViewNavigationStartingEventArgs e)
        {
            if (this.isLoadingCustomContent || !this.IsNewPageNavigation(e.Uri?.ToString() ?? ""))
                return;
            e.Cancel = true;
            await this.LoadCustomContent(e.Uri.ToString());
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage));
        }

        private bool IsNewPageNavigation(string uriString1)
        {
            // string uriString1 = e.Uri.ToString();
            string uriString2 = this.currentLoadedUrl ?? "";
            if (uriString1.StartsWith("javascript:") || uriString1.StartsWith("about:") || uriString1.StartsWith("data:") || uriString1.StartsWith("mailto:") || uriString1.StartsWith("tel:") || !uriString1.StartsWith("http://") && !uriString1.StartsWith("https://"))
                return false;
            try
            {
                Uri uri1 = new Uri(uriString1);
                if (!string.IsNullOrEmpty(uriString2))
                {
                    Uri uri2 = new Uri(uriString2);
                    if (uri1.Host.Equals(uri2.Host, StringComparison.OrdinalIgnoreCase) && uri1.AbsolutePath.Equals(uri2.AbsolutePath, StringComparison.OrdinalIgnoreCase))
                        return false;
                    if (uri1.Host.Equals(uri2.Host, StringComparison.OrdinalIgnoreCase))
                    {
                        string lower1 = uri1.AbsolutePath.TrimEnd('/').ToLower();
                        string lower2 = uri2.AbsolutePath.TrimEnd('/').ToLower();
                        if (lower1 == lower2 || Math.Abs(lower1.Length - lower2.Length) <= 1 || uriString1.ToLower().Contains("redirect") || uriString1.ToLower().Contains("login") || uriString1.ToLower().Contains("auth") || uriString1.ToLower().Contains("oauth") || uriString1.ToLower().Contains("signin"))
                            return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*private void WebClient_DownloadStringCompleted(
          object sender,
          EventArgs e)
        {
            if (e.Error != null || string.IsNullOrEmpty(e.Result))
            {
                int num = (int)MessageBox.Show("Failed to fetch page content: " + (e.Error?.Message ?? "Unknown error"));
                this.isLoadingCustomContent = false;
            }
            else
            {
                this.htmlToInject = e.Result;
                try
                {
                    this.LumiaBrowserX.Navigate(new Uri(this.pendingUrl));
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show("Navigation error: " + ex.Message);
                    this.isLoadingCustomContent = false;
                }
            }
        }*/


        private async void LumiaBrowserX_NavigationCompleted(object sender, WebViewNavigationCompletedEventArgs e)
        {
            if (this.htmlToInject == null)
                return;
            this.AddressBar.Text = e.Uri.ToString();
            this.currentLoadedUrl = e.Uri.ToString();
            string htmlToInject = this.htmlToInject;
            List<MainPage.ScriptInfo> scripts = this.ExtractScripts(htmlToInject);
            List<MainPage.LinkInfo> links = this.ExtractLinks(htmlToInject);
            string str = this.EscapeForJS(this.StripScriptsAndLinks(htmlToInject));
            try
            {
                await this.LumiaBrowserX.InvokeScriptAsync("eval", new[]
                {
                    string.Format("\n    try {{\n        document.documentElement.outerHTML = '<html>' + '{0}' + '</html>';\n    }} catch(e) {{\n        // Fallback to creating new document\n        var newDoc = document.implementation.createHTMLDocument('');\n        newDoc.documentElement.innerHTML = '{1}';\n        document.replaceChild(document.importNode(newDoc.documentElement, true), document.documentElement);\n    }}\n", (object)str, (object)str)
                });

                foreach (MainPage.LinkInfo linkInfo in links)
                {
                    await this.LumiaBrowserX.InvokeScriptAsync("eval", new[]
                    {
                        string.Format("\r\nvar link = document.createElement('link');\r\n{0}\r\nif (document.head) document.head.appendChild(link);\r\n", (object)string.Join("\n", linkInfo.Attributes.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>)(attr => string.Format("link.setAttribute('{0}', '{1}');", (object)attr.Key, (object)this.EscapeForJS(attr.Value))))))
                    });
                }
                
                foreach (MainPage.ScriptInfo scriptInfo in scripts)
                {
                    await this.LumiaBrowserX.InvokeScriptAsync("eval", new[]
                    {
                        string.Format("\r\nvar script = document.createElement('script');\r\n{0}\r\n{1}\r\nif (document.head) document.head.appendChild(script);\r\n", (object)string.Join("\n", scriptInfo.Attributes.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>)(attr => string.Format("script.setAttribute('{0}', '{1}');", (object)attr.Key, (object)this.EscapeForJS(attr.Value))))), string.IsNullOrEmpty(scriptInfo.InnerText) ? (object)"" : (object)string.Format("script.text = '{0}';", (object)this.EscapeForJS(scriptInfo.InnerText)))
                    });
                }
            }
            catch (Exception ex)
            {
                //int num = (int)MessageBox.Show("Script injection error: " + ex.Message);
                Debug.WriteLine("Script injection error: " + ex.Message);
            }
            this.htmlToInject = (string)null;
            this.isLoadingCustomContent = false;
        }

        private List<MainPage.ScriptInfo> ExtractScripts(string html)
        {
            List<MainPage.ScriptInfo> scripts = new List<MainPage.ScriptInfo>();
            string pattern = "<script([^>]*)>(.*?)</script>|<script([^>]*)/>";
            foreach (Match match in Regex.Matches(html, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline))
            {
                MainPage.ScriptInfo scriptInfo = new MainPage.ScriptInfo();
                string attributeString = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[3].Value;
                scriptInfo.Attributes = this.ParseAttributes(attributeString);
                if (match.Groups[2].Success)
                    scriptInfo.InnerText = match.Groups[2].Value.Trim();
                scripts.Add(scriptInfo);
            }
            return scripts;
        }

        private List<MainPage.LinkInfo> ExtractLinks(string html)
        {
            List<MainPage.LinkInfo> links = new List<MainPage.LinkInfo>();
            string pattern = "<link([^>]*)/?>";
            foreach (Match match in Regex.Matches(html, pattern, RegexOptions.IgnoreCase))
                links.Add(new MainPage.LinkInfo()
                {
                    Attributes = this.ParseAttributes(match.Groups[1].Value)
                });
            return links;
        }

        private Dictionary<string, string> ParseAttributes(string attributeString)
        {
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            string pattern = "(\\w+)\\s*=\\s*[\"']([^\"']*)[\"']";
            foreach (Match match in Regex.Matches(attributeString, pattern))
            {
                string lower = match.Groups[1].Value.ToLower();
                string str = match.Groups[2].Value;
                attributes[lower] = str;
            }
            return attributes;
        }

        private string StripScriptsAndLinks(string html)
        {
            html = Regex.Replace(html, "<script[^>]*>.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            html = Regex.Replace(html, "<script[^>]*/?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<link[^>]*/?>", "", RegexOptions.IgnoreCase);
            return html;
        }

        private string EscapeForJS(string jsCode)
        {
            if (string.IsNullOrEmpty(jsCode))
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder(jsCode.Length * 3);
            foreach (char ch in jsCode)
            {
                switch (ch)
                {
                    case '\b':
                        stringBuilder.Append("\\b");
                        break;
                    case '\t':
                        stringBuilder.Append("\\t");
                        break;
                    case '\n':
                        stringBuilder.Append("\\n");
                        break;
                    case '\f':
                        stringBuilder.Append("\\f");
                        break;
                    case '\r':
                        stringBuilder.Append("\\r");
                        break;
                    case '"':
                        stringBuilder.Append("\\\"");
                        break;
                    case '\'':
                        stringBuilder.Append("\\'");
                        break;
                    case '\\':
                        stringBuilder.Append("\\\\");
                        break;
                    default:
                        if (ch >= 'a' && ch <= 'z' || ch >= 'A' && ch <= 'Z' || ch >= '0' && ch <= '9' || " .,;()[]{}=+-*!?_".IndexOf(ch) >= 0)
                        {
                            stringBuilder.Append(ch);
                            break;
                        }
                        stringBuilder.AppendFormat("\\u{0:X4}", (object)(int)ch);
                        break;
                }
            }
            return stringBuilder.ToString();
        }

        
        public class ScriptInfo
        {
            public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

            public string InnerText { get; set; } = "";
        }

        public class LinkInfo
        {
            public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
        }
    }
}
