using System.Windows.Controls;
using WpfMVVMRZHD.ViewModels;

namespace WpfMVVMRZHD.Views.Pages;

public partial class PurchasesPage : Page {
    public PurchasesPage(PurchasesViewModel purchasesViewModel) {
        InitializeComponent();
        DataContext = purchasesViewModel;
    }
}
