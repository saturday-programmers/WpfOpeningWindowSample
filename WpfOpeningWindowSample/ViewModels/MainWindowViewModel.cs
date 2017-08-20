using System.Windows.Input;


namespace WpfOpeningWindowSample.ViewModels
{
    class MainWindowViewModel
    {
        #region インスタンス変数

        private bool isChildWindowVisible;

        #endregion

        #region プロパティ
 
        public ICommand OpenWindowCommand { get; }

        private bool IsChildWindowVisible
        {
            get { return this.isChildWindowVisible; }
            set {
                this.isChildWindowVisible = value;
                this.OpenWindowCommand.RaiseCanExecuteChanged();
            }
        }

		#endregion

		#region コンストラクタ

		public MainWindowViewModel()
        {
            this.OpenWindowCommand = new DelegateCommand(this.OpenWindow, () => !this.isChildWindowVisible);
        }

		#endregion

		#region メソッド

		public void OnModelessViewClosed()
		{
			this.IsChildWindowVisible = false;
		}

		private void OpenWindow()
        {
            App app = App.Current as App;
            var nextViewModel = new MoelessViewModel();
            this.IsChildWindowVisible = true;
            app.ShowView<MoelessViewModel>(nextViewModel, this.OnModelessViewClosed);
        }

		#endregion
	}
}
