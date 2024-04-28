using ISSLab.Model;
using ISSLab.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.ViewModel
{
    public class ChatFactory : IChatFactory
    {
        public IChat _realChat;
        public IChat chat => _realChat;

        public IChat Get_realChat()
        {
            return _realChat;
        }

        public IChat CreateChat(ChatViewModel chatViewModel)
        {
            _realChat = new Chat(chatViewModel);
            return _realChat;
        }
    }
}
