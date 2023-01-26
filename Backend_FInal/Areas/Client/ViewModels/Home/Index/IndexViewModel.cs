using Backend_Final.Areas.Admin.ViewModels.Payment;
using Backend_Final.Areas.Admin.ViewModels.Slider;


namespace Backend_Final.Areas.Client.ViewModels.Home.Index
{
    public class IndexViewModel
    {
        public List<PaymentListViewModel> Payments { get; set; }
        public List<ListViewModel>? Sliders { get; set; }
    }
}
