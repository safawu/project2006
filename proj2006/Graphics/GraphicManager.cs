using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using project2006.IO;

namespace project2006.Graphics
{
    /// <summary>
    /// 这个类负责调用IO，提供实质内容
    /// </summary>
    internal static class GraphicManager
    {
        private static Dictionary<string, Texture2D> internalRes;//系统自带资源
        private static Dictionary<string, Texture2D> externalRes;//系统自带资源
        private static Dictionary<string, Texture2D> tempRes;//临时资源，比如某个stage才需要的

        private static RResource mainDll;
        #region 初始化、清理
        /// <summary>
        /// 模拟构造函数
        /// </summary>
        internal static void Init()
        {
            internalRes = new Dictionary<string, Texture2D>();//系统自带资源
            externalRes = new Dictionary<string, Texture2D>();//系统自带资源
            tempRes = new Dictionary<string, Texture2D>();
            mainDll = new RResource("main", "");
        }
        /// <summary>
        /// 模拟析构函数，但不叫dispose避免误解
        /// </summary>
        internal static void DisposeAll()
        {
            ClearDictAll();
            internalRes = null;
            externalRes = null;
            tempRes = null;
        }

        /// <summary>
        /// 清空和释放字典中的缓存
        /// </summary>
        internal static void ClearDictAll()
        {
            clearDict(internalRes);
            clearDict(externalRes);
            clearDict(tempRes);
        }

        private static void clearDict(Dictionary<string, Texture2D> dict)
        {
            foreach (KeyValuePair<string, Texture2D> pair in dict)
            {
                pair.Value.Dispose();
            }
            dict.Clear();
        }
        #endregion

        private static Dictionary<string, Texture2D> getDict(CacheKeep cache)
        {
            switch (cache)
            {
                case CacheKeep.Intern:
                    return internalRes;
                case CacheKeep.Extern:
                    return externalRes;
                case CacheKeep.Temp:
                default:
                    return tempRes;
            }
        }

        private static void saveCache(string name, Texture2D t, CacheKeep cache)
        {
            if (t == null)
            {
                return;
            }
            getDict(cache).Add(name, t);
        }

        private static Texture2D getCache(string name, CacheKeep cache)
        {
            Texture2D t = null;
            if (getDict(cache).TryGetValue(name, out t))
            {
                return t;
            }
            return null;
        }

        internal static Texture2D GetImage(string name)
        {
            return GetImage(name, GraphicsFrom.Own);
        }

        internal static Texture2D GetImage(string name,GraphicsFrom from)
        {
            CacheKeep cache;
            switch (from)
            {
                case GraphicsFrom.Own:
                    cache = CacheKeep.Intern;
                    break;
                case GraphicsFrom.File:
                    cache = CacheKeep.Extern;
                    break;
                case GraphicsFrom.Other:
                default:
                    cache = CacheKeep.Temp;
                    break;
            }
            return GetImage(name, from, cache);
        }

        /// <summary>
        /// 获取图片
        /// from和cache分开的逻辑可以实现加载本地文件但保存到内核cache
        /// 或者加载dll文件但放到临时cache
        /// 但是当不写明cache时，会根据from来对应出一个默认cache
        /// </summary>
        /// <param name="name"></param>
        /// <param name="from">来源</param>
        /// <param name="cache">保存到什么地方</param>
        /// <returns></returns>
        internal static Texture2D GetImage(string name, GraphicsFrom from, CacheKeep cache)
        {
            if (name == "")
            {
                return null;
            } 
            Texture2D t = null;
            //任意目录
            if ((from & GraphicsFrom.Other) > 0)
            {
                t = getCache(name, cache);
                if (t != null)
                    return t;
                t = Texture2D.FromStream(GameMain.Instance.Device, RWExternal.GetFileStream(name));
                if (t != null)
                {
                    saveCache(name, t, cache);
                    return t;
                }
                return null; //对于other，不要执行后续逻辑
            }
            //data目录
            if ((from & GraphicsFrom.File) > 0)
            {
                string localName = "data\\" + name;
                t = getCache(localName, cache);
                if (t != null)
                    return t;
                t = Texture2D.FromStream(GameMain.Instance.Device, RWExternal.GetFileStream(localName));
                if (t != null)
                {
                    saveCache(name, t, cache);
                    return t;
                }
            }
            if ((from & GraphicsFrom.Own) > 0)
            {
                t = getCache(name, cache);
                if (t != null)
                    return t;
                t = Texture2D.FromStream(GameMain.Instance.Device, mainDll.GetStream(name));
                if (t != null)
                {
                    saveCache(name, t, cache);
                    return t;
                }
            }
            return null;
        }
    }
}
