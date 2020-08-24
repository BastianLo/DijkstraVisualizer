using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace DijkstraVisualizer
{
    /// <summary>
    /// Interaktionslogik für NumberInputWindow.xaml
    /// </summary>
    public partial class NetworkInputWindow : Window
    {

        public NetworkInputWindow()
        {
            InitializeComponent();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("DijkstraVisualizer")), @"Networks");
            foreach (var file in Directory.GetFiles(path))
            {
                ListBoxFiles.Items.Add(file.Substring(file.LastIndexOf("\\") + 1).Replace(".txt", ""));
            }

        }


        private void Window_ContentRendered(object sender, EventArgs e)
        {
            ListBoxFiles.Focus();
        }

        public string Answer
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("DijkstraVisualizer")), @"Networks") + "\\" + ListBoxFiles.SelectedItem.ToString(); }
        }

        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}