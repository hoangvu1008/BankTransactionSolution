using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransactionSolution.Domain.Entities
{
    public class BaseEntity
    {
        public int id {  get; set; }    
        public bool is_deleted { get; set; }   = false;
        public DateTime date_created { get; set; } = DateTime.Now;
        public DateTime date_updated { get; set; } = DateTime.Now;
    }
}
