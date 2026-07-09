<%@ Page Title="" Language="C#"
MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true"
CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Analista_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.7/dist/chart.umd.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <section id="pageContent">
    <asp:Label ID="lblCdTicket" runat="server" Text=""></asp:Label>
    <div class="container">
      <div class="row">
        <div class="col-lg-3 col-md-6 col-12 my-1 d-flex justify-content-center">
          <div class="card w-75 text-center border-0 shadow pb-4">
            <div class="card-header border-0 mb-2 text-start bg-transparent">
              <i class="fa-regular fa-circle-question text-secondary"></i>
            </div>
            <div class="d-flex justify-content-center">
              <div class="rounded-circle bg-success-low" style="width: 70px; height: 70px">
                <i class="fa-solid fa-circle-check text-black-50 fs-6 mt-4"></i>
              </div>
            </div>
            <div class="card-body">
              <asp:Label class="card-title mt-3 fs-1" ID="lblFinishedCalls" runat="server" Text="" />
              <p class="card-text fs-6 fw-bold text-black-50 mt-3">Chamados Finalizados</p>
            </div>
          </div>
        </div>
        <div class="col-lg-3 col-md-6 col-12 my-1 d-flex justify-content-center">
          <div class="card w-75 text-center border-0 shadow pb-4">
            <div class="card-header border-0 mb-2 text-start bg-transparent">
              <i class="fa-regular fa-circle-question text-secondary"></i>
            </div>
            <div class="d-flex justify-content-center">
              <div class="rounded-circle bg-warning-low" style="width: 70px; height: 70px">
                <i class="fa-solid fa-clock text-black-50 fs-6 mt-4"></i>
              </div>
            </div>
            <div class="card-body">
              <asp:Label class="card-title mt-3 fs-1" ID="lblProgressCalls" runat="server" Text="" />
              <p class="card-text fs-6 fw-bold text-black-50 mt-3">Chamados em Andamento</p>
            </div>
          </div>
        </div>
        <div class="col-lg-3 col-md-6 col-12 my-1 d-flex justify-content-center">
          <div class="card w-75 text-center border-0 shadow pb-4">
            <div class="card-header border-0 mb-2 text-start bg-transparent">
              <i class="fa-regular fa-circle-question text-secondary"></i>
            </div>
            <div class="d-flex justify-content-center">
              <div class="rounded-circle bg-primary-low" style="width: 70px; height: 70px">
                <i class="fa-solid fa-spinner text-black-50 fs-6 mt-4"></i>
              </div>
            </div>
            <div class="card-body">
              <asp:Label class="card-title mt-3 fs-1" ID="lblOpenCalls" runat="server" Text="" />
              <p class="card-text fs-6 fw-bold text-black-50 mt-3">Chamados em Aberto (Todos)</p>
            </div>
          </div>
        </div>
        <div class="col-lg-3 col-md-6 col-12 my-1 d-flex justify-content-center">
          <div class="card w-75 text-center border-0 shadow pb-4">
            <div class="card-header border-0 mb-2 text-start bg-transparent">
              <i class="fa-regular fa-circle-question text-secondary"></i>
            </div>
            <div class="d-flex justify-content-center">
              <div class="rounded-circle bg-secondary bg-opacity-50" style="width: 70px; height: 70px">
                <i class="fa-solid fa-user text-black-50 fs-6 mt-4"></i>
              </div>
            </div>
            <div class="card-body">
              <asp:Label class="card-title mt-3 fs-1" ID="lblQuantityPerson" runat="server" Text="" />
              <p class="card-text fs-6 fw-bold text-black-50 mt-3">Colaboradores Ativos</p>
            </div>
          </div>
        </div>
      </div>
      <div class="row mt-4">
        <div class="col-md-4">
          <div class="card mb-4">
            <div class="card-header bg-primary text-white"><i class="fa-solid fa-chart-pie me-1"></i> Chamados por Status</div>
            <div class="card-body"><canvas id="chartStatus" height="200"></canvas></div>
          </div>
        </div>
        <div class="col-md-4">
          <div class="card mb-4">
            <div class="card-header bg-primary text-white"><i class="fa-solid fa-chart-bar me-1"></i> Chamados por Departamento</div>
            <div class="card-body"><canvas id="chartDepartment" height="200"></canvas></div>
          </div>
        </div>
        <div class="col-md-4">
          <div class="card mb-4">
            <div class="card-header bg-primary text-white"><i class="fa-solid fa-chart-line me-1"></i> Chamados por Período (7 dias)</div>
            <div class="card-body"><canvas id="chartPeriod" height="200"></canvas></div>
          </div>
        </div>
      </div>
      <div class="row mt-2">
        <div class="col-md-6">
          <div class="card mb-4">
            <div class="card-header bg-danger text-white"><i class="fa-solid fa-exclamation-triangle me-1"></i> Gargalos por Tipo (Reaberturas)</div>
            <div class="card-body"><canvas id="chartBottleneck" height="200"></canvas></div>
          </div>
        </div>
        <div class="col-md-6">
          <div class="card mb-4">
            <div class="card-header bg-warning text-dark"><i class="fa-solid fa-clock me-1"></i> Alertas SLA</div>
            <div class="card-body">
              <asp:Label ID="lblSlaAlerts" runat="server" CssClass="d-block" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <asp:UpdatePanel ID="upFooter" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <asp:Label ID="lblMsg" runat="server" />
      <div class="row mt-3">
        <div class="col-lg-6 col-md-12">
          <div class="card mb-4">
            <div class="card-header bg-primary text-white"><i class="fa-solid fa-table me-1"></i> Chamados em Aberto</div>
            <div class="card-body overflow-auto" style="height: 400px">
              <div class="table-responsive p-3">
                <asp:Label ID="lblTableNull" runat="server"></asp:Label>
                <asp:GridView ID="gdvTickets" runat="server" AutoGenerateColumns="false"
                  CssClass="table table-hover datatable-plugin border-top-0 border-start-0 border-end-0"
                  OnRowDataBound="gdvTickets_RowDataBound" OnRowCommand="gdvTickets_RowCommand">
                  <Columns>
                    <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_description" HeaderText="Descrição" />
                    <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_localization" HeaderText="Localização" />
                    <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_openTime" HeaderText="Horário de abertura" />
                    <asp:TemplateField HeaderText="Aceitar" ItemStyle-CssClass="py-4">
                      <ItemTemplate><asp:LinkButton ID="lkbPegar" runat="server" CommandArgument='<% #Bind("tic_id") %>'></asp:LinkButton></ItemTemplate>
                    </asp:TemplateField>
                  </Columns>
                </asp:GridView>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-6">
          <div class="card mb-4">
            <div class="card-header bg-primary text-white"><i class="fa-solid fa-table-list me-1"></i> Notificações recentes</div>
            <div class="card-body overflow-auto" style="height: 400px">
              <div class="table-responsive p-3">
                <asp:Label ID="lblTableNullNotification" runat="server"></asp:Label>
                <asp:GridView ID="gdvNotification" runat="server" HeaderStyle-CssClass="d-none"
                  AutoGenerateColumns="false" CssClass="table table-borderless table-hover border-start-0 border-end-0"
                  OnRowDataBound="gdvNotification_RowDataBound">
                  <Columns>
                    <asp:BoundField ItemStyle-CssClass="py-4 text-black-50 border-top" DataField="not_status" />
                    <asp:BoundField ItemStyle-CssClass="py-4 text-black-50 border-top" DataField="not_title" />
                    <asp:BoundField ItemStyle-CssClass="py-4 text-black-50 border-top" DataField="tic_description" />
                    <asp:BoundField ItemStyle-CssClass="py-4 text-black-50 border-top" DataField="not_description" />
                    <asp:BoundField ItemStyle-CssClass="py-4 text-black-50 border-top" DataField="not_timeMensage" />
                  </Columns>
                </asp:GridView>
              </div>
            </div>
          </div>
        </div>
      </div>
    </ContentTemplate>
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="gdvTickets" />
    </Triggers>
  </asp:UpdatePanel>
  <asp:HiddenField ID="hiddenChartStatus" runat="server" />
  <asp:HiddenField ID="hiddenChartDept" runat="server" />
  <asp:HiddenField ID="hiddenChartPeriod" runat="server" />
  <asp:HiddenField ID="hiddenChartBottleneck" runat="server" />
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
  <script>
    window.onload = (event) => {
      let myAlert = document.querySelector(".toast");
      if (myAlert) { let bsAlert = new bootstrap.Toast(myAlert); bsAlert.show(); }
      drawCharts();
    };

    function drawCharts() {
      const colors = ['#3381E2', '#FFC107', '#28A745', '#DC3545', '#6F42C1'];
      try {
        const statusData = JSON.parse(document.getElementById('hiddenChartStatus').value);
        if (statusData.length) {
          new Chart(document.getElementById('chartStatus'), {
            type: 'doughnut',
            data: { labels: statusData.map(d => d.name), datasets: [{ data: statusData.map(d => d.total), backgroundColor: colors }] },
            options: { responsive: true, plugins: { legend: { position: 'bottom' } } }
          });
        }
      } catch(e) {}
      try {
        const deptData = JSON.parse(document.getElementById('hiddenChartDept').value);
        if (deptData.length) {
          new Chart(document.getElementById('chartDepartment'), {
            type: 'bar',
            data: { labels: deptData.map(d => d.name), datasets: [{ label: 'Chamados', data: deptData.map(d => d.total), backgroundColor: colors }] },
            options: { responsive: true, plugins: { legend: { display: false } }, scales: { y: { beginAtZero: true, ticks: { stepSize: 1 } } } }
          });
        }
      } catch(e) {}
      try {
        const periodData = JSON.parse(document.getElementById('hiddenChartPeriod').value);
        if (periodData.length) {
          new Chart(document.getElementById('chartPeriod'), {
            type: 'line',
            data: { labels: periodData.map(d => d.day), datasets: [{ label: 'Chamados', data: periodData.map(d => d.total), borderColor: '#3381E2', fill: true, tension: 0.3 }] },
            options: { responsive: true, plugins: { legend: { display: false } }, scales: { y: { beginAtZero: true, ticks: { stepSize: 1 } } } }
          });
        }
      } catch(e) {}
      try {
        const bottleneckData = JSON.parse(document.getElementById('hiddenChartBottleneck').value);
        if (bottleneckData.length) {
          new Chart(document.getElementById('chartBottleneck'), {
            type: 'bar',
            data: { labels: bottleneckData.map(d => d.name), datasets: [{ label: 'Reaberturas', data: bottleneckData.map(d => d.total), backgroundColor: '#DC3545' }] },
            options: { responsive: true, plugins: { legend: { display: false } }, scales: { y: { beginAtZero: true, ticks: { stepSize: 1 } } } }
          });
        }
      } catch(e) {}
    }
  </script>
</asp:Content>
