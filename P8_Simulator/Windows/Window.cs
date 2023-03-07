using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P8_Simulator.Windows
{
    internal abstract class Window
    {
        protected int Width;
        protected int Height;
        protected Game Game;

        protected GameWindow gw;
        protected SwapChainRenderTarget scrt;
        protected Color Color;

        protected Window(Game game, int width, int height, Color color) 
        {
            Game = game;
            Width = width;
            Height = height;
            Color = color;

            gw = GameWindow.Create(game, width, height);

            scrt = new SwapChainRenderTarget(game.GraphicsDevice,
                gw.Handle,
                gw.ClientBounds.Width,
                gw.ClientBounds.Height,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24Stencil8,
                1,
                RenderTargetUsage.PlatformContents,
                PresentInterval.Default);
        }

        protected Window(Game game, int width, int height) : this(game, width, height, Color.CornflowerBlue)
        {

        }
    

        public abstract void Draw(GraphicsDevice gd);

        public virtual void Show(int x = 100, int y = 100)
        {
            var window = Control.FromHandle(gw.Handle);
            window.Location = new System.Drawing.Point(x, y);
            window.Show();
        }

    }
}
