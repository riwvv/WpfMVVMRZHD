using System.Windows.Controls;
using WpfMVVMRZHD.ViewModels;

namespace WpfMVVMRZHD.Views.Pages;

public partial class MyPurchasesPage : Page {
    public MyPurchasesPage(MyPurchasesViewModel purchasesViewModel) {
        InitializeComponent();
        DataContext = purchasesViewModel;
    }
}
