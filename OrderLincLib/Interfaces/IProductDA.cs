using System;
using OrderLinc.DTOs;
using System.Data;
namespace OrderLinc.IDataContracts
{
    public interface IProductDA
    {
        DTOProductBrand ProductBrandCreateRecord(DTOProductBrand mDTO);
        DataSet GetReferenceData(long salesOrgID, long salesRepAccountID);
        DTOProduct ProductCreateRecord(DTOProduct mDTO);
        DTOProduct ProductDeleteRecord(DTOProduct mDTO);
        //DTOProduct ProductListByID(int mRecNo);
        DTOProduct ProductUpdateRecord(DTOProduct mDTO);
        DTOProduct ProductByCriteria(ProductCriteria criteria);

        //DTOProductList ProductListByProductBrandID(int mRecNo);
        //DTOProductList ProductListByProductCategoryID(int mRecNo);
        //DTOProductList ProductListBySalesOrgID(int mRecNo);

        DTOProductList ProductListByCriteria(ProductCriteria criteria);

        DTOProductBrand ProductBrandDeleteRecord(DTOProductBrand mDTO);
        DTOProductBrand ProductBrandListByID(int mRecNo);
        DTOProductBrand ProductBrandUpdateRecord(DTOProductBrand mDTO);

        DTOProductBrandList ProductBrandList();
        DTOProductBrandList ProductBrandListBySalesOrgID(int mRecNo);

        DTOProductCategory ProductCategoryCreateRecord(DTOProductCategory mDTO);
        DTOProductCategory ProductCategoryDeleteRecord(DTOProductCategory mDTO);
        DTOProductCategory ProductCategoryListByID(int mRecNo);
        DTOProductCategory ProductCategoryUpdateRecord(DTOProductCategory mDTO);

        DTOProductCategoryList ProductCategoryList();
        DTOProductCategoryList ProductCategoryListByParentCategoryID(int mRecNo);
        DTOProductCategoryList ProductCategoryListBySalesOrgID(int mRecNo);

        DTOProductData ProductDataCreateRecord(DTOProductData mDTO);
        DTOProductData ProductDataDeleteRecord(DTOProductData mDTO);
        DTOProductData ProductDataListByID(int mRecNo);
        DTOProductData ProductDataUpdateRecord(DTOProductData mDTO);

        DTOProductDataList ProductDataList();
        DTOProductDataList ProductDataListByProductID(int mRecNo);

        DTOProductGroup ProductGroupCreateRecord(DTOProductGroup mDTO);
        DTOProductGroup ProductGroupDeleteRecord(DTOProductGroup mDTO);
        DTOProductGroup ProductGroupListByID(int mRecNo);
        DTOProductGroup ProductGroupUpdateRecord(DTOProductGroup mDTO);
        DTOProductGroup ProductGroupListByProductGroupText(long salesOrgID, string productGroupText);   
        DTOProductGroupList ProductGroupListBySalesOrgID(int mRecNo);
        DTOProductGroupList ProductGroupListBySalesOrgID_WithProduct(int mRecNo, int ProviderID);
        

        DTOProductGroupLine ProductGroupLineCreateRecord(DTOProductGroupLine mDTO);
        DTOProductGroupLine ProductGroupLineDeleteRecord(DTOProductGroupLine mDTO);
        DTOProductGroupLine ProductGroupLineListByID(int mRecNo);
        DTOProductGroupLine ProductGroupLineUpdateRecord(DTOProductGroupLine mDTO);

        DTOProductGroupLineList ProductGroupLineList();
        DTOProductGroupLineList ProductGroupLineListByProductGroupID(int mRecNo);
        DTOProductGroupLine ProductGroupLineListByProductID(long mRecNo);

        Boolean HasAffectedOrderByDateChange(int SalesOrgID, long ProviderID, string ProviderProductCode, DateTime NewStartDate, DateTime NewEndDate, out string OrderNumber, out DateTime RequestedReleaseDate);

        DTOProductUOM ProductUOMCreateRecord(DTOProductUOM mDTO);
        DTOProductUOM ProductUOMDeleteRecord(DTOProductUOM mDTO);
        DTOProductUOM ProductUOMListByID(int mRecNo);
        DTOProductUOM ProductUOMUpdateRecord(DTOProductUOM mDTO);
        DTOProductUOMList ProductUOMList();

        Boolean CheckProductIfithastSentOrder(int SalesOrg, int ProductID);


        Boolean CheckProductNumberIfExistBySalesOrg(int SalesOrg, long OrderNumber);
         Int64 GetMaxProductNumberbySalesOrg(int SalesOrgID);

        DTOProductList ProductListByProductGroupIDandProviderID(int productGroupID, long providerID, int currentPageID, int pageItemCount);

       
    }
}
