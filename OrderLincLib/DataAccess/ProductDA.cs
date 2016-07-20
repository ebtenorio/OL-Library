using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;
using System.Transactions;

namespace OrderLinc.DataAccess
{
    public class ProductDA : IProductDA
    {

        DatabaseService mDBService;

        public ProductDA(DatabaseService dbService)
        {

            mDBService = dbService;
        }


        #region ************************tblProduct CRUDS ******************************************

        public Boolean HasAffectedOrderByDateChange(int SalesOrgID, long ProviderID, string ProviderProductCode, DateTime NewStartDate, DateTime NewEndDate, out string OrderNumber, out DateTime RequestedReleaseDate)
        {
            Boolean HasAffectedOrder = false;
            OrderNumber = "";
            RequestedReleaseDate = DateTime.ParseExact("01/01/1900","dd/MM/yyyy", null);
            DTODBParameters mParams = new DTODBParameters();

            mParams.Add(new DTODBParameter("@SalesOrgID", SalesOrgID));
            mParams.Add(new DTODBParameter("@ProviderID", ProviderID));
            mParams.Add(new DTODBParameter("@ProviderProductCode", ProviderProductCode));
            mParams.Add(new DTODBParameter("@StartDate", NewStartDate));
            mParams.Add(new DTODBParameter("@EndDate", NewEndDate));

            string sp = "usp_tblOrder_ListByAffectedByProductDateChange";
            
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);

            if (mDT.Rows.Count > 0)
            {
                HasAffectedOrder = true;
                OrderNumber = mDT.Rows[0]["OrderNumber"].ToString();
                RequestedReleaseDate = (DateTime)mDT.Rows[0]["RequestedReleaseDate"];
            }

            return HasAffectedOrder;

        }

