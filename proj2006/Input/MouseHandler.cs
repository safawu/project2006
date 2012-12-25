using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace project2006.Input
{
    /// <summary>
    /// 鼠标事件参数
    /// </summary>
    internal class MouseEventArgs : EventArgs
    {
        internal MouseButton Button;
        internal Point CursorPos;
        internal int WheelDelta;
        internal MouseEventArgs(MouseButton mb, Point cursor, int delta = 0)
        {
            Button = mb;
            CursorPos = cursor;
            WheelDelta = delta;
        }
    }

    internal delegate void MouseEventHandler(object sender, MouseEventArgs e);

    internal static class MouseHandler
    {
        private static int[] lastHold = new int[3];

        internal static Point CurrentCursor = new Point(0, 0);
        internal static bool LeftDown;
        internal static bool RightDown;
        internal static bool MiddleDown;
        internal static bool LastLeft;
        internal static bool LastRight;
        internal static bool LastMiddle;
        internal static int Wheel;
        internal static int LastWheel;

        internal static event MouseEventHandler ClickHandler;
        internal static event MouseEventHandler MouseDownHandler;
        internal static event MouseEventHandler MouseUpHandler;
        internal static event MouseEventHandler MouseHoldHandler;
        internal static event MouseEventHandler WheelHandler;

        /// <summary>
        /// 鼠标按键状态是否变化，注意这里会默认三个Down变量是最新值，和三个Last比较
        /// </summary>
        /// <returns></returns>
        internal static bool Changed()
        {
            if (LeftDown != LastLeft || RightDown != LastRight || MiddleDown != LastMiddle)
            {
                return true;
            }
            return false;
        }
        #region 相应hold数据

        internal static int GetLastHold(int mouseButton)
        {
            if (mouseButton < 0 || mouseButton > 2)
            {
                return 0;
            }
            return lastHold[mouseButton];
        }

        internal static void SetLastHold(int mouseButton, int time)
        {
            if (mouseButton < 0 || mouseButton > 2)
            {
                return;
            }
            lastHold[mouseButton] = time;
        }
#endregion

        #region 事件触发
        /// <summary>
        /// 按住鼠标键触发,可以当作drag事件
        /// </summary>
        /// <param name="mb"></param>
        internal static void TriggerMouseHold(MouseButton mb)
        {
            if (MouseHoldHandler != null)
            {
                MouseHoldHandler(null, new MouseEventArgs(mb, CurrentCursor));
            }
        }

        internal static void TriggerMouseDown(MouseButton mb)
        {
            if (MouseDownHandler != null)
            {
                MouseDownHandler(null, new MouseEventArgs(mb, CurrentCursor));
            }
        }

        internal static void TriggerMouseUp(MouseButton mb)
        {
            if (MouseUpHandler != null)
            {
                MouseUpHandler(null, new MouseEventArgs(mb, CurrentCursor));
            }
        }

        internal static void TriggerClick(MouseButton mb)
        {
            if (ClickHandler != null)
            {
                ClickHandler(null, new MouseEventArgs(mb, CurrentCursor));
            }
        }

        internal static void TriggerWheel(int delta)
        {
            if (WheelHandler != null)
            {
                WheelHandler(null, new MouseEventArgs(MouseButton.None, CurrentCursor, delta));
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
                ClickHandler = null;
                MouseDownHandler = null;
                MouseUpHandler = null;
                MouseHoldHandler = null;
                WheelHandler = null;
            }
            CurrentCursor = new Point(0, 0);
            LeftDown = false;
            RightDown = false;
            MiddleDown = false;
            LastLeft = false;
            LastRight = false;
            LastMiddle = false;
            Wheel = 0;
            LastWheel = 0;
        }
    }
}
