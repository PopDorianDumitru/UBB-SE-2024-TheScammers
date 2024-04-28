using ISSLab.Model;
using ISSLab.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.ViewModel
{
    public interface IChatFactory
    {
        IChat chat { get; }
        IChat CreateChat(ChatViewModel chatViewModel);
    }
}