        private DTOProduct DataRowToDTOProduct(DataRow mRow)
        {
            DTOProduct mDTOProduct = new DTOProduct();
            mDTOProduct.ProductID = Int64.Parse(mRow["ProductID"].ToString());
            mDTOProduct.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
            mDTOProduct.GTINCode = mRow["GTINCode"].ToString();
            mDTOProduct.ProductBrandID = Int64.Parse(mRow["ProductBrandID"].ToString());
            mDTOProduct.ProductCategoryID = int.Parse(mRow["ProductCategoryID"].ToString());
            mDTOProduct.PrimarySKU = Int64.Parse(mRow["PrimarySKU"].ToString());
            mDTOProduct.ProductDescription = mRow["ProductDescription"].ToString();
            mDTOProduct.UOMID = Int64.Parse(mRow["UOMID"].ToString());
            mDTOProduct.Inactive = Boolean.Parse(mRow["Inactive"].ToString());

            // To Accommodate Discount
            // Try to remove this and see what happens.
            if (mRow["Discount"] == null || mRow["Discount"].ToString() == "")
            {
                mDTOProduct.Discount = 0.00f;
            }
            else
            {
                mDTOProduct.Discount = float.Parse(mRow["Discount"].ToString());
            }
            

            if (mRow.Table.Columns.Contains("ProviderProductCode"))
            {
                mDTOProduct.ProductCode = mRow["ProviderProductCode"].ToString();
            }

            if (mRow.Table.Columns.Contains("ProviderID") && mRow["ProviderID"] != System.DBNull.Value)
            {
                mDTOProduct.ProviderID = long.Parse(mRow["ProviderID"].ToString());
            }
            if (mRow.Table.Columns.Contains("StartDate") && mRow["StartDate"] != System.DBNull.Value)
            {
                mDTOProduct.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
            }

            if (mRow.Table.Columns.Contains("EndDate") && mRow["EndDate"] != System.DBNull.Value)
            {
                mDTOProduct.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
            }

            return mDTOProduct;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        //public DTOProductList ProductList()
        //{
        //    DTOProductList mDTOProductList = new DTOProductList();
        //    DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProduct_List");
        //    foreach (DataRow mRow in mDT.Rows)
        //    {
        //        mDTOProductList.Add(DataRowToDTOProduct(mRow));
        //    }

        //    return mDTOProductList;
        //}

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProduct ProductByCriteria(ProductCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();

            string sp = string.Empty;

            if (criteria.SearchType == ProductSearchType.ByProductID)
            {
                mParams.Add(new DTODBParameter("@ProductID", criteria.ProductID));
                mParams.Add(new DTODBParameter("@ProviderID", criteria.ProviderID));
                sp = "usp_tblProduct_ListByID";
            }
            else if (criteria.SearchType == ProductSearchType.ByGTINCode)
            {
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
                mParams.Add(new DTODBParameter("@GTINCode", criteria.GTINCode));

                sp = "usp_tblProduct_ListByGTINCode";
            }
            else if (criteria.SearchType == ProductSearchType.ByProductCode)
            {
                mParams.Add(new DTODBParameter("@ProductCode", criteria.ProductCode));
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
                mParams.Add(new DTODBParameter("@ProviderID", criteria.ProviderID));
                sp = "usp_tblProduct_ListByProductCode";
            }
            //else if (criteria.SearchType == ProductSearchType.ByProductName)
            //{
            //    mParams.Add(new DTODBParameter("@ProviderProductCode", criteria.ProductCode));
            //    mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
            //    sp = "usp_tblProduct_ListByProductCode";
            //}

            if (sp == string.Empty) throw new ArgumentException("No sp was specified.");

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);
            DTOProduct mDTOProduct = null;

            if (mDT.Rows.Count > 0)
                mDTOProduct = DataRowToDTOProduct(mDT.Rows[0]);

            return mDTOProduct;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProduct ProductCreateRecord(DTOProduct mDTO)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                DataTable mDT = mDBService.GenerateCreateTable("tblProduct");
                DataRow mRow = mDT.NewRow();


                mDT.Columns.Remove("OldID");
                mDT.AcceptChanges();

                mRow["SalesOrgID"] = mDTO.SalesOrgID;
                mRow["GTINCode"] = mDTO.GTINCode;
                mRow["ProductBrandID"] = mDTO.ProductBrandID;
                mRow["ProductCategoryID"] = mDTO.ProductCategoryID;
                mRow["PrimarySKU"] = mDTO.PrimarySKU;
                mRow["ProductDescription"] = mDTO.ProductDescription;
                mRow["UOMID"] = mDTO.UOMID;
                mRow["Inactive"] = mDTO.Inactive;

                mDT.Rows.Add(mRow);
                Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProduct_INSERT");
                Int64 ObjectID = Int64.Parse(mRetval.ToString());
                mDTO.ProductID = ObjectID;

                if (mDTO.ProviderID > 0)
                    CreateProviderProduct(mDTO);

                scope.Complete();
            }
            return mDTO;
        }

        private void CreateProviderProduct(DTOProduct mDTO)
        {

            DataTable mDT = mDBService.GenerateCreateTable("tblProviderProduct");
            DataRow mRow = mDT.NewRow();


            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProductID"] = mDTO.ProductID;
            mRow["ProviderProductCode"] = mDTO.ProductCode;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mRow["Discount"] = mDTO.Discount;
            mDT.Rows.Add(mRow);

            mDBService.CreateRecord(mDT, "usp_tblProviderProduct_INSERT");
        }
        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProduct ProductUpdateRecord(DTOProduct mDTO)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DataTable mDT = mDBService.GenerateUpdateTable("tblProduct");
                DataRow mRow = mDT.NewRow();

                mDT.Columns.Remove("OldID");
                mDT.AcceptChanges();

                mRow["ProductID"] = mDTO.ProductID;
                mRow["SalesOrgID"] = mDTO.SalesOrgID;
                mRow["GTINCode"] = mDTO.GTINCode;
                mRow["ProductBrandID"] = mDTO.ProductBrandID;
                mRow["ProductCategoryID"] = mDTO.ProductCategoryID;
                mRow["PrimarySKU"] = mDTO.PrimarySKU;
                mRow["ProductDescription"] = mDTO.ProductDescription;
                mRow["UOMID"] = mDTO.UOMID;
                mRow["Inactive"] = mDTO.Inactive;
                mDT.Rows.Add(mRow);
                mDBService.UpdateRecord(mDT, "usp_tblProduct_UPDATE");

                if (mDTO.ProviderID > 0)
                    CreateProviderProduct(mDTO);

                scope.Complete();
            }
            return mDTO;
        }


        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProduct ProductDeleteRecord(DTOProduct mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductID";
            mParam.ParameterValue = mDTO.ProductID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProduct_DELETE", mParams);
            return mDTO;
        }

      
        public DTOProductList ProductListByCriteria(ProductCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();

            string sp = string.Empty;

            if (criteria.SearchType == ProductSearchType.ByProductGroupID)
            {
                mParams.Add(new DTODBParameter("@ProductGroupID", criteria.ProductGroupID));
                mParams.Add(new DTODBParameter("@CurrentPage", criteria.CurrentPage));
                mParams.Add(new DTODBParameter("@PageItemCount", criteria.PageItemCount));
                mParams.Add(new DTODBParameter("@ProviderID", criteria.ProviderID));
                sp = "usp_tblProduct_ListByGroupIDPaged";
            }
            else if (criteria.SearchType == ProductSearchType.ByGTINCode)
            {
                mParams.Add(new DTODBParameter("@GTINCode", criteria.GTINCode));
                sp = "usp_tblProduct_ListByGTINCode";
            }
            else if (criteria.SearchType == ProductSearchType.ByProductCode)
            {
                mParams.Add(new DTODBParameter("@ProviderProductCode", criteria.ProductCode));
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
                sp = "usp_tblProduct_ListByProductCode";
            }

            if (sp == string.Empty) throw new ArgumentException("No sp was specified.");

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);

            DTOProductList mDTOProductList = new DTOProductList();

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
                mDTOProductList.TotalRecords = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            foreach (DataRow mRow in mDT.Rows)
            {
                mDTOProductList.Add(DataRowToDTOProduct(mRow));
            }
            return mDTOProductList;
        }

        #endregion


        public DTOProductList ProductListByProductGroupIDandProviderID(int productGroupID, long providerID, int currentPageID, int pageItemCount)
        {
            DTODBParameters mParams = new DTODBParameters();

            string sp = string.Empty;

                mParams.Add(new DTODBParameter("@ProductGroupID", productGroupID));
                mParams.Add(new DTODBParameter("@CurrentPage", currentPageID));
                mParams.Add(new DTODBParameter("@PageItemCount", pageItemCount));
                mParams.Add(new DTODBParameter("@ProviderID", providerID));
                sp = "usp_tblProduct_ListByGroupIDandProviderIDPaged";
         
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);

            DTOProductList mDTOProductList = new DTOProductList();

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
                mDTOProductList.TotalRecords = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            foreach (DataRow mRow in mDT.Rows)
            {
                mDTOProductList.Add(DataRowToDTOProduct(mRow));
            }
            return mDTOProductList;

        }


        #region ************************tblProductBrand CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductBrandList ProductBrandList()
        {
            DTOProductBrandList mDTOProductBrandList = new DTOProductBrandList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductBrand_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductBrand mDTOProductBrand = new DTOProductBrand();
                mDTOProductBrand.ProductBrandID = Int64.Parse(mRow["ProductBrandID"].ToString());
                mDTOProductBrand.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductBrand.ProductBrandText = mRow["ProductBrandText"].ToString();
                mDTOProductBrandList.Add(mDTOProductBrand);
            }

            return mDTOProductBrandList;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductBrand ProductBrandListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductBrandID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductBrand_ListByID", mParams);
            DTOProductBrand mDTOProductBrand = new DTOProductBrand();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProductBrand.ProductBrandID = Int64.Parse(mRow["ProductBrandID"].ToString());
                mDTOProductBrand.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductBrand.ProductBrandText = mRow["ProductBrandText"].ToString();
            }

            return mDTOProductBrand;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductBrand ProductBrandCreateRecord(DTOProductBrand mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProductBrand");
            DataRow mRow = mDT.NewRow();
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["ProductBrandText"] = mDTO.ProductBrandText;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProductBrand_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.ProductBrandID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductBrand ProductBrandUpdateRecord(DTOProductBrand mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProductBrand");
            DataRow mRow = mDT.NewRow();
            mRow["ProductBrandID"] = mDTO.ProductBrandID;
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["ProductBrandText"] = mDTO.ProductBrandText;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProductBrand_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductBrand ProductBrandDeleteRecord(DTOProductBrand mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductBrandID";
            mParam.ParameterValue = mDTO.ProductBrandID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProductBrand_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductBrandList ProductBrandListBySalesOrgID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductBrand_ListBySalesOrgID", mParams);
            DTOProductBrandList mDTOProductBrandList = new DTOProductBrandList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductBrand mDTOProductBrand = new DTOProductBrand();
                mDTOProductBrand.ProductBrandID = Int64.Parse(mRow["ProductBrandID"].ToString());
                mDTOProductBrand.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductBrand.ProductBrandText = mRow["ProductBrandText"].ToString();
                mDTOProductBrandList.Add(mDTOProductBrand);
            }

            return mDTOProductBrandList;
        }

        #endregion


        #region ************************tblProductCategory CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategoryList ProductCategoryList()
        {
            DTOProductCategoryList mDTOProductCategoryList = new DTOProductCategoryList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductCategory_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductCategory mDTOProductCategory = new DTOProductCategory();
                mDTOProductCategory.ProductCategoryID = int.Parse(mRow["ProductCategoryID"].ToString());
                mDTOProductCategory.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductCategory.ProductCategoryText = mRow["ProductCategoryText"].ToString();
                mDTOProductCategory.ParentCategoryID = int.Parse(mRow["ParentCategoryID"].ToString());
                mDTOProductCategory.InActive = Boolean.Parse(mRow["InActive"].ToString());
                mDTOProductCategoryList.Add(mDTOProductCategory);
            }

            return mDTOProductCategoryList;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategory ProductCategoryListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductCategoryID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductCategory_ListByID", mParams);
            DTOProductCategory mDTOProductCategory = new DTOProductCategory();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProductCategory.ProductCategoryID = int.Parse(mRow["ProductCategoryID"].ToString());
                mDTOProductCategory.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductCategory.ProductCategoryText = mRow["ProductCategoryText"].ToString();
                mDTOProductCategory.ParentCategoryID = int.Parse(mRow["ParentCategoryID"].ToString());
                mDTOProductCategory.InActive = Boolean.Parse(mRow["InActive"].ToString());
            }

            return mDTOProductCategory;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategory ProductCategoryCreateRecord(DTOProductCategory mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProductCategory");
            DataRow mRow = mDT.NewRow();
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["ProductCategoryText"] = mDTO.ProductCategoryText;
            mRow["ParentCategoryID"] = mDTO.ParentCategoryID;
            mRow["InActive"] = mDTO.InActive;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProductCategory_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.ProductCategoryID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategory ProductCategoryUpdateRecord(DTOProductCategory mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProductCategory");
            DataRow mRow = mDT.NewRow();
            mRow["ProductCategoryID"] = mDTO.ProductCategoryID;
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["ProductCategoryText"] = mDTO.ProductCategoryText;
            mRow["ParentCategoryID"] = mDTO.ParentCategoryID;
            mRow["InActive"] = mDTO.InActive;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProductCategory_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategory ProductCategoryDeleteRecord(DTOProductCategory mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductCategoryID";
            mParam.ParameterValue = mDTO.ProductCategoryID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProductCategory_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategoryList ProductCategoryListBySalesOrgID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductCategory_ListBySalesOrgID", mParams);
            DTOProductCategoryList mDTOProductCategoryList = new DTOProductCategoryList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductCategory mDTOProductCategory = new DTOProductCategory();
                mDTOProductCategory.ProductCategoryID = int.Parse(mRow["ProductCategoryID"].ToString());
                mDTOProductCategory.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductCategory.ProductCategoryText = mRow["ProductCategoryText"].ToString();
                mDTOProductCategory.ParentCategoryID = int.Parse(mRow["ParentCategoryID"].ToString());
                mDTOProductCategory.InActive = Boolean.Parse(mRow["InActive"].ToString());
                mDTOProductCategoryList.Add(mDTOProductCategory);
            }

            return mDTOProductCategoryList;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategoryList ProductCategoryListByParentCategoryID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ParentCategoryID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductCategory_ListByParentCategoryID", mParams);
            DTOProductCategoryList mDTOProductCategoryList = new DTOProductCategoryList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductCategory mDTOProductCategory = new DTOProductCategory();
                mDTOProductCategory.ProductCategoryID = int.Parse(mRow["ProductCategoryID"].ToString());
                mDTOProductCategory.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductCategory.ProductCategoryText = mRow["ProductCategoryText"].ToString();
                mDTOProductCategory.ParentCategoryID = int.Parse(mRow["ParentCategoryID"].ToString());
                mDTOProductCategory.InActive = Boolean.Parse(mRow["InActive"].ToString());
                mDTOProductCategoryList.Add(mDTOProductCategory);
            }

            return mDTOProductCategoryList;
        }

        #endregion


        #region ************************tblProductData CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductDataList ProductDataList()
        {
            DTOProductDataList mDTOProductDataList = new DTOProductDataList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductData_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductData mDTOProductData = new DTOProductData();
                mDTOProductData.ProductDataID = Int64.Parse(mRow["ProductDataID"].ToString());
                mDTOProductData.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProductData.Width = mRow["Width"].ToString();
                mDTOProductData.Height = mRow["Height"].ToString();
                mDTOProductData.Length = mRow["Length"].ToString();
                mDTOProductData.FileBin = (byte[])mRow["FileBin"];
                mDTOProductData.FileName = mRow["FileName"].ToString();
                mDTOProductData.OriginPath = mRow["OriginPath"].ToString();
                mDTOProductDataList.Add(mDTOProductData);
            }

            return mDTOProductDataList;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductData ProductDataListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductDataID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductData_ListByID", mParams);
            DTOProductData mDTOProductData = new DTOProductData();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProductData.ProductDataID = Int64.Parse(mRow["ProductDataID"].ToString());
                mDTOProductData.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProductData.Width = mRow["Width"].ToString();
                mDTOProductData.Height = mRow["Height"].ToString();
                mDTOProductData.Length = mRow["Length"].ToString();
                mDTOProductData.FileBin = (byte[])mRow["FileBin"];
                mDTOProductData.FileName = mRow["FileName"].ToString();
                mDTOProductData.OriginPath = mRow["OriginPath"].ToString();
            }

            return mDTOProductData;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductData ProductDataCreateRecord(DTOProductData mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProductData");
            DataRow mRow = mDT.NewRow();
            mRow["ProductID"] = mDTO.ProductID;
            mRow["Width"] = mDTO.Width;
            mRow["Height"] = mDTO.Height;
            mRow["Length"] = mDTO.Length;
            mRow["FileBin"] = mDTO.FileBin;
            mRow["FileName"] = mDTO.FileName;
            mRow["OriginPath"] = mDTO.OriginPath;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProductData_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.ProductDataID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductData ProductDataUpdateRecord(DTOProductData mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProductData");
            DataRow mRow = mDT.NewRow();
            mRow["ProductDataID"] = mDTO.ProductDataID;
            mRow["ProductID"] = mDTO.ProductID;
            mRow["Width"] = mDTO.Width;
            mRow["Height"] = mDTO.Height;
            mRow["Length"] = mDTO.Length;
            mRow["FileBin"] = mDTO.FileBin;
            mRow["FileName"] = mDTO.FileName;
            mRow["OriginPath"] = mDTO.OriginPath;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProductData_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductData ProductDataDeleteRecord(DTOProductData mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductDataID";
            mParam.ParameterValue = mDTO.ProductDataID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProductData_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductDataList ProductDataListByProductID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductData_ListByProductID", mParams);
            DTOProductDataList mDTOProductDataList = new DTOProductDataList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductData mDTOProductData = new DTOProductData();
                mDTOProductData.ProductDataID = Int64.Parse(mRow["ProductDataID"].ToString());
                mDTOProductData.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProductData.Width = mRow["Width"].ToString();
                mDTOProductData.Height = mRow["Height"].ToString();
                mDTOProductData.Length = mRow["Length"].ToString();
                mDTOProductData.FileBin = (byte[])mRow["FileBin"];
                mDTOProductData.FileName = mRow["FileName"].ToString();
                mDTOProductData.OriginPath = mRow["OriginPath"].ToString();
                mDTOProductDataList.Add(mDTOProductData);
            }

            return mDTOProductDataList;
        }

        #endregion


        #region ************************tblProductGroup CRUDS ******************************************


        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroup ProductGroupListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductGroupID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductGroup_ListByID", mParams);
            DTOProductGroup mDTOProductGroup = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProductGroup = new DTOProductGroup();
                mDTOProductGroup.ProductGroupID = int.Parse(mRow["ProductGroupID"].ToString());
                mDTOProductGroup.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductGroup.SortPosition = int.Parse(mRow["SortPosition"].ToString());
                mDTOProductGroup.ProductGroupText = mRow["ProductGroupText"].ToString();
                mDTOProductGroup.InActive = Boolean.Parse(mRow["InActive"].ToString());
            }

            return mDTOProductGroup;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroup ProductGroupCreateRecord(DTOProductGroup mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProductGroup");
            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();


            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["SortPosition"] = mDTO.SortPosition;
            mRow["ProductGroupText"] = mDTO.ProductGroupText;
            mRow["InActive"] = mDTO.InActive;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProductGroup_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.ProductGroupID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroup ProductGroupUpdateRecord(DTOProductGroup mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProductGroup");
            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();


            mRow["ProductGroupID"] = mDTO.ProductGroupID;
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["SortPosition"] = mDTO.SortPosition;
            mRow["ProductGroupText"] = mDTO.ProductGroupText;
            mRow["InActive"] = mDTO.InActive;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProductGroup_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroup ProductGroupDeleteRecord(DTOProductGroup mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductGroupID";
            mParam.ParameterValue = mDTO.ProductGroupID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProductGroup_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupList ProductGroupListBySalesOrgID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductGroup_ListBySalesOrgID", mParams);
            DTOProductGroupList mDTOProductGroupList = new DTOProductGroupList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductGroup mDTOProductGroup = new DTOProductGroup();
                mDTOProductGroup.ProductGroupID = int.Parse(mRow["ProductGroupID"].ToString());
                mDTOProductGroup.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductGroup.SortPosition = int.Parse(mRow["SortPosition"].ToString());
                mDTOProductGroup.ProductGroupText = mRow["ProductGroupText"].ToString();
                mDTOProductGroup.InActive = Boolean.Parse(mRow["InActive"].ToString());
                mDTOProductGroupList.Add(mDTOProductGroup);
            }

            return mDTOProductGroupList;
        }

        public DTOProductGroupList ProductGroupListBySalesOrgID_WithProduct(int mRecNo, int ProviderID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = ProviderID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductGroup_ListBySalesOrgID_WithProduct", mParams);
            DTOProductGroupList mDTOProductGroupList = new DTOProductGroupList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductGroup mDTOProductGroup = new DTOProductGroup();
                mDTOProductGroup.ProductGroupID = int.Parse(mRow["ProductGroupID"].ToString());
                mDTOProductGroup.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductGroup.SortPosition = int.Parse(mRow["SortPosition"].ToString());
                mDTOProductGroup.ProductGroupText = mRow["ProductGroupText"].ToString();
                mDTOProductGroup.InActive = Boolean.Parse(mRow["InActive"].ToString());
                mDTOProductGroupList.Add(mDTOProductGroup);
            }

            return mDTOProductGroupList;
        }


        public DTOProductGroup ProductGroupListByProductGroupText(long salesOrgID, string productGroupText)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = salesOrgID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductGroupText";
            mParam.ParameterValue = productGroupText;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductGroup_ListByProductGroupText", mParams);
            DTOProductGroup mDTOProductGroup = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProductGroup = new DTOProductGroup();
                mDTOProductGroup.ProductGroupID = int.Parse(mRow["ProductGroupID"].ToString());
                mDTOProductGroup.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProductGroup.SortPosition = int.Parse(mRow["SortPosition"].ToString());
                mDTOProductGroup.ProductGroupText = mRow["ProductGroupText"].ToString();
                mDTOProductGroup.InActive = Boolean.Parse(mRow["InActive"].ToString());
            }

            return mDTOProductGroup;
        }
        #endregion


        #region ************************tblProductGroupLine CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLineList ProductGroupLineList()
        {
            DTOProductGroupLineList mDTOProductGroupLineList = new DTOProductGroupLineList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductGroupLine_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductGroupLine mDTOProductGroupLine = new DTOProductGroupLine();
                mDTOProductGroupLine.ProductGroupLineID = int.Parse(mRow["ProductGroupLineID"].ToString());
                mDTOProductGroupLine.ProductGroupID = int.Parse(mRow["ProductGroupID"].ToString());
                mDTOProductGroupLine.SortPosition = int.Parse(mRow["SortPosition"].ToString());
                mDTOProductGroupLine.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProductGroupLine.DefaultQty = float.Parse(mRow["DefaultQty"].ToString());
                mDTOProductGroupLineList.Add(mDTOProductGroupLine);
            }

            return mDTOProductGroupLineList;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLine ProductGroupLineListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductGroupLineID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductGroupLine_ListByID", mParams);
            DTOProductGroupLine mDTOProductGroupLine = null;

            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProductGroupLine = new DTOProductGroupLine();
                mDTOProductGroupLine.ProductGroupLineID = int.Parse(mRow["ProductGroupLineID"].ToString());
                mDTOProductGroupLine.ProductGroupID = int.Parse(mRow["ProductGroupID"].ToString());
                mDTOProductGroupLine.SortPosition = int.Parse(mRow["SortPosition"].ToString());
                mDTOProductGroupLine.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProductGroupLine.DefaultQty = float.Parse(mRow["DefaultQty"].ToString());
            }

            return mDTOProductGroupLine;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLine ProductGroupLineCreateRecord(DTOProductGroupLine mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProductGroupLine");
            DataRow mRow = mDT.NewRow();
            mRow["ProductGroupID"] = mDTO.ProductGroupID;
            mRow["SortPosition"] = mDTO.SortPosition;
            mRow["ProductID"] = mDTO.ProductID;
            mRow["DefaultQty"] = mDTO.DefaultQty;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProductGroupLine_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.ProductGroupLineID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLine ProductGroupLineUpdateRecord(DTOProductGroupLine mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProductGroupLine");
            DataRow mRow = mDT.NewRow();
            mRow["ProductGroupLineID"] = mDTO.ProductGroupLineID;
            mRow["ProductGroupID"] = mDTO.ProductGroupID;
            mRow["SortPosition"] = mDTO.SortPosition;
            mRow["ProductID"] = mDTO.ProductID;
            mRow["DefaultQty"] = mDTO.DefaultQty;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProductGroupLine_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLine ProductGroupLineDeleteRecord(DTOProductGroupLine mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductGroupLineID";
            mParam.ParameterValue = mDTO.ProductGroupLineID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProductGroupLine_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLineList ProductGroupLineListByProductGroupID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductGroupID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductGroupLine_ListByProductGroupID", mParams);
            DTOProductGroupLineList mDTOProductGroupLineList = new DTOProductGroupLineList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductGroupLine mDTOProductGroupLine = new DTOProductGroupLine();
                mDTOProductGroupLine.ProductGroupLineID = int.Parse(mRow["ProductGroupLineID"].ToString());
                mDTOProductGroupLine.ProductGroupID = int.Parse(mRow["ProductGroupID"].ToString());
                mDTOProductGroupLine.SortPosition = int.Parse(mRow["SortPosition"].ToString());
                mDTOProductGroupLine.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProductGroupLine.DefaultQty = float.Parse(mRow["DefaultQty"].ToString());
                mDTOProductGroupLineList.Add(mDTOProductGroupLine);
            }

            return mDTOProductGroupLineList;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLine ProductGroupLineListByProductID(long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductGroupLine_ListByProductID", mParams);
            DTOProductGroupLine mDTOProductGroupLine = null;
            foreach (DataRow mRow in mDT.Rows)
            {
                mDTOProductGroupLine = new DTOProductGroupLine();
                mDTOProductGroupLine.ProductGroupLineID = int.Parse(mRow["ProductGroupLineID"].ToString());
                mDTOProductGroupLine.ProductGroupID = int.Parse(mRow["ProductGroupID"].ToString());
                mDTOProductGroupLine.SortPosition = int.Parse(mRow["SortPosition"].ToString());
                mDTOProductGroupLine.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProductGroupLine.DefaultQty = float.Parse(mRow["DefaultQty"].ToString());

            }

            return mDTOProductGroupLine;
        }

        #endregion


        #region ************************tblProductUOM CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductUOMList ProductUOMList()
        {
            DTOProductUOMList mDTOProductUOMList = new DTOProductUOMList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductUOM_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProductUOM mDTOProductUOM = new DTOProductUOM();
                mDTOProductUOM.ProductUOMID = int.Parse(mRow["ProductUOMID"].ToString());
                mDTOProductUOM.ProductUOM = mRow["ProductUOM"].ToString();
                mDTOProductUOMList.Add(mDTOProductUOM);
            }

            return mDTOProductUOMList;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductUOM ProductUOMListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductUOMID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProductUOM_ListByID", mParams);
            DTOProductUOM mDTOProductUOM = null;

            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProductUOM = new DTOProductUOM();
                mDTOProductUOM.ProductUOMID = int.Parse(mRow["ProductUOMID"].ToString());
                mDTOProductUOM.ProductUOM = mRow["ProductUOM"].ToString();
            }

            return mDTOProductUOM;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductUOM ProductUOMCreateRecord(DTOProductUOM mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProductUOM");
            DataRow mRow = mDT.NewRow();
            mRow["ProductUOM"] = mDTO.ProductUOM;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProductUOM_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.ProductUOMID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductUOM ProductUOMUpdateRecord(DTOProductUOM mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProductUOM");
            DataRow mRow = mDT.NewRow();
            mRow["ProductUOMID"] = mDTO.ProductUOMID;
            mRow["ProductUOM"] = mDTO.ProductUOM;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProductUOM_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductUOM ProductUOMDeleteRecord(DTOProductUOM mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductUOMID";
            mParam.ParameterValue = mDTO.ProductUOMID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProductUOM_DELETE", mParams);
            return mDTO;
        }


        #endregion


        public DataSet GetReferenceData(long salesOrgID, long salesRepAccountID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = salesOrgID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesRepAccountID";
            mParam.ParameterValue = salesRepAccountID;
            mParams.Add(mParam);

            return mDBService.GetDataSet(SQLCommandTypes.StoredProcedure, "usp_CAT_GetReferenceData", mParams);
        }

        public Boolean CheckProductIfithastSentOrder(int SalesOrgID, int ProductID) {

            Boolean res = false;

            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = SalesOrgID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductID";
            mParam.ParameterValue = ProductID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProduct_CheckifithasSentOrder", mParams);

            if (int.Parse(mDT.Rows[0][0].ToString()) > 0)
            {

                res = true;

            }
            else {


                res = false;
            }


            return res;
        }



        public Boolean CheckProductNumberIfExistBySalesOrg(int SalesOrgID, long OrderNumber)
        {

            Boolean res = false;

            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();

            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = SalesOrgID;
            mParams.Add(mParam);


            mParam = new DTODBParameter();

            mParam.ParameterName = "@OrderNumber";
            mParam.ParameterValue = OrderNumber;
            mParams.Add(mParam);

         

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_OrderNumberBySalesOrgIDifExist", mParams);

            if (int.Parse(mDT.Rows[0][0].ToString()) > 0)
            {

                res = true;

            }
            else
            {

                res = false;
            }


            return res;
        }


        public Int64 GetMaxProductNumberbySalesOrg(int SalesOrgID)
        {

            Int64 res = 0;

            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = SalesOrgID;
            mParams.Add(mParam);


            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_GetMaxOrderNumberBySalesOrgID", mParams);


            if (mDT != null && Int64.Parse(mDT.Rows[0][0].ToString()) > 0)
            {

                res = Int64.Parse(mDT.Rows[0][0].ToString());

            }
            else
            {

                res = 0;
            }


            return res;
        }

    } //end class
}
