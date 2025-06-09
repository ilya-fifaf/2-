using System.Configuration;
using System.Data;
using System.Windows;

namespace Individual_project;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static Person CurrentUser { get; set; }
}