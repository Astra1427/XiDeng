using System;
using System.Collections.Generic;
using System.Text;

namespace XiDeng.Data
{
    class ConfigModel
    {
        public int SleepSecond { get; set; }
        /// <summary>
        /// 动作间隔时间 毫秒 【已弃用】
        /// </summary>
        public int NumberSecond { get; set; }
        /// <summary>
        /// Down动作时间 毫秒
        /// </summary>
        public int DownNumberSecond { get; set; }
        /// <summary>
        /// Up动作时间 毫秒
        /// </summary>
        public int UpNumberSecond { get; set; }
        /// <summary>
        /// 呼吸律动 0 关闭 1开启
        /// </summary>
        public int IsRespiratoryRhythm { get; set; }
        public int StartContinueSecond { get; set; }
        public double BackAudioVolume { get; set; }
        public double PersonAudioVolume { get; set; }
        public string VersionNumber { get; set; }
    }
}
