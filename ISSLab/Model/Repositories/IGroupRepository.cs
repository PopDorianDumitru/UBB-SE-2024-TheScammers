
namespace ISSLab.Model.Repositories
{
    public interface IGroupRepository
    {
        void AddGroup(Group newGroup);
        List<Group> FindAll();
        Group FindById(Guid id);
        void RemoveGroup(Guid id);
        void UpdateGroupBigSellersAdd(Guid id, Guid user);
        void UpdateGroupBigSellersRemove(Guid id, Guid user);
        void UpdateGroupPostsAdd(Guid id, Post post);
        void UpdateGroupPostsRemove(Guid id, Post post);
        void UpdateGroupsRequestedUsersAdd(Guid id, Guid user);
        void UpdateGroupsRequestedUsersRemove(Guid id, Guid user);
        void UpdateGroupsSellersAdd(Guid id, Guid user);
        void UpdateGroupsSellersRemove(Guid id, Guid user);
    }
}