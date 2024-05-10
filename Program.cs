using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*int formid;

            Console.WriteLine("Form ID:");
            formid = Convert.ToInt32(Console.ReadLine());

            FormGen formGenObj = FormOprs.GetSingle(formid);

            FlowScheme flowschemObj = FlowSchemeOprs.GetSingle(formGenObj.SchemeId);

            Flow flowObj = new Flow();

            flowObj.FlowName = formGenObj.FormName;
            flowObj.FormId = formGenObj.FormId;

            string currSicilNo = "A20170619";

            //string currSicilNo = "A20170619";

            //System.Web.HttpContext.Current.Session["FlowSubmitter"] = currSicilNo;

            flowObj.FlowStarter = currSicilNo;
            flowObj.FlowLastSubmitter = currSicilNo;


            CheckPoint cpObj = new CheckPoint();

            cpObj.FormId = formGenObj.FormId;

            cpObj.CheckpointTemplate = formGenObj.FormTemplate;
            cpObj.CheckpointScheme = flowschemObj.Scheme;

            List<CheckPointLink> checkPointLinkList = new List<CheckPointLink>();

            checkPointLinkList = FlowOprs.GetChekPointLinkList(flowschemObj.Scheme);

            string port = "";*/

            Console.WriteLine("Akış ID:");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Sicil No:");
            string currSicilNo = Console.ReadLine();

            CheckPoint cpObj = FlowOprs.GetForm(id, currSicilNo);

            List<CheckPointLink> checkPointLinkList = new List<CheckPointLink>();

            checkPointLinkList = FlowOprs.GetChekPointLinkList(cpObj.CheckpointScheme);

            //Console.WriteLine("Son Aşama ID:");
            //string lastCPID = Console.ReadLine();

            if (string.IsNullOrEmpty(cpObj.CheckpointTarget))
            {
                cpObj.CheckpointTarget = checkPointLinkList.Where(c => c.Source.CPType == "FlowStart").First().Target.id;  //checkPointLinkList.First().Target.id;
                cpObj.CheckpointType = checkPointLinkList.Where(c => c.Source.CPType == "FlowStart").First().Source.CPType; //checkPointLinkList.First().Source.CPType;
            }
            /*else
            {
                cpObj.CheckpointTarget = lastCPID; 
                cpObj.CheckpointType = "FlowPoint";
            }*/
            
            cpObj.CheckpointSubmitter = currSicilNo;

            RunNextMethod(cpObj);

            Console.WriteLine("Akış tamamlandı!");
            Console.ReadLine();


            /*while (true)
            {
                if (lastCPID == cpObj.CheckpointTarget)
                {
                    if(cpObj.CheckpointType == "auto")
                    {
                        port = "true";
                    }
                    else
                    {
                        Console.WriteLine("True/False:");
                        port = Console.ReadLine();
                    }                    
                }
                else
                {                   
                    port = "true";
                }           

                IEnumerable<CheckPointLink> checkPointLinkListFiltered = checkPointLinkList.Where(c => c.Source.id == cpObj.CheckpointTarget && c.Source.port == port);

                cpObj.CheckpointTarget = checkPointLinkListFiltered.First().Target.id;
                if(checkPointLinkListFiltered.Count() > 0)
                {
                    cpObj.CheckpointType = checkPointLinkListFiltered.First().Source.CPType;
                }
                

                RunNextMethod(cpObj);
            }*/
            

            /*AlmanyaRezervasyon almanyaRezervasyon = new AlmanyaRezervasyon();
            BelcikaRezervasyon belcikaRezervasyon = new BelcikaRezervasyon();
            TurkiyeRezervasyon turkiyeRezervasyon = new TurkiyeRezervasyon();

            almanyaRezervasyon.BirSonrakiSorumlu = belcikaRezervasyon;
            belcikaRezervasyon.BirSonrakiSorumlu = turkiyeRezervasyon;

            almanyaRezervasyon.UygunSalonlariAra(new AramaKriteri { KatilimciSayisi = 15, Ulke = "Türkiye" });
            Console.ReadLine();*/
        }

        public static void RunNextMethod(CheckPoint criterion)
        {
            PassCheckPoint passCP = new PassCheckPoint();
            passCP.NextCheckMethod = passCP;

            passCP.NextCheckPoint(criterion);
        }

        public object GetInstance(string strFullyQualifiedName)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return Activator.CreateInstance(t);
        }

        public dynamic GetNextMethod(CheckPoint criterion)
        {
            CheckPointPort CPTestSource = new CheckPointPort();
            List<CheckPointLink> checkPointLinkList = new List<CheckPointLink>();
            checkPointLinkList = FlowOprs.GetChekPointLinkList(criterion.CheckpointScheme);

            //var CPTest = checkPointLinkList.Where(cp => cp.Source.id == criterion.CheckpointTarget).Last();
            CPTestSource = checkPointLinkList.Where(cp => cp.Source.id == criterion.CheckpointTarget).Last().Source; //<-- !!!

            return GetInstance(CPTestSource.CPMethodName);

        }

    }

}
