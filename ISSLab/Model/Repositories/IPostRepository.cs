
namespace ISSLab.Model.Repositories
{
    public interface IPostRepository
    {
        void AddPost(Post newPost);
        List<Post> GetAllPosts();
        Post GetPostById(Guid id);
        void RemovePost(Guid id);
    }
}