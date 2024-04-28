namespace ISSLab.View
{
    public interface IChat
    {
        void SendBuyingMessage(string media);
        void SendMessage(string message, bool isMine, bool isSellingPost);
        void Show();
    }
}