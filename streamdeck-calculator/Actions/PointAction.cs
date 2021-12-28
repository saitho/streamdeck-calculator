using BarRaider.SdTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

/**
 * Point Action
 * 
 * This action sets the calculator in decimal mode.
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.point")]
    public class PointAction : PluginBase
    {
        public PointAction(SDConnection connection, InitialPayload payload) : base(connection, payload) { }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override void KeyPressed(KeyPayload payload)
        {
            try
            {
                // check if decimal mode already enabled
                DataStorage.getInstance().readMemory("decimalMode");
                Connection.ShowAlert();
                return;
            }
            catch
            {
            }
            
            int currentNumber;
            try
            {
                currentNumber = int.Parse(DataStorage.getInstance().readMemory("currentNumber"));
            }
            catch
            {
            }
            DataStorage.getInstance().writeMemory("decimalMode", "1");
            Connection.ShowOk();
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload) { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }
    }
}