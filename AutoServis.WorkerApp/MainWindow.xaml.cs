using AutoServis.WorkerApp.Services;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace AutoServis.WorkerApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();


        Loaded += (s, e) =>
        {
            MainFrame.Navigate(new Uri("Views/LoginView.xaml", UriKind.Relative));
        };


    }
}