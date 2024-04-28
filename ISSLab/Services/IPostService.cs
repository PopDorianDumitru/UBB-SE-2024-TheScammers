using ISSLab.Model;

namespace ISSLab.Services
{
    public interface IPostService
    {
        void AddPost(Post post);
        void AddReport(Guid postID, Guid userID, string reason);
        void ConfirmPost(Guid postID);
        void FavoritePost(Guid postID, Guid userID);
        Post GetPostById(Guid id);
        List<Post> GetPosts();
        void RemoveConfirmation(Guid postID);
        void RemovePost(Post post);
        void RemoveReport(Guid postID, Guid userID);
        void UnfavoritePost(Guid postID, Guid userID);
    }
}