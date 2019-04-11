using System;
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

        public static void Close()
        {
            connection.Close();
        }
    }
}