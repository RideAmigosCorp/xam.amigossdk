using Android.Webkit;
using Java.Interop;
using System;

namespace AmigosSDK.Droid
{
    public class FormsWebViewBridge : Java.Lang.Object
    {

        readonly WeakReference<FormsWebViewRenderer> Reference;

        public FormsWebViewBridge(FormsWebViewRenderer renderer)
        {
            Reference = new WeakReference<FormsWebViewRenderer>(renderer);
        }

        [JavascriptInterface]
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            Console.WriteLine("AmigosSDK :: Android :: FormsWebViewBridge :: InvokeAction :: " + data);
            if (Reference == null || !Reference.TryGetTarget(out FormsWebViewRenderer renderer)) return;
            if (renderer.Element == null) return;
            renderer.Element.HandleScriptReceived(data);
            Console.WriteLine("AmigosSDK :: Android :: FormsWebViewBridge :: InvokeAction Complete");
        }
    }
}