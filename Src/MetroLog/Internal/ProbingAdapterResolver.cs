// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.ProbingAdapterResolver
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace MetroLog.Internal
{
  internal class ProbingAdapterResolver : IAdapterResolver
  {
    private readonly Func<AssemblyName, Assembly> _assemblyLoader;
    private readonly object _lock = new object();
    private readonly Dictionary<Type, object> _adapters = new Dictionary<Type, object>();
    private Assembly _assembly;

    public ProbingAdapterResolver()
      : this(new Func<AssemblyName, Assembly>(Assembly.Load))
    {
    }

    public ProbingAdapterResolver(Func<AssemblyName, Assembly> assemblyLoader)
    {
      this._assemblyLoader = assemblyLoader;
    }

    public object Resolve(Type type, object[] args)
    {
      lock (this._lock)
      {
        object obj;
        if (!this._adapters.TryGetValue(type, out obj))
        {
          obj = ProbingAdapterResolver.ResolveAdapter(this.GetPlatformSpecificAssembly(), type, args);
          this._adapters.Add(type, obj);
        }
        return obj;
      }
    }

    private static object ResolveAdapter(Assembly assembly, Type interfaceType, object[] args)
    {
      string name = ProbingAdapterResolver.MakeAdapterTypeName(interfaceType);
      try
      {
        Type type1 = assembly.GetType(name);
        if (type1 != null)
          return Activator.CreateInstance(type1);
        Type type2 = typeof (ProbingAdapterResolver).GetTypeInfo().Assembly.GetType(name);
        return type2 != null ? Activator.CreateInstance(type2, args) : (object) type2;
      }
      catch
      {
        return (object) null;
      }
    }

    private static string MakeAdapterTypeName(Type interfaceType)
    {
      return interfaceType.Namespace + "." + interfaceType.Name.Substring(1);
    }

    private Assembly GetPlatformSpecificAssembly()
    {
      if (this._assembly == null)
      {
        this._assembly = this.ProbeForPlatformSpecificAssembly();
        if (this._assembly == null)
          throw new InvalidOperationException(Strings.AssemblyNotSupported);
      }
      return this._assembly;
    }

    private Assembly ProbeForPlatformSpecificAssembly()
    {
      AssemblyName assemblyName = new AssemblyName(this.GetType().GetTypeInfo().Assembly.FullName);
      assemblyName.Name = "MetroLog.Platform";
      Assembly assembly = (Assembly) null;
     
       try
       {
          assembly = this._assemblyLoader(assemblyName);
       }
       catch (Exception ex1)
        {
            // Используем переменную
            var _ = ex1.Message; 
            assemblyName.SetPublicKey((byte[]) null);
            assemblyName.SetPublicKeyToken((byte[]) null);
        }
            
        try
        {
          assembly = this._assemblyLoader(assemblyName);
        }
        catch (Exception ex2)
        {
            // Используем переменную
            var _ = ex2.Message;
    
      }
      return assembly;
    }
  }
}
