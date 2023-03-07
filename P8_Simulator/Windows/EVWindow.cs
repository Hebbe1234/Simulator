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
    internal class EVWindow : Window
    {

        private struct Line
        {
            public Vector2 start;
            public int length;
            public float angle;
        }

        private List<EV> _evs = new();
        private List<Line> lines = new();
        public EVWindow(Game game, int width, int height) : base(game, width, height)
        {
            gw.Title = "EVs";
        }

        public EVWindow(Game game, int width, int height, Color color) : base(game, width, height, color)
        {
            gw.Title = "EVs";
        }

        public void SetEvs(List<EV> evs, EVFactory.EvParams param)
        {
            _evs = evs;
            foreach (var (ev, i) in _evs.Select((e, i) => (e, i)))
            {
                lines.Add(new Line()
                {
                    start = new Vector2(
                        ev.Time.Start / (float)(param.TimeMax - param.TimeMin) * Width ,
                        (1 - ((i + 1) / (float)(_evs.Count + 1))) * Height
                        ),
                    length = ev.Time.End - ev.Time.Start,
                    angle = 0
                });

              
            }
        }

        public override void Draw(GraphicsDevice gd)
        {
            gd.SetRenderTarget(scrt);
            gd.Clear(Color);
            SpriteBatch sp = new SpriteBatch(gd);
            sp.Begin();
            foreach (var line in lines)
            {
                sp.DrawLine(line.start, line.length, line.angle, Color.Black, 2);
            }
            sp.End();
            scrt.Present();
            gd.SetRenderTarget(null);

        }
    }
}
