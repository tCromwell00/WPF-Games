using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1
{
    
    class GenericCardDeck
    {
        public List<GenericCard> deck;
        public GenericCardDeck()
        {
            deck = new List<GenericCard>{
                new GenericCard(1, "A", "Clubs"),
                new GenericCard(2, "2", "Clubs"),
                new GenericCard(3, "3", "Clubs"),
                new GenericCard(4, "4", "Clubs"),
                new GenericCard(5, "5", "Clubs"),
                new GenericCard(6, "6", "Clubs"),
                new GenericCard(7, "7", "Clubs"),
                new GenericCard(8, "8", "Clubs"),
                new GenericCard(9, "9", "Clubs"),
                new GenericCard(10, "10", "Clubs"),
                new GenericCard(10, "J", "Clubs"),
                new GenericCard(10, "Q", "Clubs"),
                new GenericCard(10, "K", "Clubs"),
                new GenericCard(1, "A", "Diamonds"),
                new GenericCard(2, "2", "Diamonds"),
                new GenericCard(3, "3", "Diamonds"),
                new GenericCard(4, "4", "Diamonds"),
                new GenericCard(5, "5", "Diamonds"),
                new GenericCard(6, "6", "Diamonds"),
                new GenericCard(7, "7", "Diamonds"),
                new GenericCard(8, "8", "Diamonds"),
                new GenericCard(9, "9", "Diamonds"),
                new GenericCard(10, "10", "Diamonds"),
                new GenericCard(10, "J", "Diamonds"),
                new GenericCard(10, "Q", "Diamonds"),
                new GenericCard(10, "K", "Diamonds"),
                new GenericCard(1, "A", "Hearts"),
                new GenericCard(2, "2", "Hearts"),
                new GenericCard(3, "3", "Hearts"),
                new GenericCard(4, "4", "Hearts"),
                new GenericCard(5, "5", "Hearts"),
                new GenericCard(6, "6", "Hearts"),
                new GenericCard(7, "7", "Hearts"),
                new GenericCard(8, "8", "Hearts"),
                new GenericCard(9, "9", "Hearts"),
                new GenericCard(10, "10", "Hearts"),
                new GenericCard(10, "J", "Hearts"),
                new GenericCard(10, "Q", "Hearts"),
                new GenericCard(10, "K", "Hearts"),
                new GenericCard(1, "A", "Spades"),
                new GenericCard(2, "2", "Spades"),
                new GenericCard(3, "3", "Spades"),
                new GenericCard(4, "4", "Spades"),
                new GenericCard(5, "5", "Spades"),
                new GenericCard(6, "6", "Spades"),
                new GenericCard(7, "7", "Spades"),
                new GenericCard(8, "8", "Spades"),
                new GenericCard(9, "9", "Spades"),
                new GenericCard(10, "10", "Spades"),
                new GenericCard(10, "J", "Spades"),
                new GenericCard(10, "Q", "Spades"),
                new GenericCard(10, "K", "Spades") };
                
        }



        
    }
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    public static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
