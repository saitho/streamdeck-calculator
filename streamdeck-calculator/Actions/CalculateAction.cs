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
        protected string resultFileName = "result.txt";

        public CalculateAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override void KeyPressed(KeyPayload payload)
        {
            DataStorage data = DataStorage.getInstance();
            string operation;
            try
            {
                operation = data.readMemory("operation");
            }
            catch
            {
                Connection.ShowAlert();
                return;
            }

            int storedNumber = 0;
            if (data.hasFile(this.resultFileName))
            {
                try
                {
                    storedNumber = int.Parse(data.readFile(this.resultFileName));
                }
                catch
                {
                    Logger.Instance.LogMessage(TracingLevel.ERROR, "Unable to read result.txt");
                    Connection.ShowAlert();
                    return;
                }
            }

            int currentNumber = 0;
            try
            {
                currentNumber = int.Parse(data.readMemory("currentNumber"));
            }
            catch
            {
                Connection.ShowAlert();
                return;
            }

            Logger.Instance.LogMessage(TracingLevel.INFO, $"Key Pressed - Stored number: {storedNumber}, Applied number: {currentNumber}, Operation: {operation}");

            int newNumber = storedNumber;
            if (operation == "+")
            {
                storedNumber += currentNumber;
            }
            else if (operation == "-")
            {
                storedNumber -= currentNumber;
            }

            // Store number to file
            data.writeFile(this.resultFileName, storedNumber.ToString());

            data.deleteMemory("operation");
            data.deleteMemory("currentNumber");
            Connection.ShowOk();
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload) { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }
    }
}