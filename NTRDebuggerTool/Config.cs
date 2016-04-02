using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace NTRDebuggerTool
{
    public class Config
    {
        #region Config Settings
        #region MaxValuesToDisplay
        private const uint DefaultMaxValuesToDisplay = 1000;
        private static Nullable<uint> maxValuesToDisplay = null;
        public static uint MaxValuesToDisplay
        {
            get
            {
                if (maxValuesToDisplay == null)
                {
                    string TempVal = GetValue("MaxValuesToDisplay");
                    maxValuesToDisplay = string.IsNullOrWhiteSpace(TempVal) ? DefaultMaxValuesToDisplay : uint.Parse(TempVal);
                }
                return maxValuesToDisplay.Value;
            }
            set
            {
                maxValuesToDisplay = value;
                SetValue("MaxValuesToDisplay", value.ToString());
            }
        }
        #endregion

        #region ButtonStateUpdateInterval
        private const int DefaultButtonStateUpdateInterval = 250;
        private static Nullable<int> buttonStateUpdateInterval = null;
        public static int ButtonStateUpdateInterval
        {
            get
            {
                if (buttonStateUpdateInterval == null)
                {
                    string TempVal = GetValue("ButtonStateUpdateInterval");
                    buttonStateUpdateInterval = string.IsNullOrWhiteSpace(TempVal) ? DefaultButtonStateUpdateInterval : int.Parse(TempVal);
                }
                return buttonStateUpdateInterval.Value;
            }
            set
            {
                buttonStateUpdateInterval = value;
                SetValue("ButtonStateUpdateInterval", value.ToString());
            }
        }
        #endregion

        #region DefaultIP
        private const string DefaultDefaultIP = ""; //Stupid name but oh well
        private static string defaultIP = null;
        public static string DefaultIP
        {
            get
            {
                if (defaultIP == null)
                {
                    string TempVal = GetValue("DefaultIP");
                    defaultIP = string.IsNullOrWhiteSpace(TempVal) ? DefaultDefaultIP : TempVal;
                }
                return defaultIP;
            }
            set
            {
                defaultIP = value;
                SetValue("DefaultIP", value);
            }
        }
        #endregion

        #region ConnectTimeout
        private const int DefaultConnectTimeout = 3000;
        private static Nullable<int> connectTimeout = null;
        public static int ConnectTimeout
        {
            get
            {
                if (connectTimeout == null)
                {
                    string TempVal = GetValue("ConnectTimeout");
                    connectTimeout = string.IsNullOrWhiteSpace(TempVal) ? DefaultConnectTimeout : int.Parse(TempVal);
                }
                return connectTimeout.Value;
            }
            set
            {
                connectTimeout = value;
                SetValue("ConnectTimeout", value.ToString());
            }
        }
        #endregion

        #region ConnectTries
        private const int DefaultConnectTries = 5;
        private static Nullable<int> connectTries = null;
        public static int ConnectTries
        {
            get
            {
                if (connectTries == null)
                {
                    string TempVal = GetValue("ConnectTries");
                    connectTries = string.IsNullOrWhiteSpace(TempVal) ? DefaultConnectTries : int.Parse(TempVal);
                }
                return connectTries.Value;
            }
            set
            {
                connectTries = value;
                SetValue("ConnectTries", value.ToString());
            }
        }
        #endregion
        #endregion

        #region Add to default set here
        public static Dictionary<string, string> All
        {
            get
            {
                Dictionary<string, string> All = new Dictionary<string, string>();
                foreach (XmlNode Node in RootXmlElement.ChildNodes)
                {
                    All.Add(Node.Name, Node.InnerText);
                }
                if (!All.ContainsKey("MaxValuesToDisplay"))
                {
                    All.Add("MaxValuesToDisplay", MaxValuesToDisplay.ToString());
                }
                if (!All.ContainsKey("ButtonStateUpdateInterval"))
                {
                    All.Add("ButtonStateUpdateInterval", ButtonStateUpdateInterval.ToString());
                }
                if (!All.ContainsKey("DefaultIP"))
                {
                    All.Add("DefaultIP", DefaultIP.ToString());
                }
                if (!All.ContainsKey("ConnectTimeout"))
                {
                    All.Add("ConnectTimeout", ConnectTimeout.ToString());
                }
                if (!All.ContainsKey("ConnectTries"))
                {
                    All.Add("ConnectTries", ConnectTries.ToString());
                }
                return All;
            }
            set
            {
                foreach (string Key in value.Keys)
                {
                    SetValue(Key, value[Key]);
                }
                ConfigFile = null;
                InitializeConfigFile();
            }
        }

        private static void CreateConfigFile()
        {
            Directory.CreateDirectory(ConfigFileDirectory);
            ConfigFile.LoadXml("<root></root>");
            ConfigFile.Save(ConfigFilePath);
            MaxValuesToDisplay = DefaultMaxValuesToDisplay;
            ButtonStateUpdateInterval = DefaultButtonStateUpdateInterval;
            DefaultIP = DefaultDefaultIP;
            ConnectTimeout = DefaultConnectTimeout;
            ConnectTries = DefaultConnectTries;
        }

        #endregion

        #region XML Stuff

        private static string ConfigFileDirectory = Directory.GetParent(Application.UserAppDataPath).FullName;
        private static string ConfigFilePath = ConfigFileDirectory + Path.DirectorySeparatorChar + "NTRDebuggerTool.config.xml";
        private static XmlDocument ConfigFile;

        private static void SetValue(string Key, string Value)
        {
            XmlElement XmlNode = (XmlElement)RootXmlElement.SelectSingleNode("//" + Key);
            if (XmlNode == null)
            {
                XmlNode = ConfigFile.CreateElement(Key);
                RootXmlElement.AppendChild(XmlNode);
            }
            XmlNode.InnerText = Value;
            ConfigFile.Save(ConfigFilePath);
        }
        private static string GetValue(string Key)
        {
            XmlElement XmlNode = (XmlElement)RootXmlElement.SelectSingleNode("//" + Key);
            if (XmlNode != null) { return XmlNode.InnerText; }
            return "";
        }

        private static XmlElement RootXmlElement
        {
            get
            {
                if (ConfigFile == null || ConfigFile.FirstChild == null)
                {
                    InitializeConfigFile();
                }
                return (XmlElement)ConfigFile.FirstChild;
            }
        }

        private static void NodeChangedHandler(Object src, XmlNodeChangedEventArgs args)
        {
            if (ConfigFile != null)
            {
                ConfigFile.Save(ConfigFilePath);
            }
        }

        private static void InitializeConfigFile()
        {
            ConfigFile = new XmlDocument();
            try
            {
                ConfigFile.Load(ConfigFilePath);
            }
            catch (Exception)
            {
                CreateConfigFile();
            }
            ConfigFile.NodeChanged += new XmlNodeChangedEventHandler(NodeChangedHandler);
            ConfigFile.NodeInserted += new XmlNodeChangedEventHandler(NodeChangedHandler);
            ConfigFile.NodeRemoved += new XmlNodeChangedEventHandler(NodeChangedHandler);
        }

        static Config()
        {
            if (All != null) ;
        }

        #endregion
    }
}
