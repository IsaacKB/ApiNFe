using Models;
using pocApiSefaz.Repositories.Interfaces;
using pocApiSefaz.Services.Interfaces;
using pocApiSefaz.DTOs;
using AutoMapper;

namespace pocApiSefaz.Services
{
    public class SoapService : ISoapService
    {
        private readonly ISoapRepository _soapRepository;
        private readonly IMapper _mapper;

        public SoapService(ISoapRepository soapRepository, IMapper mapper)
        {
            _soapRepository = soapRepository;
            _mapper = mapper;
        }

        public IResult GetAll()
        {
            var todos = _soapRepository.execute();
            return todos;
        }
    }
}
