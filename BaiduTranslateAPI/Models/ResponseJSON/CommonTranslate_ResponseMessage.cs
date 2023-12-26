namespace DJDQfff.BaiduTranslateAPI.Models.ResponseJSON
{
    /// <summary> 百度服务器返回的翻译结果JSON </summary>
    public class CommonTranslate_ResponseMessage
    {
        /// <summary> 源语种 </summary>
        public string from { set; get; }

        /// <summary> 翻译语种 </summary>
        public string to { set; get; }

        /// <summary>
        /// 服务器返回的翻译结果json
        /// </summary>
        public trans_result[] trans_result { set; get; }
    }

    /// <summary> 翻译内容 </summary>
#pragma warning disable IDE1006 // 命名样式

    public class trans_result
#pragma warning restore IDE1006 // 命名样式
    {
        /// <summary> 源文本 </summary>
        public string src { set; get; }

        /// <summary> 翻译文本 </summary>
        public string dst { set; get; }
    }
}