using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace project2006.Input
{


    internal class InputManager
    {
        private const int HOLD_INTV = 83; // 12次/秒
        private int lastUpdateTime;

        internal bool AcceptKeyInput;  //允许键盘
        internal bool AcceptMouseInput;  //允许鼠标移动和点击
        internal bool AcceptMouseButton;  //允许鼠标点击

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptInput">允许输入</param>
        internal InputManager(bool acceptInput = true)
        {
            AcceptKeyInput = acceptInput;
            AcceptMouseInput = acceptInput;
            AcceptMouseButton = acceptInput;
        }

        #region key/mouse hold状态

        //由2组函数组成下调key/mouse的相应方法
        private int getLastHold(Keys key)
        {
            return KeyHandler.GetLastHold(key);
        }

        //这里用int，避免三遍转换
        private int getLastHold(int mouse)
        {
            return MouseHandler.GetLastHold(mouse);
        }

        private void setLastHold(Keys key, int time)
        {
            KeyHandler.SetLastHold(key, time);
        }

        private void setLastHold(int mouse, int time)
        {
            MouseHandler.SetLastHold(mouse, time);
        }

        #endregion

        #region 鼠标相关

        private MouseState mouseState;
        private MouseState lastMouseState;

        /// <summary>
        /// 更新鼠标，中间会触发事件。在事件触发时，关于鼠标键的两个状态分别是上一个和本次，鼠标位置是新的
        /// 这里的鼠标hold数据存在array里，因为只有3个，不用字典
        /// </summary>
        private void UpdateMouse()
        {
            mouseState = Mouse.GetState();
            bool[] key = new bool[3];
            bool[] lastKey = new bool[3];
            lastKey[0] = MouseHandler.LastLeft;
            lastKey[1] = MouseHandler.LastMiddle;
            lastKey[2] = MouseHandler.LastRight;
            MouseHandler.LeftDown = (key[0] = mouseState.LeftButton == ButtonState.Pressed);
            MouseHandler.MiddleDown = (key[1] = mouseState.MiddleButton == ButtonState.Pressed);
            MouseHandler.RightDown = (key[2] = mouseState.RightButton == ButtonState.Pressed);
            MouseHandler.CurrentCursor.X = mouseState.X;
            MouseHandler.CurrentCursor.Y = mouseState.Y;
            MouseHandler.Wheel = mouseState.ScrollWheelValue;

            //只是不触发事件，但非要查到鼠标按键也可以通过MouseHandler的状态值观察
            if (AcceptMouseButton)
            {
                for (int i = 0; i < 3; i++)
                {
                    //down事件
                    if (!lastKey[i] && key[i])
                    {
                        MouseHandler.TriggerMouseDown((MouseButton)i);
                    }
                    //up事件,先up 再click
                    if (lastKey[i] && !key[i])
                    {
                        MouseHandler.TriggerMouseUp((MouseButton)i);
                        MouseHandler.TriggerClick((MouseButton)i);
                    }
                    //hold事件,这个事件不与mouse down up同时触发
                    if (lastKey[i] && key[i])
                    {
                        int last = getLastHold(i);
                        if (lastUpdateTime - last > HOLD_INTV)
                        {
                            setLastHold(i, lastUpdateTime);
                            MouseHandler.TriggerMouseHold((MouseButton)i);
                        }
                    }
                }
            }

            //wheeldown
            if (MouseHandler.Wheel != MouseHandler.LastWheel)
            {
                MouseHandler.TriggerWheel(MouseHandler.Wheel - MouseHandler.LastWheel);
            }

            MouseHandler.LastLeft = MouseHandler.LeftDown;
            MouseHandler.LastMiddle = MouseHandler.MiddleDown;
            MouseHandler.LastRight = MouseHandler.RightDown;
            MouseHandler.LastWheel = MouseHandler.Wheel;
            lastMouseState = mouseState;
        }
        #endregion

        #region 键盘相关
        private KeyboardState keyState;
        private KeyboardState lastKeyState;

        private void UpdateKey()
        {
            keyState = Keyboard.GetState();
            Keys[] keys = keyState.GetPressedKeys();
            KeyHandler.CtrlDown = false;
            KeyHandler.AltDown = false;
            KeyHandler.ShiftDown = false;
            if (keyState.IsKeyDown(Keys.LeftControl) || keyState.IsKeyDown(Keys.RightControl))
            {
                KeyHandler.CtrlDown = true;
            }
            if (keyState.IsKeyDown(Keys.LeftAlt) || keyState.IsKeyDown(Keys.RightAlt))
            {
                KeyHandler.AltDown = true;
            }
            if (keyState.IsKeyDown(Keys.LeftShift) || keyState.IsKeyDown(Keys.RightShift))
            {
                KeyHandler.ShiftDown = true;
            }
            //检测新按下和hold的键
            for (int i = 0; i < keys.Length; i++)
            {
                if (!lastKeyState.IsKeyDown(keys[i]))
                {
                    KeyHandler.TriggerKeyDown(keys[i]);
                }
                else
                {
                    int last = getLastHold(keys[i]);
                    if (lastUpdateTime - last > HOLD_INTV)
                    {
                        setLastHold(keys[i], lastUpdateTime);
                        KeyHandler.TriggerKeyHold(keys[i]);
                    }
                }
            }
            //检测up的键
            keys = lastKeyState.GetPressedKeys();
            for (int i = 0; i < keys.Length; i++)
            {
                if (!keyState.IsKeyDown(keys[i]))
                {
                    KeyHandler.TriggerKeyUp(keys[i]);
                    KeyHandler.TriggerKeyPress(keys[i]);
                }
            }

            lastKeyState = keyState;
        }
        #endregion

        internal void Update(int time)
        {
            lastUpdateTime = time;
            if (AcceptMouseInput)
            {
                UpdateMouse();
            }
            if (AcceptKeyInput)
            {
                UpdateKey();
            }

        }



    }
}
