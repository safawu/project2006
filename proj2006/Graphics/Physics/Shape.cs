using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace project2006.Graphics.Physics
{
    internal class Shape:IShape
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected float rotation;
        protected float aVelocity;
        #region IDisposable 成员

        public virtual void Dispose()
        {
            ;
        }

        #endregion

        #region IShape 成员

        public virtual Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position=value;
            }
        }

        public virtual Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity=value;
            }
        }

        public virtual float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation=value;
            }
        }

        public virtual float AVelocity
        {
            get
            {
                return aVelocity;
            }
            set
            {
                aVelocity=value;
            }
        }

        public virtual void MoveX(float offsetX)
        {
            position.X += offsetX;
        }

        public virtual void MoveY(float offsetY)
        {
            position.Y += offsetY;
        }

        public virtual void MoveXY(float offsetX, float offsetY)
        {
            position.X += offsetX;
            position.Y += offsetY;
        }

        /// <summary>
        /// 绕中心点旋转，少年啊数死早了么
        /// </summary>
        /// <param name="offsetAngle">角度偏移，Descartes直角坐标</param>
        /// <param name="center">旋转中心</param>
        public virtual void Rotate(float offsetAngle,Vector2 center)
        {
            rotation += offsetAngle;
            position.X = (float)((position.X - center.X) * Math.Cos(offsetAngle) + (position.Y - center.Y) * Math.Sin(offsetAngle) + center.X);
            position.Y = (float)(-(position.X - center.X) * Math.Sin(offsetAngle) + (position.Y - center.Y) * Math.Cos(offsetAngle) + center.Y); 
        }

        public virtual bool IsIntersectWithShape(Shape shape)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsInShape(Vector2 p)
        {
            throw new NotImplementedException();
        }

        public virtual int RemovePointsInShape(List<Vector2> plist)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    internal class Circle : Shape
    {
        private float radius;
        private Vector2 center;
    }

    /// <summary>
    /// 多边形试作型一
    /// a--------b
    ///   \    /
    ///    \  /
    ///     \/
    ///     /\
    ///    /  \
    ///   /    \
    /// c--------d
    ///  TODO:这种怎么搞
    /// </summary>
    internal class Polygon : Shape
    {
        private List<Vector2> points;/*\点集矢量*/
        private List<Vector2> edges;/*\边集矢量，TODO:这个什么时候艹掉*/

        internal Polygon(List<Vector2> pointList, Vector2 pos)
        {
            points = new List<Vector2>();
            foreach (Vector2 v in pointList)
            {
                Vector2 o = new Vector2(v.X, v.Y);
                points.Add(o);
            }
            position = new Vector2(pos.X, pos.Y);
            //BuildEdges();
        }

        internal void BuildEdges()
        {
            edges = new List<Vector2>();
            Vector2 p1;
            Vector2 p2;
            for (int i = 0; i < points.Count; i++)
            {
                p1 = points[i];
                if (i + 1 >= points.Count)
                {
                    p2 = points[0];
                }
                else
                {
                    p2 = points[i + 1];
                }
                edges.Add(p2 - p1);
            }
        }

        internal List<Vector2> Edges
        {
            get { return edges; }
        }

        internal List<Vector2> Points
        {
            get { return points; }
        }

        /// <summary>
        /// 求形心
        /// TODO:什么时候把这个弄到接口里去
        /// </summary>
        internal Vector2 Center
        {
            get
            {
                float totalX = 0;
                float totalY = 0;
                foreach (Vector2 v in points)
                {
                    totalX += v.X;
                    totalY += v.Y;
                }
                return new Vector2(totalX / (float)points.Count, totalY / (float)points.Count);
            }
        }

        public override void Dispose()
        {
            points.Clear();
            edges.Clear();
        }

        #region IShape 成员

        public override void MoveX(float offsetX)
        {
            throw new NotImplementedException();
        }

        public override void MoveY(float offsetY)
        {
            throw new NotImplementedException();
        }

        public override void MoveXY(float offsetX, float offsetY)
        {
            throw new NotImplementedException();
        }

        public override void Rotate(float offsetAngle, Vector2 center)
        {
            throw new NotImplementedException();
        }

        public override bool IsIntersectWithShape(Shape shape)
        {
            throw new NotImplementedException();
        }

        public override bool IsInShape(Vector2 p)
        {
            /*\传说中的射线法*/
            if (points.Count <= 3)
                return false;
            Boolean inside = false;
            Boolean flag1, flag2;
            flag1 = (p.Y >= points[0].Y);
            for (int i = 0; i < points.Count(); i++)
            {
                int j = i + 1;
                if (i == points.Count() - 1)
                    j = 0;
                flag2 = (p.Y >= points[j].Y);
                if (flag1 != flag2)
                {
                    if (((points[j].Y - p.Y) * (points[i].X - points[j].X) >= (points[j].X - p.X) * (points[i].Y - points[j].Y)) == flag2)
                        inside = !inside;
                }
                flag1 = flag2;
            }
            return inside;
        }

        public override int RemovePointsInShape(List<Vector2> plist)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    internal class BezierCurve : Shape
    {
        internal List<Vector2> controlPoints;
        internal int division;
    }
}
