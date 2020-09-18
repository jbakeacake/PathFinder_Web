using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using API.Tests.Utils;
using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Persistence;
using Xunit;
using Xunit.Abstractions;

namespace API.Tests
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ITestOutputHelper _output;
        private readonly string BaseUrl = "/api/v1";
        public UserControllerTests(ITestOutputHelper output, CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _output = output;

            // Ensure that we have a clean DataContext for every run:
            WebAppTestHelper.ResetWebAppDataContext(_factory);
        }
        [Fact]
        public async void CanListUsersAsync()
        {

            // Proceed with the test:
            using (var client = _factory.CreateClient())
            {
                var users = await UserTestHelper.GetUsers(client);
                users.ForEach(x => _output.WriteLine(x.Username));
                Assert.Contains(users, x => x.Username.ToLower().Equals("dummy user 1"));
                Assert.Contains(users, x => x.Username.ToLower().Equals("dummy user 2"));
                Assert.Contains(users, x => x.Username.ToLower().Equals("dummy user 3"));
            }
        }

        [Fact]
        public async void CanDeleteUserAsync()
        {
            using (var client = _factory.CreateClient())
            {
                //Query the list of users and grab the first Id:
                var rawUsers = await UserTestHelper.GetUsers(client);
                var idOne = rawUsers.First().Id;

                //Delete the first user:
                var res = await client.DeleteAsync($"{BaseUrl}/user/{idOne.ToString()}");

                //Query the list of updated users, and determine if the deleted Id exists:
                rawUsers = await UserTestHelper.GetUsers(client);

                //Assert that deleted Id doesn't exist anymore
                Assert.False(rawUsers.Any(x => x.Id == idOne));
            }
        }
        [Fact]
        public async void CanEditUserAsync()
        {
            using (var client = _factory.CreateClient())
            {
                //Grab a single user:
                var rawUser = (await UserTestHelper.GetUsers(client)).First();
                //Edit the user:
                rawUser.Username = "Test_Edit";
                var content = new StringContent(JsonConvert.SerializeObject(rawUser), Encoding.UTF8, "application/json");
                //Send the PUT request:
                var res = await client.PutAsync($"{BaseUrl}/user/{rawUser.Id.ToString()}", content);
                res.EnsureSuccessStatusCode();

                var rawUsers = await UserTestHelper.GetUsers(client);
                Assert.Contains(rawUsers, x => x.Username.ToLower().Equals("test_edit"));
            }
        }
        [Fact]
        public async void CanRegisterUserAsync()
        {
            using(var client = _factory.CreateClient())
            {
                //Create a user:
                var user = new { Username = "New_User", PlaintextPassword = "password"};
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                //Send the POST request:
                var res = await client.PostAsync($"{BaseUrl}/auth/register", content);
                res.EnsureSuccessStatusCode();

                var rawUsers = await UserTestHelper.GetUsers(client);
                Assert.Contains(rawUsers, x => x.Username.ToLower().Equals("new_user"));
            }
        }
        [Fact]
        public void CanLoginUserAsync()
        {
            Assert.True(1==0); //for purpose of testing in Jenkins
        }
    }
}
