using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

namespace AmigosSDK.Droid
{
    public class FormsWebViewChromeClient : WebChromeClient
    {

        readonly WeakReference<FormsWebViewRenderer> Reference;

        public FormsWebViewChromeClient(FormsWebViewRenderer renderer)
        {
            Console.WriteLine("AmigosSDK :: Android :: FormsWebViewChromeClient :: Set Renderer");
            Reference = new WeakReference<FormsWebViewRenderer>(renderer);
        }

    }
}