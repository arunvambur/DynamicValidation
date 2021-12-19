using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileTransferApi.Model;
using FileTransferApi.Provider;
using FluentValidation;
using FileTransferApi.Provider.DataStream;

namespace FileTransferApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {

        private readonly ILogger<TransferController> _logger;
        private readonly ITransferServiceFactory _transferServiceFactory;

        public TransferController(ILogger<TransferController> logger, ITransferServiceFactory transferServiceFactory)
        {
            _logger = logger;
            _transferServiceFactory = transferServiceFactory;
        }

        [HttpPost]
        public async Task<TransferResponse> Post([FromBody] TransferRequest request)
        {
            IProviderSettingsValidator sSettingsValidator = _transferServiceFactory.GetPlatformSettingsValidator(request.Source.ProviderType);
            IProviderSettingsValidator dSettingsValidator = _transferServiceFactory.GetPlatformSettingsValidator(request.Destination.ProviderType);

            string srcSettingsJson = Convert.ToString(request.Source.Settings).Replace(Environment.NewLine, string.Empty);
            string dstSettingsJson = Convert.ToString(request.Destination.Settings).Replace(Environment.NewLine, string.Empty);
            var failure = new[]
            {
                sSettingsValidator?.Validate(srcSettingsJson, "Source"),
                dSettingsValidator?.Validate(dstSettingsJson, "Destination")
            }.SelectMany(t => t.Errors)
            .Where(error => error != null);

            if (failure.Any())
            {
                throw new ValidationException($"Node settings({request.Source.ProviderType}) validation error", failure);
            }

            //If the validation is successful then transfer the file
            var sourceTransferService = _transferServiceFactory.GetTransferService(request.Source.ProviderType);
            var destinationTransferService = _transferServiceFactory.GetTransferService(request.Destination.ProviderType);
            var dataStream = new DataStream();
            await sourceTransferService.Pull(request.Source, dataStream);
            await destinationTransferService.Push(request.Destination, dataStream);


            return new TransferResponse { Message = "File transferred succesfully" };
        }
    }
}
