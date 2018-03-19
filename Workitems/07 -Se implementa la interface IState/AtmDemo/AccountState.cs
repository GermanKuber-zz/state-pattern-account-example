using System;

namespace AtmDemo
{
    [Flags]
    public enum AccountState
    {
        Open = 1,
        NotVerified = 2,
        Frozen = 3,
        Close = 4
    }
}
