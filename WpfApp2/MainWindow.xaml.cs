using System;
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
using MySql.Data.MySqlClient;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Loaded += MainWindow_Loaded;
            InitializeComponent();
        }
      /*  //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
         // Open a Stream and decode a GIF image
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
         ImageBehavior.SetAnimatedSource( img, image);

        // }*/
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

        private void ColorChange_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                newNoteBox.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B));
            }
        }

       
        private void saveTextBox_Click(object sender, RoutedEventArgs e)
        {
            #region PastVersions
            /*Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Filter = "Rich Text file (*.rtf)|*.rtf";
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, newNoteBox.Text);
                } THIS IS IF DATABASE DOESNT WORK... PULLS FILE FROM COMPUTER INSTEAD*/


            //string testName = "testName";
            //string testSummary = "testSummary";
            //string connectionString = "SERVER=localhost;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            //using (MySqlConnection connection = new MySqlConnection(connectionString))
            //{
            //    connection.Open();
            //    using (MySqlCommand myCmd = new MySqlCommand("INSERT INTO notes(note_name, note_summary, note_content) Values(@testName,@testSummary,@noteContent)", connection))
            //    {

            //        myCmd.Parameters.AddWithValue("@testName", testName.ToString());
            //        myCmd.Parameters.AddWithValue("@testSummary", testSummary.ToString());
            //        myCmd.Parameters.AddWithValue("@noteContent", newNoteBox.Text);

            //        myCmd.ExecuteNonQuery();
            //        connection.Close();
            //    }


            //} // yay this works, now to test loading UPDATE This was my crude way of saving to database, rewritten below to make program useable   
            #endregion
            string testName = "testName";
            string testSummary = "testSummary";
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand myCmd = connection.CreateCommand();
            myCmd.Parameters.AddWithValue("@testName", testName.ToString());
            myCmd.Parameters.AddWithValue("@testSummary", testSummary.ToString());
            myCmd.Parameters.AddWithValue("@noteContent", newNoteBox.Text);
            myCmd.CommandText = "UPDATE notes SET note_name=@testName, note_summary=@testSummary, note_content=@noteContent WHERE note_id=" + 1 + ";";
            if (myCmd.ExecuteNonQuery() > 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Note was saved to Note 1!");
            }

            connection.Close();

        }
      
        private void saveTextBox2_Click(object sender, RoutedEventArgs e)
        {
            string testName = "testName";
            string testSummary = "testSummary";
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand myCmd = connection.CreateCommand();
            myCmd.Parameters.AddWithValue("@testName", testName.ToString());
            myCmd.Parameters.AddWithValue("@testSummary", testSummary.ToString());
            myCmd.Parameters.AddWithValue("@noteContent", newNoteBox.Text);
            myCmd.CommandText = "UPDATE notes SET note_name=@testName, note_summary=@testSummary, note_content=@noteContent WHERE note_id=" + 2 + ";";
            if (myCmd.ExecuteNonQuery() > 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Note was saved to Note 2!");
            }
            connection.Close();
        }

        private void saveTextBox3_Click(object sender, RoutedEventArgs e)
        {
            string testName = "testName";
            string testSummary = "testSummary";
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand myCmd = connection.CreateCommand();
            myCmd.Parameters.AddWithValue("@testName", testName.ToString());
            myCmd.Parameters.AddWithValue("@testSummary", testSummary.ToString());
            myCmd.Parameters.AddWithValue("@noteContent", newNoteBox.Text);
            myCmd.CommandText = "UPDATE notes SET note_name=@testName, note_summary=@testSummary, note_content=@noteContent WHERE note_id=" + 3 + ";";
            if (myCmd.ExecuteNonQuery() > 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Note was saved to Note 3!");
            }

            connection.Close();
        }

        private void saveTextBox4_Click(object sender, RoutedEventArgs e)
        {
            string testName = "testName";
            string testSummary = "testSummary";
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand myCmd = connection.CreateCommand();
            myCmd.Parameters.AddWithValue("@testName", testName.ToString());
            myCmd.Parameters.AddWithValue("@testSummary", testSummary.ToString());
            myCmd.Parameters.AddWithValue("@noteContent", newNoteBox.Text);
            myCmd.CommandText = "UPDATE notes SET note_name=@testName, note_summary=@testSummary, note_content=@noteContent WHERE note_id=" + 4 + ";";
            if (myCmd.ExecuteNonQuery() > 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Note was saved to Note 4!");
            }

            connection.Close();
        }

        private void saveTextBox5_Click(object sender, RoutedEventArgs e)
        {
            string testName = "testName";
            string testSummary = "testSummary";
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand myCmd = connection.CreateCommand();
            myCmd.Parameters.AddWithValue("@testName", testName.ToString());
            myCmd.Parameters.AddWithValue("@testSummary", testSummary.ToString());
            myCmd.Parameters.AddWithValue("@noteContent", newNoteBox.Text);
            myCmd.CommandText = "UPDATE notes SET note_name=@testName, note_summary=@testSummary, note_content=@noteContent WHERE note_id=" + 5 + ";";
            if (myCmd.ExecuteNonQuery() > 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Note was saved to Note 5");
            }

            connection.Close();
        }

        private void saveTextBox6_Click(object sender, RoutedEventArgs e)
        {
            string testName = "testName";
            string testSummary = "testSummary";
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand myCmd = connection.CreateCommand();
            myCmd.Parameters.AddWithValue("@testName", testName.ToString());
            myCmd.Parameters.AddWithValue("@testSummary", testSummary.ToString());
            myCmd.Parameters.AddWithValue("@noteContent", newNoteBox.Text);
            myCmd.CommandText = "UPDATE notes SET note_name=@testName, note_summary=@testSummary, note_content=@noteContent WHERE note_id=" + 6 + ";";
            if (myCmd.ExecuteNonQuery() > 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Note was saved to Note 6!");
            }

            connection.Close();
        }
        
        
        private void openNote_Click(object sender, RoutedEventArgs e)
        {
            #region Past versions
            /* Stream myStream;
                 Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                 openFileDialog.Filter = "Rich Text file (*.rtf)|*.rtf";
                 if (openFileDialog.ShowDialog() == true)
                 {
                     if ((myStream = openFileDialog.OpenFile()) != null)
                     {
                         string fileName = openFileDialog.FileName;
                         string fileText = File.ReadAllText(fileName);
                         newNoteBox.Text = fileText;
                     }

                 }*/

            // DATABASE PULLING WORKS :D, just need further development to make it less hard coded and more automatic.
            #endregion
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT note_content FROM notes WHERE note_id=" + 1, connection))
                using (MySqlDataReader sqlreader = mySqlCommand.ExecuteReader())
                {
                    if (sqlreader.Read())
                    {
                        newNoteBox.Text = sqlreader["note_content"].ToString();
                    }
                }
                Xceed.Wpf.Toolkit.MessageBox.Show("Note successfully opened, tab over to 'New Note' to view/edit");

                connection.Close();
            }
        }

        private void openNote2_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT note_content FROM notes WHERE note_id=" + 2, connection))
                using (MySqlDataReader sqlreader = mySqlCommand.ExecuteReader())
                {
                    if (sqlreader.Read())
                    {
                        newNoteBox.Text = sqlreader["note_content"].ToString();
                    }
                }
                Xceed.Wpf.Toolkit.MessageBox.Show("Note successfully opened, tab over to 'New Note' to view/edit");

                connection.Close();
            }
        }

        private void openNote4_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT note_content FROM notes WHERE note_id=" + 3, connection))
                using (MySqlDataReader sqlreader = mySqlCommand.ExecuteReader())
                {
                    if (sqlreader.Read())
                    {
                        newNoteBox.Text = sqlreader["note_content"].ToString();
                    }
                }
                Xceed.Wpf.Toolkit.MessageBox.Show("Note successfully opened, tab over to 'New Note' to view/edit");

                connection.Close();
            }
        }

        private void openNote6_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT note_content FROM notes WHERE note_id=" + 4, connection))
                using (MySqlDataReader sqlreader = mySqlCommand.ExecuteReader())
                {
                    if (sqlreader.Read())
                    {
                        newNoteBox.Text = sqlreader["note_content"].ToString();
                    }
                }
                Xceed.Wpf.Toolkit.MessageBox.Show("Note successfully opened, tab over to 'New Note' to view/edit");

                connection.Close();
            }
        }

        private void openNote8_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT note_content FROM notes WHERE note_id=" + 5, connection))
                using (MySqlDataReader sqlreader = mySqlCommand.ExecuteReader())
                {
                    if (sqlreader.Read())
                    {
                        newNoteBox.Text = sqlreader["note_content"].ToString();
                    }
                }
                Xceed.Wpf.Toolkit.MessageBox.Show("Note successfully opened, tab over to 'New Note' to view/edit");

                connection.Close();
            }
        }

        private void openNote10_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT note_content FROM notes WHERE note_id=" + 6, connection))
                using (MySqlDataReader sqlreader = mySqlCommand.ExecuteReader())
                {
                    if (sqlreader.Read())
                    {
                        newNoteBox.Text = sqlreader["note_content"].ToString();
                    }
                }
                Xceed.Wpf.Toolkit.MessageBox.Show("Note successfully opened, tab over to 'New Note' to view/edit");
                connection.Close();
            }
        }
       
        
        private void deleteNote_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("UPDATE notes SET note_name = ' ', note_summary = ' ', note_content = ' ' WHERE note_id=" + 1 + ";", connection);

                if(mySqlCommand.ExecuteNonQuery() > 0)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Note 1 successfully deleted, you can now save a new note here!");
                }
               
                connection.Close();
            }
        }
        
        private void deleteNote1_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("UPDATE notes SET note_name = ' ', note_summary = ' ', note_content = ' ' WHERE note_id=" + 2 + ";", connection);
                if (mySqlCommand.ExecuteNonQuery() > 0)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Note 2 successfully deleted, you can now save a new note here!");
                }
                connection.Close();
            }
        }

        private void deleteNote2_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("UPDATE notes SET note_name = ' ', note_summary = ' ', note_content = ' ' WHERE note_id=" + 3 + ";", connection);

                if (mySqlCommand.ExecuteNonQuery() > 0)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Note 3 successfully deleted, you can now save a new note here!");
                }

                connection.Close();
            }
        }

        private void deleteNote3_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("UPDATE notes SET note_name = ' ', note_summary = ' ', note_content = ' ' WHERE note_id=" + 4 + ";", connection);

                if (mySqlCommand.ExecuteNonQuery() > 0)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Note 4 successfully deleted, you can now save a new note here!");
                }

                connection.Close();
            }
        }

        private void deleteNote4_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("UPDATE notes SET note_name = ' ', note_summary = ' ', note_content = ' ' WHERE note_id=" + 5 + ";", connection);

                if (mySqlCommand.ExecuteNonQuery() > 0)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Note 5 successfully deleted, you can now save a new note here!");
                }

                connection.Close();
            }
        }

        private void deleteNote5_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1;DATABASE=wpf_2;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("UPDATE notes SET note_name = ' ', note_summary = ' ', note_content = ' ' WHERE note_id=" + 6 + ";", connection);

                if (mySqlCommand.ExecuteNonQuery() > 0)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Note 6 successfully deleted, you can now save a new note here!");
                }

                connection.Close();
            }
        }
    }
}




