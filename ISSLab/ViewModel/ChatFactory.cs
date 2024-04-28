using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
using ISSLab.View;

namespace ISSLab.ViewModel
{
    public class ChatFactory : IChatFactory
    {
        public IChat RealChat;
        public IChat OurChat => RealChat;

        public IChat CreateChat(ChatViewModel chatViewModel)
        {
            RealChat = new Chat(chatViewModel);
            return RealChat;
        }
    }
}
