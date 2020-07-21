using System;
using ClientSite.controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ShippingApi.DataStructure;
using ShippingApi.Helpers;
using WPPDataModel.ShippingSystem.DataStructure;

namespace ShippingApi.Tests
{
    [TestClass]
    public class UtexShippingControllerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //shippingSelected = shippingController.CalculateShipping(false);

            UTEXShippingController utexShippingController = null;
            //--------------------------------------------------------------------------------------------------------------
            // Instantiate the controller for rating. The process is now instantiated and populated from values from the 
            // configuration table.
            //--------------------------------------------------------------------------------------------------------------
            utexShippingController = new UTEXShippingController();

            //-----------------------------------------------------------------------------------------------------
            // Rate the Shipment - If successful, we map the utex codes to the appropriate return rating value
            // Failure at any point in this process will lead to zero freight which will end up on invoice
            // as TBD with message indicating inability to rate for order. Will require manual intervention.
            //-----------------------------------------------------------------------------------------------------
            if (utexShippingController != null && utexShippingController.instantiatedOk)
            {
                BCart cart = new BCart();
                cart.CartItems = new BCartItem[3];

                cart.CartItems[0] = new BCartItem() { Color = "Red", ItemId = "1", OrderQuantity = 4, Price = 20.0M, Volume = 2, Weight = 20 };
                cart.CartItems[1] = new BCartItem() { Color = "Yellow", ItemId = "2", OrderQuantity = 2, Price = 30.0M, Volume = 1, Weight = 30 };
                cart.CartItems[2] = new BCartItem() { Color = "Green", ItemId = "POP3", OrderQuantity = 1, Price = 10.0M, Volume = 4, Weight = 10 };

                ShipToAddress shipToAddress = new ShipToAddress(new CustomerAddressData());
                ShipRateConfigData shipRateConfigData = new ShipRateConfigData() { RateForUSA = true };
                shipToAddress.Company = "company";
                shipToAddress.Address1 = "Address1";
                shipToAddress.Address2 = "Address2";
                shipToAddress.City     = "City";
                shipToAddress.State    = "State";
                shipToAddress.Zip      = "Zip";
                shipToAddress.Country  = "UNITED STATES";
                RateShipmentRequest request = new RateShipmentRequest() { Cart = cart, ShipRateConfigData = shipRateConfigData, ShippingLineItemId = "12", ShipToAddress = shipToAddress };
                string dummy = JsonConvert.SerializeObject(request);

                var shippingResults = utexShippingController.RateShipment(request);
            }


            //if (utexShippingController != null && utexShippingController.ratingSuccess)
            //{
            //    hasDiscountedFreight = (shippingResults.discountPct != 0);

            //    if (hasDiscountedFreight)
            //    {
            //        shoppingCart.ShippingLineItem.Description += string.Format(" ({0:P} off)", shippingResults.discountPct);
            //    }

            //    switch (shoppingCart.Shipvia)
            //    {
            //        case "UPSGRNC":
            //        case "UPSCDAG":
            //            if (shippingResults.groundRate > 0)
            //            {
            //                shoppingCart.ShippingLineItem.Price = shippingResults.groundRate + shippingResults.groundReturnRate;
            //            }
            //            else
            //            {
            //                shoppingCart.ShippingLineItem.Price = 0;
            //                labelShipping.Text = "(Shipping Option Not Available)";
            //                selectedShipMethodOK = false;
            //            }
            //            break;

            //        case "UPS2NDS":
            //        case "UPSCDA2":
            //            if (shippingResults.twoDayRate > 0)
            //            {
            //                shoppingCart.ShippingLineItem.Price = shippingResults.twoDayRate + shippingResults.groundReturnRate;
            //            }
            //            else
            //            {
            //                shoppingCart.ShippingLineItem.Price = 0;
            //                labelShipping.Text = "(Shipping Option Not Available)";
            //                selectedShipMethodOK = false;
            //            }
            //            break;

