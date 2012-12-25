using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project2006
{
    internal class GameMain:Game
    {
        internal static GameMain Instance;

        
        internal GameMain()
        {
            Graphics = new GraphicsDeviceManager(this); 
            Instance = this;
            WindowHandle = this.Window.Handle;
        }

        protected override void Initialize()
        {
            Device = Graphics.GraphicsDevice;
            if (Device == null)
                throw new Exception("Failed to create graphics device!");
            base.Initialize();
        }

        #region 窗口相关
        internal IntPtr WindowHandle;
        #endregion

        #region 基础图形相关
        internal GraphicsDeviceManager Graphics;
        internal GraphicsDevice Device;
        #endregion
    }
}
