namespace ISSLab.Model.Repositories
{
    public interface IGroupRepository
    {
        void AddGroup(Group newGroup);
        List<Group> FindAll();
        Group FindById(Guid id);
        void RemoveGroup(Guid id);
    }
}