using ISSLab.Model;

namespace ISSLab.Services
{
    public interface IUserService
    {
        void AddItemToCart(Guid groupId, Guid postId, Guid userId);
        void AddItemToFavorites(Guid groupId, Guid postId, Guid userId);
        void AddReview(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime date, int rating);
        void AddUser(User user);
        void AddUserToGroup(User user, Group group);
        List<Post> GetFavoritePosts(Guid groupId, Guid userId);
        User GetUserById(Guid id);
        List<User> GetUsers();
        bool IsUserInGroup(Guid userId, Guid groupId);
        void RemoveFromCart(Guid groupId, Guid postId, Guid userId);
        void RemoveFromFavorites(Guid groupId, Guid postId, Guid userId);
        void RemoveUser(User user);
        void UpdateUserUsername(Guid user, string username);
        bool UserIsMemberInGroup(User userId, Guid groupId);
        bool UserIsSellerInGroup(Guid userId, Guid groupId);
        List<Post> GetItemsFromCart(Guid userId, Guid groupId);
    }
}