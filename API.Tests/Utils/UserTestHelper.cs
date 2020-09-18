using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;

namespace API.Tests.Utils
{
    public static class UserTestHelper
    {
        private static readonly string BaseUrl = "/api/v1";
        public static async Task<List<User>> GetUsers(HttpClient client)
        {
            var usersRes = await client.GetAsync($"{BaseUrl}/user/");
            var rawUsers = await ParseUsersAsync(usersRes);
            return rawUsers;
        }
        private static async Task<List<User>> ParseUsersAsync(HttpResponseMessage res)
        {
            var stringifyRes = await res.Content.ReadAsStringAsync();
            var rawUsers = JsonConvert.DeserializeObject<IEnumerable<User>>(stringifyRes);

            return (List<User>)rawUsers;
        }
    }
}