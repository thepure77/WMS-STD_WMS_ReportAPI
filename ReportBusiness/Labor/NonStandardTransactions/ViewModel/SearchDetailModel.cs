using BinbalanceBusiness;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Labor.NonStandardTransactions
{
    public class SearchDetailModel : Pagination
    {

        public string ns_Date { get; set; }

        public string ns_Date_To { get; set; }

        public Guid? user_Index { get; set; }
        public string user_Id { get; set; }
        public string user_Name { get; set; }
        public Guid? userGroup_Index { get; set; }
        public string userGroup_Id { get; set; }
        public string userGroup_Name { get; set; }
        public string first_Name { get; set; }
        public string last_Name { get; set; }
        public Guid? menu_Index { get; set; }
        public Guid? menuType_Index { get; set; }
        public string menu_Id { get; set; }
        public string menu_Name { get; set; }
        public Guid? sub_Menu_Index { get; set; }
        public Guid? sub_MenuType_Index { get; set; }
        public string sub_Menu_Id { get; set; }
        public string sub_Menu_Name { get; set; }
        public string operations { get; set; }
        public Guid? ref_Document_Index { get; set; }
        public string ref_Document_No { get; set; }
        public string request_URL { get; set; }
        public string request_Body { get; set; }
        public string uDF_1 { get; set; }
        public string uDF_2 { get; set; }
        public string uDF_3 { get; set; }
        public string uDF_4 { get; set; }
        public string uDF_5 { get; set; }
        public int? isActive { get; set; }
        public int? isDelete { get; set; }
        public int? isSystem { get; set; }
        public int? status_Id { get; set; }
        public string create_By { get; set; }
        public string create_Date { get; set; }
        public string totalTime { get; set; }

        public class actionResultViewModel
        {
            public IList<SearchDetailModel> itemsNonStandard { get; set; }
            public Pagination pagination { get; set; }

            public Pagination pagination_Tab1 { get; set; }
            public Pagination pagination_Tab2 { get; set; }
            public int? total_Tab1 { get; set; }
            public int? total_Tab2 { get; set; }

            public int? countAll { get; set; }
        }

    }
}

