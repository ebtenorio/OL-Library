using System;
using OrderLinc.DTOs;

namespace OrderLinc.IDataContracts
{
    public interface IAddressDA
    {
        DTOAddress AddressCreateRecord(DTOAddress mDTO);
        DTOAddress AddressDeleteRecord(DTOAddress mDTO);
     

        DTOAddress AddressListByID(long mRecNo);
        DTOAddressType AddressTypeCreateRecord(DTOAddressType mDTO);
        DTOAddressType AddressTypeDeleteRecord(DTOAddressType mDTO);
       
        DTOAddressTypeList AddressTypeList();
        DTOAddressType AddressTypeListByID(int mRecNo);
        DTOAddressType AddressTypeUpdateRecord(DTOAddressType mDTO);
        DTOAddress AddressUpdateRecord(DTOAddress mDTO);

        DTOSYSState SYSStateCreateRecord(DTOSYSState mDTO);
        DTOSYSState SYSStateDeleteRecord(DTOSYSState mDTO);

        DTOSYSState SYSStateListByID(int mRecNo);
        DTOSYSState SYSStateUpdateRecord(DTOSYSState mDTO);
        DTOSYSStateList SYSStateList();
        DTOSYSState SYSStateListByCustomerID(long mRecNo);


        DTORegion RegionCreateRecord(DTORegion mDTO);
        DTORegion RegionDeleteRecord(DTORegion mDTO);
        DTORegionList RegionList();
        DTORegion RegionListByID(int mRecNo);
        DTORegion RegionUpdateRecord(DTORegion mDTO);
    }
}
