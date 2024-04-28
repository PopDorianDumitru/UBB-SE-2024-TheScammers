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
        private List<Group> allGroups;

        public GroupRepository()
        {
            allGroups = new List<Group>();
        }

        public List<Group> FindAll()
        {
            return allGroups;
        }

        public Group FindById(Guid id)
        {
            for (int i = 0; i < allGroups.Count; i++)
            {
                if (allGroups[i].GroupId == id)
                {
                    return allGroups[i];
                }
            }
            throw new Exception("Group does not exist");
        }
        public void RemoveGroup(Guid id)
        {
            for (int i = 0; i < allGroups.Count; i++)
            {
                if (allGroups[i].GroupId == id)
                {
                    allGroups.RemoveAt(i);
                    break;
                }
            }
        }

        public void AddGroup(Group newGroup)
        {
            allGroups.Add(newGroup);
        }
    }
}
