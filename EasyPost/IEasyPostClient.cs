using System.Collections.Generic;
using EasyPost.Model;

namespace EasyPost
{
    /// <summary>
    /// Wrapper for the EasyPost REST API.
    /// All methods throw an exception if a non-success status code comes back from EasyPost.
    /// </summary>
    /// <seealso cref="http://www.easypost.com/docs"/>
    public interface IEasyPostClient
    {
        /// <summary>
        /// Creates a new Address
        /// </summary>
        /// <param name="model">The Address to be created on the server</param>
        /// <returns>A fully populated Address, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#addresses"/>
        Address CreateAddress(Address model);
        
        /// <summary>
        /// Retrieves an Address by Id
        /// </summary>
        /// <param name="addressId">The Id of the Address to retrieve</param>
        /// <returns>The requested Address</returns>
        /// <seealso cref="http://www.easypost.com/docs#addresses"/>
        Address GetAddress(string addressId);
        
        /// <summary>
        /// Query for all Addresses
        /// </summary>
        /// <returns>A list of all Addresses</returns>
        /// <seealso cref="http://www.easypost.com/docs#addresses"/>
        List<Address> GetAddresses();
        
        /// <summary>
        /// Verifies an Address is valid for shipment
        /// </summary>
        /// <param name="addressId">The Id of the Address to validate</param>
        /// <returns>The closest verified Address, and a message if the address is valid but missing some data</returns>
        /// <seealso cref="http://www.easypost.com/docs#addresses"/>
        VerifiedAddress VerifyAddress(string addressId);
        
        /// <summary>
        /// Creates a new Parcel
        /// </summary>
        /// <param name="model">The Parcel to be created on the server</param>
        /// <returns>A fully populated Parcel, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#parcels"/>
        Parcel CreateParcel(Parcel model);

        /// <summary>
        /// Retrieves an Parcel by Id
        /// </summary>
        /// <param name="parcelId">The Id of the Parcel to retrieve</param>
        /// <returns>The requested Parcel</returns>
        /// <seealso cref="http://www.easypost.com/docs#parcels"/>
        Parcel GetParcel(string parcelId);

        /// <summary>
        /// Query for all Parcels
        /// </summary>
        /// <returns>A list of all Parcels</returns>
        /// <seealso cref="http://www.easypost.com/docs#parcels"/>
        List<Parcel> GetParcels();
        
        /// <summary>
        /// Creates a new Shipment
        /// </summary>
        /// <param name="model">The Shipment to be created on the server</param>
        /// <returns>A fully populated Shipment, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#shipments"/>
        Shipment CreateShipment(Shipment model);

        /// <summary>
        /// Retrieves an Shipment by Id
        /// </summary>
        /// <param name="shipmentId">The Id of the Shipment to retrieve</param>
        /// <returns>The requested Shipment</returns>
        /// <seealso cref="http://www.easypost.com/docs#shipments"/>
        Shipment GetShipment(string shipmentId);
        
        /// <summary>
        /// Retreives available rates for a Shipment by Id
        /// </summary>
        /// <param name="shipmentId">The Id of the Shipment to look up rates for</param>
        /// <returns>A list of available rates for the Shipment</returns>
        /// <seealso cref="http://www.easypost.com/docs#shipments"/>
        List<CarrierRate> GetShipmentRates(string shipmentId);

        /// <summary>
        /// Insure your shipment by specifing its value
        /// </summary>
        /// <param name="shipmentId">The Id of the Shipment to insure</param>
        /// <param name="amount">The value of the Shipment to insure in USD</param>
        /// <returns>The updated Shipment</returns>
        /// <seealso cref="http://www.easypost.com/docs#shipments"/>
        Shipment InsureShipment(string shipmentId, double amount);

        /// <summary>
        /// Purchase a PostageLabel for a given Shipment and CarrierRate
        /// Warning: This costs money and will only work with a LIVE API KEY
        /// </summary>
        /// <param name="shipmentId">The Id of the Shipment to buy a PostageLabel for</param>
        /// <param name="rate">The CarrierRate, selected from an available rate for the Shipment</param>
        /// <returns>The created PostageLabel</returns>
        /// <seealso cref="http://www.easypost.com/docs#shipments"/>
        PostageLabel BuyPostageLabel(string shipmentId, CarrierRate rate);
        
        /// <summary>
        /// Creates a new CustomsItem
        /// </summary>
        /// <param name="model">The CustomsItem to be created on the server</param>
        /// <returns>A fully populated CustomsItem, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        CustomsItem CreateCustomsItem(CustomsItem model);

        /// <summary>
        /// Retrieves an CustomsItem by Id
        /// </summary>
        /// <param name="customsItemId">The Id of the CustomsItem to retrieve</param>
        /// <returns>The requested CustomsItem</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        CustomsItem GetCustomsItem(string customsItemId);

