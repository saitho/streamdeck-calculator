using BarRaider.SdTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Threading.Tasks;

/**
 * Storage Action
 * 
 * This action sets the current storage to user-defined string.
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.storage")]
    public class StorageAction : AbstractStatefulAction
    {
        private class PluginSettings
        {
            public static PluginSettings CreateDefaultSettings()
            {
                PluginSettings instance = new PluginSettings
                {
                    StorageName = ""
                };
                return instance;
            }

            [JsonProperty(PropertyName = "storageName")]
            public string StorageName { get; set; }
        }
        protected override string BtnFilePath => @"images\keyStorage.png";
        #region Private Members
        private readonly PluginSettings settings;
        #endregion

        public StorageAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            if (payload.Settings == null || payload.Settings.Count == 0)
            {
                this.settings = PluginSettings.CreateDefaultSettings();
            }
            else
            {
                this.settings = payload.Settings.ToObject<PluginSettings>();
            }
        }

        protected override Color? GetBorderColor()
        {
            if (DataStorage.Instance.getFileStorageName() == this.settings.StorageName)
            {
                return Color.Red;
            }
            return null;
        }

        public override void KeyPressed(KeyPayload payload)
        {
            if (Calculator.Instance.operation != null)
            {
                try
                {
                    // if operation was already started, add result from storage to current number
                    CurrentNumberHolder.Instance.add(DataStorage.Instance.readResultFile(this.settings.StorageName));
                }
                catch
                {
                    Logger.Instance.LogMessage(TracingLevel.INFO, $"Unable to add result from storage {this.settings.StorageName} to current number");
                }
                return;
            }
            if (DataStorage.Instance.getFileStorageName().Equals(this.settings.StorageName))
            {
                return;
            }
            DataStorage.Instance.setFileStorageName(this.settings.StorageName);

            if (!DataStorage.Instance.hasResultFile())
            {
                DataStorage.Instance.writeResultFile("0");
            }
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }
        public override void KeyReleased(KeyPayload payload) { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, "ReceivedSettings");
            Tools.AutoPopulateSettings(settings, payload.Settings);
            SaveSettings();
        }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

        #region Private Methods

        private Task SaveSettings()
        {
            return Connection.SetSettingsAsync(JObject.FromObject(settings));
        }
        #endregion
    }
}