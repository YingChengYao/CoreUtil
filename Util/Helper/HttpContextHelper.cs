using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Util.Helper
{
    public static class HttpContextHelper
    {
        /// <summary>
        /// 获取客户端Ip
        /// X-Real-IP 代表的是客户端请求真实的 IP 地址，这个参数没有相关标准规范，如果是直接访问的请求，可能是客户端真实的 IP 地址，但是中间若经过了层层的代理，就是最后一层代理的 IP 地址
        /// X-Forwarded-For 记录着从客户端发起请求后访问过的每一个 IP 地址，当然第一个是发起请求的客户端本身的地址，各 IP 地址间由“英文逗号+空格”(,)分隔
        /// 相对于 X-Real-IP 的不确定性，X-Forwarded-For 已经写进了 RFC 7239，所以成了获取 IP 地址的首选途径。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIp(this HttpRequest request)
        {
            // var ip = request.Headers["X-Real-IP"].FirstOrDefault() ??
            //          request.Headers["X-Forwarded-For"].FirstOrDefault() ??
            //          request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            
            var ip = request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                     request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return ip;
        }
    }
}
