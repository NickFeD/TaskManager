using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ClientSDK.Generator;
public partial class ProblemDetails
{
    [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Type
    {
        get; set;
    }

    [Newtonsoft.Json.JsonProperty("title", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Title
    {
        get; set;
    }

    [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public int? Status
    {
        get; set;
    }

    [Newtonsoft.Json.JsonProperty("detail", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Detail
    {
        get; set;
    }

    [Newtonsoft.Json.JsonProperty("instance", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Instance
    {
        get; set;
    }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>());
        }
        set
        {
            _additionalProperties = value;
        }
    }

}
