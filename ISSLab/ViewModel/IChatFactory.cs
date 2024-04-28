using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;
using ISSLab.View;

namespace ISSLab.ViewModel
{
    public interface IChatFactory
    {
        IChat OurChat { get; }
        IChat CreateChat(ChatViewModel chatViewModel);
    }
}
