using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Easypost
{
    public interface IEasyPostClient
    {
        Address CreateAddress(Address model);
        Address GetAddress(string addressId);
        VerifyAddressResponse VerifyAddress(string addressId);
        
        Parcel CreateParcel(Parcel model);
        Parcel GetParcel(string parcelId);
        
        Shipment CreateShipment(Shipment model);
        Shipment GetShipment(string shipmentId);
        ShipmentRatesResponse GetShipmentRates(string shipmentId);
        BuyPostageLabelResponse BuyPostageLabel(string shipmentId);
        
        CustomsItem CreateCustomsItem(CustomsItem model);
        CustomsItem GetCustomsItem(string customsItemId);
        CustomsInfo CreateCustomsInfo(CustomsInfo model);
        CustomsInfo GetCustomsInfo(string customsInfoId);
        
        ScanForm CreateScanForm(ScanForm model);
        ScanForm GetScanForm(string scanFormId);
        
        Refund CreateRefund(RefundRequest model);
        Refund GetRefund(string refundId);
    }

    public class EasyPostClient : IEasyPostClient
    {
        private static class EasyPostUrls
        {
            public const string BASE_URL = "https://api.easypost.com/v2/";
            public const string ADDRESSES = "/addresses";
            public const string ADDRESS = "/addresses/{0}";
            public const string ADDRESS_VERIFY = "/addresses/{0}/verify";
            public const string PARCELS = "/parcels";
            public const string PARCEL = "/parcels/{0}";
            public const string SHIPMENTS = "/shipments";
            public const string SHIPMENT = "/shipments/{0}";
            public const string SHIPMENT_RATES = "/shipments/{0}/rates";
            public const string SHIPMENT_BUY = "/shipments/{0}/buy";
            public const string CUSTOM_ITEMS = "/customs_items";
            public const string CUSTOM_ITEM = "/customs_items/{0}";
            public const string CUSTOM_INFOS = "/customs_infos";
            public const string CUSTOM_INFO = "/customs_infos/{0}";
            public const string REFUNDS = "/refunds";
            public const string REFUND = "/refunds/{0}";
            public const string BATCHES = "/batches";
            public const string BATCH = "/batches/{0}";
            public const string SCAN_FORMS = "/scan_forms";
            public const string SCAN_FORM = "/scan_forms/{0}";
        }

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

        public Address CreateAddress(Address model)
        {
            return Execute<Address, Address>(model, EasyPostUrls.ADDRESSES);
        }

        public Address GetAddress(string addressId)
        {
            var url = string.Format(EasyPostUrls.ADDRESS, addressId);
            return Execute<Address>(url);
        }

        public VerifyAddressResponse VerifyAddress(string addressId)
        {
            var url = string.Format(EasyPostUrls.ADDRESS_VERIFY, addressId);
            return Execute<VerifyAddressResponse>(url);
        }

        public Parcel CreateParcel(Parcel model)
        {
            return Execute<Parcel, Parcel>(model, EasyPostUrls.PARCELS);
        }

        public Parcel GetParcel(string parcelId)
        {
            var url = string.Format(EasyPostUrls.PARCEL, parcelId);
            return Execute<Parcel>(url);
        }

        public Shipment CreateShipment(Shipment model)
        {
            return Execute<Shipment, Shipment>(model, EasyPostUrls.SHIPMENTS);
        }

        public Shipment GetShipment(string shipmentId)
        {
            var url = string.Format(EasyPostUrls.SHIPMENT, shipmentId);
            return Execute<Shipment>(url);
        }

        public ShipmentRatesResponse GetShipmentRates(string shipmentId)
        {
            var url = string.Format(EasyPostUrls.SHIPMENT_RATES, shipmentId);
            return Execute<ShipmentRatesResponse>(url);                
        }

        public BuyPostageLabelResponse BuyPostageLabel(string shipmentId)
        {
            var url = string.Format(EasyPostUrls.SHIPMENT_BUY, shipmentId);
            return Execute<BuyPostageLabelResponse>(url);
        }

        public CustomsItem CreateCustomsItem(CustomsItem model)
        {
            return Execute<CustomsItem, CustomsItem>(model, EasyPostUrls.CUSTOM_ITEMS);
        }

        public CustomsItem GetCustomsItem(string customsItemId)
        {
            var url = string.Format(EasyPostUrls.CUSTOM_ITEM, customsItemId);
            return Execute<CustomsItem>(url);
        }

        public CustomsInfo CreateCustomsInfo(CustomsInfo model)
        {
            return Execute<CustomsInfo, CustomsInfo>(model, EasyPostUrls.CUSTOM_INFOS);
        }

        public CustomsInfo GetCustomsInfo(string customsInfoId)
        {
            var url = string.Format(EasyPostUrls.CUSTOM_INFO, customsInfoId);
            return Execute<CustomsInfo>(url);
        }

        public ScanForm CreateScanForm(ScanForm model)
        {
            return Execute<ScanForm, ScanForm>(model, EasyPostUrls.SCAN_FORMS);
        }

        public ScanForm GetScanForm(string scanFormId)
        {
            var url = string.Format(EasyPostUrls.SCAN_FORM, scanFormId);
            return Execute<ScanForm>(url);
        }

        public Refund CreateRefund(RefundRequest model)
        {
            return Execute<Refund, RefundRequest>(model, EasyPostUrls.REFUNDS);
        }

        public Refund GetRefund(string refundId)
        {
            var url = string.Format(EasyPostUrls.REFUND, refundId);
            return Execute<Refund>(url);
        }

        private T Execute<T, TR>(TR model, string apiUri) where TR : IEncodable
        {
            var content = model.AsFormUrlEncodedContent();
            var httpResponseMessage = _client.PostAsync(apiUri, content).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(response);
        }

        private T Execute<T>(string apiUri)
        {
            var httpResponseMessage = _client.PostAsync(apiUri, null).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
