using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace finalcore
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont defont;
        private Bullet bullet;
        private Vector2 defpoint;
        private Texture2D bulletTex;
        private double fireRate = 0.1;
        private int speed;
        private double pasTime;
        private SoundEffect pew;
        private float gravity;

        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1900;
            _graphics.PreferredBackBufferHeight = 900;
            defpoint = new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight*90/100);
            speed = 700;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gravity = 8000;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            defont = this.Content.Load<SpriteFont>("default");
            bulletTex = this.Content.Load<Texture2D>("icons8-bullet-24");
            pew = this.Content.Load<SoundEffect>("pew-sound-effect_xRLQEfgl");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalSeconds;
            string basetext = "base is here";
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            MouseState mouseState = Mouse.GetState();
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                _spriteBatch.DrawString(defont, "gunpoint", new Vector2(mouseState.X, mouseState.Y), Color.Black);
                if (mouseState.LeftButton == ButtonState.Pressed && pasTime<time)
                {
                    pew.Play(volume:0.3f, pitch: 0.0f, pan: 0.0f);
                    bullet = new Bullet(this, _spriteBatch, defont,new Vector2(mouseState.X,mouseState.Y),speed,bulletTex,defpoint,_graphics, gravity);
                    this.Components.Add(bullet);
                    
                    pasTime = time + fireRate;
                }
            }
            _spriteBatch.DrawString(defont, basetext, new Vector2(_graphics.PreferredBackBufferWidth/2 - (defont.MeasureString(basetext).X/2), (_graphics.PreferredBackBufferHeight) - defont.MeasureString(basetext).Y), Color.Black);
            _spriteBatch.DrawString(defont, "practice", new Vector2(625,260), Color.Black);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }   
    }
}