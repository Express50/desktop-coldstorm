﻿using DesktopCS.ViewModels;

namespace DesktopCS.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView
    {
        public MainView()
        {
            this.InitializeComponent();
            this.DataContext = new MainViewModel();
        } 
        
        //Keep the focus on InputTextBox all the time
        private void PreviewWindow_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.InputTextBox.Focus();
        }
    }
}
