# WallpaperCrawler
用C#寫的網路爬蟲，以下載
https://support.microsoft.com/zh-tw/help/17780/featured-wallpapers
的桌布為例。

其實原本想寫個備忘爬蟲的文章，不過不知道要爬什麼東西。我想既然是用.net寫的就來爬微軟的網站吧。實際在爬的時候發現了這個網站的資料內容似乎還是用javascript加載的。

1. nuget安裝Selenium套件
1. 你可能會遇到Chrome Driver沒有實作的問題，這個時候請下載chrome drive，並如下
`IWebDriver driver = new ChromeDriver(這裡填入你chrome driver的路徑);`

## 如何找出需要的資料
1. 以這篇教學的目的來看，桌布有許多類別，每一個類別又有許多圖片。我們要做的是造訪所有類別的頁面，並把每個頁面的圖片抓下來。

1. 首先要取得頁面的連結清單，這個從左邊的尋覽列就可以得到。打開你瀏覽器的開發者工具，找出你要造訪的連結的共同特徵。我們可以發現用`.link-level-1 a`這個CSS選擇器找出這些連結，以此就能造訪所有類別桌布的頁面。

1. 用同樣的方法，我們可以在頁面裡找出所有需要的圖片連結。

1. 下載圖片

## 參考資料
* http://toolsqa.com/selenium-webdriver/browser-commands/