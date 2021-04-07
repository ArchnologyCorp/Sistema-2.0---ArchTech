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
    public class ProdutoController : Controller
    {
        List<SelectListItem> marcas = new List<SelectListItem>();
        actionsProduto acProduto = new actionsProduto();
        Produto prod = new Produto();

        MySqlConnection cn = new MySqlConnection("server=localhost;user=root;database=dblojainformatica; password=0431723748");

        public void carregaFornecedores()
        {
            using (cn)
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbFornecedor", cn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    marcas.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }
                cn.Close();
                cn.Open();
            }

            ViewBag.marcas = new SelectList(marcas, "Value", "Text");
        }

        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult consultaProduto()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogada"] == null))
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {
                var todosProdutos = acProduto.Listar();
                return View(todosProdutos);
            }
        }

        [HttpPost]
        public ActionResult consultaProduto(FormCollection frm)
        {
            if (frm["txtNomeProduto"] == "")
            {
                return RedirectToAction("consultaProduto", "Produto");
            }

            else
            {
                prod.nome_produto = frm["txtNomeProduto"];
                var produtoLista = acProduto.ListarNome(prod.nome_produto);
                if(produtoLista.Count < 1)
                {
                    return RedirectToAction("consultaProduto", "Produto");
                }

                else
                {
                    return View(produtoLista);
                }
            }
        }

        public ActionResult areaGerente(int Id = 0)
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogada"] == null))
            {
                return RedirectToAction("Index", "Home");
            }

            else if (Session["tipoLogado0"] == null)
            {
                return RedirectToAction("semAcesso", "Home");
            }

            else if (Id != 0)
            {
                carregaFornecedores();

                var produto = acProduto.BuscaId(Id);
                if (produto == null)
                {
                    return HttpNotFound();
                }
                prod.valor_unitario.ToString().Replace('.', ',');
                return View(produto);
            }

            else
            {
                carregaFornecedores();
                return View();
            }

        }

        [HttpPost]
        public ActionResult areaGerente(string btn, Produto prod, FormCollection frm)
        {
            carregaFornecedores();
            if (btn == "Cadastrar")
            {
                //ViewBag.marcas = new SelectList(marcas, "Value", "Text");
                prod.cod_fornecedor = Convert.ToInt32(Request["marcas"]);
                prod.desc_produto = frm["TextArea"];
                ViewBag.mensagemSucesso = "Cadastro realizado com sucesso!";
                acProduto.cadastraProduto(prod);
                return View();
            }

            else if (btn == "Pesquisar")
            {

                acProduto.consultaBuscaProduto(prod);
                ViewBag.nome = prod.nome_produto;
                ViewBag.desc = prod.desc_produto;
                ViewBag.valor = Math.Round(prod.valor_unitario, 2).ToString().Replace(".", ",");
                ViewBag.marca = prod.cod_fornecedor;
                ViewBag.quantidade = prod.quantidade_estoque;
                return View();
            }

            else if (btn == "Atualizar")
            {
                prod.cod_fornecedor = Convert.ToInt32(Request["marcas"]);
                prod.desc_produto = frm["TextArea"];
                acProduto.atualizaProduto(prod);
                ViewBag.mensagemSucesso = "Atualização do produto de número " + prod.cod_produto + " realizada com sucesso!";
                return View();
            }

            else if (btn == "Excluir")
            {
                acProduto.deletaProduto(prod);
                ViewBag.mensagemSucesso = "Exclusão do produto de número " + prod.cod_produto + " realizada com sucesso!";
                return View();
            }

            else
            {
                return View();
            }
        }
    }
}