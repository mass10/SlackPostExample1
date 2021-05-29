using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SlackPostExample1
{
	/// <summary>
	/// コンフィギュレーションクラスです。
	/// </summary>
	internal sealed class Configuration
	{
		/// <summary>
		/// 読み取った JSON の保管用
		/// </summary>
		private Newtonsoft.Json.Linq.JObject _settings = null;

		/// <summary>
		/// 唯一のインスタンス
		/// </summary>
		private static readonly Configuration _instance = new Configuration();

		/// <summary>
		/// コンストラクター
		/// </summary>
		private Configuration()
		{

		}

		/// <summary>
		/// インスタンスを返します。返却されるのは常に同一のインスタンスです。
		/// </summary>
		/// <returns>インスタンス</returns>
		public static Configuration GetInstance()
		{
			return _instance;
		}

		/// <summary>
		/// カレントディレクトリのファイル .settings.json でコンフィギュレーションを試みます。
		/// </summary>
		public void Configure()
		{
			const string path = ".settings.json";
			string content = System.IO.File.ReadAllText(path, Encoding.UTF8);
			var unknownData = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
			this._settings = (Newtonsoft.Json.Linq.JObject)unknownData;
		}

		/// <summary>
		/// 対象の設定項目を文字列として返却します。
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetString(string key)
		{
			return "" + this._settings[key];
		}
	}
}
