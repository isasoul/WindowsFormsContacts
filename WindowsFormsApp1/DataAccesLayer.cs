using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DataAccesLayer
    {
        string connectionString = "Server=LAPTOP-0U7CE3HJ\\SQLEXPRESS;Database=WinFormsContacts;Integrated Security=True;";
        
        private SqlConnection Connection;  // Deja esta línea sin inicializar. 

        public DataAccesLayer()
        {
            Connection = new SqlConnection(connectionString);  // Inicializa la conexión en el constructor.
        }

        public void InsertContact(Contact contact) 
        {
            try 
            {
                Connection.Open();
                string query = @"
                            INSERT INTO Contacts (FirstName, LasName, Phone, Address)
                                        VALUES (@FirstName, @LastName, @Phone, @Address)
                                        ";
                SqlParameter firstName = new SqlParameter();
                firstName.ParameterName = "@FirstName";
                firstName.Value = contact.FisrtName;
                firstName.DbType = System.Data.DbType.String;

                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query,Connection);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally 
            { Connection.Close(); }
        }

        public void UpdateContact (Contact contact)
        {
            try { 

                Connection.Open();
                string query = @" UPDATE Contacts
                                 SET FirstName = @FirstName,
                                     lasname = @LastName,
                                     Phone =  @Phone,
                                     Address = @Address
                                     WHERE Id = @Id ";
                                   
                SqlParameter id = new SqlParameter("@Id", contact.Id);
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FisrtName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query,Connection);
                command.Parameters.Add(id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (Exception) {

                throw;
            }
            finally { Connection.Close(); } 
        }

        public void DeleteContact(int id)
        {
            try
            {
                Connection.Open();
                string query = @"DELETE FROM Contacts WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query,Connection);
                command.Parameters.Add(new SqlParameter("@Id", id));

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { Connection.Close(); }
        }
        public List<Contact> GetContacts(string search = null) 
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                Connection.Open();
                string query = @" SELECT Id, FirstName, LasName, Phone, Address 
                                FROM Contacts ";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(search)) 
                {
                    query += @" WHERE FirstName LIKE @Search OR Lasname LIKE @Search OR Phone LIKE @Search OR
                                Address LIKE @Search ";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                command.CommandText = query;
                command.Connection = Connection;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) 
                {
                    contacts.Add(new Contact
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FisrtName = reader["FirstName"].ToString(),
                        LastName = reader["LasName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                    });
                }
            }
            catch (Exception)
                {
                  throw;
                }
                finally { Connection.Close(); }
                return contacts;
                
        }
    }
}
