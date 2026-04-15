using System.Windows.Controls;
using WpfMVVMRZHD.ViewModels;

namespace WpfMVVMRZHD.Views.Pages; 

public partial class AdminSchedulePage : Page {
    public AdminSchedulePage(AdminScheduleViewModel adminSchedulePage) {
        InitializeComponent();
        DataContext = adminSchedulePage;
    }
}
