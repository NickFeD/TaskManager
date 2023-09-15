using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ClientSDK
{
    public class ExHandler
    {
        private static event Action<HttpStatusCode, string> _onEx = new ((num,str)=> { });
        public ExHandler(HttpStatusCode statusCode, string ex)
        {
           Task.Run(()=>  _onEx.Invoke(statusCode, ex));
        }

        public static event Action RefreshTokenExpired = new(() => { });
        public static void OnRefreshTokenExpired() => RefreshTokenExpired.Invoke();
        public static void Handler(Action<HttpStatusCode, string> action)
        {
            _onEx += action;
        }
        public static void RemoveHandler(Action<HttpStatusCode, string> action)
        {
            _onEx -= action;
        }
    }
}
