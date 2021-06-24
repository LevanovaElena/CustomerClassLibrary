using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.Repositories
{
    public class AddressRepository : BaseRepository,IAddressRepository
    {
        public int Create(Address address,int idCustomer)
        {
            int idAddress = 0;
            using (var connection = this.GetConnection())
            {
                connection.Open();
                SqlCommand command = new();
                command.Connection = connection;

                //insert command
                command.CommandText =
                    "INSERT INTO dbo.address_customer (customer_id,address_line,address_line2,address_type,city,postal_code,[state],country)" +
                    " VALUES (@customer_id, @address_line, @address_line2, @address_type, @city, @postal_code, @state, @country);" +
                    " SELECT CAST(scope_identity() AS int)";



                command.Parameters.Add(new SqlParameter("@customer_id", SqlDbType.Int)
                {
                    Value = idCustomer
                });

                command.Parameters.Add(new SqlParameter("@address_line", SqlDbType.VarChar, 100)
                {
                    Value = address.AddressLine
                });

                command.Parameters.Add(new SqlParameter("@address_line2", SqlDbType.VarChar, 100)
                {
                    Value = address.AddressLine2
                });
                command.Parameters.Add(new SqlParameter("@address_type", SqlDbType.VarChar, 15)
                {
                    Value = address.TypeAddress
                });
                command.Parameters.Add(new SqlParameter("@city", SqlDbType.VarChar, 50)
                {
                    Value = address.City
                });
                command.Parameters.Add(new SqlParameter("@postal_code", SqlDbType.Char, 6)
                {
                    Value = address.PostalCode
                });
                command.Parameters.Add(new SqlParameter("@state", SqlDbType.VarChar, 20)
                {
                    Value = address.State
                });
                command.Parameters.Add(new SqlParameter("@country", SqlDbType.VarChar, 255)
                {
                    Value = address.Country
                });


                idAddress = (Int32)command.ExecuteScalar();

                connection.Close();
            }

            return idAddress;
        }

        public Address ReadByIdAddress(int idAddress)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand(
                    "SELECT * FROM address_customer WHERE address_customer.address_id = @address_id", connection);

                var customerIdParam = new SqlParameter("@address_id", SqlDbType.Int)
                {
                    Value = idAddress
                };

                command.Parameters.Add(customerIdParam);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Address()
                        {
                            AddressLine = reader["address_line"]?.ToString(),
                            AddressLine2 = reader["address_line2"]?.ToString(),
                            TypeAddress = (AddressType)Enum.Parse(typeof(AddressType),reader["address_type"].ToString()),
                            City = reader["city"]?.ToString(),
                            PostalCode = reader["postal_code"]?.ToString(),
                            State = reader["state"]?.ToString(),
                            Country = reader["country"]?.ToString(),
                            IdAddress = idAddress
                        };
                    }
                }

            }
            return null;
        }

        public void Update(Address address,int idCustomer)
        {
            using (var connection = this.GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                //insert command
                command.CommandText =
                    "UPDATE address_customer" + 
                      " SET"+ 
                        " customer_id=@customer_id,"+
                        " address_line =@address_line,"+
                        " address_line2 =@address_line2,"+
                        " address_type =@address_type,"+
                        " city =@city,"+
                        " postal_code =@postal_code,"+
                        " [state]=@state,"+
                        " country =@country"+
                     " WHERE"+ 
                        " address_id =@address_id";


                command.Parameters.Add(new SqlParameter("@customer_id", SqlDbType.Int)
                {
                    Value = idCustomer
                });

                command.Parameters.Add(new SqlParameter("@address_line", SqlDbType.VarChar, 100)
                {
                    Value = address.AddressLine
                });

                command.Parameters.Add(new SqlParameter("@address_line2", SqlDbType.VarChar, 100)
                {
                    Value = address.AddressLine2
                });
                command.Parameters.Add(new SqlParameter("@address_type", SqlDbType.VarChar, 15)
                {
                    Value = address.TypeAddress
                });
                command.Parameters.Add(new SqlParameter("@city", SqlDbType.VarChar, 50)
                {
                    Value = address.City
                });
                command.Parameters.Add(new SqlParameter("@postal_code", SqlDbType.Char, 6)
                {
                    Value = address.PostalCode
                });
                command.Parameters.Add(new SqlParameter("@state", SqlDbType.Char, 20)
                {
                    Value = address.State
                });
                command.Parameters.Add(new SqlParameter("@country", SqlDbType.VarChar, 255)
                {
                    Value = address.Country
                });
                command.Parameters.Add(new SqlParameter("@address_id", SqlDbType.Int)
                {
                    Value = address.IdAddress
                });

                command.ExecuteNonQuery();


                connection.Close();
            }
        }

        public void DeleteAllByCustomer(int idCustomer)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand(
                    "DELETE FROM address_customer WHERE customer_id = @customer_id", connection);

                var customerIdParam = new SqlParameter("@customer_id", SqlDbType.Int)
                {
                    Value = idCustomer
                };

                command.Parameters.Add(customerIdParam);
                command.ExecuteNonQuery();

            }
        }

        public List<Address> ReadByIdCustomer(int idCustomer)
        {
            List<Address> addressList = new List<Address>();
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand(
                    "SELECT * FROM address_customer WHERE customer_id = @customer_id", connection);

                 var customerIdParam = new SqlParameter("@customer_id", SqlDbType.Int)
                 {
                     Value = idCustomer
                 };

                command.Parameters.Add(customerIdParam);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        addressList.Add(new Address()
                        {
                            AddressLine = reader["address_line"]?.ToString(),
                            AddressLine2 = reader["address_line2"]?.ToString(),
                            TypeAddress = (AddressType)Enum.Parse(typeof(AddressType), reader["address_type"].ToString()),
                            City = reader["city"]?.ToString(),
                            PostalCode = reader["postal_code"]?.ToString(),
                            State = reader["state"]?.ToString(),
                            Country = reader["country"]?.ToString(),
                            IdAddress = int.Parse(reader["address_id"]?.ToString()),
                            IdCustomer = idCustomer
                        });
                    }
                }

            }
            return addressList;
        }
    }
}
