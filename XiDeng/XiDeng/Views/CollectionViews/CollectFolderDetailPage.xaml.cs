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
    [QueryProperty("FolderId","FolderId")]
    public partial class CollectFolderDetailPage : ContentPage
    {
        private Guid folderId;
        private CollectFolderDetailPageViewModel vm { get; set; }
        public string FolderId
        {
            set {

                if (!Guid.TryParse(value, out folderId))
                {
                    folderId = Guid.Empty;
                }
                vm = new CollectFolderDetailPageViewModel(folderId);
            }
        }

        public CollectFolderDetailPage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await vm.Load(folderId);

            this.BindingContext = vm;
            if (!vm.IsOwner)
            {
                this.ToolbarItems.RemoveAt(0);
                this.ToolbarItems.RemoveAt(0);
            }
        }

    }
}