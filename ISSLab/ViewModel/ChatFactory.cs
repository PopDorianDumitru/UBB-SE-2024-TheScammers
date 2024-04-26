using ISSLab.Model;
using ISSLab.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.ViewModel
{
    internal class ChatFactory : IChatFactory
    {
        public IChat CreateChat(ChatViewModel chatViewModel)
        {
            IChat realChat = new Chat(chatViewModel);
            return realChat;
        }
    }
}
