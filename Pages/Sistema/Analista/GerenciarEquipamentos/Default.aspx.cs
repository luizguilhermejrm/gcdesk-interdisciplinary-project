using System;
using System.Web;
using System.Web.UI;

public partial class Pages_Sistema_Analista_GerenciarEquipamentos : BasePage
{
    protected override int? RequiredAccessType => 0;

    private EquipmentRepository _equipRepo = new EquipmentRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadGrid();
    }

    private void LoadGrid()
    {
        gdvEquips.DataSource = _equipRepo.GetAll();
        gdvEquips.DataBind();
    }

    protected void gdvEquips_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteEquip")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            _equipRepo.Delete(id);
            LogAction("DELETE_EQUIPMENT", $"Equipamento excluído: ID {id}");
            LoadGrid();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int id = int.Parse(hiddenEquipId.Value);
        string desc = txtDescription.Text.Trim();
        int number = int.Parse(txtNumber.Text.Trim());

        if (string.IsNullOrEmpty(desc) || number < 1)
        {
            lblMsg.Text = "<div class='alert alert-warning'>Preencha todos os campos corretamente.</div>";
            return;
        }

        if (id == 0)
        {
            _equipRepo.Insert(new Equipment { Description = desc, EquipNumber = number });
            LogAction("CREATE_EQUIPMENT", $"Equipamento criado: {desc}");
        }
        else
        {
            var equip = _equipRepo.GetById(id);
            if (equip != null)
            {
                equip.Description = desc;
                equip.EquipNumber = number;
                _equipRepo.Update(equip);
                LogAction("UPDATE_EQUIPMENT", $"Equipamento alterado: {desc}");
            }
        }

        LoadGrid();
        ScriptManager.RegisterStartupScript(this, GetType(), "closeModal",
            "document.querySelector('#modalEquip .btn-close').click();", true);
    }
}
