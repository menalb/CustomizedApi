namespace Messages
{
    public interface IMessage { }

    public interface ILoadProductMessage : IMessage
    {
        string Name { get; set; }
        string Brand { get; set; }
    }
}