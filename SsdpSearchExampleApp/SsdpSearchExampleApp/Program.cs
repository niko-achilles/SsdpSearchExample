using Rssdp;
using System;
using System.Threading.Tasks;

namespace SsdpSearchExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            try
            {
                var t = p.SearchForDevices();
                t.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SearchForDevices()
        {
            using (var deviceLocator = new SsdpDeviceLocator())
            {

                //without argument search for all upnp enabled devices
                //possible arguments uuid [string] and or / search wait Time  
                var foundDevices = await deviceLocator.SearchAsync();

                foreach (var foundDevice in foundDevices)
                {
                    // Device data returned only contains basic device details and location
                    Console.WriteLine("Found " + foundDevice.Usn +
                        " at " + foundDevice.DescriptionLocation.ToString());

                    // Can retrieve the full device description easily though.
                    var fullDevice = await foundDevice.GetDeviceInfo();

                    Console.WriteLine(fullDevice.Manufacturer);
                    Console.WriteLine(fullDevice.ModelDescription);
                    Console.WriteLine();
                }
            }
        }
    }
}
