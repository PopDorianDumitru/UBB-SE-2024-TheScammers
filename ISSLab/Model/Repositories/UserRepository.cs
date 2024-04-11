using ISSLab.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ISSLab.Model.Repositories
{
    class UserRepository
    {
        private readonly DataSet dataSet;
        private readonly string connectionString;
        List<User> users;

        SqlDataAdapter usersDataAdapter;
        //SqlDataAdapter groupsWithRequestToSell;
        SqlDataAdapter postInteractionsDataAdapter;
        SqlDataAdapter membersDataAdapter;
        SqlDataAdapter reviewsDataAdapter;
        SqlDataAdapter postsDataAdapter;

        SqlCommandBuilder userCommandBuilder;
        SqlCommandBuilder postInteractionsCommandBuilder;
        SqlCommandBuilder membersCommandBuilder;
        SqlCommandBuilder reviewsCommandBuilder;
        SqlCommandBuilder postsCommandBuilder;

        public UserRepository()
        {
            dataSet = new DataSet();
            connectionString = "data source=DESKTOP-GIKO44L\\SQLEXPRESS;initial catalog=ISSMarketplace;trusted_connection=true";
            users = new List<User>();
        }
        public UserRepository(DataSet _dataSet)
        {
            dataSet = _dataSet;
            connectionString = "data source=DESKTOP-GIKO44L\\SQLEXPRESS;initial catalog=ISSMarketplace;trusted_connection=true";
            users = new List<User>();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectAllUsersQuery = "SELECT * FROM Users";
            //string selectAllGroupsWithSellingPrivelage = "SELECT * FROM UsersAndGroupsWithSellingPrivelage";
            string selectAllGroupsWithRequestToSell = "SELECT * FROM UsersAndGroupsWithRequestToSell";
            //string selectAllCarts = "SELECT * FROM Carts";
            //string selectAllFavorites = "SELECT * FROM Favorites";
            string selectAllPostInteractions = "SELECT * FROM PostInteractions";
            string selectAllMembers = "SELECT * FROM Members";
            string selectAllReviews = "SELECT * FROM Reviews";
            string selectAllPosts = "SELECT * FROM Posts";
            usersDataAdapter = new SqlDataAdapter(selectAllUsersQuery, connection);
            //SqlDataAdapter groupsWithSellingPrivelageDataAdapter = new SqlDataAdapter(selectAllGroupsWithSellingPrivelage, connection);
            //groupsWithRequestToSell = new SqlDataAdapter(selectAllGroupsWithRequestToSell, connection);
            //SqlDataAdapter cartsDataAdapter = new SqlDataAdapter(selectAllCarts, connection);
            //SqlDataAdapter favoritesDataAdapter = new SqlDataAdapter(selectAllFavorites, connection);
            postInteractionsDataAdapter = new SqlDataAdapter (selectAllPostInteractions, connection);
            membersDataAdapter = new SqlDataAdapter(selectAllMembers, connection);
            reviewsDataAdapter = new SqlDataAdapter(selectAllReviews, connection);
            postsDataAdapter = new SqlDataAdapter(selectAllPosts, connection);

            userCommandBuilder = new SqlCommandBuilder(usersDataAdapter);
            postInteractionsCommandBuilder = new SqlCommandBuilder(postInteractionsDataAdapter);
            membersCommandBuilder = new SqlCommandBuilder(membersDataAdapter);
            reviewsCommandBuilder = new SqlCommandBuilder(reviewsDataAdapter);
            postsCommandBuilder = new SqlCommandBuilder (postsDataAdapter);

            usersDataAdapter.Fill(dataSet, "Users");
            //groupsWithSellingPrivelageDataAdapter.Fill(dataSet, "UsersAndGroupsWithSellingPrivelage");
            //groupsWithRequestToSell.Fill(dataSet, "UsersAndGroupsWithRequestToSell");
            //cartsDataAdapter.Fill(dataSet, "Carts");
            //favoritesDataAdapter.Fill(dataSet, "Favorites");
            postInteractionsDataAdapter.Fill(dataSet, "PostInteractions");
            membersDataAdapter.Fill(dataSet, "Members");
            reviewsDataAdapter.Fill(dataSet, "Reviews");
            postsDataAdapter.Fill(dataSet, "Posts");

            DataTable usersTable = dataSet.Tables["Users"];
            //DataTable sellingPrivelageTable = dataSet.Tables["UsersAndGroupsWithSellingPrivelage"];
            DataTable requestToSellTable = dataSet.Tables["UsersAndGroupsWithRequestToSell"];
            //DataTable cartsTable = dataSet.Tables["Carts"];
            //DataTable favoritesTable = dataSet.Tables["Favorites"];
            DataTable postInteractionsTable = dataSet.Tables["PostInteractions"];
            DataTable membersTable = dataSet.Tables["Members"];
            DataTable reviewsTable = dataSet.Tables["Reviews"];
            DataTable postsTable = dataSet.Tables["Posts"];

            foreach (DataRow row in usersTable.Rows)
            {
                Guid id = (Guid)row["userID"];
                string username = (string)row["username"];
                string realName = (string)row["realName"];
                DateOnly dateOfBirth = (DateOnly)row["dateOfBirth"];
                string profilePicture = (string)row["profilePicture"];
                string password = (string)row["password"];
                DateTime creationDate = (DateTime)row["creationDate"];
                int nrOfSells = (int)row["numberOfSells"];
                List<Guid> sellPrivelageGroups = new List<Guid>();
                List<Guid> requestToSellGroups = new List<Guid>();
                List<SellingUserScore> sellingUserScores = new List<SellingUserScore>();
                foreach (DataRow row2 in membersTable.Rows)
                {
                    if ((Guid)row2["userId"] == id && (bool)row2["hasSellingPrivelage"])
                    {
                        sellPrivelageGroups.Add((Guid)row2["groupID"]);
                        Guid userScoreId = id;
                        Guid groupId = (Guid)row2["groupID"];
                        int score = (int)row2["score"];
                        SellingUserScore sellingUserScore = new SellingUserScore(userScoreId, groupId, score);
                        sellingUserScores.Add(sellingUserScore);
                    }
                }
                foreach (DataRow row3 in membersTable.Rows)
                {
                    if ((Guid)row3["userID"] == id && (bool)row3["requestedToSell"])
                    {
                        requestToSellGroups.Add((Guid)row3["groupID"]);
                    }
                }
                List<Guid> groups = new List<Guid>();
                List<Cart> carts = new List<Cart>();
                foreach (DataRow row2 in membersTable.Rows)
                {
                    if ((Guid)row2["userID"] == id)
                    {
                        Guid groupId = (Guid)row2["groupID"];
                        groups.Add(groupId);
                        List<Guid> posts = new List<Guid>();
                        foreach (DataRow row3 in postInteractionsTable.Rows)
                        {
                            if ((Guid)row3["userID"]== id)
                            {
                                Guid cartUserId = (Guid)row3["userID"];
                                if ((Guid)row3["groupID"] == groupId)
                                {
                                    Guid cartGroupId = (Guid)row3["groupID"];
                                    //foreach(DataRow row4 in postsTable.Rows)
                                    //{
                                    //    if ((Guid)row4["group_id"] == cartGroupId)
                                    //    {
                                    //        Guid postId = (Guid)row4["post_id"];
                                    //        posts.Add(postId);
                                    //    }
                                    //}
                                    Guid postId = (Guid)row3["postID"];
                                    posts.Add(postId);
                                }

                            }
                        }
                        Cart newCart = new Cart(groupId, id, posts);
                        carts.Add(newCart);
                    }
                }

                List<Favorites> favorites = new List<Favorites>();
                for (int i = 0; i <= groups.Count; i++)
                {
                    Guid currGroup = groups[i];
                    List<Guid> posts = new List<Guid>();
                    foreach (DataRow row2 in postInteractionsTable.Rows)
                    {

                        if ((Guid)row2["userID"] == id & (Guid)row2["groupID"] == currGroup)
                        {
                            Guid favoritesUserId = (Guid)row2["userID"];
                            Guid favoritesGroupId = (Guid)row2["groupID"];
                            posts.Add((Guid)row2["postID"]);
                        }
                    }
                    Favorites newFavorites = new Favorites(currGroup, id, posts);
                }
                List<Review> reviews = new List<Review>();
                foreach (DataRow row2 in reviewsTable.Rows)
                {
                    if ((Guid)row2["user_id"] == id)
                    {
                        Guid reviewId = (Guid)row2["reviewID"];
                        Guid reviewerId = (Guid)row2["reviewerID"];
                        Guid sellerId = (Guid)row2["sellerID"];
                        Guid groupId = (Guid)row2["groupID"];
                        String content = (string)row2["content"];
                        DateTime date = (DateTime)row2["creationDate"];
                        int rating = (int)row2["rating"];
                        Review newReview = new Review(reviewId, reviewerId, sellerId, groupId, content, date, rating);
                        reviews.Add(newReview);
                    }
                }
                users.Add(new User(id, username, realName, dateOfBirth, profilePicture, password, creationDate, sellPrivelageGroups, requestToSellGroups, sellingUserScores, carts, favorites, groups, reviews, nrOfSells));
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

        public User findById(Guid id)
        {
            for(int i = 0;  i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    return users[i];
                }
            }
            throw new Exception("User does not exist");
        }
        public void AddUser(User newUser)
        {
            DataRow newRow = dataSet.Tables["Users"].NewRow();
            newRow["userID"] = newUser.Id.ToString();
            newRow["username"] = newUser.Username;
            newRow["realName"] = newUser.RealName;
            //newRow["dateOfBirth"] = newUser.DateOfBirth;
            DateTime dateTime = new DateTime(newUser.DateOfBirth.Year, newUser.DateOfBirth.Month, newUser.DateOfBirth.Day, 0, 0, 0);
            newRow["dateOfBirth"] = dateTime;
            newRow["profilePicture"] = newUser.ProfilePicture;
            newRow["password"] = newUser.Password;
            newRow["creationDate"] = newUser.CreationDate;
            dataSet.Tables["Users"].Rows.Add(newRow);
            usersDataAdapter.Update(dataSet, "Users");
            //List<Guid> groupsWithSellingPrivelage = newUser.GroupsWithSellingPrivelage;
            //for (int i = 0; i < groupsWithSellingPrivelage.Count; i++)
            //{
            //    DataRow newSellingGroupsRow = dataSet.Tables["UsersAndGroupsWithSellingPrivelage"].NewRow();
            //    newSellingGroupsRow["user_id"] = newUser.Id;
            //    newSellingGroupsRow["group_id"] = groupsWithSellingPrivelage[i];
            //}
            //List<Guid> groupsWithRequestToSell = newUser.GroupsWithActiveRequestToSell;
            //for (int i = 0; i < groupsWithRequestToSell.Count; i++)
            //{
            //    DataRow newSellingRequestGroupsRow = dataSet.Tables["UsersAndGroupsWithRequestToSell"].NewRow();
            //    newSellingRequestGroupsRow["user_id"] = newUser.Id;
            //    newSellingRequestGroupsRow["group_id"] = groupsWithRequestToSell[i];
            //}
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
            DataRow row = dataSet.Tables["Members"].NewRow();
            row["userID"] = id.ToString();
            row["groupID"] = group.ToString();
            row["hasSellingPrivelage"] = 1;

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
            DataRow row = dataSet.Tables["Members"].NewRow();
            row["userID"] = id.ToString();
            row["groupID"] = group.ToString();
            row["requestedToSell"] = 1;

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == id)
                {
                    users[i].requestSellingAccess(group);
                    break;
                }
            }
        }

        public void updateGroupsWithRemovingSellingRequest(Guid userId, Guid groupId)
        {
            //DataRow row = dataSet.Tables["Members"].Rows.Fi
            //if (row != null)
            //{
            //    dataSet.Tables["UsersAndGroupsWithRequestToSell"].Rows.Remove(row);
            //}

            foreach(DataRow row in dataSet.Tables["Members"].Rows)
            {
                if ((Guid)row["userID"] == userId && (Guid)row["groupID"] == groupId)
                {
                    row["requestedToSell"] = 0;
                }
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == userId)
                {
                    users[i].accessToSellDenied(groupId);
                    break;
                }
            }
        }
        public void updateGroupsWithRemovingSellingPrivelage(Guid userId, Guid groupId)
        {
            //DataRow row = dataSet.Tables["UsersAndGroupsWithSellingPrivelage"].Rows.Find((DataRow r) => (string)(r["user_id"]) == userId.ToString() && (string)(r["group_id"]) == groupId.ToString());
            //if (row != null)
            //{
            //    dataSet.Tables["UsersAndGroupsWithSellingPrivelage"].Rows.Remove(row);
            //}

            foreach (DataRow row in dataSet.Tables["Members"].Rows)
            {
                if ((Guid)row["userID"] == userId && (Guid)row["groupID"] == groupId)
                {
                    row["hasSellingPrivelage"] = 0;
                }
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == userId)
                {
                    users[i].accessToSellWasTaken(groupId);
                    break;
                }
            }
        }


        public void updateUserRealName(Guid id, string newRealName)
        {
            DataRow row = dataSet.Tables["Users"].Rows.Find(id);
            if (row != null)
            {
                row["realName"] = newRealName;
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
                row["dateOfBirth"] = newDateOfBirth;
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

        public void AddReview(Review review)
        {
            DataRow row = dataSet.Tables["Reviews"].NewRow();
            row["reviewID"] = review.Id;
            row["reviewerID"] = review.ReviewerId;
            row["sellerID"] = review.SellerId;
            row["groupID"] = review.GroupId;
            row["content"] = review.Content;
            row["creationDate"] = review.Date;
            row["rating"] = review.Rating;
            users.Find(u => u.Id == review.SellerId).AddReview(review);
        }
            
     

        public void updateUserProfilePicture(Guid id, string newProfilePicture)
        {
            DataRow row = dataSet.Tables["Users"].Rows.Find(id);
            if (row != null)
            {
                row["profilePicture"] = newProfilePicture;
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

        public void updateUserNrOfSells(Guid id, int nrOfSells)
        {
            DataRow row = dataSet.Tables["Users"].Rows.Find(id);

            if (row != null)
            {
                row["numberOfSells"] = nrOfSells;
            }

            for(int i = 0; i < users.Count;i++)
            {
                if (users[i].Id == id)
                {
                    users[i].NrOfSells = nrOfSells;
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

            DataTable postInteractionsTable = dataSet.Tables["PostInteractions"];

            // Create a list to store the rows that match the specified age
            List<DataRow> rowsToDelete = new List<DataRow>();

            // Iterate through each row in the DataTable
            foreach (DataRow row in postInteractionsTable.Rows)
            {
                // Check if the "Age" column value matches the specified age
                if (row["userID"] != DBNull.Value && (Guid)row["userID"] == id)
                {
                    // If the condition is met, add the row to the list of rows to delete
                    rowsToDelete.Add(row);
                }
            }

            // Remove the rows from the DataTable
            foreach (DataRow rowToDelete in rowsToDelete)
            {
                postInteractionsTable.Rows.Remove(rowToDelete);
            }

            DataTable membersTable = dataSet.Tables["Members"];

            // Create a list to store the rows that match the specified age
            List<DataRow> rowsToDelete2 = new List<DataRow>();

            // Iterate through each row in the DataTable
            foreach (DataRow row in membersTable.Rows)
            {
                // Check if the "Age" column value matches the specified age
                if (row["userID"] != DBNull.Value && (Guid)row["userID"] == id)
                {
                    // If the condition is met, add the row to the list of rows to delete
                    rowsToDelete2.Add(row);
                }
            }

            // Remove the rows from the DataTable
            foreach (DataRow rowToDelete in rowsToDelete2)
            {
                membersTable.Rows.Remove(rowToDelete);
            }
        }

        public void addToCart(Guid groupId, Guid userId, Guid postId )
        {
            //DataRow row = dataSet.Tables["Carts"].NewRow();
            //row["user_id"] = userId;
            //row["group_id"] = groupId;
            //row["post_id"] = postId;
            foreach (DataRow row in dataSet.Tables["PostInteractions"].Rows)
            {
                if ((Guid)row["userID"] == userId && (Guid)row["groupID"] == groupId && (Guid)row["postID"] == postId)
                {
                    row["addedToCart"] = 1;
                }
            }

            Cart cart = users.Find(user => user.Id == userId).Carts.Find(c => c.GroupId == groupId);
            if(cart == null)
            {
                cart = new Cart(groupId, userId);
                users.Find(user=>user.Id == userId).Carts.Add(cart);
            }
            if (cart.Posts.Contains(postId))
                return;
            cart.Posts.Add(postId);
        }

        public void removeFromCart(Guid groupId, Guid userId, Guid postId)
        {

            foreach (DataRow row in dataSet.Tables["PostInteractions"].Rows)
            {
                if ((Guid)row["userID"] == userId && (Guid)row["groupID"] == groupId && (Guid)row["postID"] == postId)
                {
                    row["addedToCart"] = 0;
                }
            }

            //DataRow selectedRow = dataSet.Tables["Carts"].Rows.Find((DataRow r) => Guid.Parse((string)r["groupID"]) == groupId && Guid.Parse((string)r["user_id"]) == userId && Guid.Parse((string)r["post_id"]) == postId);
            //if(selectedRow !=  null)
            //    dataSet.Tables["Carts"].Rows.Remove(selectedRow);
            users.Find(u=>u.Id == userId).Carts.Find(c=>c.GroupId==groupId).Posts.Remove(postId);

        }

        public void addToFavorites(Guid groupId, Guid userId, Guid postId)
        {
            //DataRow row = dataSet.Tables["Favorites"].NewRow();
            //row["user_id"] = userId;
            //row["group_id"] = groupId;
            //row["post_id"] = postId;
            foreach (DataRow row in dataSet.Tables["PostInteractions"].Rows)
            {
                if ((Guid)row["userID"] == userId && (Guid)row["groupID"] == groupId && (Guid)row["postID"] == postId)
                {
                    row["favorited"] = 1;
                }
            }
            Favorites favoriteFromGroup = users.Find(user => user.Id == userId).Favorites.Find(c => c.GroupId == groupId);
            if (favoriteFromGroup == null)
            {
                favoriteFromGroup = new Favorites(userId, groupId);
                users.Find(user => user.Id == userId).Favorites.Add(favoriteFromGroup);
            }
            if (favoriteFromGroup.Posts.Contains(postId))
                return;
            favoriteFromGroup.Posts.Add(postId);
        }

        public void removeFromFavorites(Guid groupId, Guid userId, Guid postId)
        {
            foreach (DataRow row in dataSet.Tables["PostInteractions"].Rows)
            {
                if ((Guid)row["userID"] == userId && (Guid)row["groupID"] == groupId && (Guid)row["postID"] == postId)
                {
                    row["favorited"] = 0;
                }
            }
            //DataRow selectedRow = dataSet.Tables["Favorites"].Rows.Find((DataRow r) => Guid.Parse((string)r["groupID"]) == groupId && Guid.Parse((string)r["user_id"]) == userId && Guid.Parse((string)r["post_id"]) == postId);
            //if (selectedRow != null)
            //    dataSet.Tables["Favorites"].Rows.Remove(selectedRow);
            users.Find(u => u.Id == userId).Carts.Find(c => c.GroupId == groupId).Posts.Remove(postId);
        }

    }
}
