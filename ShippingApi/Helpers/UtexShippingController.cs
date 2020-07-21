
//*-- ******************************************************************************************************
//*-- *********************** EXPLANATION OF RATES *********************************************************
//*-- ******************************************************************************************************
//*-- GROUND:
//*-- 	There are three Ground rates: GROUND, NEXT DAY, and 2ND DAY.
//*-- 	Here are the variables used to store GROUND rates and carriers:
//*-- 		UPS:
//*-- 			UPSGroundRate, 	stores UPS ground rate  		(Service Code = 03) UPS Ground
//*-- 			UPS2ndDayRate, 	stores UPS 2nd Day ground rate  (Service Code = 02) UPS 2nd Day
//*-- 			UPSNextDayRate, stores UPS Next Day ground rate (Service Code = 01) UPS Next Day
//*-- 			Premium1Rate, 	stores UPS Next Day ground rate (Service Code = ) UPS Preimum 1
//*-- 			Premium2Rate, 	stores UPS Next Day ground rate (Service Code = ) UPS Preimum 2
//*-- 			Premium3Rate, 	stores UPS Next Day ground rate (Service Code = ) UPS Preimum 3
//*-- 			
//*-- INTERNATIONAL:
//*-- 	There are three International rates: INTERNATIONAL GROUND, NEXT DAY, and 2ND DAY.
//*-- 		UPS:
//*-- 			UPSGroundRate, 	stores UPS ground rate..........(Service Code = 11) UPS Intl Standard 
//*-- 			UPS2ndDayRate, 	stores UPS 2nd Day ground rate..(Service Code = 08) UPS Intl Expedited 
//*-- 			UPSNextDayRate, stores UPS Next Day ground rate.(Service Code = 07) UPS Intl Express
//*-- 			Premium1Rate, 	stores UPS Next Day ground rate (Service Code = ) UPS Intl Preimum 1
//*-- 			Premium2Rate, 	stores UPS Next Day ground rate (Service Code = ) UPS Intl Preimum 2
//*-- 			Premium3Rate, 	stores UPS Next Day ground rate (Service Code = ) UPS Intl Preimum 3
//*--
//*-- ******************************************************************************************************
//*--
//*--  UPS Standard					11
//*--  UPS Ground					03
//*--  UPS 3 Day Select				12
//*--  UPS 2nd Day Air				02
//*--  UPS 2nd Day Air AM			59
//*--  UPS Next Day Air Saver		13
//*--  UPS Next Day Air				01
//*--  UPS Next Day Air Early A.M.	14
//*--  UPS Worldwide Express		07
//*--  UPS Worldwide Express Plus	54
//*--  UPS Worldwide Expedited		08
//*--  UPS World Wide Saver			65
//*--
//*-- ******************************************************************************************************
//*-- ************************************************ EXPLANATION OF RATES ********************************
//*-- ******************************************************************************************************
//*-- UTEX CREDENTIALS
//*-- UserId : Ultimate API
//*-- Ultimate API – pw Zu2NWsED8eB5J9crnH6U6r3C
//*-- Your Access Key is: 9D0CAB30B3B1B978
//*-- ******************************************************************************************************

using ShippingApi.DataStructure;
using ShippingApi.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml;
using WPPBusinessRules.ShippingSystem;

namespace ClientSite.controllers
{
    public class UTEXShippingController
    {
        private const string CONST_RENTAL = "RENTAL";
        private const string CONST_RISOP = "RISOP";
        private const string CONST_POP = "POP";
        private const string CONST_SALE = "SALE";

        private const string CONST_RENTAL_PREFIX = "REN";
        private const string CONST_RISOP_PREFIX = "RIS";
        private const string CONST_POP_PREFIX = "POP";
        private const string CONST_BYTHECASE_PREFIX = "BTC";

        private const string CONST_FATAL = "FATAL";
        private const string CONST_INFO = "INFO";

        //-----------------------------------------------------------------------------
        // Declare BOXITEM Class
        //-----------------------------------------------------------------------------
        public class BoxItem
        {
            public int boxId;
            public string itemId;
            public int itemQty;
            public decimal itemWeight;
            public decimal itemVolume;
            public decimal itemValue;
        }

        //-----------------------------------------------------------------------------
        // Declare SHIPPINGNOTE CLASS
        //-----------------------------------------------------------------------------
        public class ShippingNote
        {
            public string source;
            public string severity;
            public string message;
        }

        //-----------------------------------------------------------------------------
        // Declare SHIPPINGRESULT CLASS
        //-----------------------------------------------------------------------------
        public class ShippingResult
        {
            public string description;
            public decimal groundReturnRate;
            public decimal groundRate;
            public decimal twoDayRate;
            public decimal nextDayRate;
            public decimal premiumRate1;
            public decimal premiumRate2;
            public decimal premiumRate3;
            public decimal totalWeight;
            public decimal totalVolume;
            public decimal totalValue;
            public decimal totalInsValue;
            public UTEXShippingController.BoxItem[] boxedItemArray;
            public UTEXShippingController.ShippingNote[] shippingNotesArray;
            public ShippingBox[] shippedBoxArray;
            public decimal discountPct;

            public int StatusCode { get; set; }
            public string ErrorMessage { get; set; }
        }

        //-----------------------------------------------------------------------------
        // Configuration properties
        //-----------------------------------------------------------------------------
        private bool configRateUSA;
        private bool configRateCanada;
        private bool configRateInternational;

        private string configUpsErrorXml;
        private string configUpsAccessXml;
        private string configUpsRatingXml;
        private string configUpsRatingUrl;
        private string configUpsAVUrl;

        private decimal configRisopGroundPctMarkup;
        private decimal configRisopAirPctMarkup;
        private decimal configRentalGroundPctMarkup;
        private decimal configRentalAirPctMarkup;
        private decimal configSaleGroundPctMarkup;
        private decimal configSaleAirPctMarkup;

        private decimal configRisopDiscountPct;
        private decimal configRentalDiscountPct;
        private decimal configSaleDiscountPct;

        //-----------------------------------------------------------------------------
        // Ship To And Ship From Address properties
        //-----------------------------------------------------------------------------
        private string shipFromCompany = "Ultimate Textiles";
        private string shipFromAddr1 = "18 Market St";
        private string shipFromAddr2 = "";
        private string shipFromCity = "Paterson";
        private string shipFromState = "NJ";
        private string shipFromZip = "07501";
        private string shipFromCountry = "US";
        //private string	shipFromPhone	= "123-123-1234";

        private string shipToCompany;
        private string shipToAddr1;
        private string shipToAddr2;
        private string shipToCity;
        private string shipToState;
        private string shipToZip;
        private string shipToCountry;
        //private string	shipToPhone;

        //-------------------------------------------------------------------------------------
        // Create the Necessary Lists and arrays Needed
        //-------------------------------------------------------------------------------------
        private List<UTEXShippingController.BoxItem> itemsToBoxList = new List<UTEXShippingController.BoxItem>();
        private List<UTEXShippingController.BoxItem> boxedItemsList = new List<UTEXShippingController.BoxItem>();
        public List<UTEXShippingController.ShippingNote> shippingNoteList = new List<UTEXShippingController.ShippingNote>();
        private List<ShippingBox> shippingBoxList = new List<ShippingBox>();

        public UTEXShippingController.ShippingResult shippingResult = new UTEXShippingController.ShippingResult();

        //-----------------------------------------------------------------------------
        // References to cart , cart items and customer 
        //-----------------------------------------------------------------------------
        private BCart cart;
        private BCartItem[] cartItems;
        //private Customer	customer;

        public bool instantiatedOk = true;
        public bool ratingSuccess = false;

        private bool isInternational = false;
        private bool isCanada = false;
        private bool isDomestic = false;

        public string shipmentType = string.Empty;
        public int numberOfBoxes = 0;

        private decimal totalWeightBoxed = 0M;
        private decimal totalVolumeBoxed = 0M;

