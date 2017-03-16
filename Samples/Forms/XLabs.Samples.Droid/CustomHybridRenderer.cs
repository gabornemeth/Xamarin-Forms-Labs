// ***********************************************************************
// Assembly         : XLabs.Sample.Droid
// Author           : XLabs Team
// Created          : 12-27-2015
//
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="CustomHybridRenderer.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//
//       XLabs is a open source project that aims to provide a powerfull and cross
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
//

using XLabs.Forms.Controls;

namespace XLabs.Samples.Droid
{
    public class CustomClient : HybridWebViewRenderer.Client
    {
        public CustomClient(HybridWebViewRenderer webHybrid) : base(webHybrid) {}

        public override void OnReceivedSslError(Android.Webkit.WebView view, Android.Webkit.SslErrorHandler handler, Android.Net.Http.SslError error)
        {
            handler.Proceed();
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            System.Diagnostics.Debug.WriteLine("Webview OnPageFinished: " + url);
            base.OnPageFinished(view, url);
        }

        public override void OnReceivedError(Android.Webkit.WebView view, Android.Webkit.ClientError errorCode, string description, string failingUrl)
        {
            System.Diagnostics.Debug.WriteLine("Webview OnReceivedError: " + description);
            base.OnReceivedError(view, errorCode, description, failingUrl);
        }
    }

    public class CustomChromeClient : HybridWebViewRenderer.ChromeClient
    {
        public override bool OnConsoleMessage(Android.Webkit.ConsoleMessage consoleMessage)
        {
            System.Diagnostics.Debug.WriteLine(consoleMessage.Message());
            return base.OnConsoleMessage(consoleMessage);
        }

        public override void OnProgressChanged(Android.Webkit.WebView view, int newProgress)
        {
            System.Diagnostics.Debug.WriteLine("Webview progress: " + newProgress);
            base.OnProgressChanged(view, newProgress);
        }
    }
}

