using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    
    class GenericCardDeck
    {
        
        public GenericCardDeck()
        {
            GenericCard CA = new GenericCard(1, "A", "Clubs");
            GenericCard C2 = new GenericCard(2, "2", "Clubs");
            GenericCard C3 = new GenericCard(3, "3", "Clubs");
            GenericCard C4 = new GenericCard(4, "4", "Clubs");
            GenericCard C5 = new GenericCard(5, "5", "Clubs");
            GenericCard C6 = new GenericCard(6, "6", "Clubs");
            GenericCard C7 = new GenericCard(7, "7", "Clubs");
            GenericCard C8 = new GenericCard(8, "8", "Clubs");
            GenericCard C9 = new GenericCard(9, "9", "Clubs");
            GenericCard C10 = new GenericCard(10, "10", "Clubs");
            GenericCard CJ = new GenericCard(10, "J", "Clubs");
            GenericCard CQ = new GenericCard(10, "Q", "Clubs");
            GenericCard CK = new GenericCard(10, "K", "Clubs");

        }
    }
}
