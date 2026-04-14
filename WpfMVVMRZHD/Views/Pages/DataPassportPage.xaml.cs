using System.Windows.Controls;
using WpfMVVMRZHD.ViewModels;

namespace WpfMVVMRZHD.Views.Pages;

public partial class DataPassportPage : Page {
    public DataPassportPage(DataPassportViewModel dataPassportViewModel) {
        InitializeComponent();
        DataContext = dataPassportViewModel;
    }
}
