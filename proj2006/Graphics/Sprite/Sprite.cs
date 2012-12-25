using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace project2006.Graphics.Sprite
{
    internal class Sprite : IDisposable
    {
        private Texture2D texture;
        private OriginType originType;
        private Vector2 originPosition;

        internal Vector2 Position;
        internal int Width;
        internal int Height;
        internal float Rotation;
        internal Vector2 Scale;
        internal float Alpha;
        internal Color Color;
        internal float Depth;
        internal SpriteEffects SpriteEffects;
        internal bool IgnoreThis; //忽略这个sprite，不做任何处理
        internal object Tag;

        internal Sprite(Texture2D texture, Vector2 position)
            : this(texture, position, OriginType.TopLeft, Color.White, 1, null)
        {
        }

        internal Sprite(Texture2D texture, Vector2 position, float depth)
            : this(texture, position, OriginType.TopLeft, Color.White, depth, null)
        {
        }

        internal Sprite(Texture2D texture, Vector2 position, Color color, float depth)
            : this(texture, position, OriginType.TopLeft, color, depth, null)
        {
        }

        internal Sprite(Texture2D texture, Vector2 position, OriginType originType, Color color, float depth, object tag)
        {
            this.Texture = texture;
            this.Position = position;
            this.Color = color;
            this.Depth = depth;
            this.Tag = tag;
            this.OriginType = originType;
            this.Scale = new Vector2(1,1);
            this.Rotation = 0;
            this.Alpha = 1;
        }

        public virtual void Dispose()
        {
            if (texture != null)
            {
                texture.Dispose();
            }
        }

        internal Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                if (texture == value)
                {
                    return;
                }
                if (texture != null)
                {
                    texture.Dispose();
                }
                texture = value;
                if (texture != null)
                {
                    Width = texture.Width;
                    Height = texture.Height;
                }
                else
                {
                    Width = 0;
                    Height = 0;
                }
            }
        }

        internal OriginType OriginType
        {
            get
            {
                return originType;
            }
            set
            {
                originType = value;
                switch (originType)
                {
                    case OriginType.TopLeft:
                        originPosition = Vector2.Zero;
                        break;
                    case OriginType.TopCentre:
                        originPosition = new Vector2(Width / 2, 0);
                        break;
                    case OriginType.TopRight:
                        originPosition = new Vector2(Width, 0);
                        break;
                    case OriginType.Centre:
                        originPosition = new Vector2(Width / 2, Height / 2);
                        break;
                    case OriginType.CentreLeft:
                        originPosition = new Vector2(0, Height / 2);
                        break;
                    case OriginType.CentreRight:
                        originPosition = new Vector2(Width, Height / 2);
                        break;
                    case OriginType.BottomLeft:
                        originPosition = new Vector2(0, Height);
                        break;
                    case OriginType.BottomCentre:
                        originPosition = new Vector2(Width / 2, Height);
                        break;
                    case OriginType.BottomRight:
                        originPosition = new Vector2(Width, Height);
                        break;
                }
            }
        }

        internal Vector2 OriginPosition
        {
            get
            {
                return originPosition;
            }
        }

        #region 动画快捷函数
        internal void ToggleShow(bool show)
        {
            if (show)
            {
                Alpha = 1;
            }
            else
            {
                Alpha = 0;
            }
        }


        #endregion
    }
}
