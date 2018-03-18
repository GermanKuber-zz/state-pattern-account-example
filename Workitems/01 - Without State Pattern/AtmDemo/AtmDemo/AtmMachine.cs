namespace AtmDemo
{
    public class AtmMachine
    {
        private Account _account;

        public AtmMachine(Account account)
        {
            _account = account;
        }

        public void Deposit(decimal amount) => _account.Deposit(amount);
        public void WithDraw(decimal amount) => _account.WithDraw(amount);
        public void HolderVirfied() => _account.HolderVirfied();
        public decimal Summary() => _account.Summary();
        public void OpenAccount() => _account.Open();



    }
}
