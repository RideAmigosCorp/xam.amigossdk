using System;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;
using Newtonsoft.Json;

namespace AmigosSDK.Abstractions
{
    public class RAWebViewContext
    {
        public String action;

        public ExpandoObject metadata;

        /// <summary>
        /// Return a base64 encoded json state object
        /// </summary>
        public String getEncodedValue()
        {
            string json = JsonConvert.SerializeObject(this);
            var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
            return System.Convert.ToBase64String(jsonBytes);
        }

    }
}
