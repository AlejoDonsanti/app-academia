using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Microsoft.Ajax.Utilities;

namespace UI.Web
{
    public partial class Default : System.Web.UI.Page
    {
        UsuarioLogic ul;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Panel header = (Panel)Master.FindControl("header");
                header.Visible = false;
            }
        }
        protected void ingresarLinkButton_Click(object sender, EventArgs e)
        {
            ul = new UsuarioLogic();
            Usuario usuario = ul.Login(this.tbUsuario.Text,this.tbContraseña.Text);
            if(usuario != null) {
                Panel header = (Panel)Master.FindControl("header");
                header.Visible = true;
                Session["user"] = usuario;
                Response.Redirect("usuarios.aspx");
            }
            else
            {
                string script = "alert(\"Usuario o contraseña incorrecto\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
            
        }
    }
}