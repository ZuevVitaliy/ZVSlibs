using System;
using System.Windows.Input;

namespace ZVS.WPF.Libs.Models.Commands
{
    public class DelegateCommand : Prism.Commands.DelegateCommand
    {
        public DelegateCommand(Action executeMethod) : base(executeMethod)
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }

        public override event EventHandler CanExecuteChanged
        {
            add
            {
                base.CanExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                base.CanExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }
    }

    public class DelegateCommand<T> : Prism.Commands.DelegateCommand<T>
    {
        public DelegateCommand(Action<T> executeMethod) : base(executeMethod)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }

        public override event EventHandler CanExecuteChanged
        {
            add
            {
                base.CanExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                base.CanExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}