using MoSteganography.Core;

namespace MoSteganography
{
    public partial class Form1 : Form
    {
        private Bitmap originalBitmap;
        private Bitmap stegoBitmap;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        originalBitmap = new Bitmap(fs);
                    }
                    pictureBoxOriginal.Image = new Bitmap(originalBitmap);
                }
            }
        }

        private void btnHideText_Click(object sender, EventArgs e)
        {
            if (originalBitmap == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }
            try
            {
                stegoBitmap?.Dispose();
                stegoBitmap = new Bitmap(originalBitmap);
                stegoBitmap = ImageSteganography.EmbedText(stegoBitmap, txtMessage.Text);
                pictureBoxStego.Image = new Bitmap(stegoBitmap);
                MessageBox.Show("Text hidden successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error hiding text: " + ex.Message);
            }
        }

        private void btnSaveStego_Click(object sender, EventArgs e)
        {
            if (stegoBitmap == null)
            {
                MessageBox.Show("No stego image to save.");
                return;
            }
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Bitmap Image|*.bmp|PNG Image|*.png";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    stegoBitmap.Save(sfd.FileName);
                    MessageBox.Show("Stego image saved.");
                }
            }
        }

        private void btnExtractText_Click(object sender, EventArgs e)
        {
            if (stegoBitmap == null)
            {
                MessageBox.Show("No stego image for extraction.");
                return;
            }
            try
            {
                string extracted = ImageSteganography.ExtractText(stegoBitmap);
                lblExtractedMessage.Text = extracted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error extracting text: " + ex.Message);
            }
        }

        private void btnLoadStegoImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        // Dispose the old stegoBitmap if we have one,
                        // then load the new one from the selected file.
                        stegoBitmap?.Dispose();
                        stegoBitmap = new Bitmap(fs);
                    }

                    // Show the newly loaded stego image in pictureBoxStego
                    pictureBoxStego.Image = new Bitmap(stegoBitmap);
                }
            }
        }

    }
}
