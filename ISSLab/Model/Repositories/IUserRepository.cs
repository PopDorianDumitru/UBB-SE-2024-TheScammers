
namespace ISSLab.Model.Repositories
{
    public interface IUserRepository
    {
        void AddReview(Review review);
        void AddToCart(Guid groupId, Guid userId, Guid postId);
        void AddToFavorites(Guid groupId, Guid userId, Guid postId);
        void AddUser(User newUser);
        void DeleteUser(Guid id);
        List<User> FindAllUsers();
        User FindById(Guid id);
        void RemoveFromCart(Guid groupId, Guid userId, Guid postId);
        void RemoveFromFavorites(Guid groupId, Guid userId, Guid postId);
        void UpdateGroupsWithRemovingSellingPrivelage(Guid userId, Guid groupId);
        void UpdateGroupsWithRemovingSellingRequest(Guid userId, Guid groupId);
        void UpdateGroupsWithSellingPrivelage(Guid id, Guid group);
        void UpdateGroupsWithSellingRequest(Guid id, Guid group);
        void UpdateUserDateOfBirth(Guid id, DateOnly newDateOfBirth);
        void UpdateUserNrOfSells(Guid id, int nrOfSells);
        void UpdateUserPassword(Guid id, string newPassword);
        void UpdateUserProfilePicture(Guid id, string newProfilePicture);
        void UpdateUserRealName(Guid id, string newRealName);
        void UpdateUserUsername(Guid id, string newUsername);
    }
}