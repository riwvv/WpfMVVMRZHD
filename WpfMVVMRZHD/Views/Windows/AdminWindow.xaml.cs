using System.Windows;
using WpfMVVMRZHD.ViewModels;

namespace WpfMVVMRZHD.Views.Windows;

public partial class AdminWindow : Window {
    public AdminWindow(AdminViewModel adminViewModel) {
        InitializeComponent();
        DataContext = adminViewModel;
    }
}
