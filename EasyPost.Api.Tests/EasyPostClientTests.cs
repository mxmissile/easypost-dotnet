using System.Collections.Generic;
using System.Linq;
using EasyPost.Model;
using NUnit.Framework;

namespace EasyPost.Api.Tests
{
    [TestFixture]
    public class EasyPostClientTests
    {
        private IEasyPostClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new EasyPostClient("TEST_API_KEY");
        }

        [Test]
        public void TestAddresses()
        {
            var fromAddress = _client.CreateAddress(new Address
            {
                Name = "EasyPost",
                Street1 = "2135 Sacramento St",
                City = "San Francisco",
                State = "CA",
                Zip = "94109",
                Email = "support@easypost.com",
            });
            Assert.IsNotNull(fromAddress.Id);

            var verified = _client.VerifyAddress(fromAddress.Id);
            Assert.IsNotNull(verified.Address.Id);

            var sameAsFrom = _client.GetAddress(fromAddress.Id);
            Assert.AreEqual(fromAddress.Id, sameAsFrom.Id);

            var allAddresses = _client.GetAddresses();
            var shouldExist = allAddresses.SingleOrDefault(x => string.Equals(x.Id, fromAddress.Id));
            Assert.IsNotNull(shouldExist);
        }

        [Test]
        public void TestParcels()
        {
            var customParcel = _client.CreateParcel(new Parcel
            {
                LengthInches = 6,
                WidthInches = 6,
                HeightInches = 4,
                WeightOunces = 13,
            });
            Assert.IsNotNull(customParcel.Id);

            var parcel = _client.CreateParcel(new Parcel
            {
                PredefinedPackage = ParcelType.FedExEnvelope,
                WeightOunces = 2,
            });
            Assert.IsNotNull(parcel.Id);

            var sameAsCustomParcel = _client.GetParcel(customParcel.Id);
            Assert.AreEqual(customParcel.Id, sameAsCustomParcel.Id);

            var allParcels = _client.GetParcels();
            var shouldExist = allParcels.SingleOrDefault(x => string.Equals(x.Id, customParcel.Id));
            Assert.IsNotNull(shouldExist);
        }

        [Test]
        public void TestShipments()
        {
            var addresses = _client.GetAddresses();
            var parcels = _client.GetParcels();

            var shipment = _client.CreateShipment(new Shipment
            {
                Parcel = parcels[0],
                FromAddress = addresses[0],
                ToAddress = addresses[1],
            });
            Assert.IsNotNull(shipment.Id);
            
            var sameAsShipment = _client.GetShipment(shipment.Id);
            Assert.AreEqual(shipment.Id, sameAsShipment.Id);

            var rates = _client.GetShipmentRates(shipment.Id);
            Assert.AreEqual(rates.Count, shipment.Rates.Count);

            // TODO this is failing with 400 (Bad Request)
            // maybe you need to pay for a shipment before setting insurance?
            /*
            var insurance = new Insurance {Amount = 80.5};
            var insuredShipment = _client.InsureShipment(shipment.Id, insurance);
            Assert.AreEqual(shipment.Id, insuredShipment.Id);
            Assert.AreEqual(shipment.Insurance, insurance.Amount);
            */
        }

        [Test]
        public void TestBuyingLabel()
        {
            // TODO test _client.BuyPostageLabel() without actually buying
        }

