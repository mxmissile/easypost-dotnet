easypost-net
============

Client library for accessing the EasyPost Shipping API via C#

## Buy a shipping label in 5 quick steps

#### Step 1 - Initialize client

    var easyPost = new EasyPostClient("YOUR_LIVE_API_KEY");

#### Step 2 - Create addresses
    
    var fromAddress = easyPost.CreateAddress(new Address
    {
        Name = "EasyPost",
        Street1 = "2135 Sacramento St",
        City = "San Francisco",
        State = "CA",
        Zip = "94109",
        Email = "support@easypost.com",
    });
    
    var toAddress = easyPost.CreateAddress(new Address
    {
        Company = "Cool Company Name",
        Street1 = "123 Main Street",
        City = "New York",
        State = "NY",
        Zip = "10001",
    });

#### Step 3 - Create parcel

    var customParcel = easyPost.CreateParcel(new Parcel
    {
        LengthInches = 6,
        WidthInches = 6,
        HeightInches = 4,
        WeightOunces = 13,
    });
    
#### Step 4 - Create shipment

    var shipment = new Shipment
    {
        FromAddress = fromAddress,
        ToAddress = toAddress,
        Parcel = customParcel,
    });
    shipment.Options.Add("delivery_confirmation", "SIGNATURE");
    shipment = easyPost.CreateShipment(shipment);

#### Step 5 - Buy postage label

    var cheapestRate = shipment.Rates.OrderBy(x => x.Rate).First();
    
    var label = easyPost.BuyPostageLabel(shipment.Id, cheapestRate);

