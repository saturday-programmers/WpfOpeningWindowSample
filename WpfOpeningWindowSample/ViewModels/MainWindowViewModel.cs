using System.Windows.Input;


namespace WpfOpeningWindowSample.ViewModels
{
    class MainWindowViewModel
    {
        private bool isChildWIndowVisible;
        public ICommand OpenWindowCommand { get; }

        public MainWindowViewModel()
        {
            this.OpenWindowCommand = new DelegateCommand(this.OpenWindow, () => !this.isChildWIndowVisible);
        }

        private void OpenWindow()
        {
            App app = App.Current as App;
            var nextViewModel = new MoelessViewModel();
            this.isChildWIndowVisible = true;
            this.OpenWindowCommand.RaiseCanExecuteChanged();
            app.ShowView<MoelessViewModel>(nextViewModel, this.OnModelessViewClosed);
        }

        public void OnModelessViewClosed()
        {
            this.isChildWIndowVisible = false;
            this.OpenWindowCommand.RaiseCanExecuteChanged();
        }
    }
}