        //========================================================================================
        // FIRST call to this instantiates the shipping controller after preparing itself for
        // calculation of rates.
        //========================================================================================
        public void Setup(BCart pCart, ShipToAddress shipAddr, ShipRateConfigData shipRateConfigData)
        {
            //-------------------------------------------------------------------------------------
            // Assign parms to globals for this process.
            //-------------------------------------------------------------------------------------
            this.cart = pCart;
            this.cartItems = pCart.CartItems.OrderBy(z => z.ItemId).ThenBy(z => z.Color).ToArray();
            //this.customer	= CustomerController.Customer;

            //-------------------------------------------------------------------------------------
            // Set config variables based upon table values. Set Configuration routine will log issue
            // if necessary. If we fail, instantiation/configuration went poorly.
            //-------------------------------------------------------------------------------------
            if (!SetConfiguration(shipRateConfigData))
            {
                this.instantiatedOk = false;
                return;
            }

            //-------------------------------------------------------------------------------------
            // Get The Shipping Address whether its a predefined shipto address or typed in.
            //-------------------------------------------------------------------------------------
            //AddressController ac	= new AddressController();
            //ShipToAddress shipAddr	= ac.FromOrderShipto();

            if (shipAddr != null && !string.IsNullOrEmpty(shipAddr.Address1) &&
                                    !string.IsNullOrEmpty(shipAddr.Zip))
            {
                this.shipToCompany = shipAddr.Company;
                this.shipToAddr1 = shipAddr.Address1;
                this.shipToAddr2 = shipAddr.Address2;
                this.shipToCity = shipAddr.City;
                this.shipToState = shipAddr.State;
                this.shipToZip = shipAddr.Zip;
                this.shipToCountry = shipAddr.Country;
                LogShippingMessage("Configuration", "Retrieved and assigned valid Ship To Address and/or Zip Values.", CONST_INFO);
                LogShippingMessage("Configuration", "Rating For Shipment to ZIP: " + this.shipToZip, CONST_INFO);
            }
            else
            {
                LogShippingMessage("Configuration", "Unable to retrieve valid Ship To Address and/or Zip Values.", CONST_FATAL);
                this.instantiatedOk = false;
                return;
            }

            //-------------------------------------------------------------------------------------
            // Normalize the country if US or Canada with the 2 character symbol and if we have 
            // some other coutnry , attempt to determine its 2 character code.
            //-------------------------------------------------------------------------------------
            if ((shipToCountry.ToUpper() == "UNITED STATES") || (shipToCountry.ToUpper() == "USA"))
            {
                shipToCountry = "US";
                LogShippingMessage("Configuration", "Determined valid country code US for rating.", CONST_INFO);
            }
            else if (shipToCountry.ToUpper() == "CANADA" || shipToCountry.ToUpper() == "CAN")
            {
                shipToCountry = "CA";
                LogShippingMessage("Configuration", "Determined valid country code CA for rating.", CONST_INFO);
            }
            else
            {
                shipToCountry = Country.GetByCode2Char(shipToCountry).Code2Char;

                // Code is null or empty for ship to country so were not gonna be able to rate.
                if (string.IsNullOrEmpty(shipToCountry))
                {
                    LogShippingMessage("Configuration", "Unable to determine valid country code for rating", CONST_FATAL);
                    this.instantiatedOk = false;
                    return;
                }
                else
                {
                    LogShippingMessage("Configuration", "Determined valid country code " + shipToCountry + " for rating.", CONST_INFO);
                }
            }

            //-------------------------------------------------------------------------------------
            // Set up Booleans to identify some thangs
            //-------------------------------------------------------------------------------------
            isInternational = International();
            isCanada = Canada();
            isDomestic = DomesticUS();

            return;
        }

        //========================================================================================
        // SET Rating Object Configuration based on table properties
        //========================================================================================
        protected bool SetConfiguration(ShipRateConfigData shipRateConfiguration)
        {
            try
            {
                // Determine shiprate settings from table.
               // ShipRateConfigData shipRateConfiguration = ShipRateConfigAccess.GetSettings();

                // No results returned so get out
                if (shipRateConfiguration == null)
                {
                    return false;
                }

                configRateUSA = shipRateConfiguration.RateForUSA;
                configRateCanada = shipRateConfiguration.RateForCanada;
                configRateInternational = shipRateConfiguration.RateForIntnl;

                configUpsErrorXml = "UPSNotUp.xml";
                configUpsAccessXml = "UPSAccessRequest.xml";
                configUpsRatingXml = "UPSRatingServiceSelectionRequest.xml";

                configUpsRatingUrl = shipRateConfiguration.UpsUrl;
                configUpsAVUrl = shipRateConfiguration.UpsAVUrl;

                configRisopGroundPctMarkup = shipRateConfiguration.RisopGroundPctMarkup;
                configRisopAirPctMarkup = shipRateConfiguration.RisopAirPctMarkup;
                configRentalGroundPctMarkup = shipRateConfiguration.RentalGroundPctMarkup;
                configRentalAirPctMarkup = shipRateConfiguration.RentalAirPctMarkup;
                configSaleGroundPctMarkup = shipRateConfiguration.SaleGroundPctMarkup;
                configSaleAirPctMarkup = shipRateConfiguration.SaleAirPctMarkup;

                configRisopDiscountPct = shipRateConfiguration.RisopDiscountPct;
                configRentalDiscountPct = shipRateConfiguration.RentalDiscountPct;
                configSaleDiscountPct = shipRateConfiguration.SaleDiscountPct;


                LogShippingMessage("Set Configuration", "Successfully retrieved/assigned rating configuration properties", CONST_INFO);
                return true;
            }
            catch
            {
                LogShippingMessage("Set Configuration", "Unable to retrieve/assign rating configuration properties", CONST_FATAL);
                return false;
            }
        }

        //========================================================================================
        protected bool DomesticUS()
        {
            return (!Canada() && !International());
        }

        //========================================================================================
        protected bool Canada()
        {
            return (shipToCountry.ToUpper().IndexOf("CA") > -1);
        }

        //========================================================================================

        protected bool International()
        {
            return (shipToCountry.ToUpper().IndexOf("US") == -1 &&
                    shipToCountry.ToUpper().IndexOf("UNITED") == -1);
        }

        //========================================================================================
        // MAIN RATE DETERMINATION ROUTINE - THIS IS THE MAIN LINE
        //========================================================================================
        public UTEXShippingController.ShippingResult RateShipment(RateShipmentRequest RateShipmentRequest)// BCart cart, ShipRateConfigData shipRateConfigData, ShipToAddress shipToAddress, string ShippingLineItemId)
        {
            try
            {
                shippingResult.StatusCode = 200;
                Setup(RateShipmentRequest.Cart, RateShipmentRequest.ShipToAddress, RateShipmentRequest.ShipRateConfigData);
                //LogShippingMessage("RateShipment", "Freight Rating For WPPSONO: " +
                //                    Wppsono.ToString(), CONST_INFO);

                //---------------------------------------------------------------------------------------
                // If international and configured for international  OR
                // If canada and configured for canada OR
                // If not international or canada and rating for USA
                //---------------------------------------------------------------------------------------
                if ((International() && configRateInternational) || (Canada() && configRateCanada) ||
                    (DomesticUS() && configRateUSA))
                {
                    this.ratingSuccess = PrepareItemsToBox(RateShipmentRequest.ShippingLineItemId) &&
                                         BoxAllCaseItems() &&
                                         BoxAllLooseItems() &&
                                         PreRateHandling() &&
                                         RateForUps() &&
                                         (shipmentType == CONST_RENTAL ? RateForUps(true) : true) &&
                                         PostRateHandling() &&
                                         AdditionalHandling() &&
                                          ApplyDiscount() &&
                                         FinalizeShippingResults();
                }

                //--------------------------------------------------------------------------------------	
                //-- Send Autopsy Email For Debug Purposes.
                //--------------------------------------------------------------------------------------	
                //  SendShippingAutopsyEmail(Wppsono, customerNumber);

                //--------------------------------------------------------------------------------------	
                // Store the notes cursor to the shippingResult object in case errors have been logged
                // and perhaps FinalizeShippingResults never ran.
                //--------------------------------------------------------------------------------------	
                shippingResult.shippingNotesArray = shippingNoteList.ToArray();
               
            }
            catch (Exception ex)
            {
                shippingResult.StatusCode = 400;
                shippingResult.ErrorMessage = ex.Message;
            }
            return shippingResult;
        }

