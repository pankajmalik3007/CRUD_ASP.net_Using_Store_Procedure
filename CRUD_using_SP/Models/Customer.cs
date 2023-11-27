namespace CRUD_using_SP.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public DateTime DOB { get; set; }
    }
}
