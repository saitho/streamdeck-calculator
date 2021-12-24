using BarRaider.SdTools;

/**
 * SubtractOperation Action
 * 
 * This action sets the current operation to "subtract" mode.
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.operationsubtract")]
    public class SubtractOperationAction : PluginBase
    {
        #region Private Members

        #endregion
        public SubtractOperationAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override void KeyPressed(KeyPayload payload)
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Subtract operation pressed");
            MemoryStore.getInstance().set("operation", "-");
            Connection.ShowOk();
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload) { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }
    }
}