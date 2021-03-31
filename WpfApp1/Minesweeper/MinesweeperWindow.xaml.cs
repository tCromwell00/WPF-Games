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
        public MinesweeperWindow()
        {
            SelectDifficulty diff = new SelectDifficulty();
            diff.ShowDialog();
            
            InitializeComponent();
            tiles = new Cell[diff.sizeX, diff.sizeY];
            h =(tiles.GetUpperBound(tiles.Rank - 1) + 1) * 49 + 2;
            w = (tiles.GetUpperBound(0) + 1) * 42;
            gridGame.Width = w;
            gridGame.Height = h;
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
                for (int j = 0; j < colNum; j++)
                {
                    gridGame.ColumnDefinitions.Add(new ColumnDefinition());
                    
                }
            }

           /* for (int r = 0; r < gridGame.RowDefinitions.Count; r++)
            {
                for (int c = 0; c < gridGame.ColumnDefinitions.Count; c++)
                {
                    cells[r, c] = new Cell(r, c);
                    Cell f = cells[r, c];
                    gridGame.Children.Add(f);
                    string cu = "cells" + r + c;
                    Grid.SetRow(cells[r, c], r);
                    Grid.SetColumn(cells[r, c], c);
                    //cells[r, c].MouseDown += new MouseEventHandler(button_MouseClick);
                    //cells[i, j].Paint += new PaintEventHandler(_Paint);

                    
                }
            }*/
        }
    }
}
