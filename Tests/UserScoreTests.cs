using System;
using ISSLab.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class UserScoreTests
    {

        SellingUserScore userScoreEmptyConstructor;


        SellingUserScore userScoreSimpleConstructor;

        Guid userSimpleConstructor;
        Guid groupSimpleConstructor;


        SellingUserScore userScoreFullConstructor;

        Guid userFullConstructor;
        Guid groupFullConstructor;
        int scoreFullConstructor;

        [SetUp]
        public void Setup()
        {
            userScoreEmptyConstructor = new SellingUserScore();

            userSimpleConstructor = new Guid();
            groupSimpleConstructor = new Guid();

            userScoreSimpleConstructor = new SellingUserScore(userSimpleConstructor, groupSimpleConstructor);

            userFullConstructor = new Guid();
            groupFullConstructor = new Guid();
            scoreFullConstructor = 0;

            userScoreFullConstructor = new SellingUserScore(userFullConstructor, groupFullConstructor, scoreFullConstructor);
        }


        [Test]
        public void UserScoreGetUserId_GetUserIdFromFullConstructor_UserIdMatches()
        {
            Assert.That(userScoreFullConstructor.UserId, Is.EqualTo(userFullConstructor));
        }

        [Test]
        public void UserScoreGetGroupId_GetGroupIdFromFullConstructor_GroupIdMatches()
        {
            Assert.That(userScoreFullConstructor.GroupId, Is.EqualTo(groupFullConstructor));
        }

        [Test]
        public void UserScoreSetScore_SetAndGetScoreForFullConstructor_NewScoreMatches()
        {
            userScoreFullConstructor.Score = 1;
            Assert.That(userScoreFullConstructor.Score, Is.EqualTo(1));
        }

    }
}
