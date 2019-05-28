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
	/// LearnedWords.xaml etkileşim mantığı
	/// </summary>
	public partial class LearnedWords : Page
	{
		public LearnedWords()
		{
			InitializeComponent();
		}
		DutyMan dutyMan = new DutyMan();
		private void PageLearnedWords_Loaded(object sender, RoutedEventArgs e)
		{
			dataGridWordsMy.ItemsSource = dutyMan.FillLearned().DefaultView;
		}
	}
}
