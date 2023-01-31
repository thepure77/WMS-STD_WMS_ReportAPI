using GIDataAccess.Models;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using TransferDataAccess.Models;

namespace DataAccess
{
    public class temp_MasterDataDbContext : DbContext
    {
        //public DbSet<MasterType> MasterTypes { get; set; }
        public DbSet<VendorType> VendorTypes { get; set; }
        public DbSet<MS_AddressCountry> MS_AddressCountry { get; set; }
        public DbSet<MS_AddressDistrict> MS_AddressDistrict { get; set; }
        public DbSet<MS_AddressPostcode> MS_AddressPostcode { get; set; }
        public DbSet<MS_AddressProvince> MS_AddressProvince { get; set; }
        public DbSet<MS_AddressSubDistrict> MS_AddressSubDistrict { get; set; }
        public DbSet<MS_DocumentType> MS_DocumentType { get; set; }
        public DbSet<MS_DocumentTypeItemStatus> MS_DocumentTypeItemStatus { get; set; }
        public DbSet<MS_DocumentTypeNumber> MS_DocumentTypeNumber { get; set; }
        public DbSet<MS_Equipment> MS_Equipment { get; set; }
        public DbSet<MS_EquipmentItem> MS_EquipmentItem { get; set; }
        public DbSet<MS_EquipmentSubType> MS_EquipmentSubType { get; set; }
        public DbSet<MS_EquipmentType> MS_EquipmentType { get; set; }
        public DbSet<MS_ItemStatus> MS_ItemStatus { get; set; }
        public DbSet<MS_Location> MS_Location { get; set; }
        public DbSet<MS_LocationEquipment> MS_LocationEquipment { get; set; }
        public DbSet<MS_LocationAisle> MS_LocationAisle { get; set; }
        public DbSet<MS_LocationType> MS_LocationType { get; set; }
        public DbSet<MS_LocationWorkArea> MS_LocationWorkArea { get; set; }
        public DbSet<MS_Owner> MS_Owner { get; set; }
        public DbSet<MS_OwnerSoldTo> MS_OwnerSoldTo { get; set; }
        public DbSet<MS_OwnerType> MS_OwnerType { get; set; }
        public DbSet<MS_OwnerVendor> MS_OwnerVendor { get; set; }
        public DbSet<MS_Pallet> MS_Pallet { get; set; }
        public DbSet<MS_PalletType> MS_PalletType { get; set; }
        public DbSet<MS_Product> MS_Product { get; set; }
        public DbSet<MS_ProductCategory> MS_ProductCategory { get; set; }
        public DbSet<MS_ProductConversion> MS_ProductConversion { get; set; }
        public DbSet<MS_ProductConversionBarcode> MS_ProductConversionBarcode { get; set; }
        public DbSet<MS_ReasonCode> MS_ReasonCode { get; set; }
        public DbSet<MS_ProductOwner> MS_ProductOwner { get; set; }
        public DbSet<MS_ProductSubType> MS_ProductSubType { get; set; }
        public DbSet<MS_ProductType> MS_ProductType { get; set; }
        public DbSet<MS_Room> MS_Room { get; set; }
        public DbSet<MS_Rule> MS_Rule { get; set; }
        public DbSet<MS_RuleCondition> MS_RuleCondition { get; set; }
        public DbSet<MS_RuleZone> MS_RuleZone { get; set; }
        public DbSet<MS_ShipTo> MS_ShipTo { get; set; }
        public DbSet<MS_ShipToType> MS_ShipToType { get; set; }
        public DbSet<MS_SoldTo> MS_SoldTo { get; set; }
        public DbSet<MS_SoldToShipTo> MS_SoldToShipTo { get; set; }
        public DbSet<MS_SoldToType> MS_SoldToType { get; set; }
        public DbSet<MS_TaskGroup> MS_TaskGroup { get; set; }
        public DbSet<MS_TaskGroupEquipment> MS_TaskGroupEquipment { get; set; }
        public DbSet<MS_TaskGroupUser> MS_TaskGroupUser { get; set; }
        public DbSet<MS_TaskGroupWorkArea> MS_TaskGroupWorkArea { get; set; }
        public DbSet<MS_User> MS_User { get; set; }
        public DbSet<MS_UserEquipment> MS_UserEquipment { get; set; }
        public DbSet<MS_UserGroup> MS_UserGroup { get; set; }
        public DbSet<MS_UserGroupMenu> MS_UserGroupMenu { get; set; }
        public DbSet<MS_UserGroupZone> MS_UserGroupZone { get; set; }
        public DbSet<MS_Vendor> MS_Vendor { get; set; }
        public DbSet<MS_VendorType> MS_VendorType { get; set; }
        public DbSet<MS_Warehouse> MS_Warehouse { get; set; }
        public DbSet<MS_WorkArea> MS_WorkArea { get; set; }
        public DbSet<MS_Zone> MS_Zone { get; set; }
        public DbSet<MS_Zoneputaway> MS_Zoneputaway { get; set; }
        public DbSet<MS_ZoneLocation> MS_ZoneLocation { get; set; }
        public DbSet<MS_Wave> MS_Wave { get; set; }
        public DbSet<sy_Menu> sy_Menu { get; set; }
        public DbSet<sy_MenuType> sy_MenuType { get; set; }
        public DbSet<sy_Process> sy_Process { get; set; }
        public DbSet<sy_ProcessStatus> sy_ProcessStatus { get; set; }
        public DbSet<sy_RuleConditionField> sy_RuleConditionField { get; set; }
        public DbSet<sy_RuleConditionOperation> sy_RuleConditionOperation { get; set; }
        public DbSet<MS_DockDoor> MS_DockDoor { get; set; }
        public DbSet<MS_VehicleType> MS_VehicleType { get; set; }
        public DbSet<MS_ContainerType> MS_ContainerType { get; set; }
        public DbSet<WM_TagOutPick> WM_TagOutPick { get; set; }
        public DbSet<MS_3PL> MS_3PL { get; set; }
        public DbSet<View_ProductPopup> View_ProductPopup { get; set; }
        public DbSet<View_ProductConversionBarcode> View_ProductConversionBarcode { get; set; }
        //public DbSet<MS_ProductConversion> MS_ProductConversionPaging { get; set; }
        public DbSet<MS_ProductConversionPaging> MS_ProductConversionPaging { get; set; }
        public DbSet<MS_ProductOwnerPaging> MS_ProductOwnerPaging { get; set; }

