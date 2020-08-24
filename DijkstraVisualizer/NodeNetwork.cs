using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DijkstraVisualizer
{
    class NodeNetwork
    {
        public List<Node> Nodes;
        private Canvas _DrawCanvas;
        public List<Line> VisualConnections;

        public NodeNetwork(Canvas drawCanvas)
        {
            Nodes = new List<Node>();
            _DrawCanvas = drawCanvas;
            VisualConnections = new List<Line>();
        }

        public void ResetNetwork()
        {
            //Clear all temp infos
            foreach (var node in Nodes)
            {
                node.OriginNodes.Clear();
                node.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            }


        }

        public void FindConnection(Node startNode, Node endNode)
        {
            var newNodes = new List<Node>() { startNode };
            var checkedNodes = new List<Node>();
            var visualRouteConnections = new List<Line>();

            Node nextOriginNode = null;
            Node nextTargetNode = null;

            endNode.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 190, 0));

            startNode.Distance = 0;


            while (!newNodes.Contains(endNode) && !checkedNodes.Contains(endNode))
            {
                var shortestDistance = 100000d;
                foreach (var newNode in newNodes)
                {
                    foreach (var nodeConnection in newNode.Connections)
                    {
                        if (Node.GetDistance(newNode, nodeConnection) + newNode.Distance < shortestDistance - 1 && !newNodes.Contains(nodeConnection) && !checkedNodes.Contains(nodeConnection))
                        {
                            shortestDistance = Node.GetDistance(newNode, nodeConnection) + newNode.Distance;
                            nextTargetNode = nodeConnection;
                            nextOriginNode = newNode;
                            nextTargetNode.Distance = shortestDistance;
                        }
                        if (nodeConnection == endNode)
                        {

                            shortestDistance = 1;
                            nextOriginNode = newNode;
                            nextTargetNode = nodeConnection;
                        }
                    }
                }

                if (nextTargetNode == null || newNodes.Distinct().Count() != newNodes.Count)
                {

                    var errorResult = MessageBox.Show("No route has been found. Nodes are not connected", "No route found");
                    if (errorResult == MessageBoxResult.OK)
                    {
                        ResetNetwork();
                        visualRouteConnections.ForEach(c => _DrawCanvas.Children.Remove(c));
                        return;

                    }
                }
                nextOriginNode.OriginNodes.ForEach(c => nextTargetNode.OriginNodes.Add(c));
                nextTargetNode.OriginNodes.Add(nextOriginNode);
                newNodes.Add(nextTargetNode);
                if (nextTargetNode != endNode)
                    nextTargetNode.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 200));

                var newConnection = new Line();
                visualRouteConnections.Add(newConnection);
                newConnection.X1 = nextOriginNode.GetLocation().X;
                newConnection.Y1 = nextOriginNode.GetLocation().Y;
                newConnection.X2 = nextTargetNode.GetLocation().X;
                newConnection.Y2 = nextTargetNode.GetLocation().Y;
                newConnection.Stroke = new SolidColorBrush(Color.FromRgb(240, 0, 0));
                newConnection.StrokeThickness = 4;
                _DrawCanvas.Children.Add(newConnection);


            }

            if (newNodes.Contains(endNode))
            {
                for (int i = 0; i < endNode.OriginNodes.Count - 1; i++)
                {
                    var rightRoute = new Line();
                    visualRouteConnections.Add(rightRoute);
                    rightRoute.X1 = endNode.OriginNodes[i].GetLocation().X;
                    rightRoute.Y1 = endNode.OriginNodes[i].GetLocation().Y;
                    rightRoute.X2 = endNode.OriginNodes[i + 1].GetLocation().X;
                    rightRoute.Y2 = endNode.OriginNodes[i + 1].GetLocation().Y;
                    rightRoute.Stroke = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    rightRoute.StrokeThickness = 4;
                    _DrawCanvas.Children.Add(rightRoute);
                }
                var lastRoute = new Line();
                visualRouteConnections.Add(lastRoute);
                lastRoute.X1 = endNode.OriginNodes.Last().GetLocation().X;
                lastRoute.Y1 = endNode.OriginNodes.Last().GetLocation().Y;
                lastRoute.X2 = endNode.GetLocation().X;
                lastRoute.Y2 = endNode.GetLocation().Y;
                lastRoute.Stroke = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                lastRoute.StrokeThickness = 4;
                _DrawCanvas.Children.Add(lastRoute);

                endNode.Distance = endNode.OriginNodes.Last().Distance + Node.GetDistance(endNode.OriginNodes.Last(), endNode);
            }

            var result = MessageBox.Show("A route has been found. Distance: " + Math.Round(endNode.Distance, 1), "Route found");
            if (result == MessageBoxResult.OK)
            {
                ResetNetwork();
                visualRouteConnections.ForEach(c => _DrawCanvas.Children.Remove(c));

            }
        }

        public Node FindNode(Point location)
        {
            Node outNode = null;

            foreach (var node in Nodes)
            {
                if (Math.Sqrt(Math.Pow(node.GetLocation().X - location.X, 2) +
                              Math.Pow(node.GetLocation().Y - location.Y, 2)) < 12.5)
                {
                    outNode = node;
                }
            }

            return outNode;
        }
        public void AddNode(Node node)
        {
            if (CanAddNode(node))
            {
                Nodes.Add(node);
                node.Visualize(_DrawCanvas);
            }
        }

        private bool CanAddNode(Node newNode)
        {
            var CanAddNode = true;
            foreach (var node in Nodes)
            {
                if (Node.GetDistance(newNode, node) < 50)
                {
                    CanAddNode = false;
                }
            }
            return CanAddNode;
        }
    }
}