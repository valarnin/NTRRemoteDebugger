using System;
using System.IO;
using System.Windows.Forms;

namespace NTRDebuggerTool.Objects
{
    public class SaveManager
    {
        public String[] addr;
        public int[] type;

        public void Init()
        {
            if (addr == null) addr = new String[0];
            if (type == null) type = new int[0];
        }

        public static void SaveToXml(string filePath, SaveManager sourceObj)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer =
                        new System.Xml.Serialization.XmlSerializer(sourceObj.GetType());
                    xmlSerializer.Serialize(writer, sourceObj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static SaveManager LoadFromXml(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(SaveManager));
                    return (SaveManager)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            { }
            return new SaveManager();
        }
    }
}