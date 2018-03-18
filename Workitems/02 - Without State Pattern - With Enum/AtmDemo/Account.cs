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

        private AccountState _state = AccountState.Open;
        public Account(bool isVerified, bool isOpen, bool isFrozen, decimal initialAmount, string holder, Action onUnFreeze)
        {
            if (isVerified)
                _state |= AccountState.Verfied;
            if (isOpen)
                _state |= AccountState.Open;
            else
            {
                _state |= AccountState.Close;
                _state &= ~AccountState.Open;
            }

            if (isFrozen)
                _state |= AccountState.Frozen;


            _amount = initialAmount;
            _holder = holder;
            this.onUnFreeze = onUnFreeze;
        }

        public decimal Summary() => _amount;

        public void Deposit(decimal amount)
        {
            if (_state.HasFlag(AccountState.Close))
            {
                Console.WriteLine("La cuenta se encuentra cerrada");
                throw new AccountClosedException();
            }

            if (_state.HasFlag(AccountState.Open) && !_state.HasFlag(AccountState.Verfied))
            {
                Console.WriteLine("La cuenta no se encuentra verificada");
                throw new AccountNotVerifiedException();
            }

            if (_state.HasFlag(AccountState.Open) && _state.HasFlag(AccountState.Verfied))
                deposit(amount);

            if (_state.HasFlag(AccountState.Frozen))
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
            if (_state.HasFlag(AccountState.Close))

            {
                Console.WriteLine("La cuenta se encuentra cerrada");
                throw new AccountClosedException();
            }

            if (_state.HasFlag(AccountState.Open) && !_state.HasFlag(AccountState.Verfied))
            {
                Console.WriteLine("La cuenta no se encuentra verificada");
                throw new AccountNotVerifiedException();
            }

            if (_state.HasFlag(AccountState.Open) && _state.HasFlag(AccountState.Verfied))
                withDraw(amount);

            if (_state.HasFlag(AccountState.Frozen))
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
            if (_state.HasFlag(AccountState.Close))
                throw new AccountClosedException();

            _state |= AccountState.Verfied;
        }
        public void Open()
        {
            _state |= AccountState.Open;
            _state &= ~AccountState.Close;

        }
        public void Close()
        {
            if (_amount > 0)
                throw new AccountHasMoneyException();

            _state |= AccountState.Close;
            _state &= ~AccountState.Open;
        }
        public AccountState State()
        {
            if (_state.HasFlag(AccountState.Open) && !_state.HasFlag(AccountState.Close))
                return AccountState.Open;

            return AccountState.Close;
        }

    }
}
