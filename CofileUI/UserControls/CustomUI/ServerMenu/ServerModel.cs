﻿using MahApps.Metro;
using MahApps.Metro.IconPacks;
using CofileUI.Classes;
using CofileUI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using CofileUI.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;

namespace CofileUI.UserControls
{
	public class ServerModel : ModelBase
	{
		private JProperty jpropData;
		public JProperty JpropData {
			get { return jpropData; }
		}

		private SSHManager sshManager = null;
		public SSHManager SshManager { get { return sshManager; } }
		public LinuxTreeView EncFileTree { get; set; }
		public LinuxTreeView DecFileTree { get; set; }
		
		public string Name
		{
			get { return JpropData?.Name; }
		}
		public string Ip
		{
			get { return JpropData?.Value?["ip"]?.ToString(); }
		}
		public int Port
		{
			get {
				var v = JpropData?.Value?["port"];
				int retval = -1;
				if(v != null)
					retval = Int32.Parse(v.ToString());
				return retval;
			}
		}
		public string Id { get; set; }
		
		public ServerModel(JProperty _data)
		{
			EncFileTree = new LinuxTreeView();
			DecFileTree = new LinuxTreeView();

			sshManager = new SSHManager(this);
			this.jpropData = _data;
		}
	}
}
