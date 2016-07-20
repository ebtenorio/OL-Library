using System;
using System.Data;
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;
using OrderLinc;


public class ContactDA : IContactDA
{

    DatabaseService mDBService;

    public ContactDA(DatabaseService dbService)
    {
        mDBService = dbService;
    }


    #region ************************tblContact CRUDS ******************************************

    public DTOContact ContactListByID(long mRecNo)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@ContactID";
        mParam.ParameterValue = mRecNo;
        mParams.Add(mParam);
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblContact_ListByID", mParams);
        DTOContact mDTOContact = new DTOContact();
        foreach (DataRow mRow in mDT.Rows)//not required but...
        {
            mDTOContact.ContactID = int.Parse(mRow["ContactID"].ToString());
            mDTOContact.Phone = mRow["Phone"].ToString();
            mDTOContact.Fax = mRow["Fax"].ToString();
            mDTOContact.Mobile = mRow["Mobile"].ToString();
            mDTOContact.Email = mRow["Email"].ToString();
            mDTOContact.LastName = mRow["LastName"].ToString();
            mDTOContact.FirstName = mRow["FirstName"].ToString();

            if (mRow["OldID"] != System.DBNull.Value) {

                mDTOContact.OldID = Int64.Parse(mRow["OldID"].ToString());
            }

        }

        return mDTOContact;
    }

    public DTOContact ContactListByProviderID(long providerID)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@ProviderID";
        mParam.ParameterValue = providerID;
        mParams.Add(mParam);
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblContact_ListByProviderID", mParams);
        DTOContact mDTOContact = new DTOContact();
        foreach (DataRow mRow in mDT.Rows)//not required but...
        {
            mDTOContact.ContactID = int.Parse(mRow["ContactID"].ToString());
            mDTOContact.Phone = mRow["Phone"].ToString();
            mDTOContact.Fax = mRow["Fax"].ToString();
            mDTOContact.Mobile = mRow["Mobile"].ToString();
            mDTOContact.Email = mRow["Email"].ToString();
            mDTOContact.LastName = mRow["LastName"].ToString();
            mDTOContact.FirstName = mRow["FirstName"].ToString();
        }

        return mDTOContact;

    }

    public DTOContact ContactListBySalesOrgID(long salesOrgID)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@SalesOrgID";
        mParam.ParameterValue = salesOrgID;
        mParams.Add(mParam);
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblContact_ListBySalesOrgID", mParams);
        DTOContact mDTOContact = new DTOContact();
        foreach (DataRow mRow in mDT.Rows)//not required but...
        {
            mDTOContact.ContactID = int.Parse(mRow["ContactID"].ToString());
            mDTOContact.Phone = mRow["Phone"].ToString();
            mDTOContact.Fax = mRow["Fax"].ToString();
            mDTOContact.Mobile = mRow["Mobile"].ToString();
            mDTOContact.Email = mRow["Email"].ToString();
            mDTOContact.LastName = mRow["LastName"].ToString();
            mDTOContact.FirstName = mRow["FirstName"].ToString();
        }

        return mDTOContact;
    }

    public DTOContactList ContactListBySalesOrgID(long salesOrgID, AccountType accountTypeID)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@SalesOrgID";
        mParam.ParameterValue = salesOrgID;
        mParams.Add(mParam);

        mParam = new DTODBParameter();
        mParam.ParameterName = "@AccountTypeID";
        mParam.ParameterValue = (int)accountTypeID;
        mParams.Add(mParam);

        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblContact_ListByAccountTypeSalesOrgID", mParams);
        DTOContactList mDTOContactList = new DTOContactList();
        foreach (DataRow mRow in mDT.Rows)//not required but...
        {
            DTOContact mDTOContact = new DTOContact();
            mDTOContact.ContactID = int.Parse(mRow["ContactID"].ToString());
            mDTOContact.Phone = mRow["Phone"].ToString();
            mDTOContact.Fax = mRow["Fax"].ToString();
            mDTOContact.Mobile = mRow["Mobile"].ToString();
            mDTOContact.Email = mRow["Email"].ToString();
            mDTOContact.LastName = mRow["LastName"].ToString();
            mDTOContact.FirstName = mRow["FirstName"].ToString();
            mDTOContactList.Add(mDTOContact);
        }

        return mDTOContactList;
    }

    public DTOContact ContactListByAccountID(long accountID)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@AccountID";
        mParam.ParameterValue = accountID;
        mParams.Add(mParam);
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblContact_ListByAccountID", mParams);
        DTOContact mDTOContact = new DTOContact();
        foreach (DataRow mRow in mDT.Rows)//not required but...
        {
            mDTOContact.ContactID = int.Parse(mRow["ContactID"].ToString());
            mDTOContact.Phone = mRow["Phone"].ToString();
            mDTOContact.Fax = mRow["Fax"].ToString();
            mDTOContact.Mobile = mRow["Mobile"].ToString();
            mDTOContact.Email = mRow["Email"].ToString();
            mDTOContact.LastName = mRow["LastName"].ToString();
            mDTOContact.FirstName = mRow["FirstName"].ToString();
        }

        return mDTOContact;
    }

    public DTOContact ContactCreateRecord(DTOContact mDTO)
    {
        DataTable mDT = mDBService.GenerateCreateTable("tblContact");
        DataRow mRow = mDT.NewRow();

        mDT.Columns.Remove("OldID");
        mDT.AcceptChanges();

        mRow["Phone"] = mDTO.Phone;
        mRow["Fax"] = mDTO.Fax;
        mRow["Mobile"] = mDTO.Mobile;
        mRow["Email"] = mDTO.Email;
        mRow["LastName"] = mDTO.LastName;
        mRow["FirstName"] = mDTO.FirstName;
        mDT.Rows.Add(mRow);
        Object mRetval = mDBService.CreateRecord(mDT, "usp_tblContact_INSERT");
        int ObjectID = int.Parse(mRetval.ToString());
        mDTO.ContactID = ObjectID;
        return mDTO;
    }

    public DTOContact ContactUpdateRecord(DTOContact mDTO)
    {
        DataTable mDT = mDBService.GenerateUpdateTable("tblContact");
        DataRow mRow = mDT.NewRow();

        mDT.Columns.Remove("OldID");
        mDT.AcceptChanges();

        mRow["ContactID"] = mDTO.ContactID;
        mRow["Phone"] = mDTO.Phone;
        mRow["Fax"] = mDTO.Fax;
        mRow["Mobile"] = mDTO.Mobile;
        mRow["Email"] = mDTO.Email;
        mRow["LastName"] = mDTO.LastName;
        mRow["FirstName"] = mDTO.FirstName;
      
        mDT.Rows.Add(mRow);
        mDBService.UpdateRecord(mDT, "usp_tblContact_UPDATE");
        return mDTO;
    }

    public DTOContact ContactDeleteRecord(DTOContact mDTO)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@ContactID";
        mParam.ParameterValue = mDTO.ContactID;
        mParams.Add(mParam);
        mDBService.DeleteRecord("usp_tblContact_DELETE", mParams);
        return mDTO;
    }


    #endregion




} //end class
