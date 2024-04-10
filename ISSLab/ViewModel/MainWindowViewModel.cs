using ISSLab.Model.Repositories;
using ISSLab.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSLab.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        PostService postService;
        UserService userService;
        GroupRepository groupRepository;
        public ViewModelBase CurrentViewModel { get; }
        public MainWindowViewModel() 
        {
            DataSet dataSet = new DataSet();
            PostRepository postRepo = new PostRepository(dataSet, Guid.Parse("e21f4b3b-f80b-48ea-a309-a25948fd150e"));
            UserRepository userRepo = new UserRepository(dataSet);
            GroupRepository groupRepo = new GroupRepository(dataSet);
            postService = new PostService(postRepo,userRepo,groupRepo);
            userService = new UserService(userRepo,postRepo,groupRepo);
        }



    }
}
