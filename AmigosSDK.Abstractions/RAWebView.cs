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
        public static readonly BindableProperty JWTStringProperty = BindableProperty.Create(nameof(JWTString), typeof(String), typeof(FormsWebView), null, propertyChanged: OnJWTSTringPropertyChanged);

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

        public RAWebView() {
            this.Source = "https://linkedin.rideamigos.com/workplace-app-preview";
            //this.Source = "https://httpbin.org/anything";
        }

    }
}
