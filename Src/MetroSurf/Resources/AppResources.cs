// Decompiled with JetBrains decompiler
// Type: MetroSurf_8._1.Resources.AppResources
// Assembly: MetroSurf 8.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2E5FE69-3186-4CAE-91FC-629F72538042
// Assembly location: C:\Users\Admin\Desktop\RE\MetroSurf\MetroSurf 8.1.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MetroSurf_8._1.Resources
{
  //[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  //[DebuggerNonUserCode]
  //[CompilerGenerated]
  public class AppResources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal AppResources()
    {
    }

        public static ResourceManager ResourceManager
        {
            get
            {
                if (AppResources.resourceMan == null)
                    AppResources.resourceMan = new ResourceManager("MetroSurf_8._1.Resources.AppResources",
                    typeof(AppResources).GetTypeInfo().Assembly);
                return AppResources.resourceMan;
            }
        }

        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
    {
      get => AppResources.resourceCulture;
      set => AppResources.resourceCulture = value;
    }

    public static string ResourceFlowDirection
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (ResourceFlowDirection), 
        AppResources.resourceCulture);
      }
    }

    public static string ResourceLanguage
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (ResourceLanguage), 
        AppResources.resourceCulture);
      }
    }

    public static string ApplicationTitle
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (ApplicationTitle), 
        AppResources.resourceCulture);
      }
    }

    public static string AppBarButtonText
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (AppBarButtonText), 
        AppResources.resourceCulture);
      }
    }

    public static string AppBarMenuItemText
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (AppBarMenuItemText), 
        AppResources.resourceCulture);
      }
    }
  }
}
