using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XiDeng.Common
{
    class FileHelper
    {
        public static string BasePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string VideoUriFile = "VideoUriFile";
        public static string SettingFile = "SettingFile";
        public static string ExerciseLogFile = "ExerciseLogFile";
        public static string LoginInfoFile = "LoginInfoFile";
        public static string SkillFile = "SkillFile";

        public static bool WriteFile(string name,string content)
        {
            try
            {
                File.WriteAllText(BasePath+"/" + name + ".txt",content);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static string ReadFile(string name)
        {
            try
            {
                return File.ReadAllText(BasePath+"/" + name + ".txt");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool IsExist(string name)
        {
            return File.Exists(BasePath+"/" + name + ".txt");
        }
    }
}
