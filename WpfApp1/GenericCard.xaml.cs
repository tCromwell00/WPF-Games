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
        private int _faceValue;  //1-14
        private string _symbol;  //Capitalize
        private string _suit; //Capitalize first letter: Clubs, Hearts, Diamonds, Spades
        ImageBrush faceImage=new ImageBrush();
        ImageBrush backImage=new ImageBrush();
        public bool faceDown = true;

        public GenericCard(int fV, string sym, string suit, bool facingDown = true)
        {
            InitializeComponent();
            _faceValue = fV;
            _symbol = sym;
            _suit = suit;
            string cardImageURI = "pack://application:,,,/KingsCorners/Images/Cards/card" + _suit + _symbol+".png";
            faceImage.ImageSource = new BitmapImage(new Uri(cardImageURI));
            backImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/KingsCorners/Images/Cards/cardBack_red3.png"));
            faceDown = facingDown;
            if (faceDown == true)
            {
                Card.Fill = backImage;
            }
        }

        public void Flip()
        {
            if (faceDown == true) { faceDown = false; Card.Fill = faceImage; }
            else { faceDown = true;Card.Fill = backImage; }
        }

        
    }
}
