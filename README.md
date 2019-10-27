
## **Shape Component for Xamarin Forms**
You can use this component in case you want to create a container with a specific rounded corner. By default Xamarin.Forms offers Frame which can have rounded corners but you can't customize specific corner. Also Shape component supports linear gradient. 

<a href="https://www.nuget.org/packages/Utmdev.Xf.ColorWheel/1.0.0" target="_blank"><img src="https://img.shields.io/nuget/v/Utmdev.Xf.Shape?style=for-the-badge"/></a>

**Usage:**

Namespaces:

    xmlns:utmdev="clr-namespace:Utmdev.Xf.Shape.Content;assembly=Utmdev.Xf.Shape"
    xmlns:shapeMarkupExtensions="clr-namespace:Utmdev.Xf.Shape.MarkupExtensions;assembly=Utmdev.Xf.Shape"

Component:

    <utmdev:Shape BackgroundColor="#56A3A6"
	              Parameters="{shapeMarkupExtensions:ShapeParameters TopLeft=50,  TopRight=50, BottomRight=50, BottomLeft=50}"
                  Padding="25">

	    <utmdev:Shape.Section>

	        <Label Text="Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old."
	                FontAttributes="Bold"
	                TextColor="White"
	                VerticalOptions="CenterAndExpand"
	                HorizontalTextAlignment="Center" />

	    </utmdev:Shape.Section>
	    
    </utmdev:Shape>

**Specific corner:**

    <utmdev:Shape BackgroundColor="#F9C80E"
                Parameters="{shapeMarkupExtensions:ShapeParameters TopLeft=50}"
                Padding="25">
                
	    <utmdev:Shape.Section>
		    ...
	    </utmdev:Shape.Section>
	    
    </utmdev:Shape>


Shape parameters supports next values:
 - TopLeft - top left corner
 - TopRight - top right corner
 - BottomRight - bottom right corner
 - BottomLeft - bottom left corner 

<p align="center">
  <img width="500" src="https://github.com/utmdev/xf.shape/blob/master/Component/Demo/top_left.png" />
</p>

**Gradient:**

    <utmdev:Shape Gradient="{shapeMarkupExtensions:ShapeGradient Start=#659999, End=#f4791f, Degrees=0, EndPosition=1}"
                  Parameters="{shapeMarkupExtensions:ShapeParameters TopLeft=50}"
                  Padding="25">

	    <utmdev:Shape.Section>
			...
	    </utmdev:Shape.Section>
	    
    </utmdev:Shape>

<p align="center">
  <img width="500" src="https://github.com/utmdev/xf.shape/blob/master/Component/Demo/gradient.png" />
</p>

Gradient markup extension supports next values:
 - Start - color
 - End - color
 - Degrees - rotate gradient, values from 0-360
 -  Start position (adjust start color position, value from [0-1])
 -  End position (adjust end color position, value from [0-1])
 
 (adjust these values to customize your gradient color).

**Usage example:**
<p align="center">
  <img width="550" src="https://github.com/utmdev/xf.shape/blob/master/Component/Demo/points.png" />
</p>
