using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.View
{
    public class FakeChat : IChat
    {
        public bool SendBuyingMessageCalled { get; set; }
        public void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public void SendBuyingMessage(string media)
        {
            SendBuyingMessageCalled = true;
            return;
        }

        public void SendMessage(string message, bool isMine, bool isSellingPost)
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
