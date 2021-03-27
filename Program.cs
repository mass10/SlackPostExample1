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
				// コンフィギュレーション
				var conf = Configuration.GetInstance();
				conf.Configure();

				// テキストを投稿します。
				PostText();

				// ファイルを付けてテキストを投稿します。
				PostTextWithFile();
			}
			catch (Exception e)
			{
				Console.WriteLine("[ERROR] {0}", e);
			}
		}

		/// <summary>
		/// ファイルを付けてテキストを投稿します。
		/// </summary>
		private static void PostTextWithFile()
		{
			// コンフィギュレーション
			var conf = Configuration.GetInstance();

			var client = new SimpleClient();

			// トークン(※Slack Application のページから出力されたものを使用します)
			var slackAccessToken = conf.GetString("slack_accesskey");
			client.AddHeader("Authorization", "Bearer " + slackAccessToken);

			// 投稿先チャネル
			client.AddFormData("channels", "notifications");

			// メッセージ
			client.AddFormData("initial_comment", "これをさずけよう");

			// 投稿するファイル
			client.AddFilePart("file", "IMG_3995.JPG");

			// リクエスト送出
			var response = client.Post("https://slack.com/api/files.upload");

			Console.WriteLine(response);
		}

		/// <summary>
		/// テキストを投稿します。
		/// </summary>
		private static void PostText()
		{
			// コンフィギュレーション
			var conf = Configuration.GetInstance();

			var client = new SimpleClient();

			// トークン(※Slack Application のページから出力されたものを使用します)
			var slackAccessToken = conf.GetString("slack_accesskey");
			client.AddHeader("Authorization", "Bearer " + slackAccessToken);

			// 投稿先チャネル
			client.AddFormData("channel", "notifications");

			// メッセージ
			client.AddFormData("text", "こんにちは🌮");

			// リクエスト送出
			var response = client.Post("https://slack.com/api/chat.postMessage");

			Console.WriteLine(response);
		}
	}
}
