using ISSLab.Model;
using ISSLab.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.ViewModel
{
    class PostContentViewModel : ViewModelBase
    {
        private PostService postService;
        private UserService userService;
        private Guid groupId;

        public PostContentViewModel(PostService postService, UserService userService, Guid groupId, Guid postId, Guid userId) : base()
        {
            this.postService = postService;
            this.userService = userService;
            this.groupId = groupId;
            
        }
        public PostContentViewModel()
        {
            postService = new PostService();
            userService = new UserService();
            groupId = Guid.Parse(new Guid().ToString());
        }

 


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

}
