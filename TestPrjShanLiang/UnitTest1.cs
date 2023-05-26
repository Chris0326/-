using Azure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using prjShanLiang.Controllers;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System.Runtime.CompilerServices;
using System.Text;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Data;
using AspNetCore;

namespace TestPrjShanLiang
{
    [TestFixture]
    public class TestsStoreController
    {
        StoreController _sc;

        public TestsStoreController()
        {
            ShanLiang21Context db = new();
            _sc = new(db);
        }
        [Test]
        public void TestShowList()
        {
            var result = _sc.List();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestShowRestaurant()
        {
            int storeID = 1;
            IActionResult result = _sc.Restaurant(storeID);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetRestaurantType()
        {
            IActionResult result = _sc.GetRestaurantType(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestSearchRestaurantType()
        {
            IActionResult result = _sc.SearchRestaurantType(1);
            Assert.IsNotNull(result);
        }

        //[Test]
        //public void TestShowFavorate()
        //{

        //}
        //[Test]
        //public void TestShowFavorate()
        //{
        //    IActionResult result = _sc.ShowFavorate(1);
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void TestAddComment()
        //{
        //    StoreEvaluate se = new();
        //    IActionResult result = _sc.AddComment(se);
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void TestReserveInt()
        //{
        //    IActionResult result = _sc.Reserve(1);
        //    Assert.IsNotNull(result);
        //}
        //[Test]
        //public void TestReservePost()
        //{
        //    StoreEvaluate se = new();
        //    IActionResult result = _sc.Reserve(se);
        //    Assert.IsNotNull(result);
        //}

        [Test]
        public void TestGetName()
        {
            string keyword = "IJIDJh7e674841IOIG";
            IActionResult result = _sc.GetName(keyword);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetType()
        {
            IActionResult result = _sc.GetType();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestSearchStore()
        {
            IActionResult result0 = _sc.SearchStore(null, null, null, null);
            Assert.IsNotNull(result0);

            string keywoed = "��³";
            string types = "4";
            string districts = "[\"5\"]";
            int rating = 3;
            int order = 0;
            IActionResult result1 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result1);

            keywoed = "��³";
            types = "[\"4\"]";
            districts = "5";
            IActionResult result2 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result2);

            keywoed = "��³";
            types = "[\"4\"]";
            districts = "[\"5\"]";
            rating = 5;
            order = 1;
            IActionResult result2_2 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result2_2);

            order = 2;
            IActionResult result3 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result3);

            order = 3;
            IActionResult result4 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result4);

            order = 4;
            IActionResult result5 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result5);
        }

        [Test]
        public void TestGetRACAD()
        {
            object result = _sc.GetRACAD();
            Assert.IsNotNull(result);
        }
    }
    [TestFixture]
    public class TestsUserController
    {
        Mock<HttpContext>? _mockHttpContext;
        Mock<ISession>? _mockSession;
        Mock<IWebHostEnvironment>? _mockEnvironment;
        UserController _uc;

        public TestsUserController()
        {
            _mockHttpContext = new();
            _mockSession = new();
            _mockEnvironment = new();

            UserController uc = new(_mockEnvironment.Object);
            _uc = uc;

            _mockHttpContext.SetupGet(x => x.Session).Returns(_mockSession.Object);

            _uc.ControllerContext = new()
            {
                HttpContext = _mockHttpContext.Object
            };
        }

