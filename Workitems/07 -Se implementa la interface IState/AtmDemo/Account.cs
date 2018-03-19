using System;

namespace AtmDemo
{
    public class AccountClosedException : Exception { }
    public class AccountNotVerifiedException : Exception { }
    public class AccountHasNotMoneyException : Exception { }
    public class AccountHasMoneyException : Exception { }
    public class Account
    {
        private decimal _amount;
        private readonly string _holder;
        private readonly Action onUnFreeze;
        //TODO: 02 - Renombro Freezable a State
        private IAccountState State { get; set; }

        public Account(bool isVerified, bool isOpen,  decimal initialAmount, string holder, Action onUnFreeze)
        {
            if (isVerified)
                State = new Active(onUnFreeze);
            if (isOpen)
                State = new Active(onUnFreeze);
            else
                State = new Closed(onUnFreeze);

            _amount = initialAmount;
            _holder = holder;
            this.onUnFreeze = onUnFreeze;

        }

        public void Freeze()
        {
            this.State = this.State.Freeze();
        }

        public decimal Summary() => _amount;

        public void Deposit(decimal amount)=> State.Deposit(() => { deposit(amount); });
        private void deposit(decimal amount) => this._amount += amount;

        public void WithDraw(decimal amount)
        {
            State.WithDraw(() => { withDraw(amount); });
        }

        private void withDraw(decimal amount)
        {
            if (amount <= this._amount)
                this._amount -= amount;
            else
            {
                Console.WriteLine("La cuenta no tiene suficiente dinero");
                throw new AccountHasNotMoneyException();
            }
        }

        public void HolderVirfied() => this.State = this.State.HolderVerified();

        public void Open() => this.State = this.State.Open();
        public void Close()=>this.State = this.State.Close(_amount);
        public AccountState GetState() => this.State.State;

    }
}
