using System.Windows.Controls;
using WpfMVVMRZHD.ViewModels;

namespace WpfMVVMRZHD.Views.Pages; 

public partial class SchedulePage : Page {
    public SchedulePage(ScheduleViewModel scheduleViewModel) {
        InitializeComponent();
        DataContext = scheduleViewModel;
    }
}