        [Test]
        public void TestLogin()
        {
            IActionResult result0 = _uc.Login();
            Assert.IsNotNull(result0);

            CAccountPasswordViewModel vm = new()
            {
                AccountName = "chris@gmail.com",
                AccountPassword = "aaaBBB1234567A",
            };
            IActionResult result1 = _uc.Login(vm);
            Assert.IsNotNull(result1);

            vm.AccountName = "ShanLiangBentou";
            vm.AccountPassword = "1234";
            IActionResult result2 = _uc.Login(vm);
            Assert.IsNotNull(result2);

            vm.AccountName = "Mark";
            vm.AccountPassword = "1234";
            IActionResult result3 = _uc.Login(vm);
            Assert.IsNotNull(result3);
        }
        [Test]
        public void TestlogOut()
        {
            IActionResult result1 = _uc.logOut();
            Assert.IsNotNull(result1);
        }
        [Test]
        public void TestMemberManager()
        {
            string? account = null;
            IActionResult result1 = _uc.MemberManager(account);
            Assert.IsNotNull(result1);

            account = "NotFindMember@mail.com";
            IActionResult result2 = _uc.MemberManager(account);
            Assert.IsNotNull(result2);

            account = "Adam";
            IActionResult result3 = _uc.MemberManager(account);
            Assert.IsNotNull(result3);
        }
        [Test]
        public void TestStoreManager()
        {
            string? account = null;
            IActionResult result1 = _uc.StoreManager(account);
            Assert.IsNotNull(result1);

            account = "NotFindStore";
            IActionResult result2 = _uc.StoreManager(account);
            Assert.IsNotNull(result2);

            account = "ShanLiangBentou";
            IActionResult result3 = _uc.StoreManager(account);
            Assert.IsNotNull(result3);
        }
        [Test]
        public void TestMypage()
        {
            IActionResult result1 = _uc.Mypage();
            Assert.IsNotNull(result1);
        }
        [Test]
        public void TestEdit()
        {
            string? account = null;
            IActionResult result1 = _uc.Edit(account);
            Assert.IsNotNull(result1);

            account = "admin";
            IActionResult result2 = _uc.Edit(account);
            Assert.IsNotNull(result2);
        }
        [Test]
        public void TestSignup()
        {
            ShanLiang21Context db = new();

            //�ˬd���U�����O�_���`����
            IActionResult result1 = _uc.Signup();
            Assert.IsNotNull(result1);

            //�s�W�@�����ո��
            CCreateMemberAccountViewModel vm = new()
            {
                AccountName = "TestMember",
                Memberphone = "TestPhone",
                MemberName = "TestName",
                Email = "Test@email.com",
                BrithDate = DateTime.Now,
                Address = "���ե��椸�Ϸ|����",
                AccountPassword = "TestPassword",
                AccountPassword2= "TestPassword"
            };
            //�N��Ʊa�JSignup��k���A�إߤ@�����ո��
            IActionResult result2 = _uc.Signup(vm);
            Assert.IsNotNull(result2);
            //�ˬd�O�_�إߦ��\
            bool result3 = db.Members.Where(x => x.AccountName == "TestMember" && x.Address == "���ե��椸�Ϸ|����").Any();
            Assert.IsTrue(result3);

            //�R�����ո��
            db.Members.Remove(db.Members.Where(x => x.AccountName == "TestMember" && x.Address == "���ե��椸�Ϸ|����").First());
            db.SaveChanges();
            //�ˬd�O�_�w�R��
            bool result4 = db.Members.Where(x => x.AccountName == "TestMember" && x.Address == "���ե��椸�Ϸ|����").Any();
            Assert.IsFalse(result4);
        }
        [Test]
        public void TestSignupStore()
        {
            ShanLiang21Context db = new();

            IActionResult result1 = _uc.SignupStore();
            Assert.IsNotNull(result1);

            ViewResult? result2 = _uc.SignupStore() as ViewResult;
            Assert.IsNotNull(result2);

            Assert.IsNotNull(result2.ViewData["chosenCity"]);
            string chosenCity = result2.ViewData["chosenCity"].ToString();
            Assert.AreEqual("", chosenCity);

            Assert.IsNotNull(result2.ViewData["chosenDistrict"]);
            string chosenDistrict = result2.ViewData["chosenDistrict"].ToString();
            Assert.AreEqual("", chosenDistrict);

            CCreateStoreAccountViewModel vm = new()
            {
                AccountName = "TestStore",
                TaxID = "00000000",
                RestaurantName = "TestStoreName",
                RestaurantAddress = "���ե��椸�ϩ��a��",
                RestaurantPhone = "(02)0000-0000",
                Website = "www.TestStore.com",
                Seats = 20,
                StoreMail = "TsetStore@mail",
                AccountPassword = "TestPassword",
                storeDistrict = "�j�w��",
            };
            List<IFormFile> files = new();
            Store vm2 = new()
            {
                OpeningTime = TimeSpan.FromHours(0),
                ClosingTime = TimeSpan.FromHours(10),
            };
            IActionResult result3 = _uc.SignupStore(vm, files, vm2);
            Assert.IsNotNull(result3);
            //�ˬd�O�_�إߦ��\
            bool result4 = db.Stores.Where(x => x.AccountName == "TestStore" && x.RestaurantAddress == "���ե��椸�ϩ��a��").Any();
            Assert.IsTrue(result4);

            //�R�����ո��
            db.Stores.Remove(db.Stores.Where(x => x.AccountName == "TestStore" && x.RestaurantAddress == "���ե��椸�ϩ��a��").First());
            db.SaveChanges();
            //�ˬd�O�_�w�R��
            bool result5 = db.Stores.Where(x => x.AccountName == "TestStore" && x.RestaurantAddress == "���ե��椸�ϩ��a��").Any();
            Assert.IsFalse(result5);
        }
        [Test]
        public void TestMemberManagement()
        {
            //IActionResult result = _uc.memberManagement();
            //Assert.IsNotNull(result);
        }
        [Test]
        public void TestStoreManagement()
        {
            //IActionResult result = _uc.storeManagement();
            //Assert.IsNotNull(result);
        }
        [Test]
        public void TestMemberDataRevision()
        {
            ShanLiang21Context db = new();

            string email = "";
            IActionResult result1 = _uc.memberDataRevision(email);
            Assert.IsNotNull(result1);

            Member testData = new()
            {
                Email = "Test@mail",
                Password = "password",
                CustomerLevel = 0
            };
            db.Members.Add(testData);
            db.SaveChanges();

            email = "Test@mail";
            bool result2 = db.Members.Where(x => x.Email == email).Any();
            Assert.IsTrue(result2);

            IActionResult result3 = _uc.memberDataRevision(email);
            Assert.IsNotNull(result3);
            ViewResult result3_2 = result3 as ViewResult;
            Assert.IsNotNull(result3_2.ViewData["CustomerLevel"]);
            string CustomerLevel = result3_2.ViewData["CustomerLevel"].ToString();
            Assert.That(CustomerLevel, Is.EqualTo("�@��|��"));

            testData.CustomerLevel = 1;
            db.Members.Update(testData);
            db.SaveChanges();
            IActionResult result4 = _uc.memberDataRevision(email);
            Assert.IsNotNull(result4);
            ViewResult result4_2 = result3 as ViewResult;
            Assert.IsNotNull(result4_2.ViewData["CustomerLevel"]);
            CustomerLevel = result4_2.ViewData["CustomerLevel"].ToString();
            Assert.That(CustomerLevel, Is.EqualTo("�ժ��|��"));

            testData.CustomerLevel = 2;
            db.Members.Update(testData);
            db.SaveChanges();
            IActionResult result5 = _uc.memberDataRevision(email);
            Assert.IsNotNull(result5);
            ViewResult result5_2 = result3 as ViewResult;
            Assert.IsNotNull(result5_2.ViewData["CustomerLevel"]);
            CustomerLevel = result5_2.ViewData["CustomerLevel"].ToString();
            Assert.That(CustomerLevel, Is.EqualTo("�p�۷|��"));

            db.Members.Remove(testData);
            db.SaveChanges();
        }
        [Test]
        public void TestStoreDataRevision()
        {
            string account = "";
            IActionResult result1 = _uc.storeDataRevision(account);
            Assert.IsNotNull(result1);

            account = "ShanLiangBentou";
            IActionResult result2 = _uc.storeDataRevision(account);
            Assert.IsNotNull(result2);
        }
        [Test]
        public void TestDelete()
        {
            string account = "";
            IActionResult result = _uc.Delete(account);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestMemberDataRevision2()
        {
            ShanLiang21Context db = new();
            //�s�W�@�����ո��
            Member testData = new()
            {
                AccountName = "TestMember",
                Memberphone = "TestPhone",
                MemberName = "TestName",
                Email = "Test@email.com",
                BrithDate = DateTime.Now,
                Address = "���ե��椸�Ϸ|����",
                Password = "TestPassword",
            };
            db.Members.Add(testData);
            db.SaveChanges();
            string Address = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Address).First();
            //�T�{��L��ƬO�_�����e��
            Assert.That(Address, Is.EqualTo("���ե��椸�Ϸ|����"));

            //�s�W�@���Ψӧ�諸���
            CMemberWrap vm = new()
            {
                Email = "Test@email.com",
                Memberphone = "0988888888",
                MemberName = "EditName",
                Address = "���ե��椸�Ͻs�訽"
            };
            //���դ��]�t���K�X
            IActionResult result1 = _uc.memberDataRevision2(vm);
            Assert.IsNotNull(result1);
            Address = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Address).First();
            //�T�{��L��ƬO�_�w�g���
            Assert.That(Address, Is.EqualTo("���ե��椸�Ͻs�訽"));
            //�T�{�K�X�O�_�����e��
            string Password = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Password).First();
            Assert.That(Password, Is.EqualTo("TestPassword"));

