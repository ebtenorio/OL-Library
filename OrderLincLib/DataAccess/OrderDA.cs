using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;

namespace OrderLinc.DataAccess
{
    public class OrderDA : IOrderDA
    {

        DatabaseService mDBService;

        public OrderDA(DatabaseService dbService)
        {

            mDBService = dbService;
        }

        #region ************************tblOrder CRUDS ******************************************

        private DTOOrder DataRowToDTOOrder(DataRow mRow)
        {
            DTOOrder mDTOOrder = new DTOOrder();
            mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
            mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
            mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
            mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
            mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
            mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());

            mDTOOrder.CustomerName = mRow["CustomerName"].ToString();
            mDTOOrder.CreatedByName = mRow["CreatedByName"].ToString();
            mDTOOrder.SYSOrderStatusText = mRow["SYSOrderStatusText"].ToString();
            mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
            mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
            mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
            mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
            mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
            mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
            try
            {
                mDTOOrder.ProviderName = mRow["ProviderName"].ToString();
            }
            catch { 
            
            }

            if (mRow["OrderDate"] != System.DBNull.Value)
            {
                mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
            }
            if (mRow["DeliveryDate"] != System.DBNull.Value)
            {
                mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
            }
            if (mRow["InvoiceDate"] != System.DBNull.Value)
            {
                mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
            }

            if (mRow["ReceivedDate"] != System.DBNull.Value)
            {
                mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
            }
            if (mRow["ReleaseDate"] != System.DBNull.Value)
            {
                mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
            }
       
            if (mRow["DateCreated"] != System.DBNull.Value)
            {
                mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
            }
            if (mRow["DateUpdated"] != System.DBNull.Value)
            {
                mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
            }
            mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
            mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
            if (mRow["HoldUntilDate"] != System.DBNull.Value)
            {
                mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
            }

            // Pre-sell and Future-Dated Orders

            if (mRow["IsRegularOrder"] != DBNull.Value)
            {
                mDTOOrder.IsRegularOrder = (bool)mRow["IsRegularOrder"];
            }
            else
            {
                mDTOOrder.IsRegularOrder = true;
            }

            if (mRow["RequestedReleaseDate"] != DBNull.Value)
            {
                mDTOOrder.RequestedReleaseDate = (DateTime)mRow["RequestedReleaseDate"]; //DateTime.ParseExact(mRow["RequestedReleaseDate"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                mDTOOrder.RequestedReleaseDate = null;
            }

            // This is an additional field for the PepsiCo Distributor Project
            if (mRow["PONumber"] != DBNull.Value)
            {
                mDTOOrder.PONumber = mRow["PONumber"].ToString();
            }
            else
            {
                mDTOOrder.PONumber = null;
            }


            return mDTOOrder;
        }


        public void OrderUpdateStatus(DTOOrder mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            mParams.Add(new DTODBParameter("@OrderID", mDTO.OrderID));
            mParams.Add(new DTODBParameter("@SYSOrderStatusID", mDTO.SYSOrderStatusID));
            mParams.Add(new DTODBParameter("@IsSent", mDTO.IsSent));
            mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_UPDATE_OrderStatus", mParams);
        }

        public DTOOrder OrderByCriteria(OrderCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = string.Empty;
            if (criteria.SearchType == OrderSearchType.ByOrderID)
            {
                mParams.Add(new DTODBParameter("@OrderID", criteria.OrderID));
                sp = "usp_tblOrder_ListByID";
            }
            else if (criteria.SearchType == OrderSearchType.BySenderReceiver)
            {
                mParams.Add(new DTODBParameter("@SenderCode", criteria.SenderCode));
                mParams.Add(new DTODBParameter("@ReceiverCode", criteria.ReceiverCode));
                mParams.Add(new DTODBParameter("@OrderID", criteria.OrderID));

                sp = "usp_tblOrder_ListBySenderReceiver";
            }

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);
            DTOOrder mDTOOrder = null;

            if (mDT.Rows.Count > 0)
                mDTOOrder = DataRowToDTOOrder(mDT.Rows[0]);

