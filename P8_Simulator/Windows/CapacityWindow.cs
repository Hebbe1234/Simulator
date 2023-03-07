using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using P8_Simulator.Data;
using P8_Simulator.Model;
using SharpDX.Direct3D9;

namespace P8_Simulator.Windows
{
    internal class CapacityWindow : Window
    {
        private struct Line
        {
            public Vector2 start;
            public int length;
            public float angle;
            public Color color;
        }
        private List<Line> lines = new();

        private Graph<int> _graph;
        public CapacityWindow(Game game, int width, int height) : base(game, width, height)
        {
            gw.Title = "Capacity";
        }

        public CapacityWindow(Game game, int width, int height, Color color) : base(game, width, height, color)
        {
            gw.Title = "Capacity";
        }

        public void SetEvs(List<EV> evs, EVFactory.EvParams param)
        {
            var totalCapacity = 
                Enumerable.Range(param.TimeMin, param.TimeMax - param.TimeMin).Select(t => evs
                    .Where(e => e.Time.Start <= t && e.Time.End >= t)
                    .Sum(e => e.Charges.Max - e.Charges.Current)).ToList();
            
            _graph = new Graph<int>(totalCapacity, (item) => (float)(item) / totalCapacity.Max(), Width, Height, Color.AliceBlue);

            var paramTime = (param.TimeMax - param.TimeMin);

            var rand = new Random();
            foreach (var (ev, i) in evs.OrderBy(e => e.Time.Start).Select((e, i) => (e, i)))
            {
                var col = new Color(rand.NextSingle(), rand.NextSingle(), rand.NextSingle());
                lines.Add(new Line()
                {
                    start = new Vector2(
                        ev.Time.Start / (float)paramTime * Width,
                        ((1 - ((ev.Charges.Max - ev.Charges.Current) / (float)totalCapacity.Max())) * Height)
                    ),
                    length = (int) ((ev.Time.End / (float)paramTime) * Width - (ev.Time.Start / (float)paramTime) * Width),
                    color = col
                });

                lines.Add(new Line()
                {
                    start = new Vector2(
                        ev.Time.Start / (float)paramTime * Width,
                        (1 - ((float)totalCapacity[ev.Time.Start] / totalCapacity.Max())) * Height
                    ),
                    length = Height - (int)((1 - ((float)totalCapacity[ev.Time.Start] / totalCapacity.Max())) * Height),
                    angle = (float)((Math.PI) / 2),
                    color = col

                });
                lines.Add(new Line()
                {
                    start = new Vector2(
                        ev.Time.End / (float)paramTime * Width,
                        (1 - ((float)totalCapacity[ev.Time.End] / totalCapacity.Max())) * Height
                    ),
                    length = Height - (int)((1 - ((float)totalCapacity[ev.Time.End] / totalCapacity.Max())) * Height),
                    angle = (float)((Math.PI) / 2),
                    color = col

                });
            }

        }

        public override void Draw(GraphicsDevice gd)
        {

            gd.SetRenderTarget(scrt);
            gd.Clear(Color);
            _graph?.Draw(gd);
            SpriteBatch sp = new SpriteBatch(gd);
            sp.Begin();
            foreach (var line in lines)
            {
                sp.DrawLine(line.start, line.length, line.angle, line.color  , 2);
            }
            sp.End();
            scrt.Present();
            gd.SetRenderTarget(null);
        }
    }
}
