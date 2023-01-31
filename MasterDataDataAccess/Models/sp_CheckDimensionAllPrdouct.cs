using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_CheckDimensionAllPrdouct
    {
        [Key]
        public long? RowIndex { get; set; }
        public string Product_Id               {get;set;}
    public string Product_Name            {get;set;}
    public string BU_UNIT                 { get;set;}
    public string SALE_UNIT               {get;set; }
    public string IN_UNIT                 {get;set;}
    public string UNIT                    {get;set;}
    public decimal? Ratio                   {get;set;}
    public decimal? Weight                  {get;set;}
    public decimal? GrsWeight               {get;set;}
    public decimal? W                       {get;set;}
    public decimal? L                       {get;set;}
    public decimal? H                       {get;set;}
    public string TI                      {get;set;}
    public string HI                      {get;set;}
    public string Ref_No1                 {get;set;}
    public string Ref_No2                 {get;set;}
    public int? ProductItemLife_D       {get;set;}
    public int? ProductShelfLifeGI_D    {get;set;}
    public string ProductShelfLifeGR_D    {get;set;}
    public string ProductConversionBarcode{get;set;} 
    public string SUP                     {get;set;}
    public string IsPiecePcik { get; set; }
    public int? IsMfgDate               {get;set;}
    public int? IsExpDate               {get;set;}
    public int? IsLot                   {get;set;}
    public int? IsSerial                {get;set;}
    public decimal? BU_Qty_Per_Pallet       {get;set;}
    public decimal? Qty_Per_Pallet { get; set; }

    public string Room { get; set; }
    }
}
