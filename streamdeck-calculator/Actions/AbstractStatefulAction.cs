using System.Threading.Tasks;
using System.Drawing;
using BarRaider.SdTools;

namespace saitho.Calculator.Actions
{
    public abstract class AbstractStatefulAction : PluginBase
    {
        #region Private Members
        const int SCENE_BORDER_SIZE = 20;
        #endregion

        protected abstract string BtnFilePath
        {
            get;
        }

        public AbstractStatefulAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        private async Task DrawSceneBorder()
        {
            using Image img = Tools.GenerateGenericKeyImage(out Graphics graphics);
            int height = img.Height;
            int width = img.Width;
            graphics.PageUnit = GraphicsUnit.Pixel;

            // Draw originalimage
            if (BtnFilePath != null)
            {
                graphics.DrawImage(Image.FromFile(BtnFilePath), new Rectangle(0, 0, width, height));
            }

            try
            {
                Color? color = GetBorderColor();
                if (color != null)
                {
                    // Draw border
                    graphics.DrawRectangle(new Pen((Color)color, SCENE_BORDER_SIZE), new Rectangle(0, 0, width, height));
                }
            }
            catch
            {
                // ignore errors as we just don't draw the border then
            }

            await Connection.SetImageAsync(img);
            graphics.Dispose();
        }

        protected abstract Color? GetBorderColor();

        public override void OnTick() {
            _ = DrawSceneBorder();
        }
    }
}
