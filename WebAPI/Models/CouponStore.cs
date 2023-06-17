namespace WebAPI.Models
{
    public static class CouponStore
    {
        public static List<Coupon> Coupons = new List<Coupon>
        {
            new Coupon{Id=1,Name="A",IsActive=true,Percent=10},
            new Coupon{Id=2,Name="B",IsActive=true,Percent=20},
            new Coupon{Id=3,Name="C",IsActive=false,Percent=30},
            new Coupon{Id=4,Name="D",IsActive=true,Percent=40},
            new Coupon{Id=5,Name="E",IsActive=false,Percent=50}
        };
    }
}
