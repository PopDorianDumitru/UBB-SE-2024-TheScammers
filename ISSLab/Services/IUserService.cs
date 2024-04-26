using ISSLab.Model;

namespace ISSLab.Services
{
    public interface IUserService
    {
        void AddPostToCart(Guid groupId, Guid postId, Guid userId);
        void AddPostToFavorites(Guid groupId, Guid postId, Guid userId);
        void AddReview(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime date, int rating);
        void AddUser(User user);
        List<Post> GetFavoritePosts(Guid groupId, Guid userId);
        User GetUserById(Guid id);
        List<User> GetUsers();
        bool IsUserInGroup(Guid userId, Guid groupId);
        void RemovePostFromCart(Guid groupId, Guid postId, Guid userId);
        void RemovePostFromFavorites(Guid groupId, Guid postId, Guid userId);
        void RemoveUser(User user);
        void UpdateUserUsername(Guid user, string username);
        List<Post> GetItemsFromCart(Guid userId, Guid groupId);
    }
}