using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalcore
{
    public class CollisonManager : DrawableGameComponent
    {
        private Shotgun shotgun;
        private Rectangle shotgunBarrel;
        private SoundEffect hitSound;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;


        public CollisonManager(Game game,
            Shotgun shotgun,
            Rectangle shotgunBarrel) : base(game)
        {
            this.shotgun = shotgun;
            this.shotgunBarrel = shotgunBarrel;
        }
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
    }
}
