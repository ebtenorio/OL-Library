using System;
using OrderLinc.DTOs;


namespace OrderLinc.IDataContracts
{
    public interface IAddressService
    {
        DTOAddress AddressDeleteRecord(DTOAddress mDTO);

        bool AddressIsValid(DTOAddress mDTO, out string mValidationResponse);

        DTOAddress AddressListByID(long mRecNo);

        DTOAddress AddressSaveRecord(DTOAddress mDTO);

        DTOAddressTypeList AddressTypeList();

        bool RegionIsValid(DTORegion mDTO, out string mValidationResponse);
        bool SYSStateIsValid(DTOSYSState mDTO, out string mValidationResponse);

        DTOSYSState SYSStateCreateRecord(DTOSYSState mDTO);
        DTOSYSState SYSStateDeleteRecord(DTOSYSState mDTO);
        DTOSYSState SYSStateListByID(int mRecNo);
        DTOSYSState SYSStateUpdateRecord(DTOSYSState mDTO);
        DTOSYSStateList SYSStateList();
        DTOSYSState SYSStateListByCustomerID(long mRecNo);


        DTORegion RegionCreateRecord(DTORegion mDTO);
        DTORegion RegionDeleteRecord(DTORegion mDTO);
        DTORegion RegionListByID(int mRecNo);
        DTORegion RegionUpdateRecord(DTORegion mDTO);
        DTORegionList RegionList();

    }
}
