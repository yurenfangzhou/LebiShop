﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.product
{
    public partial class product_batch_edit : AdminPageBase
    {
        protected string lang;
        protected string key;
        protected string Pro_Type_id;
        protected int status;
        protected int brand;
        protected int tag;
        protected string dateFrom;
        protected string dateTo;
        protected string OrderBy;
        protected string orderstr="";
        protected List<Lebi_Product> models;
        protected string PageString;
        protected int Type_id_ProductType;
        protected List<Lebi_UserLevel> userlevels;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("product_batch_edit", "批量编辑"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            Pro_Type_id = RequestTool.RequestString("Pro_Type_id");
            status = RequestTool.RequestInt("status", 0);
            brand = RequestTool.RequestInt("brand", 0);
            tag = RequestTool.RequestInt("tag", 0);
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            OrderBy = RequestTool.RequestString("OrderBy");
            lang = RequestTool.RequestString("lang");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            Type_id_ProductType = RequestTool.RequestInt("Type_id_ProductType", 320);
            if (lang == "")
                lang = "CN";
            string where = "Product_id=0";
            if (Pro_Type_id != "")
                where += " and Pro_Type_id in (" + Shop.Bussiness.EX_Product.Categoryid(Pro_Type_id) + ")";
            if (status > 0)
                where += " and Type_id_ProductStatus=" + status + "";
            if (Type_id_ProductType > 0)
                where += " and Type_id_ProductType=" + Type_id_ProductType + "";
            if (tag > 0)
            {
                if (DataBase.DBType == "sqlserver")
                {
                    where += " and Charindex('" + tag + "',Pro_Tag_id)>0";
                }
                if (DataBase.DBType == "access")
                {
                    where += " and Instr(Pro_Tag_id,'" + tag + "')>0";
                }
            }
            if (brand > 0)
                where += " and Brand_id=" + brand + "";
            if (key != "")
                where += " and (Name like lbsql{'%" + key + "%'} or Number like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and (datediff(d,Time_Add,'" + FormatDate(lbsql_dateFrom) + "')<=0 and datediff(d,Time_Add,'" + FormatDate(lbsql_dateTo) + "')>=0)";

            if (CurrentAdmin.Pro_Type_ids != "")
            {
                string[] ids = CurrentAdmin.Pro_Type_ids.Split(',');
                string sonwhere = "";
                foreach (string id in ids)
                {
                    sonwhere += " or Path like '%," + id + ",%'";
                }
                sonwhere = "select id from Lebi_Pro_Type where id in (" + CurrentAdmin.Pro_Type_ids + ")" + sonwhere;
                where += " and Pro_Type_id in (" + sonwhere + ")";

            }

            
            if (OrderBy == "StatusDesc")
            {
                orderstr = " Type_id_ProductStatus desc";
            }
            else if (OrderBy == "StatusAsc")
            {
                orderstr = " Type_id_ProductStatus asc";
            }
            else if (OrderBy == "ViewsDesc")
            {
                orderstr = " Count_Views desc";
            }
            else if (OrderBy == "ViewsAsc")
            {
                orderstr = " Count_Views asc";
            }
            else if (OrderBy == "SalesDesc")
            {
                orderstr = " Count_Sales desc";
            }
            else if (OrderBy == "SalesAsc")
            {
                orderstr = " Count_Sales asc";
            }
            else if (OrderBy == "CountDesc")
            {
                orderstr = " Count_Stock desc";
            }
            else if (OrderBy == "CountAsc")
            {
                orderstr = " Count_Stock asc";
            }
            else if (OrderBy == "Price_CostDesc")
            {
                orderstr = " Price_Cost desc";
            }
            else if (OrderBy == "Price_CostAsc")
            {
                orderstr = " Price_Cost asc";
            }
            else if (OrderBy == "PriceDesc")
            {
                orderstr = " Price desc";
            }
            else if (OrderBy == "PriceAsc")
            {
                orderstr = " Price asc";
            }
            else if (OrderBy == "FreezeDesc")
            {
                orderstr = " Count_Freeze desc";
            }
            else if (OrderBy == "FreezeAsc")
            {
                orderstr = " Count_Freeze asc";
            }
            else if (OrderBy == "SortDesc")
            {
                orderstr = " Sort desc";
            }
            else if (OrderBy == "SortAsc")
            {
                orderstr = " Sort asc";
            }
            else
            {
                orderstr = " id desc";
            }
            SQLDataAccess.SQLPara sp = new SQLDataAccess.SQLPara(where, orderstr,"*");
            models = B_Lebi_Product.GetList(sp,PageSize, page);
            int recordCount = B_Lebi_Product.Counts(sp);
            PageString = Pager.GetPaginationString("?page={0}&brand=" + brand + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&key=" + key + "&lang=" + lang + "&OrderBy=" + OrderBy + "&Pro_Type_id=" + Pro_Type_id + "&status=" + status + "&Type_id_ProductType=" + Type_id_ProductType + "&tag=" + tag, page, PageSize, recordCount);
            userlevels = B_Lebi_UserLevel.GetList("", "Grade asc");
        }

        public int CountSon(int pid)
        {
            return B_Lebi_Product.Counts("Product_id="+pid+" and Product_id<>0");
        }

        public void LicenseWord()
        {
            return;
            //if (!Shop.LebiAPI.Service.Instanse.check("biaozhun") && !Shop.LebiAPI.Service.Instanse.check("zengqiang"))
            //{
            //    response.write("<div class=\"licensealt\"><p class=\"title\">" + tag("敬告") + "：</p>");
            //    response.write("您使用的是免费系统，可录入100条主商品数据</br>");
            //    response.write("获得标准授权后可录入500条主商品数据</br>");
            //    response.write("获得增强授权可取消此限制，<a href=\"" + Shop.LebiAPI.Service.Instanse.weburl + "/plans/\" target=\"_bank\">点此获取</a>");
            //    response.write("</div>");
            //    return;
            //}
            //if (Shop.LebiAPI.Service.Instanse.check("biaozhun"))
            //{
            //    response.write("<div class=\"licensealt\"><p class=\"title\">" + tag("敬告") + "：</p>");
            //    response.write("您使用的是系统为标准授权，可录入500条主商品数据</br>");
            //    response.write("获得增强授权可取消此限制，<a href=\"" + Shop.LebiAPI.Service.Instanse.weburl + "/plans/\" target=\"_bank\">点此获取</a>");
            //    response.write("</div>");
            //    return;
            //}
             
        }


        public decimal GetUserLevelPrice(List<ProductUserLevelPrice> UserLevelPrices, int id)
        {
            try
            {
                return (from m in UserLevelPrices
                        where m.UserLevel_id == id
                        select m).ToList().FirstOrDefault().Price;
            }
            catch
            {
                return 0;
            }
        }
    }
}