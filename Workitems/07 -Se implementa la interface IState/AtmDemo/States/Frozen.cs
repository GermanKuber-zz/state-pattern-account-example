using System;

namespace AtmDemo
{
    public class Frozen : IAccountState
    {
        private readonly Action _onUnFreeze;
        public AccountState State => AccountState.Frozen;

        public Frozen(Action onUnFreeze)
        {
            this._onUnFreeze = onUnFreeze;
        }

        public IAccountState Close(decimal actualAmount)
        {
            if (actualAmount > 0)
                throw new AccountHasMoneyException();
            return new Closed(_onUnFreeze);
        }

        public IAccountState Deposit(Action addMoney)
        {
            _onUnFreeze();
            return new Active(_onUnFreeze);
        }


        public IAccountState HolderVerified() => this;

        public IAccountState UnFrezee() => new Active(_onUnFreeze);

        public IAccountState WithDraw(Action withDrawMoney)
        {
            _onUnFreeze();
            return new Active(_onUnFreeze);
        }
        public IAccountState Open() => new NotVerified(_onUnFreeze);
        public IAccountState Freeze() => new Frozen(_onUnFreeze);

    }
}
