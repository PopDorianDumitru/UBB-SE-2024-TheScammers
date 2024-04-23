
namespace ISSLab.Model.Repositories
{
    public interface IPostRepository
    {
        void addPost(Post newPost);
        List<Post> getAll();
        Post getById(Guid id);
        void removePost(Guid id);
        void updateContacts(Guid id, string newContacts);
        void updateCreationDate(Guid id, DateTime newCreationDate);
        void updateDescription(Guid id, string newDescription);
        void updateLocation(Guid id, string newLocation);
        void updatePostComment(Guid id, Comment comment);
        void updatePostFavorite(Guid id, Guid userId, Guid groupId);
        void updatePostInterestStatuses(Guid id, InterestStatus status);
        void updatePostLike(Guid id, Guid userId);
        void updatePostMedia(Guid id, string newMedia);
        void updatePostReport(Guid id, Report report);
        void updatePostShare(Guid id, Guid userId);
        void updatePromoted(Guid id, bool newPromoted);
        void updateTitle(Guid id, string newTitle);
        void updateType(Guid id, string newType);
    }
}