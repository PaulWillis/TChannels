
using System;
using System.Collections.Generic;

namespace CoreCodeTChannel.Data
{
  public class TChannel
  {
        public int Id { get; set; }
        public string ChannelName { get; set; }
        public string ChannelCallSign { get; set; }
        public string LDAPAbbrv { get; set; }
        public string OmniaChannelName { get; set; }
    }
}