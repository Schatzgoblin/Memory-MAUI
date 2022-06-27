using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    [Serializable]
    public class Card
    {
        private int column, row;
        private int cardNumber;

        public Card(int column, int row, int cardNumber)
        {
            this.column = column;
            this.row = row;
            this.cardNumber = cardNumber;
        }

        public int Column { get => column; set => column = value; }
        public int Row { get => row; set => row = value; }
        public int CardNumber { get => cardNumber; set => cardNumber = value; }

        public override bool Equals(object obj)
        {
            return obj is Card card &&
                   column == card.column &&
                   row == card.row &&
                   cardNumber == card.cardNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(column, row, cardNumber);
        }
    }
}
