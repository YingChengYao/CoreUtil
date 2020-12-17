using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Util.Extensions;

namespace Util.Helper
{
    public class AppSettings
    {
        /// <summary>
        /// 配置文件的根节点
        /// </summary>
        private static readonly IConfigurationRoot _config;

        static AppSettings()
        {
            // 加载appsettings.json，并构建IConfigurationRoot
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            _config = builder.Build();
        }

        /// <summary>
        /// IpLimit
        /// </summary>
        public static class IpLimit
        {
            /// <summary>
            /// 当为True的时候 例如设置了5次每分钟访问限流，当你getData()5次过后禁止访问，
            /// 但是还可以访问postData()5次，总得来说是每个接口都有5次在这一分钟，互不干扰。
            /// 当为False的时候 每个接口都加入计数，不管你访问哪个接口 只要在一分钟内累计够5次 将禁止访问
            /// </summary>
            public static bool EnableEndpointRateLimiting => _config["IpRateLimiting:EnableEndpointRateLimiting"].TryToBool();

            /// <summary>
            /// 设置为false，则拒绝的呼叫不会添加到油门计数器。如果客户每秒发出3个请求，
            /// 而您设置的限制为每秒一个呼叫，则其他限制（例如每分钟或每天）将仅记录第一个呼叫，
            /// 而该呼叫未被阻止。如果您希望被拒绝的请求计入其他限制，则必须设置StackBlockedRequests为true
            /// </summary>
            public static bool StackBlockedRequests => _config["IpRateLimiting:StackBlockedRequests"].TryToBool();

            /// <summary>
            /// 使用时，你的红隼服务器背后是一个反向代理，如果你的代理服务器使用不同的页眉然后提取客户端IP
            /// X-Real-IP使用此选项来设置它
            /// </summary>
            public static string RealIpHeader => _config["IpRateLimiting:RealIpHeader"];

            /// <summary>
            /// 被用于提取白名单的客户端ID。如果此标头中存在客户端ID，并且与ClientWhitelist中指定的值匹配，
            /// 则不应用速率限制
            /// </summary>
            public static string ClientIdHeader => _config["IpRateLimiting:ClientIdHeader"];

            /// <summary>
            /// 设置IP白名单
            /// </summary>
            public static List<string> IpWhitelist
            {
                get
                {
                    return _config["IpRateLimiting:IpWhitelist"].Split(",").ToList();
                }
            }

            /// <summary>
            /// 设置客户端ID白名单
            /// </summary>
            public static List<string> ClientWhitelist
            {
                get
                {
                    return _config["IpRateLimiting:ClientWhitelist"].Split(",").ToList();
                }
            }

            /// <summary>
            /// 设置返回状态码
            /// </summary>
            public static string HttpStatusCode => _config["IpRateLimiting:HttpStatusCode"];

            /// <summary>
            /// 设置返回提示信息
            /// </summary>
            public static string QuotaExceededResponse => _config["IpRateLimiting:QuotaExceededResponse"];

            /// <summary>
            /// 设置限制条件，可以设置多种条件，可以针对IP、接口
            /// </summary>
            public static string GeneralRules => _config["IpRateLimiting:GeneralRules"];
        }
    }
}
