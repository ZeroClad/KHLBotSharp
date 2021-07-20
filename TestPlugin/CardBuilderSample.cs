using KHLBotSharp.Core.Models;
using System;

namespace TestPlugin
{
    public class CardBuilderSample
    {
        public static string GetCard()
        {
            var builder = new CardBuilder();
            //Create first card, as we create card and pass arguments into it, it return us card builder to continue your card build
            Card secondCard = builder.Create(
                //Add section
                new SectionModule
                {
                    //Add plain text module
                    Text = new TextModule
                    {
                        Content = "纯文字"
                    }
                },
                new SectionModule
                {
                    //Add paragraph
                    Text = new ParagraphModule
                    {
                        Cols = 2,
                    }.AddFields(
                        //Add KMarkdown under paragraph
                        new KMarkdownModule
                        {
                            Content = "**测试第一行**"
                        },
                        new TextModule
                        {
                            Content = "第二行"
                        }
                        )
                    //Create second card, but this time we get the return result as card since we do nothing in the card create, hence we store it as variable for future build
                }).Create();
            secondCard.AddModules(
                //Add Header
                new HeaderModule { Text = new TextModule { Content = "图片测试" } },
                new ImageGroupModule().AddElements(
                    //Add image here, you can add multiple or single image
                    new ImageModule { Src = "https://img.kaiheila.cn/assets/2021-01/7kr4FkWpLV0ku0ku.jpeg" }),
                //Add divider
                new DividerModule(),
                //Add action group
                new ActionGroupModule().AddElements(
                    //Add button, you can add multiple buttons as well
                    new ButtonModule {
                        //Button Text
                        Text = new TextModule { Content = "这是按钮" }, 
                        //Button Color
                        Theme = CardTheme.Info,
                        //Value which will be sent to bot again
                        Value = "OK"
                    }
                    ),
                //Add context
                new ContextModule().AddElements(
                    new TextModule { Content = "小字 + 小图" },
                    new ImageModule { Src = "https://img.kaiheila.cn/assets/2021-01/7kr4FkWpLV0ku0ku.jpeg" }
                    ),
                //Add file modules
                new FileModule { Src = "https://img.kaiheila.cn/attachments/2021-01/21/600972b5d0d31.txt", Title = "这个是个文件.txt" },
                new VideoModule { Src = "https://img.kaiheila.cn/attachments/2021-01/20/6008127e8c8de.mp4", Title = "这个是个文件.mp4" },
                new AudioModule { Src = "https://img.kaiheila.cn/attachments/2021-01/21/600975671b9ab.mp3", Title = "这个是个文件.mp3" },
                //10 second count from now
                new CountdownModule { Mode = CountMode.Hour, EndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 10000 }
                );
            return builder.ToString();
        }
    }
}
