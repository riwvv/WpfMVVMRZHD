using System.Windows.Controls;
using WpfMVVMRZHD.ViewModels;

namespace WpfMVVMRZHD.Views.Pages;

public partial class AddSchedulePage : Page {
    public AddSchedulePage(AddScheduleViewModel addScheduleViewModel) {
        InitializeComponent();
        DataContext = addScheduleViewModel;
    }
}
