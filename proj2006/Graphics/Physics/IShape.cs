using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace project2006.Graphics.Physics
{
    internal interface IShape:IDisposable
    {
        /// <summary>
        /// Getters Setters
        /// 位置、速度、角度、旋度
        /// </summary>
        Vector2 Position{get;set;}
        Vector2 Velocity{get;set;}
        float Rotation{get;set;}
        float AVelocity{get;set;}
        /// <summary>
        /// 整体横向移动offsetX
        /// </summary>
        /// <param name="offsetX">横向偏移</param>
        void MoveX(float offsetX);
        /// <summary>
        /// 整体纵向移动offsetY
        /// </summary>
        /// <param name="offsetY">纵向偏移</param>
        void MoveY(float offsetY);
        /// <summary>
        /// 整体移动offsetX,offsetY
        /// </summary>
        /// <param name="offsetX">横向偏移</param>
        /// <param name="offsetY">纵向偏移</param>
        void MoveXY(float offsetX,float offsetY);
        /// <summary>
        /// 整体旋转一定角度偏移
        /// 一般计算旋转顺序是先移再转再移
        /// </summary>
        /// <param name="offsetAngle">角度偏移</param>
        /// <param name="center">旋转中心</param>
        void Rotate(float offsetAngle,Vector2 center);
        /// <summary>
        /// 判断是否与shape相交
        /// </summary>
        Boolean IsIntersectWithShape(Shape shape);
        /// <summary>
        /// 判断一个点是否在形状内部
        /// </summary>
        /// <param name="p">点坐标</param>
        Boolean IsInShape(Vector2 p);
        /// <summary>
        /// 从一系列点中去除在形状内的点
        /// </summary>
        /// <param name="plist">点集</param>
        /// <returns>去除点的个数</returns>
        int RemovePointsInShape(List<Vector2> plist);
    }
}
