﻿using BarRaider.SdTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

/**
 * Number Action
 * 
 * This action can be set multiple times by the user, and they can configure a number.
 * If the button is pressed, the number will be as new digit to the current number in
 * "currentNumber" memory storage.
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.number")]
    public class NumberAction : PluginBase
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
        public NumberAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
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
            try
            {
                DataStorage.getInstance().readMemory("operation");
            }
            catch
            {
                Connection.ShowAlert();
                return;
            }

            bool decimalMode = false;
            try
            {
                DataStorage.getInstance().readMemory("decimalMode");
                decimalMode = true;
            }
            catch
            {
            }

            string memoryName = decimalMode ? "currentDecimalNumber" : "currentNumber";

            // add to number in memory
            string currentNumber = "0";
            try
            {
                currentNumber = int.Parse(DataStorage.getInstance().readMemory(memoryName)).ToString();
            }
            catch
            {
            }

            if (!decimalMode && currentNumber == "0")
            {
                currentNumber = "";
            }

            string newNumber = currentNumber.ToString() + this.settings.Number.ToString();
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Key Pressed - Old number: {currentNumber}, new number: {newNumber} ({memoryName})");
            DataStorage.getInstance().writeMemory(memoryName, newNumber.ToString());
            Connection.ShowOk();
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }

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