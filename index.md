Xamarin Forms Labs
=====================

**Xamarin Forms Labs** is a open source project that aims to provide a powerful and cross platform set of controls tailored to work with [Xamarin Forms](http://xamarin.com/forms).

Call for action for all Xamarin Developers, embrace this project and share your controls and services with the community, add your own control to the toolkit.

## NOTICE: This project is no longer officially maintained.
**This is a private fork, which can be significantly differenct from the [original one](https://github.com/XLabs/Xamarin-Forms-Labs). It's focusing on the controls only. Right now recent NuGet packages can be grabbed from [MyGet](https://www.myget.org/F/gabornemeth/api/v3/index.json).**

**Available controls**

You can check the latest state of available controls [here](controls.md)
 
**Available services (Beta)**

 - Accelerometer
 - Cache
 - Camera (Picture and Video picker, Take Picture, Take Video)
 - Device (battery info, device info, sensors, accelerometers)
 - Display
 - Geolocator
 - Phone Service (cellular network info, make phonecalls)
 - SoundService
 - Text To Speech 


**Available Mvvm helpers (Beta)**

 - ViewModel (navigation, isbusy)
 - ViewFactory
 - IOC
 - IXFormsApp (application events)

**Available Plugins (Beta)**
    
 - Serialization (ServiceStackV3, ProtoBuf, JSON.Net)
 - Caching (SQLLiteSimpleCache)
 - Dependency Injection containers (TinyIOC, Autofac, NInject, SimpleInjector, Unity)
 - Web (RestClient)
 - [Charting (Line, Bar & Pie) (Alpha)](https://github.com/XForms/Xamarin-Forms-Labs/wiki/Charting)
 
_________________


**Getting started**
======

Using the controls
-----------


Add Xamarin.Forms.Labs.Controls reference to your projects , main pcl, ios, android, and wp.

Xaml :

Reference the assembly namespace 

     xmlns:controls="clr-namespace:XLabs.Forms.Controls;assembly=XLabs.Forms"

Render your control:

     <controls:ImageButton Text="Twitter" BackgroundColor="#01abdf" TextColor="#ffffff" HeightRequest="75" WidthRequest="175" Image="icon_twitter" Orientation="ImageToLeft"  ImageHeightRequest="50" ImageWidthRequest="50" />
      
Or from your codebehind:


	var button = new ImageButton() {
				ImageHeightRequest = 50,
				ImageWidthRequest = 50,
				Orientation = Orientation.ImageToLeft,
				Source = "icon_twitter.png",
				Text = "Twitter"
			};
	stacker.Children.Add (button);
	


Using the Services
-----------
**TextToSpeechService** 

	DependencyService.Get<ITextToSpeechService>().Speak(TextToSpeak);
	
**Device** 

		var device = Resolver.Resolve<IDevice>();
		device.Display; //display information
		device.Battery; //battery information

	
**PhoneService** 

	 	var device = Resolver.Resolve<IDevice>();
		// not all devices have phone service, f.e. iPod and Android tablets
		// so we need to check if phone service is available
		if (device.PhoneService != null)
		{
			device.PhoneService.DialNumber("+1 (855) 926-2746");
		}


Initializing the Services
-----------
Do this before using the services

**Step 1:** 
* iOS => Make sure your AppDelegate inherits from XFormsApplicationDelegate

* Android => MainActivity inherits from XFormsApplicationDroid

* Windows Phone => Add this line to your App.cs 
				  var app = new XFormsAppWP(); app.Init(this);

**Step 2:** 
		Initialize the container in your app startup code.

		var container = new SimpleContainer ();
		container.Register<IDevice> (t => AppleDevice.CurrentDevice);
		container.Register<IDisplay> (t => t.Resolve<IDevice> ().Display);
		container.Register<INetwork>(t=> t.Resolve<IDevice>().Network);

		Resolver.SetResolver (container.GetResolver ());

[For more info on initialization go to the Labs Wiki](https://github.com/XLabs/Xamarin-Forms-Labs/wiki)
________________


**Helper**
======

> Current version v1.2.0

[v1.2.0 - Xamarin Forms Labs Framework Helper for online use](http://htmlpreview.github.io/?https://raw.githubusercontent.com/XLabs/Xamarin-Forms-Labs/master/Helper/v1.2.0/Web/Index.html)

[v1.2.0 - Xamarin.Forms.Labs.chm file for offline use](https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/Helper/v1.2.0/Xamarin.Forms.Labs.chm)

> Based in last developments (master)

[Master- Xamarin Forms Labs Framework Helper for online use](http://htmlpreview.github.io/?https://raw.githubusercontent.com/XLabs/Xamarin-Forms-Labs/master/Helper/master/Web/Index.html)

[Master - Xamarin.Forms.Labs.chm file for offline use](https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/Helper/master/Xamarin.Forms.Labs.chm)
________________


**Build the project**
======

To develop on this project, just clone the project to your computer, package restore is enable so build the solution first, if you get any errors try to build each project independently .
		
__________________

**Nuget**
======

**Main Packages:**

- [Xamarin.Forms.Labs](https://www.nuget.org/packages/Xamarin.Forms.Labs/)

**Plugins:**

* Caching 

 - [Xamarin.Forms.Labs.Caching.SQLiteNet](https://www.nuget.org/packages/Xamarin.Forms.Labs.Caching.SQLiteNet/)

* DI 

 - [Xamarin.Forms.Labs.Services.SimpleContainer](https://www.nuget.org/packages/Xamarin.Forms.Labs.Services.SimpleContainer/)
 - [Xamarin.Forms.Labs.Services.Ninject](https://www.nuget.org/packages/Xamarin.Forms.Labs.Services.Ninject/)
 - [Xamarin.Forms.Labs.Services.Autofac](https://www.nuget.org/packages/Xamarin.Forms.Labs.Services.Autofac/)
 - [Xamarin.Forms.Labs.Services.TinyIOC](https://www.nuget.org/packages/Xamarin.Forms.Labs.Services.TinyIOC/)
 
* Serialization

 - [Xamarin.Forms.Labs.Services.Serialization.ProtoBuf](https://www.nuget.org/packages/Xamarin.Forms.Labs.Services.Serialization.ProtoBuf/)
 - [Xamarin.Forms.Labs.Serialization.JsonNET](https://www.nuget.org/packages/Xamarin.Forms.Labs.Services.Serialization.JsonNET/)

* Cryptography

 - [Xamarin.Forms.Labs.Cryptography](https://www.nuget.org/packages/Xamarin.Forms.Labs.Cryptography/)
 
__________________

**Contributions:**
======
 - Michael Ridland [@rid00z ](https://twitter.com/rid00z)
 - [Rui Marinho](http://ruimarinho.net/)  [@ruiespinho](https://twitter.com/ruiespinho)
 - Filip De Vos  [@foxtricks](https://twitter.com/foxtricks)
 - ThomasLebrun 
 - Sami M. Kallio 
 - [Kevin E. Ford](http://windingroadway.blogspot.com/) [@Bowman74](https://twitter.com/Bowman74)
 - Jason Smith [@jassmith87](https://twitter.com/jassmith87)
 - Shawn Anderson
 - [Sara Silva](http://saramgsilva.com) [@saramgsilva](https://twitter.com/saramgsilva)
 - Ben Ishiyama-Levy [@mrbrl](http://www.monovo.io)
 - Ryan Wischkaemper
 - [Eric Grover](http://www.ericgrover.com) [@bluechiperic](https://twitter.com/bluechiperic)
 - [Mitch Milam](http://blogs.infinite-x.net) [@mitchmilam](https://twitter.com/mitchmilam)
 - [Jim Bennett](http://www.jimbobbennett.io) [@jimbobbennett](https://twitter.com/jimbobbennett)
 - Kazuki Yasufuku
 - Petr Klíma
 - Bart Kardol
 - [Nicholas Rogoff](http://blog.nicholasrogoff.com/) [@nrogoff](https://twitter.com/nrogoff)
 
**Contribute**
------------------

Everbody is welcome to contribute. 

Twitter hashtag : [#xflabs](https://twitter.com/search?q=xflabs)
		  
_________________

**License**
======

License Apache 2.0 more about that in the [LICENSE](https://github.com/gabornemeth/Xamarin-Forms-Labs/blob/master/LICENSE) file. 
