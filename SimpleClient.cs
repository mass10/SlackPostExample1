using System;
using System.Collections.Generic;
using System.Text;

namespace SlackPostExample1
{
	/// <summary>
	/// 簡易 HTTP クライアントです。
	/// </summary>
	internal sealed class SimpleClient
	{
		/// <summary>
		/// HTTP クライアント
		/// </summary>
		private System.Net.Http.HttpClient _client = null;

		/// <summary>
		/// フォームデータ
		/// </summary>
		private System.Net.Http.MultipartFormDataContent _form = null;

		/// <summary>
		/// コンストラクター
		/// </summary>
		public SimpleClient()
		{
			// 「SSL/TLS のセキュリティで保護されているチャネルを作成できませんでした」の回避
			System.Net.ServicePointManager.SecurityProtocol =
				System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
		}

		/// <summary>
		/// リクエストにヘッダー情報を追加します。
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void AddHeader(string name, string value)
		{
			var client = this.GetHttpClient();
			client.DefaultRequestHeaders.Add(name, value);
		}

		/// <summary>
		/// リクエストにフォームデータを追加します。
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void AddFormData(string name, string value)
		{
			var field = new System.Net.Http.StringContent(value);
			var form = this.GetForm();
			form.Add(field, name);
		}

		/// <summary>
		/// リクエストにファイルデータを追加します。
		/// </summary>
		/// <param name="name"></param>
		/// <param name="path"></param>
		public void AddFilePart(string name, string path)
		{
			var attachment = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
			attachment.Name = name;
			attachment.FileName = System.IO.Path.GetFileName(path);

			var fileContent = new System.Net.Http.StreamContent(System.IO.File.OpenRead(path));
			fileContent.Headers.ContentDisposition = attachment;

			var form = this.GetForm();
			form.Add(fileContent);
		}

		/// <summary>
		/// POST
		/// </summary>
		/// <param name="url">URL</param>
		/// <returns></returns>
		public string Post(string url)
		{
			try
			{
				var form = this.GetForm();
				var client = this.GetHttpClient();
				var response = client.PostAsync(url, form).Result;
				return response.Content.ReadAsStringAsync().Result;
			}
			finally
			{
				this.Close();
			}
		}

		/// <summary>
		/// 現在のセッションをクローズします。
		/// </summary>
		private void Close()
		{
			this._client = null;
			this._form = null;
		}

		private System.Net.Http.HttpClient GetHttpClient()
		{
			if (this._client != null)
				return this._client;
			this._client = new System.Net.Http.HttpClient();
			return this._client;
		}

		/// <summary>
		/// フォームオブジェクトを取得します。
		/// </summary>
		/// <returns></returns>
		private System.Net.Http.MultipartFormDataContent GetForm()
		{
			if (this._form != null)
				return this._form;
			this._form = new System.Net.Http.MultipartFormDataContent();
			return this._form;
		}
	}
}
