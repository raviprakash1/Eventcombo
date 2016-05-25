using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using System.Configuration;

namespace EventCombo.Models
{
    public class PaymentProcess
    {
        public static cardtransaction CheckCreditCard(String ApiLoginID, String ApiTransactionKey, string strCardNo, string strExpDate, string strCvvCode, decimal dAmount)
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

                var transactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card
                    amount = dAmount,
                    payment = paymentType
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
                    }
                }

            }catch (Exception ex)
            {
                message = "E";
                carddet.Transactionhash = "";
                carddet.TransactionId = "";
                carddet.message = message;
                ExceptionLogging.SendErrorToText(ex);
            }

            return carddet;
        }
    }

    public class cardtransaction
    {
        public string message { get; set; }
        public string Transactionhash { get; set; }
        public string TransactionId { get; set; }
    }
}