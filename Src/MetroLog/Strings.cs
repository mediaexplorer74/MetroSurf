// Decompiled with JetBrains decompiler
// Type: MetroLog.Strings
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace MetroLog
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Strings
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Strings()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (Strings.resourceMan == null)
          Strings.resourceMan = new ResourceManager("MetroLog.Strings", typeof (Strings).GetTypeInfo().Assembly);
        return Strings.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Strings.resourceCulture;
      set => Strings.resourceCulture = value;
    }

    internal static string AdapterNotSupported
    {
      get
      {
        return Strings.ResourceManager.GetString(nameof (AdapterNotSupported), Strings.resourceCulture);
      }
    }

    internal static string AssemblyNotSupported
    {
      get
      {
        return Strings.ResourceManager.GetString(nameof (AssemblyNotSupported), Strings.resourceCulture);
      }
    }
  }
}
