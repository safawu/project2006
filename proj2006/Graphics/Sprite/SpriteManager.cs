using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project2006.Graphics.Sprite
{
    internal class SpriteManager:IDisposable
    {
        private SpriteBatch spriteBatch;
        private List<Sprite> spriteList;  //!!测试用的
        private Random RD; //!!测试用的

        internal SpriteManager(GraphicsDevice device)
        {
            spriteBatch = new SpriteBatch(device);
            spriteList = new List<Sprite>();
            RD = new Random(555);
        }

        public void Dispose()
        {
            spriteBatch.Dispose();
        }

        internal void AddSprite(Sprite sprite)
        {
            spriteList.Add(sprite);
        }

        internal void AddSprite(List<Sprite> sprites)
        {

        }

        internal void Draw(int time)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
            for (int i = 0; i < spriteList.Count; i++)
            {
                Sprite s=spriteList[i];
                if (s.IgnoreThis)
                {
                    continue;
                }
                spriteBatch.Draw(s.Texture, s.Position, null, s.Color, s.Rotation, s.OriginPosition, s.Scale, s.SpriteEffects, s.Depth);
            }
            spriteBatch.End();
        }
    }
}
