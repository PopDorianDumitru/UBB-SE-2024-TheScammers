using ISSLab.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.ViewModel
{
    public class FakeChatFactory : IChatFactory
    {
        public IChat CreateChat(ChatViewModel chatViewModel)
        {
            IChat fakeChat = new FakeChat();
            return fakeChat;
        }
    }
}
