using System;
using System.Windows.Input;

namespace Component.Helpers
{
    public static class XamarinHelper
    {
        /// <summary>
        /// Execute command with specified parameter
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static void ExecuteCommand(ICommand command, object parameter = null)
        {
            if (command == null || !command.CanExecute(parameter))
                return;

            command.Execute(parameter);
        }

        /// <summary>
        /// Get hex string from Xamarin.Forms.Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string GetHexString(this Xamarin.Forms.Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var alpha = (int)(color.A * 255);
            var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
    }
}
