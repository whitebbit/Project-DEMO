namespace _Game.Scripts.Units.Interfaces
{
    public interface IHitVisitor
    {
        public void Visit(DamageData damageData);
    }
}