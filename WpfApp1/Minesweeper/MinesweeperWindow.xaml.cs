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
    /// Interaction logic for MinesweeperWindow.xaml
    /// </summary>
    public partial class MinesweeperWindow : Window
    {
        Random r = new Random();
        public int c = 0;
        public Cell[,] tiles;
        public int w;
        public int h;
        int counter = 0;
        Grid gridGame;
        public MinesweeperWindow()
        {
            SelectDifficulty diff = new SelectDifficulty();
            diff.ShowDialog();
            
            InitializeComponent();
            lblDiff.Content = diff.diff;

            tiles = new Cell[diff.sizeX, diff.sizeY];

            h =(tiles.GetUpperBound(tiles.Rank - 1) + 1) * 49 + 2;
            w = (tiles.GetUpperBound(0) + 1) * 42;

            gridGame = new Grid();
            gridGame.HorizontalAlignment = HorizontalAlignment.Left;
            gridGame.VerticalAlignment = VerticalAlignment.Top;
            gridGame.Width = w;
            gridGame.Height = h;
            gridGame.SetValue(Grid.RowProperty, 1);

            gridView.Children.Add(gridGame);
            
            GenerateTiles(tiles);
            //LayMines();
            //FindNearby();
        }

        public void GenerateTiles(Cell[,] cells)
        {
            int rowNum = cells.GetUpperBound(0) + 1;
            int colNum = cells.GetUpperBound(cells.Rank - 1) + 1;

            for (int i = 0; i < rowNum; i++)
            {
                gridGame.RowDefinitions.Add(new RowDefinition());
                
            }
            for (int j = 0; j < colNum; j++)
            {
                gridGame.ColumnDefinitions.Add(new ColumnDefinition());

            }
            

            for (int i = 0; i< rowNum ; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    cells[i, j] = new Cell(i, j);
                    cells[i, j].SetValue(Grid.RowProperty, i);
                    cells[i, j].SetValue(Grid.ColumnProperty, j);
                    //cells[i, j].SetValue(Cell.WidthProperty, 20);
                    //cells[i, j].SetValue(Cell.HeightProperty, 20);
                    cells[i, j].MouseLeftButtonUp += new MouseButtonEventHandler(Cell_LeftClick);
                    cells[i, j].MouseRightButtonUp += new MouseButtonEventHandler(Cell_RightClick);
                    //cells[i, j].Paint += new PaintEventHandler(_Paint);
                    gridGame.Children.Add(cells[i, j]);
                }
            }
        }

        public void RevealNearby(int row, int column)
        {
            if (row < 0 || row >= tiles.GetUpperBound(0)+1 || column < 0 || column >= tiles.GetUpperBound(tiles.Rank-1)+1) { return; }
            if (tiles[row, column].getNearby() < 9 && tiles[row, column].getRevealed() == false)
            {
                if (tiles[row, column].getNearby() == 0)
                {
                    tiles[row, column].setRevealed(true);
                    tiles[row, column].Content = " ";
                    counter++;
                    RevealNearby(row + 1, column);
                    RevealNearby(row - 1, column);
                    RevealNearby(row, column + 1);
                    RevealNearby(row, column - 1);
                    RevealNearby(row + 1, column + 1);
                    RevealNearby(row + 1, column - 1);
                    RevealNearby(row - 1, column + 1);
                    RevealNearby(row - 1, column - 1);


                }
                if (tiles[row, column].getNearby() > 0)
                {
                    tiles[row, column].setRevealed(true);
                    tiles[row, column].Content = tiles[row, column].getNearby().ToString();

                    switch (tiles[row, column].getNearby())
                    {
                        case 1:
                            tiles[row, column].Foreground = Brushes.Blue;
                            break;
                        case 2:
                            tiles[row, column].Foreground = Brushes.Green;
                            break;
                        case 3:
                            tiles[row, column].Foreground = Brushes.Red;
                            break;
                        case 4:
                            tiles[row, column].Foreground = Brushes.DarkBlue;
                            break;
                        case 5:
                            tiles[row, column].Foreground = Brushes.Maroon;
                            break;
                        case 6:
                            tiles[row, column].Foreground = Brushes.Aquamarine;
                            break;
                        case 7:
                            tiles[row, column].Foreground = Brushes.Black;
                            break;
                        case 8:
                            tiles[row, column].Foreground = Brushes.DimGray;
                            break;
                        default:
                            tiles[row, column].Foreground = Brushes.Black;
                            break;
                    }
                    counter++;
                }
            }

            else if (tiles[row, column].getActive() == true)
            {
                tiles[row, column].setRevealed(true);
                tiles[row, column].Content = "💣";
                lblWinLose.Content = "LOSE!!!";
            }

            if (counter + c == ( (tiles.GetUpperBound(0)) * (tiles.GetUpperBound(tiles.Rank-1)) ) )
            {
                lblWinLose.Content = "WIN!!!";
            }
        }
        private void Cell_LeftClick(object sender, MouseEventArgs e) 
        {       
            Cell clicked = (Cell)sender;
            RevealNearby(clicked.getRow(), clicked.getCol());
        }
        private void Cell_RightClick(object sender, MouseEventArgs e)
        {
            Cell clicked = (Cell)sender;

            
                if (clicked.Content.Equals(""))
                {
                    clicked.Content = "🚩";
                }
                else if (clicked.Content.Equals("🚩"))
                {
                    clicked.Content = "?";
                }
                else if (clicked.Content.Equals("?"))
                {
                    clicked.Content = "";
                }
            
            
        }
    }
}