        public DbSet<View_GetLocationEquipment> View_GetLocationEquipment { get; set; }
        public DbSet<View_GetZoneLocation> View_GetZoneLocation { get; set; }
        public DbSet<View_GetLocationWorkArea> View_GetLocationWorkArea { get; set; }
        public DbSet<View_PopupProduct> View_PopupProduct { get; set; }
        public DbSet<ms_Round> MS_Round { get; set; }
        public DbSet<ms_Route> MS_Route { get; set; }
        public DbSet<ms_TypeCar> ms_TypeCar { get; set; }
        public DbSet<ms_Transport> ms_Transport { get; set; }
        //View_StockOnCartonFlow
        public DbSet<View_StockOnCartonFlow> View_StockOnCartonFlow { get; set; }

        public DbSet<View_GetProductOwner> View_GetProductOwner { get; set; }

        public DbSet<View_ProductDetail> View_ProductDetail { get; set; }

        public DbSet<sy_SuggestPutawayByProduct> sy_SuggestPutawayByProduct { get; set; }
        public DbSet<sy_AutoNumber> sy_AutoNumber { get; set; }

        public DbSet<MS_WHOwner> MS_WHOwner { get; set; }
        public DbSet<MS_WHOwnerType> MS_WHOwnerType { get; set; }
        public DbSet<MS_Facility> MS_Facility { get; set; }
        public virtual DbSet<View_User> View_User { get; set; }
        public virtual DbSet<View_Vendor> View_Vendor { get; set; }
        public virtual DbSet<View_SoldTo> View_SoldTo { get; set; }
        public virtual DbSet<View_LocationEquipment> View_LocationEquipment { get; set; }
        public virtual DbSet<View_LocationWorkArea> View_LocationWorkArea { get; set; }
        public virtual DbSet<View_ShipTo> View_ShipTo { get; set; }
        public virtual DbSet<View_SoldToShipTo> View_SoldToShipTo { get; set; }
        public virtual DbSet<View_Location> View_Location { get; set; }
        public virtual DbSet<View_Room> View_Room { get; set; }
        public virtual DbSet<View_EquipmentSubType> View_EquipmentSubType { get; set; }
        public virtual DbSet<View_Equipment> View_Equipment { get; set; }
        public virtual DbSet<View_TaskGroupEquipment> View_TaskGroupEquipment { get; set; }
        public virtual DbSet<View_TaskGroupUser> View_TaskGroupUser { get; set; }
        public virtual DbSet<View_TaskGroupWorkArea> View_TaskGroupWorkArea { get; set; }
        public virtual DbSet<View_WHOwner> View_WHOwner { get; set; }
        public virtual DbSet<View_Owner> View_Owner { get; set; }
        public virtual DbSet<View_OwnerVendor> View_OwnerVendor { get; set; }
        public virtual DbSet<View_UserGroupMenu> View_UserGroupMenu { get; set; }
        public virtual DbSet<View_UserGroupZone> View_UserGroupZone { get; set; }
        public virtual DbSet<View_ProductDetailV2> View_ProductDetailV2 { get; set; }
        public virtual DbSet<View_ProductType> View_ProductType { get; set; }
        public virtual DbSet<View_ProductSubType> View_ProductSubType { get; set; }
        public virtual DbSet<View_ProductOwner> View_ProductOwner { get; set; }
        public virtual DbSet<View_ProductConversion> View_ProductConversion { get; set; }
        public virtual DbSet<View_ProductConversionBarcodeV2> View_ProductConversionBarcodeV2 { get; set; }
        public virtual DbSet<View_OwnerSoldTo> View_OwnerSoldTo { get; set; }
        public virtual DbSet<View_ZoneLocation> View_ZoneLocation { get; set; }
        public virtual DbSet<View_Facility> View_Facility { get; set; }
        public virtual DbSet<View_RuleConditionOperation> View_RuleConditionOperation { get; set; }
        public virtual DbSet<View_Rule> View_Rule { get; set; }
        public virtual DbSet<View_RuleCondition> View_RuleCondition { get; set; }
        public virtual DbSet<View_RuleConditionField> View_RuleConditionField { get; set; }
        public virtual DbSet<View_DocumentType> View_DocumentType { get; set; }
        public virtual DbSet<View_LocationZoneputaway> View_LocationZoneputaway { get; set; }
        public virtual DbSet<View_RuleputawayCondition> View_RuleputawayCondition { get; set; }
        
