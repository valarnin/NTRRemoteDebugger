using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace NTRDebuggerTool
{
    public class Config
    {
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

        public static Dictionary<string, string> All
        {
            get
            {
                Dictionary<string, string> All = new Dictionary<string, string>();
                foreach (XmlNode Node in RootXmlElement.ChildNodes)
                {
                    All.Add(Node.Name, Node.InnerText);
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
        }

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
                if (ConfigFile == null)
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

        #endregion
    }
}
