using BarRaider.SdTools;

/**
 * SubtractOperation Action
 * 
 * This action sets the current operation to "subtract" mode.
 **/
namespace saitho.Calculator.Actions
{
    [PluginActionId("com.saitho.calculator.operationsubtract")]
    public class SubtractOperationAction : AbstractOperationAction
    {
        protected override string OperationChar => "-";
        protected override string BtnFilePath => @"images\keyMinus.png";

        public SubtractOperationAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
        }
    }
}