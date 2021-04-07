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

namespace WpfApp1.KingsCorners
{
    /// <summary>
    /// Interaction logic for KingsCornersWindow.xaml
    /// </summary>
    public partial class KingsCornersWindow : Window
    {
        public KingsCornersWindow()
        {
            InitializeComponent();
            GenericCard gc = new GenericCard(10, "10", "Clubs");
            
        }
    }
}
