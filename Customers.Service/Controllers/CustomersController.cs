using AutoMapper;
using Customers.Service.Contracts.Repositories;
using Customers.Service.Contracts.Requests;
using Customers.Service.Dto.Customer;
using Customers.Service.Models;
using Customers.Service.Events;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Customers.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public CustomersController(ICustomerRepository customerRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public ActionResult<PaginatedCustomerDto> GetCustomers(int page = 1, int limit = 10)
    {
        var customers = _customerRepository.GetCustomersPaged(FilterDefinition<Customer>.Empty, page, limit);

        return Ok(_mapper.Map<PaginatedCustomerDto>(customers));
    }

    [HttpGet("{id}", Name = "GetCustomer")]
    public ActionResult<CustomerReadDto> GetCustomer(string id)
    {
        var customer = _customerRepository.GetCustomerById(id);
        if (customer == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<CustomerReadDto>(customer));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerReadDto>> CreateCustomer(CreateCustomerRequest request)
    {
        var customer = _customerRepository.CreateCustomer(_mapper.Map<Customer>(request));

        await _publishEndpoint.Publish(_mapper.Map<CustomerCreated>(customer));

        return CreatedAtRoute(
            nameof(GetCustomer),
            new { id = customer.Id },
            _mapper.Map<CustomerReadDto>(customer)
        );
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCustomer(UpdateCustomerRequest request)
    {
        var customer = _customerRepository.GetCustomerById(request.Id);
        if (customer == null)
        {
            return NotFound();
        }

        var updatedCustomer = _customerRepository.UpdateCustomer(_mapper.Map<Customer>(request));
        await _publishEndpoint.Publish(_mapper.Map<CustomerUpdated>(updatedCustomer));

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(string id)
    {
        _customerRepository.DeleteCustomer(id);
        await _publishEndpoint.Publish(new CustomerDeleted
        {
            Id = id
        });

        return NoContent();
    }
}