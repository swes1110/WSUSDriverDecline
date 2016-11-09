using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WSUSDeclineUpdates
{
    using System;
    using System.Xml;

    using Microsoft.UpdateServices.Administration;

    class Program
    {
        static void Main(string[] args)
        {
            IUpdateServer server = AdminProxy.GetUpdateServer("ltpatches.theabfm.org", true, 8443);

            foreach (IUpdate update in server.GetUpdates(ApprovedStates.NotApproved, DateTime.MinValue, DateTime.MaxValue, null, null))
            {
                string updatetype = update.UpdateType.ToString();
                if(false == update.IsDeclined && false == update.IsApproved && updatetype == "Driver")
                {
                    update.Decline();
                    Console.WriteLine("\n{0} \n {1}",update.Title,update.UpdateType);
                }
                
            }


            //Wait at end of program
            string strWaitText = Environment.NewLine + "Press return to close";
            Console.WriteLine(strWaitText);
            Console.ReadLine();
        }
    }
}
