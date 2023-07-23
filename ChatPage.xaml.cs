using Firebase.Auth;
using Firebase.Auth.UI;
using SecretMessage.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecretMessage
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Window
    {
        private static ChatPage instance;

        public ChatPage()
        {
            InitializeComponent();
            instance = this;
            var currentUser = FirebaseUI.Instance.Client.User;
            lbUsername.Content = currentUser.Info.DisplayName;
            avatar.ImageSource = new BitmapImage(new Uri(currentUser.Info.PhotoUrl));
            lvContacts.SelectedItem = lvContacts.Items[0];
            var selectedContact = (ContactModel)lvContacts.SelectedItem;
            lbUsernameFriend.Content = selectedContact.Username;
        }

        public static ChatPage GetInstance()
        {
            return instance ?? (instance = new ChatPage());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            instance = null;
            this.Close();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow("type");
            main.Show();
            instance = null;
            this.Close();

        }

        private void ListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (lvMessages.Items.Count > 0)
            {
                var border = (Border)VisualTreeHelper.GetChild(lvMessages, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedContact = (ContactModel)lvContacts.SelectedItem;
            lbUsernameFriend.Content = selectedContact.Username;
            if (lvMessages.Items.Count > 0)
            {
                var border = (Border)VisualTreeHelper.GetChild(lvMessages, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (lvMessages.Items.Count > 0)
                {
                    var border = (Border)VisualTreeHelper.GetChild(lvMessages, 0);
                    var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                    scrollViewer.ScrollToBottom();
                }
            }
        }
    }
}
