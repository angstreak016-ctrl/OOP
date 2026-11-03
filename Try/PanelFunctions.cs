using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentTransitions;
public class PanelFormAnimator
{
    private int _originalTop;
    private bool _isOriginalTopStored = false;
    public void ShowFormInPanel(Form formToShow, Panel hostPanel, int slideDistance = 200)
    {
        
        if (!_isOriginalTopStored)
        {
            _originalTop = hostPanel.Top;
            _isOriginalTopStored = true;
        }
        formToShow.TopLevel = false;
        formToShow.FormBorderStyle = FormBorderStyle.None;
        formToShow.Dock = DockStyle.Fill;


        hostPanel.Controls.Clear();
        hostPanel.Controls.Add(formToShow);
        hostPanel.Visible = true;

        formToShow.Show();

        int targetTop = _originalTop - slideDistance;

        Transition.With(hostPanel, nameof(Panel.Top), targetTop)
                  .EaseInEaseOut(TimeSpan.FromMilliseconds(400));

        formToShow.FormClosed += (s, e) =>
        {
            Transition.With(hostPanel, nameof(Panel.Top), _originalTop)
                      .EaseInEaseOut(TimeSpan.FromMilliseconds(500));
        };
    }
}