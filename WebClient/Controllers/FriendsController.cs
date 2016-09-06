using BusinessEntities.Entities;
using BusinessEntities.Enums;
using Socializr.Code.Attributes;
using Socializr.Code.Base;
using Socializr.Models.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socializr.Controllers
{
    [AuthorizePermissions(Permissions.AccessPublicApp)]
    public class FriendsController : BaseController
    {

        [HttpPost]
        public ActionResult SendFriendRequest(int userId)
        {
            var request = new FriendRequest()
            {
                IdRequester = Session.CurrentUser.IdUser,
                IdReceiver = userId,
                CreatedOn = DateTime.Now,
                State = (byte)FriendRequestStatus.RequestPending
            };

            Services.FrienshipService.SendFriendRequest(request);

            return Json(request, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CancelFriendRequest(int userId)
        {
            Services.FrienshipService.CancelFriendRequest(Session.CurrentUser.IdUser, userId);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AcceptFriendRequest(int userId)
        {
            Services.FrienshipService.AcceptFriendRequest(userId, Session.CurrentUser.IdUser);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RejectFriendRequest(int userId)
        {
            Services.FrienshipService.RejectFriendRequest(userId, Session.CurrentUser.IdUser);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CancelFriendRelation(int userId)
        {
            Services.FrienshipService.CancelFriendRelation(userId, Session.CurrentUser.IdUser);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Options(int userId)
        {
            var relation = Services.FrienshipService.GetRelationshipStatus(Session.CurrentUser.IdUser, userId);
            var status = new FriendStateModel()
            {
                IdFirstUser = Session.CurrentUser.IdUser,
                IdSecondUser = userId,
                Status = relation
            };
            return PartialView("Options", status);
        }

        [HttpGet]
        public ActionResult GetRecentFriendRequests()
        {
            var idRequested = Session.CurrentUser.IdUser;
            var requesters = Services.FrienshipService.GetRecentRequests(idRequested, Config.ResultNumber);
            var result = requesters.Select(r => new
            {
                requesterFullName = r.FName + " " + r.LName,
                requesterId = r.IdUser
            })
            .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}