﻿using CofileUI.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using MS.Internal.AppModel;
using MS.Win32;
using MahApps.Metro.IconPacks;
using Renci.SshNet.Sftp;
using System.Text.RegularExpressions;
using CofileUI.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using System.Windows.Controls.Primitives;
using Newtonsoft.Json.Linq;
using MahApps.Metro.Controls.Dialogs;
using CofileUI.UserControls.ConfigOptions;

namespace CofileUI.UserControls
{
	/// <summary>
	/// Cofile.xaml에 대한 상호 작용 논리
	/// </summary>

	public partial class DecryptTab : UserControl
	{
		public static DecryptTab current;
		public bool bUpdated = false;
		ConfigMenuRootPanel configMenu = new ConfigMenuRootPanel();

		public DecryptTab()
		{
			InitializeComponent();

			current = this;
			
			this.Loaded += (sender, e) => {
				if(!bUpdated)
					DecryptTab.current.Refresh();
			};
			this.IsVisibleChanged += (sender, e) =>
			{
				if(this.IsVisible)
				{
					if(this.bUpdated)
						configMenu.Refresh();
					else
						this.Refresh();
				}
			};
			grid_config_menu.Children.Add(configMenu);
		}
		public int Refresh()
		{
			if(WindowMain.current == null)
				return -1;

			if(WindowMain.current?.EnableConnect?.SshManager?.IsConnected != true)
				return -2;

			if(WindowMain.current?.EnableConnect?.DecFileTree == null)
				return -3;

			grid_treeView_linux_directory.Children.Clear();
			grid_treeView_linux_directory.Children.Add(WindowMain.current?.EnableConnect?.DecFileTree);
			configMenu.Refresh();

			Log.PrintLog("[refresh]", "UserControls.Cofile.Refresh");

			bUpdated = true;

			return 0;
		}
		public void Clear()
		{
			configMenu.Clear();

			WindowMain.current?.EnableConnect?.DecFileTree?.Clear();
			grid_treeView_linux_directory.Children.Clear();
		}
	}
}
