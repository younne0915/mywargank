#if client
using UnityEngine;
#endif

namespace Util
{
    public struct FixedVector3
    {
        public FixedNum x;
        public FixedNum y;
        public FixedNum z;

        public string toString()
        {
            return "x: " + x.toString() + " y: " + y.toString() + " z: " + z.toString();
        }

        #region Constructor
        public FixedVector3(FixedNum x, FixedNum y, FixedNum z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public FixedVector3(int x, int y, int z)
        {
            this.x = new FixedNum(x);
            this.y = new FixedNum(y);
            this.z = new FixedNum(z);
        }

        public FixedVector3(FixedNum x, int y, int z)
        {
            this.x = x;
            this.y = new FixedNum(y);
            this.z = new FixedNum(z);
        }

        public FixedVector3(int x, FixedNum y, int z)
        {
            this.x = new FixedNum(x);
            this.y = y;
            this.z = new FixedNum(z);
        }

        public FixedVector3(int x, int y, FixedNum z)
        {
            this.x = new FixedNum(x);
            this.y = new FixedNum(y);
            this.z = z;
        }

		public FixedVector3(float x, float y, float z)
		{
			this.x = new FixedNum(x);
			this.y = new FixedNum(y);
			this.z = new FixedNum(z);
		}

		public FixedVector3(FixedNum x, float y, float z)
		{
			this.x = x;
			this.y = new FixedNum(y);
			this.z = new FixedNum(z);
		}

		public FixedVector3(float x, FixedNum y, float z)
		{
			this.x = new FixedNum(x);
			this.y = y;
			this.z = new FixedNum(z);
		}

		public FixedVector3(float x, float y, FixedNum z)
		{
			this.x = new FixedNum(x);
			this.y = new FixedNum(y);
			this.z = z;
		}
        #endregion

		public static FixedVector3 zero
		{
			get{return new FixedVector3(0,0,0);}
		}

		public static FixedVector3 right
		{
			get{return new FixedVector3(1,0,0);}
		}

        public static FixedVector3 left
        {
            get { return new FixedVector3(-1, 0, 0); }
        }

        public static FixedVector3 up
		{
			get{return new FixedVector3(0,1,0);}
		}

        public static FixedVector3 down
        {
            get { return new FixedVector3(0, -1, 0); }
        }

        public static FixedVector3 forward
		{
			get{return new FixedVector3(0,0,1);}
		}

        public static FixedVector3 back
        {
            get { return new FixedVector3(0, 0, -1); }
        }
			
        public FixedNum sqrmagnitude
        {
            get { return x * x + y * y + z * z; }
        }

        public FixedNum magnitude
        {
            get { return FixedNum.Sqrt(sqrmagnitude); }
        }

        public FixedVector3 normalized
        {            
            get
            {
                FixedNum magnitude = this.magnitude;
                if (magnitude.IsZero())
                {
                    return this;
                }
				return new FixedVector3(x / magnitude, y / magnitude, z / magnitude);
            }
        }

        public FixedNum Distance(FixedVector3 other)
        {
            FixedVector3 deltaVector3 = this - other;
            return deltaVector3.magnitude;
        }

		public FixedNum Dot(FixedVector3 other)
		{
			return x * other.x + y * other.y + z * other.z;
		}

        public FixedNum SqrDistance(FixedVector3 other)
        {
            FixedVector3 deltaVector3 = this - other;
            return deltaVector3.sqrmagnitude;
        }

        public FixedVector3 Limit(FixedNum len)
        {
            if (magnitude < len)
                return this;
            return this.normalized * len;
        }

#if client
        public Vector3 vector3
		{
			get{return new Vector3(x.NormalizeToFloat(),y.NormalizeToFloat(),z.NormalizeToFloat());}
		}
#endif

        public static FixedVector3 operator + (FixedVector3 lhs, FixedVector3 rhs)
        {
            return new FixedVector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static FixedVector3 operator - (FixedVector3 lhs, FixedVector3 rhs)
        {
            return new FixedVector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static FixedVector3 operator * (FixedVector3 lhs, FixedVector3 rhs)
        {
            return new FixedVector3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
        }

		public static FixedVector3 operator * (FixedVector3 lhs, FixedNum rhs)
		{
			return new FixedVector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		public static FixedVector3 operator * (FixedNum lhs, FixedVector3 rhs)
		{
			return new FixedVector3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

		public static FixedVector3 operator * (FixedVector3 lhs, int rhs)
		{
			return new FixedVector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		public static FixedVector3 operator * (int lhs, FixedVector3 rhs)
		{
			return new FixedVector3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

        public static FixedVector3 operator / (FixedVector3 lhs, FixedVector3 rhs)
        {
            return new FixedVector3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
        }

		public static FixedVector3 operator / (FixedVector3 lhs, FixedNum rhs)
		{
			return new FixedVector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}		

		public static FixedVector3 operator / (FixedVector3 lhs, int rhs)
		{
			return new FixedVector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		public static bool operator ==(FixedVector3 lhs, FixedVector3 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		public static bool operator !=(FixedVector3 lhs, FixedVector3 rhs)
		{
			return !(lhs == rhs);
		}

    }
}