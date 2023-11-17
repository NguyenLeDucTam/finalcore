using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalcore
{
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
        public class Laser : DrawableGameComponent
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
            private float rotate = 0f;


            public Laser(Game game, SpriteBatch spriteBatch, SpriteFont defont, Vector2 position, int speed
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

                base.Initialize();
            }


            public override void Draw(GameTime gameTime)
            {

                spriteBatch.Begin();
                if (defpoint.X <= list[2] || defpoint.Y <= list[1])
                {
                    /*Debug.WriteLine("fied");*/
                    spriteBatch.Draw(tex, defpoint, null, Color.White, rotate, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                }


                spriteBatch.End();
                base.Draw(gameTime);
            }
            public override void Update(GameTime gameTime)
            {

                //stage(400,800) position(c,d)
                /*float m = (stage.Y - position.Y) / (stage.X - position.X);
                float n = stage.Y - stage.X * m;
                float y = (403 * m) + n;
                float x = (y - n) / m;*/
                Vector2 direction = position - stage;
                direction.Normalize();
                Vector2 final = direction * speed;

                float milisec = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (position.X >= list[0] && defpoint.Y <= list[1] && defpoint.Y >= list[2])
                {
                    /*                Debug.WriteLine(defpoint.Y);*/
                    defpoint += final * milisec;

                }
                else if (position.X < list[0] && defpoint.Y >= list[2] && defpoint.X > list[2])
                {

                    defpoint += new Vector2(-Math.Abs(final.X), final.Y) * milisec;
                    /*Debug.WriteLine(gravity);*/
                }
                rotate = (float)Math.Atan2(final.Y, final.X);
                base.Update(gameTime);
            }
        }
    }

}
