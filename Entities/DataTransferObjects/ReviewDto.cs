using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Star { get; set; }
        public string Status { get; set; }
        public string OparatedBy { get; set; }
        public string UserId { get; set; }
        
    }
}
