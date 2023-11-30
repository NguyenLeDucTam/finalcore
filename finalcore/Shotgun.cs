using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Threading;
using System.Diagnostics;

namespace finalcore
{
    public class Shotgun : DrawableGameComponent
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
        private Vector2 barrelBack;
        private Random random;
        private float range1;
        private float range;
        private Vector2 pellet = new Vector2(0, 0);
        private Vector2 origin;
        private float length;
        public Shotgun(Game game, SpriteBatch spriteBatch, SpriteFont defont, Vector2 position, int speed
            , Texture2D tex, Vector2 defpoint, GraphicsDeviceManager graphic, float gravity = 0) : base(game)
        {
            this.defont = defont;
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.stage = defpoint;
            this.defpoint = defpoint;
            this.tex = tex;
            this.speed = speed;
            this.graphic = graphic;
        }
        public override void Initialize()
        {
            list.Add(graphic.PreferredBackBufferWidth / 2); list.Add(graphic.PreferredBackBufferHeight + 100); list.Add(-50);
            barrelBack = defpoint;
            base.Initialize();
            random = new Random();
            range = graphic.PreferredBackBufferHeight * 1 / 8;
            range1 = random.Next(0, (int)range);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            length = graphic.PreferredBackBufferWidth * 2 / 10;
        }


        public override void Draw(GameTime gameTime)
        {
           
            Vector2 direction = position - stage;
            direction.Normalize();
            float targetX = stage.X + length * direction.X;
            float targetY =stage.Y + length*direction.Y;
            float milisec = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 pen1 = new Vector2(direction.Y , -direction.X);
            Vector2 pen2 = new Vector2(-direction.Y, direction.X);
            bool Distance = Vector2.Distance(stage, defpoint) > length;
            
            if (range1 % 2 == 0 )
            {
                pellet = new Vector2(targetX + range1 * pen1.X, targetY + range1 * pen1.Y);
            }
            else if (range1 %2 != 0 )
            {
                pellet = new Vector2(targetX + range1 * pen2.X, targetY + range1 * pen2.Y);
            }
            Vector2 pelletDir = pellet-stage;     
            pelletDir.Normalize();  
            Vector2 final = pelletDir*speed;
            if (position.X >= list[0] && defpoint.Y <= list[1] && defpoint.Y >= list[2])
            {
                defpoint += final * milisec;

            }
            
            else if (position.X < list[0] && defpoint.Y >= list[2] && defpoint.X > list[2])
            {

                defpoint += new Vector2(-Math.Abs(final.X), final.Y) * milisec;
            }
            
            
                spriteBatch.Begin(); 
            if (!Distance)
            {
                spriteBatch.Draw(tex, defpoint, null, Color.White, 0f, origin, 0.15f, SpriteEffects.None, 0);

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
    }
}
