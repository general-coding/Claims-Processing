using Bogus;
using claimsprocessing.api.Models;
using claimsprocessing.api.Utilities;

namespace claimsprocessing.api.tests
{
    internal class FakeDataGenerator
    {
        public static List<tbl_user> GenerateFakeUsers(int count)
        {
            Faker<tbl_user> faker = new Faker<tbl_user>()
                .RuleFor(u => u.user_fname, f => f.Name.FirstName())
                .RuleFor(u => u.user_mname, f => f.Name.FirstName())
                .RuleFor(u => u.user_lname, f => f.Name.LastName())
                .RuleFor(u => u.user_fullname, setter: (f, u) => UserUtilities.GetUserFullName(u))
                .RuleFor(u => u.user_email, f => f.Internet.Email())
                .RuleFor(u => u.user_password, setter: (f, u) => UserUtilities.GeneratePasswordHash("password"))
                .RuleFor(u => u.created_on, DateTime.Now);

            return faker.Generate(count);
        }

        private static readonly string[] claimTypes =
        [
            "Health Insurance Claims",
            "Motor Insurance Claims",
            "Life Insurance Claims",
            "Travel Insurance Claims",
            "Home Insurance Claims",
            "Commercial Insurance Claims",
            "Personal Accident Insurance Claims"
        ];

        private static readonly string[] claimStatuses =
        [
            "Initiated",
            "Under Review",
            "Documents Pending",
            "Approved",
            "Rejected",
            "Settled",
            "Closed",
            "On Hold",
            "Escalated",
            "Cancelled"
        ];

        public static List<tbl_claim> GenerateFakeClaims(int count, int minUserId, int maxUserId)
        {
            Faker<tbl_claim> faker = new Faker<tbl_claim>()
                .RuleFor(c => c.claim_user_id, f => f.Random.Int(minUserId, maxUserId))
                .RuleFor(c => c.claim_type, f => f.PickRandom(claimTypes))
                .RuleFor(c => c.claim_amount, f => f.Random.Int(1, 10000))
                .RuleFor(c => c.claim_status, f => f.PickRandom(claimStatuses))
                .RuleFor(c => c.created_on, DateTime.Now);
            return faker.Generate(count);
        }
    }
}