using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;

namespace OrderLinc.Services
{
    public class OrderService : IOrderService
    {

        private IOrderDA _orderDA;
        private ILogService _logService;

        public OrderService(IOrderDA orderDA, ILogService logService)
        {
            _logService = logService;
            _orderDA = orderDA;
        }

        #region ************************tblOrder CRUDS ******************************************

        public DataTable OrderGetCount(long refId, long accountID, int orgUnitID, int accountTypeID)
        {
            OrderCriteria criteria = new OrderCriteria()
            {
                RefID = refId,
                AccountID = accountID,
                OrgUnitID = orgUnitID,
                AccountTypeID = accountTypeID
            };

            return _orderDA.OrderGetCount(criteria);
        }

        public void OrderUpdateStatus(DTOOrder mDTO)
        {
            try
            {
                if (mDTO.OrderID > 0)
                {
                    _orderDA.OrderUpdateRecord(mDTO);
                }
                else
                {
                    throw new ArgumentException("OrderID is 0.");
                }
            }
            catch (Exception ex)
            {
                _logService.LogSave("Order", string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace), mDTO.CreatedByUserID);
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrder OrderListByID(long mRecNo)
        {
            try
            {
                OrderCriteria criteria = new OrderCriteria()
                {
                    SearchType = OrderSearchType.ByOrderID,
                    OrderID = mRecNo
                };
                return _orderDA.OrderByCriteria(criteria);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DTOOrder OrderListBySenderReceiver(long mRecNo, string senderCode, string ReceiverCode)
        {
            try
            {
                OrderCriteria criteria = new OrderCriteria()
                {
                    SearchType = OrderSearchType.BySenderReceiver,
                    OrderID = mRecNo,
                    SenderCode = senderCode,
                    ReceiverCode = ReceiverCode
                };
                return _orderDA.OrderByCriteria(criteria);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrder OrderSaveRecord(DTOOrder mDTO)
        {          
            try
            {
                if (mDTO.OrderID == 0)
                {
                    _orderDA.OrderCreateRecord(mDTO);
                   
                    if (mDTO.OrderID > 0 && mDTO.SYSOrderStatusID != 101)
                    {
                        // Only create Inbound Message if the order is a regualr one.
                        if (mDTO.IsRegularOrder != null && mDTO.IsRegularOrder == true)
                        {
                        }

                        DTOMessageInbound mDTOMessageIB = new DTOMessageInbound()
                        {
                            CustomerID = mDTO.CustomerID,
                            DateSent = null,
                            MessageInboundType = "Order",
                            Error = false,
                            MessageinboundID = 0,
                            OrderID = mDTO.OrderID,
                            SentFlag = false,
                        };

                        MessageInboundSaveRecord(mDTOMessageIB);
                    }
                    
                }
                else
                {
                    _orderDA.OrderUpdateRecord(mDTO);
                }


                return mDTO;
            }
            catch (Exception ex)
            {
                _logService.LogSave("Order", string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace), mDTO.CreatedByUserID);
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public void OrderDeleteRecord(long mRecNo)
        {
            try
            {
                _orderDA.OrderDeleteRecord(new DTOOrder() { OrderID = mRecNo });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DTOOrder ListOrderByOrderNumberANDSalesOrgID(long OrderNumber, long SalesOrgID)
        {

            return _orderDA.ListOrderByOrderNumberANDSalesOrgID(OrderNumber,SalesOrgID);
        }
        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public Boolean OrderIsValid(DTOOrder mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }

            if (mDTO.CustomerID == 0)
            {
                mValidationResponse = "CustomerID cannnot be 0.";
                return false;
            }

            if (mDTO.SalesRepAccountID == 0)
            {
                mValidationResponse = "SalesRepAccountID cannnot be 0.";
                return false;
            }

            if (mDTO.ProviderID == 0)
            {
                mValidationResponse = "ProviderID cannnot be 0.";
                return false;
            }

            if (mDTO.ProviderWarehouseID == 0)
            {
                mValidationResponse = "ProviderWarehouseID cannnot be 0.";
                return false;
            }

            if (mDTO.OrderDate == null)
            {
                mValidationResponse = "OrderDate cannnot be null.";
                return false;
            }

            //if (mDTO.DeliveryDate == null)
            //{
            //    mValidationResponse = "DeliveryDate cannnot be null.";
            //    return false;
            //}

            if (mDTO.InvoiceDate == null)
            {
                mValidationResponse = "InvoiceDate cannnot be null.";
                return false;
            }

            if (mDTO.SYSOrderStatusID == 0)
            {
                mValidationResponse = "SYSOrderStatusID cannnot be 0.";
                return false;
            }

            if (mDTO.OrderNumber == null)
            {
                mValidationResponse = "OrderNumber cannnot be null.";
                return false;
            }

            if (mDTO.ReceivedDate == null)
            {
                mValidationResponse = "ReceivedDate cannnot be null.";
                return false;
            }

            //if (mDTO.ReleaseDate == null)
            //{
            //    mValidationResponse = "ReleaseDate cannnot be null.";
            //    return false;
            //}

            if (mDTO.DateCreated == null)
            {
                mValidationResponse = "DateCreated cannnot be null.";
                return false;
            }

            if (mDTO.DateUpdated == null)
            {
                mValidationResponse = "DateUpdated cannnot be null.";
                return false;
            }

            if (mDTO.CreatedByUserID == 0)
            {
                mValidationResponse = "CreatedByUserID cannnot be 0.";
                return false;
            }

            if (mDTO.UpdatedByUserID == 0)
            {
                mValidationResponse = "UpdatedByUserID cannnot be 0.";
                return false;
            }

            //if (mDTO.HoldUntilDate == null)
            //{
            //    mValidationResponse = "HoldUntilDate cannnot be null.";
            //    return false;
            //}

            if (mDTO.OrderGUID == null)
            {
                mValidationResponse = "OrderGUID cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListBySalesOrgID(int mRecNo)
        {
            return _orderDA.OrderListBySalesOrgID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListByCustomerID(int mRecNo)
        {
            return _orderDA.OrderListByCustomerID(mRecNo);
        }

        public DTOOrder OrderListByGUIDID(string mGUID)
        {
            return _orderDA.OrderListByGUIDID(mGUID);
        }


        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListBySalesRepAccountID(int mRecNo)
        {
            return _orderDA.OrderListBySalesRepAccountID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListByProviderID(int mRecNo)
        {
            return _orderDA.OrderListByProviderID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListByProviderWarehouseID(int mRecNo)
        {
            return _orderDA.OrderListByProviderWarehouseID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListBySYSOrderStatusID(int mRecNo)
        {
            return _orderDA.OrderListBySYSOrderStatusID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListByOrderGUID(int mRecNo)
        {
            return _orderDA.OrderListByOrderGUID(mRecNo);
        }

        public DTOOrderList OrderListByOrderStatus(bool isSent, bool isHeld)
        {
            OrderCriteria criteria = new OrderCriteria()
            {
                SearchType =  OrderSearchType.ByStatus,
                IsSent = isSent,
                IsHeld = isHeld,
            };
            return _orderDA.OrderListByCriteria(criteria);
        }

        public DTOOrderList OrderListBySalesOrgID_OfficeNewOrders(int mRecNo, long AccountID, int AccountType, int CurrentPage, int PageItemCount,
            long providerID, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, string GTINCode, int StateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo)
        {
            return _orderDA.OrderListBySalesOrgID_OfficeNewOrders(mRecNo, AccountID, AccountType, CurrentPage, PageItemCount,providerID, OrderNo, CustomerID, CreatedByUserID, DateFrom, DateTo, GTINCode, StateID, IsRegularOrder, ActualReleaseDateFrom, ActualReleaseDateTo);
        }

        public DTOOrderList OrderListBySalesOrgID_SalesRepNewOrders(int mRecNo, long AccountID, int AccountType, int CurrentPage, int PageItemCount, int OrgUnit,
            long providerID, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, string GTINCode, int StateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo)
        {
            return _orderDA.OrderListBySalesOrgID_SalesRepNewOrders(mRecNo, AccountID, AccountType, CurrentPage, PageItemCount, OrgUnit, providerID, OrderNo, CustomerID, CreatedByUserID, DateFrom, DateTo, GTINCode, StateID, IsRegularOrder, ActualReleaseDateFrom, ActualReleaseDateTo);
        }

        public DTOOrderList OrderListBySalesOrgID_AllNewOrders(int mRecNo, long AccountID, int AccountType, int CurrentPage, int PageItemCount,
            long providerID, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, string GTINCode, int StateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo)
        {
            return _orderDA.OrderListBySalesOrgID_AllNewOrders(mRecNo, AccountID, AccountType, CurrentPage, PageItemCount, providerID, OrderNo, CustomerID, CreatedByUserID, DateFrom, DateTo, GTINCode, StateID, IsRegularOrder, ActualReleaseDateFrom, ActualReleaseDateTo);
        }

        public DTOOrderList OrderListBySalesOrgID_OfficeSentOrders(int mRecNo, long providerID, long AccountID, int AccountType, int CurrentPage, int PageItemCount, long OrderNo, long CustomerID, long CreatedByUserID, DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int SYSStateID, bool? IsRegularOrder, DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo)
        {
            return _orderDA.OrderListBySalesOrgID_OfficeSentOrders(mRecNo, providerID, AccountID, AccountType, CurrentPage, PageItemCount, OrderNo, CustomerID, CreatedByUserID, DateFrom, DateTo, StatusID, GTINCode, SYSStateID, IsRegularOrder, ActualReleaseDateFrom, ActualReleaseDateTo);
        }

        public DTOOrderList OrderListBySalesOrgID_SalesRepSentOrders(int mRecNo, long providerID, long AccountID, int AccountType, int CurrentPage, int PageItemCount, int OrgUnit, long OrderNo, long CustomerID, long CreatedByUserID, DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int StateID, bool? IsRegularOrder, DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo)
        {
            return _orderDA.OrderListBySalesOrgID_SalesRepSentOrders(mRecNo, providerID, AccountID, AccountType, CurrentPage, PageItemCount, OrgUnit, OrderNo, CustomerID, CreatedByUserID, DateFrom, DateTo, StatusID, GTINCode,  StateID, IsRegularOrder, ActualReleaseDateFrom, ActualReleaseDateTo);
        }

        public DataTable OrderListBySalesOrgID_ExportOrders(int mRecNo, long providerID, long AccountID, int AccountType,
            int CurrentPage, int PageItemCount, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int SYSStateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo, string CreationMethod)
        {
            return _orderDA.OrderListBySalesOrgID_ExportOrders(mRecNo, providerID, AccountID, AccountType, CurrentPage,
                PageItemCount, OrderNo, CustomerID, CreatedByUserID, DateFrom, DateTo, StatusID, GTINCode, SYSStateID,
                IsRegularOrder, ActualReleaseDateFrom, ActualReleaseDateTo, CreationMethod);
        }


        public DTOOrderList OrderListBySalesOrgID_AllSentOrders(
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
            DateTime ActualReleaseDateTo)
        {
            return _orderDA.OrderListBySalesOrgID_AllSentOrders(mRecNo, providerID, AccountID, AccountType, CurrentPage, PageItemCount, OrgUnit, OrderNo, CustomerID, CreatedByUserID, DateFrom, DateTo, StatusID, GTINCode, StateID, IsRegularOrder, ActualReleaseDateFrom, ActualReleaseDateTo);
        }
        #endregion

        #region ************************tblOrderLine CRUDS ******************************************



        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLine OrderLineListByID(long providerID, long orderLineID)
        {
            return _orderDA.OrderLineListByID(providerID, orderLineID);
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLine OrderLineSaveRecord(DTOOrderLine mDTO)
        {
            try
            {
                if (mDTO.OrderLineID == 0)
                {
                    //_logService.LogSave("OrderLine", string.Format("Attempting to create orderline for OrderID {0}, ProductID {1}", mDTO.OrderID.ToString(), mDTO.ProductID.ToString()), 0);
                    _orderDA.OrderLineCreateRecord(mDTO);
                    //_logService.LogSave("OrderLine", string.Format("Orderline created for OrderID {0}, ProductID {1}", mDTO.OrderID.ToString(), mDTO.ProductID.ToString()), 0);
                }
                else
                {
                    //_logService.LogSave("OrderLine", string.Format("Attempting to update orderline for OrderID {0}, ProductID {1}", mDTO.OrderID.ToString(), mDTO.ProductID.ToString()), 0);
                    _orderDA.OrderLineUpdateRecord(mDTO);
                    //_logService.LogSave("OrderLine", string.Format("Orderline updated for OrderID {0}, ProductID {1}", mDTO.OrderID.ToString(), mDTO.ProductID.ToString()), 0);
                }

                return mDTO;
            }
            catch (Exception ex)
            {
                _logService.LogSave("OrderLine", string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public void OrderLineDeleteRecord(long orderLineID)
        {

            try
            {
                _orderDA.OrderLineDeleteRecord(new DTOOrderLine() { OrderLineID = orderLineID });


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public Boolean OrderLineIsValid(DTOOrderLine mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.OrderID == 0)
            {
                mValidationResponse = "OrderID cannnot be 0.";
                return false;
            }

            //if (mDTO.LineNum == 0)
            //{
            //    mValidationResponse = "LineNum cannnot be 0.";
            //    return false;
            //}

            if (mDTO.ProductID == 0)
            {
                mValidationResponse = "ProductID cannnot be 0.";
                return false;
            }

            if (mDTO.OrderQty == 0)
            {
                mValidationResponse = "OrderQty cannnot be 0.";
                return false;
            }

            if (string.IsNullOrEmpty(mDTO.UOM))
            {
                mValidationResponse = "UOM cannnot be null.";
                return false;
            }

            //if (mDTO.OrderPrice == 0)
            //{
            //    mValidationResponse = "OrderPrice cannnot be 0.";
            //    return false;
            //}

            //if (mDTO.DespatchPrice == 0)
            //{
            //    mValidationResponse = "DespatchPrice cannnot be 0.";
            //    return false;
            //}

            //if (mDTO.ItemStatus == null)
            //{
            //    mValidationResponse = "ItemStatus cannnot be null.";
            //    return false;
            //}

            //if (mDTO.ErrorText == null)
            //{
            //    mValidationResponse = "ErrorText cannnot be null.";
            //    return false;
            //}

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLineList OrderLineListByOrderID( long mRecNo)
        {
            return _orderDA.OrderLineListByOrderID( mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLineList OrderLineListByProductID(int mRecNo)
        {
            return _orderDA.OrderLineListByProductID(mRecNo);
        }

        #endregion


        #region ************************tblOrderSignature CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderSignatureList OrderSignatureList()
        {
            return _orderDA.OrderSignatureList();
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderSignature OrderSignatureListByID(int mRecNo)
        {
            return _orderDA.OrderSignatureListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderSignature OrderSignatureSaveRecord(DTOOrderSignature mDTO)
        {
            try
            {
                _orderDA.OrderSignatureCreateRecord(mDTO);


                return mDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public void OrderSignatureDeleteRecord(long orderID)
        {
            try
            {
                _orderDA.OrderSignatureDeleteRecord(new DTOOrderSignature() { OrderID = orderID });
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public Boolean OrderSignatureIsValid(DTOOrderSignature mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.OrderID == 0)
            {
                mValidationResponse = "OrderID cannnot be 0.";
                return false;
            }

            if (mDTO.Path == null)
            {
                mValidationResponse = "Path cannnot be null.";
                return false;
            }

            if (mDTO.DateCreated == null)
            {
                mValidationResponse = "DateCreated cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderSignature OrderSignatureListByOrderID(long mRecNo)
        {
            return _orderDA.OrderSignatureListByOrderID(mRecNo);
        }

        #endregion


        //#region ************************tblSYSOrderStatus CRUDS ******************************************

        ////CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOSYSOrderStatusList SYSOrderStatusList()
        {
            return _orderDA.SYSOrderStatusList();
        }

        ////CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        //public DTOSYSOrderStatus SYSOrderStatusListByID(int mRecNo)
        //{
        //    return _orderDA.SYSOrderStatusListByID(mRecNo);
        //}

        ////CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        //public DTOSYSOrderStatus SYSOrderStatusCreateRecord(DTOSYSOrderStatus mDTO)
        //{
        //    return _orderDA.SYSOrderStatusCreateRecord(mDTO);
        //}

        ////CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        //public DTOSYSOrderStatus SYSOrderStatusUpdateRecord(DTOSYSOrderStatus mDTO)
        //{
        //    return _orderDA.SYSOrderStatusUpdateRecord(mDTO);
        //}

        ////CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        //public DTOSYSOrderStatus SYSOrderStatusDeleteRecord(DTOSYSOrderStatus mDTO)
        //{
        //    return _orderDA.SYSOrderStatusDeleteRecord(mDTO);
        //}

        ////CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        //public Boolean SYSOrderStatusIsValid(DTOSYSOrderStatus mDTO, out string mValidationResponse)
        //{
        //    //please add your validation rules here. - Lazy Dog 

        //    if (mDTO.SYSOrderStatusCode == null)
        //    {
        //        mValidationResponse = "SYSOrderStatusCode cannnot be null.";
        //        return false;
        //    }

        //    if (mDTO.SYSOrderStatusText == null)
        //    {
        //        mValidationResponse = "SYSOrderStatusText cannnot be null.";
        //        return false;
        //    }

        //    mValidationResponse = "Ok";
        //    return true;
        //}

        //#endregion


        public DTOMessageInbound MessageInboundSaveRecord(DTOMessageInbound mDTO)
        {
            try
            {
                if (mDTO.MessageinboundID == 0)
                {
                    _orderDA.MessageInboundCreateRecord(mDTO);
                }
                else
                {
                    _orderDA.MessageInboundUpdateRecord(mDTO);
                }

                return mDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public DTOSYSOrderStatusList OrderStatusList()
        {
            return _orderDA.SYSOrderStatusList();
        }

        public DTOSYSOrderStatus OrderStatusListByID(int orderStatusID)
        {
            return _orderDA.SYSOrderStatusList(orderStatusID, string.Empty, string.Empty);
        }

        public DTOSYSOrderStatus OrderStatusListByText(string orderStatusText)
        {
            return _orderDA.SYSOrderStatusList(0, string.Empty, orderStatusText);
        }
        public DTOSYSOrderStatus OrderStatusListByCode(string orderStatusCode)
        {
            return _orderDA.SYSOrderStatusList(0, orderStatusCode, string.Empty);
        }

        public DTOMessageInboundList MessageInboundListBySentFlag(bool sentFlag, bool error)
        {
            return _orderDA.MessageInboundListBySentFlag(sentFlag, error);
        }

        public DTOOrderList OrderListToBeReleased(int mSalesOrgID, DateTime mDateCriteria, string mReleaseType)
        {
            return _orderDA.OrderListToBeReleased(mSalesOrgID, mDateCriteria.Date, mReleaseType);
        }

    } //end class

}
