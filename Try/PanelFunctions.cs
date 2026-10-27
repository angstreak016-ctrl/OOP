using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class PanelAnimator
{
    // Slide Up (Internal helper)
    public static async Task SlideUpAsync(Control control, int distance, int durationMs = 80)
    {
        Color originalBackColor = control.BackColor;
        control.BackColor = Color.Peru; // Solid color during motion to prevent jitter

        int startY = control.Top;
        int targetY = startY - distance;
        int frames = Math.Max(durationMs / 16, 1);

        control.SuspendLayout();

        for (int i = 1; i <= frames; i++)
        {
            float progress = (float)i / frames;
            progress = (float)Math.Sin(progress * Math.PI / 2); // Ease-out curve

            control.Top = (int)(startY + (targetY - startY) * progress);
            await Task.Delay(16);
        }

        control.Top = targetY;
        control.ResumeLayout(true);

        control.BackColor = originalBackColor;
        control.Invalidate(true);
    }

    // Slide Down (Internal helper)
    public static async Task SlideDownAsync(Control control, int distance, int durationMs = 200)
    {
        Color originalBackColor = control.BackColor;
        control.BackColor = Color.Peru; // Solid color during motion

        int startY = control.Top;
        int targetY = startY + distance;
        int frames = Math.Max(durationMs / 16, 1);

        control.SuspendLayout();

        for (int i = 1; i <= frames; i++)
        {
            float progress = (float)i / frames;
            progress = (float)Math.Sin(progress * Math.PI / 2); // Ease-out curve

            control.Top = (int)(startY + (targetY - startY) * progress);
            await Task.Delay(16);
        }

        control.Top = targetY;
        control.ResumeLayout(true);

        control.BackColor = originalBackColor;
        control.Invalidate(true);

        control.Visible = false; // Hide completely when finished sliding down
    }

    // NEW: Master method to handle the form and the panel animation together!
    public static async Task ShowFormInsidePanelAsync(Form f, Panel host, int distance = 400)
    {
        f.TopLevel = false;
        host.Visible = true;
        f.FormBorderStyle = FormBorderStyle.None;
        host.Controls.Add(f);
        f.Show();
        await SlideUpAsync(host, distance);
        f.FormClosed += async (s, e) =>
        {
            await SlideDownAsync(host, distance);
        };
    }
}