namespace EasyPost.Model
{
    /// <summary>
    /// Predefined package sizes for USPS, UPS and FedEx
    /// </summary>
    public enum ParcelType
    {
        // USPS
        Card,
        Letter,
        Flat,
        Parcel,
        LargeParcel,
        IrregularParcel,
        FlatRateEnvelope,
        FlatRateLegalEnvelope,
        FlatRatePaddedEnvelope,
        FlatRateGiftCardEnvelope,
        FlatRateWindowEnvelope,
        FlatRateCardboardEnvelope,
        SmallFlatRateEnvelope,
        SmallFlatRateBox,
        MediumFlatRateBox,
        LargeFlatRateBox,
        RegionalRateBoxA,
        RegionalRateBoxB,
        RegionalRateBoxC,
        LargeFlatRateBoardGameBox,

        // UPS
        UPSLetter,
        UPSExpressBox,
        UPS25kgBox,
        UPS10kgBox,
        Tube,
        Pak,
        Pallet,
        SmallExpressBox,
        MediumExpressBox,
        LargeExpressBox,

        // FedEx
        FedExEnvelope,
        FedExBox,
        FedExPak,
        FedExTube,
        FedEx10kgBox,
        FedEx25kg,
    }
}