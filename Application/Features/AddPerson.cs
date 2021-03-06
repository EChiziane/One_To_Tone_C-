using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Features.Persons
{
    public class AddPerson
    {
        public class AddPersonCommand : IRequest<Person>
        {
            public string Name { get; set; }
        }
        
        public class AddPersonValidator : AbstractValidator<AddPersonCommand>
        {
            public AddPersonValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
           
            }
        }
        
        public class AddPersonHandler : IRequestHandler<AddPersonCommand, Person>
        {
            private readonly DataContext _context;

            public AddPersonHandler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Person> Handle(AddPersonCommand request, CancellationToken cancellationToken)
            {
               // var animalType = await _context.Addresses.FindAsync(request.AnimalTypeId);
               // if (animalType is null || animalType.Description != "Person")
               // {
                //    throw new Exception("Animal Type not found or is not Person");
               // }
               Address address = new Address
               {
                   Place = ""
               };

                var Person = new Person
                {
                    Name = request.Name,
                   Address = address,
                   // AddressId = address.Id
                };
              
                await _context.Addresses.AddAsync(address);
                var result1 = await _context.SaveChangesAsync();

                await _context.People.AddAsync(Person);
                var result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    throw new Exception("Fail to add Person");
                }

                return Person;
            }
        }
    }
}