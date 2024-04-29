using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSLab.Model;

namespace Tests.Model
{
    internal class GroupTest
    {
        private Group groupEmpty;
        private Group groupWithId;
        private Group groupWithoutId;

        private Guid groupId;
        private string name;
        private int memberCount;
        private List<Guid> members;
        private List<Guid> posts;
        private List<Guid> topSellers;
        private List<Guid> admins;
        private List<Guid>? sellingUsers;
        private List<Guid> usersRequestingToSell;

        private string description;
        private string type;
        private string bannerPath;
        private DateTime creationDate;

        [SetUp]
        public void SetUp()
        {
            groupId = Guid.NewGuid();
            name = "name";
            memberCount = 10;
            members = new List<Guid>();
            posts = new List<Guid>();
            topSellers = new List<Guid>();
            admins = new List<Guid>();
            usersRequestingToSell = new List<Guid>();
            description = "description";
            type = "type";
            bannerPath = "path";
            creationDate = DateTime.Now;

            groupEmpty = new Group();
            groupWithoutId = new Group(name, description, type, bannerPath);
            groupWithId = new Group(groupId, name, memberCount, members, posts, admins, sellingUsers, description, type, bannerPath, creationDate, topSellers, usersRequestingToSell);
        }

        [Test]
        public void GroupId_GetGroupIdFromGroupEmpty_ShouldNotBeEmpty()
        {
            Assert.That(groupEmpty.GroupId, Is.Not.EqualTo(Guid.Empty));
        }
        [Test]
        public void GroupId_GetGroupIdFromGroupWithoutId_ShouldNotBeEmpty()
        {
            Assert.That(groupWithoutId.GroupId, Is.Not.EqualTo(Guid.Empty));
        }
        [Test]
        public void GroupId_GetGroupIdFromGroupWithId_ShouldBeEqualToGroupId()
        {
            Assert.That(groupWithId.GroupId, Is.EqualTo(groupId));
        }

        [Test]
        public void Name_GetNameFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.That(groupEmpty.Name, Is.Empty);
        }
        [Test]
        public void Name_GetNameFromGroupWithoutId_ShouldBeEqualToName()
        {
            Assert.That(groupWithoutId.Name, Is.EqualTo(name));
        }
        [Test]
        public void Name_GetNameFromGroupWithId_ShouldBeEqualToName()
        {
            Assert.That(groupWithId.Name, Is.EqualTo(name));
        }

        [Test]
        public void Name_SetNameForGroupEmpty_ShouldBeNewGroupName()
        {
            string newGroupName = "new";
            groupEmpty.Name = newGroupName;
            Assert.That(groupEmpty.Name, Is.EqualTo(newGroupName));
        }
        [Test]
        public void Name_SetNameForGroupWithoutId_ShouldBeNewGroupName()
        {
            string newGroupName = "new";
            groupWithoutId.Name = newGroupName;
            Assert.That(groupWithoutId.Name, Is.EqualTo(newGroupName));
        }
        [Test]
        public void Name_SetNameForGroupWithId_ShouldBeNewGroupName()
        {
            string newGroupName = "new";
            groupWithId.Name = newGroupName;
            Assert.That(groupWithId.Name, Is.EqualTo(newGroupName));
        }

        [Test]
        public void MemberCount_GetFromGroupEmpty_ShouldBeZero()
        {
            Assert.That(groupEmpty.MemberCount, Is.Zero);
        }
        [Test]
        public void MemberCount_GetFromGroupWithoutIde_ShouldBeZero()
        {
            Assert.That(groupWithoutId.MemberCount, Is.Zero);
        }
        [Test]
        public void MemberCount_GetFromGroupWithId_ShouldBeEqualToMemberCount()
        {
            Assert.That(memberCount, Is.EqualTo(groupWithId.MemberCount));
        }

