using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_DetalheChamado : BasePage
{
    protected override int? RequiredAccessType => null;

    private readonly TicketService _ticketService = new TicketService();
    private readonly UserRepository _userRepository = new UserRepository();
    private Ticket _ticket;

    protected void Page_Load(object sender, EventArgs e)
    {
        int ticketId = Convert.ToInt32(Request.QueryString["id"]);

        _ticket = _ticketService.GetTicketById(ticketId);
        if (_ticket == null)
        {
            Response.Redirect("~/Default.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadTicket();
            LoadComments();
            LoadHistory();
        }
    }

    private void LoadTicket()
    {
        lblTicketId.Text = _ticket.TicketId.ToString();
        lblDescription.Text = _ticket.Description;
        lblType.Text = _ticket.TypeTicket;
        lblLocalization.Text = _ticket.Localization;
        lblOpenTime.Text = _ticket.OpenTime;
        lblCloseTime.Text = string.IsNullOrEmpty(_ticket.CloseTime) ? "-" : _ticket.CloseTime;

        User requester = _userRepository.GetById(_ticket.UserId);
        lblRequester.Text = requester?.Name ?? "N/A";

        if (_ticket.AnalystId > 0)
        {
            User analyst = _userRepository.GetById(_ticket.AnalystId);
            lblAnalyst.Text = analyst?.Name ?? "N/A";
        }
        else
        {
            lblAnalyst.Text = "Aguardando analista";
        }

        string statusClass = "bg-secondary";
        string statusText = "Aberto";
        if (_ticket.Status == 1) { statusClass = "bg-warning text-dark"; statusText = "Em Andamento"; }
        else if (_ticket.Status == 2) { statusClass = "bg-success"; statusText = "Finalizado"; }

        lblStatus.CssClass = $"badge {statusClass} fs-6";
        lblStatus.Text = statusText;

        if (_ticket.Rating > 0)
        {
            pnlRating.Visible = true;
            lblRating.Text = new string('\u2605', _ticket.Rating) + new string('\u2606', 5 - _ticket.Rating);
            lblRatingComment.Text = _ticket.RatingComment;
        }
    }

    private void LoadComments()
    {
        DataSet ds = CommentRepository.GetByTicketId(_ticket.TicketId);
        rptComments.DataSource = ds;
        rptComments.DataBind();
        lblNoComments.Visible = ds.Tables[0].Rows.Count == 0;
    }

    private void LoadHistory()
    {
        DataSet ds = HistoryRepository.GetByTicketId(_ticket.TicketId);
        rptHistory.DataSource = ds;
        rptHistory.DataBind();
        lblNoHistory.Visible = ds.Tables[0].Rows.Count == 0;
    }

    protected void btnSendComment_Click(object sender, EventArgs e)
    {
        string text = txtComment.Text.Trim();
        if (string.IsNullOrEmpty(text))
            return;

        User user = GetCurrentUser();
        CommentRepository.Insert(_ticket.TicketId, user.UserId, text);
        HistoryRepository.Insert(_ticket.TicketId, user.UserId, "COMENTARIO", $"Comentário adicionado por {user.Name}");
        LogAction("COMMENT", $"Comentário no chamado #{_ticket.TicketId}");
        txtComment.Text = "";
        LoadComments();
    }

    protected string GetActionIcon(string action)
    {
        switch (action)
        {
            case "CRIADO": return "<i class='fa-solid fa-plus-circle text-primary fs-4'></i>";
            case "ACEITO": return "<i class='fa-solid fa-hand text-warning fs-4'></i>";
            case "FINALIZADO": return "<i class='fa-solid fa-check-circle text-success fs-4'></i>";
            case "COMENTARIO": return "<i class='fa-solid fa-comment text-info fs-4'></i>";
            case "AVALIACAO": return "<i class='fa-solid fa-star text-warning fs-4'></i>";
            default: return "<i class='fa-solid fa-circle text-secondary fs-4'></i>";
        }
    }
}
