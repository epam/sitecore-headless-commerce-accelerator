namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses
{
    public class Address
    {
        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipPostalCode { get; set; }

        public string ExternalId { get; set; }

        public string PartyId { get; set; }

        public bool IsPrimary { get; set; }

        public string Email { get; set; }
    }
}