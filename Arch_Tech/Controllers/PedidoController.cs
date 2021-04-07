using Arch_Tech.Models;
using Arch_Tech.Repositório;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arch_Tech.Controllers
{
    public class PedidoController : Controller
    {
        List<SelectListItem> clientes = new List<SelectListItem>();
        List<SelectListItem> produtos = new List<SelectListItem>();
        List<SelectListItem> pagamentos = new List<SelectListItem>();
        List<SelectListItem> situacao = new List<SelectListItem>();

        MySqlConnection cn = new MySqlConnection("server=localhost;user=root;database=dblojainformatica; password=0431723748");
        Pedido pedido = new Pedido();

        actionsPedido acPedido = new actionsPedido();


        public void carregaSituacao()
        {
            situacao.Add(new SelectListItem
            {
                Text = "Concluído",
                Value = "Concluído",
            });

            situacao.Add(new SelectListItem { 
                Text = "Em Andamento",
                Value = "Em Andamento"
            });

            situacao.Add(new SelectListItem
            {
                Text = "Cancelado",
                Value = "Cancelado",
            });

            ViewBag.situacao = new SelectList(situacao, "Text", "Value");
        }

        public void carregaClientes()
        {
            using (cn)
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCliente", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clientes.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                cn.Close();
                cn.Open();
            }
            ViewBag.clientes = new SelectList(clientes, "Value", "Text");
        }
        public void carregaProdutos()
        {
            using (cn)
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProduto", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    produtos.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                cn.Close();
                cn.Open();
            }
            ViewBag.produtos = new SelectList(produtos, "Value", "Text");
        }
        public void carregaPagamentos()
        {
            using (cn)
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbPagamento", cn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    pagamentos.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                cn.Close();
                cn.Open();
            }
            ViewBag.pagamentos = new SelectList(pagamentos, "Value", "Text");
        }
        // GET: Pedido
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult consultaPedido()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogada"] == null))
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {
                var todosPedidos = acPedido.Listar();
                return View(todosPedidos);
            }
        }

        [HttpPost]
        public ActionResult consultaPedido(FormCollection frm)
        {
            if(frm["txtCodPedido"] == "")
            {
                return RedirectToAction("consultaPedido", "Pedido");
            }

            else
            {
                pedido.cod_pedido = Convert.ToInt32(frm["txtCodPedido"]);
                var pedidoLista = acPedido.ListarIdPedido(pedido.cod_pedido);
                if (pedidoLista.Count < 1)
                {
                    return RedirectToAction("consultaPedido", "Pedido");
                }
                return View(pedidoLista);

            }
        }

        public ActionResult cadastroPedido()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogada"] == null))
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {
                carregaClientes();
                carregaPagamentos();
                carregaProdutos();
                carregaSituacao();
                return View();
            }
        }

        [HttpPost]
        public ActionResult cadastroPedido(Pedido pedido)
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogada"] == null))
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {
                carregaClientes();
                carregaPagamentos();
                carregaProdutos();
                carregaSituacao();

                pedido.cod_cli = Convert.ToInt32(Request["clientes"]);
                pedido.cod_pagamento = Convert.ToInt32(Request["pagamentos"]);
                pedido.cod_produto = Convert.ToInt32(Request["produtos"]);
                pedido.situacao = Request["situacao"].ToString();
                acPedido.cadastraPedido(pedido);
                ViewBag.confCadastro = "Cadastro realizado com sucesso!";
                return View();

            }
        }
    }
}