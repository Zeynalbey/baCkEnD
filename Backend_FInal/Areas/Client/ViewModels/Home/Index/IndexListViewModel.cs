using Backend_Final.Areas.Admin.ViewModels.Payment;
using Backend_Final.Areas.Admin.ViewModels.Slider;


namespace Backend_Final.Areas.Client.ViewModels.Home.Index
{
    public class IndexListViewModel
    {
        public List<ListViewModel> Sliders { get; set; }
        public List<PaymentListViewModel> PaymentBenefits { get; set; }
        public List<ProductListViewModel> Products { get; set; }
        public List<ClientFeedbackViewModel> ClientFeedbacks { get; set; }
    }
}
