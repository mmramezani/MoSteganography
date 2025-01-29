namespace MoSteganography
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            pictureBoxOriginal = new PictureBox();
            pictureBoxStego = new PictureBox();
            txtMessage = new TextBox();
            lblExtractedMessage = new TextBox();
            btnHideText = new Button();
            btnSaveStego = new Button();
            btnExtractText = new Button();
            btnLoadStegoImage = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxStego).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 441);
            button1.Name = "button1";
            button1.Size = new Size(391, 53);
            button1.TabIndex = 0;
            button1.Text = "Load Image";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnLoadImage_Click;
            // 
            // pictureBoxOriginal
            // 
            pictureBoxOriginal.Location = new Point(12, 52);
            pictureBoxOriginal.Name = "pictureBoxOriginal";
            pictureBoxOriginal.Size = new Size(391, 367);
            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxOriginal.TabIndex = 1;
            pictureBoxOriginal.TabStop = false;
            // 
            // pictureBoxStego
            // 
            pictureBoxStego.Location = new Point(482, 52);
            pictureBoxStego.Name = "pictureBoxStego";
            pictureBoxStego.Size = new Size(436, 367);
            pictureBoxStego.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxStego.TabIndex = 2;
            pictureBoxStego.TabStop = false;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(482, 463);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(436, 31);
            txtMessage.TabIndex = 3;
            // 
            // lblExtractedMessage
            // 
            lblExtractedMessage.Location = new Point(386, 609);
            lblExtractedMessage.Name = "lblExtractedMessage";
            lblExtractedMessage.Size = new Size(533, 31);
            lblExtractedMessage.TabIndex = 4;
            // 
            // btnHideText
            // 
            btnHideText.Location = new Point(539, 509);
            btnHideText.Name = "btnHideText";
            btnHideText.Size = new Size(141, 44);
            btnHideText.TabIndex = 5;
            btnHideText.Text = "Inject Text";
            btnHideText.UseVisualStyleBackColor = true;
            btnHideText.Click += btnHideText_Click;
            // 
            // btnSaveStego
            // 
            btnSaveStego.Location = new Point(713, 509);
            btnSaveStego.Name = "btnSaveStego";
            btnSaveStego.Size = new Size(136, 44);
            btnSaveStego.TabIndex = 6;
            btnSaveStego.Text = "Save Stego";
            btnSaveStego.UseVisualStyleBackColor = true;
            btnSaveStego.Click += btnSaveStego_Click;
            // 
            // btnExtractText
            // 
            btnExtractText.Location = new Point(208, 595);
            btnExtractText.Name = "btnExtractText";
            btnExtractText.Size = new Size(161, 45);
            btnExtractText.TabIndex = 7;
            btnExtractText.Text = "Extract Text";
            btnExtractText.UseVisualStyleBackColor = true;
            btnExtractText.Click += btnExtractText_Click;
            // 
            // btnLoadStegoImage
            // 
            btnLoadStegoImage.Location = new Point(12, 595);
            btnLoadStegoImage.Name = "btnLoadStegoImage";
            btnLoadStegoImage.Size = new Size(190, 45);
            btnLoadStegoImage.TabIndex = 8;
            btnLoadStegoImage.Text = "Load Stego Image";
            btnLoadStegoImage.UseVisualStyleBackColor = true;
            btnLoadStegoImage.Click += btnLoadStegoImage_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(386, 581);
            label1.Name = "label1";
            label1.Size = new Size(119, 25);
            label1.TabIndex = 9;
            label1.Text = "Extracted Text";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(482, 435);
            label2.Name = "label2";
            label2.Size = new Size(110, 25);
            label2.TabIndex = 10;
            label2.Text = "Text to inject";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ControlDarkDark;
            label3.Location = new Point(12, 24);
            label3.Name = "label3";
            label3.Size = new Size(103, 25);
            label3.TabIndex = 11;
            label3.Text = "Base Image";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ControlDarkDark;
            label4.Location = new Point(482, 24);
            label4.Name = "label4";
            label4.Size = new Size(194, 25);
            label4.TabIndex = 12;
            label4.Text = "Image with hidden text";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(931, 665);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLoadStegoImage);
            Controls.Add(btnExtractText);
            Controls.Add(btnSaveStego);
            Controls.Add(btnHideText);
            Controls.Add(lblExtractedMessage);
            Controls.Add(txtMessage);
            Controls.Add(pictureBoxStego);
            Controls.Add(pictureBoxOriginal);
            Controls.Add(button1);
            MaximizeBox = false;
            Name = "Form1";
            Text = "SteganographyForm";
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxStego).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private PictureBox pictureBoxOriginal;
        private PictureBox pictureBoxStego;
        private TextBox txtMessage;
        private TextBox lblExtractedMessage;
        private Button btnHideText;
        private Button btnSaveStego;
        private Button btnExtractText;
        private Button btnLoadStegoImage;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