            //���ե]�t���K�X
            vm.Password = "EditPassword";
            IActionResult result2 = _uc.memberDataRevision2(vm);
            Assert.IsNotNull(result2);
            Password = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Password).First();
            //�T�{�K�X�O�_�O�w�g���
            Assert.That(Password, Is.EqualTo("EditPassword"));

            //�R�����ո��
            db.Members.Remove(db.Members.Where(x => x.Email == "Test@email.com").First());
            db.SaveChanges();
        }   
        [Test]
        public void TestStoreDataRevision2()
        {
            //ShanLiang21Context db = new();
            //List<IFormFile> files = new();
            ////�s�W�@�����ո��
            //Store testData = new()
            //{
            //    AccountName = "TestStore",
            //    DistrictId = 1,
            //    TaxId = "00000000",
            //    RestaurantName = "TestStoreName",
            //    RestaurantAddress = "���ե��椸�ϩ��a��",
            //    RestaurantPhone = "(02)0000-0000",
            //    Seats = 20,
            //    OpeningTime = TimeSpan.FromHours(0),
            //    ClosingTime = TimeSpan.FromHours(10),
            //    Website = "www.TestStore.com",
            //    StoreMail = "TsetStore@mail",
            //    Password = "TestPassword"
            //};
            //db.Stores.Add(testData);
            //db.SaveChanges();
            //string Address = db.Stores.Where(x => x.AccountName == "TestStore").Select(x => x.RestaurantAddress).First();
            ////�T�{��L��ƬO�_�����e��
            //Assert.That(Address, Is.EqualTo("���ե��椸�ϩ��a��"));

            ////�s�W�@����諸���
            //Store editData = new()
            //{
            //    AccountName = "EditStore",
            //    TaxId = "88888888",
            //    RestaurantName = "EditStoreName",
            //    RestaurantAddress = "���ե��椸�Ͻs�訽",
            //    RestaurantPhone = "(02)8888-8888",
            //    Seats = 10,
            //    OpeningTime = TimeSpan.FromHours(1),
            //    ClosingTime = TimeSpan.FromHours(9),
            //    Website = "www.EditStore.com",
            //    StoreMail = "EditStore@mail"
            //};
            //IActionResult result1 = _uc.storeDataRevision2(testData, files);
            //Assert.IsNotNull(result1);
            //Address = db.Stores.Where(x => x.AccountName == "TestStore").Select(x => x.RestaurantAddress).First();
            ////�T�{��L��ƬO�_�w�g���
            //Assert.That(Address, Is.EqualTo("���ե��椸�Ͻs�訽"));
            //string Password = db.Stores.Where(x => x.AccountName == "TestStore").Select(x => x.Password).First();
            ////�T�{�K�X�O�_�����e��
            //Assert.That(Password, Is.EqualTo("TestPassword"));

            //editData.Password = "EditPassword";

            //IActionResult result2 = _uc.storeDataRevision2(testData, files);
            //Assert.IsNotNull(result2);
            ////�T�{�K�X�O�_���w�g���
            //Assert.That(Password, Is.EqualTo("EditPassword"));

            ////�R�����ո��
            //db.Stores.Remove(db.Stores.Where(x => x.AccountName == "TestStore").First());
            //db.SaveChanges();
        }
        [Test]
        public void TestGetCities()
        {
            IActionResult result = _uc.GetCities();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestGetDistricts()
        {
            string cityName = "�x�_��";
            IActionResult result = _uc.GetDistricts(cityName);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestCheckName()
        {
            string name = "NoFindMember";
            IActionResult result = _uc.CheckName(name);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestCheckStoreName()
        {
            string name = "NoFindStore";
            IActionResult result = _uc.CheckStoreName(name);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestCheckLoginAccount()
        {
            string name = "NoFindAll";
            IActionResult result = _uc.CheckLoginAccount(name);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestForgetPwd()
        {
            IActionResult result = _uc.forgetPwd();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestSendEmail()
        {
            ShanLiang21Context db = new();
            //�s�W�@�����ո��
            Member testData = new()
            {
                AccountName = "TestMember",
                Memberphone = "TestPhone",
                MemberName = "TestName",
                Email = "Test@email.com",
                BrithDate = DateTime.Now,
                Address = "���ե��椸�Ϸ|����",
                Password = "TestPassword",
            };
            db.Members.Add(testData);
            db.SaveChanges();

            IActionResult result1 = _uc.sendEmail("Test@email.com");
            Assert.IsNotNull(result1);
            //�T�{�K�X�O�_�w�g�Q���
            bool isEditPwd = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Password).Equals("TestPassword");
            Assert.IsFalse(isEditPwd);

            //�R�����ո��
            db.Members.Remove(db.Members.Where(x => x.Email == "Test@email.com").First());
            db.SaveChanges();
        }
    }
    [TestFixture]
    public class TestsUserAdminController
    {
        UserAdminController _uac;
        public TestsUserAdminController()
        {
            UserAdminController uac = new();
            _uac = uac;
        }
        [Test]
        public void TestIndex()
        {
            IActionResult result = _uac.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestList()
        {
            IActionResult result = _uac.List();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestEdit()
        {
            int? TestMemberID = null;
            IActionResult result1 = _uac.Edit(TestMemberID);
            Assert.IsNotNull(result1);
            ShanLiang21Context db = new();
            Member mem = new()
            {
                Email = "TestMember@mail.com",
                Password = "0000"
            };
            db.Add(mem);
            db.SaveChanges();
            TestMemberID = db.Members.Where(x => x.Email == "TestMember@mail.com").Select(x => x.MemberId).First();
            IActionResult result2 = _uac.Edit(TestMemberID);
            Assert.IsNotNull(result2);
            Member memEdit = new()
            {
                MemberId = (int)TestMemberID,
                Email = "TestEdit@mail.com",
                MemberName = "Member",
                Memberphone = "0900000000",
                BrithDate = DateTime.Now,
                Address = "���ե��椸�Ͻs�訽",
                AccountStatus = 1
            };
            IActionResult result3 = _uac.Edit(memEdit);
            Assert.IsNotNull(result3);
            Member d = db.Members.FirstOrDefault(x => x.MemberId == TestMemberID);
            db.Members.Remove(d);
            db.SaveChanges();
        }
        [Test]
        public void TestCheckStatus()
        {
            ShanLiang21Context db = new();
            int L = db.AccountStatuses.Count();
            for(int statusID = 0; statusID < L + 1; statusID++)
            {
                IActionResult result = _uac.CheckStatus(1);
                Assert.IsNotNull(result);
            }
        }
    }
    [TestFixture]
    public class TestsStoreAdminController
    {
        StoreAdminController _sc;
        public TestsStoreAdminController()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            var mockEmailSender = new Mock<IEmailSender>();
            StoreAdminController sc = new(mockEnvironment.Object,mockEmailSender.Object);
            _sc = sc;
        }
        [Test]
        public void TestList()
        {
            IActionResult result = _sc.List();
            Assert.IsNotNull(result);
        }
        [Test]
        public void Test2() { }
        [Test]
        public void Test3() { }
        [Test]
        public void Test4() { }
        [Test]
        public void Test5() { }
        [Test]
        public void Test6() { }
        [Test]
        public void Test7() { }
        [Test]
        public void Test8() { }
    }
    [TestFixture]
    public class TestsShoppingController
    {
        ShoppingController _sc;
        public TestsShoppingController()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            ShanLiang21Context db = new();
            ShoppingController sc = new(db,mockEnvironment.Object);
            _sc = sc;
        }
        [Test]
        public void TestMenu()
        {
            IActionResult result = _sc.Menu(1);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestMyMealOrderDetail()
        {
            IActionResult result = _sc.MyMealOrderDetail(33);
            Assert.IsNotNull(result);
        }
    }
    [TestFixture]
    public class TestsHomeController
    {
        HomeController _hc;
        public TestsHomeController()
        {
            ShanLiang21Context db = new();
            HomeController hc = new(db);
            _hc = hc;
        }
        [Test]
        public void TestIndex()
        {
            IActionResult result = _hc.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestPrivacy()
        {
            IActionResult result = _hc.Privacy();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestError()
        {
            IActionResult result = _hc.Error();
            Assert.IsNotNull(result);
        }
    }
    [TestFixture]
    public class TestsBloggerController
    {
        BloggerController _bc;
        public TestsBloggerController()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            BloggerController bc = new(mockEnvironment.Object);
            _bc = bc;
        }
        [Test]
        public void TestBloggerList()
        {
            CKeywordViewModel vm = new()
            {
                txtKeyword = ""
            };
            IActionResult result1 = _bc.BloggerList(vm);
            Assert.IsNotNull(result1);

            vm.txtKeyword = "�馡";
            IActionResult result2 = _bc.BloggerList(vm);
            Assert.IsNotNull(result2);
        }
        [Test]
        public void TestBloggerCard()
        {
            CKeywordViewModel vm = new()
            {
                txtKeyword = ""
            };
            IActionResult result1 = _bc.BloggerCard(vm);
            Assert.IsNotNull(result1);

            vm.txtKeyword = "�馡";
            IActionResult result2 = _bc.BloggerList(vm);
            Assert.IsNotNull(result2);
        }
        [Test]
        public void TestBloggerCED()
        {
            //�s�W
            ShanLiang21Context db = new();
            IActionResult result1 = _bc.BloggerCreate();
            Assert.IsNotNull(result1);
            int BlogID;
            Blog blog = new(){
                BlogHeader = "TestHeader",
                BlogContent = "�o�O�@�ӳ椸���աA�A�����Ӭݨ�o�g�峹�C",
                CityName = "���ե�",
                DistrictName = "�椸��",
                RestaurantName = "TestCoffee"
            };
            IActionResult result2 = _bc.BloggerCreate(blog);
            Assert.IsNotNull(result2);
            //�ק�

            BlogID = db.Blogs.Where(b => b.BlogHeader == "TestHeader").Select(b => b.BlogId).First();
            IActionResult result3 = _bc.BloggerEdit(BlogID);
            Assert.IsNotNull(result3);
            IActionResult result4 = _bc.BloggerEdit(999999999);
            Assert.IsNotNull(result4);

            CBlogwrap edit = new()
            {
                blog = blog,
                BlogId = BlogID,
                BlogHeader = "TestHeaderEdit",
                BlogContent = "�o�O�@�ӳ椸���ի᪺�s��A�A�٬O���Ӭݨ�o�g�峹�C",
                //photo = @"eeee/eee",
            };
            IActionResult result5 = _bc.BloggerEdit(edit);
            Assert.IsNotNull(result5);

            //�R��
            IActionResult result6 = _bc.BloggerDelete(BlogID);
            Assert.IsNotNull(result6);
            IActionResult result7 = _bc.BloggerDelete(999999999);
            Assert.IsNotNull(result7);
        }
        [Test]
        public void TestBloggerDetail()
        {
            IActionResult result1 = _bc.BloggerDetail(999999999);
            Assert.IsNotNull(result1);
            IActionResult result2 = _bc.BloggerDetail(1);
            Assert.IsNotNull(result2);
        }
    }
    [TestFixture]
    public class TestsAdvertisementController
    {
        AdvertisementController _ac;
        Mock<HttpContext>? _mockHttpContext;
        Mock<ISession>? _mockSession;

        public TestsAdvertisementController()
        {
            Mock<HttpContext> mockHttpContext = new();
            _mockHttpContext = mockHttpContext;

            Mock<ISession> mockSession = new();
            _mockSession = mockSession;

            ShanLiang21Context db = new();
            AdvertisementController ac = new(db);
            _ac = ac;

            _mockHttpContext.SetupGet(x => x.Session).Returns(_mockSession.Object);

            _ac.ControllerContext = new()
            {
                HttpContext = _mockHttpContext.Object
            };
        }
        [Test]
        public void TestBuyAdv()
        {
            IActionResult result = _ac.BuyAdv();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestAddAdvToCart()
        {
            int? id = null;
            IActionResult result1 = _ac.AddAdvToCart(id);
            Assert.IsNotNull(result1);

            id = 1;
            IActionResult result2 = _ac.AddAdvToCart(id);
            Assert.IsNotNull(result2);

            ViewResult result3 = _ac.AddAdvToCart(id) as ViewResult;
            string message = "";
            foreach (var c in result3.ViewData.Values)
            {
                message += c.ToString();
            }
            Assert.AreEqual("1", message);

            CAddAdvToCartViewModel vm = new()
            {
                txtCount = 1,
                txtStoreID = 1
            };
            IActionResult result4 = _ac.AddAdvToCart(vm);
            Assert.IsNotNull(result4);
        }
        [Test]
        public void TestDelete()
        {
            int? id = null;
            IActionResult result1 = _ac.Delete(id);
            Assert.IsNotNull(result1);

            //id = 1;
            //IActionResult result2 = _ac.Delete(id);
            //Assert.IsNotNull(result2);
        }
        [Test]
        public void TestCartView()
        {
            IActionResult result = _ac.CartView();
            Assert.IsNotNull(result);
        }
        [Test]
        public void ConfirmADOrder()
        {
            //IActionResult result = _ac.ConfirmADOrder();
            //Assert.IsNotNull(result);
        }
    }
    [TestFixture]
    public class TestsAdminController
    {
        AdminController _ac;
        public TestsAdminController()
        {
            AdminController ac = new();
            _ac = ac;
        }
        [Test]
        public void TestIndex()
        {
            IActionResult result = _ac.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestList()
        {
            IActionResult result = _ac.List();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestCreate()
        {
            IActionResult result1 = _ac.Create();
            Assert.IsNotNull(result1);

            Admin a = new()
            {
                IdentificationId = 1
            };
            IActionResult result2 = _ac.Create(a);
            Assert.IsNotNull(result2);
            ViewResult result3 = _ac.Create(a) as ViewResult;
            string message = "";
            foreach(var c in result3.ViewData.Values)
            {
                message += c.ToString();
            }
            Assert.AreEqual("�L�k�ק��v��", message);

            a.IdentificationId = 3;
            IActionResult result4 = _ac.Create(a);
            Assert.IsNotNull(result4);
        }
        [Test]
        public void TestDelete()
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Admin a = new()
            {
                AdminName = "TestProjectAdmin",
                Passwoed = "0000",
                IdentificationId = 1
            };
            db.Add(a);
            db.SaveChanges();
            Assert.IsTrue(db.Admins.Where(x => x.AdminName == "TestProjectAdmin").Any());
            int TestAdminID = db.Admins.Where(x => x.AdminName == "TestProjectAdmin").Select(x => x.AdminId).First();
            IActionResult result = _ac.Delete(TestAdminID);
            Assert.IsFalse(db.Admins.Where(x => x.AdminName == "TestProjectAdmin").Any());
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestEdit()
        {
            int TestAdminID = 999999999;
            IActionResult result1 = _ac.Edit(TestAdminID);
            Assert.IsNotNull(result1);

            ShanLiang21Context db = new ShanLiang21Context();
            Admin a = new()
            {
                AdminName = "TestProjectAdmin",
                Passwoed = "0000",
                IdentificationId = 1
            };
            db.Add(a);
            db.SaveChanges();
            TestAdminID = db.Admins.Where(x => x.AdminName == "TestProjectAdmin").Select(x => x.AdminId).First();
            IActionResult result2 = _ac.Edit(TestAdminID);
            Assert.IsNotNull(result2);
            a.AdminName = "TestProjectAdminEdit";
            a.Passwoed = "1111";
            a.IdentificationId = 3;
            IActionResult result3 = _ac.Edit(a);
            Assert.IsNotNull(result3);
            db.Admins.Remove(a);
            db.SaveChanges();
        }
    }
}