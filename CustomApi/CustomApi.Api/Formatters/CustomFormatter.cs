using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace CustomizedApi.Api.Formatters
{
    public class CustomFormatter<TCustomModel> : BufferedMediaTypeFormatter
    {

        public CustomFormatter(MediaTypeHeaderValue mediaType)
        {
            SupportedMediaTypes.Add(mediaType);
        }

        public override bool CanReadType(Type type)
        {
            return type.IsAssignableFrom(typeof(TCustomModel));
        }

        public override bool CanWriteType(Type type)
        {
            return type.IsAssignableFrom(typeof(TCustomModel));
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            base.WriteToStream(type, value, writeStream, content);
        }

        public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            string contentJson = string.Empty;
            using (var reader = new StreamReader(readStream))
            {
                contentJson = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(contentJson))
                return null;

            return JObject.Parse(contentJson).ToObject<TCustomModel>();
        }
    }
}