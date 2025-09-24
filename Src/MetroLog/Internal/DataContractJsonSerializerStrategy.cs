// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.DataContractJsonSerializerStrategy
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Internal.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

#nullable disable
namespace MetroLog.Internal
{
  public class DataContractJsonSerializerStrategy : PocoJsonSerializerStrategy
  {
    public DataContractJsonSerializerStrategy()
    {
      this.GetCache = (IDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>>) new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, ReflectionUtils.GetDelegate>>(((PocoJsonSerializerStrategy) this).GetterValueFactory));
      this.SetCache = (IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>) new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>(((PocoJsonSerializerStrategy) this).SetterValueFactory));
    }

    internal override IDictionary<string, ReflectionUtils.GetDelegate> GetterValueFactory(Type type)
    {
      if (ReflectionUtils.GetAttribute(type, typeof (DataContractAttribute)) == null)
        return base.GetterValueFactory(type);
      IDictionary<string, ReflectionUtils.GetDelegate> dictionary = (IDictionary<string, ReflectionUtils.GetDelegate>) new Dictionary<string, ReflectionUtils.GetDelegate>();
      string jsonKey;
      foreach (PropertyInfo property in ReflectionUtils.GetProperties(type))
      {
        if (!property.IsDefined(typeof (JsonIgnoreAttribute)) && property.CanRead && !ReflectionUtils.GetGetterMethodInfo(property).IsStatic && DataContractJsonSerializerStrategy.CanAdd((MemberInfo) property, out jsonKey))
          dictionary[jsonKey] = ReflectionUtils.GetGetMethod(property);
      }
      foreach (FieldInfo field in ReflectionUtils.GetFields(type))
      {
        if (!field.IsStatic && DataContractJsonSerializerStrategy.CanAdd((MemberInfo) field, out jsonKey))
          dictionary[jsonKey] = ReflectionUtils.GetGetMethod(field);
      }
      return dictionary;
    }

    internal override IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> SetterValueFactory(
      Type type)
    {
      if (ReflectionUtils.GetAttribute(type, typeof (DataContractAttribute)) == null)
        return base.SetterValueFactory(type);
      IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> dictionary = (IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>) new Dictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>();
      string jsonKey;
      foreach (PropertyInfo property in ReflectionUtils.GetProperties(type))
      {
        if (property.CanWrite && !ReflectionUtils.GetSetterMethodInfo(property).IsStatic && DataContractJsonSerializerStrategy.CanAdd((MemberInfo) property, out jsonKey))
          dictionary[jsonKey] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(property.PropertyType, ReflectionUtils.GetSetMethod(property));
      }
      foreach (FieldInfo field in ReflectionUtils.GetFields(type))
      {
        if (!field.IsInitOnly && !field.IsStatic && DataContractJsonSerializerStrategy.CanAdd((MemberInfo) field, out jsonKey))
          dictionary[jsonKey] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(field.FieldType, ReflectionUtils.GetSetMethod(field));
      }
      return dictionary;
    }

    private static bool CanAdd(MemberInfo info, out string jsonKey)
    {
      jsonKey = (string) null;
      if (ReflectionUtils.GetAttribute(info, typeof (IgnoreDataMemberAttribute)) != null)
        return false;
      DataMemberAttribute attribute = (DataMemberAttribute) ReflectionUtils.GetAttribute(info, typeof (DataMemberAttribute));
      if (attribute == null)
        return false;
      jsonKey = string.IsNullOrEmpty(attribute.Name) ? info.Name : attribute.Name;
      return true;
    }
  }
}
