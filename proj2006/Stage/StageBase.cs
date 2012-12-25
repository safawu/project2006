using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project2006.Stage
{
    internal class StageBase : DrawableGameComponent
    {
        public StageBase(Game game):base(game)
        {

        }

        protected override void Dispose(bool disposing) { }

        public override void Draw(GameTime gameTime) { }

        public override void Initialize() { }

        public override void Update(GameTime gameTime)
        {
            
        }
        #region 切换相关
        internal bool FadeReady;
        internal StageType NextStage;

        #endregion
    }
}
