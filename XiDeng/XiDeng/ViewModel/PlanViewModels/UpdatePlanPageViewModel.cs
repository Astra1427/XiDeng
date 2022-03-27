using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XiDeng.Models.ExercisePlanModels;

namespace XiDeng.ViewModel.PlanViewModels
{
    public class UpdatePlanPageViewModel:PlanViewModel
    {
        public UpdatePlanPageViewModel(ExercisePlanDTO plan):base(plan)
        {

            SelectCoverCommand = new Command<object>(async delegate {
                var fileResult = await FilePicker.PickAsync(PickOptions.Images);
                if (fileResult != null)
                {
                    var stream = await fileResult.OpenReadAsync();
                    Cover = ImageSource.FromStream(() => stream);
                }
            });
            CancelCommand = new Command<object>(async delegate {
                await Shell.Current.GoToAsync("../");
            });

        }

        public Command<object> CancelCommand { get; set; }
        public Command<object> SelectCoverCommand { get; set; }
    }
}
