// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.PocoJsonSerializerStrategy
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Internal.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

#nullable disable
namespace MetroLog.Internal
{
  internal class PocoJsonSerializerStrategy : IJsonSerializerStrategy
  {
    internal IDictionary<Type, ReflectionUtils.ConstructorDelegate> ConstructorCache;
    internal IDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>> GetCache;
    internal IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>> SetCache;
    internal static readonly Type[] EmptyTypes = new Type[0];
    internal static readonly Type[] ArrayConstructorParameterTypes = new Type[1]
    {
      typeof (int)
    };
    private static readonly string[] Iso8601Format = new string[3]
    {
      "yyyy-MM-dd\\THH:mm:ss.FFFFFFF\\Z",
      "yyyy-MM-dd\\THH:mm:ss\\Z",
      "yyyy-MM-dd\\THH:mm:ssK"
    };

    public PocoJsonSerializerStrategy()
    {
      this.ConstructorCache = (IDictionary<Type, ReflectionUtils.ConstructorDelegate>) new ReflectionUtils.ThreadSafeDictionary<Type, ReflectionUtils.ConstructorDelegate>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, ReflectionUtils.ConstructorDelegate>(this.ContructorDelegateFactory));
      this.GetCache = (IDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>>) new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, ReflectionUtils.GetDelegate>>(this.GetterValueFactory));
      this.SetCache = (IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>) new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>(this.SetterValueFactory));
    }

    internal virtual ReflectionUtils.ConstructorDelegate ContructorDelegateFactory(Type key)
    {
      Type type = key;
      return ReflectionUtils.GetContructor(type, type.IsArray ? PocoJsonSerializerStrategy.ArrayConstructorParameterTypes : PocoJsonSerializerStrategy.EmptyTypes);
    }

    internal virtual IDictionary<string, ReflectionUtils.GetDelegate> GetterValueFactory(Type type)
    {
      IDictionary<string, ReflectionUtils.GetDelegate> dictionary = (IDictionary<string, ReflectionUtils.GetDelegate>) new Dictionary<string, ReflectionUtils.GetDelegate>();
      foreach (PropertyInfo property in ReflectionUtils.GetProperties(type))
      {
        if (property.CanRead)
        {
          MethodInfo getterMethodInfo = ReflectionUtils.GetGetterMethodInfo(property);
          if (!getterMethodInfo.IsStatic && getterMethodInfo.IsPublic)
            dictionary[property.Name] = ReflectionUtils.GetGetMethod(property);
        }
      }
      foreach (FieldInfo field in ReflectionUtils.GetFields(type))
      {
        if (!field.IsStatic && field.IsPublic)
          dictionary[field.Name] = ReflectionUtils.GetGetMethod(field);
      }
      return dictionary;
    }

    internal virtual IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> SetterValueFactory(
      Type type)
    {
      IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> dictionary = (IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>) new Dictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>();
      foreach (PropertyInfo property in ReflectionUtils.GetProperties(type))
      {
        if (property.CanWrite)
        {
          MethodInfo setterMethodInfo = ReflectionUtils.GetSetterMethodInfo(property);
          if (!setterMethodInfo.IsStatic && setterMethodInfo.IsPublic)
            dictionary[property.Name] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(property.PropertyType, ReflectionUtils.GetSetMethod(property));
        }
      }
      foreach (FieldInfo field in ReflectionUtils.GetFields(type))
      {
        if (!field.IsInitOnly && !field.IsStatic && field.IsPublic)
          dictionary[field.Name] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(field.FieldType, ReflectionUtils.GetSetMethod(field));
      }
      return dictionary;
    }

    public virtual bool SerializeNonPrimitiveObject(object input, out object output)
    {
      return this.TrySerializeKnownTypes(input, out output) || this.TrySerializeUnknownTypes(input, out output);
    }

    public virtual object DeserializeObject(object value, Type type)
    {
      object source = (object) null;
      object obj1;
      switch (value)
      {
        case string _:
          string str = value as string;
          obj1 = string.IsNullOrEmpty(str) ? (type != typeof (Guid) ? (!ReflectionUtils.IsNullableType(type) || Nullable.GetUnderlyingType(type) != typeof (Guid) ? (object) str : (object) null) : (object) new Guid()) : (type == typeof (DateTime) || ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (DateTime) ? (object) DateTime.ParseExact(str, PocoJsonSerializerStrategy.Iso8601Format, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal) : (type == typeof (Guid) || ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (Guid) ? (object) new Guid(str) : (object) str));
          break;
        case bool _:
          obj1 = value;
          break;
        case null:
          obj1 = (object) null;
          break;
        case long _ when type == typeof (long):
        case double _ when type == typeof (double):
          obj1 = value;
          break;
        case double _ when type != typeof (double):
        case long _ when type != typeof (long):
          obj1 = type == typeof (int) || type == typeof (long) || type == typeof (double) || type == typeof (float) || type == typeof (bool) || type == typeof (Decimal) || type == typeof (byte) || type == typeof (short) ? Convert.ChangeType(value, type, (IFormatProvider) CultureInfo.InvariantCulture) : value;
          break;
        case IDictionary<string, object> _:
          IDictionary<string, object> dictionary1 = (IDictionary<string, object>) value;
          if (ReflectionUtils.IsTypeDictionary(type))
          {
            Type[] genericTypeArguments = ReflectionUtils.GetGenericTypeArguments(type);
            Type type1 = genericTypeArguments[0];
            Type type2 = genericTypeArguments[1];
            IDictionary dictionary2 = (IDictionary) this.ConstructorCache[typeof (Dictionary<,>).MakeGenericType(type1, type2)]();
            foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary1)
              dictionary2.Add((object) keyValuePair.Key, this.DeserializeObject(keyValuePair.Value, type2));
            source = (object) dictionary2;
            goto default;
          }
          else if (type == typeof (object))
          {
            source = value;
            goto default;
          }
          else
          {
            source = this.ConstructorCache[type]();
            using (IEnumerator<KeyValuePair<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>> enumerator = this.SetCache[type].GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                KeyValuePair<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> current = enumerator.Current;
                object obj2;
                if (dictionary1.TryGetValue(current.Key, out obj2))
                {
                  obj2 = this.DeserializeObject(obj2, current.Value.Key);
                  current.Value.Value(source, obj2);
                }
              }
              goto default;
            }
          }
        case IList<object> _:
          IList<object> objectList = (IList<object>) value;
          IList list = (IList) null;
          if (type.IsArray)
          {
            list = (IList) this.ConstructorCache[type]((object) objectList.Count);
            int num = 0;
            foreach (object obj3 in (IEnumerable<object>) objectList)
              list[num++] = this.DeserializeObject(obj3, type.GetElementType());
          }
          else if (ReflectionUtils.IsTypeGenericeCollectionInterface(type) || ReflectionUtils.IsAssignableFrom(typeof (IList), type))
          {
            Type genericTypeArgument = ReflectionUtils.GetGenericTypeArguments(type)[0];
            list = (IList) this.ConstructorCache[typeof (List<>).MakeGenericType(genericTypeArgument)]((object) objectList.Count);
            foreach (object obj4 in (IEnumerable<object>) objectList)
              list.Add(this.DeserializeObject(obj4, genericTypeArgument));
          }
          source = (object) list;
          goto default;
        default:
          return source;
      }
      if (ReflectionUtils.IsNullableType(type))
        return ReflectionUtils.ToNullableType(obj1, type);
      return obj1 == null && type == typeof (Guid) ? (object) new Guid() : obj1;
    }

    protected virtual object SerializeEnum(Enum p) => (object) p.ToString();

    protected virtual bool TrySerializeKnownTypes(object input, out object output)
    {
      output = null; // ensure out param is always assigned
      bool flag = true;
      switch (input)
      {
        case DateTime dateTime2:
          ref object local1 = ref output;
          DateTime dateTime1 = dateTime2;
          dateTime1 = dateTime1.ToUniversalTime();
          string str1 = dateTime1.ToString(PocoJsonSerializerStrategy.Iso8601Format[0], (IFormatProvider) CultureInfo.InvariantCulture);
          local1 = (object) str1;
          break;
        case DateTimeOffset dateTimeOffset2:
          ref object local2 = ref output;
          DateTimeOffset dateTimeOffset1 = dateTimeOffset2;
          dateTimeOffset1 = dateTimeOffset1.ToUniversalTime();
          string str2 = dateTimeOffset1.ToString(PocoJsonSerializerStrategy.Iso8601Format[0], (IFormatProvider) CultureInfo.InvariantCulture);
          local2 = (object) str2;
          break;
        case Guid guid:
          output = (object) guid.ToString("D");
          break;
        default:
          if ((object) (input as Uri) != null)
          {
            output = (object) input.ToString();
            break;
          }
          if (input is Enum)
          {
            output = this.SerializeEnum((Enum) input);
            break;
          }
          flag = false;
          output = (object) null;
          break;
      }
      return flag;
    }

    protected virtual bool TrySerializeUnknownTypes(object input, out object output)
    {
      output = (object) null;
      Type type = input.GetType();
      if (type.FullName == null)
        return false;
      IDictionary<string, object> dictionary = (IDictionary<string, object>) new JsonObject();
      foreach (KeyValuePair<string, ReflectionUtils.GetDelegate> keyValuePair in (IEnumerable<KeyValuePair<string, ReflectionUtils.GetDelegate>>) this.GetCache[type])
      {
        if (keyValuePair.Value != null)
          dictionary.Add(keyValuePair.Key, keyValuePair.Value(input));
      }
      output = (object) dictionary;
      return true;
    }
  }
}
