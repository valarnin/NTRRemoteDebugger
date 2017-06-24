using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Linq;

namespace NTRDebuggerTool.Objects.Saving
{
    public class SaveManager
    {
        public uint LastUsedStartAddress = 0;
        public uint LastUsedRangeSize = 0;
        public String TitleId { get; set; } = null;
        public List<SaveCode> Codes { get; set; } = new List<SaveCode>();
        public List<GateShark> GateSharkCodes { get; set; } = new List<GateShark>();
        [JsonIgnore]
        public string Filename { get => PathHelper.BuildFileConfigPath(TitleId, "json", "codes"); }

        public override string ToString()
        {
            return TitleId + ",[" + Codes.ToString() + "],[" + GateSharkCodes.ToString() + "]";
        }

        public void SaveToJson()
        {
            if (TitleId == null || Codes.Count == 0) return;
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(Filename, json);
            }
            catch (Exception ex)
            {
                Logger.Log("Exception saving codes [" + this + "] to JSON file", ex);
                MessageBox.Show(ex.Message);
            }
        }

        public static SaveManager LoadFromJson(string gamename)
        {
            try
            {
                gamename = PathHelper.BuildFileConfigPath(gamename, "json", "codes");
                string json = File.ReadAllText(gamename);
                return JsonConvert.DeserializeObject<SaveManager>(json);
            }
            catch (System.IO.FileNotFoundException fex)
            {
                Logger.Log("Exception JSON file not found " + gamename, fex);
            }
            catch (Exception ex)
            {
                Logger.Log("Exception loading codes from JSON file " + gamename, ex);
            }
            return new SaveManager();
        }


    }
}