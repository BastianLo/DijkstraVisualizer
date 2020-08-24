using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DijkstraVisualizer
{
    /// <summary>
    /// Interaktionslogik für NumberInputWindow.xaml
    /// </summary>
    public partial class NumberInputWindow : Window
    {

        public NumberInputWindow(string question, int min, int max, double defaultNumber = 0)
        {
            InitializeComponent();
            lblQuestion.Content = question;
            txtAnswer.Value = defaultNumber;
            txtAnswer.Minimum = min;
            txtAnswer.Maximum = max;
        }


        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.Focus();
        }

        public double Answer
        {
            get { return txtAnswer.Value; }
        }

        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
