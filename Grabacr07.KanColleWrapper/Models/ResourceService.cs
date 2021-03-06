﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Properties;
using Livet;

namespace Grabacr07.KanColleWrapper.Models
{
	public class ResourceServiceWrapper : NotificationObject
	{
		#region static members

		private static readonly ResourceServiceWrapper current = new ResourceServiceWrapper();

		public static ResourceServiceWrapper Current
		{
			get { return current; }
		}

		#endregion

		/// <summary>
		/// サポートされているカルチャの名前。
		/// </summary>
		private readonly string[] supportedCultureNames =
		{
			"ja", // Resources.resx
			"en",
			"zh-CN",
			"zh-TW",
		};

		/// <summary>
		/// 多言語化されたリソースを取得します。
		/// </summary>
		internal Resources Resources { get; private set; }

		/// <summary>
		/// サポートされているカルチャを取得します。
		/// </summary>
		public IReadOnlyCollection<CultureInfo> SupportedCultures { get; private set; }

		/// <summary>
		/// サポートされているカルチャを取得します。
		/// </summary>
		private ResourceServiceWrapper()
		{
			this.Resources = new Resources();
			this.SupportedCultures = this.supportedCultureNames
				.Select(x =>
				{
					try
					{
						return CultureInfo.GetCultureInfo(x);
					}
					catch (CultureNotFoundException)
					{
						return null;
					}
				})
				.Where(x => x != null)
				.ToList();
		}

		/// <summary>
		/// 指定されたカルチャ名を使用して、リソースのカルチャを変更します。
		/// </summary>
		/// <param name="name">カルチャの名前。</param>
		public void ChangeCulture(string name)
		{
			Resources.Culture = this.SupportedCultures.SingleOrDefault(x => x.Name == name);

			this.RaisePropertyChanged("Resources");
		}
	}
}
