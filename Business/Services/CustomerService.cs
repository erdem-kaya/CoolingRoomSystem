using Business.Factories;
using Business.Interfaces;
using Business.Models.Customer;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerForm> CreateAsync(CustomerRegistrationForm form)
    {
        if (form == null)
            throw new ArgumentNullException(nameof(form), "Customer form cannot be null.");

        await _customerRepository.BeginTransactionAsync();

        try
        {
            var customer = CustomerFactory.Create(form);
            var createdCustomer = await _customerRepository.CreateAsync(customer);
            await _customerRepository.CommitTransactionAsync();

            return createdCustomer != null ? CustomerFactory.Create(createdCustomer) : null!;
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating customer: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<CustomerForm>> GetAllAsync()
    {
        try
        {
            var allCustomers = await _customerRepository.GetAllAsync();
            var result = allCustomers.Select(CustomerFactory.Create).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all customers: {ex.Message}");
            return [];
        }
    }

    public async Task<CustomerForm> GetByIdAsync(int id)
    {
        try
        {
            var customer = await _customerRepository.GetItemAsync(x => x.Id == id);
            var result = customer != null ? CustomerFactory.Create(customer) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting customer by id: {ex.Message}");
            return null!;
        }
    }

    public async Task<CustomerForm> UpdateAsync(CustomerUpdateForm form)
    {
        await _customerRepository.BeginTransactionAsync();

        try
        {
            var findCustomer = await _customerRepository.GetItemAsync(x => x.Id == form.Id) ?? throw new Exception($"User with ID {form.Id} does not exist.");
            findCustomer.UpdatedAt = DateTime.Now;
            CustomerFactory.Update(findCustomer, form);
            var updatedCustomer = await _customerRepository.UpdateAsync(x => x.Id == form.Id, findCustomer);
            var result = updatedCustomer != null ? CustomerFactory.Create(updatedCustomer) : null!;
            await _customerRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating customer: {ex.Message}");
            await _customerRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var deleteUser = await _customerRepository.DeleteAsync(x => x.Id == id);
            if (!deleteUser)
                throw new Exception($"Error deleting customer with ID {id}");

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting customer: {ex.Message}");
            return false;
        }
    }
}
