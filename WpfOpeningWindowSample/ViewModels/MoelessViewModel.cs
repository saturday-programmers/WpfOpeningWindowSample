using System.Windows.Input;


namespace WpfOpeningWindowSample.ViewModels
{
    class MoelessViewModel
    {
        public ICommand CloseCommand { get; }

        public MoelessViewModel()
        {
            this.CloseCommand = new DelegateCommand(this.CloseWindow);
        }

        private void CloseWindow()
        {
            App app = App.Current as App;
            app.CloseView(this.GetType());
        }
    }
}
