using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISSLab.Model.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private List<Group> _allGroups;

        public GroupRepository()
        {
            _allGroups = new List<Group>();
        }

        public List<Group> FindAll()
        {
            return _allGroups;
        }

        public Group FindById(Guid id)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    return _allGroups[i];
                }
            }
            throw new Exception("Group does not exist");
        }
        
        public void RemoveGroup(Guid id)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    _allGroups.RemoveAt(i);
                    break;
                }
            }
        }

        public void AddGroup(Group newGroup)
        {
            _allGroups.Add(newGroup);
        }

        public void UpdateGroupBigSellersAdd(Guid id, Guid user)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    _allGroups[i].AddBigSeller(user);
                    break;
                }
            }
        }

        public void UpdateGroupBigSellersRemove(Guid id, Guid user)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    _allGroups[i].RemoveBigSeller(user);
                    break;
                }
            }
        }

        public void UpdateGroupsSellersAdd(Guid id, Guid user)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    _allGroups[i].AddSellingUser(user);
                    break;
                }
            }
        }

        public void UpdateGroupsSellersRemove(Guid id, Guid user)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    _allGroups[i].RemoveSellingUser(user);
                    break;
                }
            }
        }

        public void UpdateGroupsRequestedUsersAdd(Guid id, Guid user)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    _allGroups[i].AddRequestedUser(user);
                    break;
                }
            }
        }

        public void UpdateGroupsRequestedUsersRemove(Guid id, Guid user)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    _allGroups[i].RemoveRequestedUser(user);
                    break;
                }
            }
        }
        
        public void UpdateGroupPostsAdd(Guid id, Post post)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    _allGroups[i].AddPost(post.Id);
                    break;
                }
            }
        }

        public void UpdateGroupPostsRemove(Guid id, Post post)
        {
            for (int i = 0; i < _allGroups.Count; i++)
            {
                if (_allGroups[i].Id == id)
                {
                    _allGroups[i].RemovePost(post.Id);
                    break;
                }
            }
        }
    }
}
