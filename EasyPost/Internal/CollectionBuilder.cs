using System.Collections.Generic;
using System.Net.Http;
using EasyPost.Model;

namespace Easypost.Internal
{
    internal class CollectionBuilder
    {
        private readonly List<KeyValuePair<string, string>> _collection = new List<KeyValuePair<string, string>>();

        public CollectionBuilder AddRequired(KeyValuePair<string, string> kvp)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                _collection.Add(kvp);
            }

            return this;
        }

        public CollectionBuilder Add(KeyValuePair<string, string> kvp)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                _collection.Add(kvp);
            }

            return this;
        }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            return new FormUrlEncodedContent(_collection);
        }

        public CollectionBuilder AddAddress(string keyBase, Address addr)
        {
            if (!string.IsNullOrEmpty(addr.Id))
            {
                return Add("[id]".ToKvp(keyBase, addr.Id));
            }

            return AddRequired("[street1]".ToKvp(keyBase, addr.Street1))
                .AddRequired("[street2]".ToKvp(keyBase, addr.Street2))
                .AddRequired("[city]".ToKvp(keyBase, addr.City))
                .AddRequired("[state]".ToKvp(keyBase, addr.State))
                .AddRequired("[zip]".ToKvp(keyBase, addr.Zip))
                .Add("[country]".ToKvp(keyBase, addr.Country))
                .Add("[name]".ToKvp(keyBase, addr.Name))
                .Add("[company]".ToKvp(keyBase, addr.Company))
                .Add("[email]".ToKvp(keyBase, addr.Email))
                .Add("[phone]".ToKvp(keyBase, addr.Phone));
        }

        public CollectionBuilder AddParcel(string keyBase, Parcel parcel)
        {
            if (!string.IsNullOrEmpty(parcel.Id))
            {
                return Add("[id]".ToKvp(keyBase, parcel.Id));
            }

            if (parcel.PredefinedPackage == null)
            {
                AddRequired("[length]".ToKvp(keyBase, parcel.LengthInches));
                AddRequired("[width]".ToKvp(keyBase, parcel.WidthInches));
                AddRequired("[height]".ToKvp(keyBase, parcel.HeightInches));
            }
            else
            {
                Add("[predefined_package]".ToKvp(keyBase, parcel.PredefinedPackage.Value));
            }

            return AddRequired("[weight]".ToKvp(keyBase, parcel.WeightOunces));
        }

        public CollectionBuilder AddCustomsItem(string keyBase, CustomsItem item)
        {
            if (!string.IsNullOrEmpty(item.Id))
            {
                return Add("[id]".ToKvp(keyBase, item.Id));
            }

            return AddRequired("[description]".ToKvp(keyBase, item.Description))
                .AddRequired("[quantity]".ToKvp(keyBase, item.Quantity))
                .AddRequired("[weight]".ToKvp(keyBase, item.WeightOunces))
                .AddRequired("[value]".ToKvp(keyBase, item.Value.ToString()))
                .AddRequired("[hs_tariff_number]".ToKvp(keyBase, item.HsTariffNumber))
                .AddRequired("[origin_country]".ToKvp(keyBase, item.OriginCountry));
        }

        public CollectionBuilder AddScanForm(string keyBase, ScanForm form)
        {
            if (!string.IsNullOrEmpty(form.Id))
            {
                return Add("[id]".ToKvp(keyBase, form.Id));
            }

            return AddRequired("[tracking_codes]".ToKvp(keyBase, string.Join(",", form.TrackingCodes)))
                .AddAddress(keyBase + "[from_address]", form.Address);
        }

        public CollectionBuilder AddCustomsInfo(string keyBase, CustomsInfo info)
        {
            if (!string.IsNullOrEmpty(info.Id))
            {
                return Add("[id]".ToKvp(keyBase, info.Id));
            }

            AddRequired("[customs_certify]".ToKvp(keyBase, info.CustomsCertify.ToString()));
            AddRequired("[contents_type]".ToKvp(keyBase, info.ContentsType));
            AddRequired("[contents_explanation]".ToKvp(keyBase, info.ContentsExplanation));
            AddRequired("[restriction_type]".ToKvp(keyBase, info.RestrictionType));
            AddRequired("[eel_pfc]".ToKvp(keyBase, info.EelPfc));
            Add("[customs_signer]".ToKvp(keyBase, info.CustomsSigner));
            Add("[non_delivery_option]".ToKvp(keyBase, info.NonDeliveryOption));
            Add("[restriction_comments]".ToKvp(keyBase, info.RestrictionComments));

            if (info.CustomsItems != null)
            {
                for (var i = 0; i < info.CustomsItems.Count; i++)
                {
                    AddCustomsItem(string.Format("{0}[customs_items][{1}]", keyBase, i), info.CustomsItems[i]);
                }
            }

            return this;
        }

        public CollectionBuilder AddShipment(string keyBase, Shipment shipment)
        {
            if (!string.IsNullOrEmpty(shipment.Id))
            {
                return Add("[id]".ToKvp(keyBase, shipment.Id));
            }

            AddAddress(keyBase + "[from_address]", shipment.FromAddress);
            AddAddress(keyBase + "[to_address]", shipment.ToAddress);
            AddParcel(keyBase + "[parcel]", shipment.Parcel);
            Add("[reference]".ToKvp(keyBase, shipment.Reference));

            var optionsKeyBase = keyBase + "[options]";
            foreach (var option in shipment.Options)
            {
                Add(new KeyValuePair<string, string>(optionsKeyBase + option.Key, option.Value));
            }

            if (shipment.CustomsInfo != null)
            {
                AddCustomsInfo(keyBase + "[customs_info]", shipment.CustomsInfo);
            }

            if (shipment.ScanForm != null)
            {
                AddScanForm(keyBase + "[scan_form]", shipment.ScanForm);
            }

            return this;
        }
    }
}