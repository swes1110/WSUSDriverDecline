using System;
using Microsoft.UpdateServices.Administration;
namespace WSUSDeclineUpdates
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create IUpdateServer variable and connect to WSUS Server

            //replace the "REPLACETHIS" with your WSUS Server Name, SSL or NOT, and Port. Syntax here is (SERVERNAME, REQUIRESSL, PORT)
            IUpdateServer server = AdminProxy.GetUpdateServer("REPLACETHIS", true, 443);

            //Begin loop thru updates and filter updates to just unapproved updates from beginning of time to current time
            foreach (IUpdate update in server.GetUpdates(ApprovedStates.NotApproved, DateTime.MinValue, DateTime.MaxValue, null, null))
            {
                //Convert UpdateType to string for comparison in following if statement
                string updatetype = update.UpdateType.ToString();

                //Filter for driver updates only
                if (updatetype == "Driver")
                {
                    //Write update name to console
                    Console.WriteLine("Declining update {0}", update.Title);
                    //Decline driver update
                    update.Decline();
                    //Write confimation to console
                    Console.WriteLine("Update Successfully Declined");
                }

            }


            //Wait at end of program
            string strWaitText = Environment.NewLine + "Press return to close";
            Console.WriteLine(strWaitText);
            Console.ReadLine();
        }
    }
}
