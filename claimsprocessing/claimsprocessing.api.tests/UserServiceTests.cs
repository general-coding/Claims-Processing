using claimsprocessing.api.Models;
using claimsprocessing.api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace claimsprocessing.api.tests
{
    [TestClass]
    public sealed class UserServiceTests
    {
        private TestSetup _testSetup = null!;
        private IUserService _userService = null!;
        private claims_processingContext _context = null!;

        private static int minUserId;
        private static int maxUserId;

        [TestInitialize]
        public async Task SetupAsync()
        {
            _testSetup = new TestSetup(services =>
            {
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                services.AddScoped<IUserService, UserService>();
            });

            _userService = _testSetup.GetService<IUserService>();
            _context = _testSetup._dbContext;

            //Do not run this if there are claims in the database
            if (!_context.tbl_claim.Any())
            {
                _context.tbl_user.RemoveRange(_context.tbl_user);
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('tbl_user', RESEED, 0)");

                //Add fake data
                List<tbl_user> users = FakeDataGenerator.GenerateFakeUsers(10);
                _context.tbl_user.AddRange(users);
                _context.SaveChanges();
            }

            minUserId = await _context.tbl_user.MinAsync(u => u.user_id);
            maxUserId = await _context.tbl_user.MaxAsync(u => u.user_id);
        }

        [TestMethod]
        public async Task GetUsersAsyncTest()
        {
            IEnumerable<tbl_user> users = await _userService.GetUsersAsync();
            Assert.AreNotEqual(0, users.Count());
        }

        [TestMethod]
        public async Task GetUserByIdAsyncTest()
        {
            int userId = new Random().Next(minUserId, maxUserId +1 );
            tbl_user? user = await _userService.GetUserByIdAsync(userId);
            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.user_id);
        }

        [TestMethod]
        public async Task CreateUserAsyncTest()
        {
            tbl_user user = FakeDataGenerator.GenerateFakeUsers(1).First();
            tbl_user? createdUser = await _userService.CreateUserAsync(user);
            Assert.IsNotNull(createdUser);
        }

        [TestMethod]
        public async Task UpdateUserByIdAsyncTest()
        {
            int userId = new Random().Next(minUserId, maxUserId); // Assuming this ID exists in the database
            tbl_user user = FakeDataGenerator.GenerateFakeUsers(1).First();
            user.user_id = userId;
            user.modified_on = DateTime.Now;
            bool isUpdated = await _userService.UpdateUserByIdAsync(userId, user);
            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public async Task DeleteUserByIdAsyncTest()
        {
            int userId = new Random().Next(minUserId, maxUserId +1 ); // Assuming this ID exists in the database
            bool isDeleted = await _userService.DeleteUserByIdAsync(userId);
            Assert.IsTrue(isDeleted);
        }

        [TestCleanup]
        public void Cleanup()
        {
            //General cleanup
            _context.Dispose();
            _testSetup.Dispose();
        }
    }
}
