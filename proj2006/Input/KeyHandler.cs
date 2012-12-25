using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace project2006.Input
{
    /// <summary>
    /// 键盘事件参数
    /// </summary>
    internal class KeyEventArgs : EventArgs
    {
        internal Keys Key;
        internal KeyEventArgs(Keys key)
        {
            this.Key = key;
        }
    }

    internal delegate void KeyEventHandler(object sender, KeyEventArgs e);

    internal static class KeyHandler
    {
        private static Dictionary<Keys, int> lastHold=new Dictionary<Keys, int>(84);//84键键盘

        internal static bool CtrlDown;
        internal static bool AltDown;
        internal static bool ShiftDown;

        internal static event KeyEventHandler KeyDownHandler;
        internal static event KeyEventHandler KeyUpHandler;
        internal static event KeyEventHandler KeyPressHandler;
        internal static event KeyEventHandler KeyHoldHandler;

        #region last state相关
        internal static int GetLastHold(Keys key)
        {
            int time;
            if (lastHold.TryGetValue(key, out time))
            {
                return time;
            }
            return 0;
        }

        internal static void SetLastHold(Keys key, int time)
        {
            lastHold[key] = time;
        }

#endregion

        #region 事件触发

        internal static void TriggerKeyDown(Keys key)
        {
            if (KeyDownHandler != null)
            {
                KeyDownHandler(null, new KeyEventArgs(key));
            }
        }

        internal static void TriggerKeyUp(Keys key)
        {
            if (KeyUpHandler != null)
            {
                KeyUpHandler(null, new KeyEventArgs(key));
            }
        }

        internal static void TriggerKeyPress(Keys key)
        {
            if (KeyPressHandler != null)
            {
                KeyPressHandler(null, new KeyEventArgs(key));
            }
        }

        internal static void TriggerKeyHold(Keys key)
        {
            if (KeyHoldHandler != null)
            {
                KeyHoldHandler(null, new KeyEventArgs(key));
            }
        }
        #endregion

        /// <summary>
        /// 恢复初始状态
        /// <param name="clearHandler">清空Handler</param>
        /// </summary>
        internal static void Reset(bool clearHandler)
        {
            if (clearHandler)
            {
                KeyDownHandler = null;
                KeyUpHandler = null;
                KeyHoldHandler = null;
                KeyPressHandler = null;
            }
            CtrlDown = false;
            AltDown = false;
            ShiftDown = false;
        }
    }
}
