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

namespace wpfZad3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int ilosc;
        private double rabat, rabatText;
        private double sum, postRabat;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (amount.Text.Length > 0)
            {
                if (!int.TryParse(amount.Text, out (this.ilosc)))
                {
                    MessageBox.Show("Wpisz liczbę!");
                    finalText.Text = "";
                    return;
                }
                this.sum = (double)this.ilosc;
                checkRabat(this.ilosc);
                
            }
            else finalText.Text = "";
            update();
        }

        private void update()
        {
            getSum();
            UpdateText();
        }

        

        private void checkRabat(int am)
        {
            if (am / 100 > 10)
            {
                this.rabatText = 10;
                this.rabat = 0.9;
                return;
            }
            else if (am / 100 < 1)
            {
                this.rabatText = 0;
                this.rabat = 0;
                return;
            }
            else
            {
                double t = am/100;
                this.rabatText = t;
                this.rabat = 1 - t/100;
            }

        }

        private void colorPaper_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)colorPaper.IsChecked)
            {
                colorPicker.IsEnabled = true;
                update();
            }
            else
            {
                update();
                colorPicker.IsEnabled = false;
            }
        }

        private void UpdateText()
        {
            finalText.Text = "Przedmiot zamówienia: " + amount.Text + " szt., format ";
            switch (format.Value)
            {
                case 0:
                    finalText.Text += "A5, ";
                    break;
                case 1:
                    finalText.Text += "A4, ";
                    break;
                case 2:
                    finalText.Text += "A3, ";
                    break;
                case 3:
                    finalText.Text += "A2, ";
                    break;
                case 4:
                    finalText.Text += "A1, ";
                    break;
                case 5:
                    finalText.Text += "A0, ";
                    break;
            }

            if ((bool)colorPaper.IsChecked)
                finalText.Text += "papier " + colorPicker.Text + ", ";

            if ((bool)gr80.IsChecked) finalText.Text += "80g/m², ";
            if ((bool)gr120.IsChecked) finalText.Text += "120g/m², ";
            if ((bool)gr200.IsChecked) finalText.Text += "200g/m², ";
            if ((bool)gr240.IsChecked) finalText.Text += "240g/m², ";

            if ((bool)colorPrint.IsChecked || (bool)twoSides.IsChecked || (bool)UV.IsChecked)
            {
                finalText.Text += "druk: ";

                if ((bool)colorPrint.IsChecked)
                {
                    finalText.Text += "kolor";
                    if ((bool)twoSides.IsChecked || (bool)UV.IsChecked || (bool)express.IsChecked)
                        finalText.Text += ", ";
                }
                if ((bool)twoSides.IsChecked)
                {
                    finalText.Text += "dwustronny";
                    if ((bool)UV.IsChecked || (bool)express.IsChecked)
                        finalText.Text += ", ";
                }
                if ((bool)UV.IsChecked)
                {
                    finalText.Text += "UV";
                    if ((bool)express.IsChecked)
                        finalText.Text += ", ";
                }
            }
            if ((bool)express.IsChecked)
                finalText.Text += "ekspres";
            finalText.Text += "\n";
            finalText.Text += "Cena przed rabatem: " + Math.Round(this.sum, 2) + "zł\n";
            finalText.Text += "Naliczony rabat: " + this.rabatText + "%\n";
            finalText.Text += "Cena po rabacie: " + Math.Round(this.postRabat, 2) + "zł";
        }

        private void colorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(colorPicker.SelectedIndex)
            {
                case 0:
                    colorPicker.Text = "Żółty"; break;
                case 1:
                    colorPicker.Text = "Zielony"; break;
                case 2:
                    colorPicker.Text = "Niebieski"; break;
                case 3:
                    colorPicker.Text = "Czerwony"; break;
            }
            update();
        }

        private void gr_Click(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Złożono zamówienie!");
            amount.Text = "";
            format.Value = 0;
            colorPaper.IsChecked = false;
            colorPicker.Text = "";
            gr80.IsChecked = false;
            gr120.IsChecked = true;
            gr200.IsChecked = false;
            gr240.IsChecked = false;
            colorPrint.IsChecked = true;
            twoSides.IsChecked = false;
            UV.IsChecked = false;
            standard.IsChecked = true;
            express.IsChecked = false;
            finalText.Text = "";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void getSum()
        {
            this.sum = (double)this.ilosc * (0.2 * Math.Pow(2.5, (double)format.Value));
            if ((bool)colorPaper.IsChecked) this.sum *= 1.5;
            if ((bool)gr120.IsChecked) this.sum *= 2;
            if ((bool)gr200.IsChecked) this.sum *= 2.5;
            if ((bool)gr240.IsChecked) this.sum *= 3;
            if ((bool)colorPrint.IsChecked) this.sum *= 1.3;
            if ((bool)twoSides.IsChecked) this.sum *= 1.5;
            if ((bool)UV.IsChecked) this.sum *= 1.2;
            if ((bool)express.IsChecked) this.sum += 15;
            this.postRabat = this.sum * this.rabat;
        }

        private void format_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            switch (format.Value)
            {
                case 0:
                    paperSize.Content = "A5 - cena 20gr/szt. ";
                    break;
                case 1:
                    paperSize.Content = "A4 - cena 50gr/szt. ";
                    break;
                case 2:
                    paperSize.Content = "A3 - cena 1,25zł/szt. ";
                    break;
                case 3:
                    paperSize.Content = "A2 - cena 3,12zł/szt. ";
                    break;
                case 4:
                    paperSize.Content = "A1 - cena 7,81zł/szt. ";
                    break;
                case 5:
                    paperSize.Content = "A0 - cena 19,53zł/szt. ";
                    break;
            }
            update();
        }
    }
}
