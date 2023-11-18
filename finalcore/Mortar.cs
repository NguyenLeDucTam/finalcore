using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Threading;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Linq.Expressions;

namespace finalcore
{
    public class Mortar : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont defont;
        private Vector2 position;
        private int speed;
        private Vector2 stage;
        private Vector2 defpoint;
        private Texture2D tex;
        private GraphicsDeviceManager graphic;
        List<float> list = new List<float>();
        private float gravity = 4000f;
        private float rotate = 0f;
        private Texture2D explosionSheet;
        private Rectangle[] sourceRectangles;
        private int frameWidth;
        private int frameHeight;
        TimeSpan animationInterval = TimeSpan.FromMilliseconds(50);
        TimeSpan lastFrameChangeTime;
        int currentFrame = 0;
        private bool played=false;
        private float hit=950;
        private bool stop = false;
        private SoundEffect mortarFlying;
        private SoundEffect mortarSound;
        private SoundEffectInstance mortarInstance;
        private bool fired = false;
        private double oneSecAfterFiring=0.2;
        private bool mortarHit = false;
        private bool soManyBool = false;
        private Vector2 center;
        public Mortar(Game game, SpriteBatch spriteBatch, SpriteFont defont, Vector2 position, int speed
            , Texture2D tex,Texture2D explosonSheet, Vector2 defpoint, GraphicsDeviceManager graphic, float gravity, SoundEffect mortarFlying, SoundEffect mortarSound) : base(game)
        {
            this.defont = defont;
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.stage = defpoint;
            this.defpoint = new Vector2(defpoint.X - (tex.Height / 2) *0.3f, defpoint.Y);
            this.tex = tex;
            this.speed = speed;
            this.graphic = graphic;
            this.explosionSheet = explosonSheet;
            this.mortarSound = mortarSound;
            this.mortarFlying = mortarFlying;
        }
        public override void Initialize()
        {
            mortarInstance = mortarFlying.CreateInstance();
            list.Add(graphic.PreferredBackBufferWidth / 2); list.Add(graphic.PreferredBackBufferHeight + 100); list.Add(-50);
            int numRows = 5;
            int numColumns = 10;
            center = defpoint;
            frameWidth = explosionSheet.Width / numColumns;
            frameHeight = explosionSheet.Height / numRows;

            // Populate the array with source rectangles
            sourceRectangles = new Rectangle[numRows * numColumns];
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    int x = j * frameWidth;
                    int y = i * frameHeight;
                    sourceRectangles[i * numColumns + j] = new Rectangle(x, y, frameWidth, frameHeight);
                }
            }
            base.Initialize();
        }


        
        public override void Update(GameTime gameTime)
        {
            
            if ((gameTime.TotalGameTime - lastFrameChangeTime > animationInterval) && stop)
            {
                currentFrame++;
                lastFrameChangeTime = gameTime.TotalGameTime;

                if (currentFrame == 50)
                {
                    currentFrame = 0;
                    played = true;
                }
            }
            Vector2 direction = position - stage;
            direction.Normalize();
            Vector2 final = direction * 850;
            float milisec = (float)gameTime.ElapsedGameTime.TotalSeconds;
            final = new Vector2(final.X, final.Y + gravity * milisec);

            if (position.X >= list[0] && defpoint.Y <= list[1] && defpoint.Y >= list[2])
            {
                defpoint += new Vector2(final.X, final.Y + (gravity * milisec)) * milisec;

            }
            else if (position.X < list[0] && defpoint.Y >= list[2] && defpoint.X > list[2])
            {
                
                defpoint += new Vector2(-Math.Abs(final.X), final.Y + (gravity * milisec)) * milisec;
            }
            gravity += 400f;
            if ((final.Y + (gravity * milisec) * milisec) > 0)
            {
                
                rotate = (float)Math.Atan2(final.Y, final.X)*3f;
            }
            else
            {               
                rotate = (float)Math.Atan2(final.Y, final.X);
            }
            if (defpoint.Y >= graphic.PreferredBackBufferHeight && stop==false)
            {
                
                hit = defpoint.X;
                stop = true;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            if (fired)
            {
                mortarInstance.Play();
                fired = false;
            }
            oneSecAfterFiring -= gameTime.ElapsedGameTime.TotalSeconds;
            if (oneSecAfterFiring < 0 && !soManyBool)
            {
                fired = true;
                soManyBool = true;
            }
            spriteBatch.Begin();
            if ((defpoint.X <= list[2] || defpoint.Y <= list[1]) && defpoint!=center)
            {
                spriteBatch.Draw(tex, defpoint, null, Color.White, rotate, new Vector2(0, 0), 0.3f, SpriteEffects.None, 0);
                
            }
            if ((defpoint.Y >= graphic.PreferredBackBufferHeight)&& played==false)
            {
                if (!mortarHit)
                {
                    mortarInstance.Stop();
                    mortarSound.Play();
                    mortarHit = true;
                }
                if (list[0]>position.X)
                {
                    spriteBatch.Draw(explosionSheet, new Vector2(hit - sourceRectangles[currentFrame].Width*1.5f, graphic.PreferredBackBufferHeight - sourceRectangles[currentFrame].Height * 1.8f), sourceRectangles[currentFrame], Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);

                }
                else if (list[0]<position.X)
                {
                    spriteBatch.Draw(explosionSheet, new Vector2(hit - sourceRectangles[currentFrame].Width / 2, graphic.PreferredBackBufferHeight - sourceRectangles[currentFrame].Height * 1.8f), sourceRectangles[currentFrame], Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0);

                }

            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
