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
            var contract1Points = 0;
            var contract2Points = 0;

            contract1Points = GetPoints(request.Contract1);
            contract2Points = GetPoints(request.Contract2);

            return contract1Points > contract2Points ? "Contract1 wins!" : "Contract2 wins!";
        }

        private int GetPoints(string contract)
        {
            var result = 0;

            var upperContract = contract.ToUpper();

            if (upperContract.Contains("K") && upperContract.Contains("V"))
            {
                upperContract = upperContract.Replace("V",string.Empty);
            }

            foreach (var rol in upperContract)
            {
                switch (rol) 
                {
                    case 'K':
                        result += 5;
                        break;
                    case 'N':
                        result += 2;
                        break;
                    case 'V':
                        result += 1;
                        break;
                    default:
                        throw new Exception("Caracter inesperado");
                }
            }

            return result;
        }
    }
}
