using System.Windows.Input;


namespace WpfOpeningWindowSample.ViewModels
{
    class MoelessViewModel
    {
		#region プロパティ

		public ICommand CloseCommand { get; }

		#endregion

		#region コンストラクタ

		public MoelessViewModel()
        {
            this.CloseCommand = new DelegateCommand(this.CloseWindow);
        }

		#endregion

		#region メソッド

		private void CloseWindow()
        {
            App app = App.Current as App;
            app.CloseView(this.GetType());
        }

		#endregion
	}
}
