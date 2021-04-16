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
        private bool _matchingMode;
        private GenericCard[] clickedCards = new GenericCard[2];
        List<GenericCard> deck;
        GenericCard[,] CardsInPlay = new GenericCard[4,4]; 
        Grid[,] spots = new Grid[4, 4];
        public KingsCornersWindow()
        {
            InitializeComponent(); 
            GenericCardDeck cards = new GenericCardDeck();
            deck = cards.deck;
            MyExtensions.Shuffle(deck);
            foreach (GenericCard gc in deck)
            {
                gc.Visibility = Visibility.Collapsed;
                spCards.Children.Add(gc);               
            }
            ShowNextCardInPile();
            _matchingMode = false;
        }

        private void ShowNextCardInPile()
        {
            GenericCard f = (GenericCard)spCards.Children[0];
            f.Visibility = Visibility.Visible;
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
                if (_matchingMode == false)
                {
                    lblWarning.Visibility = Visibility.Hidden;
                    Panel _panel = (Panel)sender;
                    UIElement _element = (UIElement)e.Data.GetData("Object");

                    GenericCard _gcDragging = new GenericCard((GenericCard)_element);

                    if (_panel != null && _element != null)
                    {
                        // Get the panel that the element currently belongs to,
                        // then remove it from that panel and add it the Children of
                        // the panel that its been dropped on.
                        Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                        if (_parent != null)
                        {
                            //placing new cards on the board
                            if (_parent == spCards)
                            {
                                
                                if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                                {
                                    if (_gcDragging.Symbol.Equals("K"))
                                    {
                                        //Kings
                                        if (((_panel.GetValue(Grid.RowProperty).Equals(0) && _panel.GetValue(Grid.ColumnProperty).Equals(0)) || (_panel.GetValue(Grid.RowProperty).Equals(0) && _panel.GetValue(Grid.ColumnProperty).Equals(3)) || (_panel.GetValue(Grid.RowProperty).Equals(3) && _panel.GetValue(Grid.ColumnProperty).Equals(0)) || (_panel.GetValue(Grid.RowProperty).Equals(3) && _panel.GetValue(Grid.ColumnProperty).Equals(3))))
                                        {
                                            _gcDragging.locked = true;
                                            _parent.Children.Remove(_element);
                                            int column = (int)_panel.GetValue(Grid.ColumnProperty);
                                            int row = (int)_panel.GetValue(Grid.RowProperty);
                                            CardsInPlay[column, row] = _gcDragging;
                                            _panel.Children.Add(_gcDragging);
                                            
                                            ShowNextCardInPile();
                                        }
                                    }

                                    if (_gcDragging.Symbol.Equals("Q"))
                                    {
                                        if (((_panel.GetValue(Grid.RowProperty).Equals(0) && _panel.GetValue(Grid.ColumnProperty).Equals(1)) || (_panel.GetValue(Grid.RowProperty).Equals(0) && _panel.GetValue(Grid.ColumnProperty).Equals(2)) || (_panel.GetValue(Grid.RowProperty).Equals(3) && _panel.GetValue(Grid.ColumnProperty).Equals(1)) || (_panel.GetValue(Grid.RowProperty).Equals(3) && _panel.GetValue(Grid.ColumnProperty).Equals(2))))
                                        {

                                            _gcDragging.locked = true;
                                            _parent.Children.Remove(_element);
                                            _panel.Children.Add(_gcDragging);
                                            int column = (int)_panel.GetValue(Grid.ColumnProperty);
                                            int row = (int)_panel.GetValue(Grid.RowProperty);
                                            CardsInPlay[column, row] = _gcDragging;
                                            ShowNextCardInPile();

                                        }
                                    }

                                    //Jacks
                                    if (_gcDragging.Symbol.Equals("J"))
                                    {
                                        if (((_panel.GetValue(Grid.RowProperty).Equals(1) && _panel.GetValue(Grid.ColumnProperty).Equals(0)) || (_panel.GetValue(Grid.RowProperty).Equals(1) && _panel.GetValue(Grid.ColumnProperty).Equals(3)) || (_panel.GetValue(Grid.RowProperty).Equals(2) && _panel.GetValue(Grid.ColumnProperty).Equals(0)) || (_panel.GetValue(Grid.RowProperty).Equals(2) && _panel.GetValue(Grid.ColumnProperty).Equals(3))))
                                        {
                                            _gcDragging.locked = true;
                                            _parent.Children.Remove(_element);

                                            _panel.Children.Add(_gcDragging);
                                            int column = (int)_panel.GetValue(Grid.ColumnProperty);
                                            int row = (int)_panel.GetValue(Grid.RowProperty);
                                            CardsInPlay[column, row] = _gcDragging;
                                            ShowNextCardInPile();
                                        }
                                    }
                                    //Empty
                                    else
                                    {
                                         if(!(_gcDragging.Symbol.Equals("K")) && !(_gcDragging.Symbol.Equals("Q")) && !(_gcDragging.Symbol.Equals("J")))
                                         {
                                             _parent.Children.Remove(_element);
                                             _panel.Children.Add(_gcDragging);
                                            int column = (int)_panel.GetValue(Grid.ColumnProperty);
                                            int row = (int)_panel.GetValue(Grid.RowProperty);
                                            CardsInPlay[column, row] = _gcDragging;
                                            ShowNextCardInPile();
                                         }

                                    }
                                    
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Move;

                                    // Need to add logic for checking if there are still playable moves.
                                    if (IsBoardFilled() == true)
                                    {
                                        _matchingMode = true;
                                    }
                                }
                            }
                            else if(VisualTreeHelper.GetParent(_parent).Equals(gridBoard))
                            {
                                lblWarning.Visibility = Visibility.Visible;
                            }

                        }
                    }
                }
            }
        }

        private void CardClick(object sender, MouseEventArgs e)
        {

            if (_matchingMode == true)
            {
                if (clickedCards[0] == null && clickedCards[1] == null)
                {
                    clickedCards[0] = sender as GenericCard;
                }
                else if (clickedCards[0] != null && clickedCards[1] == null)
                {
                    if (clickedCards[0].Symbol.Equals("10"))
                    {
                        Discard();
                    }
                    else
                    {
                        clickedCards[1] = sender as GenericCard;
                        CheckPair();
                    }
                }
            }
        }


        private void IsBoardPlayable()
        {
            //Check to see if there are still playable moves. 
        }

        private void Discard()
        {
            if (clickedCards[0] != null) 
            { 
                cnvDiscard.Children.Add(clickedCards[0]);
                int x = (int)clickedCards[0].GetValue(Grid.ColumnProperty);
                int y = (int)clickedCards[0].GetValue(Grid.RowProperty);
                foreach(Grid a in gridBoard.Children)
                {
                    if (a.Children.Contains(CardsInPlay[x, y]))
                    {
                        a.Children.Remove(CardsInPlay[x, y]);
                        CardsInPlay[x, y] = null;
                        clickedCards[0] = null;
                        break;
                    }
                }
            }
            if (clickedCards[1] != null) 
            {
                cnvDiscard.Children.Add(clickedCards[1]);
                int x = (int)clickedCards[1].GetValue(Grid.ColumnProperty);
                int y = (int)clickedCards[1].GetValue(Grid.RowProperty);
                foreach (Grid a in gridBoard.Children)
                {
                    if (a.Children.Contains(CardsInPlay[x, y]))
                    {
                        a.Children.Remove(CardsInPlay[x, y]);
                        CardsInPlay[x, y] = null;
                        clickedCards[1] = null;
                        break;
                    }
                }
            }

        }
        private void CheckPair()
        {
            var a = clickedCards[0].FaceValue;
            var b = clickedCards[1].FaceValue;
            if (a + b == 10)
            {
                Discard();
            }
            else
            {
                clickedCards[0] = null;
                clickedCards[1] = null;
            }
            
        }

        private bool IsBoardFilled()
        {
            int f = 0;
            foreach (GenericCard g in CardsInPlay)
            {
                if (g != null) { f++; }
            }
            if (f == 16) { return true; }
            return false;
        }
        
    }
   



}
