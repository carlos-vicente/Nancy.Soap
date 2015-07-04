using System.Collections.Generic;

namespace WSDL.Models.Message
{
    public class Message
    {
        public string Name { get; set; }

        public IEnumerable<MessagePart> Parts { get; set; }

        public Message()
        {
            Parts = new List<MessagePart>();
        }
    }
}