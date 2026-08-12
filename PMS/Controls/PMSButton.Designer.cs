namespace PMS.Controls
{
    partial class PMSButton
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components =
                new System.ComponentModel.Container();

            this.AutoSize = false;

            this.BackColor =
                System.Drawing.Color.FromArgb(
                    75,
                    110,
                    255);

            this.FlatAppearance.BorderSize = 0;

            this.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.ForeColor =
                System.Drawing.Color.White;

            this.Name =
                "PMSButton";

            this.Size =
                new System.Drawing.Size(
                    140,
                    45);

            this.TabStop = true;

            this.Text =
                "PMS Button";

            this.UseVisualStyleBackColor =
                false;
        }
    }
}