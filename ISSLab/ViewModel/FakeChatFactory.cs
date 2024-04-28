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
        public IChat _fakeChat;
        public IChat chat => _fakeChat;

        public IChat CreateChat(ChatViewModel chatViewModel)
        {
            _fakeChat = new FakeChat(chatViewModel);
            return _fakeChat;
        }
    }
}
