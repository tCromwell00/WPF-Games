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
    public partial class GenericCard : Grid
    {
        private int _faceValue;  //1-14
        private string _symbol;  //Capitalize
        private string _suit; //Capitalize first letter: Clubs, Hearts, Diamonds, Spades
        ImageBrush faceImage=new ImageBrush();
        ImageBrush backImage=new ImageBrush();
        public bool faceDown = true;
        public bool locked = false;
        private Brush _previousFill = null;
        public GenericCard()
        {
            InitializeComponent();
        }
        public GenericCard(int fV=0, string sym="", string suit="", bool facingDown = true)
        {
            InitializeComponent();
            _faceValue = fV;
            _symbol = sym;
            _suit = suit;
            locked = false;
            if(!sym.Equals("") && !suit.Equals(""))
            {
                string cardImageURI = "pack://application:,,,/KingsCorners/Images/Cards/card" + _suit + _symbol + ".png";
                faceImage.ImageSource = new BitmapImage(new Uri(cardImageURI));
                backImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/KingsCorners/Images/Cards/cardBack_red3.png"));
                faceDown = facingDown;
                if (faceDown == true)
                {
                    g.Background = backImage;
                }
            }
            else { g.Background = Brushes.Transparent; }
            
        }

        public GenericCard(GenericCard gc)
        {
            InitializeComponent();
            this.g.Background = gc.Background;
            this._faceValue = gc._faceValue;
            this._symbol = gc._symbol;
            this._suit = gc._suit;
            this.faceDown = gc.faceDown;
            this.faceImage = gc.faceImage;
            this.backImage = gc.backImage;
            this.g.Height = gc.g.Height;
            this.g.Width = gc.g.Width;
            this.locked = gc.locked;
        }

        public int FaceValue
        {
            get { return _faceValue; }           
        }

        public string Symbol
        {
            get { return _symbol; }
        }

        public string Suit
        {
            get { return _suit; }
        }


        public void Flip()
        {
            if (faceDown == true) { faceDown = false; g.Background = faceImage; }
            else { faceDown = true;g.Background= backImage; }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData(DataFormats.StringFormat, g.Background.ToString());
                data.SetData("Int", _faceValue);
                data.SetData(DataFormats.StringFormat, _symbol);
                data.SetData(DataFormats.StringFormat, _suit);
                data.SetData("Bool", faceDown);
                data.SetData(DataFormats.StringFormat, faceImage.ToString());
                data.SetData(DataFormats.StringFormat, backImage.ToString());
                data.SetData("Double", g.Width);
                data.SetData("Double", g.Height);
                data.SetData("Object", this);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            
            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                // If the string can be converted into a Brush,
                // convert it and apply it to the ellipse.
                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    Brush newFill = (Brush)converter.ConvertFromString(dataString);
                    g.Background=newFill;
                 

                    // Set Effects to notify the drag source what effect
                    // the drag-and-drop operation had.
                    // (Copy if CTRL is pressed; otherwise, move.)
                    if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                    {
                        e.Effects = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.Move;
                    }
                }
            }
            e.Handled = true;
            
        }
        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            e.Effects = DragDropEffects.None;

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                // If the string can be converted into a Brush, allow copying or moving.
                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    // Set Effects to notify the drag source what effect
                    // the drag-and-drop operation will have. These values are
                    // used by the drag source's GiveFeedback event handler.
                    // (Copy if CTRL is pressed; otherwise, move.)
                    if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                    {
                        e.Effects = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.Move;
                    }
                }
            }
            e.Handled = true;
        }
        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            // Save the current Fill brush so that you can revert back to this value in DragLeave.
            _previousFill = g.Background;

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                // If the string can be converted into a Brush, convert it.
                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    Brush newFill = (Brush)converter.ConvertFromString(dataString.ToString());
                    g.Background = newFill;
                }
            }
        }
        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);
            // Undo the preview that was applied in OnDragEnter.
            g.Background = _previousFill;
        }



        

    }
}
