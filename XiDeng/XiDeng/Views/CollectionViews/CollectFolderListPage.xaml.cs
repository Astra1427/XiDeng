using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XiDeng.ViewModel.CollectionViewModels;

namespace XiDeng.Views.CollectionViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectFolderListPage : ContentPage
    {
        public CollectFolderListPage()
        {
            InitializeComponent();
            this.BindingContext = new CollctFolderListPageViewModel();
        }
    }
}