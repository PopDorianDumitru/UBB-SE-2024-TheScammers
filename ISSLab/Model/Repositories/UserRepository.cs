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
    public class UserRepository
    {
        private readonly DataSet dataSet;
        private readonly string connectionString;
        List<User> users;

        public UserRepository(DataSet _dataSet, string _connectionString)
        {
            dataSet = _dataSet;
            connectionString = _connectionString;
            users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectAllUsersQuery = "SELECT * FROM Users";
                string selectAllGroupsWithSellingPrivelage = "SELECT * FROM UsersAndGroupsWithSellingPrivelage";
                string selectAllGroupsWithRequestToSell = "SELECT * FROM UsersAndGroupsWithRequestToSell";
                SqlDataAdapter usersDataAdapter = new SqlDataAdapter(selectAllUsersQuery, connection);
                SqlDataAdapter groupsWithSellingPrivelageDataAdapter = new SqlDataAdapter(selectAllGroupsWithSellingPrivelage, connection);
                SqlDataAdapter groupsWithRequestToSell = new SqlDataAdapter(selectAllGroupsWithRequestToSell, connection);
                usersDataAdapter.Fill(dataSet, "Users");
                groupsWithSellingPrivelageDataAdapter.Fill(dataSet, "UsersAndGroupsWithSellingPrivelage");
                groupsWithRequestToSell.Fill(dataSet, "UsersAndGroupsWithRequestToSell");
                DataTable usersTable = dataSet.Tables["Users"];
                DataTable sellingPrivelageTable = dataSet.Tables["UsersAndGroupsWithSellingPrivelage"];
                DataTable requestToSellTable = dataSet.Tables["UsersAndGroupsWithRequestToSell"];
                foreach(DataRow row in usersTable.Rows)
                {
                    Guid id = (Guid)row["id"];
                    string username = (string)row["username"];
                    string realName = (string)row["real_name"];
                    DateOnly dateOfBirth = (DateOnly)row["date_of_birth"];
                    string profilePicture = (string)row["profile_picture"];
                    string password = (string)row["password"];
                    DateTime creationDate = (DateTime)row["creation_date"];
                    List<Guid> sellPrivelageGroups = new List<Guid>();
                    List<Guid> requestToSellGroups = new List<Guid>(); 
                    foreach(DataRow row2 in sellingPrivelageTable.Rows)
                    {
                        if ((Guid)row2["user_id"] == id)
                        {
                            sellPrivelageGroups.Add((Guid)row2["group_id"]);
                        }
                    }
                    foreach(DataRow row3 in requestToSellTable.Rows)
                    {
                        if ((Guid)row3["user_id"] == id)
                        {
                            requestToSellGroups.Add((Guid)row3["group_id"]);
                        }
                    }

                    users.Add(new User(id, username, realName, dateOfBirth, profilePicture, password, creationDate, sellPrivelageGroups, requestToSellGroups));
                }
            }
        }
        //private Guid id;
        //private string username;
        //private string realName;
        //private DateOnly dateOfBirth;
        //private string profilePicture;
        //private string password;
        //private DateTime creationDate;
        //private List<Guid> groupsWithSellingPrivelage;
        //private List<Guid> groupsWithActiveRequestToSell;

        public List<User> findAllUsers()
        {
            return users;
        }
        public void AddUser(User newUser)
        {
            DataRow newRow = dataSet.Tables["Users"].NewRow();
            newRow["Id"] = newUser.Id.ToString();
            newRow["username"] = newUser.Username;
            newRow["real_name"] = newUser.RealName;
            newRow["date_of_birth"] = newUser.DateOfBirth;
            newRow["profile_picture"] = newUser.ProfilePicture;
            newRow["password"] = newUser.Password;
            newRow["creation_date"] = newUser.CreationDate;
            List<Guid> groupsWithSellingPrivelage = newUser.GroupsWithSellingPrivelage;
            for(int i = 0; i < groupsWithSellingPrivelage.Count; i++)
            {
                DataRow newSellingGroupsRow = dataSet.Tables["UsersAndGroupsWithSellingPrivelage"].NewRow();
                newSellingGroupsRow["user_id"] = newUser.Id;
                newSellingGroupsRow["group_id"] = groupsWithSellingPrivelage[i];
            }
            List<Guid> groupsWithRequestToSell = newUser.GroupsWithActiveRequestToSell;
            for(int i = 0; i < groupsWithRequestToSell.Count; i++)
            {
                DataRow newSellingRequestGroupsRow = dataSet.Tables["UsersAndGroupsWithRequestToSell"].NewRow();
                newSellingRequestGroupsRow["user_id"] = newUser.Id;
                newSellingRequestGroupsRow["group_id"] = groupsWithRequestToSell[i];
            }
            users.Add(newUser);
        }

        public void updateUserUsername(Guid id, string newUsername)
        {
            DataRow row = dataSet.Tables["Users"].Rows.Find(id);
            if (row != null)
            {
                row["username"] = newUsername;
            }

            for(int i = 0; i <  users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    users[i].Username = newUsername;
                    break;
                }
            }
        }

        public void updateGroupsWithSellingPrivelage(Guid id, Guid group)
        {
            DataRow row = dataSet.Tables["UsersAndGroupsWithSellingPrivelage"].NewRow();
            row["user_id"] = id.ToString();
            row["group_id"] = group.ToString();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    users[i].receivedPrivelageToSell(group);
                    break;
                }
            }
        }

        public void updateGroupsWithSellingRequest(Guid id, Guid group)
        {
            DataRow row = dataSet.Tables["UsersAndGroupsWithRequestToSell"].NewRow();
            row["user_id"] = id.ToString();
            row["group_id"] = group.ToString();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    users[i].requestSellingAccess(group);
                    break;
                }
            }
        }

        public void updateUserRealName(Guid id, string newRealName)
        {
            DataRow row = dataSet.Tables["Users"].Rows.Find(id);
            if (row != null)
            {
                row["real_name"] = newRealName;
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    users[i].RealName = newRealName;
                    break;
                }
            }
        }

        public void updateUserDateOfBirth(Guid id, DateOnly newDateOfBirth)
        {
            DataRow row = dataSet.Tables["Users"].Rows.Find(id);
            if (row != null)
            {
                row["date_of_birth"] = newDateOfBirth;
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    users[i].DateOfBirth = newDateOfBirth;
                    break;
                }
            }
        }

        public void updateUserProfilePicture(Guid id, string newProfilePicture)
        {
            DataRow row = dataSet.Tables["Users"].Rows.Find(id);
            if (row != null)
            {
                row["profile_picture"] = newProfilePicture;
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    users[i].ProfilePicture = newProfilePicture;
                    break;
                }
            }
        }

        public void updateUserPassword(Guid id, string newPassword)
        {
            DataRow row = dataSet.Tables["Users"].Rows.Find(id);

            if (row != null)
            {
                row["password"] = newPassword;
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    users[i].Password = newPassword;
                    break;
                }
            }
        }

        public void DeleteUser(Guid id)
        {
            DataRow userRow = dataSet.Tables["Users"].Rows.Find(id);
            if (userRow != null)
            {
                userRow.Delete();
            }

            for(int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    users.RemoveAt(i);
                    break;
                }
            }

            DataTable groupsWithSellingPrivelageTable = dataSet.Tables["UsersAndGroupsWithSellingPrivelage"];

            // Create a list to store the rows that match the specified age
            List<DataRow> rowsToDelete = new List<DataRow>();

            // Iterate through each row in the DataTable
            foreach (DataRow row in groupsWithSellingPrivelageTable.Rows)
            {
                // Check if the "Age" column value matches the specified age
                if (row["user_id"] != DBNull.Value && (Guid)row["user_id"] == id)
                {
                    // If the condition is met, add the row to the list of rows to delete
                    rowsToDelete.Add(row);
                }
            }

            // Remove the rows from the DataTable
            foreach (DataRow rowToDelete in rowsToDelete)
            {
                groupsWithSellingPrivelageTable.Rows.Remove(rowToDelete);
            }

            DataTable groupsWithRequestToSellTable = dataSet.Tables["UsersAndGroupsWithRequestToSell"];

            // Create a list to store the rows that match the specified age
            List<DataRow> rowsToDelete2 = new List<DataRow>();

            // Iterate through each row in the DataTable
            foreach (DataRow row in groupsWithRequestToSellTable.Rows)
            {
                // Check if the "Age" column value matches the specified age
                if (row["user_id"] != DBNull.Value && (Guid)row["user_id"] == id)
                {
                    // If the condition is met, add the row to the list of rows to delete
                    rowsToDelete2.Add(row);
                }
            }

            // Remove the rows from the DataTable
            foreach (DataRow rowToDelete in rowsToDelete2)
            {
                groupsWithRequestToSellTable.Rows.Remove(rowToDelete);
            }
        }
    }
}
