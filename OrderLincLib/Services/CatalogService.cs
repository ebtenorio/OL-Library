using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;
using System.Text;
using System.Transactions;

namespace OrderLinc.Services
{
    public class CatalogService : ICatalogService
    {

        private IProductDA _productDA;
        private ILogService _logService;
        const string SOURCE = "Catalog Service";

        public CatalogService(IProductDA productDA, ILogService logService)
        {
            _logService = logService;
            _productDA = productDA;
        }

        #region ************************tblProduct CRUDS ******************************************


        public DTOProductList ProductListByProductGroupID(int productGroupID, long providerID, int currentPageID, int pageItemCount)
        {
            ProductCriteria criteria = new ProductCriteria()
            {
                SearchType = ProductSearchType.ByProductGroupID,
                ProductGroupID = productGroupID,
                ProviderID = providerID,
                CurrentPage = currentPageID,
                PageItemCount = pageItemCount
            };
            return _productDA.ProductListByCriteria(criteria);
        }

        public DTOProductList ProductListByProductGroupIDandProviderID(int productGroupID, long providerID, int currentPageID, int pageItemCount)
        {
            return _productDA.ProductListByProductGroupIDandProviderID(productGroupID, providerID, currentPageID, pageItemCount);
        }

        public Boolean HasAffectedOrderByDateChange(int SalesOrgID, long ProviderID, string ProviderProductCode, DateTime NewStartDate, DateTime NewEndDate, out string OrderNumber, out DateTime RequestedReleaseDate)
        {
            return _productDA.HasAffectedOrderByDateChange(SalesOrgID, ProviderID, ProviderProductCode, NewStartDate, NewEndDate, out OrderNumber, out RequestedReleaseDate);
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProduct ProductListByID(long providerID, long mRecNo)
        {
            ProductCriteria criteria = new ProductCriteria()
            {
                ProductID = mRecNo,
                ProviderID = providerID,
                SearchType = ProductSearchType.ByProductID
            };
            return _productDA.ProductByCriteria(criteria);
        }

        /// <summary>
        /// This function returns the Product object without the ProductCode, StartDate and EndDate populated.
        /// </summary>
        /// <param name="GTINCode"></param>
        /// <returns></returns>
        public DTOProduct ProductListByGTINCode(long saleOrgID, string GTINCode)
        {
            ProductCriteria criteria = new ProductCriteria()
            {
                GTINCode = GTINCode,
                SalesOrgID = saleOrgID,
                SearchType = ProductSearchType.ByGTINCode
            };
            return _productDA.ProductByCriteria(criteria);
        }

        public DTOProduct ProductListByProductCode(long salesOrgID, string productCode)
        {

            ProductCriteria criteria = new ProductCriteria()
            {
                ProductCode = productCode,
                SalesOrgID = salesOrgID,
                SearchType = ProductSearchType.ByProductCode
            };
            return _productDA.ProductByCriteria(criteria);

        }

        public DTOProduct ProductListByProviderProductCode(long providerID, string productCode)
        {

            ProductCriteria criteria = new ProductCriteria()
            {
                ProductCode = productCode,
                ProviderID = providerID,
                SearchType = ProductSearchType.ByProductCode
            };
            return _productDA.ProductByCriteria(criteria);

        }

        //public DTOProduct ProductListByProductName(long salesOrgID, string productName)
        //{
        //    ProductCriteria criteria = new ProductCriteria()
        //    {
        //        ProductName = productName,
        //        SalesOrgID = salesOrgID,
        //        SearchType = ProductSearchType.ByProductName
        //    };
        //    return _productDA.ProductByCriteria(criteria);
        //}

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProduct ProductSaveRecord(DTOProduct mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("product");

                if (mDTO.ProductID == 0)
                    return _productDA.ProductCreateRecord(mDTO);
                else
                {

                    return _productDA.ProductUpdateRecord(mDTO);
                }
            }
            catch (Exception ex)
            {
                _logService.LogSave("Product", string.Format("ProductSaveRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }

        public void ProductDeleteRecord(long mRecNo, long userId)
        {
            try
            {
                _productDA.ProductDeleteRecord(new DTOProduct() { ProductID = mRecNo });
            }
            catch (Exception ex)
            {
                _logService.LogSave(SOURCE, string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace), userId);
                throw;
            }

        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public Boolean ProductIsValid(DTOProduct mDTO, out string mValidationResponse)
        {
            StringBuilder blr = new StringBuilder(100);

            if (mDTO.SalesOrgID == 0)
            {
                blr.AppendLine("SalesOrgID cannnot be 0.");

            }

            //if (string.IsNullOrEmpty(mDTO.GTINCode))
            //{
            //    blr.AppendLine("GTIN Code cannnot be empty.");
            //}
            //else
            //{
            //    DTOProduct product = this.ProductListByGTINCode(mDTO.SalesOrgID, mDTO.GTINCode);
            //    if (product != null && product.ProductID != mDTO.ProductID)
            //    {
            //        DTOProductGroupLine mGroupLine = ProductGroupLineListByProductID(int.Parse(product.ProductID.ToString())); 

            //        DTOProductGroup mGroup = ProductGroupListByID(mGroupLine.ProductGroupID);

            //        blr.AppendLine("GTINCode already exists in Product Group " + mGroup.ProductGroupText + ".");

            //        product = null;
            //    }
                

            //}

            if (string.IsNullOrEmpty(mDTO.ProductDescription))
            {
                blr.AppendLine("Product Description cannnot be null.");
            }
            // Commented out functions below not in used. Ringo Ray Piedraverde 22-08-2014
            
            //if (string.IsNullOrEmpty(mDTO.ProductCode))
            //{
            //    blr.AppendLine("Product Description cannnot be null.");
            //}

            //if (mDTO.ProviderID == 0)
            //{
            //    blr.AppendLine("ProviderID cannot be 0.");
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(mDTO.ProductCode))
            //    {
            //        DTOProduct p = this.ProductListByProviderProductCode(mDTO.ProviderID, mDTO.ProductCode);

            //        if (p != null && p.ProductID != mDTO.ProductID)
            //        {
            //            blr.AppendLine("Product code is already exists.");
            //        }
            //        p = null;
            //    }

            //}



            //if (mDTO.GTINCode == null)
            //{
            //    blr.AppendLine( "GTINCode cannnot be null.");

            //}

            //if (mDTO.ProductBrandID == 0)
            //{
            //    blr.AppendLine( "ProductBrandID cannnot be 0.");

            //}

            //if (mDTO.ProductCategoryID == 0)
            //{
            //    blr.AppendLine( "ProductCategoryID cannnot be 0.");

            //}

            //if (mDTO.PrimarySKU == 0)
            //{
            //    blr.AppendLine("PrimarySKU cannnot be 0.");

            //}



            if (mDTO.UOMID == 0)
            {
                blr.AppendLine("UOMID cannnot be 0.");
            }

            mValidationResponse = blr.ToString();

            if (blr.Length == 0) mValidationResponse = "OK";

            return blr.ToString() == string.Empty;
        }


        ////CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        //public DTOProductList ProductListBySalesOrgID(int mRecNo)
        //{
        //    return _productDA.ProductListBySalesOrgID(mRecNo);
        //}

        ////CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        //public DTOProductList ProductListByProductBrandID(int mRecNo)
        //{
        //    return _productDA.ProductListByProductBrandID(mRecNo);
        //}

        ////CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        //public DTOProductList ProductListByProductCategoryID(int mRecNo)
        //{
        //    return _productDA.ProductListByProductCategoryID(mRecNo);
        //}

        #endregion


        #region ************************tblProductBrand CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductBrandList ProductBrandList()
        {
            return _productDA.ProductBrandList();
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductBrand ProductBrandListByID(int mRecNo)
        {
            return _productDA.ProductBrandListByID(mRecNo);
        }

        public DTOProductBrand ProductBrandSaveRecord(DTOProductBrand mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("productbrand");

                if (mDTO.ProductBrandID == 0)
                    return _productDA.ProductBrandCreateRecord(mDTO);
                else
                    return _productDA.ProductBrandUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public void ProductBrandDeleteRecord(long mRecNo)
        {
            try
            {
                _productDA.ProductBrandDeleteRecord(new DTOProductBrand() { ProductBrandID = mRecNo });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public Boolean ProductBrandIsValid(DTOProductBrand mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }

            if (mDTO.ProductBrandText == null)
            {
                mValidationResponse = "ProductBrandText cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductBrandList ProductBrandListBySalesOrgID(int mRecNo)
        {
            return _productDA.ProductBrandListBySalesOrgID(mRecNo);
        }

        #endregion


        #region ************************tblProductCategory CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategoryList ProductCategoryList()
        {
            return _productDA.ProductCategoryList();
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategory ProductCategoryListByID(int mRecNo)
        {
            return _productDA.ProductCategoryListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategory ProductCategorySaveRecord(DTOProductCategory mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("Product Category");

                if (mDTO.ProductCategoryID == 0)
                    return _productDA.ProductCategoryCreateRecord(mDTO);
                else
                    return _productDA.ProductCategoryUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ProductCategoryDeleteRecord(int mRecNo)
        {

            try
            {

                _productDA.ProductCategoryDeleteRecord(new DTOProductCategory() { ProductCategoryID = mRecNo });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public Boolean ProductCategoryIsValid(DTOProductCategory mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }

            if (mDTO.ProductCategoryText == null)
            {
                mValidationResponse = "ProductCategoryText cannnot be null.";
                return false;
            }

            if (mDTO.ParentCategoryID == 0)
            {
                mValidationResponse = "ParentCategoryID cannnot be 0.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategoryList ProductCategoryListBySalesOrgID(int mRecNo)
        {
            return _productDA.ProductCategoryListBySalesOrgID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductCategoryList ProductCategoryListByParentCategoryID(int mRecNo)
        {
            return _productDA.ProductCategoryListByParentCategoryID(mRecNo);
        }

        #endregion


        #region ************************tblProductData CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductDataList ProductDataList()
        {
            return _productDA.ProductDataList();
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductData ProductDataListByID(int mRecNo)
        {
            return _productDA.ProductDataListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductData ProductDataSaveRecord(DTOProductData mDTO)
        {

            try
            {
                if (mDTO == null) throw new ArgumentNullException("Product Data");

                if (mDTO.ProductDataID == 0)
                    return _productDA.ProductDataCreateRecord(mDTO);
                else
                    return _productDA.ProductDataUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ProductDataDeleteRecord(long mRecNo)
        {
            try
            {
                _productDA.ProductDataDeleteRecord(new DTOProductData() { ProductDataID = mRecNo });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public Boolean ProductDataIsValid(DTOProductData mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.ProductID == 0)
            {
                mValidationResponse = "ProductID cannnot be 0.";
                return false;
            }

            if (mDTO.Width == null)
            {
                mValidationResponse = "Width cannnot be null.";
                return false;
            }

            if (mDTO.Height == null)
            {
                mValidationResponse = "Height cannnot be null.";
                return false;
            }

            if (mDTO.Length == null)
            {
                mValidationResponse = "Length cannnot be null.";
                return false;
            }

            if (mDTO.FileBin == null)
            {
                mValidationResponse = "FileBin cannnot be null.";
                return false;
            }

            if (mDTO.FileName == null)
            {
                mValidationResponse = "FileName cannnot be null.";
                return false;
            }

            if (mDTO.OriginPath == null)
            {
                mValidationResponse = "OriginPath cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductDataList ProductDataListByProductID(int mRecNo)
        {
            return _productDA.ProductDataListByProductID(mRecNo);
        }

        #endregion


        #region ************************tblProductGroup CRUDS ******************************************


        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroup ProductGroupListByID(int mRecNo)
        {
            return _productDA.ProductGroupListByID(mRecNo);
        }

        public DTOProductGroup ProductGroupListByProductGroupText(long salesOrgID, string productGroupText)
        {
            return _productDA.ProductGroupListByProductGroupText(salesOrgID, productGroupText);
        }


        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroup ProductGroupSaveRecord(DTOProductGroup mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("Product Group");

                if (mDTO.ProductGroupID == 0)
                    return _productDA.ProductGroupCreateRecord(mDTO);
                else
                    return _productDA.ProductGroupUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ProductGroupDeleteRecord(int mRecNo)
        {

            try
            {
                _productDA.ProductGroupDeleteRecord(new DTOProductGroup() { ProductGroupID = mRecNo });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public Boolean ProductGroupIsValid(DTOProductGroup mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }

            //if (mDTO.SortPosition == 0)
            //{
            //    mValidationResponse = "SortPosition cannnot be 0.";
            //    return false;
            //}

            if (mDTO.ProductGroupText == null)
            {
                mValidationResponse = "ProductGroupText cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupList ProductGroupListBySalesOrgID(int mRecNo)
        {
            return _productDA.ProductGroupListBySalesOrgID(mRecNo);
        }



        public DTOProductGroupList ProductGroupListBySalesOrgID_WithProduct(int mRecNo,int ProviderID)
        {
            return _productDA.ProductGroupListBySalesOrgID_WithProduct(mRecNo, ProviderID);
        }

        #endregion


        #region ************************tblProductGroupLine CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLineList ProductGroupLineList()
        {
            return _productDA.ProductGroupLineList();
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLine ProductGroupLineListByID(int mRecNo)
        {
            return _productDA.ProductGroupLineListByID(mRecNo);
        }

        public DTOProductGroupLine ProductGroupLineSaveRecord(DTOProductGroupLine mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("Product Group Line");

                if (mDTO.ProductGroupLineID == 0)
                    return _productDA.ProductGroupLineCreateRecord(mDTO);
                else
                    return _productDA.ProductGroupLineUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Product Group Line", string.Format("ProductGroupLineSaveRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }

        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public void ProductGroupLineDeleteRecord(int mRecNo)
        {
            try
            {
                _productDA.ProductGroupLineDeleteRecord(new DTOProductGroupLine() { ProductGroupLineID = mRecNo });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public Boolean ProductGroupLineIsValid(DTOProductGroupLine mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.ProductGroupID == 0)
            {
                mValidationResponse = "ProductGroupID cannnot be 0.";
                return false;
            }

            //if (mDTO.SortPosition == 0)
            //{
            //    mValidationResponse = "SortPosition cannnot be 0.";
            //    return false;
            //}

            if (mDTO.ProductID == 0)
            {
                mValidationResponse = "ProductID cannnot be 0.";
                return false;
            }

            //if (mDTO.DefaultQty == 0)
            //{
            //    mValidationResponse = "DefaultQty cannnot be 0.";
            //    return false;
            //}

            mValidationResponse = "Ok";
            return true;
        }

        public Boolean CheckProductIfithastSentOrder(int SalesOrg, int ProductID)
        {
            return _productDA.CheckProductIfithastSentOrder(SalesOrg, ProductID);
        }


        public Boolean CheckProductNumberIfExistBySalesOrg(int SalesOrg, long OrderNumber)
        {
            return _productDA.CheckProductNumberIfExistBySalesOrg(SalesOrg, OrderNumber);
        }

        public Int64 GetMaxProductNumberbySalesOrg(int SalesOrgID)
        {
            return _productDA.GetMaxProductNumberbySalesOrg(SalesOrgID);
        }


        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLineList ProductGroupLineListByProductGroupID(int mRecNo)
        {
            return _productDA.ProductGroupLineListByProductGroupID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductGroupLine ProductGroupLineListByProductID(long mRecNo)
        {
            return _productDA.ProductGroupLineListByProductID(mRecNo);
        }

        #endregion


        #region ************************tblProductUOM CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductUOMList ProductUOMList()
        {
            return _productDA.ProductUOMList();
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductUOM ProductUOMListByID(int mRecNo)
        {
            return _productDA.ProductUOMListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public DTOProductUOM ProductUOMSaveRecord(DTOProductUOM mDTO)
        {

            try
            {
                if (mDTO == null) throw new ArgumentNullException("Product UOM");

                if (mDTO.ProductUOMID == 0)
                    return _productDA.ProductUOMCreateRecord(mDTO);
                else
                    return _productDA.ProductUOMUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void ProductUOMDeleteRecord(int mRecNo)
        {

            try
            {

                _productDA.ProductUOMDeleteRecord(new DTOProductUOM() { ProductUOMID = mRecNo });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:32:50 PM Lazy Dog 3.3.1.0
        public Boolean ProductUOMIsValid(DTOProductUOM mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.ProductUOM == null)
            {
                mValidationResponse = "ProductUOM cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        #endregion



        public DataSet GetReferenceData(long salesOrgID, long salesRepAccountID)
        {
            return _productDA.GetReferenceData(salesOrgID, salesRepAccountID);
        }



    } //end class
}

