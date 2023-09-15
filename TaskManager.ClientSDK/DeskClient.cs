using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Command.Models;

namespace TaskManager.ClientSDK
{
    public class DeskClient : BaseClient<DeskModel>
    {
        public DeskClient(string baseUrl, HttpClient httpClient, CancellationToken cancellationToken) : base(baseUrl, httpClient, cancellationToken)
        {
        }


    }
}
