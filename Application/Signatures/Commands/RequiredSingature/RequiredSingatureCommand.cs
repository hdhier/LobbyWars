using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Signatures.Commands.CompareSignatures
{
    public class RequiredSigtnatureCommand : IRequest<string>
    {
        public string? Contract1 { get; set; }
        public string? Contract2 { get; set; }
    }

    public class RequiredSigtnatureCommandHandler : IRequestHandler<RequiredSigtnatureCommand, string>
    {

        public RequiredSigtnatureCommandHandler()
        {
        }

        public async Task<string> Handle(RequiredSigtnatureCommand request, CancellationToken cancellationToken)
        {
            var contractWithHashtag = string.Empty;
            var contractWithoutHashtag = string.Empty;

            if (request.Contract1.Contains("#"))
            {
                contractWithHashtag = request.Contract1;
                contractWithoutHashtag = request.Contract2;
            }
            else
            {
                contractWithHashtag = request.Contract2;
                contractWithoutHashtag = request.Contract1;
            }
            

            var contractWithHashtagPoints = GetPoints(contractWithHashtag);
            var contractWithoutHashtagPoints = GetPoints(contractWithoutHashtag);

            if (contractWithHashtagPoints <= contractWithoutHashtagPoints)
            {
                var difference = contractWithoutHashtagPoints - contractWithHashtagPoints;

                if (difference < 1 && !contractWithHashtag.ToUpper().Contains("K"))
                {
                    return "El rol necesario será V";
                }

                if (difference < 2)
                {
                    return "El rol necesario será N";
                }

                // Si contractWithHashtag no tenia k a la diferencia se le restaba 1 más que ahora se le suma por las v
                if (!contractWithHashtag.ToUpper().Contains("K")) 
                {
                    var numberOfV = contractWithHashtag.ToUpper().Count(x => x == 'V');
                    difference += numberOfV;
                }

                if (difference < 5)
                {   
                    return "El rol necesario será K";
                }
            }
            else
            {
                return "El contrato con # ya es el de mayor puntuación";
            }

            return "No es posible superar el valor de la firma con ningun ROL";
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
                    case '#':
                        result += 0;
                        break;
                    default:
                        throw new Exception("Caracter inesperado");
                }
            }

            return result;
        }
    }
}
