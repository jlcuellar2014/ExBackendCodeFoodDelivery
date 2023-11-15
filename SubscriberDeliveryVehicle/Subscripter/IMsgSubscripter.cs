namespace SubscriberDeliveryVehicle.Subscripter
{
    internal interface IMsgSubscripter: IDisposable
    {
        void ReciveDeliveryVehicheMovedEvent();
    }
}