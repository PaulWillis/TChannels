using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeTChannel.Models
{
    public class TChannelModel
    {
        //Id,ChannelName,ChannelCallSign,LDAPAbbrv,OmniaChannelName

        [Required]
        [StringLength(100)]
        public string Id { get; set; }
        [Required]
        public string ChannelName { get; set; } 

        public string ChannelCallSign { get; set; }
        public string LDAPAbbrv { get; set; }
        public string OmniaChannelName { get; set; } 
    }
}
