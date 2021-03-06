﻿using CofileUI.Classes;
using CofileUI.Windows;
using MahApps.Metro.IconPacks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CofileUI.UserControls
{
	/// <summary>
	/// ServerMenu.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class ServerMenu : UserControl
	{
		ServerGroupRootPanel panel_server;
		
		public ServerMenu()
		{
			InitializeComponent();

			DataContext = new ServerViewModel();
			panel_server = new ServerGroupRootPanel();
			grid_server.Children.Add(panel_server);

			this.ContextMenu = new ContextMenu();
			MenuItem item;

			item = new MenuItem();
			item.Click += BtnAddServerMenu_Click;
			item.Header = "Add Server Group";
			item.Icon = new PackIconMaterial()
			{
				Kind = PackIconMaterialKind.FolderPlus,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center
			};
			this.ContextMenu.Items.Add(item);
		}

		private void BtnAddServerMenu_Click(object sender, RoutedEventArgs e)
		{
			Window_AddServerMenu wms = new Window_AddServerMenu();
			Point pt = this.PointToScreen(new Point(0, 0));
			wms.Left = pt.X;
			wms.Top = pt.Y;
			if(wms.ShowDialog() == true)
			{
				string server_menu_name = wms.textBox_name.Text;
				
				if(panel_server.ServerViewModel.AddServerGroup(server_menu_name) != 0)
					return;

				ServerGroupPanel sgp = new ServerGroupPanel() { Content = server_menu_name };
				panel_server.AddChild(sgp);
			}
		}
	}
}
