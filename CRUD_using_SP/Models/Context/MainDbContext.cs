using CRUD_using_SP.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace CRUD_using_SP.Models.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options): base(options) { }

        public DbSet<Customer> Customers { get; set; }

        public IQueryable<Customer> SearchCustomer(string Name)
        {
            var pCustomerName = new SqlParameter("@name", Name);

            return this.Customers.FromSqlRaw("EXECUTE Customers_SearchCustomers @name", pCustomerName);
        }

        public async Task<string> InsertCstomerintoTable( string name, string city, string country, DateTime date) 
        {
            
            var pCustomerName = new SqlParameter("@Name", name);
            var pCustomercity = new SqlParameter("@City", city);
            var pCustomercountry = new SqlParameter("@Country", country);
            var pCustomerDOB = new SqlParameter("@Dob", date);

            var result = await Database.ExecuteSqlRawAsync("EXECUTE InsertCustomer  @Name, @City, @Country, @Dob",
                    pCustomerName, pCustomercity, pCustomercountry, pCustomerDOB);

            return result == 1? "Success" : "Error";
        }

        public async Task<string> UpdateCstomerfromTable(int customerId, string name, string city, string country, DateTime date)
        {
            var pCustomerId = new SqlParameter("@CustomerId", customerId);
            var pCustomerName = new SqlParameter("@Name", name);
            var pCustomercity = new SqlParameter("@City", city);
            var pCustomercountry = new SqlParameter("@Country", country);
            var pCustomerDOB = new SqlParameter("@Dob", date);

            var result = await Database.ExecuteSqlRawAsync("EXECUTE UpdateCustomer @CustomerId, @Name, @City, @Country, @Dob",
                    pCustomerId, pCustomerName, pCustomercity, pCustomercountry, pCustomerDOB);

            return result == 1 ? "Success" : "Error";
        }

        public async Task<string> DeleteCustomerFromTable(int customerId)
        {
            var pCustomerId = new SqlParameter("@CustomerId", customerId);

            var result = await Database.ExecuteSqlRawAsync("EXECUTE DeleteCustomer @CustomerId", pCustomerId);

            return result == 1 ? "Success" : "Error: Customer with the provided CustomerId was not found.";
        }

    }
}
