using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using System.Configuration;
using NLog;

namespace EventCombo.Models
{
    public class PaymentProcess
    {
      private static Logger logger = LogManager.GetCurrentClassLogger();
      public static cardtransaction CheckCreditCard(String ApiLoginID, String ApiTransactionKey, string strCardNo, string strExpDate, string strCvvCode, decimal dAmount, string strOrderNo, TicketPayment model, string strUserId)
        {
            Console.WriteLine("Charge Credit Card Sample");
            string message = "";
            string transactionhash = "", transactionid = "";
            cardtransaction carddet = new cardtransaction();
            try
            {
                string strEnv = ConfigurationManager.AppSettings.Get("AuthorzieNetEnvironment");
                if (strEnv.ToLower() == "production")
                    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
                else
                    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = ApiLoginID,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = ApiTransactionKey,
                };
            

                var creditCard = new creditCardType
                {
                    cardNumber = strCardNo,
                    expirationDate = strExpDate,
                    cardCode = strCvvCode
                };

                //standard api call to retrieve response
                var paymentType = new paymentType { Item = creditCard };
                orderType objot = new orderType();
                objot.invoiceNumber = strOrderNo;
                //objot.description = "description";
                nameAndAddressType objShiping = new nameAndAddressType();
                customerAddressType objCAddType = new customerAddressType();
                using (EventComboEntities context = new EventComboEntities())
                {
                    int iCid = (model.billcountry != "" ? Convert.ToInt16(model.billcountry) : 0);
                    var vCountry = (from mycountry in context.Countries where mycountry.CountryID == iCid select mycountry.Country1).FirstOrDefault();


                    
                    objCAddType.address = model.billaddress1 + " " + model.billaddress2;
                    objCAddType.city = model.billcity;
                    objCAddType.country = vCountry;
                    objCAddType.firstName = model.billfname;
                    objCAddType.lastName = model.billLname;
                    objCAddType.phoneNumber = model.billingphno;
                    objCAddType.state = model.billstate;
                    objCAddType.zip = model.billzip;

                   


                    if (model.Saveshipdetail != "N")
                    {
                        iCid = (model.shipcountry != "" ? Convert.ToInt16(model.shipcountry) : 0);
                        vCountry = (from mycountry in context.Countries where mycountry.CountryID == iCid select mycountry.Country1).FirstOrDefault();

                        objShiping.address = model.shipaddress1 + " " + model.shipaddress2;
                        objShiping.city = model.shipcity;
                        objShiping.country = vCountry;
                        objShiping.firstName = model.shipfname;
                        objShiping.lastName = model.shipLname;
                        objShiping.state = model.shipstate;
                        objShiping.zip = model.shipzip;
                    }
                    if (model.sameshipbilldetail == "Y")
                    {
                        objShiping.address = model.billaddress1 + " " + model.billaddress2;
                        objShiping.city = model.billcity;
                        objShiping.country = vCountry;
                        objShiping.firstName = model.billfname;
                        objShiping.lastName = model.billLname;
                        objShiping.state = model.billstate;
                        objShiping.zip = model.billzip;
                    }

                }
               
                
//Shipping Address 
              

                customerDataType objCdt = new customerDataType();
                objCdt.id = "";
                objCdt.email = model.AccEmail;

                var transactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card
                    amount = dAmount,
                    payment = paymentType,
                    order = objot,
                    billTo = objCAddType,
                    shipTo = objShiping,
                    customer = objCdt
                };

                var request = new createTransactionRequest { transactionRequest = transactionRequest };

                // instantiate the contoller that will call the service
                var controller = new createTransactionController(request);
                controller.Execute();

                // get the response from the service (errors contained if any)
                var response = controller.GetApiResponse();

                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse != null)
                    {
                        Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                        message = "O";
                        transactionhash = response.transactionResponse.transHash;
                        transactionid = response.transactionResponse.transId;
                        var uerfileds = response.transactionResponse.userFields;
                        carddet.Transactionhash = transactionhash;
                        carddet.TransactionId = transactionid;
                        carddet.message = message;
                        using (EventComboEntities context = new EventComboEntities())
                        {
                            Order_Detail_T objOdt = (from myOrder in context.Order_Detail_T where myOrder.O_Order_Id == strOrderNo select myOrder).FirstOrDefault();
                            if (objOdt != null)
                            {
                                objOdt.O_Card_TransId = transactionid;
                                objOdt.O_Card_TransHash = transactionhash;
                                context.SaveChanges();
                            }
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                    if (response.transactionResponse != null)
                    {
                        Console.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                        message = response.transactionResponse.errors[0].errorText;
                        carddet.Transactionhash = "";
                        carddet.TransactionId = "";
                        carddet.message = message;
                        using (EventComboEntities context = new EventComboEntities())
                        {
                            context.Database.ExecuteSqlCommand("DELETE FROM Order_Detail_T WHERE O_Order_Id ='" + strOrderNo + "'");
                        }
                    }
                }

            }catch (Exception ex)
            {
                message = "E";
                carddet.Transactionhash = "";
                carddet.TransactionId = "";
                carddet.message = message;
                using (EventComboEntities context = new EventComboEntities())
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM Order_Detail_T WHERE O_Order_Id ='" + strOrderNo + "'");
                }
                logger.Error("Exception during request processing", ex);
            }

            return carddet;
        }
    }

    public class cardtransaction
    {
        public string message { get; set; }
        public string UserMessage { get; set; }
        public string Transactionhash { get; set; }
        public string TransactionId { get; set; }
        public bool Success { get; set; }
    }
}