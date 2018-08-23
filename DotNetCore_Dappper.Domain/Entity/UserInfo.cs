namespace DotNetCore_Dappper.Domain.Entity
{
    using  Dapper.Contrib.Extensions;
    [Table("UserInfo")]
    public class UserInfo
    {
        public  int Id { get; set; }

        public string Name { get; set; }
    }
}
