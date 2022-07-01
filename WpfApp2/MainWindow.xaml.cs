using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void saveTextBox_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Rich Text file (*.rtf)|*.rtf";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, newNoteBox.Text);
            }

        }

        private void toggleBullets_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleBullets.Execute(null, newNoteBox);
        }

        private void underLine_Click(object sender, RoutedEventArgs e)
        {
           EditingCommands.ToggleUnderline.Execute(null, newNoteBox);  
        }

        private void toggleNumerals_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleNumbering.Execute(null, newNoteBox);
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleBold.Execute(null, newNoteBox);
        }

        private void Italics_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.ToggleItalic.Execute(null, newNoteBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FontDialog fd = new FontDialog();
            var result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Debug.WriteLine(fd.Font);

                newNoteBox.FontFamily = new FontFamily(fd.Font.Name);
                newNoteBox.FontSize = fd.Font.Size * 96.0 / 72.0;
                newNoteBox.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                newNoteBox.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }
    }
}



