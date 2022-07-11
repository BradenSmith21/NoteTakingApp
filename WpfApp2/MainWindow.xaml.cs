﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            /* // Open a Stream and decode a GIF image
             Stream imageStreamSource = new FileStream("C:/Users/Fault/Desktop/temp/test.gif", FileMode.Open, FileAccess.Read, FileShare.Read);
             newNoteBox.Paste();
             GifBitmapDecoder decoder = new GifBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
             BitmapSource bitmapSource = decoder.Frames[9];

             // Draw the Image
             System.Windows.Controls.Image myImage = new System.Windows.Controls.Image
             {
                 Source = bitmapSource,
                 Stretch = Stretch.None,
                 Margin = new Thickness(20)
             };
             //Bitmap bmp = new Bitmap(@"C:\Users\Fault\Desktop\temp\test.gif");
             //System.Windows.Forms.Clipboard.GetImage();
           
            
            
            //newNoteBox.Paste();*/
            /* var image = new BitmapImage();
             image.BeginInit();
             image.UriSource = new Uri("C:/Users/Fault/Desktop/temp/test.gif");
             image.EndInit();
             ImageBehavior.SetAnimatedSource( img, image);*/

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

                newNoteBox.FontFamily = new System.Windows.Media.FontFamily(fd.Font.Name);
                newNoteBox.FontSize = fd.Font.Size * 96.0 / 72.0;
                newNoteBox.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                newNoteBox.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }

        private void leftAlignment_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignLeft.Execute(null, newNoteBox);
        }

        private void CenterAlignment_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignCenter.Execute(null, newNoteBox);
        }

        private void RightAlignment_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.AlignRight.Execute(null, newNoteBox);
        }

        private void FontIncrease_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.IncreaseFontSize.Execute(null, newNoteBox);
        }

        private void FontDecrease_Click(object sender, RoutedEventArgs e)
        {
            EditingCommands.DecreaseFontSize.Execute(null, newNoteBox);
        }

        private void openNote_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Rich Text file (*.rtf)|*.rtf";
            if (openFileDialog.ShowDialog() == true)
            { 
                if((myStream = openFileDialog.OpenFile())!=null)
                {
                    string fileName = openFileDialog.FileName;
                    string fileText = File.ReadAllText(fileName);
                    newNoteBox.Text = fileText;
                }

            }
        }

        private void ColorChange_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                newNoteBox.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B));
            }
        }
    }
}



