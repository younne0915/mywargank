using UnityEngine;

namespace Util
{
    public enum FixedNumRatio
    {
        Percentage,
        Permill,
    }
    public struct FixedNum
    {
        public const int SHIFT_AMOUNT = 16;
        public const long ONE = 1 << SHIFT_AMOUNT;
        public const long MAX = long.MaxValue >> SHIFT_AMOUNT;
        const int FLOAT_SCALAR = 1000;
        public long val;

        public string toString()
        {
            return NormalizeToFloat() + "";
        }

        public string toIntString()
        {
            return NormalizeToInt() + "";
        }

        public static FixedNum zero
        {
            get { return new FixedNum(0); }
        }

		public static FixedNum one
		{
			get { return new FixedNum(1); }
		}

        #region constructor
        public FixedNum(long arg)
        {
			val = arg * ONE;
        }

        public FixedNum(int arg)
        {
			val = arg * ONE;
        }

		public FixedNum(float arg)
		{
			val = (long)(arg * FLOAT_SCALAR);
			val = val * ONE;
			val /= FLOAT_SCALAR;
		}

        public FixedNum(int arg, FixedNumRatio type)
        {
			val = arg * ONE;
            if (type == FixedNumRatio.Percentage)
            {
                val /= 100;
            }
            else if (type == FixedNumRatio.Permill)
            {
                val /= 1000;
            }
        }
        #endregion

        public long Normalize()
        {
            return val / ONE;
        }

        public int CeilToInt()
        {
            return (int)((val + ONE / 2) / ONE);
        }

        public int NormalizeToInt()
        {
            return (int)(val / ONE);
        }

		//used for render only, don't use this to calculate anything to ignore mistake on different platform
		public float NormalizeToFloat()
		{
			return val * 1.0f / ONE;
		}

        //public float NormalizeFloat()
        //{
        //    return (float)(val) / One ;
        //}

        public void ResetToZero()
        {
            val = 0;
        }

        public bool IsZero()
        {
            return val == 0;
        }

        public static FixedNum Sqrt(FixedNum lhs)
        {
            if (lhs.val == 0)
                return FixedNum.zero;
            long n = (lhs.val >> 1) + 1;
            long n1 = (n + (lhs.val / n)) >> 1;
            while (n1 < n)
            {
                n = n1;
                n1 = (n + (lhs.val / n)) >> 1;
            }
            FixedNum val = new FixedNum(1);
            val.val = n << (SHIFT_AMOUNT / 2);
            return val;
        }

        public static FixedNum Power(FixedNum lhs, FixedNum times)
        {
            FixedNum val = new FixedNum(1);
            long cnt = WGMath.Abs(times.Normalize());
            for (int i = 0; i < cnt; i++)
            {
                val *= lhs;
            }
            if (times < 0)
            {
                val = new FixedNum(1) / val;
            }
            return val;
        }

        public static FixedNum Power(FixedNum lhs, int times)
        {
			FixedNum val = FixedNum.one;
            int cnt = WGMath.Abs(times);
            for (int i = 0; i < cnt; i++)
            {
                val *= lhs;
            }
            if (times < 0)
            {
                val = new FixedNum(1) / val;
            }
            return val;
        }


        #region override +
        public static FixedNum operator +(FixedNum lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum();
            result.val = lhs.val + rhs.val;
            return result;
        }

        public static FixedNum operator +(int lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            result.val = result.val + rhs.val;
            return result;
        }

        public static FixedNum operator +(FixedNum lhs, int rhs)
        {
            FixedNum result = new FixedNum(rhs);
            result.val = lhs.val + result.val;
            return result;
        }

        public static FixedNum operator +(long lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            result.val = result.val + rhs.val;
            return result;
        }

        public static FixedNum operator +(FixedNum lhs, long rhs)
        {
            FixedNum result = new FixedNum(rhs);
            result.val = lhs.val + result.val;
            return result;
        }
        #endregion

        #region override -
        public static FixedNum operator -(FixedNum lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum();
            result.val = lhs.val - rhs.val;
            return result;
        }

        public static FixedNum operator -(int lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            result.val = result.val - rhs.val;
            return result;
        }

        public static FixedNum operator -(FixedNum lhs, int rhs)
        {
            FixedNum result = new FixedNum(rhs);
            result.val = lhs.val - result.val;
            return result;
        }

        public static FixedNum operator -(long lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            result.val = result.val - rhs.val;
            return result;
        }

        public static FixedNum operator -(FixedNum lhs, long rhs)
        {
            FixedNum result = new FixedNum(rhs);
            result.val = lhs.val - result.val;
            return result;
        }

        public static FixedNum operator -(FixedNum lhs)
        {
            return 0 - lhs;
        }

        #endregion

        #region override *
        public static FixedNum operator *(FixedNum lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum();
            result.val = (lhs.val * rhs.val) >> SHIFT_AMOUNT;
            return result;
        }

        public static FixedNum operator *(int lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            result.val = (result.val * rhs.val) >> SHIFT_AMOUNT;
            return result;
        }

        public static FixedNum operator *(FixedNum lhs, int rhs)
        {
            FixedNum result = new FixedNum(rhs);
            result.val = (lhs.val * result.val) >> SHIFT_AMOUNT;
            return result;
        }

        public static FixedNum operator *(long lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            result.val = (result.val * rhs.val) >> SHIFT_AMOUNT;
            return result;
        }

        public static FixedNum operator *(FixedNum lhs, long rhs)
        {
            FixedNum result = new FixedNum(rhs);
            result.val = (lhs.val * result.val) >> SHIFT_AMOUNT;
            return result;
        }
        #endregion

