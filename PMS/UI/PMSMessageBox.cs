using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PMS.Controls
{
    public partial class PMSMessageBox : Form
    {
        // =========================================================
        // Message Icon
        // =========================================================

        public enum PMSMessageIcon
        {
            None,
            Information,
            Success,
            Warning,
            Error,
            Question
        }

        // =========================================================
        // Custom Status Images
        // =========================================================

        private Image informationImage =
            PMS.Properties.Resources.information;

        private Image warningImage =
            PMS.Properties.Resources.caution;

        private Image errorImage =
            PMS.Properties.Resources.mark;

        private Image questionImage =
            PMS.Properties.Resources.caution;

        private Image successImage =
            PMS.Properties.Resources.check;

        // =========================================================
        // Information Image
        // =========================================================

        [Category("PMS MessageBox")]
        [Description("รูปสำหรับสถานะ Information")]
        public Image InformationImage
        {
            get => informationImage;

            set
            {
                informationImage = value;

                if (currentIcon ==
                    PMSMessageIcon.Information)
                {
                    UpdateIcon();
                }
            }
        }

        // =========================================================
        // Success Image
        // =========================================================

        [Category("PMS MessageBox")]
        [Description("รูปสำหรับสถานะ Success")]
        public Image SuccessImage
        {
            get => successImage;

            set
            {
                successImage = value;

                if (currentIcon ==
                    PMSMessageIcon.Success)
                {
                    UpdateIcon();
                }
            }
        }

        // =========================================================
        // Warning Image
        // =========================================================

        [Category("PMS MessageBox")]
        [Description("รูปสำหรับสถานะ Warning")]
        public Image WarningImage
        {
            get => warningImage;

            set
            {
                warningImage = value;

                if (currentIcon ==
                    PMSMessageIcon.Warning)
                {
                    UpdateIcon();
                }
            }
        }

        // =========================================================
        // Error Image
        // =========================================================

        [Category("PMS MessageBox")]
        [Description("รูปสำหรับสถานะ Error")]
        public Image ErrorImage
        {
            get => errorImage;

            set
            {
                errorImage = value;

                if (currentIcon ==
                    PMSMessageIcon.Error)
                {
                    UpdateIcon();
                }
            }
        }

        // =========================================================
        // Question Image
        // =========================================================

        [Category("PMS MessageBox")]
        [Description("รูปสำหรับสถานะ Question")]
        public Image QuestionImage
        {
            get => questionImage;

            set
            {
                questionImage = value;

                if (currentIcon ==
                    PMSMessageIcon.Question)
                {
                    UpdateIcon();
                }
            }
        }

        // =========================================================
        // Current Icon
        // =========================================================

        private PMSMessageIcon currentIcon =
            PMSMessageIcon.None;

        // =========================================================
        // Constructor
        // =========================================================

        public PMSMessageBox()
        {
            InitializeComponent();

            StartPosition =
                FormStartPosition.CenterParent;

            FormBorderStyle =
                FormBorderStyle.None;

            ShowInTaskbar = false;

            KeyPreview = true;

            AcceptButton = null;

            CancelButton = null;
        }

        // =========================================================
        // Constructor With Message
        // =========================================================

        public PMSMessageBox(
            string text,
            string caption,
            MessageBoxButtons buttons,
            PMSMessageIcon icon)
            : this()
        {
            lblTitle.Text = caption;

            lblMessage.Text = text;

            currentIcon = icon;

            SetupButtons(buttons);

            UpdateIcon();

            ResizeMessage();
        }

        // =========================================================
        // Show
        // =========================================================

        public static DialogResult Show(
            string text)
        {
            return Show(
                text,
                "Pawn Management System",
                MessageBoxButtons.OK,
                PMSMessageIcon.None);
        }

        // =========================================================
        // Show
        // =========================================================

        public static DialogResult Show(
            string text,
            string caption)
        {
            return Show(
                text,
                caption,
                MessageBoxButtons.OK,
                PMSMessageIcon.None);
        }

        // =========================================================
        // Show
        // =========================================================

        public static DialogResult Show(
            string text,
            string caption,
            MessageBoxButtons buttons)
        {
            return Show(
                text,
                caption,
                buttons,
                PMSMessageIcon.None);
        }

        // =========================================================
        // Show
        // =========================================================

        public static DialogResult Show(
            string text,
            string caption,
            MessageBoxButtons buttons,
            PMSMessageIcon icon)
        {
            using (PMSMessageBox box =
                   new PMSMessageBox(
                       text,
                       caption,
                       buttons,
                       icon))
            {
                return box.ShowDialog();
            }
        }

        // =========================================================
        // Show With Owner
        // =========================================================

        public static DialogResult Show(
            IWin32Window owner,
            string text)
        {
            return Show(
                owner,
                text,
                "Pawn Management System",
                MessageBoxButtons.OK,
                PMSMessageIcon.None);
        }

        // =========================================================
        // Show With Owner
        // =========================================================

        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption)
        {
            return Show(
                owner,
                text,
                caption,
                MessageBoxButtons.OK,
                PMSMessageIcon.None);
        }

        // =========================================================
        // Show With Owner
        // =========================================================

        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxButtons buttons)
        {
            return Show(
                owner,
                text,
                caption,
                buttons,
                PMSMessageIcon.None);
        }

        // =========================================================
        // Show With Owner + Icon
        // =========================================================

        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxButtons buttons,
            PMSMessageIcon icon)
        {
            using (PMSMessageBox box =
                   new PMSMessageBox(
                       text,
                       caption,
                       buttons,
                       icon))
            {
                return box.ShowDialog(owner);
            }
        }

        // =========================================================
        // Setup Buttons
        // =========================================================

        private void SetupButtons(
            MessageBoxButtons buttons)
        {
            btn1.Visible = false;
            btn2.Visible = false;
            btn3.Visible = false;

            btn1.DialogResult =
                DialogResult.None;

            btn2.DialogResult =
                DialogResult.None;

            btn3.DialogResult =
                DialogResult.None;

            switch (buttons)
            {
                // =================================================
                // OK
                // =================================================

                case MessageBoxButtons.OK:

                    SetupButton(
                        btn1,
                        "ตกลง",
                        DialogResult.OK);

                    break;

                // =================================================
                // OK + Cancel
                // =================================================

                case MessageBoxButtons.OKCancel:

                    SetupButton(
                        btn1,
                        "ตกลง",
                        DialogResult.OK);

                    SetupButton(
                        btn2,
                        "ยกเลิก",
                        DialogResult.Cancel);

                    break;

                // =================================================
                // Yes + No
                // =================================================

                case MessageBoxButtons.YesNo:

                    SetupButton(
                        btn1,
                        "ใช่",
                        DialogResult.Yes);

                    SetupButton(
                        btn2,
                        "ไม่",
                        DialogResult.No);

                    break;

                // =================================================
                // Yes + No + Cancel
                // =================================================

                case MessageBoxButtons.YesNoCancel:

                    SetupButton(
                        btn1,
                        "ใช่",
                        DialogResult.Yes);

                    SetupButton(
                        btn2,
                        "ไม่",
                        DialogResult.No);

                    SetupButton(
                        btn3,
                        "ยกเลิก",
                        DialogResult.Cancel);

                    break;

                // =================================================
                // Retry + Cancel
                // =================================================

                case MessageBoxButtons.RetryCancel:

                    SetupButton(
                        btn1,
                        "ลองอีกครั้ง",
                        DialogResult.Retry);

                    SetupButton(
                        btn2,
                        "ยกเลิก",
                        DialogResult.Cancel);

                    break;

                // =================================================
                // Abort + Retry + Ignore
                // =================================================

                case MessageBoxButtons.AbortRetryIgnore:

                    SetupButton(
                        btn1,
                        "ยกเลิกการทำงาน",
                        DialogResult.Abort);

                    SetupButton(
                        btn2,
                        "ลองอีกครั้ง",
                        DialogResult.Retry);

                    SetupButton(
                        btn3,
                        "ไม่สนใจ",
                        DialogResult.Ignore);

                    break;
            }

            ArrangeButtons();
        }

        // =========================================================
        // Setup Individual Button
        // =========================================================

        private void SetupButton(
            PMSButton button,
            string text,
            DialogResult result)
        {
            button.Visible = true;

            button.Text = text;

            button.DialogResult = result;

            button.TabStop = true;
        }

        // =========================================================
        // Arrange Buttons
        // =========================================================

        private void ArrangeButtons()
        {
            PMSButton[] buttons =
            {
                btn1,
                btn2,
                btn3
            };

            int count = 0;

            foreach (PMSButton button in buttons)
            {
                if (button.Visible)
                {
                    count++;
                }
            }

            const int buttonWidth = 110;
            const int buttonHeight = 42;
            const int gap = 10;

            int totalWidth =
                (count * buttonWidth) +
                ((count - 1) * gap);

            int startX =
                (messageCard.Width -
                 totalWidth) / 2;

            int index = 0;

            foreach (PMSButton button in buttons)
            {
                if (!button.Visible)
                    continue;

                button.Size =
                    new Size(
                        buttonWidth,
                        buttonHeight);

                button.Location =
                    new Point(
                        startX +
                        index *
                        (buttonWidth + gap),

                        messageCard.Height -
                        buttonHeight -
                        25);

                index++;
            }
        }

        // =========================================================
        // Update Icon
        // =========================================================

        private void UpdateIcon()
        {
            Image image = null;

            switch (currentIcon)
            {
                case PMSMessageIcon.Information:

                    image = informationImage;
                    this.BackColor = Color.FromArgb(79, 140, 255);
                    break;

                case PMSMessageIcon.Success:

                    image = successImage;

                    this.BackColor = Color.FromArgb(60, 203, 127);
                    break;

                case PMSMessageIcon.Warning:

                    image = warningImage;

                    this.BackColor = Color.FromArgb(245, 185, 66);
                    break;

                case PMSMessageIcon.Error:

                    image = errorImage;

                    this.BackColor = Color.FromArgb(255, 92, 92);
                    break;

                case PMSMessageIcon.Question:

                    image = questionImage;

                    this.BackColor = Color.FromArgb(255, 212, 59);
                    break;

                case PMSMessageIcon.None:

                    pictureIcon.Image = null;

                    pictureIcon.Visible = false;

                    return;
            }

            pictureIcon.Image = image;

            pictureIcon.Visible =
                image != null;
        }

        // =========================================================
        // Resize Message
        // =========================================================

        private void ResizeMessage()
        {
            using (Graphics g = CreateGraphics())
            {
                SizeF size =
                    g.MeasureString(
                        lblMessage.Text,
                        lblMessage.Font,
                        330);

                int messageHeight =
                    Math.Max(
                        50,
                        (int)size.Height + 10);

                lblMessage.Height =
                    messageHeight;

                int iconHeight =
                    pictureIcon.Visible
                        ? pictureIcon.Height
                        : 0;

                int contentHeight =
                    Math.Max(
                        iconHeight,
                        messageHeight);

                int requiredHeight =
                    35 +
                    contentHeight +
                    80;

                messageCard.Height =
                    Math.Max(
                        250,
                        requiredHeight);

                ClientSize =
                    new Size(
                        ClientSize.Width,
                        messageCard.Height + 45);

                ArrangeButtons();
            }
        }

        // =========================================================
        // Keyboard
        // =========================================================

        protected override bool ProcessDialogKey(
            Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                foreach (Control control
                    in messageCard.Controls)
                {
                    if (control is PMSButton button &&
                        button.Visible &&
                        button.DialogResult ==
                        DialogResult.Cancel)
                    {
                        DialogResult =
                            DialogResult.Cancel;

                        Close();

                        return true;
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        // =========================================================
        // Form Closing
        // =========================================================

        private void PMSMessageBox_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            if (DialogResult ==
                DialogResult.None)
            {
                DialogResult =
                    DialogResult.Cancel;
            }
        }

        private void PMSMessageBox_Load(object sender, EventArgs e)
        {

        }
    }
}