        public DbSet<MS_FacilityType> MS_FacilityType { get; set; }
        public DbSet<MS_LocationZoneputaway> MS_LocationZoneputaway { get; set; }
        public DbSet<MS_RuleputawayConditionField> MS_RuleputawayConditionField { get; set; }
        public DbSet<MS_RuleputawayCondition> MS_RuleputawayCondition { get; set; }
        public DbSet<MS_Ruleputaway> MS_Ruleputaway { get; set; }
        public DbSet<MS_RuleputawaySuggest> MS_RuleputawaySuggest { get; set; }

        public DbSet<View_WaveTemplate> View_WaveTemplate { get; set; }

        public DbSet<ms_WaveRule> MS_WaveRule { get; set; }

        public DbSet<View_WaveRule> View_WaveRule { get; set; }
        public DbSet<View_LocatinCyclecount> View_LocatinCyclecount { get; set; }

        public virtual DbSet<View_TaskGroupLocationWorkArea> View_TaskGroupLocationWorkArea { get; set; }
        public virtual DbSet<View_UserTaskGroup> View_UserTaskGroup { get; set; }
        public virtual DbSet<sy_Config> sy_Config { get; set; }
        public virtual DbSet<View_SugesstionPutaway> View_SugesstionPutaway { get; set; }
        public virtual DbSet<View_ProductCategory> View_ProductCategory { get; set; }
        public virtual DbSet<View_RPT14_ProductType> View_RPT14_ProductType { get; set; }
        public virtual DbSet<View_RPT10_Location> View_RPT10_Location { get; set; }
        public virtual DbSet<View_RPT11_Location> View_RPT11_Location { get; set; }
        public virtual DbSet<View_RPT_MS_SummaryMaterialsStock> View_RPT_MS_SummaryMaterialsStock { get; set; }
        public virtual DbSet<View_RPT_MS_InventoryStock> View_RPT_MS_InventoryStock { get; set; }

