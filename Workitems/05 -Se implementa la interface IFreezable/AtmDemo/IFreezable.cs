namespace AtmDemo
{
    //TODO: 01 - Creo una interface con los metodos comúnes 
    public interface IFreezable
    {
        IFreezable Deposit();
        IFreezable WithDraw();
        IFreezable UnFrezee();
    }
}
