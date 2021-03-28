using System;

namespace SlackPostExample1
{
	internal sealed class Program
	{
		/// <summary>
		/// このアプリケーションのントリーポイント
		/// </summary>
		/// <param name="args">コマンドライン引数</param>
		public static void Main(string[] args)
		{
			try
			{
				// テキストを投稿します。
				PostText("notifications", "こんにちは🌮");

				// ファイルを付けてテキストを投稿します。
				PostTextWithFile("notifications", "これをさずけよう", "IMG_3995.JPG");
			}
			catch (Exception e)
			{
				Console.WriteLine("[ERROR] {0}", e);
			}
		}

		/// <summary>
		/// ファイルを付けてテキストを投稿します。
		/// </summary>
		/// <param name="channel">チャネル</param>
		/// <param name="text">テキスト</param>
		/// <param name="file">ファイル</param>
		private static void PostTextWithFile(string channel, string text, string file)
		{
			// コンフィギュレーション
			var conf = Configuration.GetInstance();
			conf.Configure();

			var client = new SimpleClient();

			// トークン(※Slack Application のページから出力されたものを使用します)
			var slackAccessToken = conf.GetString("slack_accesskey");
			client.AddHeader("Authorization", "Bearer " + slackAccessToken);
			// 投稿先チャネル
			client.AddFormData("channels", channel);
			// メッセージ
			client.AddFormData("initial_comment", text);
			// 投稿するファイル
			client.AddFilePart("file", file);
			// リクエスト送出
			var response = client.Post("https://slack.com/api/files.upload");

			Console.WriteLine(response);
		}

		/// <summary>
		/// テキストを投稿します。
		/// </summary>
		/// <param name="channel">チャネル</param>
		/// <param name="text">テキスト</param>
		private static void PostText(string channel, string text)
		{
			// コンフィギュレーション
			var conf = Configuration.GetInstance();
			conf.Configure();

			var client = new SimpleClient();

			// トークン(※Slack Application のページから出力されたものを使用します)
			var slackAccessToken = conf.GetString("slack_accesskey");
			client.AddHeader("Authorization", "Bearer " + slackAccessToken);
			// 投稿先チャネル
			client.AddFormData("channel", channel);
			// メッセージ
			client.AddFormData("text", text);
			// リクエスト送出
			var response = client.Post("https://slack.com/api/chat.postMessage");

			Console.WriteLine(response);
		}
	}
}
