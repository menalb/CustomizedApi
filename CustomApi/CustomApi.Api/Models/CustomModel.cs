using Messages;
using Newtonsoft.Json.Linq;
using System;

namespace CustomizedApi.Api.Models
{
    public abstract class CustomModel
    {
        public T Parse<T>(string json)
        {
            return JObject.Parse(json).ToObject<T>();
        }

        public abstract void Process(Action<CustomModel> actionToPerform);        
    }

    public abstract class AbstractLocation : CustomModel { }
    public abstract class DynamicProductModel : CustomModel
    {
        public abstract ILoadProductMessage GetMessageToSend();
    }
}