        /// <summary>
        /// Query for all CustomsItems
        /// </summary>
        /// <returns>A list of all CustomsItems</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        List<CustomsItem> GetCustomsItems();
        
        /// <summary>
        /// Creates a new CustomsInfo
        /// </summary>
        /// <param name="model">The CustomsInfo to be created on the server</param>
        /// <returns>A fully populated CustomsInfo, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        CustomsInfo CreateCustomsInfo(CustomsInfo model);

        /// <summary>
        /// Retrieves an CustomsInfo by Id
        /// </summary>
        /// <param name="customsInfoId">The Id of the CustomsInfo to retrieve</param>
        /// <returns>The requested CustomsInfo</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        CustomsInfo GetCustomsInfo(string customsInfoId);

        /// <summary>
        /// Query for all CustomsInfos
        /// </summary>
        /// <returns>A list of all CustomsInfos</returns>
        /// <seealso cref="http://www.easypost.com/docs#customs"/>
        List<CustomsInfo> GetCustomsInfos();

        /// <summary>
        /// Attempts to create a set of Refunds based on tracking codes
        /// </summary>
        /// <param name="model">The RefundRequest used to create multiple refunds</param>
        /// <returns>A list of Refunds, one for each TrackingCode</returns>
        /// <seealso cref="http://www.easypost.com/docs#refunds"/>
        List<Refund> CreateRefund(RefundRequest model);

        /// <summary>
        /// Retrieves an Refund by Id
        /// </summary>
        /// <param name="refundId">The Id of the Refund to retrieve</param>
        /// <returns>The requested Refund</returns>
        /// <seealso cref="http://www.easypost.com/docs#refunds"/>
        Refund GetRefund(string refundId);

        /// <summary>
        /// Query for all Refunds
        /// </summary>
        /// <returns>A list of all Refunds</returns>
        /// <seealso cref="http://www.easypost.com/docs#refunds"/>
        List<Refund> GetRefunds();

        /// <summary>
        /// Create a new Batch, specifying the carrier and service for each shipment if you'd like.
        /// </summary>
        /// <param name="model">The Batch to create</param>
        /// <returns>A fully populated Batch, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#batches"/>
        Batch CreateBatch(Batch model);

        /// <summary>
        /// Retrieves an Batch by Id
        /// </summary>
        /// <param name="batchId">The Id of the Batch to retrieve</param>
        /// <returns>The requested Batch</returns>
        /// <seealso cref="http://www.easypost.com/docs#batches"/>
        Batch GetBatch(string batchId);

        /// <summary>
        /// Query for all Batches
        /// </summary>
        /// <returns>A list of all Batches</returns>
        /// <seealso cref="http://www.easypost.com/docs#batches"/>
        List<Batch> GetBatches();

        /// <summary>
        /// Buy an existing Batch or create and buy a new Batch, specifying the carrier and service for each shipment if you'd like.
        /// After the method is called we will automatically purchase the requested shipping labels and update the batch status accordingly. 
        /// Postage labels are purchased and created asyncronously, so polling the batch object is recommended to determine its updated status.
        /// Warning: This costs money and will only work with a LIVE API KEY
        /// </summary>
        /// <param name="model">The Batch to purchase</param>
        /// <returns>The updated Batch, which may have the label url</returns>
        /// <seealso cref="http://www.easypost.com/docs#batches"/>
        Batch BuyBatch(Batch model);

        /// <summary>
        /// Retrieve the postage labels for the entire batch in one file, in 'pdf' or 'epl2' format.
        /// This can only be done once all shipments in the batch are in 'postage_purchased' status. 
        /// Batch label generation is asyncronous, so polling the batch object for the presense of a non-empty label_url is recommended.
        /// </summary>
        /// <param name="batchId">The Id of the Batch</param>
        /// <param name="labelFormat">The BatchLabelFormat to generate</param>
        /// <returns>The updated Batch</returns>
        /// <seealso cref="http://www.easypost.com/docs#batches"/>
        Batch GenerateBatchLabel(string batchId, BatchLabelFormat labelFormat);

        /// <summary>
        /// Creates a new ScanForm
        /// </summary>
        /// <param name="model">The ScanForm to be created on the server</param>
        /// <returns>A fully populated ScanForm, included the new Id</returns>
        /// <seealso cref="http://www.easypost.com/docs#scan-forms"/>
        ScanForm CreateScanForm(ScanForm model);

        /// <summary>
        /// Retrieves an ScanForm by Id
        /// </summary>
        /// <param name="scanFormId">The Id of the ScanForm to retrieve</param>
        /// <returns>The requested ScanForm</returns>
        /// <seealso cref="http://www.easypost.com/docs#scan-forms"/>
        ScanForm GetScanForm(string scanFormId);

        /// <summary>
        /// Query for all ScanForms
        /// </summary>
        /// <returns>A list of all ScanForms</returns>
        /// <seealso cref="http://www.easypost.com/docs#scan-forms"/>
        List<ScanForm> GetScanForms();
    }
}