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
        private bool testing = true;
        
        List<GenericCard> deck;
        GenericCard[,] CardsInPlay = new GenericCard[4, 4];
        
        /// <summary>
        /// 
        /// </summary>
        public KingsCornersWindow()
        {
            InitializeComponent();
            txtRules.Text = KCstrings.KCrules;
            GenericCardDeck cards = new GenericCardDeck();
            deck = cards.deck;
            if (testing == false)
            {
                MyExtensions.Shuffle(deck);
            }
            foreach (GenericCard gc in deck)
            {
                gc.Visibility = Visibility.Collapsed;

                spCards.Children.Add(gc);
            }
            ShowNextCardInPile();
            _matchingMode = false;
        }
        /// <summary>
        /// 
        /// </summary>
        private void ShowNextCardInPile()
        {
            GenericCard f = (GenericCard)spCards.Children[0];
            f.Visibility = Visibility.Visible;
            f.Flip();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Drop(object sender, DragEventArgs e)
        {
            // If an element in the panel has already handled the drop,
            // the panel should not also handle it.         
            if (e.Handled == false)
            {
                lblAlerts.Content = "";
                lblAlerts.Visibility = Visibility.Collapsed;
                if (_matchingMode == false) //WE ARE PLACING CARDS ON THE BOARD
                {                    
                    Panel _target= (Panel)sender; //THE TARGET OF THE DROP EVENT
                    UIElement _element = (UIElement)e.Data.GetData("Object"); //THE UIELEMENT BEING DRAGGED AND DROPPED              
                    GenericCard _gcDragging = new GenericCard((GenericCard)_element); //THE GenericCard CONTAINED IN _element

                    if (_target != null && _element != null)
                    {
                        // Get the panel that the element currently belongs to,
                        // then remove it from that panel and add it the Children of
                        // the panel that its been dropped on.
                        Panel _targetParent = (Panel)VisualTreeHelper.GetParent(_target); //THE Panel THAT CONTAINS _target
                        Panel _elementParent = (Panel)VisualTreeHelper.GetParent(_element); //THE Panel THAT CONTAINS _element

                        if (_targetParent != null)
                        {                         
                            if (_elementParent == spCards) //THE PARENT OF _element IS THE DRAW PILE (spCards)
                            {
                                if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                                {
                                    if (_gcDragging.Symbol.Equals("K")) //IF THE CARD IS A KING
                                    {
                                        if ((_target.GetValue(Grid.RowProperty).Equals(0) && _target.GetValue(Grid.ColumnProperty).Equals(0)) || 
                                            (_target.GetValue(Grid.RowProperty).Equals(0) && _target.GetValue(Grid.ColumnProperty).Equals(3)) || 
                                            (_target.GetValue(Grid.RowProperty).Equals(3) && _target.GetValue(Grid.ColumnProperty).Equals(0)) || 
                                            (_target.GetValue(Grid.RowProperty).Equals(3) && _target.GetValue(Grid.ColumnProperty).Equals(3))) //CHECK IF THE TARGET IS ONE OF THE CORNER SLOTS
                                        {
                                            _gcDragging.locked = true; //SET THE locked PROPERTY OF THE CARD TO true
                                            _elementParent.Children.Remove(_element); //REMOVE THE CARD FROM THE DRAW PILE
                                            _gcDragging.SetValue(Grid.RowProperty, _target.GetValue(Grid.RowProperty)); //SET THE ROW PROPERTY OF THE CARD
                                            _gcDragging.SetValue(Grid.ColumnProperty, _target.GetValue(Grid.ColumnProperty)); //SET THE COLUMN PROPERTY OF THE CARD
                                            _targetParent.Children.Remove(_target); //REMOVE THE TARGET (the empty grid filling the slot in the board with the placeholder image for the King) FROM THE BOARD (gridBoard)
                                            _targetParent.Children.Add(_gcDragging); //ADD THE CARD TO BOARD
                                            ShowNextCardInPile();
                                        }
                                    }
                                    else if (_gcDragging.Symbol.Equals("Q")) //IF THE CARD IS A QUEEN
                                    {
                                        if ((_target.GetValue(Grid.RowProperty).Equals(0) && _target.GetValue(Grid.ColumnProperty).Equals(1)) ||
                                            (_target.GetValue(Grid.RowProperty).Equals(0) && _target.GetValue(Grid.ColumnProperty).Equals(2)) ||
                                            (_target.GetValue(Grid.RowProperty).Equals(3) && _target.GetValue(Grid.ColumnProperty).Equals(1)) ||
                                            (_target.GetValue(Grid.RowProperty).Equals(3) && _target.GetValue(Grid.ColumnProperty).Equals(2))) //CHECK IF THE TARGET IS ONE OF THE TOP OR BOTTOM MIDDLE SLOTS
                                        {
                                            _gcDragging.locked = true; //SET THE locked PROPERTY OF THE CARD TO true
                                            _elementParent.Children.Remove(_element); //REMOVE THE CARD FROM THE DRAW PILE
                                            _gcDragging.SetValue(Grid.RowProperty, _target.GetValue(Grid.RowProperty)); //SET THE ROW PROPERTY OF THE CARD
                                            _gcDragging.SetValue(Grid.ColumnProperty, _target.GetValue(Grid.ColumnProperty)); //SET THE COLUMN PROPERTY OF THE CARD
                                            _targetParent.Children.Remove(_target); //REMOVE THE TARGET (the empty grid filling the slot in the board with the placeholder image for the Queen) FROM THE BOARD (gridBoard)
                                            _targetParent.Children.Add(_gcDragging); //ADD THE CARD TO BOARD
                                            ShowNextCardInPile();

                                        }
                                    }                                   
                                    else if (_gcDragging.Symbol.Equals("J")) //IF THE CARD IS A JACK
                                    {
                                        if ((_target.GetValue(Grid.RowProperty).Equals(1) && _target.GetValue(Grid.ColumnProperty).Equals(0)) ||
                                            (_target.GetValue(Grid.RowProperty).Equals(1) && _target.GetValue(Grid.ColumnProperty).Equals(3)) ||
                                            (_target.GetValue(Grid.RowProperty).Equals(2) && _target.GetValue(Grid.ColumnProperty).Equals(0)) ||
                                            (_target.GetValue(Grid.RowProperty).Equals(2) && _target.GetValue(Grid.ColumnProperty).Equals(3))) //CHECK IF THE TARGET IS ONE OF THE LEFT OR RIGHT MIDDLE SLOTS
                                        {
                                            _gcDragging.locked = true; //SET THE locked PROPERTY OF THE CARD TO true
                                            _elementParent.Children.Remove(_element); //REMOVE THE CARD FROM THE DRAW PILE
                                            _gcDragging.SetValue(Grid.RowProperty, _target.GetValue(Grid.RowProperty)); //SET THE ROW PROPERTY OF THE CARD
                                            _gcDragging.SetValue(Grid.ColumnProperty, _target.GetValue(Grid.ColumnProperty)); //SET THE COLUMN PROPERTY OF THE CARD
                                            _targetParent.Children.Remove(_target); //REMOVE THE TARGET (the empty grid filling the slot in the board with the placeholder image for the Jack) FROM THE BOARD (gridBoard)
                                            _targetParent.Children.Add(_gcDragging); //ADD THE CARD TO BOARD
                                            ShowNextCardInPile();
                                        }
                                    }                                   
                                    else //IF THE CARD IS AN ACE OR NUMBER CARD
                                    {
                                        
                                        _elementParent.Children.Remove(_element); //REMOVE THE CARD FROM THE DRAW PILE
                                        _gcDragging.SetValue(Grid.RowProperty, _target.GetValue(Grid.RowProperty)); //SET THE ROW PROPERTY OF THE CARD
                                        _gcDragging.SetValue(Grid.ColumnProperty, _target.GetValue(Grid.ColumnProperty)); //SET THE COLUMN PROPERTY OF THE CARD
                                        _targetParent.Children.Remove(_target); //REMOVE THE TARGET (the empty grid filling one of the empty slots in the center) FROM THE BOARD (gridBoard)
                                        _targetParent.Children.Add(_gcDragging); //ADD THE CARD TO BOARD
                                        ShowNextCardInPile();
                                    }
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Move;
                                    
                                    if (IsBoardFilled() == true) //Is every slot on the board filled?
                                    {
                                        //Are there legal matches on the board?
                                        if (LegalMatchCheck() == true)  //YES
                                        {
                                            _matchingMode = true;
                                        }
                                        else //NO
                                        {
                                            //game over
                                            lblAlerts.Content = KCstrings.GameOver;
                                            lblAlerts.Visibility = Visibility.Visible;
                                        }
                                    }
                                }
                            }
                            else if (VisualTreeHelper.GetParent(_elementParent).Equals(gridBoard)) //YOU CAN'T DISCARD WHILE THERE ARE STILL EMPTY SLOTS ON THE BOARD
                            {
                                lblAlerts.Content = KCstrings.BoardNotFilled;
                                lblAlerts.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
                if (_matchingMode == true) //WE ARE NOW REMOVING CARDS FROM THE BOARD
                {
                    Panel _target = (Panel)sender; //THE TARGET OF THE DROP EVENT
                    UIElement _element = (UIElement)e.Data.GetData("Object"); //THE UIELEMENT BEING DRAGGED AND DROPPED              
                    GenericCard _gcDragging = new GenericCard((GenericCard)_element); //THE GenericCard CONTAINED IN _element

                    if (_target != null && _element != null)
                    {
                        // Get the panel that the element currently belongs to,
                        // then remove it from that panel and add it the Children of
                        // the panel that its been dropped on.
                        Panel _elementParent = (Panel)VisualTreeHelper.GetParent(_element);

                        if (_elementParent != null)
                        {                            
                            if (_elementParent == gridBoard) //THE PARENT OF THE DRAGGED CARD IS THE BOARD
                            {
                                if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                                {
                                    // set the value to return to the DoDragDrop call
                                    e.Effects = DragDropEffects.Move;
                                    //Are there legal matches on the board? Is the board filled?
                                    if (LegalMatchCheck() == false && IsBoardFilled() == false) //NO AND NO
                                    {
                                        //Can the next card on the draw pile be legally placed on the board?
                                        if (CanPlayNextCard() == true) //YES
                                        {
                                            _matchingMode = false;
                                        }
                                        else //NO. GAME OVER!
                                        {
                                            lblAlerts.Content = KCstrings.GameOver;
                                            lblAlerts.Visibility = Visibility.Visible;
                                        }
                                    }                                    
                                }
                            }                          
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check to see if the next card in the draw pile can be played
        /// </summary>
        /// <returns>boolean</returns>
        private bool CanPlayNextCard()
        {
           

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool LegalMatchCheck()
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dragged"></param>
        /// <param name="target"></param>
        private void Discard(GenericCard dragged, GenericCard target)
        {
            Panel _parentD = (Panel)VisualTreeHelper.GetParent(dragged);
            Panel _parentT = (Panel)VisualTreeHelper.GetParent(target);
            if (dragged.FaceValue + target.FaceValue == 10)
            {
                _parentD.Children.Remove(dragged);
                _parentT.Children.Remove(target);
                target.RenderTransform = new RotateTransform(30.0);
                dragged.RenderTransform = new RotateTransform(-14.5);
                cnvDiscard.Children.Add(target);
                cnvDiscard.Children.Add(dragged);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRules_Click(object sender, RoutedEventArgs e)
        {
            lblAlerts.Visibility = Visibility.Collapsed;
            txtRules.Visibility = (txtRules.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;         
        }
    }
}
