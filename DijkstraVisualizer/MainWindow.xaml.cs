using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DijkstraVisualizer
{
    class NodeNetwork
    {
        private List<Node> Nodes;
        private Canvas _DrawCanvas;

        public NodeNetwork(Canvas drawCanvas)
        {
            Nodes = new List<Node>();

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
            Trace.WriteLine(CanAddNode);
            return CanAddNode;
        }
    }

    class Node
    {
        private Point _location;
        public List<Node> Connections;

        public Node(Point location)
        {
            _location = location;
            Connections = new List<Node>();
        }

        public void Visualize(Canvas drawCanvas)
        {

        }

        public Point GetLocation() => _location;

        public void SetLocation(Point location) => _location = location;

        public static double GetDistance(Node originNode, Node targetNode)
            => Math.Sqrt(Math.Pow(originNode._location.X - targetNode._location.X, 2) +
                         Math.Pow(originNode._location.Y - targetNode._location.Y, 2));
    }

    public partial class MainWindow : Window
    {
        private NodeNetwork MainNetwork;

        public MainWindow()
        {
            InitializeComponent();
            MainNetwork = new NodeNetwork(DrawCanvas);
        }

        private void DrawCanvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainNetwork.AddNode(new Node(e.GetPosition(this)));
        }
    }
}
