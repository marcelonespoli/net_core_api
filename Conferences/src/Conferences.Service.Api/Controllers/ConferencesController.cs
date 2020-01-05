using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conferences.Application.Interfaces;
using Conferences.Application.ViewModels;
using Conferences.Domain.Core.Notifications;
using Conferences.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conferences.Service.Api.Controllers
{
    public class ConferencesController : BaseController
    {
        private readonly IConferenceService _conferenceService;

        public ConferencesController(
            IUser user,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications,
            IConferenceService conferenceService) : base(user, mediator, notifications)
        {
            _conferenceService = conferenceService;
        }

        [HttpGet]
        [Route("conferences")]
        [AllowAnonymous]
        public IEnumerable<ConferenceViewModel> Get()
        {
            return _conferenceService.GetAll();
        }

        [HttpGet]
        [Route("conferences/{id:guid}")]
        [AllowAnonymous]
        public ConferenceViewModel Get(Guid id)
        {
            return _conferenceService.GetById(id);
        }

        [HttpGet]
        [Route("conferences/categories")]
        [AllowAnonymous]
        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _conferenceService.GetCategories();
        }

        [HttpGet]
        [Route("conferences/my-conferences")]
        public IEnumerable<ConferenceViewModel> GetMyConferences()
        {
            return _conferenceService.GetMyConferences(OrganizerId);
        }

        [HttpGet]
        [Route("conferences/my-conferences/{id:guid}")]
        public IActionResult GetMyConferenceById(Guid id)
        {
            var conference = _conferenceService.GetMyConferenceById(id, OrganizerId);
            return conference == null ? StatusCode(404) : Response(conference);
        }

        [HttpPost]
        [Route("conferences")]
        public IActionResult Post([FromBody] ConferenceViewModel conferenceViewModel)
        {
            if (!IsModelStateValid())
            {
                return Response();
            }

            var conferenceCommand = _conferenceService.RegisterConference(conferenceViewModel);
            return Response(conferenceCommand);
        }
        
        [HttpPut]
        [Route("conferences")]
        public IActionResult Put([FromBody] ConferenceViewModel conferenceViewModel)
        {
            if (!IsModelStateValid())
            {
                return Response();
            }

            var conferenceCommand = _conferenceService.UpdateConference(conferenceViewModel);
            return Response(conferenceCommand);
        }

        [HttpDelete]
        [Route("conferences/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var conferenceCommand = _conferenceService.DeleteConference(id);
            return Response(conferenceCommand);
        }

        [HttpPost]
        [Route("address")]
        public IActionResult PostAddress([FromBody] AddressViewModel addressViewModel)
        {
            if (!IsModelStateValid())
            {
                Response();
            }

            var addressCommand = _conferenceService.AddAddress(addressViewModel);
            return Response(addressCommand);
        }

        [HttpPut]
        [Route("address")]
        public IActionResult PutAddress([FromBody] AddressViewModel addressViewModel)
        {
            if (!IsModelStateValid())
            {
                Response();
            }

            var addressCommand = _conferenceService.UpdateAddress(addressViewModel);
            return Response(addressCommand);
        }

        private bool IsModelStateValid()
        {
            if (ModelState.IsValid) return true;

            NofifyErrorModelInvalid();
            return false;
        }
    }
}