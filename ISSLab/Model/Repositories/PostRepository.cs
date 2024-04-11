using ISSLab.Services;
using Lab3_1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace ISSLab.Model.Repositories
{
    class PostRepository
    {
        private readonly DataSet dataSet;
        private readonly string connectionString;
        List<Post> posts;
        Guid groupID;
        SqlDataAdapter postsDataAdapter;
        SqlDataAdapter postInteractionsDataAdapter;
        SqlDataAdapter commentsDataAdapter;
        SqlDataAdapter reportsDataAdapter;

        SqlCommandBuilder postsCommandBuilder;
        SqlCommandBuilder postInteractionsCommandBuilder;
        SqlCommandBuilder commentsCommandBuilder;
        SqlCommandBuilder reportsCommandBuilder;

        public PostRepository()
        {
            dataSet = new DataSet();
            connectionString = "data source=DESKTOP-GIKO44L\\SQLEXPRESS;initial catalog=ISSMarketplace;trusted_connection=true";
            posts = new List<Post>();
            groupID = Guid.NewGuid();
        }
        public PostRepository(DataSet dataSet, Guid _groupID)
        {
            this.dataSet = dataSet;
            this.connectionString = "data source=DESKTOP-GIKO44L\\SQLEXPRESS;initial catalog=ISSMarketplace;trusted_connection=true";
            this.posts = new List<Post>();
            this.groupID = _groupID;

            SqlConnection connection = new SqlConnection(this.connectionString);
            connection.Open();
            string selectAllPostsQuery = "SELECT * FROM Posts";
            //string selectAllSharedPosts = "SELECT * FROM UsersSharedPosts";
            //string selectAllLikedPosts = "SELECT * FROM UsersLikedPosts";
            string selectAllPostInteractions = "SELECT * FROM PostInteractions";
            string selectAllComments = "SELECT * FROM Comments";
            //string selectAllFavoritedPosts = "SELECT * FROM Favorites";
            string selectAllReports = "SELECT * FROM Reports";
            //string selectAllInterestStatuses = "SELECT * FROM InterestStatuses";

            postsDataAdapter = new SqlDataAdapter(selectAllPostsQuery, connection);
            //SqlDataAdapter sharedPostsDataAdapter = new SqlDataAdapter(selectAllSharedPosts, connection);
            //SqlDataAdapter likedPostsDataAdapter = new SqlDataAdapter(selectAllLikedPosts, connection);
            postInteractionsDataAdapter = new SqlDataAdapter(selectAllPostInteractions, connection);
            commentsDataAdapter = new SqlDataAdapter(selectAllComments, connection);
            //SqlDataAdapter favoritedPostsDataAdapter = new SqlDataAdapter(selectAllFavoritedPosts, connection);
            reportsDataAdapter = new SqlDataAdapter(selectAllReports, connection);
            //SqlDataAdapter interestStatusesDataAdapter = new SqlDataAdapter(selectAllInterestStatuses, connection);

            postsCommandBuilder = new SqlCommandBuilder(postsDataAdapter);
            postInteractionsCommandBuilder = new SqlCommandBuilder(postInteractionsDataAdapter);
            commentsCommandBuilder = new SqlCommandBuilder(commentsDataAdapter);
            reportsCommandBuilder = new SqlCommandBuilder(reportsDataAdapter);

            postsDataAdapter.Fill(dataSet, "Posts");
            //sharedPostsDataAdapter.Fill(dataSet, "UsersSharedPosts");
            //likedPostsDataAdapter.Fill(dataSet, "UsersLikedPosts");
            postInteractionsDataAdapter.Fill(dataSet, "PostInteractions");
            commentsDataAdapter.Fill(dataSet, "Comments");
            //favoritedPostsDataAdapter.Fill(dataSet, "Favorites");
            reportsDataAdapter.Fill(dataSet, "Reports");
            //interestStatusesDataAdapter.Fill(dataSet, "InterestStatuses");

            DataTable postsTable = dataSet.Tables["Posts"];
            //DataTable sharedPostsTable = dataSet.Tables["UsersSharedPosts"];
            //DataTable likedPostsTable = dataSet.Tables["UsersLikedPosts"];
            DataTable postInteractionsTable = dataSet.Tables["PostInteractions"];
            DataTable commentsTable = dataSet.Tables["Comments"];
            //DataTable favoritedPostsTable = dataSet.Tables["Favorites"];
            DataTable reportsTable = dataSet.Tables["Reports"];
            //DataTable interestStatusesTable = dataSet.Tables["InterestStatuses"];
            //private Guid id;
            //private List<Guid> usersThatShared;
            //private List<Guid> usersThatLiked;
            //private List<Comment> comments;
            //private string media;
            //private DateTime creationDate;
            //private Guid authorId;
            //private Guid groupId;
            //private bool promoted;
            //private List<Guid> usersThatFavorited;
            //private List<Report> reports;
            //private string location;
            //private string description;
            //private string title;
            //private List<InterestStatus> interestStatuses;
            //private string contacts;
            //private string type;

            foreach (DataRow row1 in postsTable.Rows)
            {
                if ((Guid)row1["groupID"] == groupID)
                {
                    Guid id = (Guid)row1["postID"];
                    List<Guid> usersThatShared = new List<Guid>();
                    foreach (DataRow row2 in postInteractionsTable.Rows)
                    {
                        if ((Guid)row2["postID"] == id && (bool)row2["shared"])
                        {
                            usersThatShared.Add((Guid)row2["userID"]);
                        }
                    }
                    List<Guid> usersThatLiked = new List<Guid>();
                    foreach (DataRow row2 in postInteractionsTable.Rows)
                    {
                        if ((Guid)row2["postID"] == id && (bool)row2["liked"])
                        {
                            usersThatLiked.Add((Guid)row2["userID"]);
                        }
                    }
                    List<Comment> comments = new List<Comment>();
                    // Here we should add all the comments
                    //foreach (DataRow row2 in commentsTable.Rows)
                    //{
                    //    if ((Guid)row2["post_id"] == id)
                    //    {
                    //        Guid
                    //    }
                    //}
                    string media = (string)row1["media"];
                    DateTime creationDate = (DateTime)row1["creationDate"];
                    Guid authorId = (Guid)row1["authorID"];
                    Guid groupId = (Guid)row1["groupID"];
                    bool promoted = (bool)row1["promoted"];
                    List<Guid> usersThatFavorited = new List<Guid>();
                    foreach (DataRow row2 in postInteractionsTable.Rows)
                    {
                        if ((Guid)row2["postID"] == id && (bool)row2["favorited"])
                        {
                            usersThatFavorited.Add((Guid)row2["userID"]);
                        }
                    }
                    List<Report> reports = new List<Report>();
                    //private Guid id;
                    //private Guid userId;
                    //private Guid postId;
                    //private string reason;
                    //private DateTime date;
                    foreach (DataRow row2 in reportsTable.Rows)
                    {
                        if ((Guid)row2["postID"] == id)
                        {
                            Guid reportId = (Guid)row2["reportID"];
                            Guid userId = (Guid)row2["userID"];
                            Guid postId = (Guid)row2["postID"];
                            string reason = (string)row2["reason"];
                            DateTime date = (DateTime)row2["creationDate"];
                            Report newReport = new Report(reportId, userId, postId, reason, date);
                            reports.Add(newReport);
                        }
                    }
                    string location = (string)row1["location"];
                    string description = (string)row1["description"];
                    string title = (string)row1["title"];
                    List<InterestStatus> interstStatuses = new List<InterestStatus>();
                    foreach (DataRow row2 in postInteractionsTable.Rows)
                    {
                        if ((Guid)row2["postID"] == id)
                        {
                            if ((int)row2["interested"] != 0)
                            {
                                Guid userId = (Guid)row2["userID"];
                                Guid postId = (Guid)row2["postID"];
                                bool interested = false;
                                if ((int)row2["interested"] == 1)
                                {
                                    interested = true;
                                }
                                InterestStatus interestStatus = new InterestStatus(userId, postId, interested);
                                interstStatuses.Add(interestStatus);
                            }
                        }
                    }
                    string contacts = (string)row1["phoneNumber"];
                    string type = (string)row1["type"];
                    Post newPost = new Post(id, usersThatShared, usersThatLiked, comments, media, creationDate, authorId, groupId, promoted, usersThatFavorited, location, description, title, interstStatuses, contacts, reports, type, false, 0);
                    posts.Add(newPost);
                }
            }
        }


        public void addPost(Post newPost)
        {
            DataRow newRow = dataSet.Tables["Posts"].NewRow();
            newRow["postID"] = newPost.Id;
            newRow["media"] = newPost.Media;
            newRow["creationDate"] = newPost.CreationDate;
            newRow["authorID"] = newPost.AuthorId;
            newRow["groupID"] = newPost.GroupId;
            newRow["promoted"] = newPost.Promoted;
            newRow["location"] = newPost.Location;
            newRow["description"] = newPost.Description;
            newRow["title"] = newPost.Title;
            newRow["type"] = newPost.Type;
            newRow["phoneNumber"] = newPost.Contacts;
            posts.Add(newPost);
            //dataSet.Tables["Posts"].Rows.Add(newRow);
            //postsDataAdapter.Update(dataSet, "Posts");
        }

        public void removePost(Guid id) {
            DataRow postRow = dataSet.Tables["Posts"].Rows.Find(id);
            if (postRow != null)
            {
                postRow.Delete();
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts.RemoveAt(i);
                    break;
                }
            }

            DataTable postInteractionsTable = dataSet.Tables["PostInteractions"];

            List<DataRow> rowsToDelete = new List<DataRow>();

            // Iterate through each row in the DataTable
            foreach (DataRow row in postInteractionsTable.Rows)
            {
                // Check if the "Age" column value matches the specified age
                if (row["postID"] != DBNull.Value && (Guid)row["postID"] == id)
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


            //DataTable usersThatLikedTable = dataSet.Tables["UsersLikedPosts"];
            //List<DataRow> rowsToDelete2 = new List<DataRow>();
            //foreach (DataRow row in usersThatLikedTable.Rows)
            //{
            //    if (row["post_id"] != DBNull.Value && (Guid)row["post_id"] == id)
            //    {
            //        rowsToDelete2.Add(row);
            //    }
            //}

            //foreach(DataRow rowToDelete in rowsToDelete2)
            //{
            //    usersThatLikedTable.Rows.Remove(rowToDelete);
            //}


            DataTable commentsTable = dataSet.Tables["Comments"];
            List<DataRow> rowsToDelete3 = new List<DataRow>();
            foreach (DataRow row in commentsTable.Rows)
            {
                if (row["postID"] != DBNull.Value && (Guid)row["postID"] == id)
                {
                    rowsToDelete3.Add(row);
                }
            }

            foreach (DataRow rowToDelete in rowsToDelete3)
            {
                commentsTable.Rows.Remove(rowToDelete);
            }

            //DataTable usersThatFavoritedTable = dataSet.Tables["Favorites"];
            //List<DataRow> rowsToDelete4 = new List<DataRow>();
            //foreach (DataRow row in usersThatFavoritedTable.Rows)
            //{
            //    if (row["post_id"] != DBNull.Value && (Guid)row["post_id"] == id)
            //    {
            //        rowsToDelete4.Add(row);
            //    }
            //}

            //foreach (DataRow rowToDelete in rowsToDelete4)
            //{
            //    usersThatFavoritedTable.Rows.Remove(rowToDelete);
            //}

            DataTable reportsTable = dataSet.Tables["Reports"];
            List<DataRow> rowsToDelete5 = new List<DataRow>();
            foreach (DataRow row in reportsTable.Rows)
            {
                if (row["postID"] != DBNull.Value && (Guid)row["postID"] == id)
                {
                    rowsToDelete5.Add(row);
                }
            }

            foreach (DataRow rowToDelete in rowsToDelete5)
            {
                reportsTable.Rows.Remove(rowToDelete);
            }

            //DataTable interestStatusesTable = dataSet.Tables["InterestStatuses"];
            //List<DataRow> rowsToDelete6 = new List<DataRow>();
            //foreach (DataRow row in interestStatusesTable.Rows)
            //{
            //    if (row["post_id"] != DBNull.Value && (Guid)row["post_id"] == id)
            //    {
            //        rowsToDelete6.Add(row);
            //    }
            //}

            //foreach (DataRow rowToDelete in rowsToDelete6)
            //{
            //    interestStatusesTable.Rows.Remove(rowToDelete);
            //}


        }

        public void updatePostShare(Guid id, Guid userId)
        {
            //DataRow row = dataSet.Tables["Posts"].Rows.Find();
            //if (row != null)
            //{
            //    row["media"] = newMedia;
            //}
            bool found = false;
            foreach (DataRow row in dataSet.Tables["PostInteractions"].Rows)
            {
                if ((Guid)row["userID"] == userId && (Guid)row["postID"] == id)
                {
                    row["shared"] = 1;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                DataRow newRow = dataSet.Tables["PostInteractions"].NewRow();
                newRow["postID"] = id;
                newRow["groupID"] = groupID;
                newRow["userID"] = userId;
                newRow["shared"] = 1;
            }


            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].toggleShare(userId);
                    break;
                }
            }
        }

        public void updatePostLike(Guid id, Guid userId)
        {
            bool found = false;
            foreach (DataRow row in dataSet.Tables["PostInteractions"].Rows)
            {
                if ((Guid)row["userID"] == userId && (Guid)row["postID"] == id)
                {
                    row["liked"] = 1;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                DataRow newRow = dataSet.Tables["PostInteractions"].NewRow();
                newRow["postID"] = id;
                newRow["groupID"] = groupID;
                newRow["userID"] = userId;
                newRow["liked"] = 1;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].toggleLike(userId);
                    break;
                }
            }
        }

        public void updatePostComment(Guid id, Comment comment)
        {
            //DataRow row = dataSet.Tables["Comments"].NewRow();
            //row["id"] = comment.Id.ToString();
            //row["content"] = comment.Content;
            //row["user_id"] = comment.UserId;

            //for (int i = 0; i < posts.Count; i++)
            //{
            //    if (posts[i].Id == id)
            //    {
            //        posts[i].addComment(comment);
            //        break;
            //    }
            //}
        }

        public void updatePostMedia(Guid id, string newMedia)
        {
            DataRow row = dataSet.Tables["Posts"].Rows.Find(id);
            if (row != null)
            {
                row["media"] = newMedia;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].Media = newMedia;
                    break;
                }
            }
        }

        public void updateCreationDate(Guid id, DateTime newCreationDate)
        {
            DataRow row = dataSet.Tables["Posts"].Rows.Find(id);
            if (row != null)
            {
                row["creation_date"] = newCreationDate;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].CreationDate = newCreationDate;
                    break;
                }
            }
        }

        public void updatePromoted(Guid id, bool newPromoted)
        {
            DataRow row = dataSet.Tables["Posts"].Rows.Find(id);
            if (row != null)
            {
                row["promoted"] = newPromoted;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].Promoted = newPromoted;
                    break;
                }
            }
        }

        public void updatePostFavorite(Guid id, Guid userId, Guid groupId)
        {
            bool found = false;
            foreach (DataRow row in dataSet.Tables["PostInteractions"].Rows)
            {
                if ((Guid)row["userID"] == userId && (Guid)row["postID"] == id)
                {
                    row["favorited"] = 1;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                DataRow newRow = dataSet.Tables["PostInteractions"].NewRow();
                newRow["postID"] = id;
                newRow["groupID"] = groupID;
                newRow["userID"] = userId;
                newRow["favorited"] = 1;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].toggleFavorite(userId);
                    break;
                }
            }
        }

        public void updatePostReport(Guid id, Report report)
        {
            DataRow row = dataSet.Tables["Reports"].NewRow();
            row["id"] = report.Id.ToString();
            row["user_id"] = report.UserId.ToString();
            row["post_id"] = report.PostId.ToString();
            row["reason"] = report.Reason;
            row["date"] = report.Date;

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].addReport(report);
                    break;
                }
            }
        }

        public void updateLocation(Guid id, string newLocation)
        {
            DataRow row = dataSet.Tables["Posts"].Rows.Find(id);
            if (row != null)
            {
                row["location"] = newLocation;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].Location = newLocation;
                    break;
                }
            }
        }

        public void updateDescription(Guid id, string newDescription)
        {
            DataRow row = dataSet.Tables["Posts"].Rows.Find(id);
            if (row != null)
            {
                row["description"] = newDescription;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].Description = newDescription;
                    break;
                }
            }
        }

        public void updateTitle(Guid id, string newTitle)
        {
            DataRow row = dataSet.Tables["Posts"].Rows.Find(id);
            if (row != null)
            {
                row["title"] = newTitle;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].Title = newTitle;
                    break;
                }
            }
        }

        public void updatePostInterestStatuses(Guid id, InterestStatus status)
        {
            bool found = false;
            foreach (DataRow row in dataSet.Tables["PostInteractions"].Rows)
            {
                if ((Guid)row["userID"] == status.UserId && (Guid)row["postID"] == id)
                {
                    if (status.Interested)
                    {
                        row["interested"] = 1;
                    }
                    else
                    {
                        row["interested"] = 0;
                    }
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                DataRow newRow = dataSet.Tables["PostInteractions"].NewRow();
                newRow["postID"] = id;
                newRow["userID"] = status.UserId;
                if (status.Interested)
                {
                    newRow["interested"] = 1;
                }
                else
                {
                    newRow["interested"] = -1;
                }
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].addInterestStatus(status);
                    break;
                }
            }
        }

        public void updatePostInterestStatusesRemove(Guid id, InterestStatus status)
        {
            foreach (DataRow row in dataSet.Tables["PostInteractions"].Rows)
            {
                if ((Guid)row["userID"] == status.UserId && (Guid)row["postID"] == id)
                {
                    row["interested"] = 0;
                    break;
                }
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].removeInterestStatus(status.UserId);
                    break;
                }
            }

        }




        public void updateContacts(Guid id, string newContacts)
        {
            DataRow row = dataSet.Tables["Posts"].Rows.Find(id);
            if (row != null)
            {
                row["contacts"] = newContacts;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].Contacts = newContacts;
                    break;
                }
            }
        }

        public void updateType(Guid id, string newType)
        {
            DataRow row = dataSet.Tables["Posts"].Rows.Find(id);
            if (row != null)
            {
                row["type"] = newType;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id)
                {
                    posts[i].Type = newType;
                    break;
                }
            }
        }




        public List<Post> getAll()
        {
            return posts;
        }


        public Post getById(Guid id)
        {
            for (int i = 0; i < posts.Count; i++)
            {
                if (posts[i].Id == id) {
                    return posts[i];
                }
            }
            throw new Exception("Post does not exist!");
        }


    }
}
