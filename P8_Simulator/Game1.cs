
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using P8_Simulator.Data;
using P8_Simulator.Model;
using P8_Simulator.Windows;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace P8_Simulator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PriceWindow pw;
        private CapacityWindow cw;
        private EVWindow ew;

        private List<EV> evs;
        private EVFactory evFactory;
        private EVFactory.EvParams param = new EVFactory.EvParams()
        {
            MaxCharge = 100,
            ChargeRateMin = 100,
            ChargeRateMax = 100,
            TimeMin = 0,
            TimeMax = 24 * 7
        };

        private bool _hasEvsChanged = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
           
            evFactory = new EVFactory(param);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pw = new PriceWindow(this, 400, 400);
            pw.Show();

            cw = new CapacityWindow(this, 1200, 800);
            cw.Show();

            ew = new EVWindow(this, 400, 400);
            //ew.Show();

            evs = evFactory.GetN(10);
            cw.SetEvs(evs, param);
            ew.SetEvs(evs, param);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (_hasEvsChanged)
            {

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            pw.Draw(GraphicsDevice);
            cw.Draw(GraphicsDevice);
            ew.Draw(GraphicsDevice);
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
    }
}