using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
//using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerClassLibrary.Repositories
{
    public class CustomerRepository : BaseRepository,ICustomerRepository
    {
        public int CustomerId { get; set; } = 0;
        public int  Create(Customer customer)
        {
            using (var connection = this.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                //insert command
                command.CommandText =
                    "INSERT INTO dbo.customers (first_name,last_name,phone_number,customer_email,total_purchases_amount,notes)" +
                    "VALUES(@first_name, @last_name, @phone_number, @customer_email, @total_purchases_amount,@notes);" +
                    "SELECT CAST(scope_identity() AS int)";



                command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50)
                {
                    Value = customer.FirstName
                });

                command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50)
                {
                    Value = customer.LastName
                });

                command.Parameters.Add(new SqlParameter("@phone_number", SqlDbType.VarChar, 15)
                {
                    Value = customer.PhoneNumber
                });

                command.Parameters.Add(new SqlParameter("@customer_email", SqlDbType.VarChar, 255)
                {
                    Value = customer.Email
                });

                command.Parameters.Add(new SqlParameter("@total_purchases_amount", SqlDbType.VarChar, 255)
                {
                    Value = customer.TotalPurchasesAmount
                });


                command.Parameters.Add(new SqlParameter("@notes", SqlDbType.VarChar, 255)
                {

                    Value = JsonConvert.SerializeObject(customer.Notes) //JsonSerializer.Serialize<List<string>>(customer.Notes)
                });


                CustomerId = (Int32)command.ExecuteScalar();

                connection.Close();
            }

            return CustomerId;
        }

        public void DeleteAll()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand(
                    "DELETE FROM dbo.customers ; DBCC CHECKIDENT (customers, RESEED, 0)", connection);

                command.ExecuteNonQuery();

            }
        }

        public void Delete(int idCustomer)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand(
                    "DELETE FROM dbo.customers WHERE dbo.customers.customer_id = @customer_id", connection);

                var customerIdParam = new SqlParameter("@customer_id", SqlDbType.Int)
                {
                    Value = idCustomer
                };

                command.Parameters.Add(customerIdParam);
                command.ExecuteNonQuery();

            }
        }

        public void Update(Customer customer)
        {
            using (var connection = this.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                //insert command
                command.CommandText =
                    "UPDATE customers SET first_name=@first_name,last_name=@last_name,phone_number=@phone_number,customer_email=@customer_email,total_purchases_amount=@total_purchases_amount,notes=@notes" +
                    " WHERE customer_id=@customer_id";




                command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50)
                {
                    Value = customer.FirstName
                });

                command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50)
                {
                    Value = customer.LastName
                });

                command.Parameters.Add(new SqlParameter("@phone_number", SqlDbType.VarChar, 15)
                {
                    Value = customer.PhoneNumber
                });

                command.Parameters.Add(new SqlParameter("@customer_email", SqlDbType.VarChar, 255)
                {
                    Value = customer.Email
                });

                command.Parameters.Add(new SqlParameter("@total_purchases_amount", SqlDbType.VarChar, 255)
                {
                    Value = customer.TotalPurchasesAmount
                });

                command.Parameters.Add(new SqlParameter("@notes", SqlDbType.VarChar, 255)
                {
                    Value = JsonConvert.SerializeObject(customer.Notes)//JsonSerializer.Serialize<List<string>>(customer.Notes)
                });

                command.Parameters.Add(new SqlParameter("@customer_id", SqlDbType.Int)
                {
                    Value = customer.IdCustomer
                });


                command.ExecuteNonQuery();


                connection.Close();
            }

        }

        public Customer Read(int idCustomer)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand(
                    "SELECT * FROM dbo.customers WHERE dbo.customers.customer_id = @customer_id", connection);

                var customerIdParam = new SqlParameter("@customer_id", SqlDbType.Int)
                {
                    Value = idCustomer
                };

                command.Parameters.Add(customerIdParam);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Customer()
                        {
                            FirstName = reader["first_name"]?.ToString(),
                            LastName = reader["last_name"]?.ToString(),
                            PhoneNumber = reader["phone_number"]?.ToString(),
                            Email = reader["customer_email"]?.ToString(),
                            TotalPurchasesAmount = (decimal)reader["total_purchases_amount"],
                            Notes = JsonConvert.DeserializeObject<List<string>>(reader["notes"].ToString()),//JsonSerializer.Deserialize<List<string>>(reader["notes"].ToString()),
                            IdCustomer =idCustomer
                        };
                    }
                }

            }
            return null;
        }

        public List<Customer> ReadAll()
        {
            List<Customer> listCustomer = new List<Customer>();
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand(
                    "SELECT * FROM customers ", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listCustomer.Add( new Customer()
                        {
                            FirstName = reader["first_name"]?.ToString(),
                            LastName = reader["last_name"]?.ToString(),
                            PhoneNumber = reader["phone_number"]?.ToString(),
                            Email = reader["customer_email"]?.ToString(),
                            TotalPurchasesAmount = (decimal)reader["total_purchases_amount"],
                            Notes = JsonConvert.DeserializeObject<List<string>>(reader["notes"].ToString()),//JsonSerializer.Deserialize<List<string>>(reader["notes"].ToString()),
                            IdCustomer = (int)reader["customer_id"]
                        });
                    }
                }

            }
            return listCustomer;
        }
    }

}
