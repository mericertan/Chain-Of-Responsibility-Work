using ConsoleApplication1.Helpers;
using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class FormOprs
    {
        static string conString = "Data Source=10.0.3.208;Initial Catalog=emasadb_son;User ID=portalklkuser;Password=P@rtal2019*";

        public static GeneralReturn Add(FormGen formGenObj)
        {
            GeneralReturn returnObj = new GeneralReturn();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = "INSERT INTO FORMTEMPLATES (FORMNAME, FORMTEMPLATE, SCHEMEID, FORMISYERIKODLARI, FORMSAHIBIISYERIKODLARI) VALUES (@FORMNAME, @FORMTEMPLATE, @SCHEMEID, @FORMISYERIKODLARI, @FORMSAHIBIISYERIKODLARI)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FORMNAME", formGenObj.FormName);
                    command.Parameters.AddWithValue("@FORMTEMPLATE", formGenObj.FormTemplate);
                    command.Parameters.AddWithValue("@SCHEMEID", formGenObj.SchemeId);
                    command.Parameters.AddWithValue("@FORMISYERIKODLARI", (object)formGenObj.FormIsYeriKodlari ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FORMSAHIBIISYERIKODLARI", (object)formGenObj.FormSahibiIsYeriKodlari ?? DBNull.Value);

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

        public static GeneralReturn Update(FormGen formGenObj)
        {
            GeneralReturn returnObj = new GeneralReturn();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = "UPDATE FORMTEMPLATES SET FORMNAME = @FORMNAME, FORMTEMPLATE = @FORMTEMPLATE, SCHEMEID = @SCHEMEID, FORMISYERIKODLARI = @FORMISYERIKODLARI, FORMSAHIBIISYERIKODLARI = @FORMSAHIBIISYERIKODLARI WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", formGenObj.FormId);
                    command.Parameters.AddWithValue("@FORMNAME", formGenObj.FormName);
                    command.Parameters.AddWithValue("@FORMTEMPLATE", formGenObj.FormTemplate);
                    command.Parameters.AddWithValue("@SCHEMEID", formGenObj.SchemeId);
                    command.Parameters.AddWithValue("@FORMISYERIKODLARI", (object)formGenObj.FormIsYeriKodlari ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FORMSAHIBIISYERIKODLARI", (object)formGenObj.FormSahibiIsYeriKodlari ?? DBNull.Value);

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

        public static FormGen GetSingle(int id)
        {

            FormGen returnObj = new FormGen();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = "SELECT * FROM FORMTEMPLATES WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id.ToString());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        returnObj.FormId = Convert.ToInt32(reader["ID"]);
                        returnObj.FormName = reader["FORMNAME"].ToString();
                        returnObj.FormTemplate = reader["FORMTEMPLATE"].ToString();
                        returnObj.SchemeId = Convert.ToInt32(reader["SCHEMEID"]);
                        returnObj.FormIsYeriKodlari = reader["FORMISYERIKODLARI"].ToString();
                        returnObj.FormSahibiIsYeriKodlari = reader["FORMSAHIBIISYERIKODLARI"].ToString();

                    }

                    command.Dispose();
                }
                connection.Close();
            }

            return returnObj;
        }

        public static List<FormGen> GetMultiple(string formisyerikodu = "", string formsahibiisyerikodlari = "")
        {
            List<FormGen> returnList = new List<FormGen>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                string qByAnd = "";
                string qByIsyeriKodu = "";
                string qByIsyeriSahibiKodu = "";

                if (!string.IsNullOrEmpty(formisyerikodu))
                {
                    qByIsyeriKodu = "([FORMISYERIKODLARI] IS NULL ";
                    qByIsyeriKodu += " OR CHARINDEX('" + formisyerikodu + "', [FORMISYERIKODLARI]) > 0";
                    qByIsyeriKodu += " OR  CHARINDEX('" + formisyerikodu.SplitWithChunk(4)[0] + "0000" + "', [FORMISYERIKODLARI]) > 0";
                    qByIsyeriKodu += " OR  CHARINDEX('" + formisyerikodu.SplitWithChunk(2)[0] + "000000" + "', [FORMISYERIKODLARI]) > 0)";
                }

                if (!string.IsNullOrEmpty(formsahibiisyerikodlari))
                {
                    qByIsyeriSahibiKodu = "([FORMSAHIBIISYERIKODLARI] LIKE '" + formsahibiisyerikodlari.TrimEnd(new Char[] { '0' }) + "%')";
                }

                if (!string.IsNullOrEmpty(qByIsyeriKodu) || !string.IsNullOrEmpty(qByIsyeriSahibiKodu))
                {
                    qByIsyeriKodu = "WHERE " + qByIsyeriKodu;
                }

                if (!string.IsNullOrEmpty(qByIsyeriKodu) && !string.IsNullOrEmpty(qByIsyeriSahibiKodu))
                {
                    qByAnd = " AND ";
                }

                String query = "SELECT * FROM FORMTEMPLATES " + qByIsyeriKodu + qByAnd + qByIsyeriSahibiKodu + " ORDER BY ID DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        returnList.Add(new FormGen()
                        {
                            FormId = Convert.ToInt32(reader["ID"]),
                            FormName = reader["FORMNAME"].ToString(),
                            FormTemplate = reader["FORMTEMPLATE"].ToString(),
                            SchemeId = Convert.ToInt32(reader["SCHEMEID"]),
                            FormIsYeriKodlari = reader["FORMISYERIKODLARI"].ToString(),
                            FormSahibiIsYeriKodlari = reader["FORMSAHIBIISYERIKODLARI"].ToString()
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
                String query = "DELETE FROM FORMTEMPLATES WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id.ToString());

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                    {
                        returnObj.ReturnCode = "998";
                        returnObj.ReturnDesc = "Veri kaldırma sırasında hata oluştu!";
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
