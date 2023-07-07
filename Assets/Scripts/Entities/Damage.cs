namespace AutoBattler
{
    public class Damage
    {
        public int Amount { get; set; }
        public DamageType Type { get; set; }
        public DamageIgnore DamageIgnore { get; set; }
        public int PhysicalResistIgnoreAmount { get; set; }
        public int MagicalResistIgnoreAmount { get; set; }
    }
}