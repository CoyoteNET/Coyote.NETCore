using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace CoyoteNET.Tests
{
    public static class Extensions
    {
        public static StringContent AsJsonString(this object obj) => 
            new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }
}