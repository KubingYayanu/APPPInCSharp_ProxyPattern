using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APPPInCSharp_ProxyPattern
{
    public class DB
    {
        private static SqlConnection connection;

        public static void Init()
        {
            string connectionString = @"Initial Catalog=QuickMart;Data Source=10.0.75.1;user id=dev;password=sa;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        #region Product

        public static void Store(ProductData pd)
        {
            SqlCommand command = BuildInsertionCommand(pd);
            command.ExecuteNonQuery();
        }

        private static SqlCommand BuildInsertionCommand(ProductData pd)
        {
            string sql = @"INSERT INTO Products (sku, name, price) VALUES (@sku, @name, @price)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sku", pd.sku);
            command.Parameters.AddWithValue("@price", pd.price);
            command.Parameters.AddWithValue("@name", pd.name);

            return command;
        }

        public static ProductData GetProductData(string sku)
        {
            SqlCommand command = BuildProductQureyCommand(sku);
            IDataReader reader = ExecuteQueryStatement(command);
            ProductData pd = ExtractProductDataFromReader(reader);
            reader.Close();
            return pd;
        }

        private static ProductData ExtractProductDataFromReader(IDataReader reader)
        {
            ProductData pd = new ProductData();
            pd.sku = reader["sku"].ToString();
            pd.price = Convert.ToInt32(reader["price"]);
            pd.name = reader["name"].ToString();
            return pd;
        }

        public static void DeleteProductData(string sku)
        {
            BuildProductDeleteStatement(sku).ExecuteNonQuery();
        }

        private static SqlCommand BuildProductDeleteStatement(string sku)
        {
            string sql = @"DELETE FROM Products WHERE sku = @sku";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sku", sku);
            return command;
        }

        private static IDataReader ExecuteQueryStatement(SqlCommand command)
        {
            IDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader;
        }

        private static SqlCommand BuildProductQureyCommand(string sku)
        {
            string sql = @"SELECT * FROM Products WHERE sku = @sku";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sku", sku);
            return command;
        }

        #endregion Product

        #region Order

        public static OrderData NewOrder(string customerId)
        {
            string sql = @"INSERT INTO Orders (cusId) VALUES (@cusId);
                            SELECT scope_identity()";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@cusId", customerId);
            int newOrderId = Convert.ToInt32(command.ExecuteScalar());
            return new OrderData(newOrderId, customerId);
        }

        public static OrderData GetOrderData(int orderId)
        {
            string sql = @"SELECT cusId FROM Orders WHERE orderId = @orderId";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@orderId", orderId);
            IDataReader reader = command.ExecuteReader();

            OrderData od = null;

            if (reader.Read())
            {
                od = new OrderData(orderId, reader["cusId"].ToString());
            }
            reader.Close();
            return od;
        }

        #endregion Order

        #region Item

        public static void Store(ItemData id)
        {
            SqlCommand command = BuildItemInsersionStatement(id);
            command.ExecuteNonQuery();
        }

        private static SqlCommand BuildItemInsersionStatement(ItemData id)
        {
            string sql = @"INSERT INTO Items (orderId, quantity, sku) VALUES (@orderId, @quantity, @sku)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@orderId", id.orderId);
            command.Parameters.AddWithValue("@quantity", id.qty);
            command.Parameters.AddWithValue("@sku", id.sku);
            return command;
        }

        public static ItemData[] GetItemsForOrder(int orderId)
        {
            SqlCommand command = BuildItemsForOrderQueryStatement(orderId);
            IDataReader reader = command.ExecuteReader();
            ItemData[] id = ExtractItemDataFromResultSet(reader);
            reader.Close();
            return id;
        }

        private static ItemData[] ExtractItemDataFromResultSet(IDataReader reader)
        {
            List<ItemData> items = new List<ItemData>();
            while (reader.Read())
            {
                int orderId = Convert.ToInt32(reader["orderId"]);
                int quantity = Convert.ToInt32(reader["quantity"]);
                string sku = reader["sku"].ToString();
                ItemData id = new ItemData(orderId, quantity, sku);
                items.Add(id);
            }
            return items.ToArray();
        }

        private static SqlCommand BuildItemsForOrderQueryStatement(int orderId)
        {
            string sql = @"SELECT * from Items WHERE orderId = @orderId";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@orderId", orderId);
            return command;
        }

        #endregion Item

        public static void Clear()
        {
            ExecuteSql("DELETE FROM Items");
            ExecuteSql("DELETE FROM Orders");
            ExecuteSql("DELETE FROM Products");
        }

        private static void ExecuteSql(string sql)
        {
            SqlCommand command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static void Close()
        {
            connection.Close();
        }
    }
}