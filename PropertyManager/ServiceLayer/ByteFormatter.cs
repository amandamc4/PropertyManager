using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.IO;
using System.Net.Http;

namespace PropertyManager.ServiceLayer
{
    public class ByteFormatter : BufferedMediaTypeFormatter
    {               
            public ByteFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/png"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/jpeg"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/jpg"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/gif"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/pdf"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-zip-compressed"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("audio/mpeg"));
            }

            private HttpRequestMessage _request;

            private ByteFormatter(HttpRequestMessage request)
            {
                _request = request;
            }

            public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
            {
                return new ByteFormatter(request);
            }

            public override bool CanReadType(Type type)
            {
                return (type == typeof(byte[]));
            }

            public override object ReadFromStream(Type type, System.IO.Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
            {
                var ms = new MemoryStream();
            
                readStream.CopyTo(ms);
                
                return ms.ToArray();
            }
   
            public override bool CanWriteType(Type type)
            {
                return (type == typeof(byte[]));
            }

            public override void WriteToStream(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content)
            {                
                MemoryStream ms = new MemoryStream(value as byte[]);
               
                ms.CopyTo(writeStream);
            }

        }
}