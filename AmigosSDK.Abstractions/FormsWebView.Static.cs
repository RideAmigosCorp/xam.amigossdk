using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace AmigosSDK.Abstractions
{
    public partial class FormsWebView
    {

        internal static event EventHandler<string> CallbackAdded;

        /// <summary>
        /// A bindable property for the Navigating property.
        /// </summary>
        public static readonly BindableProperty NavigatingProperty = BindableProperty.Create(nameof(Navigating), typeof(bool), typeof(FormsWebView), false);

        /// <summary>
        /// A bindable property for the Source property.
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(string), typeof(FormsWebView));

        /// <summary>
        /// A bindable property for the ContentType property.
        /// </summary>
        public static readonly BindableProperty ContentTypeProperty = BindableProperty.Create(nameof(ContentType), typeof(WebViewContentType), typeof(FormsWebView), WebViewContentType.Internet);

        /// <summary>
        /// A bindable property for the BaseUrl property.
        /// </summary>
        public static readonly BindableProperty BaseUrlProperty = BindableProperty.Create(nameof(BaseUrl), typeof(string), typeof(FormsWebView));

        /// <summary>
        /// A bindable property for the CanGoBack property.
        /// </summary>
        public static readonly BindableProperty CanGoBackProperty = BindableProperty.Create(nameof(CanGoBack), typeof(bool), typeof(FormsWebView), false);

        /// <summary>
        /// A bindable property for the CanGoForward property.
        /// </summary>
        public static readonly BindableProperty CanGoForwardProperty = BindableProperty.Create(nameof(CanGoForward), typeof(bool), typeof(FormsWebView), false);

        /// <summary>
        /// A bindable property for the EnableGlobalCallbacks property.
        /// </summary>
        public static readonly BindableProperty EnableGlobalCallbacksProperty = BindableProperty.Create(nameof(EnableGlobalCallbacks), typeof(bool), typeof(FormsWebView), true);

        /// <summary>
        /// A bindable property for the EnableGlobalHeaders property.
        /// </summary>
        public static readonly BindableProperty EnableGlobalHeadersProperty = BindableProperty.Create(nameof(EnableGlobalHeaders), typeof(bool), typeof(FormsWebView), true);


        /// <summary>
        /// A bindable property for the ScalesPageToFit property.
        /// </summary>
        public static readonly BindableProperty UseWideViewPortProperty = BindableProperty.Create(nameof(UseWideViewPort), typeof(bool), typeof(FormsWebView), false);

        /// <summary>
        /// A dictionary used to add headers which are used throughout all instances of FormsWebView.
        /// </summary>
        public readonly static Dictionary<string, string> GlobalRegisteredHeaders = new Dictionary<string, string>();

        internal readonly static Dictionary<string, Action<string>> GlobalRegisteredCallbacks = new Dictionary<string, Action<string>>();
        
        /// <summary>
        /// Adds a callback to every FormsWebView available in the application.
        /// </summary>
        /// <param name="functionName">The function to call</param>
        /// <param name="action">The returning action</param>
        public static void AddGlobalCallback(string functionName, Action<string> action)
        {
            if (string.IsNullOrWhiteSpace(functionName)) return;

            if (GlobalRegisteredCallbacks.ContainsKey(functionName))
                GlobalRegisteredCallbacks.Remove(functionName);

            GlobalRegisteredCallbacks.Add(functionName, action);
            CallbackAdded?.Invoke(null, functionName);
        }

        /// <summary>
        /// Removes a callback by the function name.
        /// Note: this does not remove it from the DOM, rather it removes the action, resulting in your view never getting the response.
        /// </summary>
        /// <param name="functionName"></param>
        public static void RemoveGlobalCallback(string functionName)
        {
            if (GlobalRegisteredCallbacks.ContainsKey(functionName))
                GlobalRegisteredCallbacks.Remove(functionName);
        }

        /// <summary>
        /// Removes a callback by the function name.
        /// Note: this does not remove it from the DOM, rather it removes the action, resulting in your view never getting the response.
        /// </summary>
        /// <param name="functionName"></param>
        public static void RemoveAllGlobalCallbacks()
        {
            GlobalRegisteredCallbacks.Clear();
        }

        internal static string InjectedFunction
        {
            get
            {
                var fnScript = "";
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        fnScript = "function amigosdk(data){window.Android.invokeAction(data);}";
                        break;
                    case Device.iOS:
                    case "macOS":
                        fnScript = "function amigosdk(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
                        break;
                }
                Console.WriteLine("AmigosSDK :: FormsWebView.Static :: InjectedFunction :: "+fnScript);
                return fnScript;
            }
        }

        internal static string GenerateFunctionScript(string name)
        {
            var deviceBridgePath = "";
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    deviceBridgePath = "window.Android";
                    break;
                case Device.iOS:
                case "macOS":
                    deviceBridgePath = "window..webkit.messageHandlers";
                    break;
            }
            var fnScript = $"{deviceBridgePath}.{name} = function(str){{amigosdk(\"{{'action':'{name}','data':'\"+window.btoa(str)+\"'}}\");}}";
            Console.WriteLine("AmigosSDK :: FormsWebView.Static :: GenerateFunctionScript :: " + fnScript);
            return fnScript;
        }
    }
}
