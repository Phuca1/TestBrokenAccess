using Newtonsoft.Json;

namespace TestBrokenAccess.Models
{
    public class GrantRoleModel
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }
    }
}
