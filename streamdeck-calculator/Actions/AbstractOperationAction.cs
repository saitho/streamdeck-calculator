using System.Threading.Tasks;
using System.Drawing;
using BarRaider.SdTools;

namespace saitho.Calculator.Actions
{
    public abstract class AbstractOperationAction : AbstractStatefulAction
    {

        public AbstractOperationAction(SDConnection connection, InitialPayload payload) : base(connection, payload) { }
        protected abstract string OperationChar
        {
            get;
        }

        public override void KeyPressed(KeyPayload payload)
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"{OperationChar} operation pressed");
            Calculator.Instance.operation = OperationChar;
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        protected override Color? GetBorderColor()
        {
            if (OperationChar == Calculator.Instance.operation)
            {
                return Color.Red;
            }
            return null;
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload) { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }
    }
}
