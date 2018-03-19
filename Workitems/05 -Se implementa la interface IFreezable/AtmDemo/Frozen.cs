using System;

namespace AtmDemo
{
    public class Frozen : IFreezable
    {
        private readonly Action _onUnFreeze;

        public Frozen(Action onUnFreeze)
        {
            this._onUnFreeze = onUnFreeze;
        }
        public IFreezable Deposit()
        {
            _onUnFreeze();
            return new UnFrozen();
        }

        public IFreezable UnFrezee() => new UnFrozen();

        public IFreezable WithDraw()
        {
            _onUnFreeze();
            return new UnFrozen();
        }
    }
}
