using BankTransactionSolution.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankTransactionSolution.Domain.Entities
{
    public class AuditLogs : BaseEntity
    {
        public AuditLogs(int user_id, UserAction user_action)
        {
            this.user_id = user_id;
            this.user_action = user_action;
        }
        public int user_id { get; set; }    
        public UserAction user_action { get; set; } 
        public string user_action_text => user_action.ToString();

        [JsonIgnore]
        public virtual User user { get; set; }  
    }
}
