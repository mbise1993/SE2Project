using System;
using System.Windows;

namespace HopeHouse.Common.Helpers
{
    public static class WindowHelper
    {
        //
        // Method to find a window in the application by type
        //
        public static Window FindWindow ( Type windowType )
        {
            if (Application.Current.Windows.Count > 0)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType ( ) == windowType)
                        return window;
                }
            }

            return null;
        }
    }
}
