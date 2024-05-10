using ConsoleApplication1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApplication1
{
    public static class FlowOprs
    {
        static string conString = "Data Source=10.0.3.208;Initial Catalog=emasadb;User ID=portalklkuser;Password=P@rtal2019*";


        public static GeneralReturn AddFlowCheckPoint(CheckPoint flowObj)
        {
            GeneralReturn returnObj = new GeneralReturn();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                String query = @"INSERT INTO CHECKPOINTS (FLOWID, CHECKPOINTTEMPLATE, CHECKPOINTVALUES, CHECKPOINTSCHEME, CHECKPOINTSOURCE, CHECKPOINTTARGET, CHECKPOINTTYPE, CHECKPOINTSUBMITTER, CHECKPOINTPREVSUBMITTER, STATUS, STATUSDESC, LASTUPDATETIME) 
                                VALUES (@FLOWID, @CHECKPOINTTEMPLATE, @CHECKPOINTVALUES, @CHECKPOINTSCHEME, @CHECKPOINTSOURCE, @CHECKPOINTTARGET, @CHECKPOINTTYPE, @CHECKPOINTSUBMITTER, @CHECKPOINTPREVSUBMITTER, @STATUS, @STATUSDESC, @LASTUPDATETIME)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FLOWID", flowObj.FlowId);
                    command.Parameters.AddWithValue("@CHECKPOINTTEMPLATE", flowObj.CheckpointTemplate);
                    command.Parameters.AddWithValue("@CHECKPOINTVALUES", (flowObj.CheckpointValues != null ? flowObj.CheckpointValues : ""));
                    command.Parameters.AddWithValue("@CHECKPOINTSCHEME", flowObj.CheckpointScheme);
                    command.Parameters.AddWithValue("@CHECKPOINTSOURCE", (flowObj.CheckpointSource != null ? flowObj.CheckpointSource : ""));
                    command.Parameters.AddWithValue("@CHECKPOINTTARGET", flowObj.CheckpointTarget);
                    command.Parameters.AddWithValue("@CHECKPOINTTYPE", (flowObj.CheckpointType != null ? flowObj.CheckpointType : ""));
                    command.Parameters.AddWithValue("@CHECKPOINTSUBMITTER", (flowObj.CheckpointSubmitter != null ? flowObj.CheckpointSubmitter : ""));
                    command.Parameters.AddWithValue("@CHECKPOINTPREVSUBMITTER", (flowObj.CheckpointPrevSubmitter != null ? flowObj.CheckpointPrevSubmitter : ""));
                    command.Parameters.AddWithValue("@STATUS", flowObj.Status);
                    command.Parameters.AddWithValue("@STATUSDESC", (flowObj.StatusDesc != null ? flowObj.StatusDesc : ""));
                    command.Parameters.AddWithValue("@LASTUPDATETIME", DateTime.Now);

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

                        returnObj = UpdateFlowStatus(flowObj);
                    }

                    connection.Close();
                }
            }

            return returnObj;
        }

        public static List<CheckPointLink> GetChekPointLinkList(string json)
        {
            JObject jObject = JObject.Parse(json);

            List<JToken> cells = jObject["cells"].Where(j => j["type"].ToString() == "link").ToList();

            List<CheckPointLink> checkPointLinkList = new List<CheckPointLink>();

            foreach (JToken jToken in cells)
            {

                JToken jTokenSource = jObject["cells"].Where(j => j["id"].ToString() == jToken["source"]["id"].ToString()).First()["flowprops"];
                IDictionary<string, JToken> dicJTokenSource = (JObject)jTokenSource;

                List<CheckPointField> sourceCParameters = null;


                //if (dicJTokenSource.ContainsKey("methodparams"))
                if (jTokenSource["methodparams"].ToString() != "[]")
                {
                    //sourceCParameters = new JavaScriptSerializer().Deserialize<List<CheckPointField>>(dicJTokenSource["methodparams"].ToString());
                    sourceCParameters = new JavaScriptSerializer().Deserialize<List<CheckPointField>>(jTokenSource["methodparams"].ToString());
                }
                else
                {
                    sourceCParameters = null;
                }

                CheckPointPort jSource = new CheckPointPort()
                {
                    id = jToken["source"]["id"].ToString(),
                    port = jToken["source"]["port"].ToString(),
                    CPMethodName = dicJTokenSource.ContainsKey("flowmethod") ? dicJTokenSource["flowmethod"].ToString() : "", //jObject["cells"].Where(j => j["id"].ToString() == jToken["source"]["id"].ToString()).Select(j => j["flowprops"]["flowmethod"].ToString()).First(),
                    CPMethodTitle = dicJTokenSource.ContainsKey("methodtitle") ? dicJTokenSource["methodtitle"].ToString() : "",
                    CPParameters = sourceCParameters,
                    CPType = dicJTokenSource.ContainsKey("cptype") ? dicJTokenSource["cptype"].ToString() : "",
                    CPConfirmType = dicJTokenSource.ContainsKey("confirmtype") ? dicJTokenSource["confirmtype"].ToString() : "",
                    CPSubmitter = dicJTokenSource.ContainsKey("submitter") ? dicJTokenSource["submitter"].ToString() : "",
                    CPSubmitterIsGroup = dicJTokenSource.ContainsKey("submittergroup") ? Convert.ToBoolean(dicJTokenSource["submittergroup"].ToString()) : false,
                    TruePortDesc = dicJTokenSource.ContainsKey("trueportDesc") ? dicJTokenSource["trueportDesc"].ToString() : "",
                    FalsePortDesc = dicJTokenSource.ContainsKey("falseportDesc") ? dicJTokenSource["falseportDesc"].ToString() : ""
                };

                JToken jTokenTarget = jObject["cells"].Where(j => j["id"].ToString() == jToken["target"]["id"].ToString()).First()["flowprops"];
                IDictionary<string, JToken> dicJTokenTarget = (JObject)jTokenTarget;

                List<CheckPointField> targetCParameters = null;

                //if (dicJTokenTarget.ContainsKey("methodparams"))
                if (jTokenTarget["methodparams"].ToString() != "[]")
                {
                    //targetCParameters = new JavaScriptSerializer().Deserialize<List<CheckPointField>>(dicJTokenTarget["methodparams"].ToString());
                    targetCParameters = new JavaScriptSerializer().Deserialize<List<CheckPointField>>(jTokenTarget["methodparams"].ToString());
                }
                else
                {
                    targetCParameters = null;
                }


                CheckPointPort jTarget = new CheckPointPort()
                {
                    id = jToken["target"]["id"].ToString(),
                    port = jToken["target"]["port"].ToString(),
                    CPMethodName = dicJTokenTarget.ContainsKey("flowmethod") ? dicJTokenTarget["flowmethod"].ToString() : "",
                    CPMethodTitle = dicJTokenTarget.ContainsKey("methodtitle") ? dicJTokenTarget["methodtitle"].ToString() : "",
                    CPParameters = targetCParameters,
                    CPType = dicJTokenTarget.ContainsKey("cptype") ? dicJTokenTarget["cptype"].ToString() : "",
                    CPConfirmType = dicJTokenTarget.ContainsKey("confirmtype") ? dicJTokenTarget["confirmtype"].ToString() : "",
                    CPSubmitter = dicJTokenTarget.ContainsKey("submitter") ? dicJTokenTarget["submitter"].ToString() : "",
                    CPSubmitterIsGroup = dicJTokenSource.ContainsKey("submittergroup") ? Convert.ToBoolean(dicJTokenTarget["submittergroup"].ToString()) : false,
                    TruePortDesc = dicJTokenSource.ContainsKey("trueportDesc") ? dicJTokenSource["trueportDesc"].ToString() : "",
                    FalsePortDesc = dicJTokenSource.ContainsKey("falseportDesc") ? dicJTokenSource["falseportDesc"].ToString() : ""
                };

                checkPointLinkList.Add(new CheckPointLink()
                {
                    Source = jSource,
                    Target = jTarget
                });

            }

            return checkPointLinkList;
        }
            
        public static CheckPoint GetForm(int id, string sicilno, bool forsubmit = true)
        {

            CheckPoint returnObj = new CheckPoint();
            int eachFlowCount = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                //String query = "SELECT TOP 1 * FROM CHECKPOINTS WHERE FLOWID = @FLOWID AND (CHECKPOINTSUBMITTER = @CHECKPOINTSUBMITTER OR CHECKPOINTSUBMITTER IN(SELECT CONVERT(varchar(20), [GROUPID]) FROM APPROVERS WHERE EMPID = @CHECKPOINTSUBMITTER)) AND CHECKPOINTPREVSUBMITTER IS NOT NULL ORDER BY ID DESC";

                String query = @"SELECT TOP 1 f.FLOWSTARTER, b.* FROM ( SELECT (SELECT COUNT(FLOWID) FROM CHECKPOINTS WHERE FLOWID = a.FLOWID) as [EACHFLOWCOUNT], * FROM CHECKPOINTS as a ) as b, FLOWS as f 
                                WHERE FLOWID = @FLOWID";
                if (forsubmit)
                {
                    query += " AND (CHECKPOINTSUBMITTER = @CHECKPOINTSUBMITTER OR CHECKPOINTSUBMITTER IN(SELECT CONVERT(varchar(20), [GROUPID]) FROM APPROVERS WHERE EMPID = @CHECKPOINTSUBMITTER))";
                }
                else
                {
                    query += " AND f.FLOWLASTSUBMITTER = @CHECKPOINTSUBMITTER";
                }
                query += " ORDER BY ID DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FLOWID", id.ToString());
                    command.Parameters.AddWithValue("@CHECKPOINTSUBMITTER", sicilno);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        returnObj.FlowId = Convert.ToInt32(reader["FLOWID"]);
                        returnObj.CheckpointTemplate = reader["CHECKPOINTTEMPLATE"].ToString();
                        returnObj.CheckpointValues = reader["CHECKPOINTVALUES"].ToString();
                        returnObj.CheckpointScheme = reader["CHECKPOINTSCHEME"].ToString();
                        returnObj.CheckpointSource = reader["CHECKPOINTSOURCE"].ToString();
                        returnObj.CheckpointTarget = reader["CHECKPOINTTARGET"].ToString();
                        returnObj.CheckpointType = reader["CHECKPOINTTYPE"].ToString();
                        returnObj.CheckpointSubmitter = reader["CHECKPOINTSUBMITTER"].ToString();
                        returnObj.CheckpointPrevSubmitter = reader["CHECKPOINTPREVSUBMITTER"].ToString();
                        returnObj.Status = (object)reader["STATUS"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["STATUS"]);
                        eachFlowCount = Convert.ToInt32(reader["EACHFLOWCOUNT"]);
                        returnObj.LastUpdateTime = reader["LASTUPDATETIME"].ToString();
                    }

                    command.Dispose();
                }
                connection.Close();
            }

            if (string.IsNullOrEmpty(returnObj.CheckpointPrevSubmitter) && eachFlowCount > 1 && forsubmit)
            {
                returnObj = new CheckPoint();
            }

            return returnObj;
        }

        public static GeneralReturn UpdateFlowStatus(CheckPoint cpObj)
    {
        GeneralReturn returnObj = new GeneralReturn();

        using (SqlConnection connection = new SqlConnection(conString))
        {
            string lastsubmitterQry = cpObj.CheckpointSubmitter != null ? "FLOWLASTSUBMITTER = @FLOWLASTSUBMITTER, " : "";

            String query = "UPDATE FLOWS SET CHECKPOINTTYPE = @CHECKPOINTTYPE, " + lastsubmitterQry + "FLOWLASTPREVSUBMITTER = @FLOWLASTPREVSUBMITTER, LASTUPDATETIME = @LASTUPDATETIME, LASTSTATUS = @LASTSTATUS, LASTSTATUSDESC = @LASTSTATUSDESC WHERE ID = @FLOWID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FLOWID", cpObj.FlowId);
                command.Parameters.AddWithValue("@CHECKPOINTTYPE", cpObj.CheckpointType);
                if (cpObj.CheckpointSubmitter != null)
                {
                    command.Parameters.AddWithValue("@FLOWLASTSUBMITTER", cpObj.CheckpointSubmitter);
                }
                command.Parameters.AddWithValue("@FLOWLASTPREVSUBMITTER", cpObj.CheckpointPrevSubmitter);
                command.Parameters.AddWithValue("@LASTUPDATETIME", DateTime.Now);
                command.Parameters.AddWithValue("@LASTSTATUS", cpObj.Status);
                command.Parameters.AddWithValue("@LASTSTATUSDESC", cpObj.StatusDesc);

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

                connection.Close();
            }


        }

        return returnObj;
    }
    }    
}
