using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using Xamarin.Forms;

namespace AmigosSDK.Abstractions
{
    public class RAWebView : FormsWebView
    {
        /// <summary>
        /// A bindable property for the JWTString property.
        /// </summary>
        public static readonly BindableProperty JWTStringProperty = BindableProperty.Create(nameof(JWTString), typeof(String), typeof(RAWebView), null, propertyChanged: OnJWTSTringPropertyChanged);

        static void OnJWTSTringPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            RAWebView.GlobalRegisteredHeaders["Authorization"] = "Bearer "+(newValue as String);
        }

        /// <summary>
        /// The JWT associated with the current user
        /// </summary>
        public String JWTString
        {
            get => (String)GetValue(JWTStringProperty);
            set
            {
                SetValue(JWTStringProperty, value);
                Console.WriteLine("AmigosSDK :: Set JWTString");

            }
        }

        /// <summary>
        /// A bindable property for the PageContext property.
        /// </summary>
        public static readonly BindableProperty PageContextProperty = BindableProperty.Create(nameof(PageContext), typeof(RAWebViewContext), typeof(RAWebView), null, propertyChanged: OnPageViewContextPropertyChanged);

        static void OnPageViewContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is null)
            {
                return;
            }
            else
            {
                RAWebView.GlobalRegisteredHeaders["Webview-Context"] = (newValue as RAWebViewContext).getEncodedValue();
            }
        }

        /// <summary>
        /// The context associed with the current page load request
        /// </summary>
        public RAWebViewContext PageContext
        {
            get => (RAWebViewContext)GetValue(PageContextProperty);
            set
            {
                SetValue(PageContextProperty, value);
                Console.WriteLine("AmigosSDK :: Set PageContext");
            }
        }

        public RAWebView() {
            this.Source = "https://linkedin.rideamigos.com/workplace-app-preview";
            Console.WriteLine("AmigosSDK :: RAWebView Instantiated");
            this.AddLocalCallback("closeApp", CloseSDK);
            this.AddLocalCallback("openExternalUrl", HandleExternalURL);
        }

        /// <summary>
        /// Tell the RideAmigos app to "go back". This may triger "OnCloseRequested" if the app is on the dashboard screen.
        /// </summary>
        async public void BackPressed() {
            await this.InjectJavascriptAsync("window.onBackPressed()");
        }

        /// <summary>
        /// If supplied, the OnCloseRequested event handler will be called whenever the web-content requests "closeApp"
        /// </summary>
        /// <param name="obj"></param>
        private void CloseSDK(string obj)
        {
            OnCloseRequested?.Invoke(null, new EventArgs());
        }

        /// <summary>
        /// Handle "openExternalUrl" request form the web-content
        /// </summary>
        /// <param name="uri"></param>
        private void HandleExternalURL(string uri)
        {
            this.HandleNavigationStartRequest(uri);
        }

        /// <summary>
        /// If supplied, the OnCloseRequested event handler will be called whenever the web-content requests "closeApp"
        /// </summary>
        /// <param name="obj"></param>
        public event EventHandler OnCloseRequested;

    }
}
