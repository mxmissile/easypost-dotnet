namespace Easypost
{
    public class ParcelType
    {
        public ParcelType(UspsParcelType type)
        {
            Usps = type;
        }

        public ParcelType(UpsParcelType type)
        {
            Ups = type;
        }

        public ParcelType(FedExParcelType type)
        {
            FedEx = type;
        }

        public UspsParcelType? Usps { get; private set; }
        public UpsParcelType? Ups { get; private set; }
        public FedExParcelType? FedEx { get; private set; }

        public override string ToString()
        {
            if (Usps != null)
            {
                return Usps.ToString();
            }

            if (Ups != null)
            {
                return Ups.ToString();
            }

            if (FedEx != null)
            {
                return FedEx.ToString();
            }

            return string.Empty;
        }
    }

    public enum UspsParcelType
    {
        Unknown,
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
    }

    public enum UpsParcelType
    {
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
    }

    public enum FedExParcelType
    {
        FedExEnvelope,
        FedExBox,
        FedExPak,
        FedExTube,
        FedEx10kgBox,
        FedEx25kg,
    }
}