using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["language"] = "es";
        }
    }

    protected void UserInput_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Convert_Click(object sender, EventArgs e)
    {
        if (userinput.Text.Length > 0)
        {
            ServicioNumerosPolaco.IServicioNumerosPolaco servicioPolaco = new ServicioNumerosPolaco.ServicioNumerosPolacoClient();
            ServicioNumerosPolaco.Conversion [] conversions = servicioPolaco.Traducir(userinput.Text, ViewState["language"].ToString());
            foreach (ServicioNumerosPolaco.Conversion conversion in conversions) {
                HtmlGenericControl list = new HtmlGenericControl("ul");
                list.Attributes.Add("class", "list-group mt-5 ");
                HtmlGenericControl listHeader = new HtmlGenericControl("li");
                listHeader.Attributes.Add("class", "list-group-item active");
                listHeader.InnerText = conversion.Tipo;
                HtmlGenericControl listElement = new HtmlGenericControl("li");
                listElement.Attributes.Add("class", "list-group-item");
                listElement.Attributes.Add("align", "justify");
                if(conversion.Respuestas.Length > 1)
                {
                    for(int i = 0; i < conversion.Respuestas.Length; i++)
                    {
                        listElement.InnerHtml = listElement.InnerHtml + conversion.Respuestas[i].ToString() + "<br/>";
                    }
                }
                else
                {
                    listElement.InnerText = conversion.Respuestas[0];
                }
                list.Controls.Add(listHeader);
                list.Controls.Add(listElement);

                PlaceHolder1.Controls.Add(list);
            }
        }

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["language"] = DropDownList1.SelectedItem.Value;
        if(ViewState["language"].ToString() == "es")
        {
            Label1.Text = "Conversor de números a texto en polaco";
            Label2.Text = "Escriba su número:";
            convertBtn.Text = "Convertir";
        }else if (ViewState["language"].ToString() == "en")
        {
            Label1.Text = "Convert numbers to text in polish";
            Label2.Text = "Write your number:";
            convertBtn.Text = "Convert";
        }else if(ViewState["language"].ToString() == "pl")
        {
            Label1.Text = "Zamiana liczb na tekst w języku polskim";
            Label2.Text = "Napisz liczbę:";
            convertBtn.Text = "Konwertuj";
        }
    }
}