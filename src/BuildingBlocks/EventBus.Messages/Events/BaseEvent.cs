using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            //Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
        //public Guid Id { get; set; }
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
}
