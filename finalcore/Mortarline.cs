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
    public class Mortarline : DrawableGameComponent
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
        public Mortarline(Game game, SpriteBatch spriteBatch, SpriteFont defont, Vector2 position, int speed
            , Texture2D tex, Texture2D explosonSheet, Vector2 defpoint, GraphicsDeviceManager graphic, float gravity, SoundEffect mortarFlying, SoundEffect mortarSound) : base(game)
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



        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            while (defpoint.Y < graphic.PreferredBackBufferHeight)
            {
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
                gravity += 350f;
                if ((final.Y + (gravity * milisec) * milisec) > 0)
                {

                    rotate = (float)Math.Atan2(final.Y, final.X);
                }
                else
                {
                    rotate = (float)Math.Atan2(final.Y, final.X);
                };

                if ((defpoint.X <= list[2] || defpoint.Y <= list[1]) && defpoint != stage)
                {
                    spriteBatch.DrawString(defont, ".", defpoint, Color.Black, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                }
                
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
