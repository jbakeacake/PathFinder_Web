using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace API.Tests
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;
        private readonly string BaseUrl = "/api/v1";
        public UserControllerTests(ITestOutputHelper output, CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _output = output;
        }
        [Fact]
        public async void CanListUsers()
        {
            var res = await _client.GetAsync($"{BaseUrl}/user/");
            res.EnsureSuccessStatusCode();

            var strRes = await res.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(strRes);
            Assert.Contains(users, x => x.Username.ToLower().Equals("dummy user 1"));
            Assert.Contains(users, x => x.Username.ToLower().Equals("dummy user 2"));
            Assert.Contains(users, x => x.Username.ToLower().Equals("dummy user 3"));
        }

        [Fact]
        public async void CanDeleteUser()
        {
            var usersRes = await _client.GetAsync($"{BaseUrl}/user/");
            var resAsStr = await usersRes.Content.ReadAsStringAsync();
            var rawUsers = JsonConvert.DeserializeObject<IEnumerable<User>>(resAsStr);

            //Delete the first user:
            var idOne = rawUsers.First().Id;
            var res = await _client.DeleteAsync($"{BaseUrl}/user/{idOne.ToString()}");
            res.EnsureSuccessStatusCode();

            var res_2 = await _client.GetAsync($"{BaseUrl}/user/");
            res.EnsureSuccessStatusCode();

            var strRes = await res_2.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(strRes);
            ((List<User>)users).ForEach(x => _output.WriteLine("\n> " + x.Username));
        }
    }
}
