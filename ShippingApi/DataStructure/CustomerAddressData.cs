using ClientSite.controllers;

namespace ShippingApi.DataStructure
{
    public class CustomerAddressData : BaseCustomer
    {

        private string _Custno;
        public string Custno
        {
            get { return _Custno; }
            set { _Custno = value; }
        }

        private int _Wppcstno;
        public int Wppcstno
        {
            get { return _Wppcstno; }
            set { _Wppcstno = value; }
        }

        private string _CShipNo;
        public string CShipNo
        {
            get { return _CShipNo; }
            set { _CShipNo = value; }
        }

        private string _Company;
        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }

        private string _Address1;
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }

        private string _Address2;
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }

        private string _City;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        private string _State;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        private string _Country;
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        private string _Shipvia;
        public string Shipvia
        {
            get { return _Shipvia; }
            set { _Shipvia = value; }
        }

        private string _Contact;
        public string Contact
        {
            get { return _Contact; }
            set { _Contact = value; }
        }

        private string _Zip;
        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }

        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private decimal _Tax;
        public decimal Tax
        {
            get { return _Tax; }
            set { _Tax = value; }
        }

        private int _Wppcshipno;
        public int Wppcshipno
        {
            get { return _Wppcshipno; }
            set { _Wppcshipno = value; }
        }

        private bool _DefaultAddress;
        public bool DefaultAddress
        {
            get { return _DefaultAddress; }
            set { _DefaultAddress = value; }
        }

        private bool _DropShip;
        public bool DropShip
        {
            get { return _DropShip; }
            set { _DropShip = value; }
        }

    }
}