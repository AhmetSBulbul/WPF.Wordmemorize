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
using System.Windows.Shapes;

namespace Denemelerfalan
{
    /// <summary>
    /// Questions.xaml etkileşim mantığı
    /// </summary>
    public partial class Questions : Window
    {
        public Questions()
        {
            InitializeComponent();
        }
		
		static DutyMan dutyMan = new DutyMan();
		static List<Sorular> sorulars = dutyMan.TakeQuestions();
		int totalQuestionCount = sorulars.Count;
		int nowQuestionNumber = 0;
		static List<String> rndAnswers = dutyMan.RndAnswers();
		int totalAnswerCount = rndAnswers.Count;


		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void Questions_Loaded(object sender, RoutedEventArgs e)
		{
			SoruGetirici();
		}

		private void A_Click(object sender, RoutedEventArgs e)
		{
			Button btnCevap = (Button)sender;

			A.IsEnabled = false;
			B.IsEnabled = false;
			C.IsEnabled = false;
			D.IsEnabled = false;
			E.IsEnabled = false;

			if ((string)btnCevap.Content == sorulars[nowQuestionNumber].turkishWorld)
			{
				btnCevap.Background = Brushes.Green;
				Skip.Visibility = Visibility.Collapsed;
				Correct.Visibility = Visibility.Visible;
				sorulars[nowQuestionNumber].seviye++;

				dutyMan.Update(sorulars[nowQuestionNumber].id, sorulars[nowQuestionNumber].englishWord,
					sorulars[nowQuestionNumber].turkishWorld, sorulars[nowQuestionNumber].seviye, DateTime.Today);
			
			}
			else
			{
				btnCevap.Background = Brushes.Red;
				int level = 0;
				dutyMan.Update(sorulars[nowQuestionNumber].id, sorulars[nowQuestionNumber].englishWord,
					sorulars[nowQuestionNumber].turkishWorld, level, DateTime.Today);
			}

			

		}
		private void SoruGetirici()
		{
			
			Soru.Text = sorulars[nowQuestionNumber].englishWord;
			Random rnd = new Random();
			int rndNumber = rnd.Next(1, 5);
			
			switch (rndNumber)
			{
				case 1:
					Quiz(A, B, C, D, E);
					break;
				case 2:
					Quiz(B, A, C, D, E);
					break;
				case 3:
					Quiz(C, B, A, D, E);
					break;
				case 4:
					Quiz(D, B, A, C, E);
					break;
				case 5:
					Quiz(E, B, A, C, D);
					break;
			}

		}

		private void Correct_Click(object sender, RoutedEventArgs e)
		{
			Correct.Visibility = Visibility.Collapsed;
			Skip.Visibility = Visibility.Visible;
			BtnCleaner();
			
			nowQuestionNumber++;
			if (nowQuestionNumber < totalQuestionCount)
			{
				SoruGetirici();
			}
			else
			{
				Close();
			}
			
		}
		private void BtnCleaner()
		{
			A.Background = Brushes.Orange;
			B.Background = Brushes.Orange;
			C.Background = Brushes.Orange;
			D.Background = Brushes.Orange;
			E.Background = Brushes.Orange;

			A.IsEnabled = true;
			B.IsEnabled = true;
			C.IsEnabled = true;
			D.IsEnabled = true;
			E.IsEnabled = true;
		}


		private void Quiz(Button a, Button b, Button c, Button d, Button e)
		{
			int[] rndAnsNo = IsThatOkay();

			a.Content = sorulars[nowQuestionNumber].turkishWorld;
			b.Content = rndAnswers[rndAnsNo[0]];
			c.Content = rndAnswers[rndAnsNo[1]];
			d.Content = rndAnswers[rndAnsNo[2]];
			e.Content = rndAnswers[rndAnsNo[3]];
		}

		private int[] IsThatOkay()
		{
			int[] rndNumbers = new int[4];
			Random rnd = new Random();
			for(int i = 0; i<4; i++)
			{
				int rndTempNo = rnd.Next(0, totalAnswerCount);
				for(int z = 0; z < rndNumbers.Length; z++)
				{
					if((sorulars[nowQuestionNumber].turkishWorld == rndAnswers[rndTempNo]) || (rndNumbers[z] == rndTempNo))
					{
						do
						{
							rndTempNo = rnd.Next(0, totalAnswerCount);
						}
						while((sorulars[nowQuestionNumber].turkishWorld == rndAnswers[rndTempNo]) || (rndNumbers[z] == rndTempNo));
					}
				}
				rndNumbers[i] = rndTempNo;
			}

			
			

			return rndNumbers;


		}

		private void Skip_Click(object sender, RoutedEventArgs e)
		{
			BtnCleaner();
			nowQuestionNumber++;
			if (nowQuestionNumber < totalQuestionCount)
			{
				SoruGetirici();
			}
			else
			{
				Close();
			}
		}
	}
}
