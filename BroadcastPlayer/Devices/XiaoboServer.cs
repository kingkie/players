using CinemaSetter.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastPlayer
{
	public class XiaoboServer
	{
		public int ServerPort
		{
			get;
			set;
		}

		public string ServerIP
		{
			get;
			set;
		}

		public static XiaoboServer LoadFromXML(string XMLFilename)
		{
			XiaoboServer xiaoboServer = Tools.DESerializer<XiaoboServer>(Tools.file_get_content(XMLFilename));
			bool flag = xiaoboServer == null;
			if (flag)
			{
				xiaoboServer = new XiaoboServer();
			}
			return xiaoboServer;
		}

		public bool SaveToXMLFile(string XMLFilename)
		{
			string content = Tools.XmlSerialize<XiaoboServer>(this);
			return Tools.file_put_content(XMLFilename, content);
		}
	}
}
