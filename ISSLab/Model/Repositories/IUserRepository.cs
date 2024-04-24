
namespace ISSLab.Model.Repositories
{
    public interface IUserRepository
    {
        void AddReview(Review review);
        void addToCart(Guid groupId, Guid userId, Guid postId);
        void addToFavorites(Guid groupId, Guid userId, Guid postId);
        void AddUser(User newUser);
        void DeleteUser(Guid id);
        List<User> findAllUsers();
        User findById(Guid id);
        void removeFromCart(Guid groupId, Guid userId, Guid postId);
        void removeFromFavorites(Guid groupId, Guid userId, Guid postId);
        void updateGroupsWithRemovingSellingPrivelage(Guid userId, Guid groupId);
        void updateGroupsWithRemovingSellingRequest(Guid userId, Guid groupId);
        void updateGroupsWithSellingPrivelage(Guid id, Guid group);
        void updateGroupsWithSellingRequest(Guid id, Guid group);
        void updateUserDateOfBirth(Guid id, DateOnly newDateOfBirth);
        void updateUserNrOfSells(Guid id, int nrOfSells);
        void updateUserPassword(Guid id, string newPassword);
        void updateUserProfilePicture(Guid id, string newProfilePicture);
        void updateUserRealName(Guid id, string newRealName);
        void updateUserUsername(Guid id, string newUsername);
    }
}