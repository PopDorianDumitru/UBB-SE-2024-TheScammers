using System;
using ISSLab.Model;
using ISSLab.Model.Repositories;

namespace Tests.Model.Repositories
{
    public class GroupRepositoryTests
    {
        private GroupRepository groupRepository;

        [SetUp]
        public void SetUp()
        {
            groupRepository = new GroupRepository();
        }

        [Test]
        public void FindAll_NoGroups_ReturnsEmptyList()
        {
            List<Group> actualGroups = groupRepository.FindAll();
            Assert.That(actualGroups, Is.Empty);
        }

        [Test]
        public void FindAll_AtLeastOneGroup_ReturnsGroupsList()
        {
            Group firstGroup = new Group(Guid.NewGuid(), string.Empty, 0, new List<Guid>(), new List<Guid>(), new List<Guid>(), new List<Guid>(),
                string.Empty, string.Empty, string.Empty, new DateTime(), new List<Guid>(), new List<Guid>());
            Group secondGroup = new Group(Guid.NewGuid(), string.Empty, 0, new List<Guid>(), new List<Guid>(), new List<Guid>(), new List<Guid>(), string.Empty, string.Empty, string.Empty, new DateTime(), new List<Guid>(), new List<Guid>());
            groupRepository.AddGroup(firstGroup);
            groupRepository.AddGroup(secondGroup);

            List<Group> expectedGroups = new List<Group>
            {
                firstGroup, secondGroup
            };
            List<Group> actualGroups = groupRepository.FindAll();
            Assert.That(actualGroups, Has.Count.EqualTo(2));
            Assert.That(actualGroups, Is.EqualTo(expectedGroups));
        }

        [Test]
        public void FindById_InvalidId_ExceptionThrown()
        {
            var exceptionMessage = Assert.Throws<Exception>(() => { groupRepository.FindById(Guid.Empty); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("Group does not exist"));
        }

        [Test]
        public void FindById_ValidId_TheGroupIsReturned()
        {
            Guid existingGuid = Guid.NewGuid();
            Group firstGroup = new Group(existingGuid, string.Empty, 0, new List<Guid>(), new List<Guid>(), new List<Guid>(), new List<Guid>(),
                string.Empty, string.Empty, string.Empty, new DateTime(), new List<Guid>(), new List<Guid>());
            groupRepository.AddGroup(firstGroup);

            Group returnedById = groupRepository.FindById(existingGuid);
            Assert.That(firstGroup, Is.EqualTo(returnedById));
        }

        [Test]
        public void RemoveGroup_InvalidId_NoGroupIsRemoved()
        {
            groupRepository.RemoveGroup(Guid.Empty);
            Assert.That(groupRepository.FindAll(), Is.Empty);
        }

        [Test]
        public void RemoveGroup_ValidId_GroupIsRemoved()
        {
            Guid existingGuid = Guid.NewGuid();
            Group firstGroup = new Group(existingGuid, string.Empty, 0, new List<Guid>(), new List<Guid>(), new List<Guid>(), new List<Guid>(), string.Empty, string.Empty, string.Empty, new DateTime(), new List<Guid>(), new List<Guid>());
            groupRepository.AddGroup(firstGroup);

            groupRepository.RemoveGroup(existingGuid);
            Assert.That(groupRepository.FindAll(), Is.Empty);
        }

        [Test]
        public void AddGroup_AnyGroup_GroupIsAdded()
        {
            Group firstGroup = new Group(Guid.NewGuid(), string.Empty, 0, new List<Guid>(), new List<Guid>(), new List<Guid>(), new List<Guid>(), string.Empty, string.Empty, string.Empty, new DateTime(), new List<Guid>(), new List<Guid>());
            groupRepository.AddGroup(firstGroup);
            Assert.That(groupRepository.FindAll(), Has.Count.EqualTo(1));
            Assert.That(groupRepository.FindAll(), Does.Contain(firstGroup));
        }
    }
}
