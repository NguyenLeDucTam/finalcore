using finalcore.finalcore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;
using System;
using System.Diagnostics;
using System.Security.Permissions;
using static System.Net.Mime.MediaTypeNames;


namespace finalcore
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        private SpriteFont defont;
        private Bullet bullet;
        private Vector2 defpoint;
        private Texture2D bulletTex;
        private double fireRate = 0.1;
        private int speedBullet;
        private int speed=2000;
        private int speedFlak;
        private int speedMortar;
        private double pasTime;
        private SoundEffect pew;
        private float gravity;
        private Texture2D flaxExp;
        private Flak flak;
        private SoundEffect Flak;
        private SoundEffect FlakExp;
        private int weapon=1;
        private Laser laser;
        private Mortar mortar;
        private Texture2D mortarTex;
        private Texture2D flakTex;
        private Texture2D mortarExp;
        private SoundEffect Mortar;
        private SoundEffect mortarFlying;
        private SoundEffect mortarSound;
        private Mortarline mortarline;
        private Shotgun shotgun;
        private Texture2D shotgunBarrel;
        private Texture2D shotgunBullet;

        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1900;
            _graphics.PreferredBackBufferHeight = 900;
            defpoint = new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight*90/100);
            speedBullet = 800;
            speedFlak = 800;
            speedMortar = 800;
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
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            defont = this.Content.Load<SpriteFont>("default");
            bulletTex = this.Content.Load<Texture2D>("bullet (1)");
            flakTex = this.Content.Load<Texture2D>("bullet");
            mortarExp = this.Content.Load<Texture2D>("explosion1");
            pew = this.Content.Load<SoundEffect>("pew-sound-effect_xRLQEfgl");
            Mortar = this.Content.Load<SoundEffect>("RimWorld by Ludeon Studios 2023-11-17 23-03-44");
            flaxExp = this.Content.Load<Texture2D>("Smoke45Frames");
            shotgunBullet = this.Content.Load<Texture2D>("icons8-circle-48");
            shotgunBarrel = this.Content.Load<Texture2D>("PngItem_761249");
            mortarTex = this.Content.Load<Texture2D>("artillery-icon-22");
            Flak = this.Content.Load<SoundEffect>("Guns- Artillery Sound EffectMexian Tech Vlog-[AudioTrimmer (mp3cut.net)");
            FlakExp = this.Content.Load<SoundEffect>("a_5Y4GyBqs");
            mortarFlying = this.Content.Load<SoundEffect>("RimWorld by Ludeon Studios 2023-11-17 23-03-44_2");
            mortarSound = this.Content.Load<SoundEffect>("RimWorld by Ludeon Studios 2023-11-17 23-03-44_4");
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
            MouseState mouseState = Mouse.GetState();
            KeyboardState key = Keyboard.GetState();    
            double time = gameTime.TotalGameTime.TotalSeconds;
            string basetext = "base";
            Vector2 direction = new Vector2(mouseState.X-defpoint.X, mouseState.Y-defpoint.Y);
            float rotate = (float)Math.Atan2(direction.Y, direction.X);
            float lenth = (float)Math.Sqrt(shotgunBarrel.Width*shotgunBarrel.Width +
                shotgunBarrel.Height*shotgunBarrel.Height);
            Vector2 textureSize = new Vector2(shotgunBarrel.Width, shotgunBarrel.Height);
            float offsetX = (float)Math.Cos(rotate) * textureSize.X;
            float offsetY = (float)Math.Sin(rotate) * textureSize.X;

            // Calculate the location of the other end of the texture
            Vector2 otherEnd = new Vector2(defpoint.X + offsetX, defpoint.Y + offsetY);
            Vector2 afterDef = new Vector2(defpoint.X + (float)Math.Cos(rotate) * lenth,
                (float)Math.Abs(defpoint.Y + (float)Math.Sin(rotate) * lenth));
            GraphicsDevice.Clear(Color.DarkGray);
            _spriteBatch.Begin();
            
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                _spriteBatch.DrawString(defont, "gunpoint", new Vector2(mouseState.X, mouseState.Y), Color.Black);
                if (weapon == 4)
                {
                    mortarline = new Mortarline(this, _spriteBatch, defont, new Vector2(mouseState.X, mouseState.Y), speed, mortarTex, mortarExp, defpoint, _graphics, gravity, mortarFlying, mortarSound);
                    this.Components.Add(mortarline);

                }
                
            }
            if (mouseState.LeftButton == ButtonState.Pressed )
            {
               
                if (weapon == 1 && pasTime < time)
                {
                    bullet = new Bullet(this, _spriteBatch, defont, new Vector2(mouseState.X, mouseState.Y), speed, bulletTex, defpoint, _graphics, gravity);
                    this.Components.Add(bullet);
                    pasTime = time + fireRate;
                    pew.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);

                }
                if (weapon == 2 && pasTime < time)
                {
                    flak = new Flak(this, _spriteBatch, defont, new Vector2(mouseState.X, mouseState.Y),
                        speed, flakTex, flaxExp, defpoint, _graphics, FlakExp);
                    this.Components.Add(flak);
                    pasTime = time + fireRate;
                    Flak.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                }
                if (weapon == 3 && pasTime < time)
                {
                    laser = new Laser(this, _spriteBatch, defont, new Vector2(mouseState.X, mouseState.Y), speed, bulletTex, defpoint, _graphics, gravity);
                    this.Components.Add(laser);
                    pasTime = time + fireRate;
                    pew.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);

                }
                if (weapon == 4 && pasTime < time)
                {
                    
                    mortar = new Mortar(this, _spriteBatch, defont, new Vector2(mouseState.X, mouseState.Y), speed, mortarTex,mortarExp, defpoint, _graphics, gravity, mortarFlying,mortarSound);
                    this.Components.Add(mortar);
                    pasTime = time + fireRate;
                    Mortar.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);

                }
                if ( weapon == 5)
                {
                    if ( pasTime < time )
                    {
                        Debug.WriteLine(defpoint);
                        Debug.WriteLine(afterDef);
                        shotgun = new Shotgun(this, _spriteBatch, defont, new Vector2(mouseState.X, mouseState.Y), speed, shotgunBullet, defpoint, _graphics, gravity);
                        this.Components.Add(shotgun);
                        pasTime = time + fireRate;
                    }                   
                    

                }
            }
            if (weapon ==5) {
                if (mouseState.X < defpoint.X) {
                    _spriteBatch.Draw(shotgunBarrel, defpoint, null, Color.White, rotate, new Vector2(0, 0), 0.07f, SpriteEffects.FlipVertically, 0);
                }
                else
                {
                    _spriteBatch.Draw(shotgunBarrel, defpoint, null, Color.White, rotate, new Vector2(0, 0), 0.07f, SpriteEffects.None, 0);

                }
            }
            if (key.IsKeyDown(Keys.D1))
            {
                weapon = 1;
            }
            if (key.IsKeyDown(Keys.D2))
            {
                weapon = 2;
            }
            if (key.IsKeyDown(Keys.D3))
            {
                weapon = 3;
            }
            if (key.IsKeyDown(Keys.D4))
            {
                weapon = 4;
            }
            if (key.IsKeyDown(Keys.D5))
            {
                weapon = 5;
            }
            _spriteBatch.DrawString(defont, basetext, new Vector2(_graphics.PreferredBackBufferWidth/2 - (defont.MeasureString(basetext).X/2), (_graphics.PreferredBackBufferHeight) - defont.MeasureString(basetext).Y), Color.Black);
            _spriteBatch.DrawString(defont, weapon.ToString(), new Vector2(_graphics.PreferredBackBufferWidth/2,50), Color.Black);
            _spriteBatch.DrawString(defont, ".", defpoint, Color.Black);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}