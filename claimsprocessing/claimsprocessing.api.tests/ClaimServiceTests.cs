using claimsprocessing.api.Models;
using claimsprocessing.api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace claimsprocessing.api.tests
{
    [TestClass]
    public sealed class ClaimServiceTests
    {
        private TestSetup _testSetup = null!;
        private IClaimService _claimService = null!;
        private claims_processingContext _context = null!;

        private static int minUserId;
        private static int maxUserId;

        private static int minClaimId;
        private static int maxClaimId;

        [TestInitialize]
        public async Task Setup()
        {
            _testSetup = new TestSetup(services =>
            {
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                services.AddScoped<IClaimService, ClaimService>();
            });

            _claimService = _testSetup.GetService<IClaimService>();
            _context = _testSetup._dbContext;

            _context.tbl_claim.RemoveRange(_context.tbl_claim);
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('tbl_claim', RESEED, 0)");

            minUserId = await _context.tbl_user.MinAsync(u => u.user_id);
            maxUserId = await _context.tbl_user.MaxAsync(u => u.user_id);

            //Add fake data
            List<tbl_claim> claims = FakeDataGenerator.GenerateFakeClaims(10, minUserId, maxUserId);
            _context.tbl_claim.AddRange(claims);
            _context.SaveChanges();

            minClaimId = await _context.tbl_claim.MinAsync(c => c.claim_id);
            maxClaimId = await _context.tbl_claim.MaxAsync(c => c.claim_id);
        }

        [TestMethod]
        public async Task GetClaimsAsyncTest()
        {
            IEnumerable<tbl_claim> claims = await _claimService.GetClaimsAsync();
            Assert.AreNotEqual(0, claims.Count());
        }

        [TestMethod]
        public async Task GetClaimByIdAsyncTest()
        {
            int claimId = new Random().Next(minClaimId, maxClaimId + 1);
            tbl_claim? claim = await _claimService.GetClaimByIdAsync(claimId);
            Assert.IsNotNull(claim);
            Assert.AreEqual(claimId, claim.claim_id);
        }

        [TestMethod]
        public async Task GetClaimsByClaimUserIdAsyncTest()
        {
            int claimUserId = new Random().Next(minUserId, maxUserId + 1);
            IEnumerable<tbl_claim> claims = await _claimService.GetClaimsByClaimUserIdAsync(claimUserId);
            Assert.AreNotEqual(0, claims.Count());
        }

        [TestMethod]
        public async Task CheckClaimUserExistsAsyncTest()
        {
            int userId = new Random().Next(minUserId, maxUserId + 1);
            bool exists = await _claimService.CheckClaimUserExistsAsync(userId);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public async Task CreateClaimAsyncTest()
        {
            tbl_claim claim = FakeDataGenerator.GenerateFakeClaims(1, minUserId, maxUserId).First();
            tbl_claim? createdClaim = await _claimService.CreateClaimAsync(claim);
            Assert.IsNotNull(createdClaim);
        }

        [TestMethod]
        public async Task UpdateClaimByIdAsyncTest()
        {
            int claimId = new Random().Next(minClaimId, maxClaimId + 1);
            tbl_claim claim = FakeDataGenerator.GenerateFakeClaims(1, minUserId, maxUserId).First();
            claim.claim_id = claimId;
            claim.modified_on = DateTime.Now;
            bool updated = await _claimService.UpdateClaimByIdAsync(claimId, claim);
            Assert.IsTrue(updated);
        }

        [TestMethod]
        public async Task DeleteClaimByIdAsyncTest()
        {
            int claimId = new Random().Next(minClaimId, maxClaimId + 1);
            bool isDeleted = await _claimService.DeleteClaimByIdAsync(claimId);
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
