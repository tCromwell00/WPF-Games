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

namespace WpfApp1.Minesweeper
{
    /// <summary>
    /// Interaction logic for SelectDifficulty.xaml
    /// </summary>
    public partial class SelectDifficulty : Window
    {
        public int size;
        public int maxMines;
        public string diff;
        public SelectDifficulty()
        {
            InitializeComponent();
        }

        private void btnEasy_Click(object sender, RoutedEventArgs e)
        {
            size = 8;
            maxMines = 10;
            diff = "Easy";
            this.Close();
        }

        private void btnMedium_Click(object sender, RoutedEventArgs e)
        {
            size = 16;
            maxMines = 40;
            diff = "Medium";
            this.Close();
        }

        private void btnHard_Click(object sender, RoutedEventArgs e)
        {
            size = 24;
            maxMines = 99;
            diff = "Hard";
            this.Close();
        }
    }
}
