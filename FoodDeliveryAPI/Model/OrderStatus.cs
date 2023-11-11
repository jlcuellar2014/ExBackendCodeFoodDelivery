namespace FoodDeliveryAPI.Model
{
    public enum OrderStatus
    {
        Pending = 0,
        Accepted = 1,
        Preparing = 2,
        Rejected = 3,
        Cancelled = 4,
        OnTheWay = 5,
        Delivered = 6,
    }
}
