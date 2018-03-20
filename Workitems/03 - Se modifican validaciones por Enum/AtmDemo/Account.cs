using System;

namespace AtmDemo
{
    public class Account
    {
        private decimal _amount;
        private readonly string _holder;
        private readonly Action onUnFreeze;

        private AccountState _state = AccountState.Active;
        public Account(bool isVerified, bool isActive, bool isFrozen, decimal initialAmount, string holder, Action onUnFreeze)
        {
            if (!isVerified)
                _state = AccountState.NotVerified;
            else
            {
                if (isActive)
                    _state = AccountState.Active;
                else
                    _state = AccountState.Close;
            }
            if (isFrozen)
                _state = AccountState.Frozen;


            _amount = initialAmount;
            _holder = holder;
            this.onUnFreeze = onUnFreeze;
        }

        public decimal Summary() => _amount;

        public void Deposit(decimal amount)
        {
            if (_state == AccountState.Close)
            {
                Console.WriteLine("La cuenta se encuentra cerrada");
                throw new AccountClosedException();
            }

            if (_state == AccountState.NotVerified)
            {
                Console.WriteLine("La cuenta no se encuentra verificada");
                throw new AccountNotVerifiedException();
            }

            if (_state == AccountState.Active)
                deposit(amount);

            if (_state == AccountState.Frozen)
            {
                //Se debe implementar el estado congelado
                //Y en caso de ser descongelado se debe llamar a una función de callback
                onUnFreeze();
                _state &= AccountState.Frozen;
            }
        }
        private void deposit(decimal amount) => this._amount += amount;

        public void WithDraw(decimal amount)
        {
            if (_state == AccountState.Close)
            {
                Console.WriteLine("La cuenta se encuentra cerrada");
                throw new AccountClosedException();
            }

            if (_state == AccountState.NotVerified)
            {
                Console.WriteLine("La cuenta no se encuentra verificada");
                throw new AccountNotVerifiedException();
            }

            if (_state == AccountState.Active)
                withDraw(amount);

            if (_state == AccountState.Frozen)
            {
                //Se debe implementar el estado congelado
                //Y en caso de ser descongelado se debe llamar a una función de callback
                onUnFreeze();
                _state &= AccountState.Frozen;
            }
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

        public void HolderVirfied()
        {
            if (_state == AccountState.Close)
                throw new AccountClosedException();

            _state = AccountState.Active;
        }

        public void Close()
        {
            if (_amount > 0)
                throw new AccountHasMoneyException();

            _state = AccountState.Close;
        }
        public AccountState State() => _state;

    }
}
