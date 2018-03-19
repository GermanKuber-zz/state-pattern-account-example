namespace AtmDemo
{
    public class UnFrozen : IFreezable
    {
        public IFreezable Deposit() => this;

        public IFreezable UnFrezee() => this;

        public IFreezable WithDraw() => this;
    }
}
