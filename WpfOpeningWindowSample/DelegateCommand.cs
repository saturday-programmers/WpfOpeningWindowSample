using System;
using System.Collections.Generic;
using System.Windows.Input;


namespace WpfOpeningWindowSample
{
    /// <summary>
    /// 以下のサイトからコピーしたクラス。
    /// http://blog.okazuki.jp/entry/20120421/1335015512
    /// </summary>
    class DelegateCommand : ICommand
    {
        private static readonly Action EmptyExecute = () => { };
        private static readonly Func<bool> EmptyCanExecute = () => true;

        private Action execute;
        private Func<bool> canExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? EmptyExecute;
            this.canExecute = canExecute ?? EmptyCanExecute;
        }

        public void Execute()
        {
            this.execute();
        }

        public bool CanExecute()
        {
            return this.canExecute();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute();
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            var h = this.CanExecuteChanged;
            if (h != null)
            {
                h(this, EventArgs.Empty);
            }
        }

        void ICommand.Execute(object parameter)
        {
            this.Execute();
        }
    }

    static class CommandExetensions
    {
        public static void RaiseCanExecuteChanged(this ICommand self)
        {
            var delegateCommand = self as DelegateCommand;
            if (delegateCommand == null)
            {
                return;
            }

            delegateCommand.RaiseCanExecuteChanged();
        }

        public static void RaiseCanExecuteChanged(this IEnumerable<ICommand> self)
        {
            foreach (var command in self)
            {
                command.RaiseCanExecuteChanged();
            }
        }

        public static void RaiseCanExecuteChanged(params ICommand[] commands)
        {
            commands.RaiseCanExecuteChanged();
        }
    }
}
