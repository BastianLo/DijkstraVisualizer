using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DijkstraVisualizer
{
    class Node
    {
        private Point _location;
        public List<Node> Connections;
        public Ellipse VisualNode = new Ellipse();
        public double Distance;
        public List<Node> OriginNodes = new List<Node>();

        public Node(Point location)
        {
            _location = location;
            Connections = new List<Node>();

        }

        public void Visualize(Canvas drawCanvas)
        {
            VisualNode.Width = 24;
            VisualNode.Height = 24;
            VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            drawCanvas.Children.Add(VisualNode);
            Canvas.SetLeft(VisualNode, _location.X - 12);
            Canvas.SetTop(VisualNode, _location.Y - 12);
        }

        public Point GetLocation() => _location;

        public void SetLocation(Point location) => _location = location;

        public static double GetDistance(Node originNode, Node targetNode)
            => Math.Sqrt(Math.Pow(originNode._location.X - targetNode._location.X, 2) +
                         Math.Pow(originNode._location.Y - targetNode._location.Y, 2));
    }
}