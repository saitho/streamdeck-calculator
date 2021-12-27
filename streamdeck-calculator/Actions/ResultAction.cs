using BarRaider.SdTools;

/**
 * Calculate Action
 * 
 * This action will calculate the new result by taking the number from the storage file (or 0 per default)
 * and apply the current number with the current operation; and write the result back to the storage file.
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.result")]
    public class ResultAction : PluginBase
    {
        public ResultAction(SDConnection connection, InitialPayload payload) : base(connection, payload) { }

        public override void OnTick() {
            DataStorage data = DataStorage.getInstance();
            try
            {
                Connection.SetTitleAsync(data.readResultFile());
            }
            catch
            {
            }
        }
        public override void Dispose() { }

        public override void KeyPressed(KeyPayload payload) { }

        public override void KeyReleased(KeyPayload payload) { }
        public override void ReceivedSettings(ReceivedSettingsPayload payload) { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }
    }
}