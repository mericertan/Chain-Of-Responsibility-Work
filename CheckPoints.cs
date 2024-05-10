using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Models;

namespace ConsoleApplication1
{
    public class Basla : FlowCheckBase
    {
        protected override void checkpointConfirm(object sender, CheckPoint criterion)
        {
            NextCheckMethod?.NextCheckPoint(criterion);
        }
    }

    public class Gonder : FlowCheckBase
    {
        protected override void checkpointConfirm(object sender, CheckPoint criterion)
        {


            Console.WriteLine("Gönder çalıştı");

            /*if(CPTestSource.CPMethodName == "Gonder" && )
            {
                NextCheckMethod?.NextCheckPoint(criterion);
            }*/


        }
    }

    public class PassCheckPoint : FlowCheckBase
    {
        protected override void checkpointConfirm(object sender, CheckPoint criterion)
        {
            CheckPointPort CPTestSource = new CheckPointPort();
            List<CheckPointLink> checkPointLinkList = new List<CheckPointLink>();
            checkPointLinkList = FlowOprs.GetChekPointLinkList(criterion.CheckpointScheme);
            
            //var CPTest = checkPointLinkList.Where(cp => cp.Source.id == criterion.CheckpointTarget).Last();
            CPTestSource = checkPointLinkList.Where(cp => cp.Source.id == criterion.CheckpointTarget).Last().Source;
            
            string port = CPTestSource.port;

            switch (CPTestSource.CPMethodName)
            {
                case "AutoPass":
                    Console.WriteLine("AutoPass" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);
                    break;

                case "CheckFlow":
                    Console.WriteLine("CheckFlow" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);
                    break;

                case "EPostaGonder":
                    Console.WriteLine("EPostaGonder" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);
                    break;

                case "GenelOnay":
                    Console.WriteLine("GenelOnay" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);
                    break;

                case "GenelOnayGrup":
                    Console.WriteLine("GenelOnayGrup" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);
                    break;

                case "Gonder":
                    Console.WriteLine("Gonder" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);                    
                    break;

                case "GonderenDBveUstuMu":
                    Console.WriteLine("GonderenDBveUstuMu" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);                    
                    break;

                case "GonderenMDveUstuMu":
                    Console.WriteLine("GonderenMDveUstuMu" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);                    
                    break;

                case "GonderenYoneticiMi":
                    Console.WriteLine("GonderenYoneticiMi" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);                    
                    break;

                case "MesaiTalebiOnay":
                    Console.WriteLine("MesaiTalebiOnay" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);                    
                    break;

                case "TalepYap":
                    Console.WriteLine("TalepYap" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);
                    break;

                case "TestFlow":
                    Console.WriteLine("TestFlow" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);
                    break;

                case "VeriAmbariOnay":
                    Console.WriteLine("VeriAmbariOnay" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);                    
                    break;

                case "YoneticiOnay":
                    Console.WriteLine("YoneticiOnay" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);
                    break;

                case "ArizaTeknikServisOnay":
                    Console.WriteLine("ArizaTeknikServisOnay" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);                    
                    break;

                case "EnerjiTesisIsEmriEkle":
                    Console.WriteLine("EnerjiTesisIsEmriEkle" + " " + CPTestSource.CPConfirmType + " Aşama ID:" + CPTestSource.id);                    
                    break;

                default:

                    Console.WriteLine("None");

                    break;

            }



            /*if (CPTestSource.CPConfirmType == "auto" || CPTestSource.CPType == "FlowStart")
            {
                port = "true";

            }
            else
            {*/
                Console.WriteLine("True/False:");
                port = Console.ReadLine();

            //}

            IEnumerable<CheckPointLink> checkPointLinkListFiltered = checkPointLinkList.Where(c => c.Source.id == criterion.CheckpointTarget && c.Source.port == port);

            criterion.CheckpointTarget = checkPointLinkListFiltered.First().Target.id;
            if (checkPointLinkListFiltered.Count() > 0)
            {
                criterion.CheckpointType = checkPointLinkListFiltered.First().Source.CPType;
                NextCheckMethod?.NextCheckPoint(criterion);
            }

            
        }
    }

    public class CheckPoints
    {

    }
}
