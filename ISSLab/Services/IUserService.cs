using ISSLab.Model;

namespace ISSLab.Services
{
    public interface IUserService
    {
        void AcceptAccessToSell(Guid userId, Guid groupId);
        void AddItemToCart(Guid groupId, Guid postId, Guid userId);
        void AddItemToFavorites(Guid groupId, Guid postId, Guid userId);
        void AddReview(Guid reviewerId, Guid sellerId, Guid groupId, string content, DateTime date, int rating);
        void AddUser(User user);
        void AddUserToGroup(User user, Group group);
        User CreateUser(string username, string realName, DateOnly dateOfBirth, string profilePicture, string password);
        void DenyAccessToSell(Guid userId, Guid groupId);
        List<Post> GetFavoritePosts(Guid groupId, Guid userId);
        User GetUserById(Guid id);
        List<User> GetUsers();
        double GetUserScore(Guid userId, Guid groupId);
        bool IsUserInGroup(Guid userId, Guid groupId);
        bool IsUserInGroup(User user, Group group);
        void RemoveAccessToSell(Guid userId, Guid groupId);
        void RemoveFromCart(Guid groupId, Guid postId, Guid userId);
        void RemoveFromFavorites(Guid groupId, Guid postId, Guid userId);
        void RemoveUser(User user);
        void RequestAccessToSell(Guid userId, Guid GroupId);
        void UpdateUserDateOfBirth(Guid user, DateOnly dateOfBirth);
        void UpdateUserPassword(Guid user, string password);
        void UpdateUserProfilePicture(Guid user, string profilePicture);
        void UpdateUserRealName(Guid user, string realName);
        void UpdateUserUsername(Guid user, string username);
        bool UserIsAdminInGroup(Guid userId, Guid groupId);
        bool UserIsMemberInGroup(User userId, Guid groupId);
        bool UserIsSellerInGroup(Guid userId, Guid groupId);
        List<Post> GetItemsFromCart(Guid userId, Guid groupId);
    }
}