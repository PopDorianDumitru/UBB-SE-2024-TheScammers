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
    class GroupRepository
    {
        private readonly DataSet dataSet;
        private readonly string connectionString;
        List<Group> groups;
        SqlDataAdapter groupsDataAdapter;
        SqlDataAdapter membersDataAdapter;
        SqlDataAdapter postsDataAdapter;


        public GroupRepository()
        {
            dataSet = new DataSet();
            connectionString = "data source=DESKTOP-GIKO44L\\SQLEXPRESS;initial catalog=ISSMarketplace;trusted_connection=true";
            groups = new List<Group>();
        }
        public GroupRepository(DataSet _dataSet)
        {
            dataSet = _dataSet;
            connectionString = "data source=DESKTOP-GIKO44L\\SQLEXPRESS;initial catalog=ISSMarketplace;trusted_connection=true";
            groups = new List<Group>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectAllGroups = "SELECT * FROM Groups";
                string selectAllMembers = "SELECT * FROM Members";
                string selectAllPosts = "SELECT * FROM Posts";
                //string selectAllBigSellers = "SELECT * FROM BigSellers";
                //string selectAllAdmins = "SELECT * FROM Admins";
                //string selectAllGroupsWithSellingPrivelage = "SELECT * FROM UsersAndGroupsWithSellingPrivelage";
                //string selectAllGroupsWithRequestToSell = "SELECT * FROM UsersAndGroupsWithRequestToSell";


                groupsDataAdapter = new SqlDataAdapter(selectAllGroups, connection);
                membersDataAdapter = new SqlDataAdapter(selectAllMembers, connection);
                postsDataAdapter = new SqlDataAdapter(selectAllPosts, connection);
                //SqlDataAdapter bigSellersDataAdapter = new SqlDataAdapter(selectAllBigSellers, connection);
                //SqlDataAdapter groupsWithSellingPrivelageDataAdapter = new SqlDataAdapter(selectAllGroupsWithSellingPrivelage, connection);
                //SqlDataAdapter groupsWithRequestToSell = new SqlDataAdapter(selectAllGroupsWithRequestToSell, connection);
                //SqlDataAdapter adminsDataAdapter = new SqlDataAdapter(selectAllAdmins, connection);


                groupsDataAdapter.Fill(dataSet, "Groups");
                groupsDataAdapter.Fill(dataSet, "Members");
                postsDataAdapter.Fill(dataSet, "Posts");
                //bigSellersDataAdapter.Fill(dataSet, "BigSellers");
                //groupsWithRequestToSell.Fill(dataSet, "UsersAndGroupsWithRequestToSell");
                //groupsWithSellingPrivelageDataAdapter.Fill(dataSet, "UsersAndGroupsWithSellingPrivelage");
                //adminsDataAdapter.Fill(dataSet, "Admins");


                DataTable groupsTable = dataSet.Tables["Groups"];
                DataTable membersTable = dataSet.Tables["Members"];
                DataTable postsTable = dataSet.Tables["Posts"];
                //DataTable bigSellersTable = dataSet.Tables["BigSellers"];
                //DataTable sellingPrivelageTable = dataSet.Tables["UsersAndGroupsWithSellingPrivelage"];
                //DataTable requestToSellTable = dataSet.Tables["UsersAndGroupsWithRequestToSell"];
                //DataTable adminsTable = dataSet.Tables["Admins"];


                foreach (DataRow row in groupsTable.Rows)
                {
                    Guid id = (Guid)row["groupID"];
                    string name = (string)row["name"];
                    int memberCount = (int)row["memberCount"];
                    string description = (string)row["description"];
                    string type = (string)row["type"];
                    string banner = (string)row["banner"];
                    DateTime creationDate = (DateTime)row["creationDate"];

                    List<Guid> members = new List<Guid>();

                    foreach (DataRow row2 in membersTable.Rows)
                    {
                        if ((Guid)row2["groupID"] == id)
                        {
                            members.Add((Guid)row2["userID"]);
                        }
                    }

                    List<Guid> posts = new List<Guid>();

                    foreach (DataRow row2 in postsTable.Rows)
                    {
                        if ((Guid)row2["groupID"] == id)
                        {
                            posts.Add((Guid)row2["postID"]);
                        }
                    }

                    List<Guid> bigSellers = new List<Guid>();

                    foreach (DataRow row2 in membersTable.Rows)
                    {
                        if ((Guid)row2["groupID"] == id && (bool)row2["isBigSeller"])
                        {
                            bigSellers.Add((Guid)row2["userID"]);
                        }
                    }

                    List<Guid> admins = new List<Guid>();

                    foreach (DataRow row2 in membersTable.Rows)
                    {
                        if ((Guid)row2["groupID"] == id && (bool)row2["isAdmin"])
                        {
                            admins.Add((Guid)row2["userID"]);
                        }
                    }

                    List<Guid> sellingUsers = new List<Guid>();

                    foreach (DataRow row2 in membersTable.Rows)
                    {
                        if ((Guid)row2["group_id"] == id && (bool)row2["hasSellingPrivelage"])
                        {
                            sellingUsers.Add((Guid)row2["userId"]);
                        }
                    }

                    List<Guid> requestedUsers = new List<Guid>();

                    foreach (DataRow row2 in membersTable.Rows)
                    {
                        if ((Guid)row2["group_id"] == id && (bool)row2["requestedToSell"])
                        {
                            requestedUsers.Add((Guid)row2["userID"]);
                        }
                    }
                    Group newGroup = new Group(id, name, memberCount, members, posts, admins, sellingUsers, description, type, banner, creationDate, bigSellers, requestedUsers);
                    groups.Add(newGroup);
                }
            }
        }

        public List<Group> FindAll()
        {
            return groups;
        }

        public Group FindById(Guid id)
        {
            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    return groups[i];
                }
            }
            throw new Exception("Group does not exist");
        }
        public void RemoveGroup(Guid id)
        {
            DataRow groupRow = dataSet.Tables["Groups"].Rows.Find(id);
            if (groupRow != null)
            {
                groupRow.Delete();
            }

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    groups.RemoveAt(i);
                    break;
                }
            }

            DataTable membersTable = dataSet.Tables["Members"];

            // Create a list to store the rows that match the specified age
            List<DataRow> rowsToDelete = new List<DataRow>();

            // Iterate through each row in the DataTable
            foreach (DataRow row in membersTable.Rows)
            {
                // Check if the "Age" column value matches the specified age
                if (row["group_id"] != DBNull.Value && (Guid)row["group_id"] == id)
                {
                    // If the condition is met, add the row to the list of rows to delete
                    rowsToDelete.Add(row);
                }
            }

            // Remove the rows from the DataTable
            foreach (DataRow rowToDelete in rowsToDelete)
            {
                membersTable.Rows.Remove(rowToDelete);
            }

            DataTable postsTable = dataSet.Tables["Posts"];

            // Create a list to store the rows that match the specified age
            List<DataRow> rowsToDelete2 = new List<DataRow>();

            // Iterate through each row in the DataTable
            foreach (DataRow row in postsTable.Rows)
            {
                // Check if the "Age" column value matches the specified age
                if (row["group_id"] != DBNull.Value && (Guid)row["group_id"] == id)
                {
                    // If the condition is met, add the row to the list of rows to delete
                    rowsToDelete2.Add(row);
                }
            }

            // Remove the rows from the DataTable
            foreach (DataRow rowToDelete in rowsToDelete2)
            {
                postsTable.Rows.Remove(rowToDelete);
            }



            //DataTable bigSellersTable = dataSet.Tables["BigSellers"];

            //// Create a list to store the rows that match the specified age
            //List<DataRow> rowsToDelete3 = new List<DataRow>();

            //// Iterate through each row in the DataTable
            //foreach (DataRow row in bigSellersTable.Rows)
            //{
            //    // Check if the "Age" column value matches the specified age
            //    if (row["group_id"] != DBNull.Value && (Guid)row["group_id"] == id)
            //    {
            //        // If the condition is met, add the row to the list of rows to delete
            //        rowsToDelete3.Add(row);
            //    }
            //}

            //// Remove the rows from the DataTable
            //foreach (DataRow rowToDelete in rowsToDelete3)
            //{
            //   bigSellersTable.Rows.Remove(rowToDelete);
            //}


            //DataTable adminsTable = dataSet.Tables["Admins"];

            //// Create a list to store the rows that match the specified age
            //List<DataRow> rowsToDelete4 = new List<DataRow>();

            //// Iterate through each row in the DataTable
            //foreach (DataRow row in adminsTable.Rows)
            //{
            //    // Check if the "Age" column value matches the specified age
            //    if (row["group_id"] != DBNull.Value && (Guid)row["group_id"] == id)
            //    {
            //        // If the condition is met, add the row to the list of rows to delete
            //        rowsToDelete4.Add(row);
            //    }
            //}

            //// Remove the rows from the DataTable
            //foreach (DataRow rowToDelete in rowsToDelete4)
            //{
            //    adminsTable.Rows.Remove(rowToDelete);
            //}


            //DataTable groupsWithSellingPrivelageTable = dataSet.Tables["UsersAndGroupsWithSellingPrivelage"];

            //// Create a list to store the rows that match the specified age
            //List<DataRow> rowsToDelete5 = new List<DataRow>();
            //// Iterate through each row in the DataTable
            //foreach (DataRow row in groupsWithSellingPrivelageTable.Rows)
            //{
            //    // Check if the "Age" column value matches the specified age
            //    if (row["group_id"] != DBNull.Value && (Guid)row["group_id"] == id)
            //    {
            //        // If the condition is met, add the row to the list of rows to delete
            //        rowsToDelete5.Add(row);
            //    }
            //}

            //// Remove the rows from the DataTable
            //foreach (DataRow rowToDelete in rowsToDelete5)
            //{
            //    groupsWithSellingPrivelageTable.Rows.Remove(rowToDelete);
            //}


            //DataTable groupsWithRequestToSellTable = dataSet.Tables["UsersAndGroupsWithRequestToSell"];

            //// Create a list to store the rows that match the specified age
            //List<DataRow> rowsToDelete6 = new List<DataRow>();

            //// Iterate through each row in the DataTable
            //foreach (DataRow row in groupsWithRequestToSellTable.Rows)
            //{
            //    // Check if the "Age" column value matches the specified age
            //    if (row["group_id"] != DBNull.Value && (Guid)row["group_id"] == id)
            //    {
            //        // If the condition is met, add the row to the list of rows to delete
            //        rowsToDelete6.Add(row);
            //    }
            //}

            //// Remove the rows from the DataTable
            //foreach (DataRow rowToDelete in rowsToDelete6)
            //{
            //    groupsWithRequestToSellTable.Rows.Remove(rowToDelete);
            //}
        }


        public void AddGroup(Group newGroup)
        {
            DataRow row = dataSet.Tables["Groups"].NewRow();
            row["id"] = newGroup.Id.ToString();
            row["name"] = newGroup.Name;
            row["member_count"] = newGroup.MemberCount;
            row["description"] = newGroup.Description;
            row["type"] = newGroup.Type;
            row["banner"] = newGroup.Banner;
            row["creation_date"] = newGroup.CreationDate;
            groups.Add(newGroup);
        }


        public void UpdateGroupBigSellersAdd(Guid id, Guid user)
        {
            DataRow row = dataSet.Tables["Members"].Rows.Find(user);
            if (row != null)
            {
                row["isBigSeller"] = 1;
            }

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    groups[i].AddBigSeller(user);
                    break;
                }
            }
        }

        public void UpdateGroupBigSellersRemove(Guid id, Guid user)
        {
            DataRow row = dataSet.Tables["Members"].Rows.Find(user);
            if(row != null)
            {
                row["isBigSeller"] = 0;
            }

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    groups[i].RemoveBigSeller(user);
                    break;
                }
            }
        }

        public void UpdateGroupsSellersAdd(Guid id, Guid user)
        {
            DataRow row = dataSet.Tables["Members"].Rows.Find(user);
            if (row != null)
            {
                row["hasSellingPrivelage"] = 1;
            }

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    groups[i].AddSellingUser(user);
                    break;
                }
            }
        }

        public void UpdateGroupsSellersRemove(Guid id, Guid user)
        {
            DataRow row = dataSet.Tables["Members"].Rows.Find(user);
            if (row != null)
            {
                row["hasSellingPrivelage"] = 0;
            }

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    groups[i].RemoveSellingUser(user);
                    break;
                }
            }
        }

        public void UpdateGroupsRequestedUsersAdd(Guid id, Guid user)
        {
            DataRow row = dataSet.Tables["Members"].Rows.Find(user);
            if (row != null)
            {
                row["requestedToSell"] = 1;
            }

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    groups[i].AddRequestedUser(user);
                    break;
                }
            }
        }

        public void UpdateGroupsRequestedUsersRemove(Guid id, Guid user)
        {
            DataRow row = dataSet.Tables["Members"].Rows.Find(user);
            if (row != null)
            {
                row["requestedToSell"] = 0;
            }

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    groups[i].RemoveRequestedUser(user);
                    break;
                }
            }
        }


        public void UpdateGroupPostsAdd(Guid id, Post post)
        {
            DataRow newRow = dataSet.Tables["Posts"].NewRow();
            newRow["postID"] = post.Id.ToString();
            newRow["views"] = post.Views;
            newRow["media"] = post.Media;
            newRow["creation_date"] = post.CreationDate;
            newRow["author_id"] = post.AuthorId.ToString();
            newRow["group_id"] = id.ToString();
            newRow["promoted"] = post.Promoted;
            newRow["location"] = post.Location;
            newRow["confirmed"] = post.Confirmed;
            newRow["description"] = post.Description;
            newRow["title"] = post.Title;
            newRow["contacts"] = post.Contacts;
            newRow["type"] = post.Type;

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    groups[i].AddPost(post.Id);
                    break;
                }
            }
        }

        public void UpdateGroupPostsRemove(Guid id, Post post)
        {
            DataRow row = dataSet.Tables["Posts"].Rows.Find(post.Id);
            if(row != null)
            {
                row.Delete();
            }

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Id == id)
                {
                    groups[i].RemovePost(post.Id);
                    break;
                }
            }
        }
    }
}
