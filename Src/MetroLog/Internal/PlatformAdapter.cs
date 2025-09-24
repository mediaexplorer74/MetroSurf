// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.PlatformAdapter
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;

#nullable disable
namespace MetroLog.Internal
{
  public static class PlatformAdapter
  {
    private static IAdapterResolver _resolver = (IAdapterResolver) new ProbingAdapterResolver();

    public static T Resolve<T>(bool throwIfNotFound = true, params object[] args)
    {
      T obj = (T) PlatformAdapter._resolver.Resolve(typeof (T), args);
      return !((object) obj == null & throwIfNotFound) ? obj : throw new PlatformNotSupportedException(Strings.AdapterNotSupported);
    }

    internal static void SetResolver(IAdapterResolver resolver)
    {
      PlatformAdapter._resolver = resolver;
    }
  }
}
