using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;

namespace WallpaperCrawler
{
    class Program
    {
        /// <summary>
        /// 下載資料夾
        /// </summary>
        private static string downloadPath = ConfigurationManager.AppSettings["DownloadPath"];               

        /// <summary>
        /// 起始網頁
        /// </summary>
        private static string startPage = "https://support.microsoft.com/zh-tw/help/17780";

        /// <summary>
        /// 放chrome driver的資料夾
        /// </summary>
        private static string driverFolder = ConfigurationManager.AppSettings["DriverFolder"];

        private static IWebDriver driver = new ChromeDriver(driverFolder);

        static void Main(string[] args)
        {
            try
            {                
                driver.Url = startPage;

                List<string> links = new List<string>();

                //取得分類清單
                var sideLinks = driver.FindElements(By.CssSelector(".link-level-1 a"));
                foreach (var sideLink in sideLinks)
                {
                    string url = sideLink.GetAttribute("href");
                    links.Add(url);
                }

                foreach (var link in links)
                {
                    getImagesFromPage(link);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            //關閉瀏覽器
            Console.WriteLine("按Enter關閉瀏覽器");
            Console.ReadLine();
            driver.Quit();
        }

        private static void getImagesFromPage(string url)
        {
            driver.Url = url;
            string title = driver.FindElement(By.CssSelector(".c-heading-3")).Text;
            createFolder(title);

            var images = driver.FindElements(By.CssSelector(".table p a"));
            int count = images.Count;

            Console.WriteLine(string.Format("{0}: 共{1}筆", title, count));

            for(int i = 0; i < count; i++)
            {
                string imgSrc = images[i].GetAttribute("href");
                downlodImage(title, imgSrc);
                Console.Write(string.Format("{0}: 下載{1} 完成\n", i + 1, imgSrc));
            }                     
        }

        /// <summary>
        /// 在下載資料夾中建立分類資料夾
        /// </summary>
        /// <param name="title"></param>
        private static void createFolder(string title)
        {            
            string pathString = System.IO.Path.Combine(downloadPath, title);
            System.IO.Directory.CreateDirectory(pathString);
        }

        /// <summary>
        /// 從圖片網址下載圖片
        /// </summary>
        /// <param name="title">資料夾名稱</param>
        /// <param name="imgSrc"></param>
        private static void downlodImage(string title, string imgSrc)
        {
            string fileName = Path.GetFileName(imgSrc);
            string localFilename = string.Format("{0}{1}\\{2}{3}", downloadPath, title, fileName, ".jpg"); //TODO: 如何得知是jpg?
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(imgSrc, localFilename);
            }
        }
    }
}
