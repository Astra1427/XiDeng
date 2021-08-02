using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace XiDeng.Common
{
    class Utility
    {

        public static ImageSource GetImage(string name)
        {
            try
            {
                return ImageSource.FromStream(() => { return new MemoryStream((byte[])Properties.Resources.ResourceManager.GetObject(name)); });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
