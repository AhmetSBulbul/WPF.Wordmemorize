using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Denemelerfalan
{
	/// <summary>
	/// MainWindow.xaml etkileşim mantığı
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		DutyMan dutyMan = new DutyMan();

		private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonCloseMenu.Visibility = Visibility.Visible;
			ButtonOpenMenu.Visibility = Visibility.Collapsed;

		}

		private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonCloseMenu.Visibility = Visibility.Collapsed;
			ButtonOpenMenu.Visibility = Visibility.Visible;

		}

		private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			switch(((ListViewItem)((ListView)sender).SelectedItem).Name)
			{
				case "ItemMyWords":
					MyWords myWords = new MyWords();
					frameMain.Navigate(myWords);
					break;
				case "ItemAddWord":
					AddWord addWord = new AddWord();
					frameMain.Navigate(addWord);
					break;
				case "ItemLearned":
					LearnedWords learnedWords = new LearnedWords();
					frameMain.Navigate(learnedWords);

					break;
				case "ItemStatistics":
					Statistics statistics = new Statistics();
					frameMain.Navigate(statistics);
					break;

			}
		}

		
		private void Btn_Exit_Click_1(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void AnaSayfa_Loaded(object sender, RoutedEventArgs e)
		{
			if(dutyMan.TakeQuestions().Count != 0)
			{
				Questions questions = new Questions();
				questions.ShowDialog();
			}
			
			
		}
	}
}
