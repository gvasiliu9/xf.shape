
**Xamarin Forms Switch Component**

<a href="https://www.nuget.org/packages/Utmdev.Xf.Switch/" target="_blank"><img src="https://img.shields.io/nuget/v/Utmdev.Xf.Switch?style=for-the-badge"/></a>

**Usage**

Namespace:

    xmlns:utmdev="clr-namespace:Utmdev.Xf.Components;assembly=Utmdev.Xf.Switch"

Component:

    <utmdev:Switch x:Name="xfSwitch"
                   VerticalOptions="CenterAndExpand"
                   HorizontalOptions="CenterAndExpand"
                   IsOn="False"
                   HeightRequest="50"
                   WidthRequest="125"
                   CircleColor="White"
                   BackgroundColor="#2FC061" />
                                
Command:                             

    xfSwitch.TapCommand = new Command<bool>(async (isOn) =>
	    {
	        Debug.WriteLine(isOn);
	    });

**Demo**

<img src="https://github.com/utmdev/xf.switcher/blob/master/Component/Demo/switcher.gif">
