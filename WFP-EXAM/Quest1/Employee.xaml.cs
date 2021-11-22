using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WFP_EXAM.Quest1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Employee : Page
    {
        public Employee()
        {
            this.InitializeComponent();
        }

        private List<HidDeviceClass> HidDeviceClasses;

        public class HidDeviceClass
        {
            public HidDeviceClass(string usageName, ushort pageID, ushort usageID)
            {
                UsageName = usageName;
                PageID = pageID;
                UsageID = usageID;
            }

            public string UsageName { get; set; }
            public ushort PageID { get; set; }
            public ushort UsageID { get; set; }
        }
        private void GetUagePageInfo()
        {
            using (StreamReader file = File.OpenText(".\\usagepage.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject jObject = (JObject)JToken.ReadFrom(reader);
                var hidDeviceClasses =
                    from p in jObject["hidDeviceClasses"]
                    select new HidDeviceClass((string)p["UsageName"], (ushort)p["PageID"], (ushort)p["UsageID"]);
                HidDeviceClasses = hidDeviceClasses.ToList<HidDeviceClass>();
                cbDevType.DataContext = HidDeviceClasses;
            }

        }

    }

}