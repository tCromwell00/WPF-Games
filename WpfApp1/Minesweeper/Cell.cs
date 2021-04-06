using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1.Minesweeper
{
    public class Cell : Button
    {
        int row = -1;
        int col = -1;
        bool revealed = false;
        bool active = false;
        int nearby = 0;

        public Cell(int x, int y)
        {
            row = x;
            col = y;
            this.Content = "";
            this.ClickMode = ClickMode.Press;
            this.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x8D, 0x8D, 0x8D));
        }


        


        //Getter methods
        public int getRow()
        {
            return row;
        }

        public int getCol()
        {
            return col;
        }

        public bool getRevealed()
        {
            return revealed;
        }

        public bool getActive()
        {
            return active;
        }

        public int getNearby()
        {
            return nearby;
        }

        //Setter methods

        public void setRow(int x)
        {
            row = x;
        }

        public void setCol(int y)
        {
            col = y;
        }

        public void setRevealed(bool r)
        {
            revealed = r;
        }

        public void setActive(bool a)
        {
            active = a;
        }

        public void setNearby(int n)
        {
            nearby = n;
        }


    }
}
