using SubscriberDeliveryVehicle.Subscripter;

Console.WriteLine("Press [Enter] to exit to the program.\t\t");

IMsgSubscripter subscripter = new RabbitMqSubscripter();
subscripter.ReciveDeliveryVehicheMovedEvent();

Console.ReadLine();
subscripter.Dispose();