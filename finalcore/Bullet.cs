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
    public class Bullet : DrawableGameComponent
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
        private float gravity=1500f;
        private float rotate = 0f;
        
        
        public Bullet(Game game, SpriteBatch spriteBatch, SpriteFont defont, Vector2 position, int speed
            , Texture2D tex, Vector2 defpoint, GraphicsDeviceManager graphic, float gravity) : base(game)
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
            
            base.Initialize();
        }


        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            if ((defpoint.X <= list[2] || defpoint.Y <= list[1]) && defpoint != stage)
            {
                spriteBatch.Draw(tex, defpoint,null,Color.White,rotate,new Vector2(0,0),0.07f,SpriteEffects.None,0);
            }
            

            spriteBatch.End();
            base.Draw(gameTime); 
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 direction = position - stage;
            direction.Normalize();
            Vector2 final = direction * speed;

            float milisec = (float)gameTime.ElapsedGameTime.TotalSeconds;
            final = new Vector2(final.X, final.Y + gravity * milisec);
           
            if (position.X >= list[0] && defpoint.Y <= list[1] && defpoint.Y >= list[2])
            {
                defpoint += final*milisec;
                
            }
            else if (position.X < list[0] && defpoint.Y >= list[2] && defpoint.X > list[2])
            {
                
                defpoint += new Vector2(-Math.Abs(final.X), final.Y + (gravity * milisec)) *milisec;
            }
            if (defpoint.X>position.X && position.X >= list[0])
            {
                gravity = gravity + 400f;
            }
            else if (defpoint.X < position.X && position.X < list[0])
            {
                gravity = gravity + 400f;
            }
            rotate = (float)Math.Atan2(final.Y, final.X);
            base.Update(gameTime); 
        }
    }
}
