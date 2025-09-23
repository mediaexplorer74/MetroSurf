// Decompiled with JetBrains decompiler
// Type: VPN.View.Controls.Sector
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using VPN.Localization;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace VPN.View.Controls
{
  public sealed class Sector : UserControl, INotifyPropertyChanged, IComponentConnector
  {
    private const int radius = 30;
    public static readonly DependencyProperty PercentProperty = DependencyProperty.Register(nameof (Percent), typeof (int), typeof (Sector), new PropertyMetadata((object) 0, new PropertyChangedCallback(Sector.OnSomePropertyChnaged)));
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid LayoutRootGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public Sector()
    {
      this.InitializeComponent();
      (this.Content as FrameworkElement).put_DataContext((object) this);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    public int Percent
    {
      get => (int) ((DependencyObject) this).GetValue(Sector.PercentProperty);
      set => ((DependencyObject) this).SetValue(Sector.PercentProperty, (object) value);
    }

    public static void OnSomePropertyChnaged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Sector sector))
        return;
      sector.OnPropertyChanged("PathPercent");
      sector.OnPropertyChanged("PathRestPart");
      sector.OnPropertyChanged("PercentString");
      sector.OnPropertyChanged("PercentStringFontSize");
    }

    public PathGeometry PathPercent
    {
      get
      {
        if (this.Percent > 0 && this.Percent < 100)
        {
          PathFigure pathFigure = new PathFigure();
          pathFigure.put_StartPoint(new Point(30.0, 30.0));
          LineSegment lineSegment = new LineSegment();
          lineSegment.put_Point(new Point(30.0 * (1.0 + Math.Sin(6.2831853071795862 * (double) this.Percent / 100.0)), 30.0 * (1.0 - Math.Cos(6.2831853071795862 * (double) this.Percent / 100.0))));
          ArcSegment arcSegment = new ArcSegment();
          arcSegment.put_Point(new Point(30.0, 0.0));
          arcSegment.put_Size(new Size(30.0, 30.0));
          arcSegment.put_IsLargeArc(this.Percent < 50);
          arcSegment.put_SweepDirection((SweepDirection) 1);
          PathSegmentCollection segmentCollection = new PathSegmentCollection();
          ((ICollection<PathSegment>) segmentCollection).Add((PathSegment) lineSegment);
          ((ICollection<PathSegment>) segmentCollection).Add((PathSegment) arcSegment);
          pathFigure.put_Segments(segmentCollection);
          PathFigureCollection figureCollection = new PathFigureCollection();
          ((ICollection<PathFigure>) figureCollection).Add(pathFigure);
          PathGeometry pathPercent = new PathGeometry();
          pathPercent.put_Figures(figureCollection);
          return pathPercent;
        }
        PathFigure pathFigure1 = new PathFigure();
        pathFigure1.put_StartPoint(new Point(30.0, 0.0));
        ArcSegment arcSegment1 = new ArcSegment();
        arcSegment1.put_Point(new Point(30.0, 60.0));
        arcSegment1.put_Size(new Size(30.0, 30.0));
        ArcSegment arcSegment2 = new ArcSegment();
        arcSegment2.put_Point(new Point(30.0, 0.0));
        arcSegment2.put_Size(new Size(30.0, 30.0));
        PathSegmentCollection segmentCollection1 = new PathSegmentCollection();
        ((ICollection<PathSegment>) segmentCollection1).Add((PathSegment) arcSegment1);
        ((ICollection<PathSegment>) segmentCollection1).Add((PathSegment) arcSegment2);
        pathFigure1.put_Segments(segmentCollection1);
        PathFigureCollection figureCollection1 = new PathFigureCollection();
        ((ICollection<PathFigure>) figureCollection1).Add(pathFigure1);
        PathGeometry pathPercent1 = new PathGeometry();
        pathPercent1.put_Figures(figureCollection1);
        return pathPercent1;
      }
    }

    public PathGeometry PathRestPart
    {
      get
      {
        if (this.Percent <= 0 || this.Percent >= 100)
          return (PathGeometry) null;
        PathFigure pathFigure = new PathFigure();
        pathFigure.put_StartPoint(new Point(30.0, 30.0));
        LineSegment lineSegment = new LineSegment();
        lineSegment.put_Point(new Point(30.0, 0.0));
        ArcSegment arcSegment = new ArcSegment();
        arcSegment.put_Point(new Point(30.0 * (1.0 + Math.Sin(6.2831853071795862 * (double) this.Percent / 100.0)), 30.0 * (1.0 - Math.Cos(6.2831853071795862 * (double) this.Percent / 100.0))));
        arcSegment.put_Size(new Size(30.0, 30.0));
        arcSegment.put_IsLargeArc(this.Percent > 50);
        arcSegment.put_SweepDirection((SweepDirection) 1);
        PathSegmentCollection segmentCollection = new PathSegmentCollection();
        ((ICollection<PathSegment>) segmentCollection).Add((PathSegment) lineSegment);
        ((ICollection<PathSegment>) segmentCollection).Add((PathSegment) arcSegment);
        pathFigure.put_Segments(segmentCollection);
        PathFigureCollection figureCollection = new PathFigureCollection();
        ((ICollection<PathFigure>) figureCollection).Add(pathFigure);
        PathGeometry pathRestPart = new PathGeometry();
        pathRestPart.put_Figures(figureCollection);
        return pathRestPart;
      }
    }

    public string PercentString
    {
      get
      {
        return this.Percent <= 0 || this.Percent >= 100 ? LocalizedResources.GetLocalizedString("WinPhoneNoDiscountLabel") : "-" + (object) this.Percent + "%";
      }
    }

    public int PercentStringFontSize => this.Percent <= 0 || this.Percent >= 100 ? 12 : 20;

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///View/Controls/Sector.xaml"), (ComponentResourceLocation) 0);
      this.LayoutRootGrid = (Grid) ((FrameworkElement) this).FindName("LayoutRootGrid");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
