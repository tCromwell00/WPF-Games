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
        public string difficulty;
        SelectDifficulty diff;
        int counter = 0;
        Grid gridGame;
        public int gameCount;

        public MinesweeperWindow(string d, int mm, int ms , int gc)
        {
            
            InitializeComponent();

            gameCount = gc;
            MAXMINES = mm;
            MAXSIDE = ms;
            difficulty = d;



            lblDiff.Content = difficulty;
            tiles = new Cell[MAXSIDE, MAXSIDE];
            gridGame = new Grid();
            gridGame.HorizontalAlignment = HorizontalAlignment.Stretch;
            gridGame.VerticalAlignment = VerticalAlignment.Stretch;

            if (difficulty.Equals("Easy"))
            {
                gridGame.Width = MAXSIDE * 50;
                gridGame.Height = MAXSIDE * 50;
            }
            else if (difficulty.Equals("Medium"))
            {
                gridGame.Width = MAXSIDE * 40;
                gridGame.Height = MAXSIDE * 40;
            }
            else if (difficulty.Equals("Hard"))
            {
                gridGame.Width = MAXSIDE * 30;
                gridGame.Height = MAXSIDE * 30;
            }




            gridGame.SetValue(Grid.RowProperty, 1);
            gridGame.SetValue(Grid.ColumnProperty, 0);

            gridView.Children.Add(gridGame);
            
            GenerateTiles(tiles);
            LayMines();
            FindNearby();
        }

        public MinesweeperWindow()
        {
            gameCount = 0;
            diff = new SelectDifficulty();
            diff.ShowDialog();

            InitializeComponent();

            difficulty = diff.diff;
            lblDiff.Content = difficulty;
            MAXMINES = diff.maxMines;
            MAXSIDE = diff.size;
            tiles = new Cell[MAXSIDE, MAXSIDE];
            
            gridGame = new Grid();
            gridGame.HorizontalAlignment = HorizontalAlignment.Stretch;
            gridGame.VerticalAlignment = VerticalAlignment.Stretch;
            if(diff.diff != null)
            {
                if (diff.diff.Equals("Easy"))
                {
                    gridGame.Width = MAXSIDE * 50;
                    gridGame.Height = MAXSIDE * 50;
                }
                else if (diff.diff.Equals("Medium"))
                {
                    gridGame.Width = MAXSIDE * 40;
                    gridGame.Height = MAXSIDE * 40;
                }
                else if (diff.diff.Equals("Hard"))
                {
                    gridGame.Width = MAXSIDE * 30;
                    gridGame.Height = MAXSIDE * 30;
                }
            }
            else
            {
                gameWindow.Close();
            }

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
                    cells[i, j].Click += Cell_LeftClick;
                    cells[i, j].MouseRightButtonDown += new MouseButtonEventHandler(Cell_RightClick);
                    
                    gridGame.Children.Add(cells[i, j]);
                }
            }
        }

        public void LayMines()
        {
            bool[] mark= new bool[MAXSIDE*MAXSIDE];
            for (int i = 0; i < MAXMINES;)
            {
                int random = r.Next() % (MAXSIDE * MAXSIDE);
                int x = random / MAXSIDE;
                int y = random % MAXSIDE;

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
        }

        public void RevealNearby(int row, int column)
        {
            
            if (row < 0 || row >=MAXSIDE || column < 0 || column >= MAXSIDE) { return; }
            Cell clicked = tiles[row, column];
            if (clicked.getNearby() < 9 && clicked.getRevealed() == false)
            {
                
                if (clicked.getNearby() == 0)
                {
                    clicked.setRevealed(true);
                    clicked.Content = " ";
                    //tiles[row,column].Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD));
                    clicked.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD));
                    counter++;
                    lblWinLose.Content = counter;
                    RevealNearby(row + 1, column);
                    RevealNearby(row - 1, column);
                    RevealNearby(row, column + 1);
                    RevealNearby(row, column - 1);
                    RevealNearby(row + 1, column + 1);
                    RevealNearby(row + 1, column - 1);
                    RevealNearby(row - 1, column + 1);
                    RevealNearby(row - 1, column - 1);


                }
                else if (clicked.getNearby() > 0)
                {
                    clicked.setRevealed(true);
                    clicked.Content = clicked.getNearby().ToString();

                    switch (clicked.getNearby())
                    {
                        case 1:
                            clicked.Foreground = Brushes.Blue;
                            break;
                        case 2:
                            clicked.Foreground = Brushes.Green;
                            break;
                        case 3:
                            clicked.Foreground = Brushes.Red;
                            break;
                        case 4:
                            clicked.Foreground = Brushes.DarkBlue;
                            break;
                        case 5:
                            clicked.Foreground = Brushes.Maroon;
                            break;
                        case 6:
                            clicked.Foreground = Brushes.Aquamarine;
                            break;
                        case 7:
                            clicked.Foreground = Brushes.Black;
                            break;
                        case 8:
                            clicked.Foreground = Brushes.DimGray;
                            break;
                        default:
                            clicked.Foreground = Brushes.Black;
                            break;
                    }
                    clicked.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD));
                    counter++;
                    lblWinLose.Content = counter;
                }
            }

            else if (clicked.getActive() == true)
            {
                clicked.setRevealed(true);
                clicked.Foreground = Brushes.Red;
                clicked.Content = "💣";
                lblWinLose.Content = "LOSE!!!";
                foreach (Cell g in tiles)
                {
                    if (g.getActive() == true)
                    {
                        g.Content = "💣";
                    }
                    g.IsEnabled = false;
                }
            }

            if (counter + minesLaidCounter == (MAXSIDE * MAXSIDE) )
            {
                foreach(Cell g in tiles)
                {
                    g.IsEnabled = false;
                }

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
                    clicked.Foreground = Brushes.Red;
                }
                else if (clicked.Content.Equals("🚩"))
                {
                    clicked.Content = "?";
                }
                else if (clicked.Content.Equals("?"))
                {
                    clicked.Content = "";
                    clicked.Foreground = Brushes.Black;
                }
            
            
        }

        private void btnGameBegin_Click(object sender, RoutedEventArgs e)
        {
            gameCount++;
            Window newGame = new MinesweeperWindow(difficulty,MAXMINES,MAXSIDE,gameCount);
            newGame.Show();
            this.Close();
        }
    }
}
