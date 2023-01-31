using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report9
{
    public class Report9ViewModel
    {
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string productCategory_Name { get; set; }
        public string ref_No3 { get; set; }
        public string itemStatus_Id { get; set; }
        public string itemStatus_Name { get; set; }
        public decimal? qty_Bal { get; set; }
        public decimal? qty_Count { get; set; }
        public decimal? qty_Diff { get; set; }
        public string count_status { get; set; }
        public string cycleCount_Date { get; set; }
        public string cycleCount_date { get; set; }
        public string cycleCount_date_To { get; set; }
        public string create_Date { get; set; }
        public string create_By { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public Guid? userGroup_Index { get; set; }

        public string userGroup_Id { get; set; }
        public string userGroup_Name { get; set; }
        public Guid? zone_Index { get; set; }

        public string zone_Id { get; set; }

        public string zone_Name { get; set; }

    }

    public class ConfigUserGroupMenuViewModel
    {
        public Guid userGroupMenu_Index { get; set; }

        public string userGroupMenu_Id { get; set; }

        public Guid? userGroup_Index { get; set; }
        public bool isActive { get; set; }


        public Guid? menu_Index { get; set; }

        public Guid? sub_Menu_Index { get; set; }

        public Guid? menuType_Index { get; set; }

        public string menu_Id { get; set; }
        public string menuControl_Name { get; set; }
        public string menu_Name { get; set; }
        public string menu_SecondName { get; set; }
        public string menu_ThirdName { get; set; }

        public List<Guid?> listUserGroup_Index { get; set; }
    }

    public class ConfigUserGroupViewModel
    {
        public Guid userGroup_Index { get; set; }

        public string userGroup_Id { get; set; }
        public string userGroup_Name { get; set; }
        public int? isActive { get; set; }
        public int? isDelete { get; set; }

    }

    public class ZoneViewModel
    {
        public Guid zone_Index { get; set; }

        public string zone_Id { get; set; }

        public string zone_Name { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }

        public string create_By { get; set; }

        public DateTime? create_Date { get; set; }

        public string update_By { get; set; }

        public DateTime? update_Date { get; set; }

        public string cancel_By { get; set; }

        public DateTime? cancel_Date { get; set; }
        public List<Guid?> listzone_Index { get; set; }

    }
}
