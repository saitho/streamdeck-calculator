using BarRaider.SdTools;

/**
 * AddOperation Action
 * 
 * This action sets the current operation to "add" mode.
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.operationadd")]
    public class AddOperationAction : AbstractOperationAction
    {
        protected override string OperationChar => "+";
        protected override string BtnFilePath => @"images\keyPlus.png";

        public AddOperationAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
        }
    }
}