using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.View;

namespace ISSLab.ViewModel
{
    public class FakeChatFactory : IChatFactory
    {
        public IChat OurFakeChat;
        public IChat OurChat => OurFakeChat;

        public IChat CreateChat(ChatViewModel chatViewModel)
        {
            OurFakeChat = new FakeChat(chatViewModel);
            return OurFakeChat;
        }
    }
}
