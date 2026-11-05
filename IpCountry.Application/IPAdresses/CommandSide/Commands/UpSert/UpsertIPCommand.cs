using CSharpFunctionalExtensions;
using MediatR;
using IpCountry.Infrastructure.Core.Models;

namespace IpCountry.Application.IPAdresses.CommandSide.Commands.UpSert
{
    public class UpsertIPCommand : IRequest<Result<Unit, Error>>
    {
        public UpsertIPCommand()
        {
            DataExecucao = DateTimeOffset.Now;
        }

        public DateTimeOffset DataExecucao { get; set; }
    }
}
