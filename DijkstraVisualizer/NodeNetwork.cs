using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace DijkstraVisualizer
{
    class NodeNetwork
    {
        private List<Node> Nodes;
        private Canvas _DrawCanvas;

        public NodeNetwork(Canvas drawCanvas)
        {
            Nodes = new List<Node>();
            _DrawCanvas = drawCanvas;
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
                    Trace.WriteLine("Node Found");
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