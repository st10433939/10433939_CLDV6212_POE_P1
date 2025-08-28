﻿using Azure;
using Azure.Data.Tables;
using _10433939_CLDV6212_POE_P1.Models;

namespace _10433939_CLDV6212_POE_P1.Services
{
    public class TableStorageService
    {
        public readonly TableClient _customerTableClient;
        public readonly TableClient _productTableClient;
        public readonly TableClient _orderTableClient;
        public TableStorageService(string connectionString)
        {
            _customerTableClient = new TableClient(connectionString, "Customer");
            _productTableClient = new TableClient(connectionString, "Product");
            _orderTableClient = new TableClient(connectionString, "orders");
        }
        //Customer
        //Get
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = new List<Customer>();

            await foreach (var customer in _customerTableClient.QueryAsync<Customer>())
            {
                customers.Add(customer);
            }
            return customers;
        }
        //Add
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
        //Delete
        public async Task DeleteCustomerAsync(string partitionKey, string rowKey)
        {
            await _customerTableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        //Product
        //Get
        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();

            await foreach (var product in _productTableClient.QueryAsync<Product>())
            {
                products.Add(product);
            }
            return products;
        }
        //Add
        public async Task AddProductAsync(Product product)
        {
            if (string.IsNullOrEmpty(product.PartitionKey) || string.IsNullOrEmpty(product.RowKey))
            {
                throw new ArgumentException("PartitionKey and Rowkey must be set.");
            }

            try
            {
                await _productTableClient.AddEntityAsync(product);
            }
            catch (RequestFailedException ex)
            {
                throw new InvalidOperationException("Error adding entity to Table Storage.", ex);
            }
        }
        //Delete
        public async Task DeleteProductAsync(string partitionKey, string rowKey)
        {
            await _productTableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        //Order
        //Get
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = new List<Order>();

            await foreach (var order in _orderTableClient.QueryAsync<Order>())
            {
                orders.Add(order);
            }
            return orders;
        }
        //Add
        public async Task AddOrderAsync(Order order)
        {
            if (string.IsNullOrEmpty(order.PartitionKey) || string.IsNullOrEmpty(order.RowKey))
            {
                throw new ArgumentException("PartitionKey and Rowkey must be set.");
            }

            try
            {
                await _orderTableClient.AddEntityAsync(order);
            }
            catch (RequestFailedException ex)
            {
                throw new InvalidOperationException("Error adding entity to Table Storage.", ex);
            }
        }
    }
}

