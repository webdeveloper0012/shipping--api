using ShippingApi.DataStructure;
using System.Web;
using System.Xml;

namespace WPPDataModel.ShippingSystem.DataAccess
{
    public class ShipRateConfigAccess : ShipRateConfigData
    {
        //        //=====================================================================
        //        protected static ShipRateConfigData[] Get(string pSelectSql)
        //        {
        //            List<ShipRateConfigData> ShipRateConfigDataList = new List<ShipRateConfigData>();
        //            DataTable shipRateConfigTable = FillTable(pSelectSql);

        //            foreach (DataRow row in shipRateConfigTable.Rows)
        //            {
        //                ShipRateConfigDataList.Add(new ShipRateConfigData(row));
        //            }

        //            return ShipRateConfigDataList.ToArray();
        //        }

        //        //=====================================================================
        //        protected static ShipRateConfigData GetSingle(string pSelectSql)
        //        {
        //            //ToDo:Shakti Hardcode data for now

        //            //ShipRateConfigData[] ShipRateConfigData = Get(pSelectSql);

        //            //if (ShipRateConfigData != null)
        //            //{
        //            //    if (ShipRateConfigData.Length > 0)
        //            //    {
        //            //        if (ShipRateConfigData[0] != null)
        //            //        {
        //            //            return ShipRateConfigData[0];
        //            //        }
        //            //    }
        //            //}

        //            return null;
        //        }

        //        //=====================================================================
        //        public static ShipRateConfigData GetSettings()
        //        {
        //            string selectSQL = "select * from shiprateconfig";
        //            return GetSingle(selectSQL);
        //        }

        public static string UpsError(bool test =false)
        {
            if (test)
            {
                return @"<?xml version='1.0' encoding='utf-8'?>
  <RatingServiceSelectionResponse>  
    <Response>  
      <ResponseStatusCode>0</ResponseStatusCode>  
      <ResponseStatusDescription>UPS Not Online</ResponseStatusDescription>
          <Error>     
           <ErrorSeverity>Hard</ErrorSeverity>     
           <ErrorCode>990001</ErrorCode>     
           <ErrorDescription>This Program could not contact UPS Online</ErrorDescription>        
            </Error>        
          </Response>
        </RatingServiceSelectionResponse>";
                
            }
            var doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("~/Xml/UPSNotUp.xml"));
            return doc.DocumentElement.OuterXml;
        }
        public static string UpsAccessXml(bool test = false)
        {
            if (test)
            {
                return @"<?xml version='1.0' encoding='utf-8'?>
  <AccessRequest xml:lang='en-US'>    
      <AccessLicenseNumber>9D0CAB30B3B1B978</AccessLicenseNumber>       
         <UserId>Ultimate API</UserId>          
            <Password>Zu2NWsED8eB5J9crnH6U6r3C</Password>
          </AccessRequest>";
            }
                var doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("~/Xml/UPSAccessRequest.xml"));
            return doc.DocumentElement.OuterXml;
        }
        public static string UpsRatingXml(bool test = false)
        {
            if (test)
            {
                return @"<?xml version='1.0' encoding='utf-8'?>
<RatingServiceSelectionRequest>
  <Request>
	<RequestAction>Rate</RequestAction>
	<RequestOption>Shop</RequestOption>
	<TransactionReference>
	  <CustomerContext>UTEX Request for Rating and Service</CustomerContext>
	</TransactionReference>
  </Request>
  <PickupType>
	<Code>01</Code>
	<Description>Daily Pickup</Description>
  </PickupType>
  <Shipment>
	<Description>Rate Shopping - Domestic</Description>
	<Shipper>
	  <Name>Ultimate Textile</Name>
	  <ShipperNumber>688e50</ShipperNumber>
	  <Address>
		<AddressLine1>18 Market Street</AddressLine1>
		<AddressLine2 />
		<AddressLine3 />
		<City>Paterson</City>
		<StateProvinceCode>NJ</StateProvinceCode>
		<PostalCode>07501</PostalCode>
		<CountryCode>US</CountryCode>
	  </Address>
	</Shipper>
	<ShipTo>
	  <CompanyName>Recipient</CompanyName>
	  <PhoneNumber></PhoneNumber>
	  <Address>
		<AddressLine1></AddressLine1>
		<AddressLine2></AddressLine2>
		<AddressLine3 />
		<City></City>
		<StateProvinceCode></StateProvinceCode>
		<PostalCode></PostalCode>
		<CountryCode>US</CountryCode>
		<ResidentialAddressIndicator>Y</ResidentialAddressIndicator>
	  </Address>
	</ShipTo>
	<ShipFrom>
	  <CompanyName>Ultimate Textile</CompanyName>
	  <PhoneNumber>2093684031</PhoneNumber>
	  <FaxNumber>2093684136</FaxNumber>
	  <Address>
		<AddressLine1>18 Market Street</AddressLine1>
		<AddressLine2 />
		<AddressLine3 />
		<City>Paterson</City>
		<StateProvinceCode>NJ</StateProvinceCode>
		<PostalCode>07501</PostalCode>
		<CountryCode>US</CountryCode>
	  </Address>
	</ShipFrom>
	<Service>
	  <Code>03</Code>
	</Service>
	<Package>
	  <PackagingType>
		<Code>02</Code>
		<Description>Package</Description>
	  </PackagingType>
	  <Description>Rate</Description>
	  <Dimensions>
		<UnitOfMeasurement>
		  <Code>IN</Code>
		</UnitOfMeasurement>
		<Length>12.00</Length>
		<Width>12.00</Width>
		<Height>15.00</Height>
	  </Dimensions>
	  <PackageWeight>
		<UnitOfMeasurement>
		  <Code>LBS</Code>
		</UnitOfMeasurement>
		<Weight>10</Weight>
	  </PackageWeight>
	  <PackageServiceOptions>
		<InsuredValue>
		  <CurrencyCode>USD</CurrencyCode>
		  <MonetaryValue>50</MonetaryValue>
		</InsuredValue>
	  </PackageServiceOptions>
	</Package>
	<ShipmentServiceOptions></ShipmentServiceOptions>
	<RateInformation>
	  <NegotiatedRatesIndicator/>
	</RateInformation>
  </Shipment>
</RatingServiceSelectionRequest>";
            }
            var doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("~/Xml/UPSRatingServiceSelectionRequest.xml"));
            return doc.DocumentElement.OuterXml;
        }
    }

}
