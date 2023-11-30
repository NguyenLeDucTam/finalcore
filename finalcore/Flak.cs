using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D9;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace finalcore
{
    public class Flak : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont defont;
        private Vector2 position;
        private int speed;
        private Vector2 stage;
        private Vector2 defpoint;
        private Texture2D tex;
        private GraphicsDeviceManager graphic;
        private List<float> list = new List<float>();
        private float rotate = 0f;
        private Texture2D explosionSheet;
        private Rectangle[] sourceRectangles;
        private int frameWidth;
        private int frameHeight;
        TimeSpan animationInterval = TimeSpan.FromMilliseconds(50);
        TimeSpan lastFrameChangeTime;
        int currentFrame=49;
        private bool stop=true;
        private bool passed = false;
        private SoundEffect exp;
        public Flak(Game game, SpriteBatch spriteBatch, SpriteFont defont, Vector2 position, int speed
            , Texture2D tex,Texture2D explosionSheet, Vector2 defpoint, GraphicsDeviceManager graphic
            ,SoundEffect exp) : base(game)
        {
            this.defont = defont;
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.stage = defpoint;
            this.defpoint = defpoint;
            this.tex = tex;
            this.speed = speed;
            this.graphic = graphic;
            this.explosionSheet = explosionSheet;
            this.exp = exp;
            //6.73
        }
        public override void Initialize()
        {
            list.Add(graphic.PreferredBackBufferWidth / 2); list.Add(graphic.PreferredBackBufferHeight + 100); list.Add(-50);
            int numRows = 7;
            int numColumns = 7;
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


        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            Rectangle bullet = new Rectangle((int)defpoint.X,(int)defpoint.Y,1,1);
            Rectangle target = new Rectangle((int)(position.X-25), (int)(position.Y-25), 50,50);
            Vector2 origin = new Vector2(tex.Width/2, tex.Height/2);
            if (bullet.Intersects(target) ==false && passed==false && defpoint != stage)
            {
                if (position.X < stage.X)
                {
                    spriteBatch.Draw(tex, new Vector2(defpoint.X, defpoint.Y), null, Color.White, rotate, origin, 0.1f, SpriteEffects.None, 0);

                }
                else
                {
                    spriteBatch.Draw(tex, new Vector2(defpoint.X, defpoint.Y), null, Color.White, rotate, origin, 0.1f, SpriteEffects.FlipVertically, 0);

                }
            }
            
            else if (bullet.Intersects(target))
            {
                exp.Play(0.1f, 0f,0f);
                passed = true;
            }
            else if (passed==true && stop == true)
            {
                
                spriteBatch.Draw(explosionSheet, new Vector2(position.X - (sourceRectangles[currentFrame].Width / 2), position.Y - (sourceRectangles[currentFrame].Height / 2)), sourceRectangles[currentFrame], Color.White);
                    
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            
            Rectangle bullet = new Rectangle((int)defpoint.X, (int)defpoint.Y, 1, 1);
            Rectangle target = new Rectangle((int)(position.X - 25), (int)(position.Y - 25), 50, 50);
            /*Debug.WriteLine(bullet);
            Debug.WriteLine(target);*/
            
            if (gameTime.TotalGameTime - lastFrameChangeTime > animationInterval)
            {
                currentFrame--;
                lastFrameChangeTime = gameTime.TotalGameTime;

                if (currentFrame == 0)
                {
                    currentFrame = 49;
                    stop = false;
                    
                }
            }


            Vector2 direction = position - stage;
            direction.Normalize();
            Vector2 final = direction * speed;

            float milisec = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.X >= list[0] && defpoint.Y <= list[1] && defpoint.Y >= list[2])
            {
                defpoint += final * milisec;

            }
            else if (position.X < list[0] && defpoint.Y >= list[2] && defpoint.X > list[2])
            {

                defpoint += new Vector2(-Math.Abs(final.X), final.Y) * milisec;
            }

            rotate = (float)Math.Atan2(final.Y, final.X);
            base.Update(gameTime);
        }
    }
}
