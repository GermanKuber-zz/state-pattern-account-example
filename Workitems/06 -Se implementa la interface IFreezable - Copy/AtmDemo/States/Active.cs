using System;

namespace AtmDemo
{
    public class Active : IAccountState
    {
        private readonly Action _onUnFreeze;
        public AccountState State => AccountState.Open;
        public Active(Action onUnFreeze)
        {
            this._onUnFreeze = onUnFreeze;
        }
        public IAccountState Close(decimal actualAmount) {
            if (actualAmount > 0)
                throw new AccountHasMoneyException();
            return new Closed(_onUnFreeze);
        }

        public IAccountState Deposit(Action addMoney)
        {
            addMoney();
            return this;
        }

        public IAccountState HolderVerified() => new Active(_onUnFreeze);

        public IAccountState UnFrezee() => new Frozen(_onUnFreeze);

        public IAccountState WithDraw(Action withDrawMoney)
        {
            withDrawMoney();
            return this;
        }
        public IAccountState Open() => new NotVerified(_onUnFreeze);
        public IAccountState Freeze() => new Frozen(_onUnFreeze);

    }
}
