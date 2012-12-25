using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace project2006.Graphics.Physics
{
    internal class MassShape:IShape
    {
        protected float mass;
        protected Vector2 acceleration;
        protected float aAcceleration;
        protected Shape shape;
        public Boolean MyMathsTeacherDiedEarly;/*\...*/
        

        #region IDisposable 成员

        public virtual void Dispose()
        {
            ;
        }

        #endregion

        #region IShape 成员

        public Vector2 Position
        {
            get
            {
                return shape.Position;
            }
            set
            {
                shape.Position=value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return shape.Velocity;
            }
            set
            {
                shape.Velocity = value;
            }
        }

        public float Rotation
        {
            get
            {
                return shape.Rotation;
            }
            set
            {
                shape.Rotation=value;
            }
        }

        public float AVelocity
        {
            get
            {
                return shape.AVelocity;
            }
            set
            {
                shape.AVelocity=value;
            }
        }

        public virtual void MoveX(float offsetX)
        {
            shape.MoveX(offsetX);
        }

        public virtual void MoveY(float offsetY)
        {
            shape.MoveY(offsetY);
        }

        public virtual void MoveXY(float offsetX, float offsetY)
        {
            shape.MoveXY(offsetX,offsetY);
        }

        public virtual void Rotate(float offsetAngle,Vector2 center)
        {
            shape.Rotate(offsetAngle,center);
        }

        public virtual bool IsIntersectWithShape(Shape shape)
        {
            return shape.IsIntersectWithShape(shape);
        }

        public virtual bool IsInShape(Vector2 p)
        {
            return shape.IsInShape(p);
        }

        public virtual int RemovePointsInShape(List<Vector2> plist)
        {
            return shape.RemovePointsInShape(plist);
        }

        #endregion

        /// <summary>
        /// Getters Setters
        /// 质量、加速度、角加速度
        /// </summary>
        public float Mass
        {
            get
            {
                return mass;
            }
            set
            {
                mass=value;
            }
        }

        public Vector2 Acceleration
        {
            get { return acceleration; }
        }

        public float AAcceleration
        {
            get { return aAcceleration; }
        }

        /// <summary>
        /// 根据力与着力点算两个加速度
        /// 先玩静力学
        /// </summary>
        /// <param name="Force">力</param>
        /// <param name="FPos">着力点</param>
        internal virtual void CalculateAccl(Vector2 Force, Vector2 FPos)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据作用在质心的力算加速度
        /// 先玩静力学
        /// </summary>
        /// <param name="Force">力</param>
        internal virtual void CalAByF(Vector2 Force)
        {
            throw new NotImplementedException();
        }
    }

    internal class MassCircle : MassShape
    {
    }

    internal class MassPolygon : MassShape
    {

    }

    internal class MassPoint : MassShape
    {
        /// <summary>
        /// 判断一个MassPoint是否在复杂多边形内部
        /// TODO:这个要艹掉
        /// </summary>
        /// <param name="polygon">多边形</param>
        /// <returns></returns>
        internal Boolean InsidePolygon(Polygon polygon)
        {
            throw new NotImplementedException();
        }

    }
}
