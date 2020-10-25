using System;

namespace SlackPostExample1
{
	internal sealed class Program
	{
		/// <summary>
		/// このアプリケーションのントリーポイント
		/// </summary>
		/// <param name="args">コマンドライン引数</param>
		static void Main(string[] args)
		{
			// コンフィギュレーション
			var conf = new Configuration();
			conf.Configure();

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
	}
}
