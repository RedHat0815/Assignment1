//using Xunit;


//namespace TestProject1
//{
//    public class TestControllerPost
//    {
//        [Fact]
//        public void Test1()
//        {

//            var postResponse = await _client.PostAsJsonAsync("/api/Events", new { Title = "Event title" });
//            var createdEvent = await GetPageFromResponse(postResponse);
//            var createdEventId = GetIdFromEvent(createdEvent);

//            Assert.Equal(new Uri($"http://localhost/api/Events/{createdEventId}"), postResponse.Headers.Location);
//            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
//            Assert.Equal("Event title", GetTitleFromEvent(createdEvent));

//            var getResponse = await _client.GetAsync($"/api/Events/{createdEventId}");
//            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
//            var loadedEvent = await GetPageFromResponse(getResponse);

//            Assert.Equal(createdEventId, GetIdFromEvent(loadedEvent));
//            Assert.Equal("Event title", GetTitleFromEvent(loadedEvent));
//        }

//        private async Task<JsonDocument> GetPageFromResponse(HttpResponseMessage response)
//        {
//            return (await response.Content.ReadFromJsonAsync<JsonDocument>())!;
//        }

//        private Guid GetIdFromEvent(JsonDocument page)
//        {
//            return page.RootElement.GetProperty("id").GetGuid();
//        }

//        private string GetTitleFromEvent(JsonDocument page)
//        {
//            return page.RootElement.GetProperty("title").GetString()!;
//        }

        
//    }
//}