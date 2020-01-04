using Conferences.Domain.Core.Notifications;
using Conferences.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Conferences.Service.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _nofications;
        private readonly IMediatorHandler _mediator;

        protected Guid OrganizerId { get; set; }

        protected BaseController(
            IUser user,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> nofications)
        {
            _nofications = (DomainNotificationHandler)nofications;
            _mediator = mediator;
            
            if (user.IsAuthenticated())
            {
                OrganizerId = user.GetUserId();
            }
        }


        protected new IActionResult Response(object result = null)
        {
            if (IsValidOpeation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _nofications.GetNotifications().Select(n => n.Value)
            });
        }

        protected bool IsValidOpeation()
        {
            return (!_nofications.HasNotifications());
        }

        protected void NofifyErrorModelInvalid()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(string.Empty, errorMsg);
            }
        }

        protected void NotifyError(string codigo, string errorMsg)
        {
            _mediator.PublishEvent(new DomainNotification(codigo, errorMsg));
        }

        //protected void AddErrorsIdentity(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        NotifyError(result.ToString(), error.Description);
        //    }
        //}

    }
}
