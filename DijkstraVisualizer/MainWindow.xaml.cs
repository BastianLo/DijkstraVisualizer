﻿using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DijkstraVisualizer
{
    public partial class MainWindow : Window
    {
        private NodeNetwork MainNetwork;
        private string buttonSelection;
        private Node selectedNode;

        public MainWindow()
        {
            InitializeComponent();
            MainNetwork = new NodeNetwork(DrawCanvas);
        }

        private void DrawCanvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (buttonSelection == "AddNode")
            {
                MainNetwork.AddNode(new Node(e.GetPosition(this)));
            }

            if (buttonSelection == "AddConnection")
            {
                //Check if First or Second Click
                if (selectedNode == null)
                {
                    selectedNode = MainNetwork.FindNode(e.GetPosition(this));
                    if (selectedNode != null)
                        selectedNode.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 190, 0));
                }
                else
                {
                    var targetNode = MainNetwork.FindNode(e.GetPosition(this));
                    //Check if User clicked on a node
                    if (targetNode != null)
                    {

                        if (Math.Abs(selectedNode.GetLocation().X - e.GetPosition(this).X) < 10 && Math.Abs(selectedNode.GetLocation().Y - e.GetPosition(this).Y) < 10)
                        { }
                        else
                        {
                            //Check if Connection already exists
                            if (!selectedNode.Connections.Contains(targetNode))
                            {
                                //Create Visual Connection
                                var connection = new Line();
                                MainNetwork.VisualConnections.Add(connection);
                                connection.X1 = selectedNode.GetLocation().X;
                                connection.X2 = targetNode.GetLocation().X;
                                connection.Y1 = selectedNode.GetLocation().Y;
                                connection.Y2 = targetNode.GetLocation().Y;
                                connection.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                                connection.StrokeThickness = 4;
                                DrawCanvas.Children.Add(connection);

                                //Create Object Connection
                                targetNode.Connections.Add(selectedNode);
                                selectedNode.Connections.Add(targetNode);

                                selectedNode.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                                selectedNode = null;

                            }

                        }

                    }
                }
            }

            if (buttonSelection == "FindRoute")
            {
                //Check if First or Second Click
                if (selectedNode == null)
                {
                    selectedNode = MainNetwork.FindNode(e.GetPosition(this));
                    if (selectedNode != null)
                        selectedNode.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 190, 0));
                }
                else
                {
                    var targetNode = MainNetwork.FindNode(e.GetPosition(this));
                    //Check if User clicked on a node
                    if (targetNode != null)
                    {

                        if (Math.Abs(selectedNode.GetLocation().X - e.GetPosition(this).X) < 10 && Math.Abs(selectedNode.GetLocation().Y - e.GetPosition(this).Y) < 10)
                        { }
                        else
                        {
                            MainNetwork.FindConnection(selectedNode, targetNode);
                            selectedNode = null;

                        }

                    }
                }
            }
        }

        private void BtnClearNetwork_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure that you want to delete the current Network?", "Delete Network",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DrawCanvas.Children.Clear();
                MainNetwork = new NodeNetwork(DrawCanvas);
            }
        }

        private void BtnAddNode_OnClick(object sender, RoutedEventArgs e)
        {
            buttonSelection = "AddNode";
            if (selectedNode != null)
            {
                selectedNode.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                selectedNode = null;
            }
        }

        private void BtnAddConnection_OnClick(object sender, RoutedEventArgs e)
        {
            buttonSelection = "AddConnection";
            if (selectedNode != null)
            {
                selectedNode.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                selectedNode = null;
            }
        }

        private void BtnFindRoute_OnClick(object sender, RoutedEventArgs e)
        {
            buttonSelection = "FindRoute";
            if (selectedNode != null)
            {
                selectedNode.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                selectedNode = null;
            }
        }

        private void MainGrid_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedNode != null)
            {
                selectedNode.VisualNode.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                selectedNode = null;
            }
        }

        private void BtnAutoConnect_OnClick(object sender, RoutedEventArgs e)
        {
            var distance = 0d;
            var inputDialog = new NumberInputWindow("Please give the max. distance between connected nodes", 0);
            if (inputDialog.ShowDialog() == true)
                distance = inputDialog.Answer;

            foreach (var iNode in MainNetwork.Nodes)
            {
                foreach (var jNode in MainNetwork.Nodes)
                {
                    if (iNode != jNode && Node.GetDistance(iNode, jNode) < distance && !iNode.Connections.Contains(jNode) && !jNode.Connections.Contains(iNode))
                    {
                        iNode.Connections.Add(jNode);
                        jNode.Connections.Add(iNode);
                        //Create Visual Connection
                        var connection = new Line();
                        MainNetwork.VisualConnections.Add(connection);
                        connection.X1 = iNode.GetLocation().X;
                        connection.X2 = jNode.GetLocation().X;
                        connection.Y1 = iNode.GetLocation().Y;
                        connection.Y2 = jNode.GetLocation().Y;
                        connection.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                        connection.StrokeThickness = 4;
                        DrawCanvas.Children.Add(connection);
                    }
                }
            }
        }

        private void BtnCreateRandom_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
