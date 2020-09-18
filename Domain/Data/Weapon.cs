namespace Domain.Data
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Gold { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int MaxDurability { get; set; }
    }
}