﻿namespace WindowsFormsApp1
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labService = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTable = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.labOldType = new System.Windows.Forms.Label();
            this.tbOldType = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbNewType = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labService
            // 
            this.labService.AutoSize = true;
            this.labService.Location = new System.Drawing.Point(46, 17);
            this.labService.Name = "labService";
            this.labService.Size = new System.Drawing.Size(41, 12);
            this.labService.TabIndex = 0;
            this.labService.Text = "服务器";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(93, 14);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(166, 21);
            this.tbServer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号";
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(93, 40);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(166, 21);
            this.tbAccount.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "密码";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(93, 68);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(166, 21);
            this.tbPassword.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "数据库";
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(93, 94);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(166, 21);
            this.tbDatabase.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "表";
            // 
            // tbTable
            // 
            this.tbTable.Location = new System.Drawing.Point(93, 120);
            this.tbTable.Name = "tbTable";
            this.tbTable.Size = new System.Drawing.Size(166, 21);
            this.tbTable.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(142, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "修改";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(85, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "（模糊查询使用通配符和占位符）";
            // 
            // labOldType
            // 
            this.labOldType.AutoSize = true;
            this.labOldType.Location = new System.Drawing.Point(35, 175);
            this.labOldType.Name = "labOldType";
            this.labOldType.Size = new System.Drawing.Size(41, 12);
            this.labOldType.TabIndex = 0;
            this.labOldType.Text = "原类型";
            // 
            // tbOldType
            // 
            this.tbOldType.Location = new System.Drawing.Point(82, 172);
            this.tbOldType.Name = "tbOldType";
            this.tbOldType.Size = new System.Drawing.Size(107, 21);
            this.tbOldType.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(206, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "新类型";
            // 
            // tbNewType
            // 
            this.tbNewType.Location = new System.Drawing.Point(253, 172);
            this.tbNewType.Name = "tbNewType";
            this.tbNewType.Size = new System.Drawing.Size(114, 21);
            this.tbNewType.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 256);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbNewType);
            this.Controls.Add(this.tbOldType);
            this.Controls.Add(this.tbTable);
            this.Controls.Add(this.tbDatabase);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbAccount);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labOldType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labService);
            this.Name = "Form1";
            this.Text = "修改SQLServer数据类型";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labService;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAccount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labOldType;
        private System.Windows.Forms.TextBox tbOldType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbNewType;
    }
}

