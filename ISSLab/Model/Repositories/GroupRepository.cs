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
    }
}
