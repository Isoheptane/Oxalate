using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Net.Sockets;
using JsonSharp;

namespace Oxalate.Standard
{
    public partial class Packet
    {
        protected JsonObject content;
        
        /// <summary>
        /// Create a packet with empty content.
        /// </summary>
        public Packet()
        {
            content = new JsonObject();
        }

        /// <summary>
        /// Create a packet with indicated JSON content.
        /// </summary>
        /// <param name="content">JSON content</param>
        public Packet(JsonObject content)
        {
            this.content = content;
        }

        public JsonValue this[string index]
        {
            get { return content[index]; }
            set { content[index] = value; }
        }

        public JsonObject Content
        {
            get { return content; }
            set { content = value; }
        }

        public override string ToString()
        {
            return Content.ToString();
        }

    }
}
