
namespace ISSLab.Model.Repositories
{
    public interface IPostRepository
    {
        void AddPost(Post newPost);
        List<Post> GetAll();
        Post GetById(Guid id);
        void RemovePost(Guid id);
        void UpdateContacts(Guid id, string newContacts);
        void UpdateCreationDate(Guid id, DateTime newCreationDate);
        void UpdateDescription(Guid id, string newDescription);
        void UpdateLocation(Guid id, string newLocation);
        void UpdatePostComment(Guid id, Comment comment);
        void UpdatePostFavorite(Guid id, Guid userId, Guid groupId);
        void UpdatePostInterestStatuses(Guid id, InterestStatus status);
        void UpdatePostLike(Guid id, Guid userId);
        void UpdatePostMedia(Guid id, string newMedia);
        void UpdatePostReport(Guid id, Report report);
        void UpdatePostShare(Guid id, Guid userId);
        void UpdatePromoted(Guid id, bool newPromoted);
        void UpdateTitle(Guid id, string newTitle);
        void UpdateType(Guid id, string newType);
    }
}