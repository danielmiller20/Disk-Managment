﻿using System;
using System.IO;
using Microsoft.Win32;
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
using System.Windows.Markup;
using Syncfusion.Windows.Tools.Controls;
using Syncfusion.Windows.Tools;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using Syncfusion.Windows.Shared;
using System.Globalization;
using System.Windows.Threading;
using System.Windows.Interop;
using FMS_adapter;
using System.Runtime.CompilerServices;

namespace FMS_GUI
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        UserControl info;
        public CreateOpenDiskPage codp { get; set; }
        public NewUserUserControl nuuc { get; set; }
        public SignUserControl suc { get; set; }
        public UserControl Info {
            get { return info; }
            set
            {
                info = value;
                if (info is DiskInfoUserControl)
                {
                    this.InfoContentControl.Content = info;
                }
            }
        }
       

        public Disk disk { get; set; }
        public string DiskName { get; set; }
        public MainPage()
        {
            InitializeComponent();
            disk = new Disk();
            //Info = new DiskInfoUserControl(disk);

            this.InfoContentControl.DataContext = this;
            
        }

        #region Disk

        private void CreateDiskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (disk.Mounted)
                    throw new Exception("You must unmount the current disk first!");
                this.shadowRectangle.Visibility = Visibility.Visible;
                codp = new CreateOpenDiskPage();
                codpContentControl.Content = codp;
                this.codpContentControl.DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void MountDiskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
                f.Filter = "All Files (*)|*|DISK Files (*.fms)|*.fms";
                if (f.ShowDialog() == true)
                {
                    //var a = new Uri(f.FileName);
                    disk.mountDisk(f.FileName);
                    disk.Mounted = true;
                    DiskName = disk.getVHD().DiskName;
                    this.MyRibbon.BackStageHeader = DiskName;
                    suc = new SignUserControl(DiskName);
                    transitionFrame.ShowPage(suc);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void FormatButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
                string diskName = "Disk Name";
                MessageBoxResult format = MessageBox.Show(
                    "WARNING: Formatting will erase ALL data on this disk.\nTo format the disk, click OK. To quit, click CANCEL.",
                    "Format " + diskName, MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel, MessageBoxOptions.None);

                switch (format)
                {
                    case MessageBoxResult.OK:
                        disk.format();
                        var inf = new DiskInfoUserControl(disk);
                        Info = inf;
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
                disk.signOut();
                suc = new SignUserControl(DiskName);
                transitionFrame.ShowPage(suc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UnmountDiskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
                disk.signOut();
                disk.unmountDisk();
                disk.Mounted = false;

                var inf = new DiskInfoUserControl(disk);
                Info = inf;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region File

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Record

        private void AddRecordButoon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region User

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
                this.shadowRectangle.Visibility = Visibility.Visible;
                nuuc = new NewUserUserControl();
                codpContentControl.Content = nuuc;
                this.codpContentControl.DataContext = this;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SignOutButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!disk.Mounted)
                    throw new Exception("No disk is mounted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

    }
}
