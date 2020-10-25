using System;

namespace SlackPostExample1
{
	class Program
	{
		static void Main(string[] args)
		{
			var conf = new Configuration();
			conf.Configure();

			// 投稿先チャネル
			var channel = "notifications";
			// メッセージ
			var text = "これをさずけよう";
			// ファイルのパス
			var path = "IMG_3995.JPG";
			// トークン(※Slack Application のページから出力されたものを使用します)
			var slackAccessToken = conf.GetString("slack_accesskey");

			var client = new SimpleClient();

			client.AddHeader("Authorization", "Bearer " + slackAccessToken);

			client.AddFormData("initial_comment", text);
			client.AddFormData("channels", channel);
			
			client.AddFilePart("file", path);

			var response = client.Post("https://slack.com/api/files.upload");

			Console.WriteLine(response);
		}
	}
}
