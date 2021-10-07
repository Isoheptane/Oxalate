using System;
using System.IO;
using System.Windows.Forms;

namespace OxalateClient_GUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Preference preference = new Preference();
            if (File.Exists("config.json"))
            {
                preference = new Preference(JsonSharp.JsonObject.Parse(File.ReadAllText("config.json")));
            }
            else
            {
                File.WriteAllText("config.json", preference.ToJsonObject().Serialize("", "  "));
            }
            Application.Run(new MainForm(preference));
        }
    }
}
