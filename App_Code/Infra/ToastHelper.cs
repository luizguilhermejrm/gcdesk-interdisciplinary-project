/// <summary>Static helper for rendering Bootstrap toast notifications.</summary>
public static class ToastHelper
{
    /// <summary>Builds a Bootstrap toast HTML string.</summary>
    /// <param name="color">Text color class (success, warning, danger).</param>
    /// <param name="icon">SVG icon ID from the sprite.</param>
    /// <param name="title">Toast header title.</param>
    /// <param name="body">Toast body message.</param>
    private static string Toast(string color, string icon, string title, string body)
    {
        return $@"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                    <div class='toast'>
                       <div class='toast-header'>
                          <svg class='bi flex-shrink-0 me-2 text-{color}' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#{icon}'/></svg>
                          <strong class='me-auto'>{title}</strong>
                          <small>Agora</small>
                        </div>
                        <div class='toast-body'>
                          {body}
                        </div>
                     </div>
                  </div>";
    }

    /// <summary>Returns a success toast.</summary>
    public static string Success(string msg) => Toast("success", "check-circle-fill", "Sucesso!", msg);
    /// <summary>Returns a warning toast.</summary>
    public static string Warning(string msg) => Toast("warning", "exclamation-triangle-fill", "Aviso!", msg);
    /// <summary>Returns an error toast.</summary>
    public static string Error(string msg) => Toast("danger", "exclamation-triangle-fill", "Erro!", msg);
}
