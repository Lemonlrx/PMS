namespace PMS.Controls
{
    partial class PMSNotificationForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            macWindowButtonsCloseOnly1 = new MacWindowButtonsCloseOnly();
            SuspendLayout();
            // 
            // macWindowButtonsCloseOnly1
            // 
            macWindowButtonsCloseOnly1.BackColor = Color.Transparent;
            macWindowButtonsCloseOnly1.Location = new Point(12, 12);
            macWindowButtonsCloseOnly1.Name = "macWindowButtonsCloseOnly1";
            macWindowButtonsCloseOnly1.Size = new Size(396, 28);
            macWindowButtonsCloseOnly1.TabIndex = 0;
            // 
            // PMSNotificationForm
            // 
            ClientSize = new Size(420, 500);
            Controls.Add(macWindowButtonsCloseOnly1);
            Name = "PMSNotificationForm";
            Text = "Notifications";
            ResumeLayout(false);
        }
        private MacWindowButtonsCloseOnly macWindowButtonsCloseOnly1;
    }
}