        public virtual DbSet<View_RPT_Location> View_RPT_Location { get; set; }

        public virtual DbSet<sp_Count_location> sp_Count_location { get; set; }

        public virtual DbSet<View_RPT_Location2> View_RPT_Location2 { get; set; }

        public virtual DbSet<sp_CheckBalanceAllLocation> sp_CheckBalanceAllLocation { get; set; }

        public virtual DbSet<sp_CheckDimensionAllPrdouct> sp_CheckDimensionAllPrdouct { get; set; }

        public virtual DbSet<sp_CheckGIEXP> sp_CheckGIEXP { get; set; }

        public virtual DbSet<sp_CheckGrPutaway> sp_CheckGrPutaway { get; set; }

        public virtual DbSet<sp_CheckOnGroundRobot> sp_CheckOnGroundRobot { get; set; }

        public virtual DbSet<sp_CheckOrderNotPick> sp_CheckOrderNotPick { get; set; }

        public virtual DbSet<sp_CheckPickTime> sp_CheckPickTime { get; set; }

        public virtual DbSet<sp_CheckPlanGiandGidiff> sp_CheckPlanGiandGidiff { get; set; }

        public virtual DbSet<sp_CheckReplenishByOrder> sp_CheckReplenishByOrder { get; set; }

        public virtual DbSet<sp_CheckReplenishCFRByMax> sp_CheckReplenishCFRByMax { get; set; }
        public virtual DbSet<sp_ReportCheckReplenishVCByMax> sp_ReportCheckReplenishVCByMax { get; set; }
        public virtual DbSet<sp_ReportCheckReturnTote> sp_ReportCheckReturnTote { get; set; }
        public virtual DbSet<sp_ReportCheckStockAVB> sp_ReportCheckStockAVB { get; set; }
        public virtual DbSet<sp_ReportCheckStockOnHand> sp_ReportCheckStockOnHand { get; set; }
        public virtual DbSet<sp_ReportCheckStockOnHandPartial> sp_ReportCheckStockOnHandPartial { get; set; }
        public virtual DbSet<sp_ReportCheckTotePendingReturn> sp_ReportCheckTotePendingReturn { get; set; }
        public virtual DbSet<sp_ReportCheckTransactionGI> sp_ReportCheckTransactionGI { get; set; }
        public virtual DbSet<sp_ReportCheckTransactionGR> sp_ReportCheckTransactionGR { get; set; }
        public virtual DbSet<sp_ReportCheckTransfer> sp_ReportCheckTransfer { get; set; }
        public virtual DbSet<sp_ReportCheckZonePutaway> sp_ReportCheckZonePutaway { get; set; }

        public virtual DbSet<ms_Howto> ms_Howto { get; set; }
        public virtual DbSet<View_CheckBypassForReplenish> View_CheckBypassForReplenish { get; set; }

        public virtual DbSet<sp_LogGr> sp_LogGr { get; set; }
        public virtual DbSet<sp_LogGi> sp_LogGi { get; set; }
        public virtual DbSet<sp_LogCancel> sp_LogCancel { get; set; }
        public virtual DbSet<sp_LogTransfer> sp_LogTransfer { get; set; }
        
        // เพิ่ม model report recall 
        public virtual DbSet<View_ReportRecall_Excel> View_ReportRecall_Excel { get; set; }

        // เพิ่ม report recall Inbound , Outbound
        public virtual DbSet<View_ReportRecall_Inbound_Excel> View_ReportRecall_Inbound_Excel { get; set; }
        public virtual DbSet<View_ReportRecall_Outbound_Excel> View_ReportRecall_Outbound_Excel { get; set; }
        public virtual DbSet<sp_ReportGIByShipmentDateAndBusinessUnit> sp_ReportGIByShipmentDateAndBusinessUnit { get; set; }
        public virtual DbSet<sp_ReportGIByShipmentNoAndProductId> sp_ReportGIByShipmentNoAndProductId { get; set; }
        public virtual DbSet<sp_ReportVolumeByShipToPickZone> sp_ReportVolumeByShipToPickZone { get; set; }

