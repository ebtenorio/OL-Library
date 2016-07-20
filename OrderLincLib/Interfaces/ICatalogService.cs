using System;
using OrderLinc.DTOs;
using System.Data;

namespace OrderLinc.IDataContracts
{
    public interface ICatalogService
    {

        DataSet GetReferenceData(long salesOrgID, long salesRepAccountID);
        
        void ProductBrandDeleteRecord(long mRecNo);
        DTOProductBrand ProductBrandListByID(int mRecNo);
        DTOProductBrand ProductBrandSaveRecord(DTOProductBrand mDTO);

        DTOProductBrandList ProductBrandList();
        DTOProductBrandList ProductBrandListBySalesOrgID(int mRecNo);
        bool ProductBrandIsValid(DTOProductBrand mDTO, out string mValidationResponse);

    
        DTOProductCategory ProductCategorySaveRecord(DTOProductCategory mDTO);
        void ProductCategoryDeleteRecord(int mRecNo);
        DTOProductCategory ProductCategoryListByID(int mRecNo);

        DTOProductCategoryList ProductCategoryList();
        DTOProductCategoryList ProductCategoryListByParentCategoryID(int mRecNo);
        DTOProductCategoryList ProductCategoryListBySalesOrgID(int mRecNo);

        bool ProductCategoryIsValid(DTOProductCategory mDTO, out string mValidationResponse);
        Boolean HasAffectedOrderByDateChange(int SalesOrgID, long ProviderID, string ProviderProductCode, DateTime NewStartDate, DateTime NewEndDate, out string OrderNumber, out DateTime RequestedReleaseDate);

        //DTOProductData ProductDataSaveRecord(DTOProductData mDTO);
        //void ProductDataDeleteRecord(long mRecNo);
        //DTOProductData ProductDataListByID(int mRecNo);

        //DTOProductDataList ProductDataList();
        //DTOProductDataList ProductDataListByProductID(int mRecNo);

        //bool ProductDataIsValid(DTOProductData mDTO, out string mValidationResponse);
     
        DTOProductGroupLineList ProductGroupLineListByProductGroupID(int mRecNo);
        DTOProductGroupLine ProductGroupLineListByProductID(long mRecNo);
        DTOProductGroupLineList ProductGroupLineList();

        DTOProductGroupLine ProductGroupLineSaveRecord(DTOProductGroupLine mDTO);
        DTOProductGroupLine ProductGroupLineListByID(int mRecNo);
        void ProductGroupLineDeleteRecord(int mRecNo);
        bool ProductGroupLineIsValid(DTOProductGroupLine mDTO, out string mValidationResponse);
      
        //DTOProductGroupList ProductGroupList();
        DTOProductGroupList ProductGroupListBySalesOrgID(int mRecNo);

        DTOProductGroupList ProductGroupListBySalesOrgID_WithProduct(int mRecNo, int ProviderID);
        

        DTOProductGroup ProductGroupListByID(int mRecNo);
        DTOProductGroup ProductGroupSaveRecord(DTOProductGroup mDTO);
        DTOProductGroup ProductGroupListByProductGroupText(long salesOrgID, string productGroupText);

        void ProductGroupDeleteRecord(int mRecNo);
        bool ProductGroupIsValid(DTOProductGroup mDTO, out string mValidationResponse);

        DTOProduct ProductSaveRecord(DTOProduct mDTO);
        DTOProduct ProductListByID(long providerID, long mRecNo);

        /// <summary>
        /// This function returns the Product object without the ProductCode, StartDate and EndDate populated.
        /// </summary>
        /// <param name="GTINCode"></param>
        /// <returns></returns>
        DTOProduct ProductListByGTINCode(long salesOrgID, string GTINCode);
        DTOProduct ProductListByProductCode(long salesOrgID, string productCode);
        DTOProduct ProductListByProviderProductCode(long providerID, string productCode);

        void ProductDeleteRecord(long mRecNo, long userId);

        bool ProductIsValid(DTOProduct mDTO, out string mValidationResponse);
  
        //DTOProductList ProductListByProductBrandID(int mRecNo);
        //DTOProductList ProductListByProductCategoryID(int mRecNo);
        //DTOProductList ProductListBySalesOrgID(int mRecNo);
        DTOProductList ProductListByProductGroupID(int productGroupID, long providerID, int currentPageID, int pageItemCount);
        DTOProductList ProductListByProductGroupIDandProviderID(int productGroupID, long providerID, int currentPageID, int pageItemCount);

        DTOProductUOM ProductUOMSaveRecord(DTOProductUOM mDTO);
        void ProductUOMDeleteRecord(int mRecNo);
        DTOProductUOM ProductUOMListByID(int mRecNo);

        DTOProductUOMList ProductUOMList();

        bool ProductUOMIsValid(DTOProductUOM mDTO, out string mValidationResponse);

        Boolean CheckProductIfithastSentOrder(int SalesOrg, int ProductID);


        Int64 GetMaxProductNumberbySalesOrg(int SalesOrgID);

        Boolean CheckProductNumberIfExistBySalesOrg(int SalesOrg, long OrderNumber);

    }
}
