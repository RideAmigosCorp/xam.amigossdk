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
            RAWebView.GlobalRegisteredHeaders.Add("Authorization", "Bearer "+(newValue as String));
        }

        /// <summary>
        /// The JWT associated with the current user
        /// </summary>
        public String JWTString
        {
            get => (String)GetValue(JWTStringProperty);
            set => SetValue(JWTStringProperty, value);
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
                RAWebView.GlobalRegisteredHeaders.Add("Webview-Context", (newValue as RAWebViewContext).getEncodedValue());
            }
        }

        /// <summary>
        /// The context associed with the current page load request
        /// </summary>
        public RAWebViewContext PageContext
        {
            get => (RAWebViewContext)GetValue(PageContextProperty);
            set => SetValue(PageContextProperty, value);
        }

        public RAWebView() {
            this.Source = "https://linkedin.rideamigos.com/workplace-app-preview";
        }

    }
}
