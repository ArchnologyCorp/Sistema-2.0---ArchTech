using Arch_Tech.Models;
using Arch_Tech.Repositório;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Arch_Tech.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        actionsLogin acL = new actionsLogin();

        //*requisição post para submeter o formulário e verificar as informações do login*//
        [HttpPost]
        public ActionResult Index(Login login)
        {
            acL.testarUsuario(login);
            if(login.nome_usuario != null && login.senha != null)
            {
                FormsAuthentication.SetAuthCookie(login.nome_usuario, false);
                Session["usuarioLogado"] = login.nome_usuario.ToString();
                Session["senhaLogada"] = login.senha.ToString();

                if(login.tipo == 1)
                {
                    Session["tipoLogado1"] = login.tipo.ToString();
                }

                else
                {
                    Session["tipoLogado0"] = login.tipo.ToString();
                }
                return RedirectToAction("Home", "Home");
            }

            else
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o usuário e a senha e tente novamente.";
                return View();
            }
        }

        public ActionResult Home()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogada"] == null))
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session["senhaLogada"] = null;
            Session["tipoLogado0"] = null;
            Session["tipoLogado1"] = null;

            return RedirectToAction("Home", "Home");
        }

        public ActionResult semAcesso()
        {
            ViewBag.Message = "Você não tem privilégios para acessar esta página.";
            return View();
        }


    }
}