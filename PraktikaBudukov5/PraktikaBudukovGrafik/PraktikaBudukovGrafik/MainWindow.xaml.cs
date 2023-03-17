using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace PraktikaBudukovGrafik
{
    public partial class MainWindow : Window
    {
        private const int _dotCount = 150;
        private List<double[]> _dataList = new List<double[]>();
        private DrawingGroup _drawingGroup = new DrawingGroup();

        public MainWindow()
        {
            DataFill();
            Execute();
            InitializeComponent();
            image1.Source = new DrawingImage(_drawingGroup);
        }

        private void DataFill()
        {
            double[] sin = new double[_dotCount + 1];
            double[] cos = new double[_dotCount + 1];

            for (int i = 0; i < sin.Length; i++)
            {
                double angle = Math.PI * 2 / _dotCount * i;
                sin[i] = Math.Sqrt(Math.Sqrt(Math.Pow(3, angle))) * Math.Sin(Math.Pow(3, angle));
                cos[i] = Math.Cos(angle);
            }
            _dataList.Add(sin);
            _dataList.Add(cos);
        }

        private void BackGroundFun()
        {
            GeometryDrawing geometryDrawing = new GeometryDrawing();

            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(0, -2, 6, 6);
            geometryDrawing.Geometry = rectangleGeometry;

            geometryDrawing.Pen = new Pen(Brushes.Red, 0.005);
            geometryDrawing.Brush = Brushes.Beige;

            _drawingGroup.Children.Add(geometryDrawing);
        }

        private void GridFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for (int i = -20; i < 40; i++)
            {
                LineGeometry line = new LineGeometry(new Point(0, i * 0.1), new Point(6.1, i * 0.1));
                geometryGroup.Children.Add(line);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);
            double[] dashes = { 1, 1, 1, 1, 1 };
            geometryDrawing.Pen.DashStyle = new DashStyle(dashes, -.1);

            geometryDrawing.Brush = Brushes.Beige;

            _drawingGroup.Children.Add(geometryDrawing);
        }

        private void SinFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for (int i = 0; i < _dataList[0].Length - 1; i++)
            {
                LineGeometry line = new LineGeometry
                (
                    new Point((double)i / (double)_dotCount, .99 - (_dataList[0][i] / 2.0)),
                    new Point((double)(i + 1) / (double)_dotCount, .99 - (_dataList[0][i + 1] / 2.0))
                );
                geometryGroup.Children.Add(line);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Pen = new Pen(Brushes.Blue, 0.005);

            _drawingGroup.Children.Add(geometryDrawing);
        }        

        [Obsolete]
        private void MarkerFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for (int i = -20; i <= 40; i++)
            {
                FormattedText formattedText = new FormattedText
                    (string.Format("{0,1}", Math.Round(1 - i * 0.2, 2)),
                    CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Verdana"),
                    0.1,
                    Brushes.Black);

                formattedText.SetFontWeight(FontWeights.Bold);

                Geometry geometry = formattedText.BuildGeometry(new Point(-0.2, i * 0.1 - 0.03));
                geometryGroup.Children.Add(geometry);
            }

            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Brush = Brushes.LightGray;
            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);

            _drawingGroup.Children.Add(geometryDrawing);
        }

        private void Execute()
        {
            BackGroundFun();
            GridFun();
            SinFun();
            MarkerFun();
        }
    }
}
