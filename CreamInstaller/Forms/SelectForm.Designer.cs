﻿using System.ComponentModel;
using System.Windows.Forms;

using CreamInstaller.Components;

namespace CreamInstaller.Forms
{
    partial class SelectForm
    {
        private IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components is not null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            installButton = new Button();
            cancelButton = new Button();
            programsGroupBox = new GroupBox();
            proxyFlowPanel = new FlowLayoutPanel();
            proxyAllCheckBox = new CheckBox();
            noneFoundLabel = new Label();
            blockedGamesFlowPanel = new FlowLayoutPanel();
            blockedGamesCheckBox = new CheckBox();
            blockProtectedHelpButton = new Button();
            allCheckBoxLayoutPanel = new FlowLayoutPanel();
            allCheckBox = new CheckBox();
            progressBar = new ProgressBar();
            progressLabel = new Label();
            scanButton = new Button();
            uninstallButton = new Button();
            progressLabelGames = new Label();
            progressLabelDLCs = new Label();
            sortCheckBox = new CheckBox();
            saveButton = new Button();
            loadButton = new Button();
            resetButton = new Button();
            saveFlowPanel = new FlowLayoutPanel();
            selectionTreeView = new CustomTreeView();
            programsGroupBox.SuspendLayout();
            proxyFlowPanel.SuspendLayout();
            blockedGamesFlowPanel.SuspendLayout();
            allCheckBoxLayoutPanel.SuspendLayout();
            saveFlowPanel.SuspendLayout();
            SuspendLayout();
            // 
            // installButton
            // 
            installButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            installButton.AutoSize = true;
            installButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            installButton.Enabled = false;
            installButton.Location = new System.Drawing.Point(495, 376);
            installButton.Name = "installButton";
            installButton.Padding = new Padding(3, 0, 3, 0);
            installButton.Size = new System.Drawing.Size(127, 25);
            installButton.TabIndex = 10000;
            installButton.Text = "安装";
            installButton.UseVisualStyleBackColor = true;
            installButton.Click += OnInstall;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cancelButton.AutoSize = true;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelButton.Location = new System.Drawing.Point(12, 376);
            cancelButton.Name = "cancelButton";
            cancelButton.Padding = new Padding(3, 0, 3, 0);
            cancelButton.Size = new System.Drawing.Size(59, 25);
            cancelButton.TabIndex = 10004;
            cancelButton.Text = "取消";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCancel;
            // 
            // programsGroupBox
            // 
            programsGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            programsGroupBox.Controls.Add(proxyFlowPanel);
            programsGroupBox.Controls.Add(noneFoundLabel);
            programsGroupBox.Controls.Add(blockedGamesFlowPanel);
            programsGroupBox.Controls.Add(allCheckBoxLayoutPanel);
            programsGroupBox.Controls.Add(selectionTreeView);
            programsGroupBox.Location = new System.Drawing.Point(12, 12);
            programsGroupBox.Name = "programsGroupBox";
            programsGroupBox.Size = new System.Drawing.Size(610, 287);
            programsGroupBox.TabIndex = 8;
            programsGroupBox.TabStop = false;
            programsGroupBox.Text = "游戏列表";
            // 
            // proxyFlowPanel
            // 
            proxyFlowPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            proxyFlowPanel.AutoSize = true;
            proxyFlowPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            proxyFlowPanel.Controls.Add(proxyAllCheckBox);
            proxyFlowPanel.Location = new System.Drawing.Point(478, -1);
            proxyFlowPanel.Margin = new Padding(0);
            proxyFlowPanel.Name = "proxyFlowPanel";
            proxyFlowPanel.Size = new System.Drawing.Size(75, 19);
            proxyFlowPanel.TabIndex = 10005;
            proxyFlowPanel.WrapContents = false;
            // 
            // proxyAllCheckBox
            // 
            proxyAllCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            proxyAllCheckBox.AutoSize = true;
            proxyAllCheckBox.Enabled = false;
            proxyAllCheckBox.Location = new System.Drawing.Point(2, 0);
            proxyAllCheckBox.Margin = new Padding(2, 0, 0, 0);
            proxyAllCheckBox.Name = "proxyAllCheckBox";
            proxyAllCheckBox.Size = new System.Drawing.Size(73, 19);
            proxyAllCheckBox.TabIndex = 4;
            proxyAllCheckBox.Text = "代理全部";
            proxyAllCheckBox.CheckedChanged += OnProxyAllCheckBoxChanged;
            // 
            // noneFoundLabel
            // 
            noneFoundLabel.Dock = DockStyle.Fill;
            noneFoundLabel.Location = new System.Drawing.Point(3, 19);
            noneFoundLabel.Name = "noneFoundLabel";
            noneFoundLabel.Size = new System.Drawing.Size(604, 265);
            noneFoundLabel.TabIndex = 1002;
            noneFoundLabel.Text = "No applicable programs nor games were found on your computer!";
            noneFoundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            noneFoundLabel.Visible = false;
            // 
            // blockedGamesFlowPanel
            // 
            blockedGamesFlowPanel.Anchor = AnchorStyles.Top;
            blockedGamesFlowPanel.AutoSize = true;
            blockedGamesFlowPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            blockedGamesFlowPanel.Controls.Add(blockedGamesCheckBox);
            blockedGamesFlowPanel.Controls.Add(blockProtectedHelpButton);
            blockedGamesFlowPanel.Location = new System.Drawing.Point(150, -1);
            blockedGamesFlowPanel.Margin = new Padding(0);
            blockedGamesFlowPanel.Name = "blockedGamesFlowPanel";
            blockedGamesFlowPanel.Size = new System.Drawing.Size(170, 19);
            blockedGamesFlowPanel.TabIndex = 1005;
            blockedGamesFlowPanel.WrapContents = false;
            // 
            // blockedGamesCheckBox
            // 
            blockedGamesCheckBox.AutoSize = true;
            blockedGamesCheckBox.Checked = true;
            blockedGamesCheckBox.CheckState = CheckState.Checked;
            blockedGamesCheckBox.Enabled = false;
            blockedGamesCheckBox.Location = new System.Drawing.Point(2, 0);
            blockedGamesCheckBox.Margin = new Padding(2, 0, 0, 0);
            blockedGamesCheckBox.Name = "blockedGamesCheckBox";
            blockedGamesCheckBox.Size = new System.Drawing.Size(148, 19);
            blockedGamesCheckBox.TabIndex = 1;
            blockedGamesCheckBox.Text = "黑名单游戏";
            blockedGamesCheckBox.UseVisualStyleBackColor = true;
            blockedGamesCheckBox.CheckedChanged += OnBlockProtectedGamesCheckBoxChanged;
            // 
            // blockProtectedHelpButton
            // 
            blockProtectedHelpButton.Enabled = false;
            blockProtectedHelpButton.Font = new System.Drawing.Font("Segoe UI", 7F);
            blockProtectedHelpButton.Location = new System.Drawing.Point(150, 0);
            blockProtectedHelpButton.Margin = new Padding(0, 0, 1, 0);
            blockProtectedHelpButton.Name = "blockProtectedHelpButton";
            blockProtectedHelpButton.Size = new System.Drawing.Size(19, 19);
            blockProtectedHelpButton.TabIndex = 2;
            blockProtectedHelpButton.Text = "?";
            blockProtectedHelpButton.UseVisualStyleBackColor = true;
            blockProtectedHelpButton.Click += OnBlockProtectedGamesHelpButtonClicked;
            // 
            // allCheckBoxLayoutPanel
            // 
            allCheckBoxLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            allCheckBoxLayoutPanel.AutoSize = true;
            allCheckBoxLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            allCheckBoxLayoutPanel.Controls.Add(allCheckBox);
            allCheckBoxLayoutPanel.Location = new System.Drawing.Point(562, -1);
            allCheckBoxLayoutPanel.Margin = new Padding(0);
            allCheckBoxLayoutPanel.Name = "allCheckBoxLayoutPanel";
            allCheckBoxLayoutPanel.Size = new System.Drawing.Size(42, 19);
            allCheckBoxLayoutPanel.TabIndex = 1006;
            allCheckBoxLayoutPanel.WrapContents = false;
            // 
            // allCheckBox
            // 
            allCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            allCheckBox.AutoSize = true;
            allCheckBox.Checked = true;
            allCheckBox.CheckState = CheckState.Checked;
            allCheckBox.Enabled = false;
            allCheckBox.Location = new System.Drawing.Point(2, 0);
            allCheckBox.Margin = new Padding(2, 0, 0, 0);
            allCheckBox.Name = "allCheckBox";
            allCheckBox.Size = new System.Drawing.Size(40, 19);
            allCheckBox.TabIndex = 4;
            allCheckBox.Text = "全选";
            allCheckBox.CheckedChanged += OnAllCheckBoxChanged;
            // 
            // selectionTreeView
            // 
            selectionTreeView.BackColor = System.Drawing.SystemColors.Control;
            selectionTreeView.BorderStyle = BorderStyle.None;
            selectionTreeView.CheckBoxes = true;
            selectionTreeView.Dock = DockStyle.Fill;
            selectionTreeView.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            selectionTreeView.Enabled = false;
            selectionTreeView.FullRowSelect = true;
            selectionTreeView.Location = new System.Drawing.Point(3, 19);
            selectionTreeView.Name = "selectionTreeView";
            selectionTreeView.Size = new System.Drawing.Size(604, 265);
            selectionTreeView.TabIndex = 1001;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new System.Drawing.Point(12, 344);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(610, 23);
            progressBar.TabIndex = 9;
            // 
            // progressLabel
            // 
            progressLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressLabel.Location = new System.Drawing.Point(12, 302);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new System.Drawing.Size(610, 15);
            progressLabel.TabIndex = 10;
            progressLabel.Text = "正在查找游戏DLC . . . 0%";
            // 
            // scanButton
            // 
            scanButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            scanButton.AutoSize = true;
            scanButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scanButton.Enabled = false;
            scanButton.Location = new System.Drawing.Point(186, 376);
            scanButton.Name = "scanButton";
            scanButton.Padding = new Padding(3, 0, 3, 0);
            scanButton.Size = new System.Drawing.Size(60, 25);
            scanButton.TabIndex = 10002;
            scanButton.Text = "选择游戏";
            scanButton.UseVisualStyleBackColor = true;
            scanButton.Click += OnScan;
            // 
            // uninstallButton
            // 
            uninstallButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            uninstallButton.AutoSize = true;
            uninstallButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            uninstallButton.Enabled = false;
            uninstallButton.Location = new System.Drawing.Point(420, 376);
            uninstallButton.Name = "uninstallButton";
            uninstallButton.Padding = new Padding(3, 0, 3, 0);
            uninstallButton.Size = new System.Drawing.Size(69, 25);
            uninstallButton.TabIndex = 10001;
            uninstallButton.Text = "卸载";
            uninstallButton.UseVisualStyleBackColor = true;
            uninstallButton.Click += OnUninstall;
            // 
            // progressLabelGames
            // 
            progressLabelGames.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressLabelGames.Font = new System.Drawing.Font("Segoe UI", 7F);
            progressLabelGames.Location = new System.Drawing.Point(12, 317);
            progressLabelGames.Name = "progressLabelGames";
            progressLabelGames.Size = new System.Drawing.Size(610, 12);
            progressLabelGames.TabIndex = 11;
            progressLabelGames.Text = " games (2): Game 1, Game 2";
            // 
            // progressLabelDLCs
            // 
            progressLabelDLCs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressLabelDLCs.Font = new System.Drawing.Font("Segoe UI", 7F);
            progressLabelDLCs.Location = new System.Drawing.Point(12, 329);
            progressLabelDLCs.Name = "progressLabelDLCs";
            progressLabelDLCs.Size = new System.Drawing.Size(610, 12);
            progressLabelDLCs.TabIndex = 12;
            progressLabelDLCs.Text = " DLC (2): 123456, 654321";
            // 
            // sortCheckBox
            // 
            sortCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            sortCheckBox.AutoSize = true;
            sortCheckBox.Location = new System.Drawing.Point(84, 380);
            sortCheckBox.Margin = new Padding(3, 0, 0, 0);
            sortCheckBox.Name = "sortCheckBox";
            sortCheckBox.Size = new System.Drawing.Size(98, 19);
            sortCheckBox.TabIndex = 10003;
            sortCheckBox.Text = "按名称排序";
            sortCheckBox.CheckedChanged += OnSortCheckBoxChanged;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.AutoSize = true;
            saveButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            saveButton.Enabled = false;
            saveButton.Location = new System.Drawing.Point(51, 0);
            saveButton.Margin = new Padding(6, 0, 6, 0);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(41, 25);
            saveButton.TabIndex = 10006;
            saveButton.Text = "保存";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += OnSaveSelections;
            // 
            // loadButton
            // 
            loadButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            loadButton.AutoSize = true;
            loadButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            loadButton.Enabled = false;
            loadButton.Location = new System.Drawing.Point(98, 0);
            loadButton.Margin = new Padding(0);
            loadButton.Name = "loadButton";
            loadButton.Size = new System.Drawing.Size(43, 25);
            loadButton.TabIndex = 10005;
            loadButton.Text = "加载";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += OnLoadSelections;
            // 
            // resetButton
            // 
            resetButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            resetButton.AutoSize = true;
            resetButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            resetButton.Enabled = false;
            resetButton.Location = new System.Drawing.Point(0, 0);
            resetButton.Margin = new Padding(0);
            resetButton.Name = "resetButton";
            resetButton.Size = new System.Drawing.Size(45, 25);
            resetButton.TabIndex = 10007;
            resetButton.Text = "刷新";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += OnResetSelections;
            // 
            // saveFlowPanel
            // 
            saveFlowPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            saveFlowPanel.AutoSize = true;
            saveFlowPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            saveFlowPanel.Controls.Add(resetButton);
            saveFlowPanel.Controls.Add(saveButton);
            saveFlowPanel.Controls.Add(loadButton);
            saveFlowPanel.Location = new System.Drawing.Point(263, 376);
            saveFlowPanel.Name = "saveFlowPanel";
            saveFlowPanel.Size = new System.Drawing.Size(141, 25);
            saveFlowPanel.TabIndex = 10008;
            saveFlowPanel.WrapContents = false;
            // 
            // SelectForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new System.Drawing.Size(634, 411);
            Controls.Add(saveFlowPanel);
            Controls.Add(sortCheckBox);
            Controls.Add(progressLabelDLCs);
            Controls.Add(progressLabelGames);
            Controls.Add(uninstallButton);
            Controls.Add(scanButton);
            Controls.Add(programsGroupBox);
            Controls.Add(progressBar);
            Controls.Add(cancelButton);
            Controls.Add(installButton);
            Controls.Add(progressLabel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectForm";
            StartPosition = FormStartPosition.Manual;
            Text = "SelectForm";
            Load += OnLoad;
            programsGroupBox.ResumeLayout(false);
            programsGroupBox.PerformLayout();
            proxyFlowPanel.ResumeLayout(false);
            proxyFlowPanel.PerformLayout();
            blockedGamesFlowPanel.ResumeLayout(false);
            blockedGamesFlowPanel.PerformLayout();
            allCheckBoxLayoutPanel.ResumeLayout(false);
            allCheckBoxLayoutPanel.PerformLayout();
            saveFlowPanel.ResumeLayout(false);
            saveFlowPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button installButton;
        private Button cancelButton;
        private GroupBox programsGroupBox;
        private ProgressBar progressBar;
        private Label progressLabel;
        internal CheckBox allCheckBox;
        private Button scanButton;
        private Label noneFoundLabel;
        private CustomTreeView selectionTreeView;
        private CheckBox blockedGamesCheckBox;
        private Button blockProtectedHelpButton;
        private FlowLayoutPanel blockedGamesFlowPanel;
        private FlowLayoutPanel allCheckBoxLayoutPanel;
        private Button uninstallButton;
        private Label progressLabelGames;
        private Label progressLabelDLCs;
        private CheckBox sortCheckBox;
        private FlowLayoutPanel proxyFlowPanel;
        internal CheckBox proxyAllCheckBox;
        private Button saveButton;
        private Button loadButton;
        private Button resetButton;
        private FlowLayoutPanel saveFlowPanel;
    }
}

