using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WpfApp2.Services
{
    public class SettingsService
    {


  
        private  const string fileName = "settings.json";
        private  static string folderPath;
        private static string filePath;
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true};
        private static StateManager man;
        public static void Start(string name,StateManager manager)
        {
            man = manager;
            folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), name);
            filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath)) {
                System.Diagnostics.Debug.WriteLine("json exits");

                string json = File.ReadAllText(filePath);


                try
                {
                    Settings? settings = JsonSerializer.Deserialize<Settings>(json);
                    man.UpdateNotificationPosition(settings.position);
                }
                catch (NullReferenceException ex) { 
                    MessageBox.Show(ex.Message);
                }
            }
        }
     

        public static void Save(Settings settings)
        {
            string json = JsonSerializer.Serialize(settings, options);
            System.Diagnostics.Debug.WriteLine(settings.position);
            Directory.CreateDirectory(folderPath);

            File.WriteAllText(filePath, json);
        }
        public void Load()
        {

        }
    }
}
