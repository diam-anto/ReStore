using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.RequestHelpers;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
        {
            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            // Api and swagger same domain and we can see the cause header
            response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData, options));
            // this header we want to be available to the client
            // in order to see header from the client side
            // beacause it's a custom header
            // "Access-Control-Expose-Headers" this should be like that 
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}