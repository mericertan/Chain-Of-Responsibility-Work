using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConsoleApplication1
{
    public static class FlowSchemeOprs
    {
        static string conString = "Data Source=10.0.3.208;Initial Catalog=emasadb;User ID=portalklkuser;Password=P@rtal2019*";

        public static GeneralReturn Add(FlowScheme flowObj)
        {
            GeneralReturn returnObj = new GeneralReturn();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = "INSERT INTO FLOWSCHEMES (SCHEMENAME, SCHEME) VALUES (@SCHEMENAME, @SCHEME)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SCHEMENAME", flowObj.SchemeName);
                    command.Parameters.AddWithValue("@SCHEME", flowObj.Scheme);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                    {
                        returnObj.ReturnCode = "998";
                        returnObj.ReturnDesc = "Veri girilirken hata oluştu!";
                    }
                    else
                    {
                        returnObj.ReturnCode = "999";
                        returnObj.ReturnDesc = "OK";
                    }
                }
            }

            return returnObj;
        }

        public static GeneralReturn Update(FlowScheme flowObj)
        {
            GeneralReturn returnObj = new GeneralReturn();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = "UPDATE FLOWSCHEMES SET SCHEMENAME = @SCHEMENAME, SCHEME = @SCHEME WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", flowObj.SchemeId);
                    command.Parameters.AddWithValue("@SCHEMENAME", flowObj.SchemeName);
                    command.Parameters.AddWithValue("@SCHEME", flowObj.Scheme);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                    {
                        returnObj.ReturnCode = "998";
                        returnObj.ReturnDesc = "Veri girilirken hata oluştu!";
                    }
                    else
                    {
                        returnObj.ReturnCode = "999";
                        returnObj.ReturnDesc = "OK";
                    }
                }
            }

            return returnObj;
        }

        public static FlowScheme GetSingle(int id)
        {
            FlowScheme returnObj = new FlowScheme();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = "SELECT * FROM FLOWSCHEMES WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id.ToString());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        returnObj.SchemeId = Convert.ToInt32(reader["ID"]);
                        returnObj.SchemeName = reader["SCHEMENAME"].ToString();
                        returnObj.Scheme = reader["SCHEME"].ToString();

                    }

                    command.Dispose();
                }
                connection.Close();
            }

            return returnObj;
        }

        public static List<FlowScheme> GetMultiple()
        {
            List<FlowScheme> returnList = new List<FlowScheme>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = "SELECT * FROM FLOWSCHEMES";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        returnList.Add(new FlowScheme()
                        {
                            SchemeId = Convert.ToInt32(reader["ID"]),
                            SchemeName = reader["SCHEMENAME"].ToString(),
                            Scheme = reader["SCHEME"].ToString()
                        });

                    }

                    command.Dispose();
                }
                connection.Close();
            }

            return returnList;
        }

        public static List<SelectListItem> GetSelectList()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = "SELECT * FROM FLOWSCHEMES";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        returnList.Add(new SelectListItem()
                        {
                            Text = reader["SCHEMENAME"].ToString(),
                            Value = reader["ID"].ToString()
                        });

                    }

                    command.Dispose();
                }
                connection.Close();
            }

            return returnList;
        }

        public static GeneralReturn Delete(int id)
        {
            GeneralReturn returnObj = new GeneralReturn();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = "DELETE FROM FLOWSCHEMES WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id.ToString());

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                    {
                        returnObj.ReturnCode = "998";
                        returnObj.ReturnDesc = "Veri kaldırdma sırasında hata oluştu!";
                    }
                    else
                    {
                        returnObj.ReturnCode = "999";
                        returnObj.ReturnDesc = "OK";
                    }
                }
            }

            return returnObj;
        }


    }
}
