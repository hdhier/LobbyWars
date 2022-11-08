using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Signatures.Commands.CompareSignatures
{
    public class CompareSignaturesCommand : IRequest<string>
    {
        public string? Contract1 { get; set; }
        public string? Contract2 { get; set; }
    }

    public class CompareSignaturesCommandHandler : IRequestHandler<CompareSignaturesCommand, string>
    {

        public CompareSignaturesCommandHandler()
        {
        }

        public async Task<string> Handle(CompareSignaturesCommand request, CancellationToken cancellationToken)
        {
            return "Winner";
        }
    }
}
