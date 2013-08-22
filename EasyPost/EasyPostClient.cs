using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Easypost.Internal;
using EasyPost.Model;
using Newtonsoft.Json;

namespace EasyPost
{
    /// <summary>
    /// Wrapper for the EasyPost REST API.
    /// All methods throw an exception if a non-success status code comes back from EasyPost.
    /// </summary>
    /// <seealso cref="http://www.easypost.com/docs"/>
    public class EasyPostClient : IEasyPostClient
    {
        private readonly HttpClient _client;

        public EasyPostClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("invalid api key", apiKey);
            }

            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseProxy = true,
            };

            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(EasyPostUrls.BASE_URL)
            };

            var apiKeyBase64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:", apiKey)));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", apiKeyBase64String);
        }

        /// <summary>
        /// Creates a new Address
        /// </summary>
        /// <param name="model">The Address to be created on the server</param>
        /// <returns>A fully populated Address, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#addresses"/>
        public Address CreateAddress(Address model)
        {
            return Execute<Address>(model, EasyPostUrls.ADDRESSES);
        }

        /// <summary>
        /// Retrieves an Address by Id
        /// </summary>
        /// <param name="addressId">The Id of the Address to retrieve</param>
        /// <returns>The requested Address</returns>
        /// <seealso cref="http://www.easypost.com/docs#addresses"/>
        public Address GetAddress(string addressId)
        {
            var url = string.Format(EasyPostUrls.ADDRESS, addressId);
            return Execute<Address>(url);
        }

        /// <summary>
        /// Query for all Addresses
        /// </summary>
        /// <returns>A list of all Addresses</returns>
        /// <seealso cref="http://www.easypost.com/docs#addresses"/>
        public List<Address> GetAddresses()
        {
            return Execute<List<Address>>(EasyPostUrls.ADDRESSES);
        }

        /// <summary>
        /// Verifies an Address is valid for shipment
        /// </summary>
        /// <param name="addressId">The Id of the Address to validate</param>
        /// <returns>The closest verified Address, and a message if the address is valid but missing some data</returns>
        /// <seealso cref="http://www.easypost.com/docs#addresses"/>
        public VerifiedAddress VerifyAddress(string addressId)
        {
            var url = string.Format(EasyPostUrls.ADDRESS_VERIFY, addressId);
            return Execute<VerifiedAddress>(url);
        }

        /// <summary>
        /// Creates a new Parcel
        /// </summary>
        /// <param name="model">The Parcel to be created on the server</param>
        /// <returns>A fully populated Parcel, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#parcels"/>
        public Parcel CreateParcel(Parcel model)
        {
            return Execute<Parcel>(model, EasyPostUrls.PARCELS);
        }

        /// <summary>
        /// Retrieves an Parcel by Id
        /// </summary>
        /// <param name="parcelId">The Id of the Parcel to retrieve</param>
        /// <returns>The requested Parcel</returns>
        /// <seealso cref="http://www.easypost.com/docs#parcels"/>
        public Parcel GetParcel(string parcelId)
        {
            var url = string.Format(EasyPostUrls.PARCEL, parcelId);
            return Execute<Parcel>(url);
        }

        /// <summary>
        /// Query for all Parcels
        /// </summary>
        /// <returns>A list of all Parcels</returns>
        /// <seealso cref="http://www.easypost.com/docs#parcels"/>
        public List<Parcel> GetParcels()
        {
            return Execute<List<Parcel>>(EasyPostUrls.PARCELS);
        }

        /// <summary>
        /// Creates a new Shipment
        /// </summary>
        /// <param name="model">The Shipment to be created on the server</param>
        /// <returns>A fully populated Shipment, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#shipments"/>
        public Shipment CreateShipment(Shipment model)
        {
            return Execute<Shipment>(model, EasyPostUrls.SHIPMENTS);
        }

        /// <summary>
        /// Retrieves an Shipment by Id
        /// </summary>
        /// <param name="shipmentId">The Id of the Shipment to retrieve</param>
        /// <returns>The requested Shipment</returns>
        /// <seealso cref="http://www.easypost.com/docs#shipments"/>
        public Shipment GetShipment(string shipmentId)
        {
            var url = string.Format(EasyPostUrls.SHIPMENT, shipmentId);
            return Execute<Shipment>(url);
        }

        /// <summary>
        /// Retreives available rates for a Shipment by Id
        /// </summary>
        /// <param name="shipmentId">The Id of the Shipment to look up rates for</param>
        /// <returns>A list of available rates for the Shipment</returns>
        /// <seealso cref="http://www.easypost.com/docs#shipments"/>
        public List<CarrierRate> GetShipmentRates(string shipmentId)
        {
            var url = string.Format(EasyPostUrls.SHIPMENT_RATES, shipmentId);
            var response = Execute<ShipmentRatesResponse>(url);
            return response.Rates;
        }

        /// <summary>
        /// Purchase a PostageLabel for a given Shipment and CarrierRate
        /// Warning: This costs money and will only work with a LIVE API KEY
        /// </summary>
        /// <param name="shipmentId">The Id of the Shipment to buy a PostageLabel for</param>
        /// <param name="rate">The CarrierRate, selected from an available rate for the Shipment</param>
        /// <returns>The created PostageLabel</returns>
        /// <seealso cref="http://www.easypost.com/docs#shipments"/>
        public PostageLabel BuyPostageLabel(string shipmentId, CarrierRate rate)
        {
            var url = string.Format(EasyPostUrls.SHIPMENT_BUY, shipmentId);
            var response = Execute<BuyPostageLabelResponse>(rate, url);
            return response.PostageLabel;
        }

        /// <summary>
        /// Creates a new CustomsItem
        /// </summary>
        /// <param name="model">The CustomsItem to be created on the server</param>
        /// <returns>A fully populated CustomsItem, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        public CustomsItem CreateCustomsItem(CustomsItem model)
        {
            return Execute<CustomsItem>(model, EasyPostUrls.CUSTOM_ITEMS);
        }

        /// <summary>
        /// Retrieves an CustomsItem by Id
        /// </summary>
        /// <param name="customsItemId">The Id of the CustomsItem to retrieve</param>
        /// <returns>The requested CustomsItem</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        public CustomsItem GetCustomsItem(string customsItemId)
        {
            var url = string.Format(EasyPostUrls.CUSTOM_ITEM, customsItemId);
            return Execute<CustomsItem>(url);
        }

        /// <summary>
        /// Query for all CustomsItems
        /// </summary>
        /// <returns>A list of all CustomsItems</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        public List<CustomsItem> GetCustomsItems()
        {
            return Execute<List<CustomsItem>>(EasyPostUrls.CUSTOM_ITEMS);
        }

        /// <summary>
        /// Creates a new CustomsInfo
        /// </summary>
        /// <param name="model">The CustomsInfo to be created on the server</param>
        /// <returns>A fully populated CustomsInfo, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        public CustomsInfo CreateCustomsInfo(CustomsInfo model)
        {
            return Execute<CustomsInfo>(model, EasyPostUrls.CUSTOM_INFOS);
        }

        /// <summary>
        /// Retrieves an CustomsInfo by Id
        /// </summary>
        /// <param name="customsInfoId">The Id of the CustomsInfo to retrieve</param>
        /// <returns>The requested CustomsInfo</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        public CustomsInfo GetCustomsInfo(string customsInfoId)
        {
            var url = string.Format(EasyPostUrls.CUSTOM_INFO, customsInfoId);
            return Execute<CustomsInfo>(url);
        }

        /// <summary>
        /// Query for all CustomsInfos
        /// </summary>
        /// <returns>A list of all CustomsInfos</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        public List<CustomsInfo> GetCustomsInfos()
        {
            return Execute<List<CustomsInfo>>(EasyPostUrls.CUSTOM_INFOS);
        }

        /// <summary>
        /// Attempts to create a set of Refunds based on tracking codes
        /// </summary>
        /// <param name="model">The RefundRequest used to create multiple refunds</param>
        /// <returns>A list of Refunds, one for each TrackingCode</returns>
        /// <seealso cref="http://www.easypost.com/docs#refunds"/>
        public List<Refund> CreateRefund(RefundRequest model)
        {
            return Execute<List<Refund>>(model, EasyPostUrls.REFUNDS);
        }

        /// <summary>
        /// Retrieves an Refund by Id
        /// </summary>
        /// <param name="refundId">The Id of the Refund to retrieve</param>
        /// <returns>The requested Refund</returns>
        /// <seealso cref="http://www.easypost.com/docs#refunds"/>
        public Refund GetRefund(string refundId)
        {
            var url = string.Format(EasyPostUrls.REFUND, refundId);
            return Execute<Refund>(url);
        }

        /// <summary>
        /// Query for all Refunds
        /// </summary>
        /// <returns>A list of all Refunds</returns>
        /// <seealso cref="http://www.easypost.com/docs#refunds"/>
        public List<Refund> GetRefunds()
        {
            return Execute<List<Refund>>(EasyPostUrls.REFUNDS);
        }

        /// <summary>
        /// Creates a new ScanForm
        /// </summary>
        /// <param name="model">The ScanForm to be created on the server</param>
        /// <returns>A fully populated ScanForm, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#scan-forms"/>
        public ScanForm CreateScanForm(ScanForm model)
        {
            return Execute<ScanForm>(model, EasyPostUrls.SCAN_FORMS);
        }

        /// <summary>
        /// Retrieves an ScanForm by Id
        /// </summary>
        /// <param name="scanFormId">The Id of the ScanForm to retrieve</param>
        /// <returns>The requested ScanForm</returns>
        /// <seealso cref="http://www.easypost.com/docs#scan-forms"/>
        public ScanForm GetScanForm(string scanFormId)
        {
            var url = string.Format(EasyPostUrls.SCAN_FORM, scanFormId);
            return Execute<ScanForm>(url);
        }

        /// <summary>
        /// Query for all ScanForms
        /// </summary>
        /// <returns>A list of all ScanForms</returns>
        /// <seealso cref="http://www.easypost.com/docs#scan-forms"/>
        public List<ScanForm> GetScanForms()
        {
            return Execute<List<ScanForm>>(EasyPostUrls.SCAN_FORMS);
        }

        #region helpers

        private T Execute<T>(IEncodable model, string apiUri)
        {
            var content = model.AsFormUrlEncodedContent();
            var responseMessage = _client.PostAsync(apiUri, content).Result;
            responseMessage.EnsureSuccessStatusCode();
            var response = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(response);
        }

        private T Execute<T>(string apiUri)
        {
            var responseMessage = _client.GetAsync(apiUri).Result;
            responseMessage.EnsureSuccessStatusCode();
            var response = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(response);
        }

        private static class EasyPostUrls
        {
            public const string BASE_URL = "https://api.easypost.com/v2/";
            public const string ADDRESSES = "addresses";
            public const string ADDRESS = "addresses/{0}";
            public const string ADDRESS_VERIFY = "addresses/{0}/verify";
            public const string PARCELS = "parcels";
            public const string PARCEL = "parcels/{0}";
            public const string SHIPMENTS = "shipments";
            public const string SHIPMENT = "shipments/{0}";
            public const string SHIPMENT_RATES = "shipments/{0}/rates";
            public const string SHIPMENT_BUY = "shipments/{0}/buy";
            public const string CUSTOM_ITEMS = "customs_items";
            public const string CUSTOM_ITEM = "customs_items/{0}";
            public const string CUSTOM_INFOS = "customs_infos";
            public const string CUSTOM_INFO = "customs_infos/{0}";
            public const string REFUNDS = "refunds";
            public const string REFUND = "refunds/{0}";
            //public const string BATCHES = "batches";
            //public const string BATCH = "batches/{0}";
            public const string SCAN_FORMS = "scan_forms";
            public const string SCAN_FORM = "scan_forms/{0}";
        }

        #endregion
    }
}
