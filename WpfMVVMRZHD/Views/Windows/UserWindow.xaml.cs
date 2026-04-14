using System.Windows;
using WpfMVVMRZHD.ViewModels;

namespace WpfMVVMRZHD.Views.Windows;

public partial class UserWindow : Window {
    public UserWindow(UserViewModel userViewModel) {
        InitializeComponent();
        DataContext = userViewModel;
    }
}
