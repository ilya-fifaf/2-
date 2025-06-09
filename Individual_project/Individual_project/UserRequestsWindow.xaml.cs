using System.Collections.Generic;
using System.Windows;

namespace Individual_project
{
    public partial class UserRequestsWindow : Window
    {
        public UserRequestsWindow(List<UserRequest> requests)
        {
            InitializeComponent();
            if (requests == null || requests.Count == 0)
            {
                EmptyText.Visibility = Visibility.Visible;
                RequestsList.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmptyText.Visibility = Visibility.Collapsed;
                RequestsList.ItemsSource = requests;
            }
        }
    }
}