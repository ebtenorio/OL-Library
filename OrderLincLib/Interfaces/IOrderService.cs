using System;
using OrderLinc.DTOs;
using System.Data;
namespace OrderLinc.IDataContracts
{
    public interface IOrderService
    {
        bool OrderIsValid(DTOOrder mDTO, out string mValidationResponse);
        bool OrderLineIsValid(DTOOrderLine mDTO, out string mValidationResponse);
        bool OrderSignatureIsValid(DTOOrderSignature mDTO, out string mValidationResponse);
        DTOMessageInbound MessageInboundSaveRecord(DTOMessageInbound mDTO);
        DTOMessageInboundList MessageInboundListBySentFlag(bool sentFlag, bool error);
        DTOOrder OrderListBySenderReceiver(long mRecNo, string senderCode, string ReceiverCode);
        DTOOrder OrderSaveRecord(DTOOrder mDTO);
        DTOOrder OrderListByID(long mRecNo);
        DTOOrder ListOrderByOrderNumberANDSalesOrgID(long OrderNumber, long SalesOrgID);

        DTOOrderList OrderListByCustomerID(int mRecNo);
        DTOOrderList OrderListByOrderGUID(int mRecNo);
        DTOOrderList OrderListByProviderID(int mRecNo);
        DTOOrderList OrderListByProviderWarehouseID(int mRecNo);
        DTOOrderList OrderListBySalesOrgID(int mRecNo);
        DTOOrderList OrderListBySalesRepAccountID(int mRecNo);
        DTOOrderList OrderListBySYSOrderStatusID(int mRecNo);
        DTOOrderList OrderListByOrderStatus(bool isSent, bool isHeld);
        DTOOrderList OrderListToBeReleased(int mSalesOrgID, DateTime mDateCriteria, string mReleaseType);

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

        DTOOrderList OrderListBySalesOrgID_OfficeSentOrders(int mRecNo, long providerID, long AccountID, int AccountType, int CurrentPage, int PageItemCount, 
            long OrderNo, long CustomerID, long CreatedByUserID, DateTime DateFrom, DateTime DateTo,
            int StatusID, string GTINCode, int SYSStateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo);

        DTOOrderList OrderListBySalesOrgID_SalesRepSentOrders(int mRecNo, long providerID, long AccountID, int AccountType, int CurrentPage, int PageItemCount, int OrgUnit,
            long OrderNo, long CustomerID, long CreatedByUserID, DateTime DateFrom, DateTime DateTo, 
            int StatusID, string GTINCode, int StateID, bool? IsRegularOrder, 
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo);

        DataTable OrderListBySalesOrgID_ExportOrders(int mRecNo, long providerID, long AccountID, int AccountType,
            int CurrentPage, int PageItemCount, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int SYSStateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo, string CreationMethod);

        
        DTOOrderList OrderListBySalesOrgID_AllSentOrders(int mRecNo,
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
            DateTime ActualReleaseDateTo
         );

        DataTable OrderGetCount(long refId, long accountID, int orgUnitID, int accountTypeID);

        DTOOrderLine OrderLineListByID(long providerID, long orderLineID);
        DTOOrderLine OrderLineSaveRecord(DTOOrderLine mDTO);

        DTOOrderLineList OrderLineListByOrderID(long mRecNo);
        DTOOrderLineList OrderLineListByProductID(int mRecNo);

        DTOOrder OrderListByGUIDID(string mGUID);

        DTOOrderSignature OrderSignatureListByID(int mRecNo);
        DTOOrderSignature OrderSignatureListByOrderID(long mRecNo);
        DTOOrderSignature OrderSignatureSaveRecord(DTOOrderSignature mDTO);
        DTOSYSOrderStatus OrderStatusListByCode(string orderStatusCode);
        DTOSYSOrderStatus OrderStatusListByID(int orderStatusID);
        DTOSYSOrderStatus OrderStatusListByText(string orderStatusText);
        DTOSYSOrderStatusList OrderStatusList();
        DTOSYSOrderStatusList SYSOrderStatusList();

        void OrderDeleteRecord(long mRecNo);
        void OrderLineDeleteRecord(long mRecNo);
        void OrderSignatureDeleteRecord(long orderID);
        void OrderUpdateStatus(DTOOrder mDTO);
      


    }
}
