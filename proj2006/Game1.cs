using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using project2006.Audio;
using project2006.Graphics.Sprite;
using project2006.IO;
using System.Drawing.Text;
using project2006.Input;

namespace project2006
{
    /// <summary>
    /// 这个暂时用来测试各个模块是否正常功能，可以拿来当单元测试用。
    /// 下面非常乱是吧
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteManager SM;
        internal static IntPtr WindowHandle;
        SpriteBatch SB;
        SpriteFontX SFX;
        InputManager IM;
        //以上是错误的变量命名示范
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;
        Random RD;
        int Time;
        double ActualTime;

        public Game1()
        {
            WindowHandle = this.Window.Handle;
            //     this.IsMouseVisible = true;
            this.IsFixedTimeStep = false;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.SynchronizeWithVerticalRetrace = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SM = new SpriteManager(this.GraphicsDevice);
            SB = new SpriteBatch(this.GraphicsDevice);
            SFX = new SpriteFontX(new System.Drawing.Font("Tahoma", 15), this.graphics, TextRenderingHint.AntiAlias);
            IM = new InputManager();
            RD = new Random(550);
            MouseHandler.WheelHandler += MouseDown;
            KeyHandler.KeyHoldHandler += KeyDown;
            base.Initialize();
        }

        Sprite cursor;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Texture2D t = Texture2D.FromStream(this.GraphicsDevice, RWExternal.GetFileStream("test.png"));
            /*    wSprite s;
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        s = new wSprite(t, new Vector2(i * 12, j * 7));
                        s.Color = new Color(100 + i, 50 + i, 150 + j);
                        s.Depth = (float)RD.NextDouble();
                        SM.AddSprite(s);
                    }
                }*/
            cursor = new Sprite(t, Vector2.Zero);
            cursor.OriginType = OriginType.Centre;
            cursor.Position = new Vector2(500, 200);
            SM.AddSprite(cursor);
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            cursor.Position.Y += 10 * (e.WheelDelta / 120);
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Keys.A)
                cursor.Position.X -= 3;
            else if (e.Key == Keys.D)
                cursor.Position.X += 3;
            else if (e.Key == Keys.W)
                cursor.Position.Y -= 3;
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            ActualTime = gameTime.TotalGameTime.TotalMilliseconds;
            Time = (int)ActualTime;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
            IM.Update(Time);
            //    cursor.Position = new Vector2(MouseHandler.CurrentCursor.X, MouseHandler.CurrentCursor.Y);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            frameCounter++;
            GraphicsDevice.Clear(Color.Black);
            SM.Draw(Time);
            SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            SB.DrawStringX(SFX, string.Format("fps:{0} Cursor:({1}:{2})", frameRate, MouseHandler.CurrentCursor.X, MouseHandler.CurrentCursor.Y), Vector2.Zero, Color.Red);
            SB.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
