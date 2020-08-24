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

namespace DijkstraVisualizer.Network_File_Handler
{
    /// <summary>
    /// Interaktionslogik für FileNameInputWindow.xaml
    /// </summary>
    public partial class FileNameInputWindow : Window
    {
        public FileNameInputWindow(string question, string defaultAnswer = "")
        {
            InitializeComponent();
            lblQuestion.Content = question;
            txtAnswer.Text = defaultAnswer;
        }

        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public string Answer
        {
            get { return txtAnswer.Text; }
        }
    }
}