        public virtual DbSet<sp_VolumeByAppoint> sp_VolumeByAppoint { get; set; }
        public virtual DbSet<sp_VolumeByShipment> sp_VolumeByShipment { get; set; }

        public virtual DbSet<sp_ReportGI_SN> sp_ReportGI_SN { get; set; }
        public virtual DbSet<sp_rpt_13_Picking> sp_rpt_13_Picking { get; set; }
        public virtual DbSet<sp_rpt_14_Shipping> sp_rpt_14_Shipping { get; set; }
        public virtual DbSet<sp_rpt_12_Picking_Performance> sp_rpt_12_Picking_Performance { get; set; }

        public virtual DbSet<sp_rpt_16_Reconcile_Sap> sp_rpt_16_Reconcile_Sap { get; set; }
        public virtual DbSet<sp_rpt_01_Traceability> sp_rpt_01_Traceability { get; set; }
        public virtual DbSet<sp_rpt_02_CoverDay> sp_rpt_02_CoverDay { get; set; }
        public virtual DbSet<sp_rpt_10_Receiving> sp_rpt_10_Receiving { get; set; }
        public virtual DbSet<sp_rpt_20_Ageging> sp_rpt_20_Ageging { get; set; }
        public virtual DbSet<sp_rpt_05_Pickinglist> sp_rpt_05_Pickinglist { get; set; }
        public virtual DbSet<sp_rpt_04_Pickingplan> sp_rpt_04_Pickingplan { get; set; }
        public virtual DbSet<sp_rpt_03_ShippingMark> sp_rpt_03_ShippingMark { get; set; }
        public virtual DbSet<sp_rpt_18_Movement> sp_rpt_18_Movement { get; set; }


        public virtual DbSet<sp_rpt_15_Space_Utilization> sp_rpt_15_Space_Utilization { get; set; }
        public virtual DbSet<sp_rpt_15_Space_Utilization_Owner> sp_rpt_15_Space_Utilization_Owner { get; set; }
        public virtual DbSet<sp_rpt_15_Space_Utilization_Percent> sp_rpt_15_Space_Utilization_Percent { get; set; }
        public virtual DbSet<sp_rpt_15_Space_Utilization_Percent_Owner> sp_rpt_15_Space_Utilization_Percent_Owner { get; set; }
        public virtual DbSet<sp_rpt_11_PutAway> sp_rpt_11_PutAway { get; set; }
        public virtual DbSet<sp_rpt_17_Not_Movement> sp_rpt_17_Not_Movement { get; set; }
        public virtual DbSet<sp_rpt_15_Space_Utilization_All> sp_rpt_15_Space_Utilization_All { get; set; }
        public virtual DbSet<sp_rpt_15_Space_Utilization_Owner_All> sp_rpt_15_Space_Utilization_Owner_All { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("temp_MasterDefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);

                //optionsBuilder.UseSqlServer(@"Server=kascoit.ddns.net,22017;Database=WMSDB_QA;Trusted_Connection=True;Integrated Security=False;user id=sa;password=K@sc0db12345;");
                //optionsBuilder.UseSqlServer(@"Server=192.168.1.11\MSSQL2017;Database=WMSDB_QA;Trusted_Connection=True;Integrated Security=False;user id=sa;password=K@sc0db12345;");
            }
            //optionsBuilder.UseSqlServer(@"Server=10.0.177.33\SQLEXPRESS;Database=WMSDB;Trusted_Connection=True;Integrated Security=False;user id=cfrffmusr;password=ffmusr@cfr;");
            //optionsBuilder.UseSqlServer(@"Server=10.100.141.37;Database=WMSDB_STD_Master;Trusted_Connection=True;Integrated Security=False;user id=SA;password=P@ssw0rd;");

        }
    }
}