        [Test]
        public void TestCustoms()
        {
            var customsItem = _client.CreateCustomsItem(new CustomsItem
            {
                Description = "testing",
                Quantity = 1,
                Value = 100,
                WeightOunces = 16,
                HsTariffNumber = "123456",
                OriginCountry = "US",
            });
            Assert.IsNotNull(customsItem.Id);

            var sameAsCustomsItem = _client.GetCustomsItem(customsItem.Id);
            Assert.AreEqual(sameAsCustomsItem.Id, customsItem.Id);

            var allCustomsItems = _client.GetCustomsItems();
            var shouldExistItem = allCustomsItems.SingleOrDefault(x => string.Equals(x.Id, customsItem.Id));
            Assert.IsNotNull(shouldExistItem);

            var customsInfo = _client.CreateCustomsInfo(new CustomsInfo
            {
                CustomsCertify = true,
                CustomsSigner = "Jonathan Calhoun",
                ContentsType = "merchandise",
                ContentsExplanation = " ",
                RestrictionType = "none",
                EelPfc = "NOEEI 30.37(a)",
                CustomsItems = new List<CustomsItem> {customsItem},
            });
            Assert.IsNotNull(customsInfo.Id);

            var sameAsCustomsInfo = _client.GetCustomsInfo(customsInfo.Id);
            Assert.AreEqual(sameAsCustomsInfo.Id, customsInfo.Id);

            var allCustomsInfos = _client.GetCustomsInfos();
            var shouldExistInfo = allCustomsInfos.SingleOrDefault(x => string.Equals(x.Id, customsInfo.Id));
            Assert.IsNotNull(shouldExistInfo);
        }

        [Test]
        public void TestRefunds()
        {
            var refunds = _client.CreateRefund(new RefundRequest
            {
                Carrier = "USPS",
                TrackingCodes = new List<string> { "CJ123456789US", "LN123456789US" }
            });
            Assert.IsTrue(refunds.Count == 2);

            // one of the two should exist
            var refundId = string.IsNullOrEmpty(refunds[0].Id) ? refunds[1].Id : refunds[0].Id;

            Assert.IsNotNull(refundId);

            var sameAsRefund = _client.GetRefund(refundId);
            Assert.AreEqual(refundId, sameAsRefund.Id);

            var allRefunds = _client.GetRefunds();
            var shouldExist = allRefunds.SingleOrDefault(x => string.Equals(x.Id, refundId));
            Assert.IsNotNull(shouldExist);
        }

        [Test]
        public void TestBatches()
        {
            var addresses = _client.GetAddresses();
            var parcels = _client.GetParcels();

            var batch = _client.CreateBatch(new Batch
            {
                Shipments = new List<BatchShipment>
                {
                    new BatchShipment
                    {
                        Parcel = parcels[0],
                        FromAddress = addresses[0],
                        ToAddress = addresses[1],
                        Carrier = "USPS",
                        Service = "Priority",
                    },
                    new BatchShipment
                    {
                        Parcel = parcels[1],
                        FromAddress = addresses[1],
                        ToAddress = addresses[2],
                    },
                }
            });
            Assert.IsNotNull(batch.Id);

            // it takes a few minutes for the shipments to be added :(
            // so we can't verify batch.Status.CreatedCount == 2 here

            var sameAsBatch = _client.GetBatch(batch.Id);
            Assert.AreEqual(batch.Id, sameAsBatch.Id);

            var allBatches = _client.GetBatches();
            var shouldExist = allBatches.SingleOrDefault(x => string.Equals(x.Id, batch.Id));
            Assert.IsNotNull(shouldExist);
        }

        [Test]
        public void TestBuyingBatch()
        {
            // TODO test _client.BuyBatch() and _client.GenerateBatchLabel() without actually buying
        }

        [Test]
        public void TestScanForms()
        {
            var scanForm = _client.CreateScanForm(new ScanForm
            {
                TrackingCodes = new List<string> { "123456", "123455", "123454" },
                Address = new Address
                {
                    Name = "EasyPost",
                    Street1 = "2135 Sacramento St",
                    City = "San Francisco",
                    State = "CA",
                    Zip = "94109",
                    Email = "support@easypost.com",
                },
            });
            Assert.IsNotNull(scanForm.Id);

            var sameAsScanForm = _client.GetScanForm(scanForm.Id);
            Assert.AreEqual(scanForm.Id, sameAsScanForm.Id);

            var allScanForms = _client.GetScanForms();
            var shouldExist = allScanForms.SingleOrDefault(x => string.Equals(x.Id, scanForm.Id));
            Assert.IsNotNull(shouldExist);
        }
    }
}
