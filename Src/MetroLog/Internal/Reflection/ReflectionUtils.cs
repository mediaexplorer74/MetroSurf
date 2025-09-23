// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.Reflection.ReflectionUtils
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace MetroLog.Internal.Reflection
{
  internal class ReflectionUtils
  {
    private static readonly object[] EmptyObjects = new object[0];

    public static Attribute GetAttribute(MemberInfo info, Type type)
    {
      return info == null || type == null || !info.IsDefined(type) ? (Attribute) null : info.GetCustomAttribute(type);
    }

    public static Attribute GetAttribute(Type objectType, Type attributeType)
    {
      return objectType == null || attributeType == null || !objectType.GetTypeInfo().IsDefined(attributeType) ? (Attribute) null : objectType.GetTypeInfo().GetCustomAttribute(attributeType);
    }

    public static Type[] GetGenericTypeArguments(Type type)
    {
      return type.GetTypeInfo().GenericTypeArguments;
    }

    public static bool IsTypeGenericeCollectionInterface(Type type)
    {
      if (!type.GetTypeInfo().IsGenericType)
        return false;
      Type genericTypeDefinition = type.GetGenericTypeDefinition();
      return genericTypeDefinition == typeof (IList<>) || genericTypeDefinition == typeof (ICollection<>) || genericTypeDefinition == typeof (IEnumerable<>);
    }

    public static bool IsAssignableFrom(Type type1, Type type2)
    {
      return type1.GetTypeInfo().IsAssignableFrom(type2.GetTypeInfo());
    }

    public static bool IsTypeDictionary(Type type)
    {
      if (typeof (IDictionary<,>).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
        return true;
      return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof (IDictionary<,>);
    }

    public static bool IsNullableType(Type type)
    {
      return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>);
    }

    public static object ToNullableType(object obj, Type nullableType)
    {
      return obj != null ? Convert.ChangeType(obj, Nullable.GetUnderlyingType(nullableType), (IFormatProvider) CultureInfo.InvariantCulture) : (object) null;
    }

    public static bool IsValueType(Type type) => type.GetTypeInfo().IsValueType;

    public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
    {
      return type.GetTypeInfo().DeclaredConstructors;
    }

    public static ConstructorInfo GetConstructorInfo(Type type, params Type[] argsType)
    {
      foreach (ConstructorInfo constructor in ReflectionUtils.GetConstructors(type))
      {
        ParameterInfo[] parameters = constructor.GetParameters();
        if (argsType.Length == parameters.Length)
        {
          int index = 0;
          bool flag = true;
          foreach (ParameterInfo parameter in constructor.GetParameters())
          {
            if (parameter.ParameterType != argsType[index])
            {
              flag = false;
              break;
            }
          }
          if (flag)
            return constructor;
        }
      }
      return (ConstructorInfo) null;
    }

    public static IEnumerable<PropertyInfo> GetProperties(Type type)
    {
      return type.GetTypeInfo().DeclaredProperties;
    }

    public static IEnumerable<FieldInfo> GetFields(Type type) => type.GetTypeInfo().DeclaredFields;

    public static MethodInfo GetGetterMethodInfo(PropertyInfo propertyInfo)
    {
      return propertyInfo.GetMethod;
    }

    public static MethodInfo GetSetterMethodInfo(PropertyInfo propertyInfo)
    {
      return propertyInfo.SetMethod;
    }

    public static ReflectionUtils.ConstructorDelegate GetContructor(ConstructorInfo constructorInfo)
    {
      return ReflectionUtils.GetConstructorByExpression(constructorInfo);
    }

    public static ReflectionUtils.ConstructorDelegate GetContructor(
      Type type,
      params Type[] argsType)
    {
      return ReflectionUtils.GetConstructorByExpression(type, argsType);
    }

    public static ReflectionUtils.ConstructorDelegate GetConstructorByReflection(
      ConstructorInfo constructorInfo)
    {
      return (ReflectionUtils.ConstructorDelegate) (args => constructorInfo.Invoke(args));
    }

    public static ReflectionUtils.ConstructorDelegate GetConstructorByReflection(
      Type type,
      params Type[] argsType)
    {
      ConstructorInfo constructorInfo = ReflectionUtils.GetConstructorInfo(type, argsType);
      return constructorInfo != null ? ReflectionUtils.GetConstructorByReflection(constructorInfo) : (ReflectionUtils.ConstructorDelegate) null;
    }

    public static ReflectionUtils.ConstructorDelegate GetConstructorByExpression(
      ConstructorInfo constructorInfo)
    {
      ParameterInfo[] parameters = constructorInfo.GetParameters();
      ParameterExpression array = Expression.Parameter(typeof (object[]), "args");
      Expression[] expressionArray = new Expression[parameters.Length];
      for (int index1 = 0; index1 < parameters.Length; ++index1)
      {
        Expression index2 = (Expression) Expression.Constant((object) index1);
        Type parameterType = parameters[index1].ParameterType;
        Expression expression = (Expression) Expression.Convert((Expression) Expression.ArrayIndex((Expression) array, index2), parameterType);
        expressionArray[index1] = expression;
      }
      Func<object[], object> compiledLambda = Expression.Lambda<Func<object[], object>>((Expression) Expression.New(constructorInfo, expressionArray), array).Compile();
      return (ReflectionUtils.ConstructorDelegate) (args => compiledLambda(args));
    }

    public static ReflectionUtils.ConstructorDelegate GetConstructorByExpression(
      Type type,
      params Type[] argsType)
    {
      ConstructorInfo constructorInfo = ReflectionUtils.GetConstructorInfo(type, argsType);
      return constructorInfo != null ? ReflectionUtils.GetConstructorByExpression(constructorInfo) : (ReflectionUtils.ConstructorDelegate) null;
    }

    public static ReflectionUtils.GetDelegate GetGetMethod(PropertyInfo propertyInfo)
    {
      return ReflectionUtils.GetGetMethodByExpression(propertyInfo);
    }

    public static ReflectionUtils.GetDelegate GetGetMethod(FieldInfo fieldInfo)
    {
      return ReflectionUtils.GetGetMethodByExpression(fieldInfo);
    }

    public static ReflectionUtils.GetDelegate GetGetMethodByReflection(PropertyInfo propertyInfo)
    {
      MethodInfo methodInfo = ReflectionUtils.GetGetterMethodInfo(propertyInfo);
      return (ReflectionUtils.GetDelegate) (source => methodInfo.Invoke(source, ReflectionUtils.EmptyObjects));
    }

    public static ReflectionUtils.GetDelegate GetGetMethodByReflection(FieldInfo fieldInfo)
    {
      return (ReflectionUtils.GetDelegate) (source => fieldInfo.GetValue(source));
    }

    public static ReflectionUtils.GetDelegate GetGetMethodByExpression(PropertyInfo propertyInfo)
    {
      MethodInfo getterMethodInfo = ReflectionUtils.GetGetterMethodInfo(propertyInfo);
      // create parameter expression for the instance
      ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "instance");
      // cast or convert instance to declaring type
      Expression instanceExpr = !ReflectionUtils.IsValueType(propertyInfo.DeclaringType)
        ? (Expression)Expression.TypeAs(parameterExpression, propertyInfo.DeclaringType)
        : (Expression)Expression.Convert(parameterExpression, propertyInfo.DeclaringType);
      // call getter
      Expression call = Expression.Call(instanceExpr, getterMethodInfo);
      // convert result to object
      Expression convertCall = Expression.Convert(call, typeof(object));
      Func<object, object> compiled = Expression.Lambda<Func<object, object>>(convertCall, parameterExpression).Compile();
      return (ReflectionUtils.GetDelegate)(source => compiled(source));
    }

    public static ReflectionUtils.GetDelegate GetGetMethodByExpression(FieldInfo fieldInfo)
    {
      // parameter for instance
      ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "instance");
      // build field access: ((DeclaringType)instance).field
      Expression instanceCast = Expression.Convert(parameterExpression, fieldInfo.DeclaringType);
      Expression fieldAccess = Expression.Field(instanceCast, fieldInfo);
      Expression convertField = Expression.Convert(fieldAccess, typeof(object));
      var lambda = Expression.Lambda<ReflectionUtils.GetDelegate>(convertField, parameterExpression).Compile();
      return (ReflectionUtils.GetDelegate)(source => lambda(source));
    }

    public static ReflectionUtils.SetDelegate GetSetMethod(PropertyInfo propertyInfo)
    {
      return ReflectionUtils.GetSetMethodByExpression(propertyInfo);
    }

    public static ReflectionUtils.SetDelegate GetSetMethod(FieldInfo fieldInfo)
    {
      return ReflectionUtils.GetSetMethodByExpression(fieldInfo);
    }

    public static ReflectionUtils.SetDelegate GetSetMethodByReflection(PropertyInfo propertyInfo)
    {
      MethodInfo methodInfo = ReflectionUtils.GetSetterMethodInfo(propertyInfo);
      return (ReflectionUtils.SetDelegate) ((source, value) => methodInfo.Invoke(source, new object[1]
      {
        value
      }));
    }

    public static ReflectionUtils.SetDelegate GetSetMethodByReflection(FieldInfo fieldInfo)
    {
      return (ReflectionUtils.SetDelegate) ((source, value) => fieldInfo.SetValue(source, value));
    }

    public static ReflectionUtils.SetDelegate GetSetMethodByExpression(PropertyInfo propertyInfo)
    {
      MethodInfo setterMethodInfo = ReflectionUtils.GetSetterMethodInfo(propertyInfo);
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (object), "instance");
      ParameterExpression parameterExpression2 = Expression.Parameter(typeof (object), "value");
      UnaryExpression instance = !ReflectionUtils.IsValueType(propertyInfo.DeclaringType) ? Expression.TypeAs((Expression) parameterExpression1, propertyInfo.DeclaringType) : Expression.Convert((Expression) parameterExpression1, propertyInfo.DeclaringType);
      UnaryExpression unaryExpression = !ReflectionUtils.IsValueType(propertyInfo.PropertyType) ? Expression.TypeAs((Expression) parameterExpression2, propertyInfo.PropertyType) : Expression.Convert((Expression) parameterExpression2, propertyInfo.PropertyType);
      Action<object, object> compiled = Expression.Lambda<Action<object, object>>((Expression) Expression.Call((Expression) instance, setterMethodInfo, (Expression) unaryExpression), parameterExpression1, parameterExpression2).Compile();
      return (ReflectionUtils.SetDelegate) ((source, val) => compiled(source, val));
    }

    public static ReflectionUtils.SetDelegate GetSetMethodByExpression(FieldInfo fieldInfo)
    {
      // parameters
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof(object), "instance");
      ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "value");
      // build assignment: ((DeclaringType)instance).field = (FieldType)value
      Expression left = Expression.Field(Expression.Convert(parameterExpression1, fieldInfo.DeclaringType), fieldInfo);
      Expression right = Expression.Convert(parameterExpression2, fieldInfo.FieldType);
      var assign = Expression.Assign(left, right);
      Action<object, object> compiled = Expression.Lambda<Action<object, object>>(assign, parameterExpression1, parameterExpression2).Compile();
      return (ReflectionUtils.SetDelegate)((source, val) => compiled(source, val));
    }

    public static BinaryExpression Assign(Expression left, Expression right)
    {
      return Expression.Assign(left, right);
    }

    public delegate object GetDelegate(object source);

    public delegate void SetDelegate(object source, object value);

    public delegate object ConstructorDelegate(params object[] args);

    public delegate TValue ThreadSafeDictionaryValueFactory<TKey, TValue>(TKey key);

    private static class Assigner<T>
    {
      public static T Assign(ref T left, T right) => left = right;
    }

    public sealed class ThreadSafeDictionary<TKey, TValue> : 
      IDictionary<TKey, TValue>,
      ICollection<KeyValuePair<TKey, TValue>>,
      IEnumerable<KeyValuePair<TKey, TValue>>,
      IEnumerable
    {
      private readonly object _lock = new object();
      private readonly ReflectionUtils.ThreadSafeDictionaryValueFactory<TKey, TValue> _valueFactory;
      private Dictionary<TKey, TValue> _dictionary;

      public ThreadSafeDictionary(
        ReflectionUtils.ThreadSafeDictionaryValueFactory<TKey, TValue> valueFactory)
      {
        this._valueFactory = valueFactory;
      }

      private TValue Get(TKey key)
      {
        TValue obj;
        return this._dictionary == null || !this._dictionary.TryGetValue(key, out obj) ? this.AddValue(key) : obj;
      }

      private TValue AddValue(TKey key)
      {
        TValue obj1 = this._valueFactory(key);
        lock (this._lock)
        {
          if (this._dictionary == null)
          {
            this._dictionary = new Dictionary<TKey, TValue>();
            this._dictionary[key] = obj1;
          }
          else
          {
            TValue obj2;
            if (this._dictionary.TryGetValue(key, out obj2))
              return obj2;
            this._dictionary = new Dictionary<TKey, TValue>((IDictionary<TKey, TValue>) this._dictionary)
            {
              [key] = obj1
            };
          }
        }
        return obj1;
      }

      public void Add(TKey key, TValue value) => throw new NotImplementedException();

      public bool ContainsKey(TKey key) => this._dictionary.ContainsKey(key);

      public ICollection<TKey> Keys => (ICollection<TKey>) this._dictionary.Keys;

      public bool Remove(TKey key) => throw new NotImplementedException();

      public bool TryGetValue(TKey key, out TValue value)
      {
        value = this[key];
        return true;
      }

      public ICollection<TValue> Values => (ICollection<TValue>) this._dictionary.Values;

      public TValue this[TKey key]
      {
        get => this.Get(key);
        set => throw new NotImplementedException();
      }

      public void Add(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();

      public void Clear() => throw new NotImplementedException();

      public bool Contains(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();

      public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
      {
        throw new NotImplementedException();
      }

      public int Count => this._dictionary.Count;

      public bool IsReadOnly => throw new NotImplementedException();

      public bool Remove(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();

      public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
      {
        return (IEnumerator<KeyValuePair<TKey, TValue>>) this._dictionary.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._dictionary.GetEnumerator();
    }
  }
}