            mDT.Dispose();
            return mDTOOrder;
        }

        public DTOOrder ListOrderByOrderNumberANDSalesOrgID(long OrderNumber, long SalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = string.Empty;

            mParams.Add(new DTODBParameter("@OrderNumber", OrderNumber));
            mParams.Add(new DTODBParameter("@SalesOrgID", SalesOrgID));


                sp = "usp_tblOrder_ListByOrderNumberAndSalesOrg";
          


            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);
            DTOOrder mDTOOrder = null;

            if (mDT.Rows.Count > 0)
                mDTOOrder = DataRowToDTOOrder(mDT.Rows[0]);

            mDT.Dispose();
            return mDTOOrder;
        }


        public DTOOrderList OrderListToBeReleased(int mSalesOrgID, DateTime mDateCriteria, string mReleaseType)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = "usp_tblOrder_ListOrdersToBeReleased";

            mParams.Add(new DTODBParameter("@SalesOrgID", mSalesOrgID));
            mParams.Add(new DTODBParameter("@DateCriteria", mDateCriteria));
            mParams.Add(new DTODBParameter("@ReleaseType", mReleaseType));

            DTOOrderList orderList = new DTOOrderList();

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);

            foreach (DataRow dr in mDT.Rows)
            {
                orderList.Add(DataRowToDTOOrder(dr));
            }

            mDT.Dispose();
            return orderList;            
        }

        public DTOOrderList OrderListByCriteria(OrderCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = string.Empty;

            if (criteria.SearchType == OrderSearchType.ByStatus)
            {
                mParams.Add(new DTODBParameter("@IsSent", criteria.OrderID));
                mParams.Add(new DTODBParameter("@IsHeld", criteria.OrderID));

                sp = "usp_tblOrder_ListByOrderStatus";
            }

            DTOOrderList orderList = new DTOOrderList();

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);

            foreach (DataRow dr in mDT.Rows)
            {
                orderList.Add(DataRowToDTOOrder(dr));
            }

            mDT.Dispose();
            return orderList;
        }



        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrder OrderCreateRecord(DTOOrder mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblOrder");
            DataRow mRow = mDT.NewRow();
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["CustomerID"] = mDTO.CustomerID;
            mRow["SalesRepAccountID"] = mDTO.SalesRepAccountID;
            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProviderWarehouseID"] = mDTO.ProviderWarehouseID;
            mRow["OrderDate"] = mDTO.OrderDate;
            mRow["DeliveryDate"] = mDTO.DeliveryDate.ToDataRowValue();
            mRow["InvoiceDate"] = mDTO.InvoiceDate.ToDataRowValue();
            mRow["SYSOrderStatusID"] = mDTO.SYSOrderStatusID;
            mRow["OrderNumber"] = mDTO.OrderNumber;
            mRow["ReceivedDate"] = mDTO.ReceivedDate.ToDataRowValue();
            mRow["ReleaseDate"] = mDTO.ReleaseDate.ToDataRowValue();
            mRow["IsSent"] = mDTO.IsSent;
            mRow["IsHeld"] = mDTO.IsHeld;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
            mRow["HoldUntilDate"] = mDTO.HoldUntilDate.ToDataRowValue();
            mRow["IsMobile"] = mDTO.IsMobile;
            mRow["OrderGUID"] = mDTO.OrderGUID;
            mRow["PONumber"] = mDTO.PONumber;
            
            // Additional 2 fields for PepsiCo Pre-sell and Future-dated Orders
            mRow["IsRegularOrder"] = mDTO.IsRegularOrder;

            if (mDTO.RequestedReleaseDate == null)
            {
                mRow["RequestedReleaseDate"] = DBNull.Value;
            }
            else
            {
                mRow["RequestedReleaseDate"] = mDTO.RequestedReleaseDate;
            }

           
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblOrder_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.OrderID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrder OrderUpdateRecord(DTOOrder mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblOrder");
            DataRow mRow = mDT.NewRow();
            mRow["OrderID"] = mDTO.OrderID;
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["CustomerID"] = mDTO.CustomerID;
            mRow["SalesRepAccountID"] = mDTO.SalesRepAccountID;
            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProviderWarehouseID"] = mDTO.ProviderWarehouseID;
            mRow["OrderDate"] = mDTO.OrderDate;
            mRow["DeliveryDate"] = mDTO.DeliveryDate.ToDataRowValue();
            mRow["InvoiceDate"] = mDTO.InvoiceDate.ToDataRowValue();
            mRow["SYSOrderStatusID"] = mDTO.SYSOrderStatusID;
            mRow["OrderNumber"] = mDTO.OrderNumber;
            mRow["ReceivedDate"] = mDTO.ReceivedDate.ToDataRowValue();
            mRow["ReleaseDate"] = mDTO.ReleaseDate.ToDataRowValue();
            mRow["IsSent"] = mDTO.IsSent;
            mRow["IsHeld"] = mDTO.IsHeld;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
            mRow["HoldUntilDate"] = mDTO.HoldUntilDate.ToDataRowValue();
            mRow["IsMobile"] = mDTO.IsMobile;
            mRow["OrderGUID"] = mDTO.OrderGUID;

            mRow["IsRegularOrder"] = mDTO.IsRegularOrder;

            if (mDTO.RequestedReleaseDate == null)
            {
                mRow["RequestedReleaseDate"] = DBNull.Value;
            }
            else
            {
                mRow["RequestedReleaseDate"] = mDTO.RequestedReleaseDate;
            }

            // An Additional field for the PepsiCo Distributor Project
            if (mDTO.PONumber == null)
            {
                mRow["PONumber"] = DBNull.Value;
            }
            else
            {
                mRow["PONUmber"] = mDTO.PONumber;
            }

            
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblOrder_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrder OrderDeleteRecord(DTOOrder mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderID";
            mParam.ParameterValue = mDTO.OrderID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblOrder_DELETE", mParams);
            return mDTO;
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
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySalesOrgID", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrder mDTOOrder = new DTOOrder();
                mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
                mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
                if (mRow["OrderDate"] != System.DBNull.Value)
                {
                    mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
                }
                if (mRow["DeliveryDate"] != System.DBNull.Value)
                {
                    mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
                }
                if (mRow["InvoiceDate"] != System.DBNull.Value)
                {
                    mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
                }
                mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
                if (mRow["ReceivedDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
                }
                if (mRow["ReleaseDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
                }
                mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
                mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                
                if (mRow["HoldUntilDate"] != System.DBNull.Value)
                {
                    mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
                }

                // An Additional Field for PepsiCo Distributor Project
                if (mRow["PONumber"] != System.DBNull.Value)
                {
                    mDTOOrder.PONumber = mRow["PONumber"].ToString();
                }

                mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
                mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
                mDTOOrderList.Add(mDTOOrder);
            }

            return mDTOOrderList;
        }

        public DTOOrder OrderListByGUIDID(string mGUID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@GUID";
            mParam.ParameterValue = mGUID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_GUID", mParams);
          DTOOrder mDTOOrder = new DTOOrder();
            foreach (DataRow mRow in mDT.Rows)
            {
                
                mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
                mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
                if (mRow["OrderDate"] != System.DBNull.Value)
                {
                    mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
                }
                if (mRow["DeliveryDate"] != System.DBNull.Value)
                {
                    mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
                }
                if (mRow["InvoiceDate"] != System.DBNull.Value)
                {
                    mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
                }
                mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
                if (mRow["ReceivedDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
                }
                if (mRow["ReleaseDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
                }
                mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
                mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                if (mRow["HoldUntilDate"] != System.DBNull.Value)
                {
                    mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
                }
                mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
                mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
               
            }

            return mDTOOrder;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListByCustomerID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListByCustomerID", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();

            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrder mDTOOrder = new DTOOrder();
                mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
                mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());

                if (mRow["OrderDate"] != System.DBNull.Value)
                {
                    mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
                }

                if (mRow["DeliveryDate"] != System.DBNull.Value)
                {
                    mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
                }
                if (mRow["InvoiceDate"] != System.DBNull.Value)
                {
                    mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
                }
                mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
                if (mRow["ReceivedDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
                }
                if (mRow["ReleaseDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
                }
                mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
                mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                if (mRow["HoldUntilDate"] != System.DBNull.Value)
                {
                    mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
                }

                // This an additional field for the PepsiCo Distributor Project
                if (mRow["PONumber"] != System.DBNull.Value)
                {
                    mDTOOrder.PONumber = mRow["PONumber"].ToString();
                }

                mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
                mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
                mDTOOrderList.Add(mDTOOrder);
            }

            return mDTOOrderList;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListBySalesRepAccountID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesRepAccountID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySalesRepAccountID", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrder mDTOOrder = new DTOOrder();
                mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
                mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
                if (mRow["OrderDate"] != System.DBNull.Value)
                {
                    mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
                }
                if (mRow["DeliveryDate"] != System.DBNull.Value)
                {
                    mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
                }
                if (mRow["InvoiceDate"] != System.DBNull.Value)
                {
                    mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
                }
                mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
                if (mRow["ReceivedDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
                }
                if (mRow["ReleaseDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
                }
                mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
                mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());

                if (mRow["HoldUntilDate"] != System.DBNull.Value)
                {
                    mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
                }

                // An additional field for PepsiCo Distributor Project
                if (mRow["PONumber"] != System.DBNull.Value)
                {
                    mDTOOrder.PONumber = mRow["PONumber"].ToString();
                }

                mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
                mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
                mDTOOrderList.Add(mDTOOrder);
            }

            return mDTOOrderList;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListByProviderID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListByProviderID", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();

            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrder mDTOOrder = new DTOOrder();
                mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
                mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
                if (mRow["OrderDate"] != System.DBNull.Value)
                {
                    mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
                }
                if (mRow["DeliveryDate"] != System.DBNull.Value)
                {
                    mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
                }
                if (mRow["InvoiceDate"] != System.DBNull.Value)
                {
                    mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
                }
                mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
                if (mRow["ReceivedDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
                }
                if (mRow["ReleaseDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
                }
                mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
                mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());

                if (mRow["HoldUntilDate"] != System.DBNull.Value)
                {
                    mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
                }

                if (mRow["PONumber"] != System.DBNull.Value)
                {
                    mDTOOrder.PONumber = mRow["HoldUntilDate"].ToString();
                }

                mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
                mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
                mDTOOrderList.Add(mDTOOrder);
            }

            return mDTOOrderList;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListByProviderWarehouseID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderWarehouseID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListByProviderWarehouseID", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrder mDTOOrder = new DTOOrder();
                mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
                mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
                if (mRow["OrderDate"] != System.DBNull.Value)
                {
                    mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
                }
                if (mRow["DeliveryDate"] != System.DBNull.Value)
                {
                    mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
                }
                if (mRow["InvoiceDate"] != System.DBNull.Value)
                {
                    mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
                }
                mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
                if (mRow["ReceivedDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
                }
                if (mRow["ReleaseDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
                }
                mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
                mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                if (mRow["HoldUntilDate"] != System.DBNull.Value)
                {
                    mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
                }
                mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
                mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
                mDTOOrderList.Add(mDTOOrder);
            }

            return mDTOOrderList;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListBySYSOrderStatusID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSOrderStatusID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySYSOrderStatusID", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrder mDTOOrder = new DTOOrder();
                mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
                mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
                if (mRow["OrderDate"] != System.DBNull.Value)
                {
                    mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
                }
                if (mRow["DeliveryDate"] != System.DBNull.Value)
                {
                    mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
                }
                if (mRow["InvoiceDate"] != System.DBNull.Value)
                {
                    mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
                }
                mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
                if (mRow["ReceivedDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
                }
                if (mRow["ReleaseDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
                }
                mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
                mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());

                if (mRow["HoldUntilDate"] != System.DBNull.Value)
                {
                    mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
                }

                // An additional field for PepsiCo Distributor Project
                if (mRow["PONumber"] != System.DBNull.Value)
                {
                    mDTOOrder.PONumber = mRow["PONumber"].ToString();
                }

                mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
                mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
                mDTOOrderList.Add(mDTOOrder);
            }

            return mDTOOrderList;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListByCreatedByUserID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CreatedByUserID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListByCreatedByUserID", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrder mDTOOrder = new DTOOrder();
                mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
                mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
                if (mRow["OrderDate"] != System.DBNull.Value)
                {
                    mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
                }
                if (mRow["DeliveryDate"] != System.DBNull.Value)
                {
                    mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
                }
                if (mRow["InvoiceDate"] != System.DBNull.Value)
                {
                    mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
                }
                mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
                if (mRow["ReceivedDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
                }
                if (mRow["ReleaseDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
                }
                mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
                mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                if (mRow["HoldUntilDate"] != System.DBNull.Value)
                {
                    mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
                }
                mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
                mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
                mDTOOrderList.Add(mDTOOrder);
            }

            return mDTOOrderList;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderList OrderListByOrderGUID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderGUID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListByOrderGUID", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrder mDTOOrder = new DTOOrder();
                mDTOOrder.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrder.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrder.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOOrder.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
                mDTOOrder.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOOrder.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
                if (mRow["OrderDate"] != System.DBNull.Value)
                {
                    mDTOOrder.OrderDate = DateTime.Parse(mRow["OrderDate"].ToString());
                }
                if (mRow["DeliveryDate"] != System.DBNull.Value)
                {
                    mDTOOrder.DeliveryDate = DateTime.Parse(mRow["DeliveryDate"].ToString());
                }
                if (mRow["InvoiceDate"] != System.DBNull.Value)
                {
                    mDTOOrder.InvoiceDate = DateTime.Parse(mRow["InvoiceDate"].ToString());
                }
                mDTOOrder.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOOrder.OrderNumber = mRow["OrderNumber"].ToString();
                if (mRow["ReceivedDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReceivedDate = DateTime.Parse(mRow["ReceivedDate"].ToString());
                }
                if (mRow["ReleaseDate"] != System.DBNull.Value)
                {
                    mDTOOrder.ReleaseDate = DateTime.Parse(mRow["ReleaseDate"].ToString());
                }
                mDTOOrder.IsSent = Boolean.Parse(mRow["IsSent"].ToString());
                mDTOOrder.IsHeld = Boolean.Parse(mRow["IsHeld"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOOrder.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOOrder.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOOrder.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                if (mRow["HoldUntilDate"] != System.DBNull.Value)
                {
                    mDTOOrder.HoldUntilDate = DateTime.Parse(mRow["HoldUntilDate"].ToString());
                }
                mDTOOrder.IsMobile = Boolean.Parse(mRow["IsMobile"].ToString());
                mDTOOrder.OrderGUID = mRow["OrderGUID"].ToString();
                mDTOOrderList.Add(mDTOOrder);
            }

            return mDTOOrderList;
        }


        public DataTable OrderGetCount(OrderCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@RefID";
            mParam.ParameterValue = criteria.RefID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountTypeID";
            mParam.ParameterValue = criteria.AccountTypeID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = criteria.AccountID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrgUnitID";
            mParam.ParameterValue = criteria.OrgUnitID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_STAT_OrderCount", mParams);


            return mDT;
        }

        public DTOOrderList OrderListBySalesOrgID_OfficeNewOrders(
            int mRecNo, long AccountID, int AccountType, int CurrentPage, int PageItemCount,
            long providerID, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, string GTINCode, int StateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = AccountID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountType";
            mParam.ParameterValue = AccountType;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CurrentPage";
            mParam.ParameterValue = CurrentPage;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@PageItemCount";
            mParam.ParameterValue = PageItemCount;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderNo";
            if (OrderNo == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = OrderNo;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            if (CustomerID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CustomerID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CreatedByUserID";
            if (CreatedByUserID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CreatedByUserID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateFrom";
            mParam.ParameterValue = DateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateTo";
            mParam.ParameterValue = DateTo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@StatusID";

            // Additional Parameters

            mParam = new DTODBParameter();
            mParam.ParameterName = "@GTINCode";
            mParam.ParameterValue = GTINCode;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSStateID";
            mParam.ParameterValue = StateID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@IsRegularOrder";
            mParam.ParameterValue = IsRegularOrder;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateFrom";
            mParam.ParameterValue = ActualReleaseDateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateTo";
            mParam.ParameterValue = ActualReleaseDateTo;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySalesOrgID_OfficeNewOrders", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();

            // Begin: Additional
            int MaxRows = 0;
            int SearchRows = 0;
            // End: Additional

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
                MaxRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
                // mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalSearchRecords"))
                SearchRows = int.Parse(mDT.Rows[0]["TotalSearchRecords"].ToString());
            // mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (MaxRows != SearchRows)
            {
                mDTOOrderList.TotalRows = SearchRows;
            }
            else
            {
                mDTOOrderList.TotalRows = MaxRows;
            }

            foreach (DataRow mRow in mDT.Rows)//not required but...
            {

                mDTOOrderList.Add(DataRowToDTOOrder(mRow));
            }

            return mDTOOrderList;
        }

        public DTOOrderList OrderListBySalesOrgID_SalesRepNewOrders(int mRecNo, long AccountID, int AccountType, int CurrentPage, int PageItemCount, int OrgUnit,
            long providerID, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, string GTINCode, int StateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo
)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = AccountID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountType";
            mParam.ParameterValue = AccountType;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CurrentPage";
            mParam.ParameterValue = CurrentPage;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@PageItemCount";
            mParam.ParameterValue = PageItemCount;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrgUnit";
            mParam.ParameterValue = OrgUnit;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderNo";
            if (OrderNo == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = OrderNo;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            if (CustomerID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CustomerID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CreatedByUserID";
            if (CreatedByUserID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CreatedByUserID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateFrom";
            mParam.ParameterValue = DateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateTo";
            mParam.ParameterValue = DateTo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@StatusID";

            // Additional Parameters

            mParam = new DTODBParameter();
            mParam.ParameterName = "@GTINCode";
            mParam.ParameterValue = GTINCode;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSStateID";
            mParam.ParameterValue = StateID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@IsRegularOrder";
            mParam.ParameterValue = IsRegularOrder;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateFrom";
            mParam.ParameterValue = ActualReleaseDateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateTo";
            mParam.ParameterValue = ActualReleaseDateTo;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySalesOrgID_SalesRepNewOrders", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();

            // Begin: Addition
            int MaxRows = 0;
            int SearchRows = 0;
            // End: Addition


            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
                MaxRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
                //mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString()); // Original

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalSearchRecords"))
                SearchRows = int.Parse(mDT.Rows[0]["TotalSearchRecords"].ToString());
                // mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalSearchRecords"].ToString()); // Original

            // Begin: Addition
            if (MaxRows != SearchRows)
            {
                mDTOOrderList.TotalRows = SearchRows;
            }
            else
            {
                mDTOOrderList.TotalRows = MaxRows;
            }
            // End: Addition


            foreach (DataRow mRow in mDT.Rows)//not required but...
            {

                mDTOOrderList.Add(DataRowToDTOOrder(mRow));
            }

            return mDTOOrderList;
        }

        public DTOOrderList OrderListBySalesOrgID_AllNewOrders(int mRecNo, long AccountID, int AccountType, 
            int CurrentPage, int PageItemCount, long providerID, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, string GTINCode, int StateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = AccountID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountType";
            mParam.ParameterValue = AccountType;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CurrentPage";
            mParam.ParameterValue = CurrentPage;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@PageItemCount";
            mParam.ParameterValue = PageItemCount;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderNo";
            if (OrderNo == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = OrderNo;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            if (CustomerID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CustomerID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CreatedByUserID";
            if (CreatedByUserID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CreatedByUserID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateFrom";
            mParam.ParameterValue = DateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateTo";
            mParam.ParameterValue = DateTo;
            mParams.Add(mParam);

            // Additional Parameters

            mParam = new DTODBParameter();
            mParam.ParameterName = "@GTINCode";
            mParam.ParameterValue = GTINCode;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSStateID";
            mParam.ParameterValue = StateID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@IsRegularOrder";
            mParam.ParameterValue = IsRegularOrder;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateFrom";
            mParam.ParameterValue = ActualReleaseDateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateTo";
            mParam.ParameterValue = ActualReleaseDateTo;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySalesOrgID_AllNewOrders", mParams);

            DTOOrderList mDTOOrderList = new DTOOrderList();

            //if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
            //    mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            // Begin : Addition
            int MaxRows = 0;
            int SearchRows = 0;
            // End: Addition

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
                MaxRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
            // mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalSearchRecords"))
                SearchRows = int.Parse(mDT.Rows[0]["TotalSearchRecords"].ToString());
            // mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (MaxRows != SearchRows)
            {
                mDTOOrderList.TotalRows = SearchRows;
            }
            else
            {
                mDTOOrderList.TotalRows = MaxRows;
            }


            foreach (DataRow mRow in mDT.Rows)//not required but...
            {

                mDTOOrderList.Add(DataRowToDTOOrder(mRow));
            }

            return mDTOOrderList;
        }



        public DataTable OrderListBySalesOrgID_ExportOrders(int mRecNo, long providerID, long AccountID, int AccountType,
            int CurrentPage, int PageItemCount, long OrderNo, long CustomerID, long CreatedByUserID,
            DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int SYSStateID, bool? IsRegularOrder,
            DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo, string CreationMethod)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = AccountID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountType";
            mParam.ParameterValue = AccountType;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CurrentPage";
            mParam.ParameterValue = CurrentPage;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@PageItemCount";
            mParam.ParameterValue = PageItemCount;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderNo";
            if (OrderNo == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = OrderNo;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            if (CustomerID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CustomerID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CreatedByUserID";
            if (CreatedByUserID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CreatedByUserID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateFrom";
            mParam.ParameterValue = DateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateTo";
            mParam.ParameterValue = DateTo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@StatusID";

            if (StatusID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = StatusID;
            }
            mParams.Add(mParam);

            // Additional Parameters

            mParam = new DTODBParameter();
            mParam.ParameterName = "@GTINCode";
            mParam.ParameterValue = GTINCode;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSStateID";
            mParam.ParameterValue = SYSStateID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@IsRegularOrder";
            mParam.ParameterValue = IsRegularOrder;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateFrom";
            mParam.ParameterValue = ActualReleaseDateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateTo";
            mParam.ParameterValue = ActualReleaseDateTo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CreationMethod";
            mParam.ParameterValue = CreationMethod;
            mParams.Add(mParam);

            // =====================
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySalesOrgID_ExportOrders", mParams);
            return mDT;
        }

        // NOTE: Updated to receive GTINCode (string), StateID (int), OrderType (bool), ActualReleaseDateFrom (DateTime),  ActualReleaseDateTo (DateTime)
        public DTOOrderList OrderListBySalesOrgID_OfficeSentOrders(int mRecNo, long providerID, long AccountID, int AccountType, int CurrentPage, int PageItemCount, long OrderNo, long CustomerID, long CreatedByUserID, DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int StateID, bool? IsRegularOrder, DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = AccountID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountType";
            mParam.ParameterValue = AccountType;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CurrentPage";
            mParam.ParameterValue = CurrentPage;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@PageItemCount";
            mParam.ParameterValue = PageItemCount;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderNo";
            if (OrderNo == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = OrderNo;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            if (CustomerID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CustomerID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CreatedByUserID";
            if (CreatedByUserID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CreatedByUserID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateFrom";
            mParam.ParameterValue = DateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateTo";
            mParam.ParameterValue = DateTo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@StatusID";

            if (StatusID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = StatusID;
            }
            mParams.Add(mParam);

            // Additional Parameters

            mParam = new DTODBParameter();
            mParam.ParameterName = "@GTINCode";
            mParam.ParameterValue = GTINCode;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSStateID";
            mParam.ParameterValue = StateID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@IsRegularOrder";
            mParam.ParameterValue = IsRegularOrder;
            mParams.Add(mParam);
           
            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateFrom";
            mParam.ParameterValue = ActualReleaseDateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateTo";
            mParam.ParameterValue = ActualReleaseDateTo;
            mParams.Add(mParam);

            // =====================
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySalesOrgID_OfficeSentOrders", mParams);

            DTOOrderList mDTOOrderList = new DTOOrderList();

            // Begin : Addition
            int MaxRows = 0;
            int SearchRows = 0;
            // End: Addition

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
                MaxRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
                // mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalSearchRecords"))
                SearchRows = int.Parse(mDT.Rows[0]["TotalSearchRecords"].ToString());
                // mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (MaxRows != SearchRows)
            {
                mDTOOrderList.TotalRows = SearchRows;
            }
            else
            {
                mDTOOrderList.TotalRows = MaxRows;
            }

            foreach (DataRow mRow in mDT.Rows)//not required but...
            {

                mDTOOrderList.Add(DataRowToDTOOrder(mRow));
            }

            return mDTOOrderList;
        }

        public DTOOrderList OrderListBySalesOrgID_SalesRepSentOrders(int mRecNo, long providerID, long AccountID, int AccountType, int CurrentPage, int PageItemCount, int OrgUnit, long OrderNo, long CustomerID, long CreatedByUserID, DateTime DateFrom, DateTime DateTo, int StatusID, string GTINCode, int StateID, bool? IsRegularOrder, DateTime ActualReleaseDateFrom, DateTime ActualReleaseDateTo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = AccountID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountType";
            mParam.ParameterValue = AccountType;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CurrentPage";
            mParam.ParameterValue = CurrentPage;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@PageItemCount";
            mParam.ParameterValue = PageItemCount;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrgUnit";
            mParam.ParameterValue = OrgUnit;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderNo";
            if (OrderNo == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = OrderNo;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            if (CustomerID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CustomerID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CreatedByUserID";
            if (CreatedByUserID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CreatedByUserID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateFrom";
            mParam.ParameterValue = DateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateTo";
            mParam.ParameterValue = DateTo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@StatusID";
            if (StatusID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = StatusID;
            }
            mParams.Add(mParam);


            // Additional Parameters

            mParam = new DTODBParameter();
            mParam.ParameterName = "@GTINCode";
            mParam.ParameterValue = GTINCode;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSStateID";
            mParam.ParameterValue = StateID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@IsRegularOrder";
            mParam.ParameterValue = IsRegularOrder;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateFrom";
            mParam.ParameterValue = ActualReleaseDateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateTo";
            mParam.ParameterValue = ActualReleaseDateTo;
            mParams.Add(mParam);

            // =====================


            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySalesOrgID_SalesRepSentOrders", mParams);
            DTOOrderList mDTOOrderList = new DTOOrderList();


            //if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
            //    mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            // Begin: Addition
            int MaxRows = 0;
            int SearchRows = 0;
            // End: Addition

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
                MaxRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
                //mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalSearchRecords"))
                SearchRows = int.Parse(mDT.Rows[0]["TotalSearchRecords"].ToString());
            //mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (MaxRows != SearchRows)
            {
                mDTOOrderList.TotalRows = SearchRows;
            }
            else
            {
                mDTOOrderList.TotalRows = MaxRows;
            }

            foreach (DataRow mRow in mDT.Rows)//not required but...
            {

                mDTOOrderList.Add(DataRowToDTOOrder(mRow));
            }

            return mDTOOrderList;
        }

        // ORIGINAL : (int mRecNo, long providerID, long AccountID, int AccountType, int CurrentPage, int PageItemCount, int OrgUnit, long OrderNo, long CustomerID, long CreatedByUserID, DateTime DateFrom, DateTime DateTo, int StatusID)
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
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = AccountID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountType";
            mParam.ParameterValue = AccountType;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CurrentPage";
            mParam.ParameterValue = CurrentPage;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@PageItemCount";
            mParam.ParameterValue = PageItemCount;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrgUnit";
            mParam.ParameterValue = OrgUnit;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderNo";
            if (OrderNo == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = OrderNo;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            if (CustomerID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CustomerID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CreatedByUserID";
            if (CreatedByUserID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = CreatedByUserID;
            }
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateFrom";
            mParam.ParameterValue = DateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DateTo";
            mParam.ParameterValue = DateTo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@StatusID";
            if (StatusID == 0)
            {
                mParam.ParameterValue = "";
            }
            else
            {
                mParam.ParameterValue = StatusID;
            }
            mParams.Add(mParam);

            // Additional Parameters here
            mParam = new DTODBParameter();
            mParam.ParameterName = "@GTINCode";
            mParam.ParameterValue = GTINCode;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSStateID";
            mParam.ParameterValue = StateID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@IsRegularOrder";
            mParam.ParameterValue = IsRegularOrder;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateFrom";
            mParam.ParameterValue = ActualReleaseDateFrom;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ActualReleaseDateTo";
            mParam.ParameterValue = ActualReleaseDateTo;
            mParams.Add(mParam);

            DTOOrderList mDTOOrderList = new DTOOrderList();

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrder_ListBySalesOrgID_AllSentOrders", mParams);


            //if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
            //    mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            // Begin: Addition
            int MaxRows = 0;
            int SearchRows = 0;
            // End: Addition

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
                MaxRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
            //mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalSearchRecords"))
                SearchRows = int.Parse(mDT.Rows[0]["TotalSearchRecords"].ToString());
            //mDTOOrderList.TotalRows = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());

            if (MaxRows != SearchRows)
            {
                mDTOOrderList.TotalRows = SearchRows;
            }
            else
            {
                mDTOOrderList.TotalRows = MaxRows;
            }

            foreach (DataRow mRow in mDT.Rows)//not required but...
            {

                mDTOOrderList.Add(DataRowToDTOOrder(mRow));
            }

            return mDTOOrderList;
        }


        #endregion


        #region ************************tblOrderLine CRUDS ******************************************


        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLine DataRowToDTOOrderLine(DataRow mRow)
        {

            DTOOrderLine mDTOOrderLine = new DTOOrderLine();
            mDTOOrderLine.OrderLineID = Int64.Parse(mRow["OrderLineID"].ToString());
            mDTOOrderLine.OrderID = Int64.Parse(mRow["OrderID"].ToString());
            mDTOOrderLine.LineNum = int.Parse(mRow["LineNum"].ToString());
            mDTOOrderLine.ProductID = Int64.Parse(mRow["ProductID"].ToString());
            mDTOOrderLine.OrderQty = float.Parse(mRow["OrderQty"].ToString());
            mDTOOrderLine.DespatchQty = float.Parse(mRow["DespatchQty"].ToString());
            mDTOOrderLine.UOM = mRow["UOM"].ToString();
            mDTOOrderLine.OrderPrice = float.Parse(mRow["OrderPrice"].ToString());
            mDTOOrderLine.DespatchPrice = float.Parse(mRow["DespatchPrice"].ToString());
            mDTOOrderLine.ItemStatus = mRow["ItemStatus"].ToString();
            mDTOOrderLine.ErrorText = mRow["ErrorText"].ToString();
            mDTOOrderLine.ProductCode = mRow["ProviderProductCode"].ToString();
            mDTOOrderLine.GTINCode = mRow["GTINCode"].ToString();
            mDTOOrderLine.ProductName = mRow["ProductDescription"].ToString();
            mDTOOrderLine.PrimarySKU = mRow["PrimarySKU"].ToString();

            if (mRow["Discount"] == null || mRow["Discount"].ToString() == "")
            {
                mDTOOrderLine.Discount = 0.00f;
            }
            else
            {
                mDTOOrderLine.Discount = float.Parse(mRow["Discount"].ToString());
            }

            return mDTOOrderLine;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLine OrderLineListByID(long providerID, long orderLineID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderLineID";
            mParam.ParameterValue = orderLineID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrderLine_ListByID", mParams);
            DTOOrderLine mDTOOrderLine = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOOrderLine = DataRowToDTOOrderLine(mRow);
            }

            return mDTOOrderLine;
        }

        public DTOOrderLineList OrderLineListByOrderID( long orderID)
        {
            DTOOrderLineList mDTOOrderLine = new DTOOrderLineList();

            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderID";
            mParam.ParameterValue = orderID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrderLine_ListByOrderID", mParams);

            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOOrderLine.Add(DataRowToDTOOrderLine(mRow));
            }

            return mDTOOrderLine;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLine OrderLineCreateRecord(DTOOrderLine mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblOrderLine");
            DataRow mRow = mDT.NewRow();
           
            mRow["OrderID"] = mDTO.OrderID;
            mRow["LineNum"] = mDTO.LineNum;
            mRow["ProductID"] = mDTO.ProductID;
            mRow["OrderQty"] = mDTO.OrderQty;
            mRow["DespatchQty"] = mDTO.DespatchQty;
            mRow["UOM"] = mDTO.UOM;
            mRow["OrderPrice"] = mDTO.OrderPrice;
            mRow["DespatchPrice"] = mDTO.DespatchPrice;
            mRow["ItemStatus"] = mDTO.ItemStatus;
            mRow["ErrorText"] = mDTO.ErrorText;
            mRow["Discount"] = mDTO.Discount;
            mDT.Rows.Add(mRow);

            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderID";
            mParam.ParameterValue = mDTO.OrderID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@LineNum";
            mParam.ParameterValue = mDTO.LineNum;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductID";
            mParam.ParameterValue = mDTO.ProductID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderQty";
            mParam.ParameterValue = mDTO.OrderQty;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DespatchQty";
            mParam.ParameterValue = mDTO.DespatchQty;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@UOM";
            mParam.ParameterValue = mDTO.UOM;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderPrice";
            mParam.ParameterValue = mDTO.OrderPrice;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@DespatchPrice";
            mParam.ParameterValue = mDTO.DespatchPrice;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ItemStatus";
            mParam.ParameterValue = mDTO.ItemStatus;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ErrorText";
            mParam.ParameterValue = mDTO.ErrorText;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@Discount";
            mParam.ParameterValue = mDTO.Discount;
            mParams.Add(mParam);

            Object mRetval = mDBService.CreateRecord(mParams, "usp_tblOrderLine_INSERT");
            
            // Object mRetval = mDBService.CreateRecord(mDT, "usp_tblOrderLine_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.OrderLineID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLine OrderLineUpdateRecord(DTOOrderLine mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblOrderLine");
            DataRow mRow = mDT.NewRow();
            mRow["OrderLineID"] = mDTO.OrderLineID;
            mRow["OrderID"] = mDTO.OrderID;
            mRow["LineNum"] = mDTO.LineNum;
            mRow["ProductID"] = mDTO.ProductID;
            mRow["OrderQty"] = mDTO.OrderQty;
            mRow["DespatchQty"] = mDTO.DespatchQty;
            mRow["UOM"] = mDTO.UOM;
            mRow["OrderPrice"] = mDTO.OrderPrice;
            mRow["DespatchPrice"] = mDTO.DespatchPrice;
            mRow["ItemStatus"] = mDTO.ItemStatus;
            mRow["ErrorText"] = mDTO.ErrorText;
            mRow["Discount"] = mDTO.Discount;

            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblOrderLine_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLine OrderLineDeleteRecord(DTOOrderLine mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderLineID";
            mParam.ParameterValue = mDTO.OrderLineID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblOrderLine_DELETE", mParams);
            return mDTO;
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

            if (mDTO.LineNum == 0)
            {
                mValidationResponse = "LineNum cannnot be 0.";
                return false;
            }

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

            if (mDTO.DespatchQty == 0)
            {
                mValidationResponse = "DespatchQty cannnot be 0.";
                return false;
            }

            if (mDTO.UOM == null)
            {
                mValidationResponse = "UOM cannnot be null.";
                return false;
            }

            if (mDTO.OrderPrice == 0)
            {
                mValidationResponse = "OrderPrice cannnot be 0.";
                return false;
            }

            if (mDTO.DespatchPrice == 0)
            {
                mValidationResponse = "DespatchPrice cannnot be 0.";
                return false;
            }

            if (mDTO.ItemStatus == null)
            {
                mValidationResponse = "ItemStatus cannnot be null.";
                return false;
            }

            if (mDTO.ErrorText == null)
            {
                mValidationResponse = "ErrorText cannnot be null.";
                return false;
            }

            mValidationResponse = "Orderline is valid.";


            

            return true;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderLineList OrderLineListByProductID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrderLine_ListByProductID", mParams);
            DTOOrderLineList mDTOOrderLineList = new DTOOrderLineList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrderLine mDTOOrderLine = new DTOOrderLine();
                mDTOOrderLine.OrderLineID = Int64.Parse(mRow["OrderLineID"].ToString());
                mDTOOrderLine.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrderLine.LineNum = int.Parse(mRow["LineNum"].ToString());
                mDTOOrderLine.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOOrderLine.OrderQty = float.Parse(mRow["OrderQty"].ToString());
                mDTOOrderLine.DespatchQty = float.Parse(mRow["DespatchQty"].ToString());
                mDTOOrderLine.UOM = mRow["UOM"].ToString();
                mDTOOrderLine.OrderPrice = float.Parse(mRow["OrderPrice"].ToString());
                mDTOOrderLine.DespatchPrice = float.Parse(mRow["DespatchPrice"].ToString());
                mDTOOrderLine.ItemStatus = mRow["ItemStatus"].ToString();
                mDTOOrderLine.ErrorText = mRow["ErrorText"].ToString();
                mDTOOrderLineList.Add(mDTOOrderLine);
            }

            return mDTOOrderLineList;
        }

        #endregion


        #region ************************tblOrderSignature CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderSignatureList OrderSignatureList()
        {
            DTOOrderSignatureList mDTOOrderSignatureList = new DTOOrderSignatureList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrderSignature_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrderSignature mDTOOrderSignature = new DTOOrderSignature();
                mDTOOrderSignature.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrderSignature.Path = mRow["Path"].ToString();
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrderSignature.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                mDTOOrderSignatureList.Add(mDTOOrderSignature);
            }

            return mDTOOrderSignatureList;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderSignature OrderSignatureListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "OrderID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrderSignature_ListByID", mParams);
            DTOOrderSignature mDTOOrderSignature = new DTOOrderSignature();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOOrderSignature.OrderID = Int64.Parse(mRow["OrderID"].ToString());
                mDTOOrderSignature.Path = mRow["Path"].ToString();
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrderSignature.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
            }

            return mDTOOrderSignature;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderSignature OrderSignatureCreateRecord(DTOOrderSignature mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblOrderSignature"); // use GenerateUpdateTable , no autonumber
            DataRow mRow = mDT.NewRow();

            mRow["OrderID"] = mDTO.OrderID;
            mRow["Path"] = mDTO.Path;
            mRow["DateCreated"] = mDTO.DateCreated;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblOrderSignature_INSERT");
            mDTO.OrderID = long.Parse(mRetval.ToString());
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderSignature OrderSignatureUpdateRecord(DTOOrderSignature mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblOrderSignature");
            DataRow mRow = mDT.NewRow();
            mRow["OrderID"] = mDTO.OrderID;
            mRow["Path"] = mDTO.Path;
            mRow["DateCreated"] = mDTO.DateCreated;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblOrderSignature_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOOrderSignature OrderSignatureDeleteRecord(DTOOrderSignature mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "OrderID";
            mParam.ParameterValue = mDTO.OrderID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblOrderSignature_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
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
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@OrderID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrderSignature_ListByOrderID", mParams);
            DTOOrderSignature mDTOOrderSignature = null;
            if (mDT.Rows.Count > 0)
            {
                mDTOOrderSignature = new DTOOrderSignature();
                mDTOOrderSignature.OrderID = Int64.Parse(mDT.Rows[0]["OrderID"].ToString());
                mDTOOrderSignature.Path = mDT.Rows[0]["Path"].ToString();
                if (mDT.Rows[0]["DateCreated"] != System.DBNull.Value)
                {
                    mDTOOrderSignature.DateCreated = DateTime.Parse(mDT.Rows[0]["DateCreated"].ToString());
                }

            }
            return mDTOOrderSignature;
        }

        #endregion


        #region ************************tblSYSOrderStatus CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOSYSOrderStatusList SYSOrderStatusList()
        {
            DTOSYSOrderStatusList mDTOSYSOrderStatusList = new DTOSYSOrderStatusList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSOrderStatus_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOSYSOrderStatus mDTOSYSOrderStatus = new DTOSYSOrderStatus();
                mDTOSYSOrderStatus.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOSYSOrderStatus.SYSOrderStatusCode = mRow["SYSOrderStatusCode"].ToString();
                mDTOSYSOrderStatus.SYSOrderStatusText = mRow["SYSOrderStatusText"].ToString();
                mDTOSYSOrderStatusList.Add(mDTOSYSOrderStatus);
            }

            return mDTOSYSOrderStatusList;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOSYSOrderStatus SYSOrderStatusList(int mRecNo, string orderStatusCode, string orderStatusText)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSOrderStatusID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSOrderStatusCode";
            mParam.ParameterValue = orderStatusCode;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSOrderStatusText";
            mParam.ParameterValue = orderStatusText;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSOrderStatus_Select", mParams);
            DTOSYSOrderStatus mDTOSYSOrderStatus = new DTOSYSOrderStatus();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOSYSOrderStatus.SYSOrderStatusID = int.Parse(mRow["SYSOrderStatusID"].ToString());
                mDTOSYSOrderStatus.SYSOrderStatusCode = mRow["SYSOrderStatusCode"].ToString();
                mDTOSYSOrderStatus.SYSOrderStatusText = mRow["SYSOrderStatusText"].ToString();
            }

            return mDTOSYSOrderStatus;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOSYSOrderStatus SYSOrderStatusCreateRecord(DTOSYSOrderStatus mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblSYSOrderStatus");
            DataRow mRow = mDT.NewRow();
            mRow["SYSOrderStatusCode"] = mDTO.SYSOrderStatusCode;
            mRow["SYSOrderStatusText"] = mDTO.SYSOrderStatusText;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblSYSOrderStatus_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.SYSOrderStatusID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOSYSOrderStatus SYSOrderStatusUpdateRecord(DTOSYSOrderStatus mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblSYSOrderStatus");
            DataRow mRow = mDT.NewRow();
            mRow["SYSOrderStatusID"] = mDTO.SYSOrderStatusID;
            mRow["SYSOrderStatusCode"] = mDTO.SYSOrderStatusCode;
            mRow["SYSOrderStatusText"] = mDTO.SYSOrderStatusText;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblSYSOrderStatus_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public DTOSYSOrderStatus SYSOrderStatusDeleteRecord(DTOSYSOrderStatus mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSOrderStatusID";
            mParam.ParameterValue = mDTO.SYSOrderStatusID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblSYSOrderStatus_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:34:46 PM Lazy Dog 3.3.1.0
        public Boolean SYSOrderStatusIsValid(DTOSYSOrderStatus mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SYSOrderStatusCode == null)
            {
                mValidationResponse = "SYSOrderStatusCode cannnot be null.";
                return false;
            }

            if (mDTO.SYSOrderStatusText == null)
            {
                mValidationResponse = "SYSOrderStatusText cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        #endregion

        #region Message Inboud

        private DTOMessageInbound DataRowToDTOMessageInbound(DataRow mRow)
        {
            DTOMessageInbound mDTOMessageInbound = new DTOMessageInbound();
            mDTOMessageInbound.MessageinboundID = Int64.Parse(mRow["MessageinboundID"].ToString());
            mDTOMessageInbound.OrderID = Int64.Parse(mRow["OrderID"].ToString());
            mDTOMessageInbound.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
            mDTOMessageInbound.SentFlag = Boolean.Parse(mRow["SentFlag"].ToString());
            mDTOMessageInbound.Error = Boolean.Parse(mRow["Error"].ToString());
            mDTOMessageInbound.MessageInboundType = mRow["MessageInboundType"].ToString();
            if (mRow["DateSent"] != System.DBNull.Value)
            {
                mDTOMessageInbound.DateSent = DateTime.Parse(mRow["DateSent"].ToString());
            }
            return mDTOMessageInbound;
        }
        //CASE Generated Code 5/9/2014 1:30:33 PM Lazy Dog 3.3.1.0
        public DTOMessageInboundList MessageInboundList()
        {
            DTOMessageInboundList mDTOMessageInboundList = new DTOMessageInboundList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblMessageInbound_List");
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOMessageInboundList.Add(DataRowToDTOMessageInbound(mRow));
            }

            return mDTOMessageInboundList;
        }

        //CASE Generated Code 5/9/2014 1:30:33 PM Lazy Dog 3.3.1.0
        public DTOMessageInbound MessageInboundListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@MessageinboundID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblMessageInbound_ListByID", mParams);
            DTOMessageInbound mDTOMessageInbound = null;

            if (mDT.Rows.Count > 0)
                mDTOMessageInbound = DataRowToDTOMessageInbound(mDT.Rows[0]);

            mDT.Dispose();

            return mDTOMessageInbound;
        }

        public DTOMessageInboundList MessageInboundListBySentFlag(bool sentFlag, bool error)
        {
            DTOMessageInboundList mDTOMessageInboundList = new DTOMessageInboundList();

            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SentFlag";
            mParam.ParameterValue = sentFlag;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@Error";
            mParam.ParameterValue = error;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblMessageInbound_ListbySentFlag", mParams);
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOMessageInboundList.Add(DataRowToDTOMessageInbound(mRow));
            }

            return mDTOMessageInboundList;
        }


        //CASE Generated Code 5/9/2014 1:30:33 PM Lazy Dog 3.3.1.0
        public DTOMessageInbound MessageInboundCreateRecord(DTOMessageInbound mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblMessageInbound");
            DataRow mRow = mDT.NewRow();
            mRow["OrderID"] = mDTO.OrderID;
            mRow["CustomerID"] = mDTO.CustomerID;
            mRow["SentFlag"] = mDTO.SentFlag;
            mRow["Error"] = mDTO.Error;
            mRow["DateSent"] = mDTO.DateSent.ToDataRowValue();
            mRow["MessageInboundType"] = mDTO.MessageInboundType;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblMessageInbound_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.MessageinboundID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/9/2014 1:30:33 PM Lazy Dog 3.3.1.0
        public DTOMessageInbound MessageInboundUpdateRecord(DTOMessageInbound mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblMessageInbound");
            DataRow mRow = mDT.NewRow();
            mRow["MessageinboundID"] = mDTO.MessageinboundID;
            mRow["OrderID"] = mDTO.OrderID;
            mRow["CustomerID"] = mDTO.CustomerID;
            mRow["SentFlag"] = mDTO.SentFlag;
            mRow["Error"] = mDTO.Error;
            mRow["DateSent"] = mDTO.DateSent.ToDataRowValue();
            mRow["MessageInboundType"] = mDTO.MessageInboundType;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblMessageInbound_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/9/2014 1:30:34 PM Lazy Dog 3.3.1.0
        public DTOMessageInbound MessageInboundDeleteRecord(DTOMessageInbound mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@MessageinboundID";
            mParam.ParameterValue = mDTO.MessageinboundID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblMessageInbound_DELETE", mParams);
            return mDTO;
        }


        #endregion





    } //end class

}
