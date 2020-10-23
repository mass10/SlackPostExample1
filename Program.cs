using System;

namespace SlackPostExample1
{
	class Program
	{
		static void Main(string[] args)
		{
			var channel = "notifications";
			var text = "これをさずけよう";
			var path = "IMG_3995.JPG";

			PostFileWithText(channel, text, path);
		}

		private static bool PostFileWithText(string channel, string text, string path)
		{
			var slackAccessToken = "";

			// ヘッダー
			var headers = new System.Collections.Generic.Dictionary<string, string>();
			headers.Add("Authorization", "Bearer " + slackAccessToken);

			var forms = new System.Collections.Generic.Dictionary<string, string>();
			forms.Add("initial_comment", text);
			forms.Add("channels", channel);

			var response = PostFile("https://slack.com/api/files.upload", headers, forms, path);
			Console.WriteLine(response);

			return true;
		}

		private static string PostFile(
			string url,
			System.Collections.Generic.IDictionary<string, string> headers,
			System.Collections.Generic.IDictionary<string, string> forms,
			string path)
		{
			// 「SSL/TLS のセキュリティで保護されているチャネルを作成できませんでした」の回避
			System.Net.ServicePointManager.SecurityProtocol =
				System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

			var client = new System.Net.Http.HttpClient();

			// ヘッダー
			foreach (var e in headers)
			{
				client.DefaultRequestHeaders.Add(e.Key, e.Value);
			}

			// マルチパートなフォームデータ
			var form = new FormData();
			form.AddFilePart("file", path);
			foreach (var e in forms)
			{
				form.AddFormData(e.Key, e.Value);
			}

			// 送信
			var response = client.PostAsync(url, form.GetForm()).Result;
			return response.Content.ReadAsStringAsync().Result;
		}
	}

	internal sealed class FormData
	{
		private System.Net.Http.MultipartFormDataContent _form = new System.Net.Http.MultipartFormDataContent();

		private static System.Net.Http.StreamContent CreateFilePart(string name, string path)
		{
			var attachment = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
			attachment.Name = name;
			attachment.FileName = System.IO.Path.GetFileName(path);

			var fileContent = new System.Net.Http.StreamContent(System.IO.File.OpenRead(path));
			fileContent.Headers.ContentDisposition = attachment;
			return fileContent;
		}

		public void AddFilePart(string name, string path)
		{
			var fileContent = CreateFilePart(name, path);
			this._form.Add(fileContent);
		}

		public void AddFormData(string name, string value)
		{
			var field = new System.Net.Http.StringContent(value);
			this._form.Add(field, name);
		}

		public System.Net.Http.MultipartFormDataContent GetForm()
		{
			return this._form;
		}
	}
}
