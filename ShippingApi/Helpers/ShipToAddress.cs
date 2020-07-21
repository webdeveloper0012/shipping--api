using ShippingApi.DataStructure;
using System.Collections.Generic;

namespace ClientSite.controllers
{
    public class ShipToAddress : CustomerAddressData
    {

        public const string NEWADDRESS_LISTVALUE_PREFIX = "!!";
        public const int BILLINGADDRESS_WPPCSHIPNO = -666;
        public const string USZIP_REGEX = @"\d{5}(-\d{4})?";
        public const string USZIP_FORMAT = "12345 or 12345-1234";
        public const string CANADAPOSTALCODE_REGEX = @"[A-Z]\d[A-Z] ?\d[A-Z]\d";
        public const string CANADAPOSTALCODE_FORMAT = "X9X 9X9";

        public ShipToAddress()
        {

        }
        public ShipToAddress(CustomerAddressData pAddressData)
        {
            Custno = pAddressData.Custno;
            Wppcstno = pAddressData.Wppcstno;
            CShipNo = pAddressData.CShipNo;
            Company = pAddressData.Company;
            Address1 = pAddressData.Address1;
            Address2 = pAddressData.Address2;
            City = pAddressData.City;
            State = pAddressData.State;
            Country = pAddressData.Country;
            Shipvia = pAddressData.Shipvia;
            Contact = pAddressData.Contact;
            Zip = pAddressData.Zip;
            Phone = pAddressData.Phone;
            Tax = pAddressData.Tax;
            Wppcshipno = pAddressData.Wppcshipno;
            DefaultAddress = pAddressData.DefaultAddress;
            DropShip = pAddressData.DropShip;
        }

        public static ShipToAddress[] CreateRange(CustomerAddressData[] pCustomerAddressData)
        {
            if (pCustomerAddressData == null)
            {
                return null;
            }

            List<ShipToAddress> addresses = new List<ShipToAddress>();
            for (int i = 0; i < pCustomerAddressData.Length; i++)
            {
                addresses.Add(new ShipToAddress(pCustomerAddressData[i]));
            }

            return addresses.ToArray();
        }

    }
}