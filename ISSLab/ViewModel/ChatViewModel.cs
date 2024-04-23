using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace ISSLab.ViewModel
{
    public class ChatViewModel
    {
        public ObservableCollection<Message> AllMessages { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }

        public ChatViewModel(User user, Post post) { 
            AllMessages = new ObservableCollection<Message>();
            this.User = user;
            this.Post = post;
        }

        public void AddMessage(Message message)
        {
            AllMessages.Add(message);
        }
    }
}
