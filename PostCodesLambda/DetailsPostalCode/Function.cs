using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using DataAccess.Model;
using DataAccess.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DetailsPostalCode
{
    public class Function
    {
        private IPostCodeRepository _postCodeRepository;

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.Log("API gateway Postal Code Details fetch started!");
            try
            {
                if (request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey("id"))
                {
                    string postalCodeId = request.QueryStringParameters["id"];
                    _postCodeRepository = new PostCodeRepository();
                    var data = await _postCodeRepository.GetPostCodeById(postalCodeId);

                    if (data.status == 200)
                    {
                        PostCode postCode = new PostCode()
                        {
                            Country = data.result.country,
                            AdminDistrict = data.result.admin_district,
                            ParliamentaryConstituency = data.result.parliamentary_constituency,
                            Region = data.result.region,
                            Area = _postCodeRepository.GetArea(data.result.latitude)
                        };
                        return new APIGatewayProxyResponse
                        {
                            Headers = new Dictionary<string, string>
                            {
                                ["Content-Type"] = "application/json",
                                ["Access-Control-Allow-Origin"] = "*",
                                ["Access-Control-Allow-Methods"] = "POST,GET,OPTIONS,PUT,DELETE"
                            },
                            StatusCode = data.status,
                            Body = $"{JsonConvert.SerializeObject(postCode)}",
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                context.Logger.Log("Error in postal details fetch API");
                context.Logger.Log("Error details:"+ex.StackTrace);
                return new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = "server error",
                };
            }
            return new APIGatewayProxyResponse
            {
                StatusCode = 204,
                Body = "No result found",
            };
        }

    }
}
