using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DijkstraVisualizer
{
    class NetworkFileHandler
    {
        public static void ExportNetwork(NodeNetwork network, string fileName)
        {
            var lines = new List<string>();
            for (int i = 0; i < network.Nodes.Count; i++)
            {
                var line = "id:" + i + ";x:" + network.Nodes[i].GetLocation().X + ";y:" +
                           network.Nodes[i].GetLocation().Y + ";Con:";
                foreach (var connection in network.Nodes[i].Connections)
                {
                    line += network.Nodes.IndexOf(connection) + ",";
                }
                lines.Add(line);
            }
            Trace.WriteLine(fileName);
            var filepath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("DijkstraVisualizer")) + "\\Networks\\";
            fileName = filepath + fileName;
            Trace.WriteLine(fileName);
            File.WriteAllLines(fileName, lines);
        }

        public static void ImportNetwork(ref NodeNetwork network, string fileName, Canvas drawCanvas)
        {
            network.Nodes.Clear();
            drawCanvas.Children.Clear();
            fileName = fileName.Substring(fileName.LastIndexOf("C:\\"));
            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                var x = Convert.ToInt32(Regex.Match(line, "x:(\\w+)").Groups[1].Value);
                var y = Convert.ToInt32(Regex.Match(line, "y:(\\w+)").Groups[1].Value);

                network.AddNode(new Node(new Point(x, y)));
            }

            for (int i = 0; i < lines.Length; i++)
            {
                var regexConnections = Regex.Match(lines[i], "Con:([\\w,]+)").Groups[1].Value;
                if (regexConnections != "")
                {
                    regexConnections = regexConnections.Substring(0, regexConnections.Length - 1);
                    var connections = regexConnections.Split(',');
                    foreach (var connection in connections)
                    {

                        network.Nodes[i].Connections.Add(network.Nodes[Convert.ToInt32(connection)]);
                        var conn = new Line();
                        network.VisualConnections.Add(conn);

                        conn.X1 = network.Nodes[i].GetLocation().X;
                        conn.X2 = network.Nodes[Convert.ToInt32(connection)].GetLocation().X;
                        conn.Y1 = network.Nodes[i].GetLocation().Y;
                        conn.Y2 = network.Nodes[Convert.ToInt32(connection)].GetLocation().Y;
                        conn.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                        conn.StrokeThickness = 4;
                        drawCanvas.Children.Add(conn);
                    }

                }
            }
        }
    }
}
