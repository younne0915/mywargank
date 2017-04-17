namespace Util
{
	public static class WGMath 
	{
		public static int Abs(int val)
		{
			return val < 0 ? -val : val;
		}

        public static long Abs(long val)
        {
            return val < 0 ? -val : val;
        }

        public static FixedNum Abs(FixedNum val)
        {
			if(val < 0)
			{
				return 0 - val;
			}
			return val;
        }

        public static FixedVector3 ClampVector3(FixedVector3 val,FixedNum minVal,FixedNum maxVal)
		{
			if(val.sqrmagnitude < minVal * minVal)
			{
				return val.normalized * minVal;
			}
			else if(val.sqrmagnitude > maxVal * maxVal)
			{
				return val.normalized * maxVal;
			}
			return val;
		}
	}
}