        #region override /
        public static FixedNum operator /(FixedNum lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum();
            try
            {
                result.val = (lhs.val << SHIFT_AMOUNT) / rhs.val;
            }
            catch
            {

            }
            return result;
        }

        public static FixedNum operator /(int lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            try
            {
                result.val = (result.val << SHIFT_AMOUNT) / rhs.val;
            }
            catch
            {

            }
            return result;
        }

        public static FixedNum operator /(FixedNum lhs, int rhs)
        {
            FixedNum result = new FixedNum(rhs);
            try
            {
                result.val = (lhs.val << SHIFT_AMOUNT) / result.val;
            }
            catch
            {

            }
            return result;
        }

        public static FixedNum operator /(long lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            try
            {
                result.val = (result.val << SHIFT_AMOUNT) / rhs.val;
            }
            catch
            {
            }
            return result;
        }

        public static FixedNum operator /(FixedNum lhs, long rhs)
        {
            FixedNum result = new FixedNum(rhs);
            try
            {
                result.val = (lhs.val << SHIFT_AMOUNT) / result.val;
            }
            catch
            {

            }
            return result;
        }
        #endregion

        #region override %
        public static FixedNum operator %(FixedNum lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum();
            result.val = lhs.val % rhs.val;
            return result;
        }

        public static FixedNum operator %(int lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            result.val = result.val % rhs.val;
            return result;
        }

        public static FixedNum operator %(FixedNum lhs, int rhs)
        {
            FixedNum result = new FixedNum(rhs);
            result.val = lhs.val % result.val;
            return result;
        }

        public static FixedNum operator %(long lhs, FixedNum rhs)
        {
            FixedNum result = new FixedNum(lhs);
            result.val = result.val % rhs.val;
            return result;
        }

        public static FixedNum operator %(FixedNum lhs, long rhs)
        {
            FixedNum result = new FixedNum(rhs);
            result.val = lhs.val % result.val;
            return result;
        }
        #endregion

        public static FixedNum operator ++(FixedNum lhs)
        {
            FixedNum result = new FixedNum();
            result.val = lhs.val + 1;
            return result;
        }

        public static FixedNum operator --(FixedNum lhs)
        {
            FixedNum result = new FixedNum();
            result.val = lhs.val - 1;
            return result;
        }

        #region override ==
        public static bool operator ==(FixedNum lhs, FixedNum rhs)
        {
            return lhs.val == rhs.val;
        }

        public static bool operator ==(int lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val == rhs.val;
        }

        public static bool operator ==(FixedNum lhs, int rhs)
        {
			return lhs.val == new FixedNum(rhs).val;
        }

        public static bool operator ==(long lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val == rhs.val;
        }

        public static bool operator ==(FixedNum lhs, long rhs)
        {
			return lhs.val == new FixedNum(rhs).val;
        }
        #endregion

        #region override !=
        public static bool operator !=(FixedNum lhs, FixedNum rhs)
        {
            return lhs.val != rhs.val;
        }

        public static bool operator !=(int lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val != rhs.val;
        }

        public static bool operator !=(FixedNum lhs, int rhs)
        {
			return lhs.val != new FixedNum(rhs).val;
        }

        public static bool operator !=(long lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val != rhs.val;
        }

        public static bool operator !=(FixedNum lhs, long rhs)
        {
			return lhs.val != new FixedNum(rhs).val;
        }
        #endregion

        #region override <
        public static bool operator <(FixedNum lhs, FixedNum rhs)
        {
            return lhs.val < rhs.val;
        }

        public static bool operator <(int lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val < rhs.val;
        }

        public static bool operator <(FixedNum lhs, int rhs)
        {
			return lhs.val < new FixedNum(rhs).val;
        }

        public static bool operator <(long lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val < rhs.val;
        }

        public static bool operator <(FixedNum lhs, long rhs)
        {
			return lhs.val < new FixedNum(rhs).val;
        }
        #endregion

        #region override >
        public static bool operator >(FixedNum lhs, FixedNum rhs)
        {
            return lhs.val > rhs.val;
        }

        public static bool operator >(int lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val > rhs.val;
        }

        public static bool operator >(FixedNum lhs, int rhs)
        {
			return lhs.val > new FixedNum(rhs).val;
        }

        public static bool operator >(long lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val > rhs.val;
        }

        public static bool operator >(FixedNum lhs, long rhs)
        {
			return lhs.val > new FixedNum(rhs).val;
        }
        #endregion

        #region override <=
        public static bool operator <=(FixedNum lhs, FixedNum rhs)
        {
            return lhs.val <= rhs.val;
        }

        public static bool operator <=(int lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val <= rhs.val;
        }

        public static bool operator <=(FixedNum lhs, int rhs)
        {
			return lhs.val <= new FixedNum(rhs).val;
        }

        public static bool operator <=(long lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val <= rhs.val;
        }

        public static bool operator <=(FixedNum lhs, long rhs)
        {
			return lhs.val <= new FixedNum(rhs);
        }
        #endregion

        #region override >=
        public static bool operator >=(FixedNum lhs, FixedNum rhs)
        {
            return lhs.val >= rhs.val;
        }

        public static bool operator >=(int lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val >= rhs.val;
        }

        public static bool operator >=(FixedNum lhs, int rhs)
        {
			return lhs.val >= new FixedNum(rhs).val;
        }

        public static bool operator >=(long lhs, FixedNum rhs)
        {
			return new FixedNum(lhs).val >= rhs.val;
        }

        public static bool operator >=(FixedNum lhs, long rhs)
        {
			return lhs.val >= new FixedNum(rhs).val;
        }
        #endregion
    }

}
