using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Playground
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            xfSwitch.TapCommand = new Command<bool>(async (isOn) =>
            {
                Debug.WriteLine(isOn);
            });
        }
    }
}
