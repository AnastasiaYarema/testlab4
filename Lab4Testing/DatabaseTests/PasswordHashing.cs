using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using IIG.PasswordHashingUtils;
using IIG.DatabaseConnectionUtils;
using IIG.FileWorker;
using IIG.BinaryFlag;
using IIG.CoSFE.DatabaseUtils;


namespace Lab4Testing
{
    public class PasswordHashingTest
    {
        private const string Server = @"DESKTOP-LG8JL84\LAB4";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"lab4";
        private const int ConnectionTime = 75;
        AuthDatabaseUtils authDatabaseUtils = new AuthDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);

        private bool Clear()
        {
            return authDatabaseUtils.ExecSql("TRUNCATE TABLE dbo.Credentials");
        }

        [Fact]
        public void CheckNonExistingCredentials()
        {
            Assert.False(authDatabaseUtils.CheckCredentials("test", PasswordHasher.GetHash("p")));
        }

        [Fact]
        public void AddCredentials()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test1", PasswordHasher.GetHash("p1")));
            Assert.True(authDatabaseUtils.CheckCredentials("test1", PasswordHasher.GetHash("p1")));
            
            Clear();
        }

        [Fact]
        public void AddCredentialsEmptyPassword()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test11", PasswordHasher.GetHash("")));
            Assert.True(authDatabaseUtils.CheckCredentials("test11", PasswordHasher.GetHash("")));

            Clear();
        }

        [Fact]
        public void DoNotAddCredentialsEmpty()
        {
            Assert.False(authDatabaseUtils.AddCredentials("", PasswordHasher.GetHash("")));

            Clear();
        }

        [Fact]
        public void AddCredentialsLongLength()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test111111111111111111111111111111111111111111111111111", PasswordHasher.GetHash("p1111111111111111111111111111111111111111111111111111111111")));
            Assert.True(authDatabaseUtils.CheckCredentials("test1111111111111111111111111111111111111111111111111", PasswordHasher.GetHash("p1111111111111111111111111111111111111111111111111111111111")));

            Clear();
        }

        [Fact]
        public void UpdateCredentials()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test2", PasswordHasher.GetHash("p2")));
            Assert.True(authDatabaseUtils.UpdateCredentials("test2", PasswordHasher.GetHash("p2"), "test2", PasswordHasher.GetHash("pass2")));
            Assert.False(authDatabaseUtils.CheckCredentials("test2", PasswordHasher.GetHash("p2")));
            Assert.True(authDatabaseUtils.CheckCredentials("test2", PasswordHasher.GetHash("pass2")));

            Clear();
        }

        [Fact]
        public void UpdateCredentialsEmptyPassword()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test22", PasswordHasher.GetHash("p2")));
            Assert.True(authDatabaseUtils.UpdateCredentials("test22", PasswordHasher.GetHash("p2"), "test22", PasswordHasher.GetHash("")));
            Assert.False(authDatabaseUtils.CheckCredentials("test22", PasswordHasher.GetHash("p2")));
            Assert.True(authDatabaseUtils.CheckCredentials("test22", PasswordHasher.GetHash("")));

            Clear();
        }

        [Fact]
        public void UpdateAnotherCredentials()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test222", PasswordHasher.GetHash("p2")));
            Assert.True(authDatabaseUtils.UpdateCredentials("test222", PasswordHasher.GetHash("p2"), "testanother", PasswordHasher.GetHash("another")));
            Assert.False(authDatabaseUtils.CheckCredentials("test222", PasswordHasher.GetHash("p2")));
            Assert.True(authDatabaseUtils.CheckCredentials("testanother", PasswordHasher.GetHash("another")));

            Clear();
        }

        [Fact]
        public void UpdateCredentialsEmpty()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test222", PasswordHasher.GetHash("p2")));
            Assert.False(authDatabaseUtils.UpdateCredentials("test222", PasswordHasher.GetHash("p2"), "", PasswordHasher.GetHash("")));
            Assert.True(authDatabaseUtils.CheckCredentials("test222", PasswordHasher.GetHash("p2")));
            Assert.False(authDatabaseUtils.CheckCredentials("", PasswordHasher.GetHash("")));

            Clear();
        }

        [Fact]
        public void DeleteCredentials()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test3", PasswordHasher.GetHash("p3")));
            Assert.True(authDatabaseUtils.DeleteCredentials("test3", PasswordHasher.GetHash("p3")));

            Clear();
        }

        [Fact]
        public void DeleteNonExistingCredentials()
        {
            Assert.True(authDatabaseUtils.DeleteCredentials("test33", PasswordHasher.GetHash("p33")));

            Clear();
        }

        [Fact]
        public void NoAddingExistingCredentials()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test4", PasswordHasher.GetHash("p4")));
            Assert.False(authDatabaseUtils.AddCredentials("test4", PasswordHasher.GetHash("p5")));
            Assert.True(authDatabaseUtils.CheckCredentials("test4", PasswordHasher.GetHash("p4")));

            Clear();
        }

        [Fact]
        public void AddVeryLongHashCredentials()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test4", PasswordHasher.GetHash("kjasfbsjanfkjasnfkjasnfkjnfkjdsanfkjasnkjfdnaksjfnkasnfkjdasnfkjasnkjfdnkjfnaskjfnakjsnfkjdanfkjsanfkjasnfkjsandfkjansdfkasnjnasdkjfnkasjdgnajknfkasdnfkjasnkandfkjasnfkjankjsdfnkjsdnjaklvn akljsvnalksjnklasdnkadjlnaksjfndklsjfnajknflaks")));
            Assert.True(authDatabaseUtils.CheckCredentials("test4", PasswordHasher.GetHash("kjasfbsjanfkjasnfkjasnfkjnfkjdsanfkjasnkjfdnaksjfnkasnfkjdasnfkjasnkjfdnkjfnaskjfnakjsnfkjdanfkjsanfkjasnfkjsandfkjansdfkasnjnasdkjfnkasjdgnajknfkasdnfkjasnkandfkjasnfkjankjsdfnkjsdnjaklvn akljsvnalksjnklasdnkadjlnaksjfndklsjfnajknflaks")));

            Clear();
        }

        [Fact]
        public void AddVeryLongLoginCredentials()
        {
            Assert.True(authDatabaseUtils.AddCredentials("kjasfbsjanfkjasnfkjasnfkjnfkjdsanfkjasnkjfdnaksjfnkasnfkjdasnfkjasnkjfdnkjfnaskjfnakjsnfkjdanfkjsanfkjasnfkjsandfkjansdfkasnjnasdkjfnkasjdgnajknfkasdnfkjasnkandfkjasnfkjankjsdfnkjsdnjaklvn akljsvnalksjnklasdnkadjlnaksjfndklsjfnajknflaks", PasswordHasher.GetHash("test5")));
            Assert.True(authDatabaseUtils.CheckCredentials("kjasfbsjanfkjasnfkjasnfkjnfkjdsanfkjasnkjfdnaksjfnkasnfkjdasnfkjasnkjfdnkjfnaskjfnakjsnfkjdanfkjsanfkjasnfkjsandfkjansdfkasnjnasdkjfnkasjdgnajknfkasdnfkjasnkandfkjasnfkjankjsdfnkjsdnjaklvn akljsvnalksjnklasdnkadjlnaksjfndklsjfnajknflaks", PasswordHasher.GetHash("test5")));

            Clear();
        }

        [Fact]
        public void AddCredentialsWithNullHash()
        {
            Assert.Throws<ArgumentNullException>(() => authDatabaseUtils.AddCredentials("test44", PasswordHasher.GetHash(null)));
        }


        [Fact]
        public void AddCredentialsWithNullLogin()
        {
            Assert.False(authDatabaseUtils.AddCredentials(null, PasswordHasher.GetHash("test")));
        }

        [Fact]
        public void NotAddCredentialsEmptyLogin()
        {
            Assert.False(authDatabaseUtils.AddCredentials("", PasswordHasher.GetHash("test44")));
        }

        [Fact]
        public void AddCredentialsUtf8Chars()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test11", PasswordHasher.GetHash("プログラミングが大好き")));
            Assert.True(authDatabaseUtils.CheckCredentials("test11", PasswordHasher.GetHash("プログラミングが大好き")));

            Clear();
        }

        [Fact]
        public void AddCredentialsUtfCodes()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test12", PasswordHasher.GetHash("\u00C4 \uD802\u0033 \u00AE \u0306 \u01FD \u03B2 \uD8FF \uDCFF")));
            Assert.True(authDatabaseUtils.CheckCredentials("test12", PasswordHasher.GetHash("\u00C4 \uD802\u0033 \u00AE \u0306 \u01FD \u03B2 \uD8FF \uDCFF")));
        }

        [Fact]
        public void AddCredentialsUtf32()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test444", PasswordHasher.GetHash("⛢👩🏽‍🚒🌹🐂🖿🌀🏯")));
            Assert.True(authDatabaseUtils.CheckCredentials("test444", PasswordHasher.GetHash("⛢👩🏽‍🚒🌹🐂🖿🌀🏯")));

            Clear();
        }

        [Fact]
        public void AddCredentialsNotPasswordHasher()
        {
            Assert.True(authDatabaseUtils.AddCredentials("test111", "password"));
            Assert.True(authDatabaseUtils.CheckCredentials("test111", "password"));

            Clear();
        }
    }
}