            //        case "UPSNXTS":
            //        case "UPSCDA2N":
            //            if (shippingResults.nextDayRate > 0)
            //            {
            //                shoppingCart.ShippingLineItem.Price = shippingResults.nextDayRate + shippingResults.groundReturnRate;
            //            }
            //            else
            //            {
            //                shoppingCart.ShippingLineItem.Price = 0;
            //                labelShipping.Text = "(Shipping Option Not Available)";
            //                selectedShipMethodOK = false;
            //            }
            //            break;

            //        case "UPSTHRD":
            //            if (shippingResults.premiumRate1 > 0)
            //            {
            //                shoppingCart.ShippingLineItem.Price = shippingResults.premiumRate1 + shippingResults.groundReturnRate;
            //            }
            //            else
            //            {
            //                shoppingCart.ShippingLineItem.Price = 0;
            //                labelShipping.Text = "(Shipping Option Not Available)";
            //                selectedShipMethodOK = false;
            //            }
            //            break;

            //        case "UPSNXAS":
            //            if (shippingResults.premiumRate2 > 0)
            //            {
            //                shoppingCart.ShippingLineItem.Price = shippingResults.premiumRate2 + shippingResults.groundReturnRate;
            //            }
            //            else
            //            {
            //                shoppingCart.ShippingLineItem.Price = 0;
            //                labelShipping.Text = "(Shipping Option Not Available)";
            //                selectedShipMethodOK = false;
            //            }
            //            break;

            //        case "UPSNXTM":
            //            if (shippingResults.premiumRate3 > 0)
            //            {
            //                shoppingCart.ShippingLineItem.Price = shippingResults.premiumRate3 + shippingResults.groundReturnRate;
            //            }
            //            else
            //            {
            //                shoppingCart.ShippingLineItem.Price = 0;
            //                labelShipping.Text = "(Shipping Option Not Available)";
            //                selectedShipMethodOK = false;
            //            }
            //            break;

            //        default:
            //            shoppingCart.Shipvia = "WILL ADVISE";
            //            shoppingCart.ShippingLineItem.Price = 0;
            //            break;
            //    }


            //    ALL RISOP assuming credit card up charge provided their is a rating.
            //    if (shoppingCart.ShippingLineItem.Price > 0 && utexShippingController.shipmentType == "RISOP")
            //    {
            //        shoppingCart.ShippingLineItem.Price = ((ShoppingCartController.OrderSubtotal + shoppingCart.ShippingLineItem.Price) * 1.03M) - ShoppingCartController.OrderSubtotal;
            //    }

            //    If rental and the customers default pterms do not include NET then we can assume a credit card upcharge of toal amount + postage * 1.03
            //    if (shoppingCart.ShippingLineItem.Price > 0 && (utexShippingController.shipmentType == "RENTAL") && !CustomerController.Pterms().ToUpper().Contains("NET"))
            //    {
            //        shoppingCart.ShippingLineItem.Price = ((ShoppingCartController.OrderSubtotal + shoppingCart.ShippingLineItem.Price) * 1.03M) - ShoppingCartController.OrderSubtotal;
            //    }

            //    shoppingCart.ShippingLineItem.Price = Math.Round(shoppingCart.ShippingLineItem.Price, 2);
            //    shoppingCart.ShippingLineItem.Extprice = shoppingCart.ShippingLineItem.Price;
            //}
            //else
            //{
            //    shoppingCart.ShippingLineItem.Price = 0;
            //    shoppingCart.ShippingLineItem.Extprice = shoppingCart.ShippingLineItem.Price;
            //}

            //if (shoppingCart.ShippingLineItem.Extprice > 0 && selectedShipMethodOK)
            //{
            //    if (hasDiscountedFreight)
            //    {
            //        labelShipping.Text = string.Format("{0:C} ({1:P} off)", shoppingCart.ShippingLineItem.Extprice, shippingResults.discountPct);
            //    }
            //    else
            //    {
            //        labelShipping.Text = string.Format("{0:C}", shoppingCart.ShippingLineItem.Extprice);
            //    }
            //}
            //else
            //{
            //    if (selectedShipMethodOK)
            //    {
            //        labelShipping.Text = "(TBD)";
            //    }


            //}
        }
        }
    }
