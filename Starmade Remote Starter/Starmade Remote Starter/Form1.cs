using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;


namespace Starmade_Remote_Starter
{
    public partial class Form1 : Form
    {
        const int defaultPort = 3444;
        public SaveManager manager;
        List<ServerObject> savedServersList;

        public Form1()
        {
            InitializeComponent();
            manager = new SaveManager();
            savedServersList = new List<ServerObject>();
            savedServersList = manager.GetSavedData();
            serverListBox.DataSource = savedServersList;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            UdpClient startClient = new UdpClient();
            if (serverListBox.SelectedIndex != -1)
            {
                byte[] sendStart = Encoding.ASCII.GetBytes("start");

                try
                {
                    startClient.Send(sendStart, sendStart.Length, savedServersList[serverListBox.SelectedIndex].Address, defaultPort);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to send to start command check format of ip and port numbers" + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please select a server to start");
            }
            startClient.Close();

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (IPtxtBox.Text.Length == 0 || portTxtBox.Text.Length == 0)
            {
                MessageBox.Show("An IPaddress and port number is required");
            }
            else
            {
                savedServersList.Add(new ServerObject(portTxtBox.Text,IPtxtBox.Text));
                updateServerListBox();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (serverListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select an item saved server to delete");
            }
            else
            {
                savedServersList.RemoveAt(serverListBox.SelectedIndex);
                updateServerListBox();
            }
        }
        private void updateServerListBox()
        {
            manager.SaveServerObjects(savedServersList);
            savedServersList = manager.GetSavedData();
            serverListBox.DataSource = null;
            serverListBox.DataSource = savedServersList;
        }

    }
}
