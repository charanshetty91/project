using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using DataAccess.Repository;
using Newtonsoft.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PostCodeSearch
{
    public class Function
    {
        private IPostCodeRepository _postCodeRepository;

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request,
            ILambdaContext context)
        {
            context.Logger.Log("Execution of API gateway GetAllPostalCodeListById started!");
            try
            {
                if (request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey("partialId"))
                {
                    string partialId = request.QueryStringParameters["partialId"];
                    _postCodeRepository = new PostCodeRepository();
                    var data = await _postCodeRepository.GetAllPostalCodeListById(partialId);

                    if (data.result.Count > 0)
                    {
                        context.Logger.Log($"query param name: {data}");
                        
                        return new APIGatewayProxyResponse
                        {
                            Headers = new Dictionary<string, string>
                            {
                                ["Content-Type"] = "application/json",
                                ["Access-Control-Allow-Origin"]="*",
                                ["Access-Control-Allow-Methods"]= "POST,GET,OPTIONS,PUT,DELETE"
                            },
                            StatusCode = 200,
                            Body = $"{JsonConvert.SerializeObject(data.result)}",
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                context.Logger.Log("Error in GetAllPostalCodeListById API");
                context.Logger.Log("Error details:" + ex.StackTrace);
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
