using System;
using System.Collections.Generic;
using System.Text;

namespace MusicMatchAPI
{ 

    public class Rootobject
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public Header header { get; set; }
        public Body body { get; set; }
    }

    public class Header
    {
        public int status_code { get; set; }
        public float execute_time { get; set; }
    }

    public class Body
    {
        public Lyrics lyrics { get; set; }
    }

    public class Lyrics
    {
        public int lyrics_id { get; set; }
        public int restricted { get; set; }
        public int instrumental { get; set; }
        public string lyrics_body { get; set; }
        public string lyrics_language { get; set; }
        public string script_tracking_url { get; set; }
        public string pixel_tracking_url { get; set; }
        public string html_tracking_url { get; set; }
        public string lyrics_copyright { get; set; }
        public DateTime updated_time { get; set; }
    }

}
