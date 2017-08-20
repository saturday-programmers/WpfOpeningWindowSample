using System;
using System.Collections.Generic;
using System.Windows;

using WpfOpeningWindowSample.ViewModels;
using WpfOpeningWindowSample.Views;


namespace WpfOpeningWindowSample
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        #region インスタンス変数
   
        /// <summary></summary>
        private Dictionary<Type, Window> viewDic;

        /// <summary></summary>
        private Dictionary<Type, Action> closedEventDic;
   
        private Window mainWindow = new MainWindow();

        #endregion

        #region コンストラクタ

        public App(): base()
        {
            this.Startup += new StartupEventHandler(this.OnAppStartup);
        }

        #endregion

        #region イベントハンドラ

        void OnAppStartup(object sender, StartupEventArgs e)
        {
            this.viewDic = new Dictionary<Type, Window>();
            this.viewDic.Add(typeof(MoelessViewModel), new ModelessView());
            foreach (var item in this.viewDic)
            {
                item.Value.IsVisibleChanged += this.OnViewVisibleChanged;
            }

            this.closedEventDic = new Dictionary<Type, Action>();
            this.closedEventDic.Add(typeof(ModelessView), null);

            this.mainWindow.Show();
        }

        private void OnViewVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                this.closedEventDic[sender.GetType()]?.Invoke();
            }
        }

		#endregion

		#region メソッド

		public void ShowView<T>(T viewModel, Action closedEventHandler)
        {
            Window view;
            this.viewDic.TryGetValue(viewModel.GetType(), out view);
            if (view != null && !view.IsVisible)
            {
                view.DataContext = viewModel;
                view.Owner = this.mainWindow;
                this.closedEventDic[view.GetType()] = closedEventHandler;
                view.Show();
            }
        }

        public void CloseView(Type viewModelType)
        {
            Window view;
            this.viewDic.TryGetValue(viewModelType, out view);
            view?.Hide();
        }

		#endregion

	}
}
