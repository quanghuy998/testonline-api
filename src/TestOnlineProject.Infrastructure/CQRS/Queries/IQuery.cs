using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOnlineProject.Infrastructure.CQRS.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
