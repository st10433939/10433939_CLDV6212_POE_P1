using Azure;
using Azure.Data.Tables;
using _10433939_CLDV6212_POE_P1.Models;

namespace _10433939_CLDV6212_POE_P1.Services
{
    public class TableStorageService
    {
        public readonly TableClient _customerTableClient;
        public TableStorageService(string connectionString)
        {
            _customerTableClient = new TableClient(connectionString, "Customer");
        }
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = new List<Customer>();

            await foreach (var customer in _customerTableClient.QueryAsync<Customer>())
            {
                customers.Add(customer);
            }
            return customers;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.PartitionKey) || string.IsNullOrEmpty(customer.RowKey))
            {
                throw new ArgumentException("PartitionKey and Rowkey must be set.");
            }

            try
            {
                await _customerTableClient.AddEntityAsync(customer);
            }
            catch (RequestFailedException ex)
            {
                throw new InvalidOperationException("Error adding entity to Table Storage.", ex);
            }
        }

        public async Task DeleteCustomerAsync(string partitionKey, string rowKey)
        {
            await _customerTableClient.DeleteEntityAsync(partitionKey, rowKey);
        }
    }
}

