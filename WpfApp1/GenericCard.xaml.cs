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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for GenericCard.xaml
    /// </summary>
    public partial class GenericCard : UserControl
    {
        private int faceValue;
        private int specialValue;
        private int suit;
        private string faceURI;
        private string backURI;
        public GenericCard(int fV, int s)
        {
            InitializeComponent();
            this.faceValue = fV;
            this.suit = s;
        }
        //public GenericCard(int fV, int s, int sV)
    }
}
