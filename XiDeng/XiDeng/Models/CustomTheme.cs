using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XiDeng.Common;

namespace XiDeng.Models
{
    public class CustomTheme:ModelBase
    {
        public Guid Creator { get; set; } = Utility.LoggedAccount.Id;
        public string TopBarHexColor { get; set; } = "#252526";
        public string BottomBarHexColor { get; set; } = "#252526";
        public string PageBgHexColor { get; set; } = "#2A2A2C";
        public string ListItemBgHexColor { get; set; } = "#1B1B1C";
        public string TextHexColor { get; set; } = "#ACACAC";
        public string MainPageImgUrl { get; set; }
        public string PageBgImgUrl { get; set; }
        /// <summary>
        /// "True" is bright tone , "False" is dark tone
        /// </summary>
        public bool Tone { get; set; }

        [SQLite.Ignore]
        public Color TopBarColor => Color.FromHex(TopBarHexColor);
        [SQLite.Ignore]
        public Color BottomBarColor => Color.FromHex(BottomBarHexColor);
        [SQLite.Ignore]
        public Color PageBgColor => Color.FromHex(PageBgHexColor);
        [SQLite.Ignore]
        public Color ListItemBgColor => Color.FromHex(ListItemBgHexColor);
        [SQLite.Ignore]
        public Color TextColor => Color.FromHex(TextHexColor);
        [SQLite.Ignore]
        public string DisTone => Tone ? "亮色调" : "暗色调";
    }
}
