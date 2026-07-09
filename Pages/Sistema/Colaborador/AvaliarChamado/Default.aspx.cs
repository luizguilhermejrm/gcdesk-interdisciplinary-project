using System;

public partial class Pages_Sistema_Colaborador_AvaliarChamado : BasePage
{
    protected override int? RequiredAccessType => 1;

    private readonly TicketService _ticketService = new TicketService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int ticketId = Convert.ToInt32(Request.QueryString["id"]);
            hiddenTicketId.Value = ticketId.ToString();
            lblInfo.Text = "Avalie o chamado #" + ticketId;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ticketId = Convert.ToInt32(hiddenTicketId.Value);
        int rating = Convert.ToInt32(Request.Form["rating"] ?? "0");

        if (rating < 1 || rating > 5)
        {
            lblMsg.Text = ToastHelper.Warning("Selecione uma nota entre 1 e 5.");
            return;
        }

        _ticketService.UpdateRating(ticketId, rating, txtComment.Text.Trim());
        HistoryRepository.Insert(ticketId, GetCurrentUser().UserId, "AVALIACAO", $"Chamado avaliado com nota {rating}");

        LogAction("RATE_TICKET");
        lblMsg.Text = ToastHelper.Success("Obrigado pela sua avaliação!");
        btnSubmit.Visible = false;
    }
}
