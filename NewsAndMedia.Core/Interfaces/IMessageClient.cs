namespace NewsAndMedia.Core.Interfaces
{
    public interface IMessageClient
    {
        void InitClient();
        void ReceiveMessage();
        public bool SendMessage(decimal result);
    }
}
