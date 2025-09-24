using System;
using System.ComponentModel;
using VPN.Localization;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

#nullable disable
namespace VPN.View.Controls
{
    public sealed partial class Sector : UserControl, INotifyPropertyChanged
    {
        private const int radius = 30;
        public static readonly DependencyProperty PercentProperty = DependencyProperty.Register(
            nameof(Percent), typeof(int), typeof(Sector),
            new PropertyMetadata(0, (d, e) =>
            {
                if (d is Sector s)
                    s.OnPercentChanged();
            }));

        public Sector()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public int Percent
        {
            get => (int)GetValue(PercentProperty);
            set => SetValue(PercentProperty, value);
        }

        public PathGeometry PathPercent
        {
            get
            {
                if (this.Percent > 0 && this.Percent < 100)
                {
                    var pathFigure = new PathFigure { StartPoint = new Point(30.0, 30.0) };
                    var lineSegment = new LineSegment
                    {
                        Point = new Point(
                            30.0 * (1.0 + Math.Sin(2 * Math.PI * this.Percent / 100.0)),
                            30.0 * (1.0 - Math.Cos(2 * Math.PI * this.Percent / 100.0)))
                    };
                    var arcSegment = new ArcSegment
                    {
                        Point = new Point(30.0, 0.0),
                        Size = new Size(30.0, 30.0),
                        IsLargeArc = this.Percent > 50,
                        SweepDirection = SweepDirection.Counterclockwise
                    };
                    pathFigure.Segments.Add(lineSegment);
                    pathFigure.Segments.Add(arcSegment);
                    var pathPercent = new PathGeometry();
                    pathPercent.Figures.Add(pathFigure);
                    return pathPercent;
                }
                var fullFigure = new PathFigure { StartPoint = new Point(30.0, 0.0) };
                var a1 = new ArcSegment { Point = new Point(30.0, 60.0), Size = new Size(30.0, 30.0) };
                var a2 = new ArcSegment { Point = new Point(30.0, 0.0), Size = new Size(30.0, 30.0) };
                fullFigure.Segments.Add(a1);
                fullFigure.Segments.Add(a2);
                var pg = new PathGeometry();
                pg.Figures.Add(fullFigure);
                return pg;
            }
        }

        public PathGeometry PathRestPart
        {
            get
            {
                if (this.Percent <= 0 || this.Percent >= 100)
                    return null;
                var pathFigure = new PathFigure { StartPoint = new Point(30.0, 30.0) };
                var lineSegment = new LineSegment { Point = new Point(30.0, 0.0) };
                var arcSegment = new ArcSegment
                {
                    Point = new Point(30.0 * (1.0 + Math.Sin(2 * Math.PI * this.Percent / 100.0)),
                                      30.0 * (1.0 - Math.Cos(2 * Math.PI * this.Percent / 100.0))),
                    Size = new Size(30.0, 30.0),
                    IsLargeArc = this.Percent > 50,
                    SweepDirection = SweepDirection.Counterclockwise
                };
                pathFigure.Segments.Add(lineSegment);
                pathFigure.Segments.Add(arcSegment);
                var pathRestPart = new PathGeometry();
                pathRestPart.Figures.Add(pathFigure);
                return pathRestPart;
            }
        }

        public string PercentString =>
            this.Percent <= 0 || this.Percent >= 100 ? LocalizedResources.GetLocalizedString("WinPhoneNoDiscountLabel") : "-" + this.Percent + "%";

        public int PercentStringFontSize => this.Percent <= 0 || this.Percent >= 100 ? 12 : 20;

        private void OnPercentChanged()
        {
            OnPropertyChanged(nameof(PathPercent));
            OnPropertyChanged(nameof(PathRestPart));
            OnPropertyChanged(nameof(PercentString));
            OnPropertyChanged(nameof(PercentStringFontSize));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
