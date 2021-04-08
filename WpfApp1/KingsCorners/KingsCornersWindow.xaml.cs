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
        List<GenericCard> deck;
        public KingsCornersWindow()
        {
            InitializeComponent();
            GenericCard gc1 = new GenericCard(10, "10", "Clubs");
          
            Grid.SetRow(gc1, 4);
            
            gridBoard.Children.Add(gc1);
            GenericCardDeck cards = new GenericCardDeck();
            deck = cards.deck;
            MyExtensions.Shuffle(deck);
            Console.WriteLine(deck.ToString());
            foreach (GenericCard gc in deck)
            {
                //gc.Flip();
                
                spCards.Children.Add(gc);
            }
            GenericCard f = (GenericCard)spCards.Children[0];
            f.Flip();
            
        }



        private void Panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                // These Effects values are used in the drag source's
                // GiveFeedback event handler to determine which cursor to display.
                if (e.KeyStates == DragDropKeyStates.ControlKey)
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.Move;
                }
            }
        }
        private void Panel_Drop(object sender, DragEventArgs e)
        {
            // If an element in the panel has already handled the drop,
            // the panel should not also handle it.
            if (e.Handled == false)
            {
                
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");
                if (((_panel.GetValue(Grid.RowProperty).Equals(0) && _panel.GetValue(Grid.ColumnProperty).Equals(0))|| (_panel.GetValue(Grid.RowProperty).Equals(0) && _panel.GetValue(Grid.ColumnProperty).Equals(3)) || (_panel.GetValue(Grid.RowProperty).Equals(3) && _panel.GetValue(Grid.ColumnProperty).Equals(0)) || (_panel.GetValue(Grid.RowProperty).Equals(3) && _panel.GetValue(Grid.ColumnProperty).Equals(3))))
                {

                }

                if (_panel != null && _element != null)
                {
                    // Get the panel that the element currently belongs to,
                    // then remove it from that panel and add it the Children of
                    // the panel that its been dropped on.
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                        if (e.KeyStates == DragDropKeyStates.ControlKey &&
                            e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            GenericCard _gc = new GenericCard((GenericCard)_element);
                            _panel.Children.Add(_gc);
                            // set the value to return to the DoDragDrop call
                            e.Effects = DragDropEffects.Copy;
                        }
                        else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            _parent.Children.Remove(_element);
                            _panel.Children.Add(_element);
                            // set the value to return to the DoDragDrop call
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
            GenericCard f = (GenericCard)spCards.Children[0];
            spCards.Children.Remove(f);
            deck.Remove(f);
            GenericCard h = (GenericCard)spCards.Children[0];
            h.Flip();



        }
    }



}
