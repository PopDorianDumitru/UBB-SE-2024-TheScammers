using ISSLab.Model;
using ISSLab.Model.Repositories;
using System;

namespace Tests.Model.Repositories
{
    public class GroupRepositoryTests
    {
        private GroupRepository _groupRepository;

        [SetUp]
        public void SetUp()
        {
            _groupRepository = new GroupRepository();
        }

        [Test]
        public void FindAll_NoGroups_ReturnsEmptyList()
        {
            List<Group> actualGroups = _groupRepository.FindAll();
            Assert.That(actualGroups, Is.Empty);
        }

        [Test]
        public void FindAll_AtLeastOneGroup_ReturnsGroupsList()
        {
            Group firstGroup = new Group(Guid.NewGuid(), string.Empty, 0, [], [], [], [], "", "", "", new DateTime(), [], []);
            Group secondGroup = new Group(Guid.NewGuid(), string.Empty, 0, [], [], [], [], "", "", "", new DateTime(), [], []);
            _groupRepository.AddGroup(firstGroup);
            _groupRepository.AddGroup(secondGroup);

            List<Group> expectedGroups = new List<Group>
            {
                firstGroup, secondGroup
            };
            List<Group> actualGroups = _groupRepository.FindAll();
            Assert.That(actualGroups, Has.Count.EqualTo(2));
            Assert.That(actualGroups, Is.EqualTo(expectedGroups));
        }

        [Test]
        public void FindById_InvalidId_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>( () => { _groupRepository.FindById(Guid.Empty); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Group does not exist"));
        }

        [Test]
        public void FindById_ValidId_TheGroupIsReturned()
        {
            Guid existingGuid = Guid.NewGuid();
            Group firstGroup = new Group(existingGuid, string.Empty, 0, [], [], [], [], "", "", "", new DateTime(), [], []);
            _groupRepository.AddGroup(firstGroup);

            Group returnedById = _groupRepository.FindById(existingGuid);
            Assert.That(firstGroup, Is.EqualTo(returnedById));
        }

        [Test]
        public void RemoveGroup_InvalidId_NoGroupIsRemoved()
        {
            _groupRepository.RemoveGroup(Guid.Empty);
            Assert.That(_groupRepository.FindAll(), Is.Empty);
        }

        [Test]
        public void RemoveGroup_ValidId_GroupIsRemoved()
        {
            Guid existingGuid = Guid.NewGuid();
            Group firstGroup = new Group(existingGuid, string.Empty, 0, [], [], [], [], "", "", "", new DateTime(), [], []);
            _groupRepository.AddGroup(firstGroup);

            _groupRepository.RemoveGroup(existingGuid);
            Assert.That(_groupRepository.FindAll(), Is.Empty);
        }

        [Test]
        public void AddGroup_AnyGroup_GroupIsAdded()
        {
            Group firstGroup = new Group(Guid.NewGuid(), string.Empty, 0, [], [], [], [], "", "", "", new DateTime(), [], []);
            _groupRepository.AddGroup(firstGroup);
            Assert.That(_groupRepository.FindAll(), Has.Count.EqualTo(1));
            Assert.That(_groupRepository.FindAll(), Does.Contain(firstGroup));
        }
    }
}
