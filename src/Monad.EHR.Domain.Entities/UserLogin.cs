namespace Monad.EHR.Domain.Entities
{
    public class UserLogin : BaseEntity
    {
        public int UserID { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }

}
