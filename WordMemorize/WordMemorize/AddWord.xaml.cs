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
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace Denemelerfalan
{
	/// <summary>
	/// AddWord.xaml etkileşim mantığı
	/// </summary>
	public partial class AddWord : Page
	{
		public AddWord()
		{
			InitializeComponent();
			
		}
		DutyMan duty = new DutyMan();
		private int id;
		private string turkishW, englishW;
		DateTime tarihKayit;
		private void TextBoxer()
		{
			turkishW = txtTurkce.Text;
			englishW = txtEnglish.Text;
			tarihKayit = DateTime.Today;

		}

		private void Cleaner()
		{
			txtEnglish.Clear();
			txtTurkce.Clear();
		}
		private void Refresh()
		{
			dataGridWords.ItemsSource = duty.Fill().DefaultView;
		}
		
		private void BtnAdd_Click(object sender, RoutedEventArgs e)
		{

			TextBoxer();

			duty.Add(englishW, turkishW, tarihKayit);
			Refresh();
			Cleaner();

		}

		private void BtnUpdate_Click(object sender, RoutedEventArgs e)
		{

			TextBoxer();
			int seviye = 0;
			duty.Update(id, englishW, turkishW, seviye, tarihKayit);
			Refresh();
			Cleaner();


		}

		private void BtnDelete_Click(object sender, RoutedEventArgs e)
		{
			duty.Delete(id);
			Refresh();
			Cleaner();
		}

		private void PageWord_Loaded(object sender, RoutedEventArgs e)
		{
			Refresh();
			
			
		}

		private void DataGridWords_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			if (dataGridWords.SelectedItem != null)
			{
				id = Convert.ToInt32(((TextBlock)dataGridWords.Columns[0].GetCellContent(dataGridWords.SelectedItem)).Text);
				txtEnglish.Text = (((TextBlock)dataGridWords.Columns[1].GetCellContent(dataGridWords.SelectedItem)).Text);
				txtTurkce.Text = (((TextBlock)dataGridWords.Columns[2].GetCellContent(dataGridWords.SelectedItem)).Text);
			}
		}
	}
}
