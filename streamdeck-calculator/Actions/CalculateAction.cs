using BarRaider.SdTools;

/**
 * Calculate Action
 * 
 * This action will calculate the new result by taking the number from the storage file (or 0 per default)
 * and apply the current number with the current operation; and write the result back to the storage file.
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.calculate")]
    public class CalculateAction : PluginBase
    {
        public CalculateAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override void KeyPressed(KeyPayload payload)
        {
            Calculator calculator = Calculator.Instance;
            if (calculator.operation == null)
            {
                Connection.ShowAlert();
                return;
            }

            float storedNumber = calculator.getCurrentResult();
            float currentNumber = calculator.getInput();
            float newNumber = calculator.performCalculation();

            Logger.Instance.LogMessage(TracingLevel.INFO, $"CALCULATE - {storedNumber} {calculator.operation} {currentNumber} = {newNumber}");

            // Store number to file
            DataStorage.Instance.writeResultFile(newNumber.ToString());
            CurrentNumberHolder.Instance.reset();
            calculator.reset();
            Connection.ShowOk();
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload) { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }
    }
}