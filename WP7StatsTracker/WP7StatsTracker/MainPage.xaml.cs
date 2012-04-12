using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using System.IO.IsolatedStorage;

namespace WP7StatsTracker
{
    public partial class MainPage : PhoneApplicationPage
    {
        int count;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void BirthDate_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            var date = (DateTime) BirthDate.Value;
            MessageBox.Show(date.ToString("d"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile directory = IsolatedStorageFile.GetUserStoreForApplication();
            if(directory.FileExists("statistics.txt"))
            {
                using (var myFilestream = new IsolatedStorageFileStream("statistics.txt", FileMode.Append, IsolatedStorageFile.GetUserStoreForApplication()))
                using (var writer = new StreamWriter(myFilestream))
                {
                    writer.Write(txtNumRew.Text + "-" + BirthDate.Value.Value.ToShortDateString() + ",");
                }

                using (var myFileStream = new IsolatedStorageFileStream("statistics.txt", FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication()))
                using (var reader = new StreamReader(myFileStream))
                {
                    var text = reader.ReadToEnd();
                    string texy = "";
                    string[] arr = text.Split(',');
                    count = 0;
                    for (int i = 0; i < arr.Length; ++i)
                    {
                        texy += arr[i] + "\n";
                        lbEntries.Items.Add(arr[i]);
                        string[] rew = arr[i].Split('-');
                        string rewData = rew[0];
                        if (rewData == "" || rewData == " " || rewData == "0") { }
                        else
                            count += Convert.ToInt32(rewData);
                    }

                    //MessageBox.Show(Convert.ToString(count));
                }
                tbTotalResult.Text = "";
                tbTotalResult.Text = "Total Number of rewards is " + count.ToString();
            }
            else if (!directory.FileExists("statistics.txt"))
            {
                MessageBox.Show("There is no database, let me create that for you!");
                using (var myFilestream = new IsolatedStorageFileStream("statistics.txt", FileMode.Create, IsolatedStorageFile.GetUserStoreForApplication()));

            }
            txtNumRew.Text = "";
            

        }

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile directory = IsolatedStorageFile.GetUserStoreForApplication();
            if (directory.FileExists("statistics.txt"))
            {
                lbEntries.Items.Clear();
                using (var myFileStream = new IsolatedStorageFileStream("statistics.txt", FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication()))
                using (var reader = new StreamReader(myFileStream))
                {
                    var text = reader.ReadToEnd();
                    string texy = "";
                    string[] arr = text.Split(',');
                    count = 0;
                    for (int i = 0; i < arr.Length; ++i)
                    {
                        texy += arr[i] + "\n";
                        lbEntries.Items.Add(arr[i]);
                        string[] rew = arr[i].Split('-');
                        string rewData = rew[0];
                        if (rewData == "" || rewData == " " || rewData == "0") { }
                        else
                            count += Convert.ToInt32(rewData);
                    }

                    //MessageBox.Show(Convert.ToString(count));
                }
            }
            else
            {
                lbEntries.Items.Clear();
                MessageBox.Show("There is no database, let me create that for you!");
                using (var myFilestream = new IsolatedStorageFileStream("statistics.txt", FileMode.Create, IsolatedStorageFile.GetUserStoreForApplication())) ;
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCountTotal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(count.ToString());
        }

        //private void btnDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    IsolatedStorageFile directory = IsolatedStorageFile.GetUserStoreForApplication();
        //    if (directory.FileExists("statistics.txt"))
        //        directory.DeleteFile("statistics.txt");
        //}

        
    }
}