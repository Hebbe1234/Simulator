using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using P8_Simulator.Data;
using SharpDX.Direct3D9;
using SharpDX.DirectWrite;
using Color = Microsoft.Xna.Framework.Color;
using PrimitiveType = Microsoft.Xna.Framework.Graphics.PrimitiveType;

namespace P8_Simulator.Windows
{
    internal class PriceWindow : Window
    {
        private const int HOURS_PER_DAY = 24;
        private const int DAYS_TO_SHOW = 7;

        private readonly Graph<SpotPrice> _graph;

        public PriceWindow(Game game, int width, int height) : base(game, width, height)
        {
            gw.Title = "Prices";
            var spotPriceManager = new SpotPriceManager("Content/Elspotprices.json");

            const int n = HOURS_PER_DAY*DAYS_TO_SHOW;
            var maxPrice = spotPriceManager.Max(n);

            var prices = spotPriceManager.GetN(n);
            _graph = new Graph<SpotPrice>(prices.ToList(), (item) => (float)(item.SpotPriceDKK / maxPrice), width, height, Color.AliceBlue);
        }

        public override void Draw(GraphicsDevice gd)
        {
            gd.SetRenderTarget(scrt);
            gd.Clear(Color);

            _graph.Draw(gd);
           
            scrt.Present();
            gd.SetRenderTarget(null);

        }


    }

}
