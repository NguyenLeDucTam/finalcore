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
        private float gravity = 1500f;
        private float rotate = 0f;
        private Vector2 barrelBack;

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
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if ((defpoint.X <= list[2] || defpoint.Y <= list[1]) && defpoint != stage)
            {
                spriteBatch.Draw(tex, defpoint, null, Color.White, rotate, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 direction = position - barrelBack;
            direction.Normalize();
            Vector2 final = direction * speed;

            float milisec = (float)gameTime.ElapsedGameTime.TotalSeconds;
            final = new Vector2(final.X, final.Y + gravity * milisec);

            if (position.X >= list[0] && defpoint.Y <= list[1] && defpoint.Y >= list[2])
            {
                defpoint += final * milisec;

            }
            else if (position.X < list[0] && defpoint.Y >= list[2] && defpoint.X > list[2])
            {

                defpoint += new Vector2(-Math.Abs(final.X), final.Y + (gravity * milisec)) * milisec;
            }
            if (defpoint.X > position.X && position.X >= list[0])
            {
                gravity = gravity + 400f;
            }
            else if (defpoint.X < position.X && position.X < list[0])
            {
                gravity = gravity + 400f;
            }
            rotate = (float)Math.Atan2(direction.Y, direction.X);
            base.Update(gameTime);
        }
    }
}
