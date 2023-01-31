using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report21
{
    public class Report21ViewModel
    {

        public string date { get; set; }

        public string warehouse_Name { get; set; }

        public string locationType_Name { get; set; }

        public string yn { get; set; }

        public string update_By { get; set; }

        public string update_Date { get; set; }

        public string zone_Name { get; set; }

        public string location_Id { get; set; }

        public long? rowIndex { get; set; }

        public bool checkQuery { get; set; }

        public string blockPut { get; set; }

        public string blockPick { get; set; }

        public string value { get; set; }
       

}
    public class LocationTypeViewModel
    {
        public Guid? locationType_Index { get; set; }

        public string locationType_Name { get; set; }
        
        public List<Guid?> listlocationType_Index { get; set; }

    }

    public class YnViewModel
    {

        public string yn { get; set; }

        public List<string> listyn { get; set; }

    }

    public class BlockViewModel
    {
        public string blockPut { get; set; }

        public string blockPick { get; set; }

        public List<string> listblock { get; set; }

    }




}
