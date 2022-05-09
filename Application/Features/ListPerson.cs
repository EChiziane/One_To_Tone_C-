using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features{
    public class ListPerson
    {
        public class ListPeopleQuery : IRequest<IReadOnlyList<Person>>
        {
        }
        
        public class ListPeopleHandler : IRequestHandler<ListPeopleQuery, IReadOnlyList<Person>>
        {
            private readonly DataContext _context;

            public ListPeopleHandler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<IReadOnlyList<Person>> Handle(ListPeopleQuery request, CancellationToken cancellationToken)
            {
                return await _context.People
                    .Include(x=>x.Address) // Adicionar o incluide para retorna o tipo de animal   
                    .ToListAsync();
            }
        }
    }
}