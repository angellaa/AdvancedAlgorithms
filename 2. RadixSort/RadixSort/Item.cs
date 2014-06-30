using System;

namespace RadixSort
{
    public class Item : IComparable<Item>
    {
        public int Id { get; private set;  }
        public int Value { get; private set; }

        public Item(int id, int value)
        {
            Id = id;
            Value = value;
        }

        public int CompareTo(Item other)
        {
            return Id - other.Id;
        }
    }
}