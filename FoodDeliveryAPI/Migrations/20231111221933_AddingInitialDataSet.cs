using FoodDeliveryAPI.Model;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingInitialDataSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_OrderProducts_OrderId");
            migrationBuilder.DropIndex("IX_OrderProducts_ProductId");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerName", "ReceivingAddress", "PhoneNumber" },
                values: new object[,]
                {
                     {1, "Ruben Bernal", "C/ Ribera del Loira, 28. Madrid-28042", "34910502905" },
                }
            );

            migrationBuilder.InsertData(
                table: "DeliveryVehicles",
                columns: new[] { "DeliveryVehicleId", "DeliverymanName", "DeliverymanPhoneNumber", "VehicleDescription", "Coordinate" },
                values: new object[,]
                {
                    {1, "Jorge Luis Cuellar", "666351395", "Peugeot 508 de Color Blanco", "38.360998544247984, -0.4186471976674082" },
                }
            );

            migrationBuilder.InsertData(
               table: "DeliveryVehicleCoordinates",
               columns: new[] { "DeliveryVehicleCoordinatesId", "DeliveryVehicleId", "Coordinate", "DateTime" },
               values: new object[,]
               {
                     {1, 1, "38.36356529613985, -0.43049532339588303", new DateTime(2023, 11, 11, 10, 0, 0) },
                     {2, 1, "38.36379071079803, -0.4254794688681666",  new DateTime(2023, 11, 11, 10, 10, 0) },
                     {3, 1, "38.36470709244036, -0.4198257059907396",  new DateTime(2023, 11, 11, 10, 20, 0) },
                     {4, 1, "38.36248906520641, -0.41703969406140096",  new DateTime(2023, 11, 11, 10, 20, 0) },
                     {5, 1, "38.360998544247984, -0.4186471976674082",  new DateTime(2023, 11, 11, 10, 30, 0) },
               }
           );

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantId", "RestaurantName", "PhoneNumber", "Coordinate", "PickUpAddress" },
                values: new object[,]
                {
                     {1, "Krokante", "966358635", "38.36861684741527, -0.4216071341552255", "Av. Historiador Vicente Ramos, 30, 03540 Alicante" },
                     {2, "Altramuz", "965466212", "38.369087143477884, -0.4170146457563579",  "Av. de Holanda, 18, 03540 Alicante"},
                     {3, "Ohana Gastro Bar", "634464540", "38.36841569179011, -0.4273581335542207",  "C. Licia Calderón, 2, 03540 Liria, Alicante"},
                     {4, "Los Pacos", "965164213", "38.371273785533205, -0.4143527803954427",  "Av. Bruselas, 9, 03540 Alicante"},
                }
            );

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "RestaurantId", "ProductName", "ProductDescription", "Price" },
                values: new object[,]
                 {
                     {1, 1, "Empanada argentina", "", 3.20m },
                     {2, 1, "Chorizo criollo", "", 4.50m },
                     {3, 1, "Patata bravas", "", 10.80m },
                     {4, 1, "Huevos rotos con jamón", "", 11.50m },
                     {5, 2, "Tostas marineras de gambas", "", 8m },
                     {6, 2, "Capricho Alicantino", "", 10m },
                     {7, 2, "Berberechos XXL", "", 13m },
                     {8, 3, "American Burguer", "Deliciosos 200g de carne 100% Black Angus con lechuga, tomate y queso chedar fundido", 10.90m },
                     {10, 4, "Menú del Día", "Puede elegir un primer plato de entre tres opciones y un segundo de entre otras tres opciones", 25m },
                }
            );

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "RestaurantId", "DeliveryVehicleId", "CreatedDate", "TrackingNumber", "SpecialInstructions", "OrderStatus" },
                values: new object[,]
                {
                     {1, 1, 1, 1, new DateTime(2023, 11, 11, 10, 0, 0), new Guid().ToString(), "Dejar con el portero del edificio", (int)OrderStatus.OnTheWay },
                }
            );

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId", "Ammount" },
                values: new object[,]
                {
                     {1, 1, 2},
                     {1, 2, 1},
                     {1, 3, 3},
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
