using System;
using System.Configuration;
using Easypost;
using Machine.Specifications;

namespace tests
{
    public class EasyPostContext
    {
        /// <summary>
        /// Please visit https://www.geteasypost.com/ for a Api key
        /// </summary>
        public static string ApiKey = "ENTER_API_KEY";

        private Establish context = () =>
            {
                if (ApiKey.Equals("ENTER_API_KEY") || String.IsNullOrWhiteSpace(ApiKey))
                    throw new ConfigurationErrorsException("PLEASE SET YOUR API KEY");
                EasyPostClient = new EasyPostClient(ApiKey);
            };

        protected static EasyPostClient EasyPostClient;

    }

    [Subject(typeof(EasyPostClient), "Retrieving Postage")]
    public class when_retrieving_postage : EasyPostContext
    {
        private Because of = () =>
            response = EasyPostClient.ListPostage();

        It should_return_at_least_one_item = () =>
            response.Postages.Count.ShouldBeGreaterThan(0);

        protected static EasyPostPostageList response;
    }

    [Subject(typeof(EasyPostClient), "Retrieving Postage")]
    public class when_retrieving_postage_list : EasyPostContext
    {
        private Because of = () =>
            response = EasyPostClient.GetPostage(new ParcelFileName() { label_file_name = "test.png" });

        It should_return_tracking_code = () =>
            response.Tracking_Code.ShouldNotBeEmpty();

        protected static PostageLabel response;
    }

    [Subject(typeof(EasyPostClient), "Buying Postage")]
    public class when_purchasing_predefined_package_postage : EasyPostContext
    {
        private Because of = () =>
            response = EasyPostClient.BuyPostage(TestData.PredefinedPostagePurchase);

        It should_return_tracking_code = () =>
            response.Tracking_Code.ShouldNotBeEmpty();

        protected static PostageLabel response;
    }

    [Subject(typeof(EasyPostClient), "Buying Postage")]
    public class when_purchasing_custom_package_postage : EasyPostContext
    {
        private Because of = () =>
            response = EasyPostClient.BuyPostage(TestData.CustomPostagePurchase);

        It should_return_tracking_code = () =>
            response.Tracking_Code.ShouldNotBeEmpty();

        protected static PostageLabel response;
    }

    [Subject(typeof(EasyPostClient), "Calculating Postage")]
    public class when_calculating_predefined_package_postage : EasyPostContext
    {
        private Because of = () =>
            response = EasyPostClient.CalculatePostage(new PostageRate { Parcel = TestData.PredefinedParcel, Zip = new Zip { To = "94107", From = "59847" } });

        It should_return_at_least_one_rate = () =>
            response.Rates.Length.ShouldBeGreaterThan(0);

        protected static ShipmentRatesResponse response;
    }

    [Subject(typeof(EasyPostClient), "Calculating Postage")]
    public class when_calculating_custom_package_postage : EasyPostContext
    {
        private Because of = () =>
            response = EasyPostClient.CalculatePostage(new PostageRate { Parcel = TestData.CustomParcel, Zip = new Zip { To = "94107", From = "59847" } });

        It should_return_at_least_one_rate = () =>
            response.Rates.Length.ShouldBeGreaterThan(0);

        protected static ShipmentRatesResponse response;
    }

    [Subject(typeof(EasyPostClient), "Address Verification")]
    public class when_validating_complete_address : EasyPostContext
    {
        private Because of = () =>
            response = EasyPostClient.VerifyAddress(TestData.InputAddress);

        It should_return_same_address = () =>
            response.ShouldEqual<VerifyAddressResponse>(new VerifyAddressResponse { Address = TestData.OutputAddress });

        protected static VerifyAddressResponse response;
    }

    [Subject(typeof(EasyPostClient), "Address Verification")]
    public class when_validating_partial_address : EasyPostContext
    {
        private Because of = () =>
            {
                removedZip = TestData.InputAddress.Zip;
                TestData.InputAddress.Zip = null;
                response = EasyPostClient.VerifyAddress(TestData.InputAddress);
            };

        private It should_return_complete_address = () =>
            {
                TestData.InputAddress.Zip = removedZip;
                response.ShouldEqual<VerifyAddressResponse>(new VerifyAddressResponse { Address = TestData.OutputAddress });
            };

        protected static VerifyAddressResponse response;
        protected static string removedZip;
    }
}
