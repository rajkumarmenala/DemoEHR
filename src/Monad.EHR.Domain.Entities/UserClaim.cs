namespace Monad.EHR.Domain.Entities
{
    public class UserClaim : BaseEntity
    {
        public int UserID { get; set; }
        //public int ClaimID { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }

}
