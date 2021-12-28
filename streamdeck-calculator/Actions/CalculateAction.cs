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

            bool decimalMode = false;
            try
            {
                DataStorage.getInstance().readMemory("decimalMode");
                decimalMode = true;
            }
            catch
            {
            }

            float storedNumber = 0;
            if (data.hasResultFile())
            {
                try
                {
                    storedNumber = float.Parse(data.readResultFile());
                }
                catch
                {
                    Logger.Instance.LogMessage(TracingLevel.ERROR, "Unable to read result.txt");
                    Connection.ShowAlert();
                    return;
                }
            }

            float currentNumber;
            try
            {
                currentNumber = float.Parse(data.readMemory("currentNumber"));
                if (decimalMode)
                {
                    int currentDecimalNumber = int.Parse(data.readMemory("currentDecimalNumber"));
                    double numberLength = currentDecimalNumber == 0 ? 1 : System.Math.Floor(System.Math.Log10(currentDecimalNumber)) + 1;
                    currentNumber += currentDecimalNumber / (float)System.Math.Pow(10.0, numberLength);
                }
            }
            catch
            {
                Connection.ShowAlert();
                return;
            }

            float newNumber = storedNumber;
            if (operation == "+")
            {
                newNumber += currentNumber;
            }
            else if (operation == "-")
            {
                newNumber -= currentNumber;
            }

            Logger.Instance.LogMessage(TracingLevel.INFO, $"CALCULATE - {storedNumber} {operation} {currentNumber} = {newNumber}");

            // Store number to file
            data.writeResultFile(newNumber.ToString());

            data.deleteMemory("operation");
            data.deleteMemory("currentNumber");
            data.deleteMemory("currentDecimalNumber");
            data.deleteMemory("decimalMode");
            Connection.ShowOk();
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload) { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }
    }
}