        //========================================================================================
        // PREPARE ITEMS TO BOX
        //========================================================================================
        protected bool PrepareItemsToBox(string ShippingLineItemId)
        {

            bool itemsPrepped = true;

            //------------------------------------------------------------------------------------
            // Loop thru the shopping cart. Each item will end up as a unique entry of qty 1 so things
            // can be boxed properly (granular shipping baby !). Creating Items To Box List.
            //------------------------------------------------------------------------------------
            foreach (BCartItem currentItem in cartItems)
            {
                //--------------------------------------------------------------------------------
                // Item Not Located ? What ?
                //--------------------------------------------------------------------------------
                if (currentItem == null)
                {
                    itemsPrepped = false;
                    LogShippingMessage("PrepareItemsToBox",
                                        "Unable to retrieve valid item record for " + currentItem.ItemId, CONST_FATAL);
                    break;
                }
                //--------------------------------------------------------------------------------
                // Ignore SHip line items
                //--------------------------------------------------------------------------------
                if (currentItem.ItemId == ShippingLineItemId)
                {
                    continue;
                }

                //--------------------------------------------------------------------------------
                // Locate the Curernt Items Inventory Record.
                //--------------------------------------------------------------------------------
                //InventoryItem currentItem = InventoryItem.GetById(cartItem.ItemId);

               

                //--------------------------------------------------------------------------------
                // If weight or volumme zero and we have POP , we'll look up the corresponding record
                // for BTC and use those weights.
                //--------------------------------------------------------------------------------
                //if ((currentItem.Weight == 0 || currentItem.Volume == 0) && cartItem.ItemId.Contains(CONST_POP_PREFIX))
                //{
                //    InventoryItem proxyItem = InventoryItem.GetById(cartItem.ItemId.Replace(CONST_POP_PREFIX, CONST_BYTHECASE_PREFIX));
                //    if (proxyItem != null)
                //    {
                //        currentItem.Weight = proxyItem.Weight;
                //        currentItem.Volume = proxyItem.Volume;
                //    }
                //}

                //--------------------------------------------------------------------------------
                // If the item has no valid weight or volume we get out and cause TBD
                //--------------------------------------------------------------------------------
                if (currentItem.Weight == 0 || currentItem.Volume == 0)
                {
                    itemsPrepped = false;
                    LogShippingMessage("PrepareItemsToBox",
                                        "Weight or volume of ZERO defined for item record for " + currentItem.ItemId, CONST_FATAL);
                    break;
                }

                //--------------------------------------------------------------------------------
                //-- Break each line item into units of 1 for packing
                //--------------------------------------------------------------------------------
                for (int ctr = 1; ctr <= currentItem.OrderQuantity; ctr++)
                {
                    BoxItem item2Add = new BoxItem();
                    item2Add.itemId = currentItem.ItemId;
                    item2Add.itemQty = 1;
                    item2Add.itemWeight = currentItem.Weight;
                    item2Add.itemVolume = currentItem.Volume;
                    item2Add.itemValue = currentItem.Price;
                    this.itemsToBoxList.Add(item2Add);
                }

                //---------------------------------------------------------------------------------------
                //-- Lets set the shipment type. Doesnt matter thats itse set each time since only items 
                //-- of a feather can ship together.
                //---------------------------------------------------------------------------------------
                if (currentItem.ItemId.Contains(CONST_RENTAL_PREFIX))
                {
                    this.shipmentType = CONST_RENTAL;
                }
                else if (currentItem.ItemId.Contains(CONST_RISOP_PREFIX))
                {
                    this.shipmentType = CONST_RISOP;
                }
                else if (currentItem.ItemId.Contains(CONST_POP_PREFIX) || currentItem.ItemId.Contains(CONST_BYTHECASE_PREFIX))
                {
                    this.shipmentType = CONST_POP;
                }
                else
                {
                    this.shipmentType = CONST_SALE;
                }

            }

            LogShippingMessage("RateShipment", "Order Type: " + this.shipmentType, CONST_INFO);

            //--------------------------------------------------------------------------------------
            //Set Shipping Result values that can be set now like total weight , volums and value
            //--------------------------------------------------------------------------------------
            if (itemsPrepped)
            {
                LogShippingMessage("PrepareItemsToBox", "Retrieved valid item records for all items.", CONST_INFO);

                this.shippingResult.totalWeight = this.itemsToBoxList.Sum(z => z.itemWeight);
                this.shippingResult.totalVolume = this.itemsToBoxList.Sum(z => z.itemVolume);
                this.shippingResult.totalValue = this.itemsToBoxList.Sum(z => z.itemValue);
                this.shippingResult.totalInsValue = this.itemsToBoxList.Sum(z => z.itemValue);


            }

            return itemsPrepped;
        }


        //========================================================================================
        // BOX ALL CASE ITEMS 
        //========================================================================================
        protected bool BoxAllCaseItems()
        {

            // Filter out all the case items...each one will get a box
            BoxItem[] caseItems = itemsToBoxList.Where(z => z.itemId.Contains("POP") || z.itemId.Contains("BTC")).ToArray();

            foreach (BoxItem itemToBox in caseItems)
            {
                bool caseItemBoxed = false;

                ShippingBox newBox = CreateShippingBox(shippingBoxList.Count + 1,
                                                        itemToBox.itemVolume,
                                                        itemToBox.itemWeight,
                                                        this.shipmentType);

                //-- Attempt to box the item
                if (newBox != null)
                {
                    shippingBoxList.Add(newBox);
                    caseItemBoxed = BoxTheItem(itemToBox, newBox);
                }
                else
                {
                    LogShippingMessage("BoxAllItems", "Unable to successfully create New Box for case", CONST_FATAL);
                    return false;
                }

                if (!caseItemBoxed)
                {
                    LogShippingMessage("BoxAllItems", "Unable to box 1 or more case items. Stopping.", CONST_FATAL);
                    return false;
                }

            }

            return true;
        }

