// Decompiled with JetBrains decompiler
// Type: VPN.View.Controls.ValidationTextBoxUpdateSourceTextHelper
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace VPN.View.Controls
{
  public class ValidationTextBoxUpdateSourceTextHelper : FrameworkElement
  {
    public static readonly DependencyProperty UpdateSourceTextProperty = DependencyProperty.RegisterAttached("UpdateSourceText", typeof (string), typeof (ValidationTextBoxUpdateSourceTextHelper), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof (bool), typeof (ValidationTextBoxUpdateSourceTextHelper), new PropertyMetadata((object) false, new PropertyChangedCallback(ValidationTextBoxUpdateSourceTextHelper.IsEnabledChangedCallback)));

    public static string GetUpdateSourceText(DependencyObject obj)
    {
      return (string) obj.GetValue(ValidationTextBoxUpdateSourceTextHelper.UpdateSourceTextProperty);
    }

    public static void SetUpdateSourceText(DependencyObject obj, string value)
    {
      obj.SetValue(ValidationTextBoxUpdateSourceTextHelper.UpdateSourceTextProperty, (object) value);
    }

    public static bool GetIsEnabled(DependencyObject obj)
    {
      return (bool) obj.GetValue(ValidationTextBoxUpdateSourceTextHelper.IsEnabledProperty);
    }

    public static void SetIsEnabled(DependencyObject obj, bool value)
    {
      obj.SetValue(ValidationTextBoxUpdateSourceTextHelper.IsEnabledProperty, (object) value);
    }

    private static void IsEnabledChangedCallback(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
      if (!(sender is TextBox textBox1))
        return;
      if ((bool) args.NewValue)
      {
        TextBox textBox2 = textBox1;
        WindowsRuntimeMarshal.AddEventHandler<TextChangedEventHandler>(new Func<TextChangedEventHandler, EventRegistrationToken>(textBox2.add_TextChanged), new Action<EventRegistrationToken>(textBox2.remove_TextChanged), new TextChangedEventHandler(ValidationTextBoxUpdateSourceTextHelper.AttachedTextBoxTextChanged));
      }
      else
        WindowsRuntimeMarshal.RemoveEventHandler<TextChangedEventHandler>(new Action<EventRegistrationToken>(textBox1.remove_TextChanged), new TextChangedEventHandler(ValidationTextBoxUpdateSourceTextHelper.AttachedTextBoxTextChanged));
    }

    private static void AttachedTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
      if (!(sender is TextBox))
        return;
      TextBox textBox = (TextBox) sender;
      ((DependencyObject) textBox).SetValue(ValidationTextBoxUpdateSourceTextHelper.UpdateSourceTextProperty, (object) textBox.Text);
    }
  }
}
