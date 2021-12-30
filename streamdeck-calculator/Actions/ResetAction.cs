using BarRaider.SdTools;

/**
 * Reset Action
 * 
 * This action will reset operation and number input
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.reset")]
    public class ResetAction : PluginBase
    {
        public ResetAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override void KeyPressed(KeyPayload payload)
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Reset Key pressed");
            Calculator.Instance.reset();
            Connection.ShowOk();
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload) { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }
    }
}