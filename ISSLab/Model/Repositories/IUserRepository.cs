namespace ISSLab.Model.Repositories
{
    public interface IUserRepository
    {
        void AddReview(Review reviewToBeAdded);
        void AddPostToCart(Guid groupId, Guid userId, Guid postId);
        void AddToFavorites(Guid groupId, Guid userId, Guid postId);
        void AddUser(User newUser);
        void DeleteUser(Guid id);
        List<User> GetAll();
        User GetById(Guid id);
        void RemoveFromCart(Guid groupId, Guid userId, Guid postId);
        void RemoveFromFavorites(Guid groupId, Guid userId, Guid postId);
        void UpdateUserDateOfBirth(Guid userId, DateOnly newDateOfBirth);
        void UpdateUserUsername(Guid userId, string newUsername);
    }
}