using System;

namespace AtmDemo
{
    public class NotVerified : IAccountState
    {
        private readonly Action _onUnFreeze;
        public AccountState State => AccountState.NotVerified;

        public NotVerified(Action onUnFreeze)
        {
            this._onUnFreeze = onUnFreeze;
        }
        public IAccountState Close(decimal actualAmount)
        {
            if (actualAmount > 0)
                throw new AccountHasMoneyException();
            return new Closed(_onUnFreeze);
        }

        public IAccountState Deposit(Action addMoney) => throw new AccountNotVerifiedException();

        public IAccountState HolderVerified() => new Active(_onUnFreeze);

        public IAccountState UnFrezee() => this;

        public IAccountState WithDraw(Action withDrawMoney) {
            throw new AccountNotVerifiedException();
        }
        public IAccountState Open() => this;
        public IAccountState Freeze() => new Frozen(_onUnFreeze);

    }
}
