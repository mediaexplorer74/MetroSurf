// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.ImmediateSourceUpdate
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace MetroLab.Common
{
  public class ImmediateSourceUpdate : DependencyObject
  {
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof (bool), typeof (ImmediateSourceUpdate), new PropertyMetadata((object) false, new PropertyChangedCallback(ImmediateSourceUpdate.OnIsEnabledChanged)));
    public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached("Source", typeof (string), typeof (ImmediateSourceUpdate), new PropertyMetadata((object) null));

    public static bool GetIsEnabled(DependencyObject obj)
    {
      return (bool) obj.GetValue(ImmediateSourceUpdate.IsEnabledProperty);
    }

    public static void SetIsEnabled(DependencyObject obj, bool value)
    {
      obj.SetValue(ImmediateSourceUpdate.IsEnabledProperty, (object) value);
    }

    private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is TextBox textBox1))
        return;
      if ((bool) e.NewValue)
      {
        TextBox textBox2 = textBox1;
        // subscribe normally
        textBox2.TextChanged += ImmediateSourceUpdate.txt_TextChanged;
      }
      else
      {
        // unsubscribe normally
        textBox1.TextChanged -= ImmediateSourceUpdate.txt_TextChanged;
      }
    }

    public static string GetSource(DependencyObject d)
    {
      return (string) d.GetValue(ImmediateSourceUpdate.SourceProperty);
    }

    public static void SetSource(DependencyObject d, string value)
    {
      d.SetValue(ImmediateSourceUpdate.SourceProperty, (object) value);
    }

    private static void txt_TextChanged(object sender, TextChangedEventArgs e)
    {
      TextBox textBox = sender as TextBox;
      ((DependencyObject) textBox).SetValue(ImmediateSourceUpdate.SourceProperty, (object) textBox.Text);
    }
  }
}
