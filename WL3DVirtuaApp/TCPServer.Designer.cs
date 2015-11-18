namespace WL3DVirtuaApp
{
    partial class TCPServer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSet = new System.Windows.Forms.Button();
            this.txtVirualData = new System.Windows.Forms.TextBox();
            this.numVirtualInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lab = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.richSend = new System.Windows.Forms.RichTextBox();
            this.richReceive = new System.Windows.Forms.RichTextBox();
            this.btnWhileSend = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.labdf = new System.Windows.Forms.Label();
            this.numServerPort = new System.Windows.Forms.NumericUpDown();
            this.btnCreateServer = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSingle = new System.Windows.Forms.TabPage();
            this.btnSingleSend = new System.Windows.Forms.Button();
            this.tabFile = new System.Windows.Forms.TabPage();
            this.btnClearSentLog = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSendFileContent = new System.Windows.Forms.Button();
            this.txtSendContent = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.openLog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numVirtualInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabSingle.SuspendLayout();
            this.tabFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(367, 47);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(115, 42);
            this.btnSet.TabIndex = 0;
            this.btnSet.Text = "设置虚拟数据";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // txtVirualData
            // 
            this.txtVirualData.BackColor = System.Drawing.Color.White;
            this.txtVirualData.Location = new System.Drawing.Point(20, 29);
            this.txtVirualData.Multiline = true;
            this.txtVirualData.Name = "txtVirualData";
            this.txtVirualData.Size = new System.Drawing.Size(330, 124);
            this.txtVirualData.TabIndex = 1;
            // 
            // numVirtualInterval
            // 
            this.numVirtualInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numVirtualInterval.Location = new System.Drawing.Point(484, 9);
            this.numVirtualInterval.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numVirtualInterval.Name = "numVirtualInterval";
            this.numVirtualInterval.Size = new System.Drawing.Size(103, 21);
            this.numVirtualInterval.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "设置虚拟数据";
            // 
            // lab
            // 
            this.lab.AutoSize = true;
            this.lab.Location = new System.Drawing.Point(365, 13);
            this.lab.Name = "lab";
            this.lab.Size = new System.Drawing.Size(113, 12);
            this.lab.TabIndex = 4;
            this.lab.Text = "设置与下次间隔时间";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(367, 95);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(115, 42);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清空之前数据";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // richSend
            // 
            this.richSend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richSend.Location = new System.Drawing.Point(20, 179);
            this.richSend.Name = "richSend";
            this.richSend.Size = new System.Drawing.Size(330, 255);
            this.richSend.TabIndex = 5;
            this.richSend.Text = "";
            // 
            // richReceive
            // 
            this.richReceive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richReceive.Location = new System.Drawing.Point(367, 179);
            this.richReceive.Name = "richReceive";
            this.richReceive.Size = new System.Drawing.Size(330, 255);
            this.richReceive.TabIndex = 5;
            this.richReceive.Text = "";
            // 
            // btnWhileSend
            // 
            this.btnWhileSend.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWhileSend.Location = new System.Drawing.Point(516, 100);
            this.btnWhileSend.Name = "btnWhileSend";
            this.btnWhileSend.Size = new System.Drawing.Size(151, 61);
            this.btnWhileSend.TabIndex = 0;
            this.btnWhileSend.Text = "Cycle Send";
            this.btnWhileSend.UseVisualStyleBackColor = true;
            this.btnWhileSend.Click += new System.EventHandler(this.btnWhileSend_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Red;
            this.btnStop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.Location = new System.Drawing.Point(564, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(72, 48);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "要发送的命令";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "接收显示";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Socket IP";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(95, 13);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(143, 21);
            this.txtServerIP.TabIndex = 7;
            this.txtServerIP.Text = "192.168.1.211";
            // 
            // labdf
            // 
            this.labdf.AutoSize = true;
            this.labdf.Location = new System.Drawing.Point(269, 17);
            this.labdf.Name = "labdf";
            this.labdf.Size = new System.Drawing.Size(71, 12);
            this.labdf.TabIndex = 6;
            this.labdf.Text = "Socket Port";
            // 
            // numServerPort
            // 
            this.numServerPort.Location = new System.Drawing.Point(362, 13);
            this.numServerPort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numServerPort.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numServerPort.Name = "numServerPort";
            this.numServerPort.Size = new System.Drawing.Size(77, 21);
            this.numServerPort.TabIndex = 8;
            this.numServerPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // btnCreateServer
            // 
            this.btnCreateServer.BackColor = System.Drawing.Color.SpringGreen;
            this.btnCreateServer.Location = new System.Drawing.Point(477, 3);
            this.btnCreateServer.Name = "btnCreateServer";
            this.btnCreateServer.Size = new System.Drawing.Size(70, 26);
            this.btnCreateServer.TabIndex = 0;
            this.btnCreateServer.Text = "Run";
            this.btnCreateServer.UseVisualStyleBackColor = false;
            this.btnCreateServer.Click += new System.EventHandler(this.btnCreateServer_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSingle);
            this.tabControl1.Controls.Add(this.tabFile);
            this.tabControl1.Location = new System.Drawing.Point(0, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(724, 469);
            this.tabControl1.TabIndex = 9;
            // 
            // tabSingle
            // 
            this.tabSingle.Controls.Add(this.btnSingleSend);
            this.tabSingle.Controls.Add(this.btnSet);
            this.tabSingle.Controls.Add(this.label1);
            this.tabSingle.Controls.Add(this.txtVirualData);
            this.tabSingle.Controls.Add(this.btnClear);
            this.tabSingle.Controls.Add(this.numVirtualInterval);
            this.tabSingle.Controls.Add(this.label3);
            this.tabSingle.Controls.Add(this.richReceive);
            this.tabSingle.Controls.Add(this.lab);
            this.tabSingle.Controls.Add(this.richSend);
            this.tabSingle.Controls.Add(this.btnWhileSend);
            this.tabSingle.Controls.Add(this.label2);
            this.tabSingle.Location = new System.Drawing.Point(4, 22);
            this.tabSingle.Name = "tabSingle";
            this.tabSingle.Padding = new System.Windows.Forms.Padding(3);
            this.tabSingle.Size = new System.Drawing.Size(716, 443);
            this.tabSingle.TabIndex = 0;
            this.tabSingle.Text = "Send Manually";
            this.tabSingle.UseVisualStyleBackColor = true;
            // 
            // btnSingleSend
            // 
            this.btnSingleSend.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSingleSend.Location = new System.Drawing.Point(516, 36);
            this.btnSingleSend.Name = "btnSingleSend";
            this.btnSingleSend.Size = new System.Drawing.Size(151, 61);
            this.btnSingleSend.TabIndex = 6;
            this.btnSingleSend.Text = "Single Send";
            this.btnSingleSend.UseVisualStyleBackColor = true;
            this.btnSingleSend.Click += new System.EventHandler(this.btnSingleSend_Click);
            // 
            // tabFile
            // 
            this.tabFile.Controls.Add(this.btnClearSentLog);
            this.tabFile.Controls.Add(this.label6);
            this.tabFile.Controls.Add(this.label5);
            this.tabFile.Controls.Add(this.btnSendFileContent);
            this.tabFile.Controls.Add(this.txtSendContent);
            this.tabFile.Controls.Add(this.txtFilePath);
            this.tabFile.Controls.Add(this.btnOpenFile);
            this.tabFile.Location = new System.Drawing.Point(4, 22);
            this.tabFile.Name = "tabFile";
            this.tabFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabFile.Size = new System.Drawing.Size(716, 443);
            this.tabFile.TabIndex = 1;
            this.tabFile.Text = "Send From File";
            this.tabFile.UseVisualStyleBackColor = true;
            // 
            // btnClearSentLog
            // 
            this.btnClearSentLog.Location = new System.Drawing.Point(71, 68);
            this.btnClearSentLog.Name = "btnClearSentLog";
            this.btnClearSentLog.Size = new System.Drawing.Size(75, 19);
            this.btnClearSentLog.TabIndex = 6;
            this.btnClearSentLog.Text = "Clear";
            this.btnClearSentLog.UseVisualStyleBackColor = true;
            this.btnClearSentLog.Click += new System.EventHandler(this.btnClearSentLog_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Sent LOG:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Folder:";
            // 
            // btnSendFileContent
            // 
            this.btnSendFileContent.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSendFileContent.Location = new System.Drawing.Point(597, 9);
            this.btnSendFileContent.Name = "btnSendFileContent";
            this.btnSendFileContent.Size = new System.Drawing.Size(86, 62);
            this.btnSendFileContent.TabIndex = 3;
            this.btnSendFileContent.Text = "Send Out";
            this.btnSendFileContent.UseVisualStyleBackColor = true;
            this.btnSendFileContent.Click += new System.EventHandler(this.btnSendFileContent_Click);
            // 
            // txtSendContent
            // 
            this.txtSendContent.Location = new System.Drawing.Point(7, 89);
            this.txtSendContent.MaxLength = 0;
            this.txtSendContent.Multiline = true;
            this.txtSendContent.Name = "txtSendContent";
            this.txtSendContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSendContent.Size = new System.Drawing.Size(675, 355);
            this.txtSendContent.TabIndex = 2;
            this.txtSendContent.WordWrap = false;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(60, 30);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(394, 21);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(486, 25);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 30);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnStopServer
            // 
            this.btnStopServer.BackColor = System.Drawing.Color.Red;
            this.btnStopServer.Location = new System.Drawing.Point(477, 30);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(70, 26);
            this.btnStopServer.TabIndex = 10;
            this.btnStopServer.Text = "Shut Down";
            this.btnStopServer.UseVisualStyleBackColor = false;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // openLog
            // 
            this.openLog.FileName = "openLog";
            // 
            // TCPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 525);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.numServerPort);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.labdf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCreateServer);
            this.Controls.Add(this.btnStop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TCPServer";
            this.Text = "TCP SERVER ASSISTANT";
            ((System.ComponentModel.ISupportInitialize)(this.numVirtualInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabSingle.ResumeLayout(false);
            this.tabSingle.PerformLayout();
            this.tabFile.ResumeLayout(false);
            this.tabFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.TextBox txtVirualData;
        private System.Windows.Forms.NumericUpDown numVirtualInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox richSend;
        private System.Windows.Forms.RichTextBox richReceive;
        private System.Windows.Forms.Button btnWhileSend;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label labdf;
        private System.Windows.Forms.NumericUpDown numServerPort;
        private System.Windows.Forms.Button btnCreateServer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSingle;
        private System.Windows.Forms.TabPage tabFile;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.OpenFileDialog openLog;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnSendFileContent;
        private System.Windows.Forms.TextBox txtSendContent;
        private System.Windows.Forms.Button btnSingleSend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClearSentLog;
    }
}

