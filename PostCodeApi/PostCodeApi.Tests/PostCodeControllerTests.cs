using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using System.IO;
using System.Reflection.Emit;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Constants;
using DataAccess.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace PostCodeApi.Tests
{
    /// <summary>
    /// PostCodeController API test class
    /// </summary>
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
                    Assert.Equal("{\"W10 4AA\":\"South\",\"W10 4AB\":\"South\",\"W10 4AD\":\"South\",\"W10 4AE\":\"South\",\"W10 4AF\":\"South\",\"W10 4AG\":\"South\",\"W10 4AH\":\"South\",\"W10 4AJ\":\"South\"}", response.Body);

                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    break;

                case "FullCode":
                    Assert.Equal(200, response.StatusCode);
                    Assert.Equal("{\"W10 4AN\":\"South\"}", response.Body);

                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));

                    Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    break;

                case "NullOrEmpty":
                    Assert.Equal(400, response.StatusCode);
                    dynamic result = JsonConvert.DeserializeObject(response.Body);
                    Assert.Contains("Value cannot be empty or white space.", ((JContainer)result).Last.ToString());

                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.Equal("application/problem+json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
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
                    var responseData = response.Body;
                    Assert.Contains("country", responseData);
                    Assert.Contains("adminDistrict", responseData);
                    Assert.Contains("region", responseData);
                    Assert.Contains("parliamentaryConstituency", responseData);
                    Assert.Contains("area", responseData);
                    Assert.Contains("England", responseData);
                    Assert.Contains("Wokingham", responseData);
                    Assert.Contains("South", responseData);
                    Assert.Contains("Maidenhead", responseData);
                    Assert.Contains("South East", responseData);

                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                   
                    break;

                case "Postcode_PartialId":
                    Assert.Equal(404, response.StatusCode);
                    Assert.Equal(Constants.NoDataMessage, response.Body);
                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.Equal("text/plain; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    break;

                case "Postcode_NullOrEmpty":
                    Assert.Equal(400, response.StatusCode);

                    dynamic result = JsonConvert.DeserializeObject(response.Body);
                    Assert.Contains("Value cannot be empty or white space.", ((JContainer)result).Last.ToString());

                    Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
                    Assert.Equal("application/problem+json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
                    break;
            }
        }
    }
}
