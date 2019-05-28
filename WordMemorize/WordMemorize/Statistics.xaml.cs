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
	/// Statistics.xaml etkileşim mantığı
	/// </summary>
	public partial class Statistics : Page
	{
		public Statistics()
		{
			InitializeComponent();
		}
		DutyMan dutyMan = new DutyMan();

		private void PageStatistics_Loaded(object sender, RoutedEventArgs e)
		{
		}

		private void ComboYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Button[] monthGraphButtons = { OcakGraph, SubatGraph, MartGraph, NisanGraph, MayisGraph, HaziranGraph, TemmuzGraph, AugustGraph, EylulGraph, EkimGraph, KasimGraph, AralikGraph };
			ResetGraph(monthGraphButtons);

			if (comboYear.SelectedItem != null)
			{
				int year = Convert.ToInt16(((ComboBoxItem)comboYear.SelectedItem).Content.ToString());
				for (int i = 1; i <= 12; i++)
				{
					float count = 0;
					int nextMonth = i + 1;
					if(i<12)
						count = dutyMan.StatisticYear(new DateTime(year, i, 01), new DateTime(year, nextMonth, 01));
					else
						count = dutyMan.StatisticYear(new DateTime(year, i, 01), new DateTime(year+1, 1, 01));

					if (count != 0f)
					{
						count *= 5.5f;
						monthGraphButtons[i-1].Visibility = Visibility.Visible;
						monthGraphButtons[i - 1].Height = count;
					}
				}
			}
		}

		private void ResetGraph(Button[] monthGraphButtons)
		{
			for(int i = 0;i<12;i++)
			{
				monthGraphButtons[i].Visibility = Visibility.Collapsed;
				monthGraphButtons[i].Height = 0;
			}
		}
	}
}
