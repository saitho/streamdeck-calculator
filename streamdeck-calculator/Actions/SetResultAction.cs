﻿using BarRaider.SdTools;
using Newtonsoft.Json;

/**
 * Set Action
 * 
 * This action will set the current result number to the value specified in settings (default: 0).
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.set")]
    public class SetResultAction : PluginBase
    {
        private class PluginSettings
        {
            public static PluginSettings CreateDefaultSettings()
            {
                PluginSettings instance = new PluginSettings
                {
                    Number = 0
                };
                return instance;
            }

            [JsonProperty(PropertyName = "number")]
            public int Number { get; set; }
        }

        #region Private Members

        private readonly PluginSettings settings;

        #endregion

        protected string resultFileName = "result.txt";

        public SetResultAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
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

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override void KeyPressed(KeyPayload payload)
        {
            DataStorage data = DataStorage.getInstance();
            Logger.Instance.LogMessage(TracingLevel.INFO, $"SETRESULT - Setting result to {this.settings.Number}");

            // Store number to file
            data.writeFile(this.resultFileName, this.settings.Number.ToString());
            Connection.ShowOk();
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload) { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }
    }
}