        //========================================================================================
        // BOX ALL ITEMS 
        //========================================================================================
        protected bool BoxAllLooseItems()
        {

            // Filter out all the case items...each one will get a box
            BoxItem[] looseItems = itemsToBoxList.Where(z => !z.itemId.Contains("POP") && !z.itemId.Contains("BTC")).ToArray();

            // If we have no loose items to contend with , just return.
            if (looseItems.Length == 0)
            {
                return true;
            }

            //--------------------------------------------------------------------------------------
            // Create the first box based upon the total shipment volume reamining. This is the seed box 
            // that starts the process for loose items. We request the total shipment volume remaining 
            // since we are looking for the  smallest box that will suffice. Add the newly created box 
            // to the box list for use.
            //--------------------------------------------------------------------------------------
            ShippingBox firstBox = CreateShippingBox(shippingBoxList.Count + 1,
                                                      this.shippingResult.totalVolume - totalVolumeBoxed,
                                                      this.shippingResult.totalWeight - totalWeightBoxed,
                                                      this.shipmentType);



            if (firstBox != null)
            {
                LogShippingMessage("PrepareItemsToBox", "Successfully created Initial Box " + firstBox.BoxName, CONST_INFO);
                shippingBoxList.Add(firstBox);
            }
            else
            {
                LogShippingMessage("PrepareItemsToBox", "Unable to successfully create Initial Box", CONST_FATAL);
                return false;
            }


            //-----------------------------------------------------------------------------------
            // Now we gonna split out the order into its granular units fer' proper boxin'
            //-----------------------------------------------------------------------------------
            foreach (BoxItem itemToBox in looseItems)
            {
                bool itemBoxed = false;

                //-------------------------------------------------------------------------------
                // Lets see if any of the currently available boxes has volume and weight space for
                // this item. If we find a box, attempt to box it and get out of this inner loop
                //-------------------------------------------------------------------------------
                foreach (ShippingBox currentBox in shippingBoxList)
                {
                    if (itemToBox.itemVolume <= (currentBox.BoxMaxVolume - currentBox.BoxVolUsed) &&
                        itemToBox.itemWeight <= (currentBox.BoxMaxWeight - currentBox.BoxWeightUsed))
                    {
                        //-- Attempt to box the item
                        itemBoxed = BoxTheItem(itemToBox, currentBox);

                        //-----------------------------------------------------------------------
                        // THe item boxing failed when it should have worked. Message was set by
                        // BoxTheItem so lets just get out of the routine. Otherwise, the item 
                        // was packed successfully in the selected box and we need to break the
                        // inner loop for next item to box.
                        //-----------------------------------------------------------------------
                        if (!itemBoxed)
                        {
                            return false;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                //--------------------------------------------------------------------------------
                // If the item failed to get boxed above in the available open boxes ,we'll request 
                // a new one based on total shipment volume - shipment volume already boxed
                // Please notwe that if the item was supposed to go in a box but failed, we wont 
                // reach this point. This is only for a second chance with a new box.
                //--------------------------------------------------------------------------------
                if (!itemBoxed)
                {
                    ShippingBox newBox = CreateShippingBox(shippingBoxList.Count + 1,
                                                            this.shippingResult.totalVolume - totalVolumeBoxed,
                                                            this.shippingResult.totalWeight - totalWeightBoxed,
                                                            this.shipmentType);

                    if (newBox != null)
                    {
                        LogShippingMessage("BoxAllItems", "Successfully created New Box " + newBox.BoxName, CONST_INFO);
                        shippingBoxList.Add(newBox);
                        itemBoxed = BoxTheItem(itemToBox, newBox);
                    }
                    else
                    {
                        LogShippingMessage("BoxAllItems", "Unable to successfully create New Box", CONST_FATAL);
                        return false;
                    }
                }

                //--------------------------------------------------------------------------------
                //We have a problem Houston. Couldnt find a box and couldnt create a box.
                //--------------------------------------------------------------------------------
                if (!itemBoxed)
                {
                    LogShippingMessage("BoxAllItems", "Unable to box 1 or more items. Stopping.", CONST_FATAL);
                    return false;
                }

            }

            return true;
        }

        //========================================================================================
        // BOX THE ITEM 
        //========================================================================================
        protected bool BoxTheItem(BoxItem pItem2Box, ShippingBox pShippingBox)
        {

            bool itemBoxed = false;
            LogShippingMessage("BoxTheItem", "Boxing Item " + pItem2Box.itemId + " in box " + pShippingBox.BoxId.ToString(), CONST_INFO);

            //----------------------------------------------------------------------------------
            //Update the shippingbox item passed by reference
            //----------------------------------------------------------------------------------
            pShippingBox.BoxVolUsed += pItem2Box.itemVolume;
            pShippingBox.BoxWeightUsed += pItem2Box.itemWeight;
            pShippingBox.BoxValue += pItem2Box.itemValue;
            pShippingBox.BoxInsValue += pItem2Box.itemValue;

            //----------------------------------------------------------------------------------
            // Update the total weight and volume so we can track how much is left for next box
            //----------------------------------------------------------------------------------
            totalWeightBoxed += pItem2Box.itemWeight;
            totalVolumeBoxed += pItem2Box.itemVolume;

            //----------------------------------------------------------------------------------
            // If we are boxing a pop or btc item, they will take up the whole box. WE also dont 
            // want the box weight added in so we take it out here.
            //----------------------------------------------------------------------------------
            if (pItem2Box.itemId.ToUpper().Contains(CONST_POP_PREFIX) ||
                pItem2Box.itemId.ToUpper().Contains(CONST_BYTHECASE_PREFIX))
            {
                pShippingBox.BoxVolUsed = pShippingBox.BoxMaxVolume;
                pShippingBox.BoxWeightUsed = pShippingBox.BoxWeightUsed - pShippingBox.BoxWeight;   // Math.Min(pShippingBox.BoxMaxWeight, pShippingBox.BoxWeightUsed);
            }

            //----------------------------------------------------------------------------------
            // Create Local Array Of Items Already Boxed For THis Box. If the item already exists
            // in the box , were gonna bump the qty , otherwise we'll add a boxed item
            //----------------------------------------------------------------------------------
            BoxItem[] itemsAlreadyBoxedArray = boxedItemsList.Where(z => z.boxId == pShippingBox.BoxId).ToArray();

            //---------------------------------------------------------------------------------
            // Look thru all "BoxedItems" for this "ShippingBox", we may just bump the qty if 
            // its already here.
            //---------------------------------------------------------------------------------
            foreach (BoxItem itemAlreadyBoxed in itemsAlreadyBoxedArray)
            {
                if (pItem2Box.itemId == itemAlreadyBoxed.itemId)
                {
                    itemAlreadyBoxed.itemQty += 1;
                    itemBoxed = true;
                    break;
                }
            }

            //--------------------------------------------------------------------------------
            // If Not Boxed Above, the item does not yet exist in this box so lets add it.
            //--------------------------------------------------------------------------------
            if (!itemBoxed)
            {
                BoxItem newBoxedItem = new BoxItem();
                newBoxedItem.boxId = pShippingBox.BoxId;
                newBoxedItem.itemId = pItem2Box.itemId;
                newBoxedItem.itemQty = 1;
                newBoxedItem.itemWeight = pItem2Box.itemWeight;
                newBoxedItem.itemVolume = pItem2Box.itemVolume;
                newBoxedItem.itemValue = pItem2Box.itemValue;
                this.boxedItemsList.Add(newBoxedItem);
                itemBoxed = true;
            }

            return itemBoxed;
        }

        //========================================================================================
        // CREATE SHIPPING BOX
        //========================================================================================
        protected ShippingBox CreateShippingBox(int pBoxId, decimal pTargetVolume, decimal pTargetWeight, string pShipType)
        {

            LogShippingMessage("CreateShippingBox", "Creating Box " + pBoxId.ToString() +
                                                    " with target volume of  " + pTargetVolume.ToString() +
                                                    " for shipment type " + pShipType, CONST_INFO);

            //--------------------------------------------------------------------------------------
            // Grab all existing Box options for this type of transaction
            //--------------------------------------------------------------------------------------
            ShippingBox[] boxOptions = ShippingBox.GetAllShippingBoxes(pShipType);

            if (boxOptions == null || boxOptions.Count() == 0)
            {
                LogShippingMessage("CreateShippingBox", "Unable to locate any shipping box options.", CONST_FATAL);
                return null;
            }

            //--------------------------------------------------------------------------------------
            // Sort the box options from smallest to largest by volume and then weight. We want to find
            // the smallest box available for the needed volume
            //--------------------------------------------------------------------------------------
            boxOptions = boxOptions.OrderBy(z => z.BoxMaxVolume).ToArray();

            //--------------------------------------------------------------------------------------
            // Cruuse thru the options looking for the smallest box to accommodate our necessary volume
            // If the target volume is bigger than any box interrogated, we'll default to largest box
            // next to box what we can.
            //--------------------------------------------------------------------------------------
            foreach (ShippingBox boxOption in boxOptions)
            {
                if (boxOption.BoxMaxVolume >= pTargetVolume && boxOption.BoxMaxWeight >= pTargetWeight)
                {
                    // Update the BoxId, set the weight used = weight of empty box
                    boxOption.BoxId = pBoxId;
                    boxOption.BoxWeightUsed += boxOption.BoxWeight;

                    //--------------------------------------------------------------------------------------
                    // If we are dealing with a rental then we have to pre-account for the possability
                    // of a duffel bag. Adding 1.6 for under 20 but under 10 really shouldnt have an extra 
                    // pound added. We'll remove it later if the total box weight turns out less than 10
                    //--------------------------------------------------------------------------------------
                    if (this.shipmentType == CONST_RENTAL)
                    {
                        if (boxOption.BoxMaxWeight < 20)
                        {
                            boxOption.BoxWeightUsed += 1.6M;
                        }
                        else if (boxOption.BoxMaxWeight < 35)
                        {
                            boxOption.BoxWeightUsed += 1.6M;
                        }
                        else
                        {
                            boxOption.BoxWeightUsed += 2M;
                        }

                    }

                    return boxOption;
                }
            }

            //--------------------------------------------------------------------------------------
            // If we didnt find a suitable box above, we have too much for a single box so 
            // lets choose the largest available to start with .
            //--------------------------------------------------------------------------------------
            ShippingBox boxOptionDefault = boxOptions[boxOptions.Length - 1];
            boxOptionDefault.BoxId = pBoxId;
            boxOptionDefault.BoxWeightUsed += boxOptionDefault.BoxWeight;

            // Largest Box for a rental will need 2 pound duffel bag,
            if (this.shipmentType == CONST_RENTAL)
            {
                boxOptionDefault.BoxWeightUsed += 2M;
            }

            return boxOptionDefault;
        }

        //========================================================================================
        // PRE RATE VALIDATE  - Lets make sure we have some boxes to rate.
        //========================================================================================
        protected bool PreRateHandling()
        {

            //----------------------------------------------------------------------------------
            // Determine the number of boxes created before validation begins
            //----------------------------------------------------------------------------------
            numberOfBoxes = shippingBoxList.Count();

            //----------------------------------------------------------------------------------
            // If boxes > 0 and < the Max boxes defined
            //----------------------------------------------------------------------------------
            if (numberOfBoxes < 1)
            {
                LogShippingMessage("PreRateHandling", "No Boxes To Ship ??", CONST_FATAL);
                return false;
            }

            // Log the total number of boxes we have assembled
            LogShippingMessage("PreRateHandling", numberOfBoxes.ToString() + " box(s) packed and ready to rate", CONST_INFO);

            //----------------------------------------------------------------------------------
            // If we have a rental , we want to increase the weight of each box by its potential duffle bag included.
            // The duffel bag weight was already included during the boxing routine as is the box weight itself which 
            // is why its commented out below. Note that the only calculation still done here is to remove the 1 pound
            // added if the box ends up holding less than 10 pounds.
            //----------------------------------------------------------------------------------
            if (this.shipmentType == CONST_RENTAL)
            {
                foreach (ShippingBox shippingBox in shippingBoxList)
                {
                    if (shippingBox.BoxWeightUsed < 10M)
                    {
                        LogShippingMessage("PreRateHandling", "Box # " + shippingBox.BoxId.ToString() + " For Rental Order Gets No Duffle - Too Light", CONST_INFO);
                        shippingBox.BoxWeightUsed -= 1.6M;
                    }
                    else if (shippingBox.BoxWeightUsed < 35)
                    {
                        LogShippingMessage("PreRateHandling", "Box # " + shippingBox.BoxId.ToString() + " For Rental Order Gets Small 1.6 lb Duffle", CONST_INFO);
                    }
                    else
                    {
                        LogShippingMessage("PreRateHandling", "Box # " + shippingBox.BoxId.ToString() + " For Rental Order Gets Large 2.0 lb Duffle ", CONST_INFO);
                    }

                }
            }

            //----------------------------------------------------------------------------------
            // Rental and Risop orders should have declared value at 100 and no insured value.
            //----------------------------------------------------------------------------------
            if (this.shipmentType == CONST_RENTAL || this.shipmentType == CONST_RISOP || true)
            {
                LogShippingMessage("PreRateHandling", "Setting Declared Value of Boxes to Zero For Rental/Risop/Sale/Pop Order", CONST_INFO);
                foreach (ShippingBox shippingBox in shippingBoxList)
                {
                    shippingBox.BoxValue = 0;
                    shippingBox.BoxInsValue = 0;
                }
            }

            // Show stats for each box prior to rating
            foreach (ShippingBox shippingBox in shippingBoxList)
            {
                LogShippingMessage("PreRateHandling",
                                    string.Format("Box {0} of type {1} has total volume of {2} and total weight of {3} and total value of ${4}",
                                    shippingBox.BoxId,
                                    shippingBox.BoxType,
                                    shippingBox.BoxVolUsed,
                                    shippingBox.BoxWeightUsed,
                                    shippingBox.BoxValue),
                                    CONST_INFO);

            }

            return true;
        }


        //========================================================================================
        // RATE FOR UPS
        //
        // pIsRentalReturn set to true causes to and from address swap and then rates and captures
        // only the ground rate needed for return.
        //========================================================================================
        protected bool RateForUps(bool pIsRentalReturn = false)
        {

            if (pIsRentalReturn)
            {
                LogShippingMessage("RateForUps", "Starting RateForUps For Return Shipment ", CONST_INFO);
            }
            else
            {
                LogShippingMessage("RateForUps", "Starting RateForUps For Shipment ", CONST_INFO);
            }

            XmlDocument objRateRequest;
            XmlNode objPackageTemplate;
            XmlNode objTemplateClone;
            XmlNodeList objNodeList;
            XmlDocument objRateReply;
            int nodeCount;

            //-----------------------------------------------------------------------------------
            // Load the rate request template	
            // Preserve First Package as template for later additions of packages
            // Get the list of the templates' "Package" Nodes for removal
            //-----------------------------------------------------------------------------------
            objRateRequest = CreateRequestXML();
            objPackageTemplate = objRateRequest.SelectSingleNode("//Package").CloneNode(true);
            objNodeList = objRateRequest.SelectNodes("//Package");

            //-----------------------------------------------------------------------------------
            // Remove a "Package" Nodes...we are gonna add real ones with template we copied
            //-----------------------------------------------------------------------------------
            for (nodeCount = 0; nodeCount <= objNodeList.Count - 1; nodeCount++)
            {
                XmlNode loopNode = objNodeList.Item(nodeCount);
                loopNode.SelectSingleNode("..").RemoveChild(loopNode);
            }

            //-----------------------------------------------------------------------------------
            //* Load ShipTo Address 
            //* Note that we swap the ship to and ship from for rental returns
            //-----------------------------------------------------------------------------------
            XmlNode shiptoNode = objRateRequest.SelectSingleNode("//ShipTo");
            shiptoNode.SelectSingleNode("CompanyName").InnerText = (pIsRentalReturn ? shipFromCompany : shipToCompany);

            shiptoNode = objRateRequest.SelectSingleNode("//ShipTo/Address");
            shiptoNode.SelectSingleNode("AddressLine1").InnerText = (pIsRentalReturn ? shipFromAddr1 : shipToAddr1);
            shiptoNode.SelectSingleNode("AddressLine2").InnerText = (pIsRentalReturn ? shipFromAddr2 : shipToAddr2);
            shiptoNode.SelectSingleNode("AddressLine3").InnerText = "";
            shiptoNode.SelectSingleNode("City").InnerText = (pIsRentalReturn ? shipFromCity : shipToCity);

            //-----------------------------------------------------------------------------------
            // If this is an International shipment, do not specify a State or Province.
            //-----------------------------------------------------------------------------------
            if (isCanada)
            {
                shiptoNode.SelectSingleNode("StateProvinceCode").InnerText = (pIsRentalReturn ? shipFromState : shipToState);
            }
            else if (International())
            {
                shiptoNode.SelectSingleNode("StateProvinceCode").InnerText = "";
            }
            else
            {
                shiptoNode.SelectSingleNode("StateProvinceCode").InnerText = (pIsRentalReturn ? shipFromState : shipToState);
            }
            shiptoNode.SelectSingleNode("PostalCode").InnerText = (pIsRentalReturn ? shipFromZip : shipToZip);
            shiptoNode.SelectSingleNode("CountryCode").InnerText = (pIsRentalReturn ? shipFromCountry : shipToCountry);

            //-----------------------------------------------------------------------------------
            //* Load ShipFrom Address
            //* Note that we swap the ship to and ship from for rental returns
            //-----------------------------------------------------------------------------------
            XmlNode shipFromNode = objRateRequest.SelectSingleNode("//ShipFrom");
            shipFromNode.SelectSingleNode("CompanyName").InnerText = (!pIsRentalReturn ? shipFromCompany : shipToCompany);


            shipFromNode = objRateRequest.SelectSingleNode("//ShipFrom/Address");
            shipFromNode.SelectSingleNode("AddressLine1").InnerText = (!pIsRentalReturn ? shipFromAddr1 : shipToAddr1);
            shipFromNode.SelectSingleNode("AddressLine2").InnerText = (!pIsRentalReturn ? shipFromAddr2 : shipToAddr2);
            shipFromNode.SelectSingleNode("AddressLine3").InnerText = "";
            shipFromNode.SelectSingleNode("City").InnerText = (!pIsRentalReturn ? shipFromCity : shipToCity);

            //-----------------------------------------------------------------------------------
            // If this is an International shipment, do not specify a State
            //-----------------------------------------------------------------------------------
            if (isCanada)
            {
                shipFromNode.SelectSingleNode("StateProvinceCode").InnerText = (!pIsRentalReturn ? shipFromState : shipToState);
            }
            else if (isInternational)
            {
                shipFromNode.SelectSingleNode("StateProvinceCode").InnerText = "";
            }
            else
            {
                shipFromNode.SelectSingleNode("StateProvinceCode").InnerText = (!pIsRentalReturn ? shipFromState : shipToState);
            }
            shipFromNode.SelectSingleNode("PostalCode").InnerText = (!pIsRentalReturn ? shipFromZip : shipToZip);
            shipFromNode.SelectSingleNode("CountryCode").InnerText = (!pIsRentalReturn ? shipFromCountry : shipToCountry);

            //-----------------------------------------------------------------------------------
            // Loop thru and create box nodes for rating request
            // -- 1. 
            //-----------------------------------------------------------------------------------
            for (int x = 0; x < shippingBoxList.Count; x++)
            {
                objTemplateClone = objPackageTemplate.CloneNode(true);

                XmlNode dimensionsNode = objTemplateClone.SelectSingleNode("Dimensions");
                dimensionsNode.SelectSingleNode("Length").InnerText = shippingBoxList[x].BoxLength.ToString().Trim();
                dimensionsNode.SelectSingleNode("Width").InnerText = shippingBoxList[x].BoxWidth.ToString().Trim();
                dimensionsNode.SelectSingleNode("Height").InnerText = shippingBoxList[x].BoxHeight.ToString().Trim();

                XmlNode weightNode = objTemplateClone.SelectSingleNode("PackageWeight/Weight");
                weightNode.InnerText = (shippingBoxList[x].BoxWeightUsed < 1.00M ? "1" : shippingBoxList[x].BoxWeightUsed.ToString().Trim());

                XmlNode monetaryValue = objTemplateClone.SelectSingleNode("*/InsuredValue/MonetaryValue");

                if (shippingBoxList[x].BoxValue < 1000.00M)
                {
                    if (shippingBoxList[x].BoxValue > 100.00M)
                    {
                        shippingResult.totalInsValue = 200;
                        monetaryValue.InnerText = "200.00";
                    }
                    else
                    {
                        shippingResult.totalInsValue = 0;
                        monetaryValue.InnerText = "0.00";
                    }
                }
                else
                {
                    shippingResult.totalInsValue = shippingBoxList[x].BoxValue;
                    monetaryValue.InnerText = shippingBoxList[x].BoxValue.ToString();
                }

                XmlNode shipmentNode = objRateRequest.SelectSingleNode("//Shipment");
                shipmentNode.AppendChild(objTemplateClone);
                objTemplateClone = null;
            }

            //-----------------------------------------------------------------------------------
            //* Get Rates
            //-----------------------------------------------------------------------------------
            objRateReply = GetUPSRate(objRateRequest);

            //-----------------------------------------------------------------------------------
            //* Get Rates and Save Results
            //-----------------------------------------------------------------------------------
            if (isReplyStatusOK(objRateReply))
            {
                LogShippingMessage("RateForUps", "Rating Success. Go Log Charges", CONST_INFO);
                SetUPSShippingResultCharges(objRateReply, pIsRentalReturn);
                return true;
            }
            else
            {
                LogShippingMessage("RateForUps", "Rating Failed. See prior entries for details.", CONST_FATAL);
                return false;
            }
        }


        //========================================================================================
        // GETUPSRATE
        //========================================================================================
        protected XmlDocument GetUPSRate(XmlDocument objXMLShipment)
        {

            LogShippingMessage("GetUPSRate", "Starting GetUPSRate Routine", CONST_INFO);

            XmlDocument objXMLAccess;
            XmlDocument objXMLReceive = new XmlDocument();
            string lcPost;

            //--------------------------------------------------------------------------------
            // Check if UPS is out there
            //--------------------------------------------------------------------------------
            if (!UPS_Online())
            {
                LogShippingMessage("GetUPSRate", "UPS Is Not Online.", CONST_FATAL);
                return null;
            }
            else
            {
                //--------------------------------------------------------------------------------
                // Force specific SSL/TLS protocols.
                // Kyle: FIXME - Add TLS1.1 and TLS1.2 when we switch to .NET Framework 4.5 in the 
                // near future. TLS1.0 won't be supported much longer. -- Never use SSL2 or SSL3.
                //--------------------------------------------------------------------------------
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //--------------------------------------------------------------------------------
                // Create a request using a URL that can receive a post. 
                // Set the Method property of the request to POST.
                //--------------------------------------------------------------------------------
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.ups.com/ups.app/xml/Rate");
                request.Method = "POST";

                //--------------------------------------------------------------------------------
                // Create a UPS Access document and append the shipment request
                // Create POST data and convert it to a byte array.
                //--------------------------------------------------------------------------------
                objXMLAccess = CreateAccessXML();
                lcPost = objXMLAccess.OuterXml + objXMLShipment.OuterXml;
                byte[] byteArray = Encoding.UTF8.GetBytes(lcPost);

                //--------------------------------------------------------------------------------
                // Set the ContentType property of the WebRequest.
                // Set the ContentLength property of the WebRequest.
                //--------------------------------------------------------------------------------
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                //--------------------------------------------------------------------------------
                // Get the request stream. Try it 10 times for good effort.
                //--------------------------------------------------------------------------------
                Stream dataStream = null;
                int requestAttempts = 10;

                for (int x = 1; x < requestAttempts; x++)
                {
                    try
                    {
                        dataStream = request.GetRequestStream();
                        if (dataStream != null)
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        string errormsg = e.Message;
                        continue;
                    }
                }

                //--------------------------------------------------------------------------------
                // Write the data to the request stream.
                // Close the Stream object.
                //--------------------------------------------------------------------------------
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                //--------------------------------------------------------------------------------
                // Get the response.
                // Get the stream containing content returned by the server.
                // Open the stream using a StreamReader for easy access.
                // Read the content.
                //--------------------------------------------------------------------------------
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                objXMLReceive.LoadXml(reader.ReadToEnd());

                //--------------------------------------------------------------------------------
                //Clean up reader , datastream and response objects,
                //--------------------------------------------------------------------------------
                reader.Close();
                dataStream.Close();
                response.Close();

            }

            return objXMLReceive;
        }

        //========================================================================================
        // IS REPLY STATUS OK
        //========================================================================================
        protected bool isReplyStatusOK(XmlDocument pXmlResponse)
        {

            LogShippingMessage("isReplyStatusOK", "Starting isReplyStatusOK Routine", CONST_INFO);

            XmlNode objCode;
            XmlNode objDesc;
            XmlNodeList objNodeList;
            XmlNode objNode;

            objCode = pXmlResponse.SelectSingleNode("//ResponseStatusCode");
            objDesc = pXmlResponse.SelectSingleNode("//ResponseStatusDescription");

            //* This is an error document
            if (string.IsNullOrEmpty(objCode.InnerText) || objCode.InnerText == "0" ||
                string.IsNullOrEmpty(objDesc.InnerText) || objDesc.InnerText == "Failure")
            {
                objNodeList = pXmlResponse.SelectNodes("//Error");
                for (int lnI = 0; lnI < objNodeList.Count; lnI++)
                {
                    objNode = objNodeList.Item(lnI);
                    LogShippingMessage("isReplyStatusOK", objNode.SelectSingleNode("ErrorDescription").InnerText, objNode.SelectSingleNode("ErrorSeverity").InnerText);
                }
                return false;
            }

            if (!string.IsNullOrEmpty(objCode.InnerText) &&
                !string.IsNullOrEmpty(objDesc.InnerText)) //&& objCode.Value = 1)
            {
                LogShippingMessage("isReplyStatusOK", "UPS RESPONSE:" + objDesc.InnerText, CONST_INFO);
                return true;
            }

            return false;
        }

        //========================================================================================
        // SET UPS SHIPPING RESLT CHARGES
        //========================================================================================
        protected void SetUPSShippingResultCharges(XmlDocument pRateReplyXml, bool pIsRentalReturn = false)
        {

            LogShippingMessage("SetUPSShippingResultCharges", "Starting SetUPSShippingResultCharges Routine", CONST_INFO);

            //-----------------------------------------------------------------------------------
            // Grab the rated shipment parent node list
            //-----------------------------------------------------------------------------------
            XmlNodeList objNodeList = pRateReplyXml.SelectNodes("//RatedShipment");

            //-----------------------------------------------------------------------------------
            // If this is a rental return rating, we just want to update the result for return ground
            // without messing with anything else.....set it and get out. If not we want to set 
            // all the values for the rating following this
            //-----------------------------------------------------------------------------------
            if (pIsRentalReturn)
            {
                LogShippingMessage("SetUPSShippingResultCharges", "Capturing a ground return shipment rating", CONST_INFO);

                for (int x = 0; x < objNodeList.Count; x++)
                {
                    string serviceCode = objNodeList.Item(x).SelectSingleNode("./Service/Code").InnerText;
                    string negotiatedCharge = objNodeList.Item(x).SelectSingleNode("./NegotiatedRates/NetSummaryCharges/GrandTotal/MonetaryValue").InnerText;

                    if ((serviceCode == "03" && isDomestic) || (serviceCode == "11" && isCanada))
                    {
                        shippingResult.groundReturnRate = Convert.ToDecimal(negotiatedCharge);
                        break;
                    }
                }

                return;
            }


            //-----------------------------------------------------------------------------------
            // For each rated shipment result we will grab the code and charge and assign it to 
            // the proper shippingReuslt bucket
            //
            // This is where we need to grab the negotiated rates / publsihed rates based upon type
            //
            //  UPS Standard				11
            //  UPS Ground					03
            //  UPS 3 Day Select			12
            //  UPS 2nd Day Air				02
            //  UPS 2nd Day Air AM			59
            //  UPS Next Day Air Saver		13
            //  UPS Next Day Air			01
            //  UPS Next Day Air Early A.M.	14
            //  UPS Worldwide Express		07
            //  UPS Worldwide Express Plus	54
            //  UPS Worldwide Expedited		08
            //  UPS World Wide Saver		65
            //
            // Id	Shipper	  Code	Description						ShippersDescription	ShipVia
            // 3	UPS       	01  UPS Next Day Air      			UPS NXDAY			UPSNXTS     
            // 1	UPS       	03  UPS Ground              		UPS GROUND			UPSGRNC     
            // 2	UPS			02	UPS 2nd Day Air 				UPS 2DAY			UPS2NDS     
            // 7	UPS			12	UPS 3 Day Select 				UPS 3DAY SLT		UPSTHRD     
            // 8	UPS 		13	UPS Next Day Air Saver 			UPS NXDAYSAV		UPSNXAS     
            // 9	UPS			14	UPS Next Day Air Early A. M. 	UPS NXDAYAM			UPSNXTM     
            //-----------------------------------------------------------------------------------
            if (this.shipmentType == CONST_RENTAL || this.shipmentType == CONST_RISOP)
            {
                LogShippingMessage("SetUPSShippingResultCharges", "Capturing negotiated rates for rental or risop.", CONST_INFO);

                for (int x = 0; x < objNodeList.Count; x++)
                {
                    string serviceCode = objNodeList.Item(x).SelectSingleNode("./Service/Code").InnerText;
                    string negotiatedCharge = objNodeList.Item(x).SelectSingleNode("./NegotiatedRates/NetSummaryCharges/GrandTotal/MonetaryValue").InnerText;

                    switch (serviceCode)
                    {
                        case "03":
                        case "11":
                            shippingResult.groundRate = Convert.ToDecimal(negotiatedCharge);
                            break;
                        case "02":
                        case "08":
                            shippingResult.twoDayRate = Convert.ToDecimal(negotiatedCharge);
                            break;
                        case "01":
                        case "07":
                            shippingResult.nextDayRate = Convert.ToDecimal(negotiatedCharge);
                            break;
                        case "12":
                            shippingResult.premiumRate1 = Convert.ToDecimal(negotiatedCharge);
                            break;
                        case "13":
                            shippingResult.premiumRate2 = Convert.ToDecimal(negotiatedCharge);
                            break;
                        case "14":
                            shippingResult.premiumRate3 = Convert.ToDecimal(negotiatedCharge);
                            break;
                    }

                }
            }
            else
            {
                LogShippingMessage("SetUPSShippingResultCharges", "Capturing published rates for sale.", CONST_INFO);

                for (int x = 0; x < objNodeList.Count; x++)
                {
                    string serviceCode = objNodeList.Item(x).SelectSingleNode("./Service/Code").InnerText;
                    string serviceCharge = objNodeList.Item(x).SelectSingleNode("./TotalCharges/MonetaryValue").InnerText;

                    switch (serviceCode)
                    {
                        case "03":
                        case "11":
                            shippingResult.groundRate = Convert.ToDecimal(serviceCharge);
                            break;
                        case "02":
                        case "08":
                            shippingResult.twoDayRate = Convert.ToDecimal(serviceCharge);
                            break;
                        case "01":
                        case "07":
                            shippingResult.nextDayRate = Convert.ToDecimal(serviceCharge);
                            break;
                        case "12":
                            shippingResult.premiumRate1 = Convert.ToDecimal(serviceCharge);
                            break;
                        case "13":
                            shippingResult.premiumRate2 = Convert.ToDecimal(serviceCharge);
                            break;
                        case "14":
                            shippingResult.premiumRate3 = Convert.ToDecimal(serviceCharge);
                            break;
                    }

                }
            }

            return;
        }

        //========================================================================================
        // POST RATE HANDLING
        //
        // private decimal	configRisopGroundPctMarkup;
        // private decimal configRisopAirPctMarkup;
        // private decimal configRentalGroundPctMarkup;
        // private decimal configRentalAirPctMarkup;
        // private decimal configSaleGroundPctMarkup;
        // private decimal configSaleAirPctMarkup;
        //
        //
        //UPSGRNC
        //UPS2NDS
        //UPSNXTS
        //UPSTHRD
        //UPSNXAS
        //UPSNXTM
        //========================================================================================
        protected bool PostRateHandling()
        {
            LogShippingMessage("PostRateHandling", String.Format("Pre-Handling UPSGRNC Rate: {0}", shippingResult.groundRate), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Pre-Handling Return UPSGRNC Rate: {0}", shippingResult.groundReturnRate), CONST_INFO);

            LogShippingMessage("PostRateHandling", String.Format("Pre-Handling UPS2NDS Rate: {0}", shippingResult.twoDayRate), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Pre-Handling UPSNXTS Rate: {0}", shippingResult.nextDayRate), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Pre-Handling UPSTHRD Rate: {0}", shippingResult.premiumRate1), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Pre-Handling UPSNXAS Rate: {0}", shippingResult.premiumRate2), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Pre-Handling UPSNXTM Rate: {0}", shippingResult.premiumRate3), CONST_INFO);

            switch (this.shipmentType)
            {
                case CONST_SALE:
                case CONST_POP:
                    LogShippingMessage("PostRateHandling", String.Format("Ground Pct Markup: {0}", configSaleGroundPctMarkup), CONST_INFO);
                    LogShippingMessage("PostRateHandling", String.Format("Air Pct Markup: {0}", configSaleAirPctMarkup), CONST_INFO);

                    shippingResult.groundRate *= configSaleGroundPctMarkup;
                    shippingResult.twoDayRate *= 1.0M; //configSaleAirPctMarkup;
                    shippingResult.nextDayRate *= 1.0M;
                    shippingResult.premiumRate1 *= 1.0M;
                    shippingResult.premiumRate2 *= 1.0M;
                    shippingResult.premiumRate3 *= 1.0M;
                    break;
                case CONST_RISOP:
                    LogShippingMessage("PostRateHandling", String.Format("Ground Pct Markup: {0}", configRisopGroundPctMarkup), CONST_INFO);
                    LogShippingMessage("PostRateHandling", String.Format("Air Pct Markup: {0}", configRisopAirPctMarkup), CONST_INFO);

                    shippingResult.groundRate *= configRisopGroundPctMarkup;
                    shippingResult.twoDayRate *= configRisopAirPctMarkup;
                    shippingResult.nextDayRate *= configRisopAirPctMarkup;
                    shippingResult.premiumRate1 *= configRisopAirPctMarkup;
                    shippingResult.premiumRate2 *= configRisopAirPctMarkup;
                    shippingResult.premiumRate3 *= configRisopAirPctMarkup;
                    break;
                case CONST_RENTAL:
                    LogShippingMessage("PostRateHandling", String.Format("Ground Pct Markup: {0}", configRentalGroundPctMarkup), CONST_INFO);
                    LogShippingMessage("PostRateHandling", String.Format("Air Pct Markup: {0}", configRentalAirPctMarkup), CONST_INFO);

                    shippingResult.groundRate *= configRentalGroundPctMarkup;
                    shippingResult.groundReturnRate *= configRentalGroundPctMarkup;

                    shippingResult.twoDayRate *= configRentalAirPctMarkup;
                    shippingResult.nextDayRate *= configRentalAirPctMarkup;
                    shippingResult.premiumRate1 *= configRentalAirPctMarkup;
                    shippingResult.premiumRate2 *= configRentalAirPctMarkup;
                    shippingResult.premiumRate3 *= configRentalAirPctMarkup;
                    break;
                default:
                    LogShippingMessage("PostRateHandling", "Unable to season the rates. Type Unknown.", CONST_FATAL);
                    return false;
            }


            LogShippingMessage("PostRateHandling", String.Format("Post-Handling UPSGRNC Rate: {0}", shippingResult.groundRate), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Post-Handling Return UPSGRNC Rate: {0}", shippingResult.groundReturnRate), CONST_INFO);

            LogShippingMessage("PostRateHandling", String.Format("Post-Handling UPS2NDS Rate: {0}", shippingResult.twoDayRate), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Post-Handling UPSNXTS Rate: {0}", shippingResult.nextDayRate), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Post-Handling UPSTHRD Rate: {0}", shippingResult.premiumRate1), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Post-Handling UPSNXAS Rate: {0}", shippingResult.premiumRate2), CONST_INFO);
            LogShippingMessage("PostRateHandling", String.Format("Post-Handling UPSNXTM Rate: {0}", shippingResult.premiumRate3), CONST_INFO);

            return true;
        }


        //========================================================================================
        // Additional Handling Charges Based On Order Like Box charges
        //========================================================================================
        protected bool AdditionalHandling()
        {

            decimal additiveBoxCharge = 0;
            decimal additiveSaleCharge = 0;

            // total up the boxcost for the order
            additiveBoxCharge = shippingBoxList.Sum(z => z.BoxCost);
            LogShippingMessage("AdditionalHandling", String.Format("Adding in total cost of boxes: {0:C}", additiveBoxCharge), CONST_INFO);

            //-------------------------------------------------------------------------------------
            // UPdate and report the results of adding in the additional handling.
            // Dont update the rental return rate since they will use same box or supply their own.
            //-------------------------------------------------------------------------------------
            shippingResult.groundRate += additiveBoxCharge;
            shippingResult.twoDayRate += additiveBoxCharge;
            shippingResult.nextDayRate += additiveBoxCharge;
            shippingResult.premiumRate1 += additiveBoxCharge;
            shippingResult.premiumRate2 += additiveBoxCharge;
            shippingResult.premiumRate3 += additiveBoxCharge;

            if (this.shipmentType == CONST_SALE || this.shipmentType == CONST_POP)
            {
                LogShippingMessage("AdditionalHandling", String.Format("Adding in {0:C} up charge for SALE/POP.", additiveSaleCharge), CONST_INFO);
                shippingResult.groundRate += additiveSaleCharge;
                shippingResult.twoDayRate += additiveSaleCharge;
                shippingResult.nextDayRate += additiveSaleCharge;
                shippingResult.premiumRate1 += additiveSaleCharge;
                shippingResult.premiumRate2 += additiveSaleCharge;
                shippingResult.premiumRate3 += additiveSaleCharge;
            }

            LogShippingMessage("AdditionalHandling", String.Format("Post-AdditionalHandling UPSGRNC Rate: {0}", shippingResult.groundRate), CONST_INFO);
            LogShippingMessage("AdditionalHandling", String.Format("Post-AdditionalHandling Return UPSGRNC Rate: {0}", shippingResult.groundReturnRate), CONST_INFO);

            LogShippingMessage("AdditionalHandling", String.Format("Post-AdditionalHandling UPS2NDS Rate: {0}", shippingResult.twoDayRate), CONST_INFO);
            LogShippingMessage("AdditionalHandling", String.Format("Post-AdditionalHandling UPSNXTS Rate: {0}", shippingResult.nextDayRate), CONST_INFO);
            LogShippingMessage("AdditionalHandling", String.Format("Post-AdditionalHandling UPSTHRD Rate: {0}", shippingResult.premiumRate1), CONST_INFO);
            LogShippingMessage("AdditionalHandling", String.Format("Post-AdditionalHandling UPSNXAS Rate: {0}", shippingResult.premiumRate2), CONST_INFO);
            LogShippingMessage("AdditionalHandling", String.Format("Post-AdditionalHandling UPSNXTM Rate: {0}", shippingResult.premiumRate3), CONST_INFO);

            return true;
        }

        //========================================================================================
        // Potential Discount Application based upon sale type. 
        //========================================================================================
        protected bool ApplyDiscount()
        {
            shippingResult.discountPct = 0;

            if (this.shipmentType == CONST_SALE || this.shipmentType == CONST_POP)
            {
                LogShippingMessage("Apply Sale Discount", String.Format("Factoring in {0:C} discount for SALE/POP.", this.configSaleDiscountPct), CONST_INFO);

                shippingResult.groundRate -= (shippingResult.groundRate * configSaleDiscountPct);
                shippingResult.twoDayRate -= (shippingResult.twoDayRate * configSaleDiscountPct);
                shippingResult.nextDayRate -= (shippingResult.nextDayRate * configSaleDiscountPct);
                shippingResult.premiumRate1 -= (shippingResult.premiumRate1 * configSaleDiscountPct);
                shippingResult.premiumRate2 -= (shippingResult.premiumRate2 * configSaleDiscountPct);
                shippingResult.premiumRate3 -= (shippingResult.premiumRate3 * configSaleDiscountPct);
                shippingResult.discountPct = configSaleDiscountPct;
            }

            if (this.shipmentType == CONST_RENTAL)
            {
                LogShippingMessage("Apply Rental Discount", String.Format("Factoring in {0:C} discount for RENTAL.", this.configRentalDiscountPct), CONST_INFO);

                shippingResult.groundRate -= (shippingResult.groundRate * configRentalDiscountPct);
                shippingResult.twoDayRate -= (shippingResult.twoDayRate * configRentalDiscountPct);
                shippingResult.nextDayRate -= (shippingResult.nextDayRate * configRentalDiscountPct);
                shippingResult.premiumRate1 -= (shippingResult.premiumRate1 * configRentalDiscountPct);
                shippingResult.premiumRate2 -= (shippingResult.premiumRate2 * configRentalDiscountPct);
                shippingResult.premiumRate3 -= (shippingResult.premiumRate3 * configRentalDiscountPct);
                shippingResult.discountPct = configRentalDiscountPct;
            }

            if (this.shipmentType == CONST_RISOP)
            {
                LogShippingMessage("Apply RISOP Discount", String.Format("Factoring in {0:C} discount for RISOP.", this.configRisopDiscountPct), CONST_INFO);

                shippingResult.groundRate -= (shippingResult.groundRate * configRisopDiscountPct);
                shippingResult.twoDayRate -= (shippingResult.twoDayRate * configRisopDiscountPct);
                shippingResult.nextDayRate -= (shippingResult.nextDayRate * configRisopDiscountPct);
                shippingResult.premiumRate1 -= (shippingResult.premiumRate1 * configRisopDiscountPct);
                shippingResult.premiumRate2 -= (shippingResult.premiumRate2 * configRisopDiscountPct);
                shippingResult.premiumRate3 -= (shippingResult.premiumRate3 * configRisopDiscountPct);
                shippingResult.discountPct = configRisopDiscountPct;
            }

            LogShippingMessage("ApplyDiscount", String.Format("Post-Discount UPSGRNC Rate: {0}", shippingResult.groundRate), CONST_INFO);
            LogShippingMessage("ApplyDiscount", String.Format("Post-Discount Return UPSGRNC Rate: {0}", shippingResult.groundReturnRate), CONST_INFO);

            LogShippingMessage("ApplyDiscount", String.Format("Post-Discount UPS2NDS Rate: {0}", shippingResult.twoDayRate), CONST_INFO);
            LogShippingMessage("ApplyDiscount", String.Format("Post-Discount UPSNXTS Rate: {0}", shippingResult.nextDayRate), CONST_INFO);
            LogShippingMessage("ApplyDiscount", String.Format("Post-Discount UPSTHRD Rate: {0}", shippingResult.premiumRate1), CONST_INFO);
            LogShippingMessage("ApplyDiscount", String.Format("Post-Discount UPSNXAS Rate: {0}", shippingResult.premiumRate2), CONST_INFO);
            LogShippingMessage("ApplyDiscount", String.Format("Post-Discount UPSNXTM Rate: {0}", shippingResult.premiumRate3), CONST_INFO);

            return true;
        }


        //========================================================================================
        // FINALISE SHIPPING RESLTS
        //========================================================================================
        protected bool FinalizeShippingResults()
        {
            //LogShippingMessage("FinalizeShippingResults", "Starting FinalizeShippingResults Routine", CONST_INFO);

            shippingResult.boxedItemArray = boxedItemsList.ToArray();
            shippingResult.shippedBoxArray = shippingBoxList.ToArray();

            return true;
        }


        //========================================================================================
        // UPS ONLINE
        //========================================================================================
        protected bool UPS_Online()
        {

            //LogShippingMessage("UPS_Online", "Starting UPS_Online Routine", CONST_INFO);
            string responseAsString;

            //------------------------------------------------------------------------------------
            // Force specific SSL/TLS protocols.
            // Create request with url for rating
            //------------------------------------------------------------------------------------
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.ups.com/ups.app/xml/Rate");

            //------------------------------------------------------------------------------------
            // Set some reasonable limits on resources used by this request 
            // Set credentials to use for this request. 
            //------------------------------------------------------------------------------------
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            request.Credentials = CredentialCache.DefaultCredentials;

            //------------------------------------------------------------------------------------
            // Get the response	
            // Get the stream associated with the response. 
            // Pipes the stream to a higher level stream reader with the required encoding format.  
            //------------------------------------------------------------------------------------
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            responseAsString = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            return (responseAsString.IndexOf("Service Name: Rate") > -1);
        }


        //========================================================================================
        // CREATE ACCESS XML
        // Create a new UPS Access Document by loading the template	
        //========================================================================================
        protected XmlDocument CreateAccessXML()
        {
            LogShippingMessage("CreateAccessXML", "Starting CreateAccessXML Routine", CONST_INFO);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc = LoadXML(configUpsAccessXml);
            return xmlDoc;
        }

        //========================================================================================
        // LOAD XML
        //========================================================================================
        protected XmlDocument LoadXML(string pcTemplateFileName)
        {

            LogShippingMessage("LoadXML", "Starting LoadXML Routine", CONST_INFO);

            //ToDo:Shakti Create template folder for storing xml templates
            string htmlPath = HttpContext.Current !=null ? HttpContext.Current.Server.MapPath("~/Xml") : @"C:\Users\Windows10\source\repos\ShippingApi\ShippingApi\Xml";
            string templatePath = Path.Combine(htmlPath, pcTemplateFileName.Trim());

            //-------------------------------------------------------------------------------------
            // Determine that the template file exists 
            //-------------------------------------------------------------------------------------
            if (!File.Exists(templatePath))
            {
                LogShippingMessage("LoadXML", "XML Template File " +
                                                templatePath +
                                                "Not Located", CONST_FATAL);
                return null;
            }

            //-------------------------------------------------------------------------------------
            // Setup XML ReaderSettings for XML Reader which allows validation of XML
            //-------------------------------------------------------------------------------------
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            XmlReader reader;

            //-------------------------------------------------------------------------------------
            // attempt to create the XML Reader with given file and settings
            //-------------------------------------------------------------------------------------
            try
            {
                reader = XmlReader.Create(templatePath, settings);
            }
            catch (XmlException)
            {
                LogShippingMessage("LoadXML", "XML Template File Creation For " + templatePath + " has failed.", CONST_FATAL);
                return null;
            }

            //-------------------------------------------------------------------------------------
            // Pass the validating reader to the XML document. 
            //-------------------------------------------------------------------------------------
            XmlDocument xmlForTemplate = new XmlDocument();
            xmlForTemplate.Load(reader);
            return xmlForTemplate;
        }

        //========================================================================================
        // CREATE REQUEST XML
        //========================================================================================
        protected XmlDocument CreateRequestXML()
        {

            LogShippingMessage("CreateRequestXML", "Starting CreateRequestXML Routine", CONST_INFO);

            XmlDocument xmlDoc = new XmlDocument();
            XmlNode ccNode;

            // Create a new UPS Access Document
            xmlDoc = LoadXML(configUpsRatingXml);

            // did it load?
            if (!string.IsNullOrEmpty(xmlDoc.DocumentElement.ToString()))
            {
                // Does it have a "//CustomerContext" node?
                ccNode = xmlDoc.SelectSingleNode("//CustomerContext");

                if (!string.IsNullOrEmpty(ccNode.ToString()))
                {
                    // Make the id of the request for shipment quote "//CustomerContext" 
                    // unique by setting it to a random number
                    int rand = new Random().Next(10000000);
                    ccNode.InnerText = rand.ToString();
                }
            }

            return xmlDoc;
        }

        //========================================================================================
        // LOG SHIPPING ERROR
        //========================================================================================
        protected void LogShippingMessage(string pSource, string pMessage, string pSeverity)
        {
            ShippingNote error = new ShippingNote();
            error.source = pSource;
            error.message = pMessage;
            error.severity = pSeverity;
            this.shippingNoteList.Add(error);
        }

        //========================================================================================
        // SEND SHIPPING AUTOPSY EMAIL
        //========================================================================================
        protected void SendShippingAutopsyEmail(int wppSono, string customerNumber)
        {
            //int wppSono = ShoppingCartController.ShoppingCart.Wppsono;

            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["CCFailedAuthorizationSmtpServer"]);

            string siteName = ConfigurationManager.AppSettings["SiteName"];
            string friendlyFrom = ConfigurationManager.AppSettings["CCFailedAuthorizationFriendlyFrom"];
            string friendlyName = string.Format("\"{0} {1}\"", siteName, friendlyFrom);
            string friendlyEmail = ConfigurationManager.AppSettings["CCFailedAuthorizationFrom"];
            string emailTo = ConfigurationManager.AppSettings["CCFailedAuthorizationTo"];

            MailMessage email = new MailMessage();
            email.Subject = string.Format("{0}{1} for Custno: {2} ", "Shipment Rating Autopsy - WppSono:", wppSono, customerNumber);
            email.Priority = MailPriority.High;

            StringBuilder body = new StringBuilder(string.Empty);
            body.Append("From: ");
            body.Append("The Dude");
            body.Append(Environment.NewLine);
            body.Append(Environment.NewLine);
            body.Append(string.Format("Web Order Number {0} Ship Rating Autopsy", wppSono));

            body.Append(Environment.NewLine);
            body.Append(Environment.NewLine);

            foreach (ShippingNote currentNote in this.shippingNoteList)
            {
                if (currentNote.severity == CONST_INFO)
                {
                    body.Append(currentNote.message);
                    body.Append(Environment.NewLine);
                }
            }

            body.Append(Environment.NewLine);
            body.Append(Environment.NewLine);

            email.Body = body.ToString();
            email.From = new MailAddress(friendlyEmail, friendlyFrom);
            //email.To.Add(emailTo);
            email.To.Add("dan.johnson@ecinternet.com");
            email.To.Add("pglickman@pecata.com");

            try
            {
                smtp.Send(email);
            }
            catch
            {
            }
        }


    }
}