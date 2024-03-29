﻿using AdaTech.CodeManager.Model;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdaTech.CodeManager
{
    public partial class SelectTeam : Form
    {
        private static TechLead currentUser = (TechLead)Session.getInstance.GetCurrentUser();
        private static List<Team>? userTeams = new List<Team>();

        public SelectTeam()
        {
            InitializeComponent();
            userTeams = TeamData.FindTeamsByTechLead(currentUser);
            ShowTeams();
            lbHello.Text = $"Hello {currentUser.Name},";
        }

        public void ShowTeams()
        {
            int index = 1;

            foreach (Team team in userTeams)
            {
                var pnTeam = CustomizePnTeam();
                pnTeam.Click += (sender, e) => PnTeam_Click(team);

                pnTeam.Controls.Add(CostumizeLbTeamName(team.Name));
                pnTeam.Controls.Add(CostumizeLbTeamID(index++));
                pnTeam.Controls.Add(CostumizeLbTeamMembers(team.TeamMembersID.Count));

                conteinerTeams.Controls.Add(pnTeam);
            }
        }

        private void PnTeam_Click(Team team)
        {
            Hide();

            if(btnEditTeam.Enabled == false)
            {
                new ManageTeam(team).ShowDialog();
                return;
            }

            new DashboardTL(team).ShowDialog();
        }

        private void OnBtnCreateTeamClick(object sender, EventArgs e)
        {
            Hide();
            new ManageTeam().ShowDialog();
        }

        private void OnBtnBackClick(object sender, EventArgs e)
        {
            Close();
            Session.getInstance.EndSession();
            new LoginPage().ShowDialog();
        }


        private void OnPnTeamMouseEnter(object sender, EventArgs e)
        {
            if (sender is Guna2GradientPanel pnTeam)
            {
                CustomizePanelOnMouseEnter(pnTeam);
            }
        }

        private void OnPnTeamMouseLeave(object sender, EventArgs e)
        {
            if (sender is Guna2GradientPanel pnTeam)
            {
                CustomizePanelOnMouseLeave(pnTeam);
            }
        }
        private void OnBtnEditTeamClick(object sender, EventArgs e)
        {
            btnCreateTeam.Enabled = false;
            btnEditTeam.Enabled = false;
            btnBack.Enabled = false;
            lbSelectTeam.BackColor = Color.DarkGray;
            lbEditMessage.Text = "Select the team you want to edit";

        }

        #region UI Element Builders
        private void CustomizePanelOnMouseEnter(Guna2GradientPanel pnTeam)
        {
            pnTeam.FillColor = Color.FromArgb(252, 239, 239);
            pnTeam.FillColor2 = Color.FromArgb(252, 239, 239);
        }

        private void CustomizePanelOnMouseLeave(Guna2GradientPanel pnTeam)
        {
            pnTeam.FillColor = Color.FromArgb(251, 152, 51);
            pnTeam.FillColor2 = Color.FromArgb(251, 152, 51);
        }

        private Guna2GradientPanel CustomizePnTeam()
        {
            var pnTeam = new Guna2GradientPanel();
            pnTeam.BackColor = Color.FromArgb(27, 32, 46);
            pnTeam.BorderColor = Color.FromArgb(255, 255, 192);
            pnTeam.BorderRadius = 15;
            pnTeam.FillColor = Color.FromArgb(251, 152, 51);
            pnTeam.FillColor2 = Color.FromArgb(251, 152, 51);
            pnTeam.Size = new Size(251, 141);
            pnTeam.Margin = new Padding(10);
            pnTeam.MouseEnter += OnPnTeamMouseEnter;
            pnTeam.MouseLeave += OnPnTeamMouseLeave;

            return pnTeam;
        }

        private Label CostumizeLbTeamName(string teamName)
        {
            Label lbTeamName = new Label();
            lbTeamName.Text = teamName;
            lbTeamName.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbTeamName.Location = new Point(33, 38);
            lbTeamName.BackColor = Color.FromArgb(1, 37, 56);
            lbTeamName.AutoSize = true;
            lbTeamName.ForeColor = Color.White;

            return lbTeamName;
        }

        private Label CostumizeLbTeamMembers(int numberOfMembers)
        {
            Label lbTeamMembers = new Label();
            lbTeamMembers.Text = $"{numberOfMembers} members";
            lbTeamMembers.Font = new Font("Century Gothic", 8, FontStyle.Regular);
            lbTeamMembers.Location = new Point(33, 76);
            lbTeamMembers.BackColor = Color.FromArgb(1, 37, 56);
            lbTeamMembers.ForeColor = Color.White;
            lbTeamMembers.AutoSize = true;

            return lbTeamMembers;
        }

        private Label CostumizeLbTeamID(int teamID)
        {
            Label lbTeamID = new Label();
            lbTeamID.Text = $"#{teamID}";
            lbTeamID.Font = new Font("Century Gothic", 8, FontStyle.Regular);
            lbTeamID.Location = new Point(181, 97);
            lbTeamID.BackColor = Color.FromArgb(1, 37, 56);
            lbTeamID.ForeColor = Color.White;
            lbTeamID.AutoSize = true;
            teamID++;

            return lbTeamID;
        }
        #endregion


    }
}
