using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using System.IO;
using System.Reflection.Emit;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Model;
using Newtonsoft.Json;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace PostCodeApi.Tests
{
    public class PostCodeControllerTests
    {

        [Theory]
        [InlineData(0, "SingleChar")]
        [InlineData(1, "NullOrEmpty")]
        [InlineData(2, "FullCode")]
        public async Task LookupPostcodeGet_Test(int dataPick, string testCase)
        {
            var lambdaFunction = new LambdaEntryPoint();

            var requestStr = JsonConvert.DeserializeObject<object[]>(File.ReadAllText("./RequestsData/LookupPostcodeGet.json"));
            ;
            var request = JsonSerializer.Deserialize<APIGatewayProxyRequest>(requestStr[dataPick].ToString(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            var context = new TestLambdaContext();
            var response = await lambdaFunction.FunctionHandlerAsync(request, context);
            switch (testCase)
            {
                case "SingleChar":
                    Assert.Equal(200, response.StatusCode);
                    Assert.Equal("[\"W10 4AA\",\"W10 4AB\",\"W10 4AD\",\"W10 4AE\",\"W10 4AF\",\"W10 4AG\",\"W10 4AH\",\"W10 4AJ\",\"W10 4AL\",\"W10 4AN\"]", response.Body);

                    Assert.True(response.MultiValueHeaders.ContainsKey("Access-Control-Allow-Origin"));
                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.True(response.MultiValueHeaders.ContainsKey("Access-Control-Allow-Methods"));

                    Assert.Equal("*", response.MultiValueHeaders["Access-Control-Allow-Origin"][0]);
                    Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    Assert.Equal("POST,GET,OPTIONS,PUT,DELETE", response.MultiValueHeaders["Access-Control-Allow-Methods"][0]);
                    break;

                case "FullCode":
                    Assert.Equal(200, response.StatusCode);
                    Assert.Equal("[\"W10 4AN\"]", response.Body);

                    Assert.True(response.MultiValueHeaders.ContainsKey("Access-Control-Allow-Origin"));
                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.True(response.MultiValueHeaders.ContainsKey("Access-Control-Allow-Methods"));

                    Assert.Equal("*", response.MultiValueHeaders["Access-Control-Allow-Origin"][0]);
                    Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    Assert.Equal("POST,GET,OPTIONS,PUT,DELETE", response.MultiValueHeaders["Access-Control-Allow-Methods"][0]);
                    break;

                case "NullOrEmpty":
                    Assert.Equal(404, response.StatusCode);
                    Assert.Equal("no data found", response.Body);

                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.Equal("text/plain; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    break;
            }
        }

        [Theory]
        [InlineData(0, "Postcode_FullId")]
        [InlineData(1, "Postcode_NullOrEmpty")]
        [InlineData(2, "Postcode_PartialId")]
        public async Task AutocompletePostcodePartial_Test(int dataPick, string testCase)
        {
            var lambdaFunction = new LambdaEntryPoint();

            var requestStr = JsonConvert.DeserializeObject<object[]>(File.ReadAllText("./RequestsData/AutocompletePostcodePartial.json"));
            ;
            var request = JsonSerializer.Deserialize<APIGatewayProxyRequest>(requestStr[dataPick].ToString(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            var context = new TestLambdaContext();
            var response = await lambdaFunction.FunctionHandlerAsync(request, context);
            switch (testCase)
            {
                case "Postcode_FullId":
                    Assert.Equal(200, response.StatusCode);
                    var responseData = JsonSerializer.Deserialize<PostCode>(response.Body);
                    Assert.Equal("England", responseData.country);
                    Assert.Equal("Wokingham", responseData.adminDistrict);
                    Assert.Equal("South", responseData.area);
                    Assert.Equal("Maidenhead", responseData.parliamentaryConstituency);
                    Assert.Equal("South East", responseData.region);

                    Assert.True(response.MultiValueHeaders.ContainsKey("Access-Control-Allow-Origin"));
                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.True(response.MultiValueHeaders.ContainsKey("Access-Control-Allow-Methods"));

                    Assert.Equal("*", response.MultiValueHeaders["Access-Control-Allow-Origin"][0]);
                    Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    Assert.Equal("POST,GET,OPTIONS,PUT,DELETE", response.MultiValueHeaders["Access-Control-Allow-Methods"][0]);
                    break;

                case "Postcode_PartialId":
                    Assert.Equal(404, response.StatusCode);
                    Assert.Equal("No Data found with this code ", response.Body);
                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.Equal("text/plain; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    break;

                case "Postcode_NullOrEmpty":
                    Assert.Equal(404, response.StatusCode);
                    Assert.Equal("No Data found with this code ", response.Body);

                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.Equal("text/plain; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    break;
            }
        }
    }
}
