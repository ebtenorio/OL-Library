using System;
using OrderLinc.DTOs;
using System.Data;
namespace OrderLinc.IDataContracts
{
    public interface IOrderDA
    {
        //DTOOrderList OrderList();
        DTOOrder OrderCreateRecord(DTOOrder mDTO);
        DTOOrder OrderDeleteRecord(DTOOrder mDTO);
        DTOOrder OrderByCriteria(OrderCriteria criteria);
        DTOOrder ListOrderByOrderNumberANDSalesOrgID(long OrderNumber, long SalesOrgID);

        DataTable OrderGetCount(OrderCriteria criteria);

        bool OrderIsValid(DTOOrder mDTO, out string mValidationResponse);
        DTOOrderLine OrderLineCreateRecord(DTOOrderLine mDTO);
        DTOOrderLine OrderLineDeleteRecord(DTOOrderLine mDTO);
        bool OrderLineIsValid(DTOOrderLine mDTO, out string mValidationResponse);

        DTOOrderLine OrderLineListByID(long providerID, long orderLineID);
        DTOOrderLineList OrderLineListByOrderID( long orderID);
        DTOOrderLineList OrderLineListByProductID(int mRecNo);
        DTOOrderLine OrderLineUpdateRecord(DTOOrderLine mDTO);

        DTOOrderList OrderListByCreatedByUserID(int mRecNo);
        DTOOrderList OrderListByCustomerID(int mRecNo);
        DTOOrderList OrderListToBeReleased(int mSalesOrgID, DateTime mDateCriteria, string mReleaseType);

        DTOOrder OrderListByGUIDID(string mGUID);

        DTOOrderList OrderListByCriteria(OrderCriteria criteria);

        // DTOOrderList OrderListBySalesOrgID_OfficeNewOrders(int mRecNo, long AccountID, int AccountType, int CurrentPage, int PageItemCount);

        DTOOrderList OrderListBySalesOrgID_OfficeNewOrders(int mRecNo, long AccountID, int AccountType, int CurrentPage, int PageItemCount,
            long providerID, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, string GTINCode, int StateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo);

        DTOOrderList OrderListBySalesOrgID_SalesRepNewOrders(int mRecNo, long AccountID, int AccountType, int CurrentPage, int PageItemCount, int OrgUnit,
            long providerID, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, string GTINCode, int StateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo);

        DTOOrderList OrderListBySalesOrgID_AllNewOrders(int mRecNo, long AccountID, int AccountType, int CurrentPage, int PageItemCount,
            long providerID, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, string GTINCode, int StateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo);

        DTOOrderList OrderListBySalesOrgID_OfficeSentOrders(int mRecNo, long providerID, long AccountID, int AccountType,
            int CurrentPage, int PageItemCount, long OrderNo, long CustomerID, long CreatedByUserID, 
            DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int SYSStateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo);

        DataTable OrderListBySalesOrgID_ExportOrders(int mRecNo, long providerID, long AccountID, int AccountType,
            int CurrentPage, int PageItemCount, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int SYSStateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo, string CreationMethod);

        DTOOrderList OrderListBySalesOrgID_SalesRepSentOrders(int mRecNo, long providerID, long AccountID, int AccountType, int CurrentPage, int PageItemCount, int OrgUnit, long OrderNo, long CustomerID, long CreatedByUserID, DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int StateID, bool? IsRegularOrder, DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo);

        DTOOrderList OrderListBySalesOrgID_AllSentOrders(
            int mRecNo,
            long providerID,
            long AccountID,
            int AccountType,
            int CurrentPage,
            int PageItemCount,
            int OrgUnit,
            long OrderNo,
            long CustomerID,
            long CreatedByUserID,
            DateTime DateFrom,
            DateTime DateTo,
            int StatusID,
            string GTINCode,
            int StateID,
            bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom,
            DateTime ActualReleaseDateTo);//(int mRecNo, long providerID, long AccountID, int AccountType, int CurrentPage, int PageItemCount, int OrgUnit, long OrderNo, long CustomerID, long CreatedByUserID, DateTime DateFrom, DateTime DateTo, int StatusID);

        DTOOrderList OrderListByOrderGUID(int mRecNo);
        DTOOrderList OrderListByProviderID(int mRecNo);
        DTOOrderList OrderListByProviderWarehouseID(int mRecNo);
        DTOOrderList OrderListBySalesOrgID(int mRecNo);
        DTOOrderList OrderListBySalesRepAccountID(int mRecNo);
        DTOOrderList OrderListBySYSOrderStatusID(int mRecNo);
        DTOOrderSignature OrderSignatureCreateRecord(DTOOrderSignature mDTO);
        DTOOrderSignature OrderSignatureDeleteRecord(DTOOrderSignature mDTO);
        bool OrderSignatureIsValid(DTOOrderSignature mDTO, out string mValidationResponse);
        DTOOrderSignatureList OrderSignatureList();
        DTOOrderSignature OrderSignatureListByID(int mRecNo);
        DTOOrderSignature OrderSignatureListByOrderID(long mRecNo);
        DTOOrderSignature OrderSignatureUpdateRecord(DTOOrderSignature mDTO);
        DTOOrder OrderUpdateRecord(DTOOrder mDTO);
        void OrderUpdateStatus(DTOOrder mDTO);
        DTOSYSOrderStatus SYSOrderStatusCreateRecord(DTOSYSOrderStatus mDTO);
        DTOSYSOrderStatus SYSOrderStatusDeleteRecord(DTOSYSOrderStatus mDTO);
        bool SYSOrderStatusIsValid(DTOSYSOrderStatus mDTO, out string mValidationResponse);
        DTOSYSOrderStatusList SYSOrderStatusList();
        DTOSYSOrderStatus SYSOrderStatusList(int mRecNo, string orderStatusCode, string orderStatusText);
        DTOSYSOrderStatus SYSOrderStatusUpdateRecord(DTOSYSOrderStatus mDTO);

        DTOMessageInboundList MessageInboundListBySentFlag(bool sentFlag, bool error);

        DTOMessageInbound MessageInboundListByID(int mRecNo);

        DTOMessageInbound MessageInboundCreateRecord(DTOMessageInbound mDTO);

        DTOMessageInbound MessageInboundUpdateRecord(DTOMessageInbound mDTO);

        DTOMessageInbound MessageInboundDeleteRecord(DTOMessageInbound mDTO);



    }
}