        [Test]
        public void Members_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.That(groupEmpty.Members, Is.Empty);
        }
        [Test]
        public void Members_GetFromGroupWithoutId_ShouldBeEmpty()
        {
            Assert.That(groupWithoutId.Members, Is.Empty);
        }
        [Test]
        public void Members_GetFromGroupWithId_ShouldBeEqualToMembers()
        {
            Assert.That(groupEmpty.Members, Is.EqualTo(members));
        }

        [Test]
        public void Posts_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.That(groupEmpty.Posts, Is.Empty);
        }
        [Test]
        public void Posts_GetFromGroupWithoutId_ShouldBeEmpty()
        {
            Assert.That(groupWithoutId.Posts, Is.Empty);
        }
        [Test]
        public void Posts_GetFromGroupWithId_ShouldBeEqualToPosts()
        {
            Assert.That(groupWithId.Posts, Is.EqualTo(posts));
        }

        [Test]
        public void Admins_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.That(groupEmpty.Admins, Is.Empty);
        }
        [Test]
        public void Admins_GetFromGroupWithoutId_ShouldBeEmpty()
        {
            Assert.That(groupWithoutId.Admins, Is.Empty);
        }
        [Test]
        public void Admins_GetFromGroupWithId_ShouldBeEqualToAdmins()
        {
            Assert.That(groupWithId.Admins, Is.EqualTo(admins));
        }

        [Test]
        public void SellingUsers_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.That(groupEmpty.SellingUsers, Is.Empty);
        }
        [Test]
        public void SellingUsers_GetFromGroupWithoutId_ShouldBeEmpty()
        {
            Assert.That(groupWithoutId.SellingUsers, Is.Empty);
        }
        [Test]
        public void SellingUsers_GetFromGroupWithId_ShouldBeEqualToSellingUsers()
        {
            Assert.That(groupWithId.SellingUsers, Is.EqualTo(sellingUsers));
        }

        [Test]
        public void UsersRequestingToSell_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.That(groupEmpty.UsersRequestingToSell, Is.Empty);
        }
        [Test]
        public void UsersRequestingToSell_GetFromGroupWithoutId_ShouldBeEmpty()
        {
            Assert.That(groupWithoutId.UsersRequestingToSell, Is.Empty);
        }
        [Test]
        public void UsersRequestingToSell_GetFromGroupWithId_ShouldBeEqualToUsersRequestingToSell()
        {
            Assert.That(groupWithId.UsersRequestingToSell, Is.EqualTo(usersRequestingToSell));
        }

        [Test]
        public void Description_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.IsEmpty(groupEmpty.Description);
        }
        [Test]
        public void Description_GetFromGroupWithoutId_ShouldBeEqualToDescription()
        {
            Assert.That(groupWithoutId.Description, Is.EqualTo(description));
        }
        [Test]
        public void Description_GetFromGroupWithId_ShouldBeEqualToDescription()
        {
            Assert.That(groupWithId.Description, Is.EqualTo(description));
        }

        [Test]
        public void Description_SetForGroupEmpty_ShouldBeNewGroupDescription()
        {
            string newGroupDescription = "new";
            groupEmpty.Description = newGroupDescription;
            Assert.That(groupEmpty.Description, Is.EqualTo(newGroupDescription));
        }
        [Test]
        public void Description_SetForGroupWithoutId_ShouldBeNewGroupDescription()
        {
            string newGroupDescription = "new";
            groupWithoutId.Description = newGroupDescription;
            Assert.That(groupWithoutId.Description, Is.EqualTo(newGroupDescription));
        }
        [Test]
        public void Description_SetForGroupWithId_ShouldBeNewGroupDescription()
        {
            string newGroupDescription = "new";
            groupWithId.Description = newGroupDescription;
            Assert.That(groupWithId.Description, Is.EqualTo(newGroupDescription));
        }

        [Test]
        public void Type_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.IsEmpty(groupEmpty.Type);
        }
        [Test]
        public void Type_GetFromGroupWithoutId_ShouldBeEqualToType()
        {
            Assert.That(groupWithoutId.Type, Is.EqualTo(type));
        }
        [Test]
        public void Type_GetFromGroupWithId_ShouldBeEqualToType()
        {
            Assert.That(groupWithId.Type, Is.EqualTo(type));
        }

        [Test]
        public void Type_SetForGroupEmpty_ShouldBeNewGroupType()
        {
            string newGroupType = "new";
            groupEmpty.Type = newGroupType;
            Assert.That(groupEmpty.Type, Is.EqualTo(newGroupType));
        }
        [Test]
        public void Type_SetForGroupWithoutId_ShouldBeNewGroupType()
        {
            string newGroupType = "new";
            groupWithoutId.Type = newGroupType;
            Assert.That(groupWithoutId.Type, Is.EqualTo(newGroupType));
        }
        [Test]
        public void Type_SetForGroupWithId_ShouldBeNewGroupType()
        {
            string newGroupType = "new";
            groupWithId.Type = newGroupType;
            Assert.That(groupWithId.Type, Is.EqualTo(newGroupType));
        }

        [Test]
        public void BannerPath_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.IsEmpty(groupEmpty.BannerPath);
        }
        [Test]
        public void BannerPath_GetFromGroupWithoutId_ShouldBeEqualToBannerPath()
        {
            Assert.That(groupWithoutId.BannerPath, Is.EqualTo(bannerPath));
        }
        [Test]
        public void BannerPath_GetFromGroupWithId_ShouldBeEqualToBannerPath()
        {
            Assert.That(groupWithId.BannerPath, Is.EqualTo(bannerPath));
        }

        [Test]
        public void BannerPath_SetForGroupEmpty_ShouldBeNewGroupBannerPath()
        {
            string newGroupBannerPath = "new";
            groupEmpty.BannerPath = newGroupBannerPath;
            Assert.That(groupEmpty.BannerPath, Is.EqualTo(newGroupBannerPath));
        }
        [Test]
        public void BannerPath_SetForGroupWithoutId_ShouldBeNewGroupBannerPath()
        {
            string newGroupBannerPath = "new";
            groupWithoutId.BannerPath = newGroupBannerPath;
            Assert.That(groupWithoutId.BannerPath, Is.EqualTo(newGroupBannerPath));
        }
        [Test]
        public void BannerPath_SetForGroupWithId_ShouldBeNewGroupBannerPath()
        {
            string newGroupBannerPath = "new";
            groupWithId.BannerPath = newGroupBannerPath;
            Assert.That(groupWithId.BannerPath, Is.EqualTo(newGroupBannerPath));
        }

        [Test]
        public void CreationDate_GetFromGroupEmpty_IsInstanceOfDateTime()
        {
            Assert.That(groupEmpty.CreationDate, Is.InstanceOf<DateTime>());
        }

        [Test]
        public void CreationDate_GetFromGroupWithoutId_IsInstanceOfDateTime()
        {
            Assert.That(groupWithoutId.CreationDate, Is.InstanceOf<DateTime>());
        }

        [Test]
        public void CreationDate_GetFromGroupWithId_ShouldBeEqualToCreationDate()
        {
            Assert.That(groupWithId.CreationDate, Is.EqualTo(creationDate));
        }

        [Test]
        public void UsersWithSellRequests_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.That(groupEmpty.UsersWithSellRequests, Is.Empty);
        }
        [Test]
        public void UsersWithSellRequests_GetFromGroupWithoutId_ShouldBeEmpty()
        {
            Assert.That(groupWithoutId.UsersWithSellRequests, Is.Empty);
        }
        [Test]
        public void UsersWithSellRequests_GetFromGroupWithId_ShouldBeEqualToUsersRequestingToSell()
        {
            Assert.That(groupWithId.UsersWithSellRequests, Is.EqualTo(sellingUsers));
        }

        [Test]
        public void UsersWithSellRequests_SetToNewList_ShouldBeNewList()
        {
            List<Guid> guids = new List<Guid>();
            guids.Add(groupId);
            groupEmpty.UsersWithSellRequests = guids;
            Assert.That(groupEmpty.UsersWithSellRequests, Is.EqualTo(guids));
        }

        [Test]
        public void TopSellers_GetFromGroupEmpty_ShouldBeEmpty()
        {
            Assert.That(groupEmpty.TopSellers, Is.Empty);
        }
        [Test]
        public void TopSellers_GetFromGroupWithoutId_ShouldBeEmpty()
        {
            Assert.That(groupWithoutId.TopSellers, Is.Empty);
        }
        [Test]
        public void TopSellers_GetFromGroupWithId_ShouldBeEqualToTopSellers()
        {
            Assert.That(groupWithId.TopSellers, Is.EqualTo(topSellers));
        }
        [Test]
        public void TopSellers_SetTopSellers_TopSellersEqualToNewList()
        {
            List<Guid> guids = new List<Guid>();
            guids.Add(groupId);
            groupEmpty.TopSellers = guids;
            Assert.That(groupEmpty.TopSellers, Is.EqualTo(guids));
        }

        [Test]
        public void AddUserWithSellRequest_AddNotExistingUser_UserShouldBeAddedToUsersWithSellRequests()
        {
            Guid userId = Guid.NewGuid();
            groupEmpty.AddUserWithSellRequest(userId);
            Assert.Contains(userId, groupEmpty.UsersWithSellRequests);
        }

        [Test]
        public void AddUserWithSellRequest_AddNotExistingUser_UserShouldNotBeInSellers()
        {
            Guid userId = Guid.NewGuid();
            groupEmpty.AddUserWithSellRequest(userId);
            Assert.Contains(userId, groupEmpty.SellingUsers);
        }

        [Test]
        public void AddUserWithSellRequest_AddExistingUser_UserShouldNotBeAddedAgain()
        {
            Guid userId = Guid.NewGuid();
            groupEmpty.AddUserWithSellRequest(userId);
            groupEmpty.AddUserWithSellRequest(userId);

            Assert.That(groupEmpty.SellingUsers, Has.Count.EqualTo(2));
        }

        [Test]
        public void RemoveUserWithSellRequest_RemoveExisting_UserShouldBeRemoved()
        {
            Guid userId = Guid.NewGuid();
            groupEmpty.AddUserWithSellRequest(userId);
            groupEmpty.RemoveUserWithSellRequest(userId);
            Assert.IsFalse(groupEmpty.UsersWithSellRequests.Contains(userId));
        }

        [Test]
        public void RemoveUserWithSellRequest_RemoveNonExisting_ShouldNotThrowException()
        {
            Guid userId = Guid.NewGuid();

            Assert.DoesNotThrow(() => { groupEmpty.RemoveUserWithSellRequest(userId); });
        }
        [Test]
        public void AddMember_UserNotMember_MembersShouldContainUser()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            Assert.Contains(user, groupEmpty.Members);
        }

        [Test]
        public void AddMember_UserNotMember_MemberCountShouldBe1()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            Assert.AreEqual(groupEmpty.MemberCount, 1);
        }
        [Test]
        public void AddMember_UserMember_ThrowException()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.AddMember(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is already a member of this group"));
        }
        [Test]
        public void RemoveMember_UserIsMember_MembersDontContainUserAnymore()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.RemoveMember(user);
            Assert.False(groupEmpty.Members.Contains(user));
        }
        [Test]
        public void RemoveMember_UserIsMember_MembersCountDecreases()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.RemoveMember(user);
            Assert.Zero(groupEmpty.MemberCount);
        }
        [Test]
        public void RemoveMember_UserNotMember_ThrowException()
        {
            Guid user = Guid.NewGuid();
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.RemoveMember(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not a member of this group"));
        }
        [Test]
        public void AddPost_AnyPost_PostIsAdded()
        {
            Guid post = Guid.NewGuid();
            groupEmpty.AddPost(post);
            Assert.That(groupEmpty.Posts, Has.Count.EqualTo(1));
        }
        [Test]
        public void RemovePost_PostExists_PostIsRemoved()
        {
            Guid post = Guid.NewGuid();
            groupEmpty.AddPost(post);
            groupEmpty.RemovePost(post);
            Assert.That(groupEmpty.Posts, Is.Empty);
        }
        [Test]
        public void RemovePost_PostDoesntExist_DoesNotThrowException()
        {
            Guid post = Guid.NewGuid();
            Assert.DoesNotThrow(() => { groupEmpty.RemovePost(post); });
        }
        [Test]
        public void AddAdmin_UserIsNotMember_ThrowException()
        {
            Guid user = Guid.NewGuid();
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.AddAdmin(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not a member of this group"));
        }
        [Test]
        public void AddAdmin_UserMemberAndAdmin_ThrowException()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.AddAdmin(user);
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.AddAdmin(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is already an admin of this group"));
        }
        [Test]
        public void AddAdmin_UserMemberNotAdmin_UserIsAddedAsAdmin()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.AddAdmin(user);
            Assert.That(groupEmpty.Admins, Has.Count.EqualTo(1));
        }

        [Test]
        public void RemoveAdmin_UserIsNotMember_ThrowException()
        {
            Guid user = Guid.NewGuid();
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.RemoveAdmin(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not a member of this group"));
        }
        [Test]
        public void RemoveAdmin_UserIsMemberNotAdmin_ThrowException()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.RemoveAdmin(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not an admin of this group"));
        }
        [Test]
        public void RemoveAdmin_UserIsAdmin_UserIsRemoved()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.AddAdmin(user);
            groupEmpty.RemoveAdmin(user);
            Assert.That(groupEmpty.Admins, Has.Count.EqualTo(0));
        }

        [Test]
        public void AddSellingUser_UserIsNotMember_ThrowException()
        {
            Guid user = Guid.NewGuid();
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.AddSellingUser(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not a member of this group"));
        }
        [Test]
        public void AddSellingUser_UserMemberAndSellingUser_ThrowException()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.AddSellingUser(user);
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.AddSellingUser(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is already a selling user of this group"));
        }
        [Test]
        public void AddSellingUser_UserMemberNotSellingUser_UserIsAddedAsSellingUser()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.AddSellingUser(user);
            Assert.That(groupEmpty.SellingUsers, Has.Count.EqualTo(1));
        }
        [Test]
        public void RemoveSellingUser_UserIsNotMember_ThrowException()
        {
            Guid user = Guid.NewGuid();
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.RemoveSellingUser(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not a member of this group"));
        }
        [Test]
        public void RemoveSellingUser_UserIsMemberNotSellingUser_ThrowException()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.RemoveSellingUser(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not a selling user of this group"));
        }
        [Test]
        public void RemoveSellingUser_UserIsSellingUser_UserIsRemoved()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.AddSellingUser(user);
            groupEmpty.RemoveSellingUser(user);
            Assert.That(groupEmpty.SellingUsers, Has.Count.EqualTo(0));
        }

        [Test]
        public void AddRequestingToSellUser_UserIsNotMember_ThrowException()
        {
            Guid user = Guid.NewGuid();
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.AddRequestingToSellUser(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not a member of this group"));
        }
        [Test]
        public void AddRequestingToSellUser_UserMemberAndRequestingUser_ThrowException()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.AddRequestingToSellUser(user);
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.AddRequestingToSellUser(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is already a selling user of this group"));
        }
        [Test]
        public void AddRequestingToSellUser_UserMemberNotRequestingUser_UserIsAddedAsRequestingUser()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.AddRequestingToSellUser(user);
            Assert.That(groupEmpty.SellingUsers, Has.Count.EqualTo(1));
        }
        [Test]
        public void RemoveRequestingToSellUser_UserIsNotMember_ThrowException()
        {
            Guid user = Guid.NewGuid();
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.RemoveRequestingToSellUser(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not a member of this group"));
        }
        [Test]
        public void RemoveRequestingToSellUser_UserIsMemberNotSellingUser_ThrowException()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            var exceptionMessage = Assert.Throws<Exception>(() => { groupEmpty.RemoveRequestingToSellUser(user); });
            Assert.That(exceptionMessage.Message, Is.EqualTo("User is not a selling user of this group"));
        }
        [Test]
        public void RemoveRequestingToSellUser_UserIsRequestingUser_UserIsRemoved()
        {
            Guid user = Guid.NewGuid();
            groupEmpty.AddMember(user);
            groupEmpty.AddRequestingToSellUser(user);
            groupEmpty.RemoveRequestingToSellUser(user);
            Assert.That(groupEmpty.SellingUsers, Has.Count.EqualTo(0));
        }

        [Test]
        public void AddTopSeller_AnyTopSeller_TopSellerIsAdded()
        {
            Guid seller = Guid.NewGuid();
            groupEmpty.AddTopSeller(seller);
            Assert.That(groupEmpty.TopSellers, Has.Count.EqualTo(1));
        }
        [Test]
        public void RemoveTopSeller_TopSellerExists_TopSellerIsRemoved()
        {
            Guid seller = Guid.NewGuid();
            groupEmpty.AddTopSeller(seller);
            groupEmpty.RemoveTopSeller(seller);
            Assert.That(groupEmpty.TopSellers, Is.Empty);
        }
        [Test]
        public void RemoveTopSeller_TopSellerDoesntExist_DoesNotThrowException()
        {
            Guid seller = Guid.NewGuid();
            Assert.DoesNotThrow(() => { groupEmpty.RemoveTopSeller(seller); });
        }
    }
}
