using System;

namespace AtmDemo
{
    //TODO: 01 - Renombro de IFreezable a IAccountState
    public interface IAccountState
    {
         AccountState State { get;  }
        IAccountState Deposit(Action addMoney);
        IAccountState WithDraw(Action withDrawMoney);
        IAccountState UnFrezee();
        IAccountState Close(decimal actualAmount);
        IAccountState HolderVerified();
        IAccountState Open();
        IAccountState Freeze();
    }
}
