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
        public int minesLaidCounter = 0;
        public Cell[,] tiles;
        public int h;
        public int w;
        public int MAXMINES;
        public int MAXSIDE;
        SelectDifficulty diff;
        int counter = 0;
        Grid gridGame;
        public MinesweeperWindow()
        {
            diff = new SelectDifficulty();
            diff.ShowDialog();
            
            InitializeComponent();
            lblDiff.Content = diff.diff;
            MAXMINES = diff.maxMines;
            MAXSIDE = diff.size;
            tiles = new Cell[diff.size, diff.size];

            h =(tiles.GetUpperBound(tiles.Rank - 1) + 1) * 42;
            w = (tiles.GetUpperBound(0) + 1) * 42;

            gridGame = new Grid();
            gridGame.HorizontalAlignment = HorizontalAlignment.Stretch;
            gridGame.VerticalAlignment = VerticalAlignment.Stretch;
            gridGame.Width = w;
            gridGame.Height = h;
           
            gridGame.SetValue(Grid.RowProperty, 1);
            gridGame.SetValue(Grid.ColumnProperty, 0);

            gridView.Children.Add(gridGame);
            
            GenerateTiles(tiles);
            LayMines();
            FindNearby();
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
                    //cells[i, j].SetValue(Cell.WidthProperty, 20.0);
                   // cells[i, j].SetValue(Cell.HeightProperty, 20.0);
                    //cells[i, j].MouseLeftButtonDown += new MouseButtonEventHandler(Cell_LeftClick);
                    cells[i, j].Click += Cell_LeftClick;
                    cells[i, j].MouseRightButtonDown += new MouseButtonEventHandler(Cell_RightClick);
                    //cells[i, j].Paint += new PaintEventHandler(_Paint);
                    gridGame.Children.Add(cells[i, j]);
                }
            }
        }

        public void LayMines()
        {
            bool[] mark= new bool[MAXSIDE*MAXSIDE];
            for (int i = 0; i < MAXMINES;)
            {
                int random = r.Next() % (diff.size * diff.size);
                int x = random / diff.size;
                int y = random % diff.size;

                // Add the mine if no mine is placed at this
                // position on the board
                if (mark[random] == false)
                {
                    tiles[x, y].setActive(true);
                    tiles[x, y].setNearby(9);
                    minesLaidCounter++;
                    mark[random] = true;
                    i++;
                }
            }
            /* for (int i = 0; i < tiles.GetLength(0); i++)
             {
                 for (int j = 0; j < tiles.GetLength(1); j++)
                 {
                     if (r.Next(1, 101) < 15)
                     {
                         tiles[i, j].setActive(true);
                         tiles[i, j].setNearby(9);
                         mineTiles[minesLaidCounter] = tiles[i, j];
                         minesLaidCounter++;
                     }

                 }
             }*/
        }

        public void RevealNearby(int row, int column)
        {
            if (row < 0 || row >=diff.size || column < 0 || column >= diff.size) { return; }
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

            if (counter + minesLaidCounter == (diff.size * diff.size) )
            {
                lblWinLose.Content = "WIN!!!";
            }
        }

        public void FindNearby()
        {
            int rowLimit = tiles.GetLength(0);
            int colLimit = tiles.GetLength(1);

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j].getNearby() < 9)
                    {
                        int neighbors = 0;
                        for (int x = Math.Max(0, i - 1); x <= Math.Min(i + 1, rowLimit - 1); x++)
                        {
                            for (int y = Math.Max(0, j - 1); y <= Math.Min(j + 1, colLimit - 1); y++)
                            {
                                if (x != i || y != j)
                                {
                                    bool check = tiles[x, y].getActive();
                                    if (check)
                                    {
                                        neighbors++;
                                    }
                                }
                            }
                        }
                        tiles[i, j].setNearby(neighbors);
                    }
                }
            }
        }



        private void Cell_LeftClick(object sender, RoutedEventArgs e) 
        {
            Cell clicked = sender as Cell;
            RevealNearby(clicked.getRow(), clicked.getCol());
        }
        private void Cell_RightClick(object sender, MouseEventArgs e)
        {
            Cell clicked = e.Source as Cell;

            
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
