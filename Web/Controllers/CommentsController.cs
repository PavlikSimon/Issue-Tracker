using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        readonly CommentFacade commentFacade = new CommentFacade();

        [Authorize]
        public ActionResult Index()
        {
            var model = commentFacade.GetAllComments();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = commentFacade.GetCommentById(id.Value);
            return View(comment);
        }

        [HttpPost]
        public ActionResult Edit(int id, CommentDTO comment)
        {
            if (ModelState.IsValid)
            {
                var originalComment = commentFacade.GetCommentById(id);
                originalComment.Text = comment.Text;
                originalComment.PostedDate = originalComment.PostedDate;

                commentFacade.UpdateComment(originalComment);

                return RedirectToAction("Index");
            }
            return View(comment);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            commentFacade.DeleteComment(id);
            return RedirectToAction("Index");
        }
    }
}