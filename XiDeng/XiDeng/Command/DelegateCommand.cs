using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace XiDeng.Command
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object > ExecuteAction { get; set; }
        public Func<object,bool> CanExecuteFunc { get; set; }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteFunc != null)
            {
                this.CanExecuteFunc(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            this.ExecuteAction?.Invoke(parameter);
        }
    }
}
