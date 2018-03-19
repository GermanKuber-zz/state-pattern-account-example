using System;

namespace AtmDemo
{
    public class Closed : IAccountState
    {
        private Action _onUnFreeze;

        public AccountState State => AccountState.Close;

        public Closed(Action onUnFreeze)
        {
            this._onUnFreeze = onUnFreeze;
        }
        public IAccountState Deposit(Action addMoney)
        {
            throw new AccountClosedException();
            //this;
        }

        public IAccountState HolderVerified()
        {
            throw new AccountClosedException();
        }

        public IAccountState UnFrezee() => this;

        public IAccountState WithDraw(Action withDrawMoney)
        {
            throw new AccountClosedException();
        }

        public IAccountState Close(decimal actualAmount)
        {
            if (actualAmount > 0)
                throw new AccountHasMoneyException();
            return new Closed(_onUnFreeze);
        }
        public IAccountState Open() => new NotVerified(_onUnFreeze);

        public IAccountState Freeze() => new Frozen(_onUnFreeze);
    }
}
