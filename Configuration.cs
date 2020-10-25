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
		/// コンストラクター
		/// </summary>
		public Configuration()
		{

		}

		/// <summary>
		/// カレントディレクトリのファイル .settings.json でコンフィギュレーションを試みます。
		/// </summary>
		public void Configure()
		{
			const string path = ".settings.json";
			string content = System.IO.File.ReadAllText(path, Encoding.UTF8);
			var unknown_data = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
			this._settings = (Newtonsoft.Json.Linq.JObject)unknown_data;

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
