using System;
using System.Collections.Generic;
using Domain.Data;

namespace Domain
{
    public class Player
    {
        public Guid Id { get; set; }
        public int Max_HP { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public ICollection<ItemData> Items { get; set; }
    }
}