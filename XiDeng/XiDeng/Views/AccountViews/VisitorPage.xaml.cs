using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel.AccountViewModels;

namespace XiDeng.Views.AccountViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("AuthorId", "AuthorId")]
    public partial class VisitorPage : ContentPage
    {
        private Guid authorId;

        public string AuthorId
        {
            
            set {
                Guid.TryParse(value,out authorId);
                this.BindingContext = new VisitorPageViewModel(authorId);
            }
        }

        public VisitorPage()
        {
            InitializeComponent();
        }
    }
}