using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using XiDeng.Common;
using XiDeng.Models;
using XiDeng.Views.ThemeSettingViews;

namespace XiDeng.ViewModel.ThemeSettingViewModels
{
    public class ThemeListPageViewModel:BaseViewModel
    {
        private ObservableCollection<CustomTheme> themes;
        public ObservableCollection<CustomTheme> Themes
        {
            get { return themes; }
            set
            {
                themes = value;
                this.RaisePropertyChanged(nameof(Themes));
            }
        }

        public ThemeListPageViewModel()
        {
            AppearingCommand = new AsyncCommand<object>(async o=> {
                await base.Appearing(o);

                Themes = (await App.Database.GetAllAsync<CustomTheme>()).ToObservableCollection();
                
            });

            ThemeTappedCommand = new AsyncCommand<object>(async o=> {
                await this.GoAsync(nameof(ThemeSettingPage)+$"?ThemeJson={Uri.EscapeDataString(((CustomTheme)o).ToJson())}");
            });

            GotoAddThemeCommand = new AsyncCommand<object>(async o=> {
                await this.GoAsync(nameof(AddThemePage));
            },
            onException:(e)=> {
                throw e;
            });
        }
        public new AsyncCommand<object> AppearingCommand { get; set; }
        public AsyncCommand<object> ThemeTappedCommand { get; set; }
        public AsyncCommand<object> GotoAddThemeCommand { get; set; }



    }
}
