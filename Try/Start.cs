using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Try
{
    public partial class Start : Form
    {
        private VideoCapture _capture;
        private Mat _frame;
        private Thread _cameraThread;
        private bool _isCameraRunning;
        public Start()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {

        }
        private void CaptureCameraLoop()
        {
            while (_isCameraRunning)
            {
                _capture.Read(_frame);
                if (_frame.Empty()) continue;

                // Convert OpenCV Mat to GDI+ Bitmap safely and display in PictureBox
                Bitmap bitmap = _frame.ToBitmap();

                // Use Invoke to safely update UI controls from a background worker thread
                pictureBox1.Invoke(new Action(() =>
                {
                    // Dispose old image to avoid memory leaks
                    var oldImage = pictureBox1.Image;
                    pictureBox1.Image = bitmap;
                    oldImage?.Dispose();
                }));

                // Roughly throttle loop to ~30 FPS
                Thread.Sleep(30);
            }
        }
        private bool _pictureBoxHasImage()
        {
            return pictureBox1.Image != null;
        }

        private void StopCamera()
        {
            _isCameraRunning = false;
            _cameraThread?.Join(1000); // Wait for thread to finish cleanly
            _capture?.Release();
            _capture?.Dispose();
            _frame?.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            if (!_isCameraRunning)
            {
                // 0 refers to the default connected webcam
                _capture = new VideoCapture(0);
                if (!_capture.IsOpened())
                {
                    MessageBox.Show("Error: Could not open the camera.");
                    return;
                }

                _frame = new Mat();
                _isCameraRunning = true;
                btnStart.Text = "Pause";

                // Start background thread to capture video frames without freezing the UI
                _cameraThread = new Thread(CaptureCameraLoop);
                _cameraThread.IsBackground = true;
                _cameraThread.Start();
            }
            else
            {
                StopCamera();
                btnStart.Text = "Resume";
            }
        }

        private void btnCapture_Click_1(object sender, EventArgs e)
        {
            if (_pictureBoxHasImage())
            {
                // Save current frame as a snapshot image
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp";
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        MessageBox.Show("Snapshot saved successfully!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please start the camera first.");
            }
        }
    }
}
